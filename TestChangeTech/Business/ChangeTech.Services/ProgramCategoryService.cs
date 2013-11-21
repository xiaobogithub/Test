using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;
using ChangeTech.Models;
using ChangeTech.IDataRepository;
using ChangeTech.Entities;
using Ethos.Utility;

namespace ChangeTech.Services
{
    public class ProgramCategoryService : ServiceBase, IProgramCategoryService
    {
        public const string MD5_KEY = "psycholo";

        public List<ProgramCategoryModel> GetProgramCategories(string windowsLiveId, string applicationId)
        {
            List<ProgramCategoryModel> programCategoryModels = new List<ProgramCategoryModel>();
            List<ProgramCategory>programCategoryEntities = Resolve<IProgramCategoryRepository>().GetProgramCategories().ToList();
            foreach (ProgramCategory programCategoryEntity in programCategoryEntities)
            {
                ProgramCategoryModel programCategoryModel = new ProgramCategoryModel
                {
                    CategoryGuid = programCategoryEntity.ProgramCategoryGUID,
                    CategoryName = programCategoryEntity.Name
                };
                programCategoryModel.Programs = GetProgramsByCategoryGuid(windowsLiveId,programCategoryEntity.ProgramCategoryGUID);
                if (!programCategoryModels.Contains(programCategoryModel))
                {
                    programCategoryModels.Add(programCategoryModel);
                }
            }

            return programCategoryModels;
        }

