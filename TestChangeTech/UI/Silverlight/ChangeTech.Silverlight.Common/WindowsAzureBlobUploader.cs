using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml;
using System.Net.Browser;


namespace ChangeTech.Silverlight.Common
{
    public delegate void ReportUploadProgressDelegate(int percentage);

    public class WindowsAzureBlobUploader
    {
        private string _uploadContainerURL;
        private long _dataSent;
        private long _dataLength;
        private FileInfo _file;
        private FileStream _fileStream;

        private const long _chunkSize = 102400; //4194304;
        private bool _useBlock;

        private string currentBlockId;
        private List<string> blockIds = new List<string>();

        public event ReportUploadProgressDelegate ReportUploadProgressEventHandler;
        public event EventHandler FinishUploadProgressEventHandler;
        public event EventHandler ReportErrorEventHandler;

        public WindowsAzureBlobUploader(FileInfo fileToUpload, string uploadContianerURL)
        {
            this._uploadContainerURL = uploadContianerURL;
            _file = fileToUpload;
            _dataLength = fileToUpload.Length;
            _dataSent = 0;

            if (_dataLength > _chunkSize)
            {
                _useBlock = true;
            }
            else
            {
                _useBlock = false;
            }

            _fileStream = _file.OpenRead();
            _fileStream.Position = 0;
        }

        public void Upload()
        {
            UriBuilder blobURL = new UriBuilder(_uploadContainerURL);
            if (_useBlock)
            {
                // encode the block name and add it to the query string
                currentBlockId = Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()));
                blobURL.Query = blobURL.Query.TrimStart('?') +
                    string.Format("&comp=block&blockid={0}", currentBlockId);
            }

            HttpWebRequest webRequest = (HttpWebRequest)WebRequestCreator.ClientHttp.Create(blobURL.Uri);
            webRequest.Method = "PUT";
            webRequest.BeginGetRequestStream(new AsyncCallback(WriteToStreamCallback), webRequest);
        }

        // write up to ChunkSize of data to the web request
        private void WriteToStreamCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest webRequest = (HttpWebRequest)asynchronousResult.AsyncState;
            Stream requestStream = webRequest.EndGetRequestStream(asynchronousResult);
            byte[] buffer = new Byte[4096];
            int bytesRead = 0;
            int tempTotal = 0;

            _fileStream.Position = _dataSent;
            while ((bytesRead = _fileStream.Read(buffer, 0, buffer.Length)) != 0 && tempTotal + bytesRead < _chunkSize)
            {
                requestStream.Write(buffer, 0, bytesRead);
                requestStream.Flush();

                _dataSent += bytesRead;
                tempTotal += bytesRead;

                int percentage = (int)(_dataSent * 100 / _dataLength);
                if (ReportUploadProgressEventHandler != null)
                {
                    ReportUploadProgressEventHandler(percentage);
                }
            }

            requestStream.Close();
            webRequest.BeginGetResponse(new AsyncCallback(ReadHttpResponseCallback), webRequest);
        }

        private void ReadHttpResponseCallback(IAsyncResult asynchronousResult)
        {
            bool error = false;

            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)asynchronousResult.AsyncState;
                HttpWebResponse webResponse = (HttpWebResponse)webRequest.EndGetResponse(asynchronousResult);
                StreamReader reader = new StreamReader(webResponse.GetResponseStream());

                string responsestring = reader.ReadToEnd();
                reader.Close();
            }
            catch
            {
                error = true;
            }

            if (!error)
            {
                blockIds.Add(currentBlockId);

                // if there's more data, send another request
                if (_dataSent < _dataLength)
                {
                    Upload();
                }
                else // all done
                {
                    _fileStream.Close();
                    _fileStream.Dispose();

                    if (_useBlock)
                    {
                        PutBlockList(); // commit the blocks into the blob
                    }
                    else
                    {
                        if (FinishUploadProgressEventHandler != null)
                        {
                            FinishUploadProgressEventHandler(this, new EventArgs());
                        }
                    }
                }
            }
            else
            {
                if (ReportErrorEventHandler != null)
                {
                    ReportErrorEventHandler(this, new EventArgs());
                }
            }
        }

        private void PutBlockList()
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequestCreator.ClientHttp.Create(
                new Uri(string.Format("{0}&comp=blocklist", _uploadContainerURL)));
            webRequest.Method = "PUT";
            webRequest.Headers["x-ms-version"] = "2009-09-19"; // x-ms-version is required for put block list!
            webRequest.BeginGetRequestStream(new AsyncCallback(BlockListWriteToStreamCallback), webRequest);
        }

        private void BlockListWriteToStreamCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest webRequest = (HttpWebRequest)asynchronousResult.AsyncState;
            Stream requestStream = webRequest.EndGetRequestStream(asynchronousResult);

            var document = new XDocument(
                new XElement("BlockList",
                    from blockId in blockIds
                    select new XElement("Uncommitted", blockId)));
            var writer = XmlWriter.Create(requestStream, new XmlWriterSettings() { Encoding = Encoding.UTF8 });
            document.Save(writer);
            writer.Flush();

            requestStream.Close();

            webRequest.BeginGetResponse(new AsyncCallback(BlockListReadHttpResponseCallback), webRequest);
        }

        private void BlockListReadHttpResponseCallback(IAsyncResult asynchronousResult)
        {
            bool error = false;

            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)asynchronousResult.AsyncState;
                HttpWebResponse webResponse = (HttpWebResponse)webRequest.EndGetResponse(asynchronousResult);
                StreamReader reader = new StreamReader(webResponse.GetResponseStream());

                string responsestring = reader.ReadToEnd();
                reader.Close();
            }
            catch
            {
                error = true;
            }

            if (!error)
            {
                if (FinishUploadProgressEventHandler != null)
                {
                    FinishUploadProgressEventHandler(this, new EventArgs());
                }
            }
            else
            {
                if (ReportErrorEventHandler != null)
                {
                    ReportErrorEventHandler(this, new EventArgs());
                }
            }
        }
    }
}