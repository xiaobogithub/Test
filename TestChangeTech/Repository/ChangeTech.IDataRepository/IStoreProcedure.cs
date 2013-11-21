using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ChangeTech.Entities;
using System.Data;

namespace ChangeTech.IDataRepository
{
    public interface IStoreProcedure
    {
        string GetPreviewPageModelAsXML(Guid pageGuid, Guid languageGuid, Guid sessionGuid, Guid pageSequenceGuid, Guid userGuid);
        string GetPreviewSessionModelAsXML(Guid languageGuid, Guid sessionGuid, Guid userGuid);
        string GetPreviewPageSequenceModelAsXMl(Guid languageGuid, Guid sessionGuid, Guid pageSequenceGuid, Guid userGuid);
        string GetTempPreviewPageSequenceModelAsXMl(Guid languageGuid, Guid sessionGuid, Guid pageSequenceGuid, Guid userGuid);
        string GetLiveSessionModelAsXML(Guid userGuid, Guid programGuid, Guid languageGuid, int day);
        string GetCTPPModelAsXML(Guid programGuid);
        string GetCTPPModelAsXMLByProgramGuidAndUserGuid(Guid programGuid, Guid userGuid);
        string GetProgramAccessoryModelAsXML(Guid programGuid, Guid languageGuid);
        string GetProgramSessionEndingModelAsXML(Guid programGuid, Guid languageGuid, Guid userGuid, int IsRegisterd);
        string GetPinCodeModelAsXML(Guid programGuid, Guid lanaugeGuid, Guid userGuid);
        string GetPaymentModelAsXML(Guid programGuid, Guid languageGuid, Guid userGuid);
        string GetEmptySessionXML(Guid programGuid, Guid userGuid, Guid languageGuid);
        string GetProgramReportXML(Guid programGuid);
        string GetRelapseModelAsXML(Guid languageGuid, Guid programGuid, Guid pageSequenceGuid, Guid userGuid);
        string GetRelapsePreviewModelAsXML(Guid languageGuid, Guid programGuid, Guid pageSequenceGuid, Guid userGuid);
        string GetProgramUserVariableAsXML(Guid programGuid);
        //string GetTipMessagePageAsXML(Guid programGuid, Guid userGuid, Guid LanguageGuid, string message);
        string GetPageGraphAsXML(Guid pageGuid);
        List<PageContent> GetPageContentByProgram(Guid ProgramGuid);
        List<PageQuestionContent> GetQuestionByProgram(Guid ProgramGuid);
        List<PageQuestionItemContent> GetQuestionItemByProgram(Guid ProgramGuid);
        List<Program> GetProgramByPageAndLanguageGUID(Guid PageGuid, Guid LanguageGuid);
        int SearchActivityLogNumber(string condition);
        List<ActivityLog> SearchActivityLog(string condition, int startNumber, int endNumber);
        //string GetActivityLogReport(string emaillistStr, string fileds, string days, string programGuid);
        void UpdatePageVariableAfterCopyProgram(Guid oldProgramGuid, Guid newProgramGuid, string variableName, string variableType);
        void UpdateProgramRoomAfterCopyProgram(Guid oldProgramGuid, Guid newProgramGuid, string roomName);
        string GetTranslationData(Guid programGuid, int startDay, int endDay, bool includeRelapse, bool includeProgramRoom,
            bool includeProgramAccessory, bool includeEmailTemplate, bool includeHelpItem, bool includeUserMenu,
            bool includeTipMessage, bool includeSpecialString);
        string GetTranslationDataForTranslate(Guid programGuid, int startDay, int endDay, bool includeRelapse, bool includeProgramRoom,
            bool includeProgramAccessory, bool includeEmailTemplate, bool includeHelpItem, bool includeUserMenu,
            bool includeTipMessage, bool includeSpecialString, Guid fromLanguageGuid);
        DataTable GetTranslationJobs(Guid translatorGuid);
        DataTable GetTranslationJobContents(Guid TranslationJobGUID);
        DataTable GetTranslationJobElements(Guid TranslationJobContentGUID);
        DataTable GetProgramUserPageVariable(Guid programGuid);
        DataTable GetProgramUserPageVariableExtension(Guid programGuid);
        //DataTable GetUserPageVariableValueAsDataTable(Guid programGuid);
        //DataTable GetUserPageVariableValueExtensionAsDataTable(Guid programGuid);
        DataTable GetProgramUserReport();
        DataTable GetPageBodyResource(Guid sessionGUID);
        DataTable GetSessionResource(Guid sessionGUID);

        int GetInactiveUserNumber(int inactiveDays);
        List<ProgramUser> GetInactiveUsers(int inactiveDays);
        int GetLoginUserNumber(int loginMinutes);
        int GetRegisteredUserNumber(int registeredDays);
        List<ProgramUser> GetMissedClassUsers(int missedDays, int pageNumber, int pageSize, out int totalCount);
        List<string> GetPageContentBySessionGUID(Guid sessionGuid);

        DataTable GetAllInformationForReminderEmailBeforeMailtime(DateTime Now);
        DataTable GetProgramDailySMSContentList(Guid ProgramGuid);
        void ExistProgramDailySMSListIntoShortMessageQueue(DateTime Now);
        void ExistRemindReportSMSListIntoShortMessageQueue(DateTime Now, int timeSpanMinutes);
        bool GetIsNeedReport(Guid programGuid, Guid userGuid, DateTime now);
        bool GetIsNeedHelp(Guid programGuid, Guid userGuid, DateTime now);
        int ShouldDoDay(Guid programUserGuid, DateTime now);

        DataTable UpdatePageVariableNameByProgramGuidAndPageVariableGuid(Guid programGuid, Guid pageVariableGuid, string newVariableName, string oldVariableName);
        DataTable GetPageVariableUsedTimesByProgramGuid(Guid programGuid, Guid pageVariableGuid, string oldVariableName);
        string UpdateLastSendEmailTimeAndInsertLog(Guid programGuid, Guid userGuid);

        //the services provider for ctpp
        List<SessionPageBody> GetAllPageBodyList(Guid ProgramGUID);
        List<SessionPageMediaResource> GetAllPageMediaResource(Guid ProgramGUID);
    }
}