        public List<ProgramInfoModel> GetProgramsByCategoryGuid(string windowsLiveId, Guid programCategoryGuid)
        {
            List<ProgramInfoModel> programsModel = new List<ProgramInfoModel>();
            List<ProgramCategoryProgram> programCategoryProgramEntities =Resolve<IProgramCategoryRepository>().GetProgramsByCategoryGuid(programCategoryGuid).Where(pcm => (!pcm.IsDeleted.HasValue || pcm.IsDeleted.HasValue && pcm.IsDeleted.Value == false)).ToList();
            foreach (ProgramCategoryProgram pcpEntity in programCategoryProgramEntities)
            {
                if (!pcpEntity.ProgramReference.IsLoaded) pcpEntity.ProgramReference.Load();
                Program programEntity = pcpEntity.Program;
                if (programEntity != null)
                {
                    ProgramInfoModel programInfoModel = new ProgramInfoModel
                    {
                        ProgramGuid = programEntity.ProgramGUID,
                        ProgramName = programEntity.Name,
                        ProgramDescription = programEntity.Description,
                        Price = programEntity.Price.HasValue ? programEntity.Price.Value : 0,
                        ProgramImage = programEntity.ProgramImageUrl,
                        OfferToken = programEntity.OfferToken,
                        ProgramPrimaryColor = ChangeColorToRGBFormat(programEntity.ProgramPrimaryColor),
                        ProgramSecondaryColor = ChangeColorToRGBFormat(programEntity.ProgramSecondaryColor),
                        SubTitle = programEntity.ShortName,
                        ProductImage = programEntity.ProductImage,
                        ProductImageLarge = programEntity.ProductImageLarge,
                        ProductImageSmall = programEntity.ProductImageSmall,
                        ProductImagePresenter = programEntity.ProductImagePresenter,
                        ProductInstructorImage = programEntity.ProductInstructorImage,
                        ProgramFunction = programEntity.ProgramFunction,
                        ProgramPurpose = programEntity.ProgramPurpose,
                        NumberOfSessions=Resolve<ISessionService>().GetSessionsByProgramGuid(programEntity.ProgramGUID).Count()
                    };

                    //Ctpp ProgramPresenterSmallImageUrl
                    programInfoModel.ProgramPresenterImage = GetProgramPresenterImageUrl(programEntity.ProgramGUID);
                    CTPPModel ctppModel = Resolve<ICTPPService>().GetCTPP(programEntity.ProgramGUID);
                    if (ctppModel != null)
                    {
                        programInfoModel.ProgramPresenterSmallImageUrl = Resolve<ICTPPRepository>().GetCTPPByProgramGuid(programEntity.ProgramGUID).ProgramPresenterSmallImageUrl;
                    }

                    Win8ProgramUser win8ProgramUserEntity = Resolve<IWin8ProgramUserRepository>().GetWindowsLiveByWindowsLiveId(windowsLiveId).Where(wl => wl.ProgramUser.Program.ProgramGUID == programEntity.ProgramGUID).FirstOrDefault();
                    if (win8ProgramUserEntity != null)
                    {
                        if (!win8ProgramUserEntity.ProgramUserReference.IsLoaded) win8ProgramUserEntity.ProgramUserReference.Load();
                        if (!win8ProgramUserEntity.ProgramUser.UserReference.IsLoaded) win8ProgramUserEntity.ProgramUser.UserReference.Load();
                        programInfoModel.ReportUrl = Resolve<IProgramService>().GetReportButtonLinkAddress(win8ProgramUserEntity.ProgramUser);
                        programInfoModel.HelpUrl = Resolve<IProgramService>().GetHelpButtonLinkAddress(win8ProgramUserEntity.ProgramUser);
                        programInfoModel.IsPaid = win8ProgramUserEntity.ProgramUser.User.IsPaid.HasValue ? win8ProgramUserEntity.ProgramUser.User.IsPaid.Value : false;

                        Session sessionEntity = Resolve<ISessionRepository>().GetSessionByProgramGuidAndDay(programEntity.ProgramGUID, 0);
                        if (sessionEntity != null)
                        {
                            ProgramUserSession puSession = sessionEntity.ProgramUserSession.Where(pus => pus.ProgramUserGUID == win8ProgramUserEntity.ProgramUser.ProgramUserGUID && (!pus.IsDeleted.HasValue || pus.IsDeleted.HasValue && pus.IsDeleted.Value == false)).FirstOrDefault();
                            //ProgramUserSession puSession = Resolve<IProgramUserSessionRepository>().GetProgramUserSessionByProgramUserGuidAndSessionGuid(win8ProgramUserEntity.ProgramUser.ProgramUserGUID, sessionEntity.SessionGUID);
                            DateTime? currentSessionDate = null;
                            if (puSession == null)
                            {
                                currentSessionDate = Resolve<IProgramUserService>().ExpectSessionDate(programEntity.ProgramGUID, win8ProgramUserEntity.ProgramUser.User.UserGUID, 0);
                            }
                            else
                            {
                                currentSessionDate = Resolve<IProgramUserService>().ExpectSessionDate(programEntity.ProgramGUID, win8ProgramUserEntity.ProgramUser.User.UserGUID, win8ProgramUserEntity.ProgramUser.Day.Value + 1);
                            }
                            if (currentSessionDate.HasValue)
                            {
                                programInfoModel.CurrentSessionDate = currentSessionDate.Value.Date;
                            }
                            else
                            {
                                programInfoModel.CurrentSessionDate = null;
                            }
                        }

                        //CurrentSessionNumber,CurrentSessionUrl
                        Dictionary<int, string> currentSessionInfo = GetCurrentSessionUrl(win8ProgramUserEntity, programEntity.ProgramGUID);
                        foreach (KeyValuePair<int, string> currentSession in currentSessionInfo)
                        {
                            programInfoModel.CurrentSessionNumber = currentSession.Key;
                            programInfoModel.CurrentSessionUrl = currentSession.Value;
                        }

                        //IsTakenCurrentSession
                        DateTime currentDateByTimeZone = Resolve<IProgramUserService>().GetCurrentTimeByTimeZone(programEntity.ProgramGUID, win8ProgramUserEntity.ProgramUser.User.UserGUID, DateTime.UtcNow);
                        if (programInfoModel.CurrentSessionDate <= currentDateByTimeZone.Date && programInfoModel.CurrentSessionUrl!=string.Empty)//|| programInfoModel.CurrentSessionDate==null)
                        {
                            programInfoModel.IsTakenCurrentSession = false;
                        }
                        else
                        {
                            programInfoModel.IsTakenCurrentSession = true;
                        }
                       
                        //IsCompleted
                        int numberOfSessions = Resolve<ISessionService>().GetLastSessionDay(programEntity.ProgramGUID);
                        if (programInfoModel.CurrentSessionNumber >= numberOfSessions && programInfoModel.IsTakenCurrentSession)
                        {
                            programInfoModel.IsCompleted = true;
                        }
                        else
                        {
                            programInfoModel.IsCompleted = false;
                        }
                    }
                    else
                    {
                        programInfoModel.IsTakenCurrentSession = false;
                        programInfoModel.IsCompleted = false;
                        programInfoModel.IsPaid = false;
                        programInfoModel.CurrentSessionDate = null;//CurrentSessionNumber,CurrentSessionUrl
                        Dictionary<int, string> currentSessionInfo = GetCurrentSessionUrl(win8ProgramUserEntity, programEntity.ProgramGUID);
                        foreach (KeyValuePair<int, string> currentSession in currentSessionInfo)
                        {
                            programInfoModel.CurrentSessionNumber = currentSession.Key;
                            programInfoModel.CurrentSessionUrl = currentSession.Value;
                        }
                    }

                    programsModel.Add(programInfoModel);
                }
            }

            return programsModel;
        }

