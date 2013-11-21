using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;
using System.Diagnostics;
using System.Threading;
using Ethos.Utility;

namespace ChangeTech.LF.Web.Handler
{
    /// <summary>
    /// Summary description for ConvertToPdfHandler
    /// </summary>
    public class ConvertToPdfHandler : IHttpHandler
    {
        private const string RESULT_PAGE = "minhalsoprofil.html";
        private const string RESULT_PAGE_PDF = "minhalsoprofil-pdf.html";
        private const string RESPONSE_SUCCESS_MESSAGE = "success";
        private const string RESPONSE_FAIL_MESSAGE = "fail";
        private string needConvertHtmlPage = string.Empty;
        private string requestUrl = string.Empty;
        public HttpContext currentContext;
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                currentContext = context;
                requestUrl = context.Request.Params["requestUrl"];
                string fileNameWithoutExtention = Guid.NewGuid().ToString();
                string pdfSavePath = "../3rdParty/" + fileNameWithoutExtention + ".pdf";
                string savePath = context.Server.MapPath(pdfSavePath);
                string fileName = fileNameWithoutExtention + ".pdf";
                if (requestUrl.Contains(RESULT_PAGE))
                {
                    needConvertHtmlPage = requestUrl.Replace(RESULT_PAGE, RESULT_PAGE_PDF);
                }
                if (HtmlPageConvertToPDF(needConvertHtmlPage, savePath))
                {
                    DownLoadPDF(savePath, fileName);
                }
                else
                {
                    context.Response.Write("<script type='text/javascript'>alert('Converted to the PDF version fail !');</script>");
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogMessage(string.Format("Method: {0},  Exception Message:{1}, Request URL: {2}", "ConvertToPdfHandler", ex, context.Request.Url.ToString()));
            }
        }

        

        public bool HtmlPageConvertToPDF(string url, string path)
        {
            bool flag = false;
            try
            {
                if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(path)) return false;
                Process process = new Process();
                var wkhtmlExePath = System.Configuration.ConfigurationManager.AppSettings["ToolPath"];
                wkhtmlExePath = currentContext.Server.MapPath(wkhtmlExePath);
                if (File.Exists(wkhtmlExePath))
                {
                    System.Diagnostics.ProcessStartInfo Pss = new ProcessStartInfo();
                    Pss.FileName = wkhtmlExePath;
                    Pss.Arguments = string.Format("{0} {1}", url, path);
                    Pss.UseShellExecute = false;
                    Pss.RedirectStandardInput = true;
                    Pss.RedirectStandardOutput = true;
                    //Pss.RedirectStandardError = true;
                    Pss.CreateNoWindow = true;

                    using (System.Diagnostics.Process PS = new Process())
                    {
                        PS.StartInfo = Pss;
                        PS.Start();
                        PS.WaitForExit();

                        if (PS.ExitCode == 0)
                        {
                            flag = true;
                            PS.Close();
                        }
                    }

                    //process.StartInfo.FileName = wkhtmlExePath;
                    //process.StartInfo.Arguments = " \"" + url + "\" " + path;
                    //process.StartInfo.UseShellExecute = false;
                    //process.StartInfo.RedirectStandardInput = true;
                    //process.StartInfo.RedirectStandardOutput = true;
                    //process.StartInfo.RedirectStandardError = true;
                    //process.StartInfo.CreateNoWindow = true;
                    //process.Start();

                    //process.WaitForExit(10000);
                    //process.Close();
                    //if (ConfirmConvertSuccess(path))
                    //{
                    //    flag = true;
                    //}
                }
            }
            catch (Exception ex)
            {
                flag = false;
                LogUtility.LogUtilityIntance.LogMessage(string.Format("Method: {0},  Exception Message:{1}", "HtmlPageConvertToPDF", ex.Message));
            }

