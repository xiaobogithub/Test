using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ethos.DependencyInjection;
using ChangeTech.Models;
using ChangeTech.Contracts;
using System.Net;
using Ethos.Utility;


namespace ChangeTech.DeveloperWeb.Monitor
{
    public partial class MonitorWorkerRole : PageBase<ActivityLogModel>
    {
        public const int MONITOREMAILTIMESPAN = 2;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //Get MonitorWorkerRole log from ActivityMonitorEmailLog
                    ActivityMonitorEmailLogModel MonitorWorkerRoleLogModel = Resolve<IActivityMonitorEmailLogService>().GetLastLogByLogTypeAndMessage((int)LogTypeEnum.MonitorWorkerRoleEmail);
                    DateTime current = DateTime.UtcNow;
                    if (MonitorWorkerRoleLogModel == null || MonitorWorkerRoleLogModel.ActivityDateTime.AddHours(MONITOREMAILTIMESPAN) < current)
                    {
                        //Get SendRemindEmail log from ActivityLog
                        //ActivityMonitorEmailLogModel ReminderEmailLogModel = Resolve<IActivityMonitorEmailLogService>().GetLastLogByLogTypeAndMessage((int)LogTypeEnum.SendReminderEmail, REMINDEREMAILSUCCESSMESSAGE);
                        ActivityLogModel ReminderEmailLogModel = Resolve<IActivityLogService>().GetLastLogByLogTypeAndMessage((int)LogTypeEnum.SendReminderEmail);
                        current = DateTime.UtcNow;
                        if (ReminderEmailLogModel == null || ReminderEmailLogModel.ActivityDateTime.AddHours(MONITOREMAILTIMESPAN) < current)
                        {
                            Response.StatusCode = (int)System.Net.HttpStatusCode.NotFound;
                            Response.End();
                        }
                        else
                        {
                            this.lblMessage.Text = string.Format("Monitor: Worker role send email success. The last email send time is at {0}", ReminderEmailLogModel.ActivityDateTime);
                        }
                    }
                    else
                    {
                        this.lblMessage.Text = string.Format("Monitor: Worker role send email success. The last email send time is at {0}", MonitorWorkerRoleLogModel.ActivityDateTime);
                    }
                }
            }
            catch (Exception ex)
            {
                InsertLogModel insertLogModel = new InsertLogModel
                {
                    ActivityLogType = (int)LogTypeEnum.MonitorWorkerRoleStatus,
                    Browser = string.Empty,
                    From = string.Empty,
                    IP = string.Empty,
                    Message = string.Format("Worker role crashed. Details:{0}", ex.ToString()),
                    PageGuid = Guid.Empty,
                    PageSequenceGuid = Guid.Empty,
                    SessionGuid = Guid.Empty,
                    ProgramGuid = Guid.Empty,
                    UserGuid = Guid.Empty,
                };
                Resolve<IActivityLogService>().Insert(insertLogModel);

                Response.StatusCode = (int)System.Net.HttpStatusCode.NotFound;
                Response.End();
            }


            #region Old follows--Logs insert to ActivityLog table
            //try
            //{
            //    if (!IsPostBack)
            //    {
            //        ActivityLogModel MonitorWorkerRoleLogModel = Resolve<IActivityLogService>().GetLastLogByLogTypeAndMessage((int)LogTypeEnum.MonitorWorkerRoleEmail, MONITOREMAILSUCCESSMESSAGE);
            //        DateTime current = DateTime.UtcNow;
            //        if (MonitorWorkerRoleLogModel == null || MonitorWorkerRoleLogModel.ActivityDateTime.AddHours(MONITOREMAILTIMESPAN) < current)
            //        {
            //            ActivityLogModel ReminderEmailLogModel = Resolve<IActivityLogService>().GetLastLogByLogTypeAndMessage((int)LogTypeEnum.SendReminderEmail, REMINDEREMAILSUCCESSMESSAGE);
            //            current = DateTime.UtcNow;
            //            if (ReminderEmailLogModel == null || ReminderEmailLogModel.ActivityDateTime.AddHours(MONITOREMAILTIMESPAN) < current)
            //            {
            //                Response.StatusCode = (int)System.Net.HttpStatusCode.NotFound;
            //                Response.End();
            //            }
            //            else
            //            {
            //                this.lblMessage.Text = string.Format("Monitor: Worker role send email success. The last email send time is at {0}", ReminderEmailLogModel.ActivityDateTime);
            //            }
            //        }
            //        else
            //        {
            //            this.lblMessage.Text = string.Format("Monitor: Worker role send email success. The last email send time is at {0}", MonitorWorkerRoleLogModel.ActivityDateTime);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    InsertLogModel insertLogModel = new InsertLogModel
            //    {
            //        ActivityLogType = (int)LogTypeEnum.MonitorWorkerRoleStatus,
            //        Browser = string.Empty,
            //        From = string.Empty,
            //        IP = string.Empty,
            //        Message = string.Format("Worker role crashed. Details:{0}", ex.ToString()),
            //        PageGuid = Guid.Empty,
            //        PageSequenceGuid = Guid.Empty,
            //        SessionGuid = Guid.Empty,
            //        ProgramGuid = Guid.Empty,
            //        UserGuid = Guid.Empty,
            //    };
            //    Resolve<IActivityLogService>().Insert(insertLogModel);

            //    Response.StatusCode = (int)System.Net.HttpStatusCode.NotFound;
            //    Response.End();
            //} 
            #endregion
        }

        protected void btnEmailDetails_Click(object sender, EventArgs e)
        {
            #region ActivityLog table follows
            int allCount = Resolve<IActivityLogService>().GetAllReminderEmailsCountOfToday();
            int allSuccessCount = Resolve<IActivityLogService>().GetAllReminderEmailsCountOfTodaySuccess();
            List<string> emails = Resolve<IActivityLogService>().GetAllReminderFailedEmailsOfToday();

            this.lblTotalCount.Text = allCount.ToString();
            this.lblSuccessCount.Text = allSuccessCount.ToString();
            this.dlFailureEmails.DataSource = emails;
            this.dlFailureEmails.DataBind(); 
            #endregion

            #region ActivityMonitorEmailLog table follows
            //int allCount = Resolve<IActivityMonitorEmailLogService>().GetAllReminderEmailsCountOfToday();
            //int allSuccessCount = Resolve<IActivityMonitorEmailLogService>().GetAllReminderEmailsCountOfTodaySuccess();
            //List<string> emails = Resolve<IActivityMonitorEmailLogService>().GetAllReminderFailedEmailsOfToday();

            //this.lblTotalCount.Text = allCount.ToString();
            //this.lblSuccessCount.Text = allSuccessCount.ToString();
            //this.dlFailureEmails.DataSource = emails;
            //this.dlFailureEmails.DataBind(); 
            #endregion
        }
    }
}