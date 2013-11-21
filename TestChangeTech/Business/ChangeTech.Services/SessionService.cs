using System.Linq;
using System.Collections.Generic;
using ChangeTech.Contracts;
using ChangeTech.IDataRepository;
using Ethos.DependencyInjection;
using ChangeTech.Entities;
using ChangeTech.Models;
using System;
using Ethos.Utility;


namespace ChangeTech.Services
{
    public class SessionService : ServiceBase, ISessionService
    {
        public const string MD5_KEY = "psycholo";
        public const string CHANGETECHPAGE = "ChangeTech.html";
        const int NOTDONE = 0;
        const int DONE = 1;
        const int STARTTODO = 2;
        #region ISessionService Members

        public List<SimpleSessionModel> GetSimpleSessionsByProgramGuid(Guid programGuid)
        {
            List<SimpleSessionModel> simpleSessions = new List<SimpleSessionModel>();
            IQueryable<Session> sessionEntities = Resolve<ISessionRepository>().GetSessionByProgramGuid(programGuid);
            foreach (Session sessionEntity in sessionEntities)
            {
                SimpleSessionModel simpleSession = new SimpleSessionModel
                {
                    ID = sessionEntity.SessionGUID,
                    Name = sessionEntity.Name,
                    dayNum = sessionEntity.Day == null ? "Null" : sessionEntity.Day.ToString()
                };
                simpleSessions.Add(simpleSession);
            }
            return simpleSessions;
        }

        public List<SessionModel> GetSessionsByProgramGuid(System.Guid programGuid, int pageNumber, int pageSize)
        {
            List<SessionModel> sessionsOfProgram = new List<SessionModel>();
            List<Session> sessionEntities = Resolve<ISessionRepository>().GetSessionByProgramGuid(programGuid).OrderBy(s => s.Day).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            foreach (Session sessionEntity in sessionEntities)
            {
                SessionModel sessionModel = new SessionModel();
                sessionModel.Day = sessionEntity.Day.HasValue ? sessionEntity.Day.Value : 0;
                sessionModel.ID = sessionEntity.SessionGUID;
                sessionModel.Name = sessionEntity.Name;
                sessionModel.Description = sessionEntity.Description;
                sessionModel.PageSequenceNumber = Resolve<ISessionContentRepository>().GetSessionContentBySessionGuid(sessionEntity.SessionGUID).Count();
                //sessionModel.PageSequences = Resolve<IPageSequenceService>().GetPageSequenceBySessionGuid(sessionEntity.SessionGUID);
                if (sessionEntity.LastUpdatedBy.HasValue)
                {
                    UserModel infoModel = Resolve<IUserService>().GetUserByUserGuid(sessionEntity.LastUpdatedBy.Value);
                    sessionModel.LastUpdateBy = new UserModel
                    {
                        UserGuid = infoModel.UserGuid,
                        UserName = infoModel.UserName,
                    };
                }
                else
                {
                    sessionModel.LastUpdateBy = new UserModel();
                }
                sessionModel.IsNeedReportButton = sessionEntity.IsNeedReport == null ? false : sessionEntity.IsNeedReport.Value;
                sessionModel.IsNeedHelpButton = sessionEntity.IsNeedHelp == null ? false : sessionEntity.IsNeedHelp.Value;
                sessionsOfProgram.Add(sessionModel);
            }
            return sessionsOfProgram;

        }

        //Get Sessions by ProgramGuid
        public List<SessionModel> GetSessionsByProgramGuid(System.Guid programGuid)
        {
            List<SessionModel> sessionsOfProgram = new List<SessionModel>();
            List<Session> sessionEntities = Resolve<ISessionRepository>().GetSessionByProgramGuid(programGuid).ToList();
            foreach (Session sessionEntity in sessionEntities)
            {
                SessionModel sessionModel = new SessionModel();
                sessionModel.Day = sessionEntity.Day.HasValue ? sessionEntity.Day.Value : 0;
                sessionModel.ID = sessionEntity.SessionGUID;
                sessionModel.Name = sessionEntity.Name;
                sessionModel.Description = sessionEntity.Description;
                //sessionModel.PageSequences = Resolve<IPageSequenceService>().GetPageSequenceBySessionGuid(sessionEntity.SessionGUID);
                if (sessionEntity.LastUpdatedBy.HasValue)
                {
                    UserModel infoModel = Resolve<IUserService>().GetUserByUserGuid(sessionEntity.LastUpdatedBy.Value);
                    sessionModel.LastUpdateBy = new UserModel
                    {
                        UserGuid = infoModel.UserGuid,
                        UserName = infoModel.UserName,
                    };
                }
                else
                {
                    sessionModel.LastUpdateBy = new UserModel();
                }
                sessionModel.IsNeedReportButton = sessionEntity.IsNeedReport == null ? false : sessionEntity.IsNeedReport.Value;
                sessionModel.IsNeedHelpButton = sessionEntity.IsNeedHelp == null ? false : sessionEntity.IsNeedHelp.Value;
                sessionsOfProgram.Add(sessionModel);
            }
            return sessionsOfProgram;
        }

        //for GetCTPPSessionModel function
        private CTPPEndUserPageModel BindCTPPSessionModelSpecialString(CTPPEndUserPageModel endUserPageModel, Guid programGuid)
        {
            LanguageModel thisLanguage = Resolve<IProgramService>().GetProgramLanguage(programGuid);
            List<SpecialStringModel> listSpecialString = new List<SpecialStringModel>();
            //if (thisLanguage.LanguageGUID.ToString().Trim().ToUpper() == "4C8140B5-25E0-46AC-A99E-6438B445C5B4")//English
            if (thisLanguage != null && thisLanguage.LanguageGUID != Guid.Empty)
            {
                listSpecialString = Resolve<ISpecialStringService>().GetSpecialStringByLanguage(thisLanguage.LanguageGUID);
            }
            else//Default language  Norwegian 
            {
                listSpecialString = Resolve<ISpecialStringService>().GetSpecialStringByLanguage(new Guid("FB5AE1DC-4CAF-4613-9739-7397429DDF25"));
            }
            for (int i = 0; i < listSpecialString.Count; i++)
            {
                switch (listSpecialString[i].Name.Trim())
                {
                    case "Buy":
                        endUserPageModel.SpeSBuy = listSpecialString[i].Value;
                        break;
                    case "Buy_subtext":
                        endUserPageModel.SpeSBuy_subtext = listSpecialString[i].Value;
                        break;
                    case "Click_a_day":
                        endUserPageModel.SpeSClick_a_day = listSpecialString[i].Value;
                        break;
                    case "Completed":
                        endUserPageModel.SpeSCompleted = listSpecialString[i].Value;
                        break;
                    case "Days_in_program":
                        endUserPageModel.SpeSDays_in_program = listSpecialString[i].Value;
                        break;
                    case "Login":
                        endUserPageModel.SpeSLogin = listSpecialString[i].Value;
                        break;
                    case "Logout":
                        endUserPageModel.SpeSLogout = listSpecialString[i].Value;
                        break;
                    case "Other_programs_from":
                        endUserPageModel.SpeSOther_programs_from = listSpecialString[i].Value;
                        break;
                    case "Price":
                        endUserPageModel.SpeSPrice = listSpecialString[i].Value;
                        break;
                    case "Price_subtext":
                        endUserPageModel.SpeSBuy_subtext = listSpecialString[i].Value;
                        break;
                    case "Start_day":
                        endUserPageModel.SpeSStart_day = listSpecialString[i].Value;
                        break;
                    case "unavailable":
                        endUserPageModel.SpeSUnavailable = listSpecialString[i].Value;
                        break;

                    case "untaken":
                        endUserPageModel.SpeSUntaken = listSpecialString[i].Value;
                        break;
                    case "provided_by":
                        endUserPageModel.SpeSProvided_by = listSpecialString[i].Value;
                        break;
                    case "use_from":
                        endUserPageModel.SpeSUseFrom = listSpecialString[i].Value;
                        break;
                    case "all_rights_reserved":
                        endUserPageModel.SpeSAllRightsReserved = listSpecialString[i].Value;
                        break;
                    case "Free":
                        endUserPageModel.SpeSFree = listSpecialString[i].Value;
                        break;
                    case "Video_Subtext_1":
                        endUserPageModel.SpeSVideoSubtext1 = listSpecialString[i].Value;
                        break;
                    case "Video_Subtext_2":
                        endUserPageModel.SpeSVideoSubtext2 = listSpecialString[i].Value;
                        break;
                    case "Retake":
                        endUserPageModel.SpeSRetake = listSpecialString[i].Value;
                        break;
                    case "Ready_To_Go":
                        endUserPageModel.SpeSReadyToGo = listSpecialString[i].Value;
                        break;
                    case "More_about_us_at_FB":
                        endUserPageModel.SpeSMoreBeforeFB = listSpecialString[i].Value;
                        break;

                    /*Special String for help/report button in CTPP.   Start from here*/
                    case "ReportButton_Heading":
                        endUserPageModel.SpeSReportButtonHeading = listSpecialString[i].Value;
                        break;
                    case "ReportButton_Actual":
                        endUserPageModel.SpeSReportButtonActual = listSpecialString[i].Value;
                        break;
                    case "ReportButton_Complete":
                        endUserPageModel.SpeSReportButtonComplete = listSpecialString[i].Value;
                        break;
                    case "ReportButton_Untaken":
                        endUserPageModel.SpeSReportButtonUntaken = listSpecialString[i].Value;
                        break;
                    case "HelpButton_Heading":
                        endUserPageModel.SpeSHelpButtonHeading = listSpecialString[i].Value;
                        break;
                    case "HelpButton_Actual":
                        endUserPageModel.SpeSHelpButtonActual = listSpecialString[i].Value;
                        break;


                    /*Special String for help/report button in CTPP.   End here*/
                    case "CTPPSmartPhoneHomescreenHeading":
                        endUserPageModel.SpeSContainerHomescreenHeading = listSpecialString[i].Value;
                        break;
                    case "CTPPSmartPhoneHomescreenText":
                        endUserPageModel.SpeSContainerHomescreenText = listSpecialString[i].Value;
                        break;
                    case "CTPPSmartPhoneBelowHelpButtonText":
                        endUserPageModel.SpeSContainerBelowHelpbuttonText = listSpecialString[i].Value;
                        break;
                    /*Special string for some smart phone end.*/

                }
            }


            return endUserPageModel;
        }