            return flag;
        }

        private void DownLoadPDF(string filePath, string fileName)
        {
            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Open);
                BinaryReader br = new BinaryReader(fs);
                byte[] BynFile = new byte[br.BaseStream.Length];
                br.BaseStream.Seek(0, SeekOrigin.Begin);
                br.Read(BynFile, 0, (int)br.BaseStream.Length);
                fs.Close();

                currentContext.Response.Buffer = true;
                currentContext.Response.Clear();
                currentContext.Response.Charset = "UTF-8";
                currentContext.Response.ContentEncoding = System.Text.Encoding.UTF8;
                currentContext.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));//强制下载
                currentContext.Response.ContentEncoding = System.Text.Encoding.UTF8;
                currentContext.Response.ContentType = "application/pdf";
                currentContext.Response.BinaryWrite(BynFile);
                System.IO.FileInfo file = new System.IO.FileInfo(filePath);
                if (file.Exists)
                {
                    file.Delete();
                }

                currentContext.Response.Flush();
                currentContext.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogMessage(string.Format("Method: {0},  Exception Message:{1}", "DownLoadPDF", ex.Message));
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


        #region HtmlToPdf
        /// <summary>
        /// HTML Convert to PDF
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="path">PDF File Path</param>
        public bool HtmlToPdf(string url, string path)
        {
            try
            {
                if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(path))
                {
                    return false;
                }
                using (Process p = new Process())
                {
                    //string str = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wkhtmltopdf.exe");
                    var wkhtml = @"..\3rdParty\wkhtmltopdf\wkhtmltopdf.exe";
                    string str = currentContext.Server.MapPath(wkhtml);
                    if (!File.Exists(str))
                    {
                        return false;
                    }
                    KillWKHtmltoPDF();

                    p.StartInfo.FileName = str;
                    p.StartInfo.Arguments = String.Format("\"{0}\" \"{1}\"", url, path);
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.RedirectStandardInput = true;
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.RedirectStandardError = true;
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    return ConfirmConvertSuccess(path);
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogMessage(string.Format("Method: {0},  Exception Message:{1}", "DownLoadPDF", ex.Message));
            }
            return false;
        }

        private static bool ConfirmConvertSuccess(string path)
        {
            int count = 0;
            bool isSuccessful = true;

            while (true)
            {
                if (File.Exists(path))
                {
                    WaitWKHtmltoPDFClose();
                    break;
                }
                Thread.Sleep(1000);
                count++;
                if (count >= 300)
                {
                    isSuccessful = false;
                    break;
                }
            }

            return isSuccessful;
        }

        private static void WaitWKHtmltoPDFClose()
        {
            while (true)
            {
                Process[] procs = Process.GetProcessesByName("wkhtmltopdf");
                if (procs.Length > 0)
                {
                    Thread.Sleep(5000);
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Kill WKHTMLTOPDF exe
        /// </summary>
        private static void KillWKHtmltoPDF()
        {
            try
            {
                Process[] procs = Process.GetProcessesByName("wkhtmltopdf");
                Array.ForEach(procs,
                delegate(Process proc)
                {
                    proc.Kill();
                });
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogMessage(string.Format("Method: {0},  Exception Message:{1}", "DownLoadPDF", ex.Message));
            }
        }
        #endregion

        public byte[] WKHtmlToPdf(string convertHtmlPage, string localFilePath)
        {
            string wkhtmlDirPath = @"..\3rdParty\wkhtmltopdf\";
            string wkhtmlExePath = @"..\3rdParty\wkhtmltopdf\wkhtmltopdf.exe";
            wkhtmlExePath = currentContext.Server.MapPath(wkhtmlExePath);
            int index = wkhtmlExePath.LastIndexOf('\\');
            wkhtmlDirPath = wkhtmlExePath.Substring(0, index + 1);
            Process process = new Process();

            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = wkhtmlExePath;
            process.StartInfo.WorkingDirectory = wkhtmlDirPath;

            string switches = "--print-media-type ";
            switches += "--margin-top 4mm --margin-bottom 4mm --margin-right 0mm --margin-left 0mm ";
            switches += "--page-size A4 ";
            switches += "--no-background ";
            switches += "--redirect-delay 100";
            process.StartInfo.Arguments = switches + " " + convertHtmlPage + " " + localFilePath;
            process.Start();

            //read output
            byte[] buffer = new byte[32768];
            byte[] file;
            using (var ms = new MemoryStream())
            {
                while (true)
                {
                    int read = process.StandardOutput.BaseStream.Read(buffer, 0, buffer.Length);

                    if (read <= 0)
                    {
                        break;
                    }
                    ms.Write(buffer, 0, read);
                }
                file = ms.ToArray();
            }

            // wait or exit
            process.WaitForExit();

            // read the exit code, close process
            int returnCode = process.ExitCode;
            process.Close();

            return returnCode == 0 ? file : null;
        }
    }
}