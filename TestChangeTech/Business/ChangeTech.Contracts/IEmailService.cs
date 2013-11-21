using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.Models;

namespace ChangeTech.Contracts
{
    public interface IEmailService
    {
        //void SendEmailsForTest();
        void SendRegisterEmail(Guid userGUID, Guid programGUID);
        void SendForgetPasswordEmail(string usernam, Guid programGUID);
        bool SendProgramRemainderEmail(User UserEntity, Guid ProgramGuid, LogTypeEnum logType = LogTypeEnum.SendReminderEmail);
        bool SendProgramRemainderEmailList(List<ReminderEmailInfoModel> reminderEmailInfoList);
        bool SendProgramRemainderEmailAsync(ReminderEmailInfoModel reminderEmailItem);
        bool SendProgramRemainderEmailListAsync(List<ReminderEmailInfoModel> reminderEmailInfoList);//the method will be wrong,don't use it
        bool AsyncSendProgramRemainderEmailList(List<ReminderEmailInfoModel> reminderEmailInfoList);//the async sending mail method
        bool SendMonitorWorkerRoleEmail();
        void SendLoginInfoToNewAdmin(Guid userGUID);
        void SendInvitiationEmail(Guid userGuid, Guid programGuid, string invitee);
        FailEmail GetProgramRemainderEmail(User programUser, Guid ProgramGuid);
        bool SendProgramRemainderEmail(ReminderEmailInfoModel reminderEmailInfoModel);
        void SendEmailToCustomer(OrderModel orderModel);
        void SendHPOrderEmails(List<HPOrderEmailModel> orderEmailModels);

        void SendLFSupportEmail(LFSupportEmailModel supportEmailModel);
        string GetServerPath(Guid programGuid);
    }
}
