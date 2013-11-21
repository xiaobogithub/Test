using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using ChangeTech.Silverlight.Common;
using ChangeTech.Silverlight.DesignPage.ChangeTechWCFService;
using System.Windows.Browser;
using System.Threading;
using System.IO;
using System.Text;
using System.Windows.Resources;
using System.Globalization;
using System.Xml;
using System.Text.RegularExpressions;

namespace ChangeTech.Silverlight.DesignPage
{
    public partial class ManageProgramLanguage : Page
    {
        public string ProgramName { get; set; }
        //private string _languageName;
        //private Guid _languageGUID;
        private Timer statusCheckTimer;
        private Timer elapseTimer;
        private DateTime startTime;
        private string _currentCommand;
        //private SaveFileDialog saveFileDialog;
        private string _exportFileName;

        public ManageProgramLanguage()
        {
            InitializeComponent();
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        #region Initialize
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            BindLanguageList();
            ExportOption.SetExportOptionEventHandler += new SetExportOptionDelegate(ExportOption_SetExportOptionEventHandler);
            ExportOption.CancelExportOptionEventHandler += new EventHandler(ExportOption_CancelExportOptionEventHandler);
        }

        private void BindLanguageList()
        {
            DisableControls();
            ServiceClient serviceClient = ServiceProxyFactory.Instance.ServiceProxy;
            serviceClient.GetLanguagesSupportByProgramCompleted += new EventHandler<GetLanguagesSupportByProgramCompletedEventArgs>(serviceClient_GetLanguagesSupportByProgramCompleted);
            serviceClient.GetLanguagesSupportByProgramAsync(new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_PROGRAM_GUID)));
        }

        private void serviceClient_GetLanguagesSupportByProgramCompleted(object sender, GetLanguagesSupportByProgramCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                HtmlPage.Window.Alert("Operation is cancelled.");

                EnableControls();
            }
            else if (e.Error != null)
            {
                HtmlPage.Window.Alert(Constants.ERROR_INTERNAL);

                EnableControls();
            }
            else
            {
                SupportLanguageList.ItemsSource = e.Result;
                //_programName = e.Result
                ServiceClient serviceClient = ServiceProxyFactory.Instance.ServiceProxy;
                serviceClient.GetLanguagesNotSupportByProgramCompleted += new EventHandler<GetLanguagesNotSupportByProgramCompletedEventArgs>(serviceClient_GetLanguagesNotSupportByProgramCompleted);
                serviceClient.GetLanguagesNotSupportByProgramAsync(new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_PROGRAM_GUID)));
            }
        }

        void serviceClient_GetLanguagesNotSupportByProgramCompleted(object sender, GetLanguagesNotSupportByProgramCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                HtmlPage.Window.Alert("Operation is cancelled.");
            }
            else if (e.Error != null)
            {
                HtmlPage.Window.Alert(Constants.ERROR_INTERNAL);
            }
            else
            {
                NotSupportLanguageList.ItemsSource = e.Result;
            }
            EnableControls();

            if (elapseTimer != null)
            {
                elapseTimer.Dispose();
            }
        }

        private void SupportLanguageList_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            Button exportButton = (Button)SupportLanguageList.Columns[2].GetCellContent(e.Row);
            Button importButton = (Button)SupportLanguageList.Columns[3].GetCellContent(e.Row);
            Button reportButton = (Button)SupportLanguageList.Columns[4].GetCellContent(e.Row);
            Button removeButton = (Button)SupportLanguageList.Columns[5].GetCellContent(e.Row);

            exportButton.Tag = ((LanguageModel)e.Row.DataContext);
            importButton.Tag = ((LanguageModel)e.Row.DataContext);
            reportButton.Tag = ((LanguageModel)e.Row.DataContext);
            removeButton.Tag = ((LanguageModel)e.Row.DataContext);

            if (((LanguageModel)e.Row.DataContext).IsDefaultLanguage)
            {
                removeButton.IsEnabled = false;
            }
        }

        private void NotSupportLanguageList_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            Button removeButton = (Button)NotSupportLanguageList.Columns[1].GetCellContent(e.Row);
            removeButton.Tag = (LanguageModel)e.Row.DataContext;
        }
        #endregion

        #region Export
        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            DisableControls();
            LanguageModel lm = ((LanguageModel)(((Button)sender).Tag));
            ExportOption.Show(lm);
        }

        private void ExportOption_SetExportOptionEventHandler(LanguageModel lm, string startDay, string endDay, bool includeRelapse, bool includeProgramRoom,
    bool includeAccessoryTemplate, bool includeEmailTemplate, bool includeHelpItem, bool includeUserMenu, bool includeTipMessage, bool includeSpecialString)
        {
            //DisableControls();
            _currentCommand = "Export";
            startTime = DateTime.UtcNow;
            UpdateStatus("Your request is processing...");
            ServiceClient serviceClient = ServiceProxyFactory.Instance.ServiceProxy;
            serviceClient.ExportProgramCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(serviceClient_ExportProgramCompleted);
            _exportFileName = "E" + StringUtility.GetQueryString(Constants.QUERYSTR_PROGRAM_GUID) + DateTime.UtcNow.ToString("yyyyMMddHHmmss") + ".xls";
            serviceClient.ExportProgramAsync(_exportFileName, new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_PROGRAM_GUID)), lm.LanguageGUID, startDay, endDay, includeRelapse,
                includeProgramRoom, includeAccessoryTemplate, includeEmailTemplate, includeHelpItem, includeUserMenu, includeTipMessage, includeSpecialString);

            TimerCallback statusTimerCallBack = new TimerCallback(UpdateStatusCallBack);
            statusCheckTimer = new Timer(statusTimerCallBack, null, 10000, 30000);

            TimerCallback elapseTimerCallBack = new TimerCallback(UpdateUseTimeCallBack);
            elapseTimer = new Timer(elapseTimerCallBack, null, 1000, 2000);
        }

        private void ExportOption_CancelExportOptionEventHandler(object sender, EventArgs e)
        {
            EnableControls();
        }

        private void downloadLnk_Click(object sender, RoutedEventArgs e)
        {
            string exportFilePath = StringUtility.GetBlobPath() + string.Format("exportcontainer/{0}", _exportFileName);
            HtmlPage.Window.Navigate(new Uri(exportFilePath), "_blank");
        }

        private void serviceClient_ExportProgramCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                HtmlPage.Window.Alert("Operation is cancelled.");

                EnableControls();
            }
            else if (e.Error != null)
            {
                HtmlPage.Window.Alert(Constants.ERROR_INTERNAL);

                EnableControls();
            }
            else
            {

            }
        }
        #endregion

        #region Add lanaguge
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            startTime = DateTime.UtcNow;
            _currentCommand = "Add";
            _exportFileName = StringUtility.GetQueryString(Constants.QUERYSTR_PROGRAM_GUID);
            UpdateStatus(string.Format("Your request is processing, please wait..."));

            DisableControls();
            ServiceClient serviceClient = ServiceProxyFactory.Instance.ServiceProxy;
            serviceClient.AddProgramLanguageCompleted += new EventHandler<AddProgramLanguageCompletedEventArgs>(serviceClient_AddProgramLanguageCompleted);
            serviceClient.AddProgramLanguageAsync(new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_PROGRAM_GUID)), ((LanguageModel)(((Button)sender).Tag)).LanguageGUID);

            TimerCallback statusTimerCallBack = new TimerCallback(UpdateStatusCallBack);
            statusCheckTimer = new Timer(statusTimerCallBack, null, 10000, 30000);

            TimerCallback elapseTimerCallBack = new TimerCallback(UpdateUseTimeCallBack);
            elapseTimer = new Timer(elapseTimerCallBack, null, 1000, 2000);
        }

        private void serviceClient_AddProgramLanguageCompleted(object sender, AddProgramLanguageCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                HtmlPage.Window.Alert("Operation is cancelled.");
            }
            else if (e.Error != null)
            {
                HtmlPage.Window.Alert(Constants.ERROR_INTERNAL);
            }
            else
            {

            }
        }
        #endregion

        #region Import
        private List<string> importIDs = new List<string>();
        private bool importError = false;
        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            //HtmlPage.Window.Alert("Under developement, this function will come tomorrow.");   
            try
            {
                DisableControls();
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Multiselect = false;
                openFileDialog.Filter = "Excel 97-2003 Workbook (*.xls)| *.xls";
                if (openFileDialog.ShowDialog().Value)
                {
                    FileInfo fi = openFileDialog.File;
                    if (openFileDialog.File.Name.EndsWith(".xls"))
                    {
                        UpdateStatus("Validating your document...");
                        importError = false;
                        using (FileStream fs = fi.OpenRead())
                        {
                            //StreamReader streamReader = new StreamReader(fs);
                            //string xmlDataString = streamReader.ReadToEnd();
                            //Regex badAmpersand = new Regex("&(?![a-zA-Z]{2,6};|#[0-9]{2,4};)");
                            //string goodAmpersand = "&amp;";
                            //xmlDataString = badAmpersand.Replace(xmlDataString, goodAmpersand);
                            //byte[] byteArray = System.Text.Encoding.Unicode.GetBytes(xmlDataString);
                            //MemoryStream stream = new MemoryStream(byteArray);
                            XmlReaderSettings xs= new XmlReaderSettings();
                            xs.DtdProcessing = DtdProcessing.Parse;
                            using (XmlReader xr = XmlReader.Create(fs,xs))
                            {
                                if (xr.ReadToFollowing("Workbook"))
                                {
                                    string programGUID = xr.GetAttribute("ProgramGUID");
                                    string lanaguageGUID = xr.GetAttribute("LanguageGUID");

                                    if (programGUID.Equals(StringUtility.GetQueryString(Constants.QUERYSTR_PROGRAM_GUID)) &&
                                        lanaguageGUID.Equals(((LanguageModel)(((Button)sender).Tag)).LanguageGUID.ToString()))
                                    {
                                        startTime = DateTime.UtcNow;

                                        TimerCallback elapseTimerCallBack = new TimerCallback(UpdateUseTimeCallBack);
                                        elapseTimer = new Timer(elapseTimerCallBack, null, 1000, 2000);
                                        int sheetNumber = 0;
                                        bool errorFlug = false;

                                        while (xr.ReadToFollowing("Worksheet"))
                                        {
                                            sheetNumber++;
                                            int rowNumber = 3;

                                            if (errorFlug == true)
                                            {
                                                break;
                                            }

                                            using (XmlReader worksheetReader = xr.ReadSubtree())
                                            {
                                                // Header row (3 rows)
                                                worksheetReader.ReadToFollowing("Row");
                                                worksheetReader.ReadToFollowing("Row");
                                                worksheetReader.ReadToFollowing("Row");
                                                while (worksheetReader.ReadToFollowing("Row"))
                                                {
                                                    rowNumber++;
                                                    UpdateStatus(string.Format("Import Sheet {0} Row {1}", sheetNumber, rowNumber));
                                                    int cellNumber = 0;
                                                    try
                                                    {
                                                        string id = string.Empty;
                                                        string tableName = string.Empty;
                                                        string type = string.Empty;

                                                        cellNumber++;
                                                        worksheetReader.ReadToFollowing("Cell"); // guid
                                                        worksheetReader.ReadToFollowing("Data");
                                                        id = worksheetReader.ReadInnerXml();
                                                        cellNumber++;
                                                        worksheetReader.ReadToFollowing("Cell"); // Object
                                                        worksheetReader.ReadToFollowing("Data");
                                                        tableName = worksheetReader.ReadInnerXml();
                                                        cellNumber++;
                                                        worksheetReader.ReadToFollowing("Cell"); // Type
                                                        worksheetReader.ReadToFollowing("Data");
                                                        type = worksheetReader.ReadInnerXml();

                                                        //if (string.IsNullOrEmpty(id))
                                                        //{
                                                        //    errorFlug = true;
                                                        //    //UpdateStatus(string.Format("keys were lost, Position Sheet {0} Row {1}", sheetNumber, rowNumber));
                                                        //    AppendError(string.Format("Sheet {0} Row {1} \r\n      {2}", sheetNumber, rowNumber,"missed GUID"));
                                                        //    // tell complete method, error occured.
                                                        //    importError = true;
                                                        //    break;
                                                        //}
                                                        //if (string.IsNullOrEmpty(tableName))
                                                        //{
                                                        //    errorFlug = true;
                                                        //    //UpdateStatus(string.Format("keys were lost, Position Sheet {0} Row {1}", sheetNumber, rowNumber));
                                                        //    AppendError(string.Format("Sheet {0} Row {1} \r\n      {2}", sheetNumber, rowNumber, "missed Object"));
                                                        //    // tell complete method, error occured.
                                                        //    importError = true;
                                                        //    break;
                                                        //}
                                                        //if (string.IsNullOrEmpty(type))
                                                        //{
                                                        //    errorFlug = true;
                                                        //    //UpdateStatus(string.Format("keys were lost, Position Sheet {0} Row {1}", sheetNumber, rowNumber));
                                                        //    AppendError(string.Format("Sheet {0} Row {1} \r\n      {2}", sheetNumber, rowNumber, "missed Type"));
                                                        //    // tell complete method, error occured.
                                                        //    importError = true;
                                                        //    break;
                                                        //}


                                                        // DTD-1243

                                                        //cellNumber++;
                                                        //worksheetReader.ReadToFollowing("Cell"); // Original Text
                                                        //worksheetReader.ReadToFollowing("Data");
                                                        //string originalText = worksheetReader.ReadInnerXml();
                                                        //cellNumber++;
                                                        //worksheetReader.ReadToFollowing("Cell"); // New Text
                                                        //worksheetReader.ReadToFollowing("Data");
                                                        //string newText = worksheetReader.ReadInnerXml();

                                                        cellNumber++;
                                                        worksheetReader.ReadToFollowing("Cell"); // Original Text
                                                        string originalText = "";
                                                        string newText = "";
                                                        if (worksheetReader.GetAttribute("ss:Index") != null)
                                                        {
                                                            switch (Convert.ToInt32(worksheetReader.GetAttribute("ss:Index")))
                                                            {
                                                                //new text
                                                                case 5:
                                                                    originalText = "";
                                                                    cellNumber++;
                                                                    //worksheetReader.ReadToFollowing("Data");
                                                                    newText = worksheetReader.ReadOuterXml();
                                                                    break;
                                                                // max length
                                                                case 6:
                                                                    originalText = "";
                                                                    cellNumber++;
                                                                    newText = "";
                                                                    break;
                                                                default:
                                                                    originalText = "";
                                                                    cellNumber++;
                                                                    newText = "";
                                                                    break;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            //worksheetReader.ReadToFollowing("Data");
                                                            originalText = worksheetReader.ReadOuterXml();
                                                            cellNumber++;
                                                            worksheetReader.ReadToFollowing("Cell"); // New Text                                                        
                                                            if (worksheetReader.GetAttribute("ss:Index") != null)
                                                            {
                                                                newText = "";
                                                            }
                                                            else
                                                            {
                                                                //worksheetReader.ReadToFollowing("Data");
                                                                newText = worksheetReader.ReadOuterXml();
                                                            }
                                                        }

                                                        while (originalText.IndexOf('<') > -1)
                                                        {
                                                            string tag = originalText.Substring(originalText.IndexOf('<'), originalText.IndexOf('>') - originalText.IndexOf('<') + 1);
                                                            originalText = originalText.Replace(tag, "");
                                                        }
                                                        while (newText.IndexOf('<') > -1)
                                                        {
                                                            string tag = newText.Substring(newText.IndexOf('<'), newText.IndexOf('>') - newText.IndexOf('<') + 1);
                                                            newText = newText.Replace(tag, "");
                                                        }

                                                        // update newText even if it equals originalText, the newText in DB might be wrong content for some reason.
                                                        //if (!originalText.Equals(newText))
                                                        //{
                                                            lock (importIDs)
                                                            {
                                                                importIDs.Add(id);
                                                            }
                                                            ServiceClient serviceClient = ServiceProxyFactory.Instance.ServiceProxy;
                                                            serviceClient.ImportProgramDataCompleted += new EventHandler<ImportProgramDataCompletedEventArgs>(serviceClient_ImportProgramDataCompleted);
                                                            if (!string.IsNullOrEmpty(newText))
                                                            {
                                                                newText = newText.Replace("&#10;", "\r").Replace("&#13;", "\n").Replace("&#9;", "\t");
                                                            }
                                                            serviceClient.ImportProgramDataAsync(tableName, id, type, newText, ((LanguageModel)(((Button)sender).Tag)).LanguageGUID);
                                                        //}
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        //UpdateStatus(ex.ToString());
                                                        // tell complete method, error occured.
                                                        importError = true;
                                                        UpdateStatus("Import failed!");
                                                        AppendError(string.Format("Sheet {0} Row {1} \r\n      {2}", sheetNumber, rowNumber, ex.Message));
                                                    }
                                                }
                                            }
                                        }
                                        if (importIDs.Count == 0)
                                        {
                                            if (elapseTimer != null)
                                            {
                                                elapseTimer.Dispose();
                                            }

                                            if (importError == false)
                                                UpdateStatus("Import successfully.");
                                            else
                                                UpdateStatus("Import failed.");
                                            EnableControls();
                                        }
                                    }
                                    else
                                    {
                                        HtmlPage.Window.Alert("Your file is not valid for current language, please make sure you are using the file which is exported form this language!");
                                        EnableControls();
                                        UpdateStatus("Import failed!");
                                        AppendError(string.Format("{0}", "Your file is not valid for current language, please make sure you are using the file which is exported form this language!"));
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        HtmlPage.Window.Alert("This file is not a valid format to import, please make your import file must be generated by Export function!");
                        EnableControls();
                        UpdateStatus("Import failed!");
                        AppendError(string.Format("{0}", "This file is not a valid format to import, please make your import file must be generated by Export function!"));

                    }
                }
                else
                {
                    EnableControls();
                }
            }
            catch (Exception ex)
            {
                UpdateStatus("Import failed!");
                AppendError(ex.Message);
            }
        }

        void serviceClient_ImportProgramDataCompleted(object sender, ImportProgramDataCompletedEventArgs e)
        {
            lock (importIDs)
            {
                importIDs.Remove(e.Result);
            }

            if (e.Cancelled)
            {
                HtmlPage.Window.Alert("Operation is cancelled.");
            }
            else if (e.Error != null)
            {
                UpdateStatus(string.Format("Error: {1}", e.Error));

                HtmlPage.Window.Alert(Constants.ERROR_INTERNAL);
            }
            else
            {
                //if (importIDs.Count == 1 && importIDs.FirstOrDefault().Equals("Error"))
                //{}
                //else
                //{
                UpdateStatus(string.Format("Left imported items count: {0}", importIDs.Count));
                //}               
                if (importIDs.Count == 0)
                {
                    if (elapseTimer != null)
                    {
                        elapseTimer.Dispose();
                    }

                    if (importError == false)
                        UpdateStatus("Import successfully.");
                    else
                        UpdateStatus("Import failed.");
                    EnableControls();
                }

            }
        }
        #endregion

        #region Remove language
        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            startTime = DateTime.UtcNow;
            _currentCommand = "Remove";
            _exportFileName = StringUtility.GetQueryString(Constants.QUERYSTR_PROGRAM_GUID);
            UpdateStatus(string.Format("Your request is processing, please wait..."));

            DisableControls();
            ServiceClient serviceClient = ServiceProxyFactory.Instance.ServiceProxy;
            serviceClient.RemoveProgramLanguageCompleted += new EventHandler<RemoveProgramLanguageCompletedEventArgs>(serviceClient_RemoveProgramLanguageCompleted);
            serviceClient.RemoveProgramLanguageAsync(new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_PROGRAM_GUID)), ((LanguageModel)(((Button)sender).Tag)).LanguageGUID);

            TimerCallback statusTimerCallBack = new TimerCallback(UpdateStatusCallBack);
            statusCheckTimer = new Timer(statusTimerCallBack, null, 10000, 30000);

            TimerCallback elapseTimerCallBack = new TimerCallback(UpdateUseTimeCallBack);
            elapseTimer = new Timer(elapseTimerCallBack, null, 1000, 2000);
        }

        private void serviceClient_RemoveProgramLanguageCompleted(object sender, RemoveProgramLanguageCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                HtmlPage.Window.Alert("Operation is cancelled.");

                EnableControls();
            }
            else if (e.Error != null)
            {
                HtmlPage.Window.Alert(Constants.ERROR_INTERNAL);

                EnableControls();
            }
            else
            {

            }
        }
        #endregion

        #region Report
        private void ReportButton_Click(object sender, RoutedEventArgs e)
        {
            DisableControls();
            _currentCommand = "Report";
            startTime = DateTime.UtcNow;

            UpdateStatus("Your request is processing...");
            ServiceClient serviceClient = ServiceProxyFactory.Instance.ServiceProxy;
            serviceClient.ReportProgramCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(serviceClient_ReportProgramCompleted);
            _exportFileName = "R" + StringUtility.GetQueryString(Constants.QUERYSTR_PROGRAM_GUID) + DateTime.UtcNow.ToString("yyyyMMddHHmmss") + ".xls";
            serviceClient.ReportProgramAsync(_exportFileName, new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_PROGRAM_GUID)), ((LanguageModel)(((Button)sender).Tag)).LanguageGUID);

            TimerCallback statusTimerCallBack = new TimerCallback(UpdateStatusCallBack);
            statusCheckTimer = new Timer(statusTimerCallBack, null, 10000, 30000);

            TimerCallback elapseTimerCallBack = new TimerCallback(UpdateUseTimeCallBack);
            elapseTimer = new Timer(elapseTimerCallBack, null, 1000, 2000);

        }

        void serviceClient_ReportProgramCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                HtmlPage.Window.Alert("Operation is cancelled.");
            }
            else if (e.Error != null)
            {
                HtmlPage.Window.Alert(Constants.ERROR_INTERNAL);
            }
            else
            {

            }
        }
        #endregion

        #region Common
        private void DisableControls()
        {
            SupportLanguageList.IsEnabled = false;
            NotSupportLanguageList.IsEnabled = false;
            downloadLnk.Visibility = Visibility.Collapsed;
        }

        private void EnableControls()
        {
            SupportLanguageList.IsEnabled = true;
            NotSupportLanguageList.IsEnabled = true;
        }

        private void UpdateStatusCallBack(object state)
        {
            Dispatcher.BeginInvoke(delegate()
            {
                ServiceClient serviceClient = ServiceProxyFactory.Instance.ServiceProxy;
                serviceClient.GetAddRemoveStautsCompleted += new EventHandler<GetAddRemoveStautsCompletedEventArgs>(serviceClient_GetAddRemoveStautsCompleted);
                serviceClient.GetAddRemoveStautsAsync(new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_PROGRAM_GUID)), _exportFileName);
            });
        }

        private void serviceClient_GetAddRemoveStautsCompleted(object sender, GetAddRemoveStautsCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                HtmlPage.Window.Alert("Operation is cancelled.");
            }
            else if (e.Error != null)
            {
                HtmlPage.Window.Alert(Constants.ERROR_INTERNAL);
            }
            else
            {
                Dispatcher.BeginInvoke(delegate()
                {
                    UpdateStatus(e.Result);

                    if (e.Result.StartsWith("Complete"))
                    {
                        switch (_currentCommand)
                        {
                            case "Add":
                            case "Remove":
                                UpdateStatus("Reloading prorgam languages.");
                                BindLanguageList();
                                break;
                            case "Report":
                            case "Export":
                                downloadLnk.Visibility = Visibility.Visible;
                                EnableControls();
                                break;
                        }
                        if (statusCheckTimer != null)
                        {
                            statusCheckTimer.Dispose();
                        }
                        if (elapseTimer != null)
                        {
                            elapseTimer.Dispose();
                        }
                    }
                });
            }
        }

        private void UpdateUseTimeCallBack(object state)
        {
            Dispatcher.BeginInvoke(delegate()
            {
                UpdateUseTime();
            });
        }

        private void UpdateStatus(string message)
        {
            StatusMessage.Text = string.Format("Current progress status: {0}.", message);
        }

        private void UpdateUseTime()
        {
            TimeMessage.Text = string.Format("Time has used:{0} seconds.", Math.Round((DateTime.UtcNow - startTime).TotalSeconds, 0));
        }

        private void AppendError(string message)
        {
            //if (ErrorMessage.Text == "")
            //    ErrorMessage.Text += string.Format("Error happened on {0} \r\n", message);
            //else
            ErrorMessage.Text += string.Format("Error:{0}\r\n", message);
        }
        #endregion
    }
}
