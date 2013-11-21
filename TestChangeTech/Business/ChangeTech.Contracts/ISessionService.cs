using System;
using System.Collections.Generic;
using ChangeTech.Models;
using ChangeTech.Entities;

namespace ChangeTech.Contracts
{
    public interface ISessionService
    {
        List<SessionModel> GetSessionsByProgramGuid(Guid programGuid);
        List<SessionModel> GetSessionsByProgramGuid(System.Guid programGuid, int pageNumber, int pageSize);
        List<SimpleSessionModel> GetSimpleSessionsByProgramGuid(Guid programGuid);
        CTPPEndUserPageModel GetCTPPEndUserPageModel(Guid programGuid, Guid userGuid, int pageNumber, int pageSize, CTPPVersionEnum ctppVersion);
        void AddSessionForProgram(SessionModel session, Guid programGuid);
        void AddMoreThanOneSessionForProgram(Guid programGuid, int days, int startDay);
        EditSessionModel GetSessionBySessonGuid(Guid sessionGuid);
        void DeleteSession(Guid sessionGuid);
        void MakeCopySession(Guid sessionGuid);
        bool IsSessionEnd(Guid programGuid, Guid UserGuid);
        void AdjustSessionUp(Guid sessionGuid);
        void AdjustSessionDown(Guid sessionGuid);
        void EditSession(Guid sessionGuid, string name, string description, int day, bool isNeedReport, bool isNeedHelp);
        bool GetIsNeedReport(Guid programGuid, Guid userGuid, DateTime now);
        bool GetIsNeedHelp(Guid programGuid, Guid userGuid, DateTime now);
        string GetSessionPreviewModelAsXML(Guid languageGuid, Guid sessionGuid, Guid userGuid);
        string GetLiveSessionModelAsXML(Guid userGuid, Guid programGuid, Guid languageGuid, int day);
        string GetEmptySessionXML(Guid programGuid, Guid userGuid, Guid languageGuid);
        Guid GetSessionGuidByProgarmAndDay(Guid programGuid, int day);
        Guid GetFirstSessionGUID(Guid programGuid);
        int GetLastSessionDay(Guid programGuid);
        int GetNumberOfSession(Guid programGuid);
        void CopyFromAnotherDay(Guid fromSessionGuid, Guid toSessionGuid);
        int GetNumberOfNormalSessions(Guid programGuid);
        int GetNumberOfCountdownSessions(Guid programGuid);
        int GetMinCountdownSessionDayNO(Guid programGuid);
        int GetMaxCountdownSessionDayNO(Guid programGuid);
        Session CloneSession(Session session, List<KeyValuePair<string, string>> cloneRelapseGUIDList, CloneProgramParameterModel cloneParameterModel);
        Session SetDefaultGuidForSession(Session needSetDefaultEntity, CloneProgramParameterModel cloneParameterModel);

        // for Win8Service
        List<SessionInfoModel> GetSessionInfoModelsByProgramGuid(Guid programGuid, ProgramUser pu);
        Dictionary<string, bool> GetRunSessionUrl(ProgramUser pu, int currentDay);
    }
}