        public Dictionary<int, string> GetCurrentSessionUrl(Win8ProgramUser win8ProgramUserEntity, Guid programGuid)
        {
            //string securityStr = StringUtility.MD5Encrypt(string.Format("{0};{1}", userEntity.Email, userEntity.Password), MD5_KEY);
            //https://program.changetech.no/ChangeTech5.html?Mode=Trial&P=8H664J
            string serverPath = string.Empty;
            string currentSessionUrl = string.Empty;
            Dictionary<int, string> currentSessionInfo = new Dictionary<int, string>();
            ProgramPropertyModel proPovertyModel = Resolve<IProgramService>().GetProgramProperty(programGuid);
            string programCode = proPovertyModel.ProgramCode;
            if (proPovertyModel.IsSupportHttps)
            {
                serverPath = Resolve<ISystemSettingService>().GetSettingValue("WebServerPathHttps");
            }
            else
            {
                serverPath = Resolve<ISystemSettingService>().GetSettingValue("WebServerPath");
            }

            if (win8ProgramUserEntity != null)
            {
                if (!win8ProgramUserEntity.ProgramUserReference.IsLoaded) win8ProgramUserEntity.ProgramUserReference.Load();
                if (!win8ProgramUserEntity.ProgramUser.UserReference.IsLoaded) win8ProgramUserEntity.ProgramUser.UserReference.Load();
                 if (!win8ProgramUserEntity.ProgramUser.ProgramReference.IsLoaded) win8ProgramUserEntity.ProgramUser.ProgramReference.Load();
                Session sessionEntity = Resolve<ISessionRepository>().GetSessionByProgramGuidAndDay(programGuid, 0);
                if (sessionEntity != null)
                {
                    ProgramUserSession puSessionEntity = Resolve<IProgramUserSessionRepository>().GetProgramUserSessionByProgramUserGuidAndSessionGuid(win8ProgramUserEntity.ProgramUser.ProgramUserGUID, sessionEntity.SessionGUID);
                    if (puSessionEntity == null)
                    {
                        string securityStr = StringUtility.MD5Encrypt(string.Format("{0};{1};{2};{3}", win8ProgramUserEntity.ProgramUser.User.Email, win8ProgramUserEntity.ProgramUser.User.Password, UserTaskTypeEnum.TakeSession.ToString(), 0), MD5_KEY);
                        currentSessionUrl = string.Format("{0}ChangeTech5.html?P={1}&Mode=Live&Security={2}", serverPath, programCode, securityStr);
                        currentSessionUrl = currentSessionUrl.Replace("#", "");
                        currentSessionInfo.Add(win8ProgramUserEntity.ProgramUser.Day.Value, currentSessionUrl);
                    }
                    else
                    {
                        int lastSessionNumByProgram = Resolve<ISessionRepository>().GetLastSessionOfProgram(win8ProgramUserEntity.ProgramUser.Program.ProgramGUID).Day.Value;
                        if (win8ProgramUserEntity.ProgramUser.Day.Value <lastSessionNumByProgram)
                        {
                            string securityStr = StringUtility.MD5Encrypt(string.Format("{0};{1};{2};{3}", win8ProgramUserEntity.ProgramUser.User.Email, win8ProgramUserEntity.ProgramUser.User.Password, UserTaskTypeEnum.TakeSession.ToString(), win8ProgramUserEntity.ProgramUser.Day.Value + 1), MD5_KEY);
                            currentSessionUrl = string.Format("{0}ChangeTech5.html?P={1}&Mode=Live&Security={2}", serverPath, programCode, securityStr);
                        }
                        currentSessionUrl = currentSessionUrl.Replace("#", "");
                        currentSessionInfo.Add(win8ProgramUserEntity.ProgramUser.Day.Value, currentSessionUrl);
                    }
                }
            }
            else
            {
                currentSessionUrl = string.Format("{0}ChangeTech5.html?Mode=Trial&P={1}", serverPath, programCode);
                currentSessionUrl = currentSessionUrl.Replace("#", "");
                currentSessionInfo.Add(0, currentSessionUrl);
            }
             
             return currentSessionInfo;
        }

        private string GetProgramPresenterImageUrl(Guid programGuid)
        {
            string serverPath = string.Empty;
            string programPresentImageUrl = string.Empty;
            CTPPModel ctppModel = Resolve<ICTPPService>().GetCTPP(programGuid);
            ProgramPropertyModel proPovertyModel = Resolve<IProgramService>().GetProgramProperty(programGuid);
            if (proPovertyModel.IsSupportHttps)
            {
                serverPath = Resolve<ISystemSettingService>().GetSettingValue("WebServerPathHttps");
            }
            else
            {
                serverPath = Resolve<ISystemSettingService>().GetSettingValue("WebServerPath");
            }
            if (ctppModel != null && ctppModel.ProgramPresenter != null)
            {
                programPresentImageUrl = serverPath + "RequestResource.aspx?target=Image&media=" + ctppModel.ProgramPresenter.NameOnServer;
            }

            return programPresentImageUrl;
        }

        private string ChangeColorToRGBFormat(string colorStr)
        {
            string rgbColorFormat = string.Empty;
            if (!string.IsNullOrEmpty(colorStr)&&colorStr.Contains("0x"))
            {
                rgbColorFormat = "#" + colorStr.Substring(2);
            }

            return rgbColorFormat;
        }
    }
}
