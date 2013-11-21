using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ChangeTech.Models;
using ChangeTech.Entities;

namespace ChangeTech.Contracts
{
    public interface IProgramUserService
    {
        void SendEmailToUserInsertIntoTestLogTableForTest(DateTime time);
        bool AddShouldReminderEmailTableBeforeNowInQueue(DateTime now);
        void SendEmailToUser(DateTime time);
        bool TranferForBeforeProcessReminderEmail(ReminderEmailInfoModel reminderEmailModel);
        void SendEmailToUserForMonitorWorkerRole();
        void SetLastSendEmailTimeOfProgramUser(Guid programgGuid, Guid userGuid);
        void SendEmailToTester(Guid UserGuid, Guid ProgramGuid);
        bool HasUserInProgram(Guid userGuid, Guid programGuid);
        int IsThereClassToday(Guid userGuid, Guid programGuid, DateTime now);
        bool PauseProgram(Guid userGuid, Guid programGuid, int day);
        void SetUserClassStatus(string hpSecurity, Guid userGuid, Guid programGuid, Guid sessionGuid);
        string GetUserClass(Guid userGuid, Guid programGuid, Guid languageGuid);
        void ExitProgram(Guid userGuid, Guid programGuid);
        JoinProgramResultEnum EndUserJoinProgram(RegisterProgramUserModel registerProgramUser);
        void AddProgramUser(Guid programGUID, Guid userGUID);
        void DeleteProgramUser(Guid programGUID, Guid userGUID);
        bool IsUserMissedDay(Guid userGuid, Guid programGuid);
        int GetOutlineDay(DateTime dateTime);
        int GetShouldDoDay(Guid programGuid, Guid userGuid, DateTime now);
        void CutConnection();
        List<CompanyUserInfoModel> GetCompanyUserList(Guid companyRightGuid, int pageNumber, int pageSize);
        List<CompanyUserInfoModel> GetUsersNotOnCompany(Guid programGuid, int pageNumber, int pageSize);
        List<CompanyUserInfoModel> GetUsersByEmailOrMobile(Guid programGuid, string email, string mobile, int pageNumber, int pageSize);
        int GetCountOfCompanyUser(Guid companyRightGuid);
        int GetCountOfUserNotOnCompany(Guid programGuid);
        int GetCountOfUserByEmailOrMobile(Guid programGuid, string email, string mobile);
        CompanyUserInfoModel GetCompanyUserInfo(Guid programUserGuid);
        void UpdateCompanyUserInfo(CompanyUserInfoModel companyUserInfo);
        void ActiveSMSKickStart(string programShortName, string userMobile);
        void UpdateProgramUser(EditProgramUserModel editProgramUserModel);
        int GetInactiveUserNumber(int inactiveDays);
        List<UserModel> GetInactiveUsers(int inactiveDays);
        int GetLoginUserNumber(int loginMinutes);
        int GetRegisteredUserNumber(int registeredDays);
        //int GetMissedClassUserNumber(int missedDays);
        //List<UserInfoModel> GetMissedClassUsers(int missedDays);
        MissedClassUsersModel GetMissedClassUsers(int missedDays, int pageNumber, int pageSize);
        string GetProgramUserStatus(Guid programGuid, Guid userGuid);

        DateTime? GetLastReportRelapseTime(Guid programGuid, Guid userGuid);
        void SetLastReportRelapseTime(Guid programGuid, Guid userGuid);
        void ExistProgramDailySMSListIntoShortMessageQueue(DateTime time);
        void ExistRemindReportSMSListIntoShortMessageQueue(DateTime time, int timeSpanMinutes);
        //void UpdateProgramUserTimeZone(ProgramUserModel programUserModel);
        //void UpdateProgramUserSMSToEmail(ProgramUserModel programUserModel);
        void UpdateProgramUserProperty(ProgramUserModel programUserModel);
        DateTime GetCurrentTimeByTimeZone(Guid programGuid, Guid userGuid, DateTime utcTime);
        DateTime GetCurrentTimeFromTimeZoneToUtc(Guid programGuid, Guid userGuid, DateTime timeZoneTime);
        ProgramUser GetProgramUserByProgramGuidAndUserGuid(Guid programGuid, Guid userGuid);

        DateTime? ExpectSessionDate(Guid programGuid, Guid userGuid, int currentDay);
        RegisterMessageModel Win8EndUserJoinProgram(RegisterWin8ProgramUsersModel registerWin8ProgramUser);
        int GetShouldDoDayCountByWin8(Guid programGuid, Guid userGuid);
        DateTime? GetCurrentSessionDate(Guid programGuid, Guid userGuid);
        void CheckPuSwitchMessageTimeForTwoPartProgram();
    }
}
