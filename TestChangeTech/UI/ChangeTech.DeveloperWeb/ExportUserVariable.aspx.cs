using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;
using ChangeTech.Models;
using Microsoft.WindowsAzure.StorageClient;
using Ethos.Utility;
using ChangeTech.Services;

namespace ChangeTech.DeveloperWeb
{
    public partial class ExportUserVariable : PageBase<ModelBase>
    {
        private string versionNumber
        {
            get
            {
                string version = System.Configuration.ConfigurationManager.AppSettings["ProjectVersionWithoutDot"];
                return string.IsNullOrEmpty(version) ? "" : version.ToLower();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]))
                {
                    string fileName = Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID] + DateTime.UtcNow.ToString("yyyyMMddHHmmss") + ".xls";
                    fileNameHF.Value = fileName;
                    string accountName = Resolve<ISystemSettingService>().GetSettingValue("AzureStorageAccountName");
                    string bolbPath = ServiceUtility.GetBlobPath(accountName);
                    downloadLnk.NavigateUrl = bolbPath + BlobContainerTypeEnum.ExportContainer.ToString().ToLower() + "/" + fileName;
                    downloadLnk.Enabled = false;
                    startDateTimeHF.Value = DateTime.UtcNow.ToString();
                    Resolve<IExportService>().AddReportProgramUserVariableCommand(fileName, new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
                }
                else
                {

                }
            }
        }

        protected void updateTimer_Tick(object sender, EventArgs e)
        {
           string statusMessageStr = GetAddRemoveStauts(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));

           if (statusMessageStr.Equals("Complete"))
           {
               msgLbl.Text = "Data is ready, click [Download] link to get the file.";
               downloadLnk.Enabled = true;
               updateTimer.Enabled = false;
           }
           else
           {
               msgLbl.Text = statusMessageStr;
           }
           timeLbl.Text = Math.Round((DateTime.UtcNow - Convert.ToDateTime(startDateTimeHF.Value)).TotalSeconds, 0).ToString() + " seconds";
        }

        private string GetAddRemoveStauts(Guid programGUID)
        {
            string statusMessageStr = "";
            try
            {
                string statusQueueName = string.Format("{0}{1}{2}", programGUID, "statusqueue", versionNumber);
                CloudQueue statusQueue = Resolve<IAzureQueueService>().GetCloudQueue(statusQueueName);
                if (statusQueue != null && statusQueue.Exists())
                {
                    CloudQueueMessage statusMessageInQueue = statusQueue.PeekMessage();

                    if (statusMessageInQueue != null)
                    {
                        string statusMessage = statusMessageInQueue.AsString;
                        string[] statusMessageArrary = statusMessage.Split(new char[] { ';' });
                        statusMessageStr = statusMessageArrary[0];
                        if (statusMessageArrary[0].Equals("Complete"))
                        {
                            statusQueue.Clear();
                        }                        
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                statusMessageStr = string.Format("Error occurs: {0}", ex.Message);
            }
            return statusMessageStr;
        }
    }
}