        //for GetCTPPSessionModel function
        private CTPPEndUserPageModel BindCTPPSessionModelOrderURL(CTPPEndUserPageModel endUserPageModel, Guid programGuid)
        {
            ProgramPropertyModel thisProgramPropgertyModel = Resolve<IProgramService>().GetProgramProperty(programGuid);
            string serverPath = string.Empty;
            if (thisProgramPropgertyModel.IsSupportHttps)
            {
                serverPath = Resolve<ISystemSettingService>().GetSettingValue("WebServerPathHttps");
            }
            else
            {
                serverPath = Resolve<ISystemSettingService>().GetSettingValue("WebServerPath");
            }
            string screenurl = string.Format(serverPath + CHANGETECHPAGE + "?Mode=Trial&P={0}", thisProgramPropgertyModel.ProgramCode);
            endUserPageModel.orderUrl = screenurl;
            return endUserPageModel;
        }

        //for GetCTPPSessionModel function
        private CTPPEndUserPageModel BindCTPPSessionModelSessionListForPC(CTPPEndUserPageModel endUserPageModel, Guid programGuid, Guid userGuid, int pageNumber, int pageSize)
        {
            CTPPModel thisCTPPModel = Resolve<ICTPPService>().GetCTPP(programGuid);
            int IsInActive = 0;
            if (thisCTPPModel != null)
            {
                IsInActive = thisCTPPModel.InActive;
            }

            List<CTPPSessionModel> ctppSessionModel = new List<CTPPSessionModel>();
            List<Session> sessionEntities = Resolve<ISessionRepository>().GetSessionByProgramGuid(programGuid)
                .Where(s => s.Day >= 0).Skip
                ((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            // [Liubo] : 
            ProgramUser programUser = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuidAndUserGuid(programGuid, userGuid);//get programUser Model
            List<ProgramUserSession> programUserSessionEntities = new List<ProgramUserSession>();
            if (programUser!=null)
            {
                programUserSessionEntities = Resolve<IProgramUserSessionRepository>().GetProgramUserSessionListByProgramUserGuid(programUser.ProgramUserGUID).ToList();//Get ProgramUserSession List by programUserGuid
            }

            int programUserDay = int.MinValue;
            DateTime setCurrentTimeByTimeZone = DateTime.UtcNow;
            if (userGuid != Guid.Empty)
            {
                setCurrentTimeByTimeZone = Resolve<IProgramUserService>().GetCurrentTimeByTimeZone(programGuid, userGuid, DateTime.UtcNow);
                programUserDay = Resolve<IProgramUserService>().GetShouldDoDay(programGuid, userGuid, setCurrentTimeByTimeZone);
            }

            int status = 0;//the status of IsThereClassToday
            int isActiveDay0 = 0;//0 means day0 is untaken and 1 means taken.

            foreach (Session sessionEntity in sessionEntities)
            {
                CTPPSessionModel itemSessionModel = new CTPPSessionModel();

                itemSessionModel.Day = (int)sessionEntity.Day;
                itemSessionModel.Description = sessionEntity.Description == null ? string.Empty : sessionEntity.Description.Replace("\r\n", "<br/>").Replace("\r", "<br/>").Replace("\n", "<br/>");
                itemSessionModel.ID = sessionEntity.SessionGUID;
                itemSessionModel.DIVSTRStart = string.Empty;
                itemSessionModel.DIVSTREnd = string.Empty;
                itemSessionModel.IsHasDone = NOTDONE;//0:no;1:Done;2:start
                if (programUserDay == int.MinValue || programUserDay < 0)//if not login，day 0 must be the start day，other untaken class，judge one by one。// <0 means day -1,-2,-3 etc
                {
                    itemSessionModel.IsHasDone = NOTDONE;
                    if (itemSessionModel.Day == 0 && IsInActive == 0)
                    {
                        if (programUserDay < 0 && programUserDay != int.MinValue)
                        {
                            itemSessionModel.IsHasDone = DONE;
                            isActiveDay0 = 1;
                        }
                        else
                        {
                            //itemSessionModel.IsHasDone = 2;
                            itemSessionModel.IsHasDone = NOTDONE;
                        }
                    }

                    if (itemSessionModel.Day == 0 || sessionEntities.IndexOf(sessionEntity) == 0)
                    {
                        if (itemSessionModel.IsHasDone == NOTDONE)
                        {
                            itemSessionModel.DIVSTRStart = "</div><div class=\"container-untaken\" style=\"margin:0px; border-radius:0px 0px 8px 8px;\" >";
                        }
                        else
                        {
                            itemSessionModel.DIVSTRStart = "</div><div class=\"container-taken\">";
                        }
                    }
                    else if (sessionEntities.IndexOf(sessionEntity) == pageSize)
                    {
                        itemSessionModel.DIVSTREnd = "</div>";
                    }
                    else if (itemSessionModel.Day == 1 && IsInActive == 0 && isActiveDay0 == 1)
                    {
                        itemSessionModel.DIVSTRStart = "</div><div class=\"container-untaken\">";
                    }
                }
                else
                {
                    //0:no;1:Done;2:start
                    if (itemSessionModel.Day > programUserDay)
                    {
                        
                        itemSessionModel.IsHasDone = NOTDONE;
                    }
                    else if (itemSessionModel.Day < programUserDay)
                    {
                        //itemSessionModel.IsHasDone = DONE;
                        //judge which session user have done from ProgramUserSession table. Default is STARTTODO of session status
                        itemSessionModel.IsHasDone = STARTTODO;
                        itemSessionModel.IsHasDone = programUserSessionEntities.Where(pus => pus.SessionGUID == sessionEntity.SessionGUID).Count() == 0 ? STARTTODO : DONE;
                        //if (programUserSessionEntities != null && programUserSessionEntities.Count != 0)
                        //{
                            //foreach (ProgramUserSession programUserSession in programUserSessionEntities)
                            //{
                            //    if (programUserSession.SessionGUID == sessionEntity.SessionGUID)
                            //    {
                            //        itemSessionModel.IsHasDone = DONE;
                            //    }
                            //}
                        //}
                    }
                    else
                    {
                        itemSessionModel.IsHasDone = STARTTODO;
                        itemSessionModel.IsHasDone = programUserSessionEntities.Where(pus => pus.SessionGUID == sessionEntity.SessionGUID).Count() == 0 ? STARTTODO : DONE;
                        //if (programUserSessionEntities != null && programUserSessionEntities.Count != 0)
                        //{
                        //    foreach (ProgramUserSession programUserSession in programUserSessionEntities)
                        //    {
                        //        if (programUserSession.SessionGUID == sessionEntity.SessionGUID)
                        //        {
                        //            itemSessionModel.IsHasDone = DONE;
                        //        }
                        //    }
                        //}
                        if (Resolve<IProgramService>().GetProgramByGUID(programGuid).IsNoCatchUp == false)
                        {
                            status = Resolve<IProgramUserService>().IsThereClassToday(userGuid, programGuid, setCurrentTimeByTimeZone);
                            if (status != 0)//Only equals 0, can start this day.
                            {
                                itemSessionModel.IsHasDone = NOTDONE;
                            }
                        }
                        
                    }

                    if (sessionEntities.IndexOf(sessionEntity) == 0)
                    {
                        //if (itemSessionModel.Day > programUserDay)
                        if (itemSessionModel.IsHasDone == NOTDONE)
                        {
                            itemSessionModel.DIVSTRStart = "</div><div class=\"container-untaken\">";
                        }
                        else
                        {
                            itemSessionModel.DIVSTRStart = "</div><div class=\"container-taken\">";
                        }
                    }
                    else if (sessionEntities.IndexOf(sessionEntity) == pageSize)
                    {
                        itemSessionModel.DIVSTREnd = "</div>";
                    }
                    else if (sessionEntity.Day == programUserDay)//judge whether need change the class of "start day or not done day"
                    {
                        if (itemSessionModel.IsHasDone == NOTDONE)
                        {
                            itemSessionModel.DIVSTRStart = "</div><div class=\"container-untaken\">";
                        }

                    }
                    else if (sessionEntity.Day == programUserDay + 1)
                    {
                        if (status == 0)
                        {
                            itemSessionModel.DIVSTRStart = "</div><div class=\"container-untaken\">";
                        }
                    }

                    //0:no;1:Done;2:start
                }
                itemSessionModel.Name = sessionEntity.Name;

                ctppSessionModel.Add(itemSessionModel);
            }

            endUserPageModel.endUserSessionModel = ctppSessionModel;
            return endUserPageModel;
        }


        //for GetCTPPSessionModel function
        private CTPPEndUserPageModel BindCTPPSessionModelSessionListForSmartPhone(CTPPEndUserPageModel endUserPageModel, Guid programGuid, Guid userGuid, int pageNumber, int pageSize)
        {
            CTPPModel thisCTPPModel = Resolve<ICTPPService>().GetCTPP(programGuid);
            int IsInActive = 0;
            if (thisCTPPModel != null)
            {
                IsInActive = thisCTPPModel.InActive;
            }

            List<CTPPSessionModel> ctppSessionModel = new List<CTPPSessionModel>();
            List<Session> sessionEntities = Resolve<ISessionRepository>().GetSessionByProgramGuid(programGuid)
                .Where(s => s.Day >= 0).Skip
                ((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            // [Liubo] : 
            ProgramUser programUser = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuidAndUserGuid(programGuid, userGuid);//get programUser Model
            List<ProgramUserSession> programUserSessionEntities = null;
            if (programUser != null)
            {
                programUserSessionEntities = Resolve<IProgramUserSessionRepository>().GetProgramUserSessionListByProgramUserGuid(programUser.ProgramUserGUID).ToList();//Get ProgramUserSession List by programUserGuid
            }

            int programUserDay = int.MinValue;
            DateTime setCurrentTimeByTimeZone = DateTime.UtcNow;
            if (userGuid != Guid.Empty)
            {
                setCurrentTimeByTimeZone = Resolve<IProgramUserService>().GetCurrentTimeByTimeZone(programGuid, userGuid, DateTime.UtcNow);
                programUserDay = Resolve<IProgramUserService>().GetShouldDoDay(programGuid, userGuid, setCurrentTimeByTimeZone);
            }

            int status = 0;//the status of IsThereClassToday
            int isActiveDay0 = 0;//0 means day0 is untaken and 1 means taken.

            foreach (Session sessionEntity in sessionEntities)
            {
                CTPPSessionModel itemSessionModel = new CTPPSessionModel();

                itemSessionModel.Day = (int)sessionEntity.Day;
                itemSessionModel.Description = sessionEntity.Description == null ? string.Empty : sessionEntity.Description.Replace("\r\n", "<br/>").Replace("\r", "<br/>").Replace("\n", "<br/>");
                itemSessionModel.ID = sessionEntity.SessionGUID;
                itemSessionModel.DIVSTRStart = string.Empty;
                itemSessionModel.DIVSTREnd = string.Empty;
                itemSessionModel.IsHasDone = NOTDONE;//0:no;1:Done;2:start
                if (programUserDay == int.MinValue || programUserDay < 0)//if not login，day 0 must be the start day，other untaken class，judge one by one。// <0 means day -1,-2,-3 etc
                {
                    itemSessionModel.IsHasDone = NOTDONE;
                    if (itemSessionModel.Day == 0 && IsInActive == 0)
                    {
                        if (programUserDay < 0 && programUserDay != int.MinValue)
                        {
                            itemSessionModel.IsHasDone = DONE;
                            isActiveDay0 = 1;
                        }
                        else
                        {
                            //itemSessionModel.IsHasDone = 2;
                            itemSessionModel.IsHasDone = NOTDONE;
                        }
                    }

                    if (itemSessionModel.Day == 0 || sessionEntities.IndexOf(sessionEntity) == 0)
                    {
                        if (itemSessionModel.IsHasDone == NOTDONE)
                        {
                            itemSessionModel.DIVSTRStart = "</div><div class=\"container-untaken\" >";
                        }
                        else
                        {
                            itemSessionModel.DIVSTRStart = "</div><div class=\"container-taken\">";
                        }
                    }
                    else if (sessionEntities.IndexOf(sessionEntity) == pageSize)
                    {
                        itemSessionModel.DIVSTREnd = "</div>";
                    }
                    else if (itemSessionModel.Day == 1 && IsInActive == 0 && isActiveDay0 == 1)
                    {
                        itemSessionModel.DIVSTRStart = "</div><div class=\"container-untaken\">";
                    }
                }
                else
                {
                    //0:no;1:Done;2:start// for smart phone ,only done and not done. 
                    //Because there is no extend part, nowhere to start or retake, the start session need be regarded as not done.
                    if (itemSessionModel.Day >= programUserDay)
                    {
                        itemSessionModel.IsHasDone = NOTDONE;
                    }
                    else if (itemSessionModel.Day < programUserDay)
                    {
                        //itemSessionModel.IsHasDone = DONE;
                        //[LiuBo]:
                        itemSessionModel.IsHasDone = STARTTODO;
                        itemSessionModel.IsHasDone = programUserSessionEntities.Where(pus => pus.SessionGUID == sessionEntity.SessionGUID).Count() == 0 ? STARTTODO : DONE;
                    }

                    if (sessionEntities.IndexOf(sessionEntity) == 0)
                    {
                        //if (itemSessionModel.Day > programUserDay)
                        if (itemSessionModel.IsHasDone == NOTDONE)
                        {
                            itemSessionModel.DIVSTRStart = "</div><div class=\"container-untaken\">";
                        }
                        else
                        {
                            itemSessionModel.DIVSTRStart = "</div><div class=\"container-taken\">";
                        }
                    }
                    else if (sessionEntities.IndexOf(sessionEntity) == pageSize)
                    {
                        itemSessionModel.DIVSTREnd = "</div>";
                    }
                    else if (sessionEntity.Day == programUserDay)
                    {
                        if (status == 0)
                        {
                            itemSessionModel.DIVSTRStart = "</div><div class=\"container-untaken\">";
                        }
                    }

                    //0:no;1:Done;2:start
                }
                itemSessionModel.Name = sessionEntity.Name;

                ctppSessionModel.Add(itemSessionModel);
            }

            endUserPageModel.endUserSessionModel = ctppSessionModel;
            return endUserPageModel;
        }

        //This is for PC, GetCTPPSessionModelForSmartPhone is for smart phone. If change one ,consider the other
        public CTPPEndUserPageModel GetCTPPEndUserPageModel(Guid programGuid, Guid userGuid, int pageNumber, int pageSize, CTPPVersionEnum ctppVersion)
        {
            CTPPEndUserPageModel endUserPageModel = new CTPPEndUserPageModel();

            //bind special string 
            endUserPageModel = BindCTPPSessionModelSpecialString(endUserPageModel, programGuid);

            //bind orderurl
            endUserPageModel = BindCTPPSessionModelOrderURL(endUserPageModel, programGuid);

            //bind session list
            switch (ctppVersion)
            {
                case CTPPVersionEnum.PCVersion:
                    endUserPageModel = BindCTPPSessionModelSessionListForPC(endUserPageModel, programGuid, userGuid, pageNumber, pageSize);
                    break;
                case CTPPVersionEnum.SmartphoneVersion:
                    endUserPageModel = BindCTPPSessionModelSessionListForSmartPhone(endUserPageModel, programGuid, userGuid, pageNumber, pageSize);
                    break;
            }

            return endUserPageModel;
        }


        public void AddSessionForProgram(SessionModel session, Guid programGuid)
        {
            Program program = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
            Session newSession = new Session();
            newSession.Day = session.Day;
            newSession.Description = session.Description;
            newSession.Name = session.Name;
            newSession.SessionGUID = Guid.NewGuid();
            //newSession.Program = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
            //Resolve<ISessionRepository>().InsertSession(newSession);
            newSession.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
            if (session.Day >= 0)
            {
                if (!program.Session.IsLoaded)
                {
                    program.Session.Load();
                }
                foreach (Session item in program.Session)
                {
                    if (item.Day >= newSession.Day && item.SessionGUID != newSession.SessionGUID)
                    {
                        item.Day++;
                    }
                }
            }
            program.Session.Add(newSession);
            program.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
            Resolve<IProgramRepository>().Update(program);
        }

        public void AddMoreThanOneSessionForProgram(Guid programGuid, int days, int startDay)
        {
            Program program = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
            if (!program.Session.IsLoaded)
            {
                program.Session.Load();
            }
            // adjust old days
            foreach (Session session in program.Session)
            {
                if (session.Day >= startDay)
                {
                    session.Day += days;
                }
            }
            // add new days
            for (int i = 0; i < days; i++)
            {
                Session newsession = new Session();
                newsession.SessionGUID = Guid.NewGuid();
                newsession.Name = "newsession";
                newsession.Day = startDay + i;
                newsession.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;

                program.Session.Add(newsession);
            }

            program.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
            Resolve<IProgramRepository>().Update(program);
        }

        public EditSessionModel GetSessionBySessonGuid(Guid sessionGuid)
        {
            EditSessionModel editSessionModel = new EditSessionModel();
            editSessionModel.PageSequences = Resolve<IPageSequenceService>().GetPageSequenceBySessionGuid(sessionGuid);
            Session session = Resolve<ISessionRepository>().GetSessionBySessionGuid(sessionGuid);
            if (!session.ProgramReference.IsLoaded)
            {
                session.ProgramReference.Load();
            }
            if (!session.Program.ProgramStatusReference.IsLoaded)
            {
                session.Program.ProgramStatusReference.Load();
            }

            editSessionModel.ProgramName = session.Program.Name;
            editSessionModel.ProgramGuid = session.Program.ProgramGUID;
            editSessionModel.Name = session.Name;
            editSessionModel.Day = (int)session.Day;
            editSessionModel.Description = session.Description;
            editSessionModel.ProgramStatusGuid = session.Program.ProgramStatus.ProgramStatusGUID;
            ProgramService programService = new ProgramService();
            editSessionModel.IsLiveProgram = programService.IsLiveProgram(session.Program.ProgramStatus.ProgramStatusGUID);
            editSessionModel.IsNeedReportButton = session.IsNeedReport == null ? false : session.IsNeedReport.Value;
            editSessionModel.IsNeedHelpButton = session.IsNeedHelp == null ? false : session.IsNeedHelp.Value;
            return editSessionModel;
        }

        public void DeleteSession(Guid sessionGuid)
        {
            Session delSession = Resolve<ISessionRepository>().GetSessionBySessionGuid(sessionGuid);
            if (!delSession.SessionContent.IsLoaded)
            {
                delSession.SessionContent.Load();
            }
            // adjust session, move the following session up
            AdjustSessionForDelete(delSession);
            // delete sessionContent
            Resolve<ISessionContentRepository>().DeleteSessionContent(delSession.SessionContent);
            // delete session
            Resolve<ISessionRepository>().DeleteSession(sessionGuid);
        }

        public void MakeCopySession(Guid sessionGuid)
        {
            Session session = Resolve<ISessionRepository>().GetSessionBySessionGuid(sessionGuid);
            if (!session.ProgramReference.IsLoaded)
            {
                session.ProgramReference.Load();
            }
            //Session newSession = ServiceUtility.CloneSessionWithExistPageSequenceNotIncludeParentGuid(session);//The previous version is :CloneSessionWithExistPageSequence
            //The old function above use the existed pageSequence,this will lead to that one pageSequence in used in more than one session
            //The new function below copy the pageSequence to new one.
            CloneProgramParameterModel cloneParameterModel = new CloneProgramParameterModel
            {
                ProgramGuidOfCopiedToProgramsInDefaultLanguage = Guid.Empty,
                source = DefaultGuidSourceEnum.FromNull,
            };
            Session newSession = CloneSession(session, new List<KeyValuePair<string, string>>(), cloneParameterModel);
            newSession = SetDefaultGuidForSession(newSession, cloneParameterModel);

            newSession.Program = session.Program;

            // make the new session the last session
            newSession.Day = Resolve<ISessionRepository>().GetLastSessionOfProgram(newSession.Program.ProgramGUID).Day + 1;

            Resolve<ISessionRepository>().InsertSession(newSession);
        }

        /// <summary>
        /// Judge the session of program is end
        /// </summary>
        /// <param name="programGuid">Program Guid</param>
        /// <param name="UserGuid">User Guid</param>
        /// <returns></returns>
        public bool IsSessionEnd(Guid programGuid, Guid UserGuid)
        {
            ProgramUser pUser = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuidAndUserGuid(programGuid, UserGuid);
            bool flag = false;
            Session currentSession = Resolve<ISessionRepository>().GetSessionByProgramGuidAndDay(programGuid, pUser.Day.Value + 1);
            if (currentSession != null)
            {
                flag = true;
            }
            return flag;
        }

        public void AdjustSessionUp(Guid sessionGuid)
        {
            Session movedSession = Resolve<ISessionRepository>().GetSessionBySessionGuid(sessionGuid);

            if (!movedSession.ProgramReference.IsLoaded)
            {
                movedSession.ProgramReference.Load();
            }
            Guid programGuid = movedSession.Program.ProgramGUID;

            // if the current day is not session 0
            if (movedSession.Day.Value > 0)
            {
                int downSessionDay = (int)movedSession.Day - 1;

                Session downSession = Resolve<ISessionRepository>().GetSessionByProgramGuidAndDay(programGuid, downSessionDay);
                downSession.Day++;
                movedSession.Day--;

                Resolve<ISessionRepository>().UpdateSession(movedSession);
                Resolve<ISessionRepository>().UpdateSession(downSession);
            }
            else if (movedSession.Day.Value < 0)
            {
                Session nextMinSession = Resolve<ISessionRepository>().GetSessionByProgramGuidAndDay(programGuid, movedSession.Day.Value - 1);
                if (nextMinSession != null)
                {
                    nextMinSession.Day++;
                    Resolve<ISessionRepository>().UpdateSession(nextMinSession);
                }
                movedSession.Day--;
                Resolve<ISessionRepository>().UpdateSession(movedSession);
            }
        }

        /// <summary>
        /// -5 is min, -1 is max
        /// </summary>
        /// <param name="programGuid"></param>
        /// <returns></returns>
        public int GetMinCountdownSessionDayNO(Guid programGuid)
        {
            int sessionDayNO = 0;
            Session sessionEntity = Resolve<ISessionRepository>().GetSessionByProgramGuid(programGuid).Where(s => s.Day.HasValue && s.Day.Value < 0).OrderBy(s => s.Day).FirstOrDefault();
            if (sessionEntity != null)
            {
                sessionDayNO = sessionEntity.Day.Value;
            }
            return sessionDayNO;
        }

        public int GetMaxCountdownSessionDayNO(Guid programGuid)
        {
            int sessionDayNO = 0;
            Session sessionEntity = Resolve<ISessionRepository>().GetSessionByProgramGuid(programGuid).Where(s => s.Day.HasValue && s.Day.Value < 0).OrderByDescending(s => s.Day).FirstOrDefault();
            if (sessionEntity != null)
            {
                sessionDayNO = sessionEntity.Day.Value;
            }
            return sessionDayNO;
        }

        private int GetNextMaxCountdownSessionDayNO(Guid programGuid, int currentDayNO)
        {
            int sessionDayNO = 0;
            Session sessionEntity = Resolve<ISessionRepository>().GetSessionByProgramGuid(programGuid).Where(s => s.Day.HasValue && s.Day.Value < 0 && s.Day.Value > currentDayNO).OrderByDescending(s => s.Day).First();
            if (sessionEntity != null)
            {
                sessionDayNO = sessionEntity.Day.Value;
            }
            return sessionDayNO;
        }

        private int GetNextMinCountdownSessionDayNO(Guid programGuid, int currentDayNO)
        {
            int sessionDayNO = 0;
            Session sessionEntity = Resolve<ISessionRepository>().GetSessionByProgramGuid(programGuid).Where(s => s.Day.HasValue && s.Day.Value < 0 && s.Day < currentDayNO).OrderByDescending(s => s.Day).First();
            if (sessionEntity != null)
            {
                sessionDayNO = sessionEntity.Day.Value;
            }
            return sessionDayNO;
        }

        public void AdjustSessionDown(Guid sessionGuid)
        {
            Session movedSession = Resolve<ISessionRepository>().GetSessionBySessionGuid(sessionGuid);

            if (!movedSession.ProgramReference.IsLoaded)
            {
                movedSession.ProgramReference.Load();
            }
            Guid programGuid = movedSession.Program.ProgramGUID;
            int upSessionDay = (int)movedSession.Day + 1;

            if (movedSession.Day > 0)
            {
                Session upSession = Resolve<ISessionRepository>().GetSessionByProgramGuidAndDay(programGuid, upSessionDay);
                if (upSession != null)
                {
                    upSession.Day--;
                    movedSession.Day++;

                    Resolve<ISessionRepository>().UpdateSession(movedSession);
                    Resolve<ISessionRepository>().UpdateSession(upSession);
                }
            }
            else if (movedSession.Day < 0)
            {
                Session nextMaxSession = Resolve<ISessionRepository>().GetSessionByProgramGuidAndDay(programGuid, movedSession.Day.Value + 1);
                if (nextMaxSession != null)
                {
                    nextMaxSession.Day--;
                    Resolve<ISessionRepository>().UpdateSession(nextMaxSession);
                }
                movedSession.Day++;
                Resolve<ISessionRepository>().UpdateSession(movedSession);
            }
        }

        public void EditSession(Guid sessionGuid, string name, string description, int day, bool isNeedReport, bool isNeedHelp)
        {
            Session editSession = Resolve<ISessionRepository>().GetSessionBySessionGuid(sessionGuid);
            int oldDay = editSession.Day.Value;
            if (!editSession.ProgramReference.IsLoaded)
            {
                editSession.ProgramReference.Load();
            }
            Program program = editSession.Program;
            if (!program.Session.IsLoaded)
            {
                program.Session.Load();
            }

            editSession.Name = name;
            editSession.Description = description;
            editSession.IsNeedReport = isNeedReport;
            editSession.IsNeedHelp = isNeedHelp;
            editSession.Day = day;
            editSession.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
            if (oldDay == day)
            {
                Resolve<ISessionRepository>().UpdateSession(editSession);
                Resolve<ITranslationJobService>().UpdateElementWhenFromUpdated("Session", editSession.SessionGUID.ToString(), Guid.Empty);
            }
            else if (oldDay >= 0)
            {
                if (oldDay < day)
                {
                    foreach (Session item in program.Session)
                    {
                        if (item.Day > oldDay && item.Day <= day && item.SessionGUID != sessionGuid)
                        {
                            item.Day--;
                        }
                    }
                    Resolve<IProgramRepository>().Update(program);
                }
                else
                {
                    foreach (Session item in program.Session)
                    {
                        if (item.Day >= day && item.Day < oldDay && item.SessionGUID != sessionGuid)
                        {
                            item.Day++;
                        }
                    }
                    Resolve<IProgramRepository>().Update(program);
                }
            }
            else
            {
                Session sessionEntity = Resolve<ISessionRepository>().GetSessionByProgramGuidAndDay(program.ProgramGUID, day);
                if (sessionEntity != null)
                {
                    if (oldDay < day)
                    {
                        foreach (Session item in program.Session)
                        {
                            if (item.Day < oldDay && item.Day >= day && item.SessionGUID != sessionGuid)
                            {
                                item.Day--;
                            }
                        }
                        Resolve<IProgramRepository>().Update(program);
                    }
                    else
                    {
                        foreach (Session item in program.Session)
                        {
                            if (item.Day < oldDay && item.Day >= day && item.SessionGUID != sessionGuid)
                            {
                                item.Day++;
                            }
                        }
                        Resolve<IProgramRepository>().Update(program);
                    }
                }
                else
                {
                    editSession.Day = day;
                    Resolve<ISessionRepository>().UpdateSession(editSession);
                    Resolve<ITranslationJobService>().UpdateElementWhenFromUpdated("Session", editSession.SessionGUID.ToString(), Guid.Empty);
                }
            }

            InsertLogModel insertLogModel = new InsertLogModel
            {
                ActivityLogType = (int)LogTypeEnum.ModifyDay,
                Browser = string.Empty,
                From = string.Empty,
                IP = string.Empty,
                Message = "Edit Session",
                PageGuid = Guid.Empty,
                PageSequenceGuid = Guid.Empty,
                SessionGuid = editSession.SessionGUID,
                ProgramGuid = editSession.Program.ProgramGUID,
                UserGuid = editSession.LastUpdatedBy.Value
            };
            Resolve<IActivityLogService>().Insert(insertLogModel);
        }

        public bool GetIsNeedReport(Guid programGuid, Guid userGuid, DateTime now)
        {
            return Resolve<IStoreProcedure>().GetIsNeedReport(programGuid, userGuid, now);
        }

        public bool GetIsNeedHelp(Guid programGuid, Guid userGuid, DateTime now)
        {
            return Resolve<IStoreProcedure>().GetIsNeedHelp(programGuid, userGuid, now);
        }

        public string GetSessionPreviewModelAsXML(Guid languageGuid, Guid sessionGuid, Guid userGuid)
        {
            return Resolve<IStoreProcedure>().GetPreviewSessionModelAsXML(languageGuid, sessionGuid, userGuid);
        }

        public string GetLiveSessionModelAsXML(Guid userGuid, Guid programGuid, Guid languageGuid, int day)
        {
            return Resolve<IStoreProcedure>().GetLiveSessionModelAsXML(userGuid, programGuid, languageGuid, day);
        }

        public string GetEmptySessionXML(Guid programGuid, Guid userGuid, Guid languageGuid)
        {
            return Resolve<IStoreProcedure>().GetEmptySessionXML(programGuid, userGuid, languageGuid);
        }

        public Guid GetSessionGuidByProgarmAndDay(Guid programGuid, int day)
        {
            Guid reSessionGuid = Guid.Empty;
            Session session = Resolve<ISessionRepository>().GetSessionByProgramGuidAndDay(programGuid, day);
            if (session != null)
            {
                reSessionGuid = session.SessionGUID;
            }
            return reSessionGuid;
        }

        public int GetLastSessionDay(Guid programGuid)
        {
            int day = 0;
            Session lastSession = Resolve<ISessionRepository>().GetLastSessionOfProgram(programGuid);
            if (lastSession != null)
            {
                day = Convert.ToInt32(lastSession.Day);
            }

            return day;
        }

        public int GetNumberOfSession(Guid programGuid)
        {
            return Resolve<ISessionRepository>().GetSessionByProgramGuid(programGuid).Count();
        }

        public int GetNumberOfNormalSessions(Guid programGuid)
        {
            return Resolve<ISessionRepository>().GetSessionByProgramGuid(programGuid).Where(s => s.Day.HasValue && s.Day.Value >= 0).Count();
        }

        public int GetNumberOfCountdownSessions(Guid programGuid)
        {
            return Resolve<ISessionRepository>().GetSessionByProgramGuid(programGuid).Where(s => s.Day.HasValue && s.Day.Value < 0).Count();
        }

        public Guid GetFirstSessionGUID(Guid programGuid)
        {
            return Resolve<ISessionRepository>().GetSessionByProgramGuid(programGuid).FirstOrDefault().SessionGUID;
        }

        public void CopyFromAnotherDay(Guid fromSessionGuid, Guid toSessionGuid)
        {
            Session fromsessionentity = Resolve<ISessionRepository>().GetSessionBySessionGuid(fromSessionGuid);
            if (!fromsessionentity.SessionContent.IsLoaded)
            {
                fromsessionentity.SessionContent.Load();
            }

            Session tosessionentity = Resolve<ISessionRepository>().GetSessionBySessionGuid(toSessionGuid);
            if (!tosessionentity.SessionContent.IsLoaded)
            {
                tosessionentity.SessionContent.Load();
            }

            //Add a judge for whether need copy relapse first. If from and to are not in the same program, need do this.
            if (!fromsessionentity.ProgramReference.IsLoaded) fromsessionentity.ProgramReference.Load();
            if (!tosessionentity.ProgramReference.IsLoaded) tosessionentity.ProgramReference.Load();
            List<KeyValuePair<string, string>> relpaseGUIDPairList = new List<KeyValuePair<string, string>>();
            CloneProgramParameterModel cloneParameterModel = new CloneProgramParameterModel
            {
                ProgramGuidOfCopiedToProgramsInDefaultLanguage = Guid.Empty,
                source = DefaultGuidSourceEnum.FromNull,
            };
            List<Guid> pageSequenceGuidInRelapse = new List<Guid>();//For copy pagevariable in relapse
            if (fromsessionentity.Program.ProgramGUID != tosessionentity.Program.ProgramGUID)//need copy relapse // and copy pageVariableGroup
            {
                // relapse
                if (!fromsessionentity.Program.Relapse.IsLoaded) fromsessionentity.Program.Relapse.Load();
                if (!tosessionentity.Program.Relapse.IsLoaded) tosessionentity.Program.Relapse.Load();

                List<PageContent> pageContentListInFromSession = Resolve<IPageContentRepository>().GetBySessionGUID(fromsessionentity.SessionGUID).ToList();
                foreach (Relapse rel in fromsessionentity.Program.Relapse)
                {
                    if (!rel.PageSequenceReference.IsLoaded) rel.PageSequenceReference.Load();
                    foreach (PageContent pageContentItem in pageContentListInFromSession)
                    {
                        //AfterShowExpression or BeforeShowExpression contain the relapse
                        string beforeExpression = string.IsNullOrEmpty(pageContentItem.BeforeShowExpression) ? "" : pageContentItem.BeforeShowExpression.ToUpper();
                        string afterExpression = string.IsNullOrEmpty(pageContentItem.AfterShowExpression) ? "" : pageContentItem.AfterShowExpression.ToUpper();
                        if (afterExpression.Contains(rel.PageSequence.PageSequenceGUID.ToString().ToUpper()) || beforeExpression.Contains(rel.PageSequence.PageSequenceGUID.ToString().ToUpper()))
                        {
                            Relapse clonedRelapse = Resolve<IRelapseService>().CloneRelapse(rel, cloneParameterModel); //CloneRelapse(rel);
                            clonedRelapse = Resolve<IRelapseService>().SetDefaultGuidForRelapse(clonedRelapse, cloneParameterModel);//set defaultguid

                            tosessionentity.Program.Relapse.Add(clonedRelapse);
                            relpaseGUIDPairList.Add(new KeyValuePair<string, string>(rel.PageSequence.PageSequenceGUID.ToString().ToUpper(), clonedRelapse.PageSequence.PageSequenceGUID.ToString().ToUpper()));

                            if (!rel.PageSequenceReference.IsLoaded) rel.PageSequenceReference.Load();
                            pageSequenceGuidInRelapse.Add(rel.PageSequence.PageSequenceGUID);
                            break;
                        }
                    }
                }
                Resolve<IProgramRepository>().Update(tosessionentity.Program);//update relapse
            }

            int startbase = tosessionentity.SessionContent.Where(s => !s.IsDeleted.HasValue || s.IsDeleted == false).Count();
            List<SessionContent> sessioncontents = fromsessionentity.SessionContent.Where(s => !s.IsDeleted.HasValue || s.IsDeleted == false).ToList();
            foreach (SessionContent sc in sessioncontents)
            {
                tosessionentity.SessionContent.Add(CopySessionContentFromDay(sc, startbase, relpaseGUIDPairList));
            }

            Resolve<ISessionRepository>().UpdateSession(tosessionentity);

            List<ChangeTech.Entities.PageVariable> shouldAddPageVariableList = GetPageVariableUsedInSessionOrRelapseShouldCopyAndReplace(fromsessionentity, pageSequenceGuidInRelapse);

            if (!tosessionentity.Program.PageVariable.IsLoaded)
            {
                tosessionentity.Program.PageVariable.Load();
            }

            // page variable group
            if (!tosessionentity.Program.PageVariableGroup.IsLoaded) tosessionentity.Program.PageVariableGroup.Load();
            if (!fromsessionentity.Program.PageVariableGroup.IsLoaded) fromsessionentity.Program.PageVariableGroup.Load();
            foreach (PageVariableGroup fromGroup in fromsessionentity.Program.PageVariableGroup)
            {
                if (tosessionentity.Program.PageVariableGroup.Where(g => g.Name == fromGroup.Name).Count() == 0)
                {
                    PageVariableGroup clonedGroup = Resolve<IPageVariableGroupService>().CloneVariableGroup(fromGroup);
                    tosessionentity.Program.PageVariableGroup.Add(Resolve<IPageVariableGroupService>().SetDefaultGuidForPageVariableGroup(clonedGroup, cloneParameterModel));
                }

            }

            // create page variable in page or page question
            foreach (ChangeTech.Entities.PageVariable variable in shouldAddPageVariableList)
            {
                if (!variable.PageVariableGroupReference.IsLoaded) variable.PageVariableGroupReference.Load();
                if (tosessionentity.Program.PageVariable.Where(p => p.Name == variable.Name).Count() == 0)
                {
                    if (ServiceUtility.isVariableTypeGeneralByName(variable.Name))
                    {
                        variable.PageVariableType = VariableTypeEnum.General.ToString();
                    }
                    tosessionentity.Program.PageVariable.Add(new ChangeTech.Entities.PageVariable
                    {
                        Name = variable.Name,
                        PageVariableGUID = Guid.NewGuid(),
                        Description = variable.Description,
                        PageVariableType = variable.PageVariableType,
                        PageVariableGroup = variable.PageVariableGroup == null ? null : tosessionentity.Program.PageVariableGroup.Where(p => p.Name.Equals(variable.PageVariableGroup.Name)).FirstOrDefault(),
                    });
                }
            }
            // create page variabe in expresion or text
            List<ChangeTech.Entities.PageVariable> variablelistusedinexpresion = GetVariableListUsedInExpresion(fromsessionentity.SessionGUID, fromsessionentity.Program.ProgramGUID, pageSequenceGuidInRelapse);
            foreach (ChangeTech.Entities.PageVariable variable in variablelistusedinexpresion)
            {
                if (!variable.PageVariableGroupReference.IsLoaded) variable.PageVariableGroupReference.Load();
                if (tosessionentity.Program.PageVariable.Where(p => p.Name == variable.Name).Count() == 0)
                {
                    if (ServiceUtility.isVariableTypeGeneralByName(variable.Name))
                    {
                        variable.PageVariableType = VariableTypeEnum.General.ToString();
                    }
                    tosessionentity.Program.PageVariable.Add(new ChangeTech.Entities.PageVariable
                    {
                        Name = variable.Name,
                        PageVariableGUID = Guid.NewGuid(),
                        Description = variable.Description,
                        PageVariableType = variable.PageVariableType,
                        PageVariableGroup = variable.PageVariableGroup == null ? null : tosessionentity.Program.PageVariableGroup.Where(p => p.Name.Equals(variable.PageVariableGroup.Name)).FirstOrDefault(),
                    });
                }
            }

            Resolve<IProgramRepository>().Update(tosessionentity.Program);
            // update page variable in page or page question
            foreach (ChangeTech.Entities.PageVariable variable in shouldAddPageVariableList)
            {
                Resolve<IStoreProcedure>().UpdatePageVariableAfterCopyProgram(fromsessionentity.Program.ProgramGUID, tosessionentity.Program.ProgramGUID, variable.Name, variable.PageVariableType);
            }
        }

        private List<ChangeTech.Entities.PageVariable> GetVariableListUsedInExpresion(Guid sessionguid, Guid prograguid, List<Guid> pageSequenceGuidListInRelapse)
        {
            List<ChangeTech.Entities.PageVariable> variablelist = new List<ChangeTech.Entities.PageVariable>();
            List<PageContent> contentlist = Resolve<IPageContentRepository>().GetBySessionGUID(sessionguid).ToList();
            contentlist.AddRange(Resolve<IPageContentRepository>().GetPageContentInPageSequenceGUIDList(pageSequenceGuidListInRelapse).ToList());

            Program programentity = Resolve<IProgramRepository>().GetProgramByGuid(prograguid);
            if (!programentity.PageVariable.IsLoaded)
            {
                programentity.PageVariable.Load();
            }
            foreach (ChangeTech.Entities.PageVariable variable in programentity.PageVariable)
            {
                string keyword = "{V:" + variable.Name + "}";
                if (contentlist.Where(p => p.AfterShowExpression != null && p.AfterShowExpression.Contains(keyword)
                    || p.BeforeShowExpression != null && p.BeforeShowExpression.Contains(keyword)
                    || p.Heading != null && p.Heading.Contains(keyword)
                    || p.Body != null && p.Body.Contains(keyword)
                    || p.FooterText != null && p.FooterText.Contains(keyword)).Count() > 0)
                {
                    variablelist.Add(variable);
                }
            }

            return variablelist;
        }



        private List<ChangeTech.Entities.PageVariable> GetPageVariableUsedInSessionOrRelapseShouldCopyAndReplace(Session fromsessionentity, List<Guid> fromRelapsePageSequenceGuid)
        {
            if (!fromsessionentity.ProgramReference.IsLoaded)
            {
                fromsessionentity.ProgramReference.Load();
            }
            if (!fromsessionentity.Program.PageVariable.IsLoaded)
            {
                fromsessionentity.Program.PageVariable.Load();
            }

            string variableType_Program = VariableTypeEnum.Program.ToString();
            IQueryable<ChangeTech.Entities.PageVariable> variables = Resolve<IPageVaribleRepository>().GetPageVariableByProgram(fromsessionentity.Program.ProgramGUID).Where(pv => pv.PageVariableType == variableType_Program);
            if (fromRelapsePageSequenceGuid != null && fromRelapsePageSequenceGuid.Count > 0)//need add the variables in relapse related pages
            {
                variables = variables.Where(p => p.Page.Where(page => page.PageSequence.SessionContent.Where(sc => sc.Session.SessionGUID == fromsessionentity.SessionGUID).Count() > 0 || fromRelapsePageSequenceGuid.Contains(page.PageSequence.PageSequenceGUID)).Count() > 0 || p.PageQuestion.Where(pq => pq.Page.PageSequence.SessionContent.Where(sc => sc.Session.SessionGUID == fromsessionentity.SessionGUID).Count() > 0 || fromRelapsePageSequenceGuid.Contains(pq.Page.PageSequence.PageSequenceGUID)).Count() > 0);
            }
            else
            {
                variables = variables.Where(p => p.Page.Where(page => page.PageSequence.SessionContent.Where(sc => sc.Session.SessionGUID == fromsessionentity.SessionGUID).Count() > 0).Count() > 0 || p.PageQuestion.Where(pq => pq.Page.PageSequence.SessionContent.Where(sc => sc.Session.SessionGUID == fromsessionentity.SessionGUID).Count() > 0).Count() > 0);
            }
            List<ChangeTech.Entities.PageVariable> variablelist = variables.OrderBy(p => p.Name).ToList();

            return variablelist;
        }

        public Session CloneSession(Session session, List<KeyValuePair<string, string>> cloneRelapseGUIDList, CloneProgramParameterModel cloneParameterModel)
        {
            try
            {
                Session cloneSession = new Session();
                cloneSession.SessionGUID = Guid.NewGuid();
                cloneSession.Name = string.Format("Copy of {0} on {1}", session.Name, DateTime.UtcNow.ToString());
                cloneSession.Day = session.Day;
                cloneSession.Description = session.Description;
                cloneSession.ParentSessionGUID = session.SessionGUID;
                cloneSession.DefaultGUID = session.DefaultGUID;
                cloneSession.IsDeleted = session.IsDeleted;
                cloneSession.IsNeedHelp = session.IsNeedHelp;
                cloneSession.IsNeedReport = session.IsNeedReport;

                if (!session.SessionContent.IsLoaded)
                {
                    session.SessionContent.Load();
                }

                List<SessionContent> sessionContents = session.SessionContent.Where(s => s.IsDeleted != true).ToList();
                // should update old page Guid to new page Guid in pageContent
                Dictionary<Guid, Guid> pageDictionary = new Dictionary<Guid, Guid>();
                foreach (SessionContent sessionContent in sessionContents)
                {
                    if (!sessionContent.PageSequenceReference.IsLoaded)
                    {
                        sessionContent.PageSequenceReference.Load();
                    }
                    SessionContent newSessionContent = Resolve<ISessionContentService>().CloneSessionContent(sessionContent, cloneRelapseGUIDList, pageDictionary, cloneParameterModel); // CloneSessionContent(sessionContent, cloneRelapseGUIDList, pageDictionary);
                    newSessionContent = Resolve<ISessionContentService>().SetDefaultGuidForSessionContent(newSessionContent, cloneParameterModel);

                    cloneSession.SessionContent.Add(newSessionContent);
                }
                Resolve<IPageService>().UpdatePageContentForCloneSessionContent(cloneSession, pageDictionary);
                return cloneSession;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public Session SetDefaultGuidForSession(Session needSetDefaultEntity, CloneProgramParameterModel cloneParameterModel)
        {
            Session newEntity = new Session();

            newEntity = needSetDefaultEntity;
            switch (cloneParameterModel.source)
            {
                case DefaultGuidSourceEnum.FromDefaultGuid:
                    //newEntity.DefaultGUID = needSetDefaultEntity.DefaultGUID;//This is default value, don't need assign again.
                    break;
                case DefaultGuidSourceEnum.FromMatchDefaultGuidFunction:
                    bool isMatchDefaultGuidSuccessful = false;//whether default guid can be matched, if not ,set null.
                    Guid fromSessionGuid = newEntity.ParentSessionGUID == null ? Guid.Empty : (Guid)newEntity.ParentSessionGUID;
                    if (fromSessionGuid != Guid.Empty)//Has parent guid. 
                    {
                        Session fromSessionEntity = Resolve<ISessionRepository>().GetSessionBySessionGuid(fromSessionGuid);
                        if (fromSessionEntity != null)
                        {
                            int? dayNum = fromSessionEntity.Day;//if dayNum == null, can't map.
                            if (!fromSessionEntity.ProgramReference.IsLoaded) fromSessionEntity.ProgramReference.Load();
                            Program fromProgramInDefaultLanguage = new Program();
                            if (fromSessionEntity.Program.DefaultGUID.HasValue)//This fromProgram is not default program
                            {
                                fromProgramInDefaultLanguage = Resolve<IProgramRepository>().GetProgramByGuid((Guid)fromSessionEntity.Program.DefaultGUID);
                            }
                            else//This fromProgram is default program. Or this is wrong data for old Data Version or some other reason.
                            {
                                fromProgramInDefaultLanguage = Resolve<IProgramRepository>().GetProgramByGuid(fromSessionEntity.Program.ProgramGUID);
                            }
                            if (fromProgramInDefaultLanguage != null)
                            {
                                Program toProgramInDefaultLanguage = Resolve<IProgramRepository>().GetProgramByGuid(cloneParameterModel.ProgramGuidOfCopiedToProgramsInDefaultLanguage);
                                if (toProgramInDefaultLanguage != null)
                                {
                                    if (toProgramInDefaultLanguage.ParentProgramGUID == fromProgramInDefaultLanguage.ProgramGUID)//Match Successful. the toProgram's parentguid == fromDefaultProgram's guid
                                    {
                                        isMatchDefaultGuidSuccessful = true;
                                    }
                                    else
                                    {
                                        List<Program> fromProgramMatchedList = Resolve<IProgramRepository>().GetProgramByDefaultGUID(fromProgramInDefaultLanguage.ProgramGUID).Where(p => p.ProgramGUID == toProgramInDefaultLanguage.ParentProgramGUID).ToList();
                                        if (fromProgramMatchedList.Count > 0)//Match Successful. the toProgram's parent guid is fromProgram's guid which program belongs to the fromDefaultProgram but not the default language.
                                        {
                                            isMatchDefaultGuidSuccessful = true;
                                        }
                                    }
                                }

                                //Set Default Guid if match successful
                                if (isMatchDefaultGuidSuccessful)
                                {
                                    try
                                    {
                                        Session toDefaultSession = Resolve<ISessionRepository>().GetSessionByProgramGuid(toProgramInDefaultLanguage.ProgramGUID).Where(s => s.Day == dayNum).FirstOrDefault();
                                        newEntity.DefaultGUID = toDefaultSession.SessionGUID;
                                    }
                                    catch (Exception ex)
                                    {
                                        Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                                        isMatchDefaultGuidSuccessful = false;
                                    }
                                }
                            }
                        }
                    }
                    //else//don't has parent guid ,so can't match,set the default guid =>null
                    //{
                    //    newEntity.DefaultGUID = null;
                    //}

                    //Can't match. Set default guid =>null.
                    if (!isMatchDefaultGuidSuccessful)
                    {
                        newEntity.DefaultGUID = null;
                    }
                    break;
                case DefaultGuidSourceEnum.FromNull:
                    newEntity.DefaultGUID = null;
                    break;
                case DefaultGuidSourceEnum.FromPrimaryKey:
                    newEntity.DefaultGUID = needSetDefaultEntity.ParentSessionGUID;
                    break;
                default:
                    throw new Exception("Error: Dont't find the DefaultGuidSource for Session Entity.");
                //break;
            }

            return newEntity;
        }
        #endregion

        #region private methods
        /// <summary>
        /// Add a new parameter (List<KeyValuePair<string, string>>).
        /// When from and to are in the same program,we don't need assign this List.
        /// When not in the same program, need clone Relapse first and then assign this new parameter(List).
        /// </summary>
        /// <param name="sc"></param>
        /// <param name="startbase"></param>
        /// <param name="cloneRelapseGUIDList">Used to update RelapseGuid when no in the same program.</param>
        /// <returns></returns>
        private SessionContent CopySessionContentFromDay(SessionContent sc, int startbase, List<KeyValuePair<string, string>> cloneRelapseGUIDList)
        {
            SessionContent newsessioncontent = new SessionContent();
            newsessioncontent.PageSequenceOrderNo = sc.PageSequenceOrderNo + startbase;
            newsessioncontent.SessionContentGUID = Guid.NewGuid();
            if (!sc.PageSequenceReference.IsLoaded)
            {
                sc.PageSequenceReference.Load();
            }
            //newsessioncontent.PageSequence = ServiceUtility.ClonePageSequenceNotIncludeParentGuid(sc.PageSequence, new List<KeyValuePair<string, string>>());//previous is :ClonePageSequence
            CloneProgramParameterModel cloneParameterModel = new CloneProgramParameterModel
            {
                ProgramGuidOfCopiedToProgramsInDefaultLanguage = Guid.Empty,
                source = DefaultGuidSourceEnum.FromNull,
            };
            newsessioncontent.PageSequence = Resolve<IPageSequenceService>().ClonePageSequence(sc.PageSequence, cloneRelapseGUIDList, cloneParameterModel);
            newsessioncontent.PageSequence = Resolve<IPageSequenceService>().SetDefaultGuidForPageSequence(newsessioncontent.PageSequence, cloneParameterModel);

            // don't copy program room

            return newsessioncontent;
        }

        private void AdjustSessionForDelete(Session session)
        {
            if (!session.ProgramReference.IsLoaded)
            {
                session.ProgramReference.Load();
            }
            if (!session.Program.Session.IsLoaded)
            {
                session.Program.Session.Load();
            }
            foreach (Session adjustSession in session.Program.Session)
            {
                if (adjustSession.Day > session.Day)
                {
                    adjustSession.Day--;
                }
            }

            Resolve<IProgramRepository>().Update(session.Program);
        }

        #endregion

        // for Win8Service.
        public List<SessionInfoModel> GetSessionInfoModelsByProgramGuid(Guid programGuid,ProgramUser pu)
        {
            List<SessionInfoModel> sessionInfoModels = new List<SessionInfoModel>();
            List<Session> sessionEntities = Resolve<ISessionRepository>().GetSessionByProgramGuid(programGuid).ToList<Session>();
            foreach (Session sessionEntity in sessionEntities)
            {
                ProgramDailySMS dailySMS = Resolve<IShortMessageRepository>().GetProgramDailySMSBySessionGuid(sessionEntity.SessionGUID);
                SessionInfoModel sessionInfoModel = new SessionInfoModel
                {
                    SessionGuid = sessionEntity.SessionGUID,
                    SessionName = sessionEntity.Name,
                    SessionDay = sessionEntity.Day.HasValue ? sessionEntity.Day.Value : 0,
                    SessionDescription = sessionEntity.Description,
                    NotificationText = dailySMS == null ? "" : dailySMS.SMSContent,
                    Resources = Resolve<IResourceService>().GetResourcesBySessionGuid(sessionEntity.SessionGUID)
                };
                if (pu != null)
                {
                    Dictionary<string, bool> sessionDic = GetRunSessionUrl(pu, sessionEntity.Day.Value);
                    foreach (var item in sessionDic)
                    {
                        sessionInfoModel.RunSessionUrl = item.Key;
                        sessionInfoModel.IsTaken = item.Value;
                        if (item.Value == true)
                        {
                            sessionInfoModel.HasAvailableSession = SessionStatusEnum.DONE;
                        }
                        else
                        {
                            sessionInfoModel.HasAvailableSession = SessionStatusEnum.NOTDONE;
                        }
                        if (sessionEntity.Day.Value==pu.Day.Value+1)
                        {
                            sessionInfoModel.HasAvailableSession = SessionStatusEnum.STARTTODO;
                        }
                    }
                    sessionInfoModel.NotificationDate = Resolve<IProgramUserService>().ExpectSessionDate(programGuid, pu.User.UserGUID, sessionEntity.Day.Value);
                }
                else
                {
                    sessionInfoModel.RunSessionUrl = string.Empty;
                    sessionInfoModel.IsTaken = false;
                    sessionInfoModel.HasAvailableSession = SessionStatusEnum.NOTDONE;
                    sessionInfoModel.NotificationDate = null;
                }

                if (!sessionInfoModels.Contains(sessionInfoModel))
                {
                    sessionInfoModels.Add(sessionInfoModel);
                }
            }
            return sessionInfoModels;
        }

        public Dictionary<string,bool> GetRunSessionUrl(ProgramUser pu,int currentDay)
        {
            string runSessionUrl = string.Empty;
            string serverPath = string.Empty;
            Dictionary<string, bool> runSessionDic = new Dictionary<string, bool>();
            if (!pu.ProgramReference.IsLoaded) pu.ProgramReference.Load();
            if (!pu.UserReference.IsLoaded) pu.UserReference.Load();
            ProgramPropertyModel proPovertyModel = Resolve<IProgramService>().GetProgramProperty(pu.Program.ProgramGUID);
            if (proPovertyModel.IsSupportHttps)
            {
                serverPath = Resolve<ISystemSettingService>().GetSettingValue("WebServerPathHttps");
            }
            else
            {
                serverPath = Resolve<ISystemSettingService>().GetSettingValue("WebServerPath");
            }

            string programCode = proPovertyModel.ProgramCode;
            Session sessionEntity = Resolve<ISessionRepository>().GetSessionByProgramGuidAndDay(pu.Program.ProgramGUID,currentDay);
            if (sessionEntity != null)
            {
                ProgramUserSession puSessionEntity = Resolve<IProgramUserSessionRepository>().GetProgramUserSessionByProgramUserGuidAndSessionGuid(pu.ProgramUserGUID, sessionEntity.SessionGUID);
                if (puSessionEntity != null)
                {
                    string securityStr = StringUtility.MD5Encrypt(string.Format("{0};{1};{2};{3}", pu.User.Email, pu.User.Password, UserTaskTypeEnum.RetakeSession.ToString(), sessionEntity.Day.Value), MD5_KEY);
                    runSessionUrl = string.Format("{0}ChangeTech5.html?P={1}&Mode=Live&Security={2}", serverPath, programCode, securityStr);
                    runSessionDic.Add(runSessionUrl, true);
                }
                else
                {
                    string securityStr = StringUtility.MD5Encrypt(string.Format("{0};{1};{2};{3}", pu.User.Email, pu.User.Password, UserTaskTypeEnum.TakeSession.ToString(), sessionEntity.Day.Value), MD5_KEY);
                    runSessionUrl = string.Format("{0}ChangeTech5.html?P={1}&Mode=Live&Security={2}", serverPath, programCode, securityStr);
                    runSessionDic.Add(runSessionUrl, false);
                }
            }

            return runSessionDic;
        }
    }
}
