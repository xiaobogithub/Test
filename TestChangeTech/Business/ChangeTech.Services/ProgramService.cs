using ChangeTech.Contracts;
using ChangeTech.Models;
using Ethos.DependencyInjection;
using ChangeTech.IDataRepository;
using ChangeTech.Entities;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Data.Objects.DataClasses;
using Ethos.Utility;
using System.Xml;
using Microsoft.WindowsAzure.StorageClient;
using System.Text;
using System.Web;

namespace ChangeTech.Services
{
    public class ProgramService : ServiceBase, IProgramService
    {
        private const string PROGRAM_NAME = "Name";
        private const string PROGRAM_DATE = "Date";
        private const string PROGRAM_USERS = "Users";
        private string versionNumber
        {
            get
            {
                string version = System.Configuration.ConfigurationManager.AppSettings["ProjectVersionWithoutDot"];
                return string.IsNullOrEmpty(version) ? "" : version.ToLower();
            }
        }

        public const string MD5_KEY = "psycholo";
        public const string STATUSQUEUEMAKT = "s";//"status" need be changed to only 1 char, to avoid the queuename beyond 63
        private const string PROGRAM_STATUS_PUBLISH = "42ED0A49-1315-4D22-A1EE-28B6CFF9AE74";
        private const string PROGRAM_STATUS_UNDER_DEVELOPMENT = "BCB92CEF-FD49-4818-BBB2-8DFC96FF0FE1";
        #region IProgramService Members
        public ProgramsModel GetProgramsModel(int pageNumber, int pageSize)
        {
            ProgramsModel programs = null;
            IQueryable<Program> programsEntity = Resolve<IProgramRepository>().GetAllPrograms().Where(p => !p.DefaultGUID.HasValue);
            programsEntity = programsEntity.OrderBy(p => p.Name).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            programs = ConvertProgramsToProgramsModel(programsEntity.ToList());

            return programs;
        }

        public List<ProgramModel> GetSortProgramsModel(int pageNumber, int pageSize, string sortByContent)
        {
            List<ProgramModel> programsModel = new List<ProgramModel>();
            IQueryable<Program> programsEntity = Resolve<IProgramRepository>().GetAllPrograms().Where(p => !p.DefaultGUID.HasValue);
            switch (sortByContent)
            {
                case PROGRAM_NAME://Program Name
                    programsEntity = programsEntity.OrderBy(p => p.NameByDeveloper).Skip((pageNumber - 1) * pageSize).Take(pageSize);
                    programsModel = ConvertProgramListToProgramsModel(programsEntity.ToList());
                    break;
                case PROGRAM_DATE://Program Created
                    programsEntity = programsEntity.OrderByDescending(p => p.Created).Skip((pageNumber - 1) * pageSize).Take(pageSize);
                    programsModel = ConvertProgramListToProgramsModel(programsEntity.ToList());
                    break;
                case PROGRAM_USERS://user's number
                    programsModel = ConvertProgramListToProgramsModel(programsEntity.ToList());
                    programsModel = programsModel.OrderByDescending(p => p.NumberOfUsers).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                    break;
            }

            return programsModel;
        }

        public List<ProgramModel> GetAllProgramsModel()
        {
            List<ProgramModel> programsModel = new List<ProgramModel>();
            IQueryable<Program> programsEntity = Resolve<IProgramRepository>().GetAllPrograms().Where(p => !p.DefaultGUID.HasValue);
            if (programsEntity.Count() > 0)
            {
                programsModel = ConvertProgramListToProgramsModel(programsEntity.ToList());
            }

            return programsModel;
        }

        private List<ProgramModel> ConvertProgramsToProgramsModelSortByContent(List<Program> list)
        {
            List<ProgramModel> listProgramModel = new List<ProgramModel>();
            List<Program> porgramList = list;

            foreach (Program program in porgramList)
            {
                ProgramModel programModel = new ProgramModel();
                programModel.Description = program.Description;
                programModel.ProgramName = program.Name;
                programModel.Guid = program.ProgramGUID;
                programModel.LastUpdated = program.LastUpdated.HasValue ? program.LastUpdated.Value : DateTime.UtcNow;
                programModel.NumberOfUsers = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuid(program.ProgramGUID).Count();
                // for project manager
                if (!program.UserReference.IsLoaded)
                {
                    program.UserReference.Load();
                }
                if (program.User != null)
                {
                    programModel.ProjectManager = program.User.Email;
                }
                else
                {
                    programModel.ProjectManager = "Assign";
                }
                //In this ProgramModel have many property have not voluation.

                listProgramModel.Add(programModel);
            }

            return listProgramModel;
        }
        public ProgramsModel GetProgramsModel()
        {
            ProgramsModel programs = null;
            List<Program> allPrograms = Resolve<IProgramRepository>().GetAllPrograms().Where(p => !p.DefaultGUID.HasValue).ToList();

            //allPrograms = Resolve<IProgramRepository>().GetAllPrograms();

            programs = ConvertProgramsToProgramsModel(allPrograms);
            // TODO: Set status of program and number of  active user after database design finished

            //foreach (ProgramModel program in programs)
            //{
            //    if (program.LastUpdateBy != null)
            //    {
            //        program.LastUpdateBy.UserName = Resolve<IUserService>().GetUserByUserGuid(program.LastUpdateBy.UserGuid).UserName;
            //    }
            //    else
            //    {
            //        program.LastUpdateBy = new UserModel();
            //    }
            //}

            return programs;
        }

        public int GetNumberOfProgram()
        {
            return Resolve<IProgramRepository>().GetAllPrograms().Where(p => !p.DefaultGUID.HasValue).Count();
        }

        public EditProgramModel GetEditProgramModelByGuid(Guid programGuid, int currentPageNumber, int pagesize)
        {
            Program programEntity = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
            EditProgramModel programModel = new EditProgramModel();
            try
            {
                programModel.Guid = programGuid;
                CTPP ctpp = Resolve<ICTPPRepository>().GetCTPPByProgramGuid(programGuid);
                if (ctpp != null)
                {
                    programModel.ProgramName = string.IsNullOrEmpty(ctpp.ProgramName) ? programEntity.Name : ctpp.ProgramName;
                }
                else
                {
                    programModel.ProgramName = programEntity.Name;
                }
                programModel.NameInDeveloper = programEntity.NameByDeveloper;
                programModel.ShortName = programEntity.ShortName;
                programModel.Description = programEntity.Description;
                programModel.GeneralColor = programEntity.GeneralColor.Substring(2);
                programModel.IsNeedPinCode = programEntity.IsNeedPinCode.HasValue ? programEntity.IsNeedPinCode.Value : false;
                if (!programEntity.LanguageReference.IsLoaded)
                {
                    programEntity.LanguageReference.Load();
                }
                programModel.DefaultLanguage = programEntity.Language.LanguageGUID;
                if (!programEntity.ProgramStatusReference.IsLoaded)
                {
                    programEntity.ProgramStatusReference.Load();
                }
                if (programEntity.ProgramStatus != null)
                {
                    programModel.ProgramStatus = programEntity.ProgramStatus.ProgramStatusGUID.ToString();
                    programModel.IsLiveProgram = IsLiveProgram(programEntity.ProgramStatus.ProgramStatusGUID);
                }
                if (!programEntity.ResourceReference.IsLoaded)
                {
                    programEntity.ResourceReference.Load();
                }
                if (programEntity.Resource != null)
                {
                    programModel.ProgramLogo = new ResourceModel
                    {
                        Extension = programEntity.Resource.FileExtension,
                        ID = programEntity.Resource.ResourceGUID,
                        Name = programEntity.Resource.Name,
                        NameOnServer = programEntity.Resource.NameOnServer,
                        Type = programEntity.Resource.Type,
                    };
                }
                programModel.ProgramStatusList = new List<KeyValuePair<Guid, string>>();
                List<ProgramStatus> statusList = Resolve<IProgramStatusRepository>().GetAllProgramStatus().ToList();
                foreach (ProgramStatus status in statusList)
                {
                    programModel.ProgramStatusList.Add(new KeyValuePair<Guid, string>(status.ProgramStatusGUID, status.Name));
                }
                programModel.Sessions = new List<SessionModel>();
                programModel.Sessions = Resolve<ISessionService>().GetSessionsByProgramGuid(programGuid, currentPageNumber, pagesize);
                if (!programEntity.ProgramLanguage.IsLoaded)
                {
                    programEntity.ProgramLanguage.Load();
                }

                //programModel.languages = new List<ProgramLanguageModel>();
                //foreach (ProgramLanguage programLanguage in programEntity.ProgramLanguage)
                //{
                //    if (!programLanguage.LanguageReference.IsLoaded)
                //    {
                //        programLanguage.LanguageReference.Load();
                //    }

                //    ProgramLanguageModel pl = new ProgramLanguageModel
                //    {
                //        language = new LanguageModel { LanguageGUID = programLanguage.Language.LanguageGUID }
                //    };
                //    programModel.languages.Add(pl);
                //}
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method Name {0}, Program GUID {1}", System.Reflection.MethodBase.GetCurrentMethod().Name, programGuid));
                throw ex;
            }
            return programModel;
        }

        public bool IsLiveProgram(Guid programStatusGuid)
        {
            bool IsLive = false;
            switch (programStatusGuid.ToString().ToUpper())
            {
                // Live Program
                case PROGRAM_STATUS_PUBLISH:
                    IsLive = true;
                    break;
                case PROGRAM_STATUS_UNDER_DEVELOPMENT:
                    IsLive = false;
                    break;
            }
            return IsLive;
        }

        public ProgramSecurityModel GetProgramSecuirtyModel(Guid programGuid)
        {
            ProgramSecurityModel psModel = new ProgramSecurityModel();
            List<ProgramUser> psList = Resolve<IProgramUserRepository>().GetProgramUserListByProgramGuid(programGuid).OrderByDescending(s => s.StartDate).ToList();
            foreach (ProgramUser ps in psList)
            {
                UserSecurityModel us = ModelUtility.ParaseProgramSecurityEntity(ps);

                //if (!ps.ProgramReference.IsLoaded)
                //{
                //    ps.ProgramReference.Load();
                //}

                //psModel.ProgramName = ps.Program.Name;
                psModel.Add(us);
            }
            psModel.ProgramName = Resolve<IProgramRepository>().GetProgramByGuid(programGuid).Name;

            return psModel;
        }

        public ProgramSecurityModel GetProgramSecuirtyModel(Guid programGuid, string email, int pageNumber, int pageSize)
        {
            ProgramSecurityModel psModel = new ProgramSecurityModel();
            IQueryable<ProgramUser> psList = Resolve<IProgramUserRepository>().GetProgramUserListByProgramGuid(programGuid);
            if (!string.IsNullOrEmpty(email))
            {
                psList = psList.Where(p => p.User.Email.Contains(email));
            }

            List<ProgramUser> userList = psList.OrderByDescending(p => p.StartDate).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            foreach (ProgramUser ps in userList)
            {
                UserSecurityModel us = ModelUtility.ParaseProgramSecurityEntity(ps);
                psModel.Add(us);
            }

            psModel.ProgramName = Resolve<IProgramRepository>().GetProgramByGuid(programGuid).Name;

            return psModel;
        }

        public UserSecurityModel GetProgramUserSecurityModel(Guid programGuid, Guid userGuid)
        {
            ProgramUser ps = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuidAndUserGuid(programGuid, userGuid);
            return ModelUtility.ParaseProgramSecurityEntity(ps);
        }

        public UserSecurityModel GetProgramUserSecureityModel(Guid programUserGuid)
        {
            ProgramUser ps = Resolve<IProgramUserRepository>().GetProgramUserByProgramUserGuid(programUserGuid);
            return ModelUtility.ParaseProgramSecurityEntity(ps);
        }

        public void CopyProgram(Guid programGuid, Guid userGuid)
        {
            #region new function include default guid
            //copy defaultProgram
            Program program = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);

            //Program insertProgram = ServiceUtility.CloneProgram(program, GenerateProgramCode());
            CloneProgramParameterModel defaultProgramModel = new CloneProgramParameterModel
            {
                ProgramGuidOfCopiedToProgramsInDefaultLanguage = Guid.Empty,
                source = DefaultGuidSourceEnum.FromNull,
            };
            Program insertProgram = CloneProgramIncludeDefaultGuid(program, GenerateProgramCode(), defaultProgramModel); //ServiceUtility.CloneProgramNotIncludeParentGuid(program, GenerateProgramCode());
            insertProgram = SetDefaultGuidForProgram(insertProgram, defaultProgramModel);

            SetProgramLanguage(program, insertProgram);

            Resolve<IProgramRepository>().InsertProgram(insertProgram);

            SetFullPermissionForProgram(insertProgram.ProgramGUID, userGuid);

            Program newProgram = Resolve<IProgramRepository>().GetProgramByGuid(insertProgram.ProgramGUID);

            CopyPageVariable(program, newProgram);

            UpdateProgramRoomAfterClone(program, newProgram);

            //copy programs not in default language
            List<Program> listPrograms = Resolve<IProgramRepository>().GetProgramByDefaultGUID(program.ProgramGUID).ToList();
            foreach (Program programItemOfInDefaultLanguage in listPrograms)
            {
                Program needBeClonedProgram = Resolve<IProgramRepository>().GetProgramByGuid(programItemOfInDefaultLanguage.ProgramGUID);

                //Program insertProgram = ServiceUtility.CloneProgram(program, GenerateProgramCode());
                CloneProgramParameterModel notDefaultProgramModel = new CloneProgramParameterModel
                {
                    ProgramGuidOfCopiedToProgramsInDefaultLanguage = insertProgram.ProgramGUID,
                    source = DefaultGuidSourceEnum.FromMatchDefaultGuidFunction,
                };
                Program insertProgramItem = CloneProgramIncludeDefaultGuid(needBeClonedProgram, GenerateProgramCode(), notDefaultProgramModel);
                insertProgramItem = SetDefaultGuidForProgram(insertProgramItem, notDefaultProgramModel);

                //SetProgramLanguage(needBeClonedProgram, insertProgramItem);
                if (!needBeClonedProgram.LanguageReference.IsLoaded) needBeClonedProgram.LanguageReference.Load();
                SetProgramLanguage(insertProgramItem, insertProgram, needBeClonedProgram.Language.LanguageGUID);

                Resolve<IProgramRepository>().InsertProgram(insertProgramItem);

                SetFullPermissionForProgram(insertProgramItem.ProgramGUID, userGuid);

                Program newProgramItem = Resolve<IProgramRepository>().GetProgramByGuid(insertProgramItem.ProgramGUID);

                CopyPageVariable(needBeClonedProgram, newProgramItem);

                UpdateProgramRoomAfterClone(needBeClonedProgram, newProgramItem);
            }
            #endregion

            #region old Function
            //Program program = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);

            ////Program insertProgram = ServiceUtility.CloneProgram(program, GenerateProgramCode());
            //Program insertProgram = ServiceUtility.CloneProgramNotIncludeParentGuid(program, GenerateProgramCode());

            //SetProgramLanguage(program, insertProgram);

            //Resolve<IProgramRepository>().InsertProgram(insertProgram);

            //SetFullPermissionForProgram(insertProgram.ProgramGUID, userGuid);

            //Program newProgram = Resolve<IProgramRepository>().GetProgramByGuid(insertProgram.ProgramGUID);

            //CopyPageVariable(program, newProgram);

            //UpdateProgramRoomAfterClone(program, newProgram);
            #endregion
        }

        /// <summary>
        /// Should be renamed to AddLanguageForProgramFromDefaultLanguage. But this rename need release a new workrole.So delay to do this.
        /// </summary>
        /// <param name="programGuid"></param>
        /// <param name="languageGuid"></param>
        /// <param name="userGuid"></param>
        public void AddLanguageForProgram(Guid programGuid, Guid languageGuid, Guid userGuid)
        {
            #region new function include default guid
            CloudQueue statusQueue = Resolve<IAzureQueueService>().GetCloudQueue(GetProgramLanguageServiceStatusQueueName(programGuid));
            string statusMsg = string.Empty;

            ProgramLanguage existLanguage = Resolve<IProgramLanguageRepository>().GetProgramLanguage(programGuid, languageGuid);
            if (existLanguage == null)
            {

                statusMsg = string.Format("{0};{1};{2};{3}", "Start", "AddProgramLanguage", programGuid, languageGuid);
                Resolve<IAzureQueueService>().AddQueueMessage(statusQueue, statusMsg, true);

                Program originalProgram = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
                //Program newProgram = ServiceUtility.CloneProgram(originalProgram, GenerateProgramCode());
                CloneProgramParameterModel cloneParameterModel = new CloneProgramParameterModel
                {
                    ProgramGuidOfCopiedToProgramsInDefaultLanguage = Guid.Empty,
                    source = DefaultGuidSourceEnum.FromPrimaryKey,
                };
                Program newProgram = CloneProgramIncludeDefaultGuid(originalProgram, GenerateProgramCode(), cloneParameterModel);
                newProgram = SetDefaultGuidForProgram(newProgram, cloneParameterModel);

                SetProgramLanguage(newProgram, originalProgram, languageGuid);
                //newProgram.ParentProgramGUID = programGuid; // This filed associate the program with original program// the new CloneProgramIncludeDefaultGuid function has assigned this field
                Resolve<IProgramRepository>().InsertProgram(newProgram);

                SetFullPermissionForProgram(newProgram.ProgramGUID, userGuid);

                statusMsg = string.Format("{0}", "Copy program page variable");
                Resolve<IAzureQueueService>().AddQueueMessage(statusQueue, statusMsg, true);

                newProgram = Resolve<IProgramRepository>().GetProgramByGuid(newProgram.ProgramGUID);
                CopyPageVariable(originalProgram, newProgram);

                statusMsg = string.Format("{0}", "Copy program room");
                Resolve<IAzureQueueService>().AddQueueMessage(statusQueue, statusMsg, true);
                UpdateProgramRoomAfterClone(originalProgram, newProgram);

                statusMsg = string.Format("{0}", "Check Tip Message");
                Resolve<IAzureQueueService>().AddQueueMessage(statusQueue, statusMsg, true);
                CheckTipMessage(programGuid);

                statusMsg = string.Format("{0}", "Check Sepcial String");
                Resolve<IAzureQueueService>().AddQueueMessage(statusQueue, statusMsg, true);
                CheckSpecialString(languageGuid);
            }
            else
            {
                statusMsg = string.Format("{0}", "This language version of program has existed, you must delete it firstly then you can add.");
                Resolve<IAzureQueueService>().AddQueueMessage(statusQueue, statusMsg, true);
            }

            statusMsg = string.Format("{0}", "Complete");
            Resolve<IAzureQueueService>().AddQueueMessage(statusQueue, statusMsg, true);
            #endregion

            #region old Function
            //CloudQueue statusQueue = Resolve<IAzureQueueService>().GetCloudQueue(GetProgramLanguageServiceStatusQueueName(programGuid));
            //string statusMsg = string.Empty;

            //ProgramLanguage existLanguage = Resolve<IProgramLanguageRepository>().GetProgramLanguage(programGuid, languageGuid);
            //if (existLanguage == null)
            //{

            //    statusMsg = string.Format("{0};{1};{2};{3}", "Start", "AddProgramLanguage", programGuid, languageGuid);
            //    Resolve<IAzureQueueService>().AddQueueMessage(statusQueue, statusMsg, true);

            //    Program originalProgram = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
            //    Program newProgram = ServiceUtility.CloneProgram(originalProgram, GenerateProgramCode());

            //    SetProgramLanguage(newProgram, originalProgram, languageGuid);
            //    newProgram.ParentProgramGUID = programGuid; // This filed associate the program with original program
            //    Resolve<IProgramRepository>().InsertProgram(newProgram);

            //    SetFullPermissionForProgram(newProgram.ProgramGUID, userGuid);

            //    statusMsg = string.Format("{0}", "Copy program page variable");
            //    Resolve<IAzureQueueService>().AddQueueMessage(statusQueue, statusMsg, true);

            //    newProgram = Resolve<IProgramRepository>().GetProgramByGuid(newProgram.ProgramGUID);
            //    CopyPageVariable(originalProgram, newProgram);

            //    statusMsg = string.Format("{0}", "Copy program room");
            //    Resolve<IAzureQueueService>().AddQueueMessage(statusQueue, statusMsg, true);
            //    UpdateProgramRoomAfterClone(originalProgram, newProgram);

            //    statusMsg = string.Format("{0}", "Check Tip Message");
            //    Resolve<IAzureQueueService>().AddQueueMessage(statusQueue, statusMsg, true);
            //    CheckTipMessage(programGuid);

            //    statusMsg = string.Format("{0}", "Check Sepcial String");
            //    Resolve<IAzureQueueService>().AddQueueMessage(statusQueue, statusMsg, true);
            //    CheckSpecialString(languageGuid);
            //}
            //else
            //{
            //    statusMsg = string.Format("{0}", "This language version of program has existed, you must delete it firstly then you can add.");
            //    Resolve<IAzureQueueService>().AddQueueMessage(statusQueue, statusMsg, true);
            //}

            //statusMsg = string.Format("{0}", "Complete");
            //Resolve<IAzureQueueService>().AddQueueMessage(statusQueue, statusMsg, true);
            #endregion
        }

        #region New copy program flow, in use now.
        public Program CloneProgramIncludeDefaultGuid(Program program, string programCode, CloneProgramParameterModel cloneParameterModel)
        {
            try
            {
                Program cloneProgram = new Program();
                cloneProgram.ProgramGUID = Guid.NewGuid();
                cloneProgram.Name = string.Format("Copy of {0} on {1}", program.Name, DateTime.UtcNow.ToString());
                cloneProgram.NameByDeveloper = string.Format("Copy of {0} on {1}", program.NameByDeveloper, DateTime.UtcNow.ToString());
                cloneProgram.Description = program.Description;
                cloneProgram.OrderProgramText = program.OrderProgramText;
                cloneProgram.EnableHtml5NewUI = program.EnableHtml5NewUI;
                cloneProgram.Code = programCode;

                //ParentGuid and DefaultGuid
                //TODO: need test this flow for all CloneProgramParameterModel enum
                cloneProgram.ParentProgramGUID = program.ProgramGUID;
                cloneProgram.DefaultGUID = program.DefaultGUID;
                cloneProgram.CoverShadowVisible = program.CoverShadowVisible;

                if (!program.ProgramStatusReference.IsLoaded)
                {
                    program.ProgramStatusReference.Load();
                }
                cloneProgram.ProgramStatus = program.ProgramStatus;
                cloneProgram.GeneralColor = program.GeneralColor;

                // layout setting
                if (!program.LayoutSettingReference.IsLoaded)
                {
                    program.LayoutSettingReference.Load();
                }
                cloneProgram.LayoutSetting = new LayoutSetting
                {
                    SettingXML = program.LayoutSetting.SettingXML
                };
                // email template
                if (!program.EmailTemplate.IsLoaded)
                {
                    program.EmailTemplate.Load();
                }
                foreach (EmailTemplate template in program.EmailTemplate)
                {
                    EmailTemplate clonedTemplate = Resolve<IEmailTemplateService>().CloneEmailTemplate(template);

                    cloneProgram.EmailTemplate.Add(Resolve<IEmailTemplateService>().SetDefaultGuidForEmailTemplate(clonedTemplate, cloneParameterModel));
                }

                // program room
                if (!program.ProgramRoom.IsLoaded)
                {
                    program.ProgramRoom.Load();
                }
                foreach (ProgramRoom room in program.ProgramRoom)
                {
                    ProgramRoom clonedRoom = Resolve<IProgramRoomService>().CloneProgramRoom(room);

                    cloneProgram.ProgramRoom.Add(Resolve<IProgramRoomService>().SetDefaultGuidForProgramRoom(clonedRoom, cloneParameterModel));
                }

                // page variable group
                if (!program.PageVariableGroup.IsLoaded)
                {
                    program.PageVariableGroup.Load();
                }
                foreach (PageVariableGroup group in program.PageVariableGroup)
                {
                    PageVariableGroup clonedGroup = Resolve<IPageVariableGroupService>().CloneVariableGroup(group);

                    cloneProgram.PageVariableGroup.Add(Resolve<IPageVariableGroupService>().SetDefaultGuidForPageVariableGroup(clonedGroup, cloneParameterModel));
                }

                // program logo
                if (!program.ResourceReference.IsLoaded)
                {
                    program.ResourceReference.Load();
                }
                cloneProgram.Resource = program.Resource;

                // login template, session ending template, password reminder template
                if (!program.AccessoryTemplate.IsLoaded)
                {
                    program.AccessoryTemplate.Load();
                }
                foreach (AccessoryTemplate accessoryTemplate in program.AccessoryTemplate)
                {
                    AccessoryTemplate clonedAccessoryTemplate = Resolve<IProgramAccessoryService>().CloneAccessoryTemplate(accessoryTemplate);

                    cloneProgram.AccessoryTemplate.Add(Resolve<IProgramAccessoryService>().SetDefaultGuidForAccessoryTemplate(clonedAccessoryTemplate, cloneParameterModel));
                }

                // Help item
                if (!program.HelpItem.IsLoaded)
                {
                    program.HelpItem.Load();
                }
                foreach (HelpItem helpItem in program.HelpItem)
                {
                    HelpItem clonedHelpItem = Resolve<IHelpItemService>().CloneHelpItem(helpItem);

                    cloneProgram.HelpItem.Add(Resolve<IHelpItemService>().SetDefaultGuidForHelpItem(clonedHelpItem, cloneParameterModel));
                }

                // User menu
                if (!program.UserMenu.IsLoaded)
                {
                    program.UserMenu.Load();
                }
                foreach (UserMenu userMenu in program.UserMenu)
                {
                    UserMenu clonedUserMenu = Resolve<IUserMenuService>().CloneUserMenu(userMenu);

                    cloneProgram.UserMenu.Add(Resolve<IUserMenuService>().SetDefaultGuidForUserMenu(clonedUserMenu, cloneParameterModel));
                }

                List<KeyValuePair<string, string>> relpaseGUIDPairList = new List<KeyValuePair<string, string>>();
                //Dictionary<Guid, Guid> GOSUBRelapseGuidDic = new Dictionary<Guid, Guid>();
                // relapse
                if (!program.Relapse.IsLoaded)
                {
                    program.Relapse.Load();
                }
                foreach (Relapse rel in program.Relapse)
                {
                    Relapse clonedRelapse = Resolve<IRelapseService>().CloneRelapse(rel, cloneParameterModel); //CloneRelapse(rel);
                    clonedRelapse = Resolve<IRelapseService>().SetDefaultGuidForRelapse(clonedRelapse, cloneParameterModel);//set defaultguid

                    cloneProgram.Relapse.Add(clonedRelapse);
                    relpaseGUIDPairList.Add(new KeyValuePair<string, string>(rel.PageSequence.PageSequenceGUID.ToString().ToUpper(), clonedRelapse.PageSequence.PageSequenceGUID.ToString().ToUpper()));
                    //GOSUBRelapseGuidDic.Add(rel.PageSequence.PageSequenceGUID, clonedRelapse.PageSequence.PageSequenceGUID);
                }

                foreach (Relapse clonedRelapse in cloneProgram.Relapse)
                {
                    Resolve<IPageService>().UpdatePageContentForCloneRelapse(clonedRelapse, relpaseGUIDPairList);
                }

                // schedule
                if (!program.ProgramSchedule.IsLoaded)
                {
                    program.ProgramSchedule.Load();
                }
                foreach (ProgramSchedule schedule in program.ProgramSchedule)
                {
                    ProgramSchedule clonedSchedule = Resolve<IProgramScheduleService>().CloneProgramSchedule(schedule);

                    cloneProgram.ProgramSchedule.Add(Resolve<IProgramScheduleService>().SetDefaultGuidForProgramSchedule(clonedSchedule, cloneParameterModel));
                }

                // tip message
                if (!program.TipMessage.IsLoaded)
                {
                    program.TipMessage.Load();
                }
                foreach (TipMessage message in program.TipMessage)
                {
                    TipMessage clonedMessage = Resolve<ITipMessageService>().CloneTipMessage(message);

                    cloneProgram.TipMessage.Add(Resolve<ITipMessageService>().SetDefaultGuidForTipMessage(clonedMessage, cloneParameterModel));
                }

                // session 
                if (!program.Session.IsLoaded)
                {
                    program.Session.Load();
                }
                List<Session> sessions = program.Session.Where(s => s.IsDeleted != true).ToList();
                foreach (Session session in sessions)
                {
                    if (!session.SessionContent.IsLoaded)
                    {
                        session.SessionContent.Load();
                    }
                    Session newSession = Resolve<ISessionService>().CloneSession(session, relpaseGUIDPairList, cloneParameterModel); //CloneSession(session, relpaseGUIDPairList);

                    newSession = Resolve<ISessionService>().SetDefaultGuidForSession(newSession, cloneParameterModel);
                    cloneProgram.Session.Add(newSession);
                }
                return cloneProgram;
            }
            catch (Exception ex)
            {
                //add log.
                InsertLogModel insertLogModel = new InsertLogModel
                {
                    ActivityLogType = (int)LogTypeEnum.AddProgram,
                    Browser = string.Empty,
                    From = string.Empty,
                    IP = string.Empty,
                    Message = string.Format("CloneProgramIncludeDefaultGuidException:{0}", ex),
                    PageGuid = Guid.Empty,
                    PageSequenceGuid = Guid.Empty,
                    SessionGuid = Guid.Empty,
                    ProgramGuid = Guid.Empty,
                    UserGuid = Guid.Empty,
                };
                Resolve<IActivityLogService>().Insert(insertLogModel);
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }
        public Program SetDefaultGuidForProgram(Program needSetDefaultEntity, CloneProgramParameterModel cloneParameterModel)
        {
            Program newEntity = new Program();

            newEntity = needSetDefaultEntity;
            switch (cloneParameterModel.source)
            {
                case DefaultGuidSourceEnum.FromDefaultGuid:
                    //newEntity.DefaultGUID = needSetDefaultEntity.DefaultGUID;//This is default value, don't need assign again.
                    break;
                case DefaultGuidSourceEnum.FromMatchDefaultGuidFunction:
                    newEntity.DefaultGUID = cloneParameterModel.ProgramGuidOfCopiedToProgramsInDefaultLanguage;
                    break;
                case DefaultGuidSourceEnum.FromNull:
                    newEntity.DefaultGUID = null;
                    break;
                case DefaultGuidSourceEnum.FromPrimaryKey:
                    newEntity.DefaultGUID = needSetDefaultEntity.ParentProgramGUID;
                    break;
                default:
                    throw new Exception("Error: Dont't find the DefaultGuidSource for Program Entity.");
                //break;
            }

            return newEntity;
        }
        #endregion

        private string GetProgramLanguageServiceStatusQueueName(Guid programGuid)
        {
            return string.Format("{0}{1}{2}", STATUSQUEUEMAKT, programGuid.ToString().ToLower(), versionNumber);
        }

        private void UpdateProgramRoomAfterClone(Program program, Program newProgram)
        {
            //update programroom with the new one
            if (!program.ProgramRoom.IsLoaded)
            {
                program.ProgramRoom.Load();
            }
            foreach (ProgramRoom room in program.ProgramRoom)
            {
                Resolve<IStoreProcedure>().UpdateProgramRoomAfterCopyProgram(program.ProgramGUID, newProgram.ProgramGUID, room.Name);
            }
        }

        public void EditProgram(ProgramModel program)  //(Guid guid, Guid statusGuid, string programName, string description, Guid defauleLanguage, Guid ProgramLogoGuid, string LogoName, string LogoType, string LogoFileExtension, string shortName)
        {   
            Program programEntity = Resolve<IProgramRepository>().GetProgramByGuid(program.Guid);
            // TODO: Set status when database design is complete
            programEntity.ProgramStatus = Resolve<IProgramStatusRepository>().GetProgramStatusByStatusGuid(program.StatusGuid);
            programEntity.Name = program.ProgramName;
            programEntity.NameByDeveloper = program.NameInDeveloper;
            programEntity.ShortName = program.ShortName;
            programEntity.Description = program.Description;
            //programEntity.IsNeedPinCode = isNeedPinCode;
            //programEntity.GeneralColor = "0x" + maincolor;
            programEntity.Language = Resolve<ILanguageRepository>().GetLanguage(program.DefaultLanguage);
            programEntity.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
            
            if (program.ProgramLogo.Name != "")
            {
                programEntity.Resource = new Resource
                {
                    ResourceGUID = program.ProgramLogo.ID,
                    Name =program.ProgramLogo.Name,
                    Type = program.ProgramLogo.Type,
                    FileExtension = program.ProgramLogo.Extension,
                    NameOnServer =program.ProgramLogo.ID+program.ProgramLogo.Extension,
                    ResourceCategory = Resolve<IResourceCategoryRepository>().GetResourceCategory("Default"),
                };
            }

            CTPP ctpp = Resolve<ICTPPRepository>().GetCTPPByProgramGuid(program.Guid);
            if (ctpp != null)
            {
                ctpp.ProgramName = program.ProgramName;
                Resolve<ICTPPRepository>().Update(ctpp);
            }

            Resolve<IProgramRepository>().Update(programEntity);

        }

        //public void EditProgram(Guid guid, Guid statusGuid, string programName, string description, Guid defauleLanguage, Guid ProgramLogoGuid, string LogoName, string LogoType, string LogoFileExtension, string shortName)
        //{
        //    Program programEntity = Resolve<IProgramRepository>().GetProgramByGuid(guid);
        //    // TODO: Set status when database design is complete
        //    programEntity.ProgramStatus = Resolve<IProgramStatusRepository>().GetProgramStatusByStatusGuid(statusGuid);
        //    programEntity.Name = programName;
        //    programEntity.ShortName = shortName;
        //    programEntity.Description = description;
        //    //programEntity.IsNeedPinCode = isNeedPinCode;
        //    //programEntity.GeneralColor = "0x" + maincolor;
        //    programEntity.Language = Resolve<ILanguageRepository>().GetLanguage(defauleLanguage);
        //    programEntity.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;

        //    if (LogoName != "")
        //    {
        //        programEntity.Resource = new Resource
        //        {
        //            ResourceGUID = ProgramLogoGuid,
        //            Name = LogoName,
        //            Type = LogoType,
        //            FileExtension = LogoFileExtension,
        //            NameOnServer = ProgramLogoGuid + LogoFileExtension,
        //            ResourceCategory = Resolve<IResourceCategoryRepository>().GetResourceCategory("Default"),
        //        };
        //    }
        //    Resolve<IProgramRepository>().Update(programEntity);
        //}

        public bool CanDeleteProgram(Guid programGuid)
        {
            bool flug = true;
            IQueryable<ProgramUser> ps = Resolve<IProgramUserRepository>().GetProgramPermissionUserListByProgramGuid(programGuid, (int)PermissionEnum.ProgramView);
            if (ps.Count() > 0)
            {
                flug = false;
            }

            return flug;
        }

        public void DeleteProgram(Guid programGuid)
        {
            Program programEntity = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
            Resolve<IProgramUserRepository>().DeleteProgramUserByProgramGuid(programGuid);

            if (!programEntity.Session.IsLoaded)
            {
                programEntity.Session.Load();
            }

            foreach (Session sessionEntity in programEntity.Session)
            {
                if (!sessionEntity.SessionContent.IsLoaded)
                {
                    sessionEntity.SessionContent.Load();
                }

                EntityCollection<SessionContent> sessionContentCollection = sessionEntity.SessionContent;
                DeletePageSequenceOfProgram(sessionContentCollection);
                Resolve<ISessionContentRepository>().DeleteSessionContent(sessionContentCollection);
            }

            Resolve<ISessionRepository>().DeleteSession(programEntity.Session);
            //Resolve<IProgramLanguageRepository>().DeleteAllLangaugeOfProgram(programEntity.ProgramGUID);
            // need to delete Relapse first, then delete ProgramRoom
            Resolve<IRelapseRepository>().DeleteRelapseOfProgram(programGuid);
            Resolve<IProgramRoomRepository>().DeleteRoomOfProgram(programGuid);

            // TODO: Check with customer about whether need to delete question answer of this program
            //Resolve<IPageVaribleRepository>().DeleteVariableOfProgram(programGuid);
            //Resolve<IPageVariableGroupRepository>().DeleteGroupOfProgram(programGuid);
            //Comment out by Di fujie 20100204

            // Help item
            Resolve<IHelpItemRepository>().DeleteHelpItemOfProgram(programGuid);

            // Added by Chen Pu 2010-06-04, Delete user menu, login, session ending, password reminder, email template
            // User menu
            Resolve<IUserMenuRepository>().DeleteUserMenu(programGuid);

            // login template, session ending template, password reminder template
            Resolve<IProgramAccessoryRepository>().DeleteAccessoryOfProgram(programGuid);

            // Email
            Resolve<IEmailTemplateRepository>().DeleteEmailTemplateOfProgram(programGuid);

            Resolve<IProgramRepository>().DeleteProgram(programGuid);
        }

        public void AddProgram(ProgramModel programModel, Guid userGuid)
        {
            Program program = new Program();
            program.ProgramGUID = Guid.NewGuid();
            program.Name = programModel.ProgramName;
            program.Code = GenerateProgramCode();
            program.Description = programModel.Description;
            program.Language = Resolve<ILanguageRepository>().GetLanguage(programModel.DefaultLanguage);
            program.ProgramStatus = Resolve<IProgramStatusRepository>().GetProgramStatusByStatusGuid(new Guid("bcb92cef-fd49-4818-bbb2-8dfc96ff0fe1"));
            if (programModel.ProgramLogo != null)
            {
                program.Resource = new Resource
                {
                    ResourceGUID = programModel.ProgramLogo.ID,
                    Name = programModel.ProgramLogo.Name,
                    Type = programModel.ProgramLogo.Type,
                    FileExtension = programModel.ProgramLogo.Extension,
                    NameOnServer = programModel.ProgramLogo.NameOnServer,
                    ResourceCategory = Resolve<IResourceCategoryRepository>().GetResourceCategory("Default"),
                };
            }
            program.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
            // add layout setting
            LayoutSetting layout = new LayoutSetting
            {
                ProgramGUID = program.ProgramGUID,
                SettingXML = Resolve<ISystemSettingRepository>().GetSettingValue("LayOutSetting")
            };
            program.LayoutSetting = layout;
            // default color can be change through the UI
            program.GeneralColor = "0x5cab3c";
            //program.CoverShadowVisible = true;
            //program.CoverShadowColor = " ";
            //program.PrimaryButtonColorNormal = "0x5CAB3C,0x0A8342";
            //program.PrimaryButtonColorOver = "0x66C141,0x096F31";
            //program.PrimaryButtonColorDown = "0x5CAB3C,0x0A8342";
            //program.PrimaryButtonColorDisable = "0x5CAB3C,0x0A8342";

            // add usermenu
            Resolve<IUserMenuService>().addUserMenuFormProgarm(program);

            Resolve<IProgramRepository>().InsertProgram(program);

            // Add language to program language table
            ProgramLanguage programLanguageEntity = new ProgramLanguage();
            programLanguageEntity.Program = Resolve<IProgramRepository>().GetProgramByGuid(program.ProgramGUID);
            programLanguageEntity.Language = Resolve<ILanguageRepository>().GetLanguage(programModel.DefaultLanguage);
            programLanguageEntity.ProgramLanguageGUID = Guid.NewGuid();

            Resolve<IProgramLanguageRepository>().AddProgramLanguage(programLanguageEntity);

            SetFullPermissionForProgram(program.ProgramGUID, userGuid);
        }

        public void JoinProgram(Guid programGuid, Guid userGuid, Guid companyguid, int mailtime, string clientIP, ProgramUserStatusEnum userStatus, string studyContent)
        {
            DateTime setCurrentTimeByTimeZone = Resolve<IProgramUserService>().GetCurrentTimeByTimeZone(programGuid, userGuid, DateTime.UtcNow);
            ProgramUser ProgramUser = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuidAndUserGuid(programGuid, userGuid);
            if (ProgramUser == null)
            {
                ProgramUser pu = new ProgramUser();
                pu.ProgramUserGUID = Guid.NewGuid();
                pu.Security = (int)PermissionEnum.ProgramView;
                pu.Program = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
                pu.User = Resolve<IUserRepository>().GetUserByGuid(userGuid);
                pu.MailTime = mailtime;
                pu.StartDate = setCurrentTimeByTimeZone; //DateTime.UtcNow;
                //ps.Language = Resolve<ILanguageRepository>().GetLanguage(languageGUID);
                pu.Day = 0;
                pu.RegisteredIP = clientIP;
                pu.Status = Enum.GetName(typeof(ProgramUserStatusEnum), userStatus);

                if (companyguid != Guid.Empty)
                {
                    pu.Company = Resolve<ICompanyRepository>().GetCompanyByGuid(companyguid);
                }

                if (!string.IsNullOrEmpty(studyContent))
                {
                    pu.StudyContent = Resolve<IStudyContentRepository>().Get(new Guid(studyContent));
                }

                Resolve<IProgramUserRepository>().Insert(pu);
            }
            #region not else
            else
            {
                // as the AddExistUserForProgram.aspx was not use now, there is no chance to go this way. 2011-07-26
                //ProgramUser.Security = (int)Permission.ProgramView;
                //ProgramUser.MailTime = mailtime;
                ////ProgramUser.Language = Resolve<ILanguageRepository>().GetLanguage(languageGUID);
                //ProgramUser.Status = Enum.GetName(typeof(ProgramUserStatusEnum), userStatus);
                //ProgramUser.StartDate = DateTime.UtcNow;
                //ProgramUser.LastFinishDate = DateTime.UtcNow;
                //ProgramUser.Day = 0;
                //if (companyguid != Guid.Empty)
                //{
                //    ProgramUser.Company = Resolve<ICompanyRepository>().GetCompanyByGuid(companyguid);
                //}
                //else
                //{
                //    ProgramUser.Company = null;
                //}

                //Resolve<IProgramUserRepository>().UpdateProgramSecurity(ProgramUser);
            } 
            #endregion
        }

        //public void UpdateProgramSecurity(Guid progamGuid, Guid userGuid, Permission security, int mailTime, string status)
        //{
        //    ProgramUser ps = Resolve<IProgramUserRepository>().GetProgramSecuirty(progamGuid, userGuid);
        //    ps.MailTime = mailTime;
        //    //ps.UserType = usertype;
        //    //ps.Language = Resolve<ILanguageRepository>().GetLanguage(languageGuid);
        //    ps.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
        //    ps.Security = (int)security;
        //    ps.Status = status;
        //    Resolve<IProgramUserRepository>().UpdateProgramSecurity(ps);
        //}

        //public void UpdateProgramSecurity(Guid programuserguid, Permission userSecurity, int mailTime, string status, string email, string firstName, string lastName, string mobile, string gender)
        //{
        //    ProgramUser ps = Resolve<IProgramUserRepository>().GetProgramuserByProgramUserGuid(programuserguid);
        //    ps.MailTime = mailTime;
        //    ps.Security = (int)userSecurity;
        //    ps.Status = status;

        //    if(!ps.UserReference.IsLoaded)
        //    {
        //        ps.UserReference.Load();
        //    }
        //    if(ps.User != null)
        //    {
        //        ps.User.Email = email;
        //        ps.User.FirstName = firstName;
        //        ps.User.LastName = lastName;
        //        ps.User.MobilePhone = mobile;
        //        ps.User.Gender = gender;
        //    }

        //    Resolve<IProgramUserRepository>().UpdateProgramSecurity(ps);
        //}

        public void DeleteProgramUser(Guid programuserguid)
        {
            OrderLicence orderLicencePuEntity = Resolve<IOrderLicenceRepository>().GetOrderLicenceByProgramUserGuid(programuserguid);
            if (orderLicencePuEntity != null)
            {
                Resolve<IOrderLicenceRepository>().DeleteEntityByProgramUserGuid(programuserguid);
            }
            Resolve<IProgramUserRepository>().DeleteProgramUser(programuserguid);
        }

        public SortedList<Guid, int> GetProgramPermissionByUserGuid(Guid userGuid)
        {
            SortedList<Guid, int> perssionList = new SortedList<Guid, int>();

            List<ProgramUser> psList = Resolve<IProgramUserRepository>().GetProgramUserListByUserGuid(userGuid).ToList();
            foreach (ProgramUser ps in psList)
            {
                if (!ps.ProgramReference.IsLoaded)
                {
                    ps.ProgramReference.Load();
                }
                perssionList.Add(ps.Program.ProgramGUID, ps.Security);
            }

            return perssionList;
        }

        public Guid GetProgramGuidBySessionContentGuid(Guid sessionContentGuid)
        {
            SessionContent sessionContent = Resolve<ISessionContentRepository>().GetSessionContentBySessionContentGuid(sessionContentGuid);
            if (!sessionContent.SessionReference.IsLoaded)
            {
                sessionContent.SessionReference.Load();
            }
            if (!sessionContent.Session.ProgramReference.IsLoaded)
            {
                sessionContent.Session.ProgramReference.Load();
            }

            return sessionContent.Session.Program.ProgramGUID;
        }

        public ProgramModel GetProgramByGUID(Guid programGUID)
        {
            ProgramModel pm = new ProgramModel();
            Program programEntity = Resolve<IProgramRepository>().GetProgramByGuid(programGUID);
            if (!programEntity.LanguageReference.IsLoaded)
            {
                programEntity.LanguageReference.Load();
            }
            pm.DefaultLanguage = programEntity.Language.LanguageGUID;
            pm.Description = programEntity.Description;
            pm.ProgramName = programEntity.Name;
            pm.Guid = programGUID;
            pm.Code = programEntity.Code;
            pm.IsNeedPinCode = programEntity.IsNeedPinCode.HasValue ? programEntity.IsNeedPinCode.Value : false;
            if (!programEntity.ProgramStatusReference.IsLoaded)
            {
                programEntity.ProgramStatusReference.Load();
            }
            pm.StatusName = programEntity.ProgramStatus.Name;
            pm.IsCTPPEnable = programEntity.IsCTPPEnable.HasValue ? programEntity.IsCTPPEnable.Value : false;
            pm.IsNoCatchUp = programEntity.IsNoCatchUp.HasValue ? programEntity.IsNoCatchUp.Value : false;
            pm.IsSupportTimeZone = programEntity.IsSupportTimeZone.HasValue ? programEntity.IsSupportTimeZone.Value : false;
            pm.TimeZone = programEntity.TimeZone.HasValue ? programEntity.TimeZone.Value : 0;
            pm.IsOrderProgram = programEntity.IsOrderProgram.HasValue ? programEntity.IsOrderProgram.Value : false;
            pm.IsHPOrderProgram = programEntity.IsHPOrderProgram.HasValue ? programEntity.IsHPOrderProgram.Value : false;
            pm.IsInvisibleStartButton = programEntity.IsNotShowStartButton.HasValue ? programEntity.IsNotShowStartButton.Value : false;
            pm.IsInvisibleDayAndSetMenu = programEntity.IsNotShowDayAndSetMenu.HasValue ? programEntity.IsNotShowDayAndSetMenu.Value : false;
            pm.OrderProgramText = !string.IsNullOrEmpty(programEntity.OrderProgramText) ? programEntity.OrderProgramText : string.Empty;
            pm.ShortName = !string.IsNullOrEmpty(programEntity.ShortName) ? programEntity.ShortName : string.Empty;
            if (!programEntity.ResourceReference.IsLoaded)
            {
                programEntity.ResourceReference.Load();
            }
            if (programEntity.Resource != null)
            {
                pm.ProgramLogo = new ResourceModel
                {
                    Extension = programEntity.Resource.FileExtension,
                    ID = programEntity.Resource.ResourceGUID,
                    Name = programEntity.Resource.Name,
                    NameOnServer = programEntity.Resource.NameOnServer,
                    Type = programEntity.Resource.Type,
                };
            }
            return pm;
        }

        public void DeleteProgramUser(Guid programGuid, Guid userGuid)
        {
            Resolve<IProgramUserRepository>().DeleteProgramUserByProgramGuidAndUserGuid(programGuid, userGuid);
        }

        public LanguageModel GetProgramLanguage(Guid programGuid)
        {
            Program program = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
            if (!program.LanguageReference.IsLoaded)
            {
                program.LanguageReference.Load();
            }

            LanguageModel languageModel = new LanguageModel
            {
                LanguageGUID = program.Language.LanguageGUID,
                Name = program.Language.Name
            };

            return languageModel;
        }

        public List<SimpleProgramModel> GetAllSimpleProgramModel()
        {
            List<SimpleProgramModel> simpleModel = new List<SimpleProgramModel>();
            IQueryable<Program> programs = Resolve<IProgramRepository>().GetAllPrograms();
            foreach (Program program in programs)
            {
                SimpleProgramModel programModel = new SimpleProgramModel
                {
                    ProgramGuid = program.ProgramGUID,
                    ProgramName = program.Name
                };
                simpleModel.Add(programModel);
            }

            return simpleModel;
        }

        public List<SimpleProgramModel> GetSimpleProgramsModel()
        {
            List<SimpleProgramModel> simpleModel = new List<SimpleProgramModel>();
            IQueryable<Program> programs = Resolve<IProgramRepository>().GetAllPrograms();
            programs = programs.Where(p => !p.DefaultGUID.HasValue).OrderBy(p => p.Name);

            foreach (Program program in programs)
            {
                SimpleProgramModel programModel = new SimpleProgramModel
                {
                    ProgramGuid = program.ProgramGUID,
                    ProgramName = program.Name
                };
                simpleModel.Add(programModel);
            }

            return simpleModel;
        }

        public string GetProgramLogo(Guid ProgramGuid)
        {
            Program program = Resolve<IProgramRepository>().GetProgramByGuid(ProgramGuid);
            string result = string.Empty;
            if (program != null)
            {
                if (!program.ResourceReference.IsLoaded)
                {
                    program.ResourceReference.Load();
                }
                if (program.Resource != null)
                {
                    result = program.Resource.NameOnServer;
                }
            }
            return result;
        }

        public void AssignManagerToProgram(Guid programGuid, Guid userGuid)
        {
            Program programEntity = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
            programEntity.User = Resolve<IUserRepository>().GetUserByGuid(userGuid);
            Resolve<IProgramRepository>().Update(programEntity);
        }

        public SimpleProgramModel GetSimpleProgram(Guid programGUID)
        {
            Program programEntity = Resolve<IProgramRepository>().GetProgramByGuid(programGUID);
            SimpleProgramModel program = new SimpleProgramModel
            {
                ProgramGuid = programGUID,
                ProgramName = programEntity.Name
            };

            return program;
        }

        public string GetProgramSetting(Guid programGuid)
        {
            string xmlSetting = string.Empty;
            LayoutSetting setting = Resolve<IProgramRepository>().GetLayoutSettingByProgramGUID(programGuid);
            if (setting != null)
            {
                xmlSetting = setting.SettingXML;
            }

            return xmlSetting;
        }

        public string GetProgramSettingBySessinGUID(Guid sessionGuid)
        {
            string xmlSetting = string.Empty;
            Session session = Resolve<ISessionRepository>().GetSessionBySessionGuid(sessionGuid);
            if (!session.ProgramReference.IsLoaded)
            {
                session.ProgramReference.Load();
            }

            return GetProgramSetting(session.Program.ProgramGUID);
        }

        public string GetProgramMainColor(Guid programGuid)
        {
            return Resolve<IProgramRepository>().GetProgramByGuid(programGuid).GeneralColor.Substring(2);
        }

        public void UpdateProgramMainColor(Guid programGuid, string toplinecolor, string shadowcolor, bool isvisible)
        {
            Program program = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
            if (!string.IsNullOrEmpty(toplinecolor))
            {
                program.GeneralColor = "0x" + toplinecolor;
            }
            else
            {
                program.GeneralColor = null;
            }

            if (!string.IsNullOrEmpty(shadowcolor))
            {
                program.CoverShadowColor = "0x" + shadowcolor;
            }
            else
            {
                program.CoverShadowColor = null;
            }
            program.CoverShadowVisible = isvisible;
            Resolve<IProgramRepository>().Update(program);
        }

        public void UpdateProgarmPrimaryButtonAndTopLineColor(Guid programGuid, string normalFrom, string normalTo, string overFrom, string overTo, string downFrom, string downTo, string disableFrom, string disableTo, string topLineColor)
        {
            string normalColor = "";
            string overColor = "";
            string disableColor = "";
            string downColor = "";
            string topLineBarColor = "";

            Program program = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
            if (!string.IsNullOrEmpty(normalFrom) &&
                !string.IsNullOrEmpty(normalTo))
            {
                normalColor = "0x" + normalFrom + ",0x" + normalTo;
                program.PrimaryButtonColorNormal = normalColor;
            }
            else
            {
                program.PrimaryButtonColorNormal = null;
            }
            if (!string.IsNullOrEmpty(overFrom) &&
                !string.IsNullOrEmpty(overTo))
            {
                overColor = "0x" + overFrom + ",0x" + overTo;
                program.PrimaryButtonColorOver = overColor;
            }
            else
            {
                program.PrimaryButtonColorOver = null;
            }
            if (!string.IsNullOrEmpty(disableFrom) &&
                !string.IsNullOrEmpty(disableTo))
            {
                disableColor = "0x" + disableFrom + ",0x" + disableTo;
                program.PrimaryButtonColorDisable = disableColor;
            }
            if (!string.IsNullOrEmpty(downFrom) &&
                !string.IsNullOrEmpty(downTo))
            {
                downColor = "0x" + downFrom + ",0x" + downTo;
                program.PrimaryButtonColorDown = downColor;
            }
            else
            {
                program.PrimaryButtonColorDown = null;
            }
            if (!string.IsNullOrEmpty(topLineColor))
            {
                topLineBarColor = "0x" + topLineColor;
            }

            Resolve<IProgramRepository>().Update(program);

            // update primary button color in layout setting
            LayoutSetting layout = Resolve<IProgramRepository>().GetLayoutSettingByProgramGUID(programGuid);
            string xmlSetting = GetProgramSetting(programGuid);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(layout.SettingXML);
            foreach (XmlNode node in doc.FirstChild.ChildNodes)
            {
                switch (node.Attributes["Name"].Value)
                {
                    case "PrimaryButtonColorNormal":
                        node.Attributes["Value"].Value = normalColor;
                        break;
                    case "PrimaryButtonColorOver":
                        node.Attributes["Value"].Value = overColor;
                        break;
                    case "PrimaryButtonColorDown":
                        node.Attributes["Value"].Value = downColor;
                        break;
                    case "PrimaryButtonColorDisable":
                        node.Attributes["Value"].Value = disableColor;
                        break;
                    case "TopBarColor":
                        node.Attributes["Value"].Value = topLineBarColor;
                        break;
                }
            }

            layout.SettingXML = doc.InnerXml;

            Resolve<IProgramRepository>().UpdateLayout(layout);
        }

        public ProgramColorModel GetProgramColor(Guid programGuid)
        {
            Program program = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
            ProgramColorModel model = new ProgramColorModel
            {
                CoverShadowColor = string.IsNullOrEmpty(program.CoverShadowColor) ? "" : program.CoverShadowColor.Substring(2),
                GeneralColor = string.IsNullOrEmpty(program.GeneralColor) ? "" : program.GeneralColor.Substring(2),
                IsShadowVisible = program.CoverShadowVisible
            };

            return model;
        }

        public PrimaryButtonColorModel GetPrimaryButtonColor(Guid programGuid)
        {
            PrimaryButtonColorModel model = new PrimaryButtonColorModel();
            LayoutSetting layout = Resolve<IProgramRepository>().GetLayoutSettingByProgramGUID(programGuid);
            string xmlSetting = GetProgramSetting(programGuid);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(layout.SettingXML);
            foreach (XmlNode node in doc.FirstChild.ChildNodes)
            {
                switch (node.Attributes["Name"].Value)
                {
                    case "PrimaryButtonColorNormal":
                        string[] normal = node.Attributes["Value"].Value.Split(',');
                        model.normalFrom = normal[0].Substring(2);
                        model.normalTo = normal[1].Substring(2);
                        break;
                    case "PrimaryButtonColorOver":
                        string[] over = node.Attributes["Value"].Value.Split(',');
                        model.overFrom = over[0].Substring(2);
                        model.overTo = over[1].Substring(2);
                        break;
                    case "PrimaryButtonColorDown":
                        string[] down = node.Attributes["Value"].Value.Split(',');
                        model.downFrom = down[0].Substring(2);
                        model.downTo = down[1].Substring(2);
                        break;
                    case "PrimaryButtonColorDisable":
                        string[] disable = node.Attributes["Value"].Value.Split(',');
                        model.disableFrom = disable[0].Substring(2);
                        model.disableTo = disable[1].Substring(2);
                        break;
                }
            }
            return model;
        }

        public int GetNumberOfUser(Guid programGuid, string email)
        {
            IQueryable<ProgramUser> puList = Resolve<IProgramUserRepository>().GetProgramUserListByProgramGuid(programGuid);
            if (!string.IsNullOrEmpty(email))
            {
                puList = puList.Where(p => p.User.Email.Contains(email));
            }
            return puList.Count();
        }

        //public XmlDocument GetProgramReport(Guid programGuid)
        //{
        //    string xmlString = Resolve<IStoreProcedure>().GetProgramReportXML(programGuid);
        //    XmlDocument doc = new XmlDocument();
        //    doc.LoadXml(xmlString);
        //    return doc;
        //}

        public List<string> CheckExpressionForProgram(Guid programGuid)
        {
            //string result = string.Empty;
            List<string> resultlist = new List<string>();
            Program programEntity = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
            if (!programEntity.Session.IsLoaded)
            {
                programEntity.Session.Load();
            }

            string checkexpression = System.Configuration.ConfigurationManager.AppSettings["CheckExpression"];
            List<RelapsePageExpressionNodeModel> pageExpressionNodeList = null;
            if (checkexpression != null && checkexpression.ToUpper().Contains("LOOP,RELAPSE"))
            {
                List<string> relapsePageList = Resolve<IExpressionRepository>().GetRelapsePagesequenceFromProgram(programEntity.ProgramGUID);
                if (relapsePageList != null && relapsePageList.Count > 0)
                {
                    pageExpressionNodeList = Resolve<IExpressionService>().GetRelapsePages(relapsePageList);
                }
            }

            List<Session> sessionList = programEntity.Session.Where(s => !s.IsDeleted.HasValue || s.IsDeleted.HasValue && s.IsDeleted == false).OrderBy(p => p.Day).ToList();
            foreach (Session sessionEntity in sessionList)
            {
                resultlist.AddRange(Resolve<IExpressionService>().CheckExpressionForSession(sessionEntity, pageExpressionNodeList));
            }
            return resultlist;
        }

        public List<string> CheckProgramSetting(Guid programGuid)
        {
            List<string> resultlist = new List<string>();
            Program programentity = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
            if (!programentity.TipMessage.IsLoaded)
            {
                programentity.TipMessage.Load();
            }
            if (programentity.TipMessage.Count == 0)
            {
                resultlist.Add("No Tip message");
            }
            if (!programentity.ProgramSchedule.IsLoaded)
            {
                programentity.ProgramSchedule.Load();
            }
            if (programentity.ProgramSchedule.Count == 0)
            {
                resultlist.Add("No program schedule");
            }
            if (!programentity.UserMenu.IsLoaded)
            {
                programentity.UserMenu.Load();
            }
            if (programentity.UserMenu.Count == 0)
            {
                resultlist.Add("No user menu");
            }
            if (!programentity.EmailTemplate.IsLoaded)
            {
                programentity.EmailTemplate.Load();
            }
            if (programentity.EmailTemplate.Count == 0)
            {
                resultlist.Add("No Emial template");
            }
            if (!programentity.AccessoryTemplate.IsLoaded)
            {
                programentity.AccessoryTemplate.Load();
            }
            if (programentity.AccessoryTemplate.Where(p => p.Type == "Login").Count() == 0)
            {
                resultlist.Add("No login template");
            }
            if (programentity.AccessoryTemplate.Where(p => p.Type == "Password reminder").Count() == 0)
            {
                resultlist.Add("No password reminder template");
            }
            if (programentity.AccessoryTemplate.Where(p => p.Type == "Session ending").Count() == 0)
            {
                resultlist.Add("No session ending template");
            }

            return resultlist;
        }






        public void DeleteProgramScheduleByProgramAndWeek(Guid programGuid, int week)
        {
            Resolve<IProgramRepository>().DeleteProgramSchedule(programGuid, week);
        }

        public List<TranslationModel> GetTranslationData(Guid programGuid, int startDay, int endDay, bool includeProgramSettings)
        {
            Program programEntity = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
            if (!programEntity.LanguageReference.IsLoaded)
            {
                programEntity.LanguageReference.Load();
            }
            List<TranslationModel> models = new List<TranslationModel>();

            if (startDay > -1)
            {
                GetSessions(models, programGuid, startDay, endDay);
                GetPageSequence(models, programGuid, startDay, endDay);
                GetPageContent(models, programGuid, startDay, endDay);
                GetPageQuestionContent(models, programGuid, startDay, endDay);
                GetPageQuestionItemContent(models, programGuid, startDay, endDay);
                GetGraphContent(models, programGuid, startDay, endDay);
                GetGraphItemContent(models, programGuid, startDay, endDay);
                GetPreference(models, programGuid);
                GetRelapsePageSequence(models, programGuid);
            }

            if (includeProgramSettings)
            {
                GetProgramRoom(models, programGuid);
                GetProgramAccessory(models, programGuid);
                GetEmailTemplates(models, programGuid);
                GetHelpItems(models, programGuid);
                GetUserMenus(models, programGuid);
                GetTipMessage(models, programGuid);
                GetSpecialStrings(models, programEntity.Language.LanguageGUID);
            }

            return models;
        }

        public void ClearProgramByDefaultGUID(Guid programGuid)
        {
            IQueryable<Program> programs = Resolve<IProgramRepository>().GetProgramByDefaultGUID(programGuid);
            foreach (Program program in programs)
            {
                Resolve<IProgramService>().DeleteProgram(program.ProgramGUID);
                break;
            }
        }

        public ProgramReportModel GetProgramReportModel(Guid programGuid)
        {
            ProgramReportModel reportModel = new ProgramReportModel();
            reportModel.Sessions = new List<SessionReportModel>();
            Program program = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
            if (!program.LanguageReference.IsLoaded)
            {
                program.LanguageReference.Load();
            }
            reportModel.Name = program.Name;
            reportModel.Description = program.Description;
            reportModel.Langeuage = program.Language.Name;

            List<Session> sessionEntities = Resolve<ISessionRepository>().GetSessionByProgramGuid(programGuid).ToList();
            foreach (Session sessionentity in sessionEntities)
            {
                SessionReportModel sessionReportModel = new SessionReportModel();
                sessionReportModel.Name = sessionentity.Name;
                sessionReportModel.Description = sessionentity.Description;
                sessionReportModel.Order = sessionentity.Day.Value;
                List<PageSequence> pagesequenceEntities = Resolve<IPageSequenceRepository>().GetPageSequenceBySessionGuid(sessionentity.SessionGUID).ToList();
                sessionReportModel.PageSequences = new List<PageSequenceReportModel>();
                int i = 1;
                foreach (PageSequence ps in pagesequenceEntities)
                {
                    PageSequenceReportModel psrModel = new PageSequenceReportModel();
                    psrModel.Name = ps.Name;
                    psrModel.Description = ps.Description;
                    psrModel.Order = i++;
                    psrModel.Pages = new List<PageReportModel>();

                    List<Page> pageEntities = Resolve<IPageRepository>().GetPagesByPageSequenceGuid(ps.PageSequenceGUID);
                    foreach (Page page in pageEntities)
                    {
                        if (!page.PageContentReference.IsLoaded)
                        {
                            page.PageContentReference.Load();
                        }
                        if (!page.PageTemplateReference.IsLoaded)
                        {
                            page.PageTemplateReference.Load();
                        }
                        PageReportModel prModel = new PageReportModel();
                        prModel.Type = page.PageTemplate.Name;
                        prModel.Order = page.PageOrderNo;
                        prModel.Title = page.PageContent.Heading;
                        prModel.Text = page.PageContent.Body;
                        prModel.FooterText = page.PageContent.FooterText;
                        prModel.ButtonCaption = page.PageContent.PrimaryButtonCaption;
                        prModel.AfterShowExpression = page.PageContent.AfterShowExpression;
                        prModel.BeforeShowExpression = page.PageContent.BeforeShowExpression;

                        psrModel.Pages.Add(prModel);
                    }

                    sessionReportModel.PageSequences.Add(psrModel);
                }

                reportModel.Sessions.Add(sessionReportModel);
            }

            return reportModel;
        }

        public int GetProgramCountByLanguageGuid(Guid languageGuid)
        {
            return Resolve<IProgramRepository>().GetProgramCountByLanguageGUID(languageGuid);
        }

        public void CopyProgramPrimaryButtonColorFromLayoutSetting(Guid programGuid)
        {
            Program program = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
            LayoutSetting layout = Resolve<IProgramRepository>().GetLayoutSettingByProgramGUID(programGuid);
            string xmlSetting = GetProgramSetting(programGuid);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(layout.SettingXML);
            foreach (XmlNode node in doc.FirstChild.ChildNodes)
            {
                switch (node.Attributes["Name"].Value)
                {
                    case "PrimaryButtonColorNormal":
                        program.PrimaryButtonColorNormal = node.Attributes["Value"].Value;
                        break;
                    case "PrimaryButtonColorOver":
                        program.PrimaryButtonColorOver = node.Attributes["Value"].Value;
                        break;
                    case "PrimaryButtonColorDown":
                        program.PrimaryButtonColorDown = node.Attributes["Value"].Value;
                        break;
                    case "PrimaryButtonColorDisable":
                        program.PrimaryButtonColorDisable = node.Attributes["Value"].Value;
                        break;
                }
            }

            Resolve<IProgramRepository>().Update(program);
        }

        public bool IsProgramNeedPinCode(Guid programGuid)
        {
            bool flug = false;
            Program program = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
            if (program != null)
            {
                if (program.IsNeedPinCode.HasValue && program.IsNeedPinCode.Value == true)
                {
                    flug = true;
                }
            }

            return flug;
        }

        public bool IsProgramNeedPay(Guid programGuid)
        {
            bool flug = false;
            Program program = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
            if (program != null)
            {
                if (program.IsWithPay.HasValue && program.IsWithPay.Value == true)
                {
                    flug = true;
                }
            }

            return flug;
        }

        public bool IsProgramCTPPEnable(Guid programGuid)
        {
            bool flug = false;
            Program program = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
            if (program != null)
            {
                if (program.IsCTPPEnable.HasValue && program.IsCTPPEnable.Value == true)
                {
                    flug = true;
                }
            }

            return flug;
        }

        public ProgramPropertyModel GetProgramProperty(Guid programGuid)
        {
            ProgramPropertyModel model = new ProgramPropertyModel();
            Program program = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
            model.IsNeedPayment = program.IsWithPay.HasValue ? program.IsWithPay.Value : false;
            model.IsNeedPinCode = program.IsNeedPinCode.HasValue ? program.IsNeedPinCode.Value : false;
            model.Price = program.Price.HasValue ? program.Price.Value.ToString() : "";
            model.Weeks = program.CutConnectWeek.HasValue ? program.CutConnectWeek.Value.ToString() : "";
            model.IsNeedCutConnect = program.NeedCutConnect.HasValue ? program.NeedCutConnect.Value : false;
            model.IsContainTwoParts = program.IsContainTwoParts.HasValue ? program.IsContainTwoParts.Value : false;
            model.SwitchDay = program.SwitchDay.HasValue ? program.SwitchDay.Value : 0;
            model.IsSupportHttps = program.IsSupportHttps.HasValue ? program.IsSupportHttps.Value : false;
            model.SeparateGender = program.SeparateGender.HasValue ? program.SeparateGender.Value : false;
            model.ProgramCode = program.Code;
            model.IsNeedSerialNumber = program.IsNeedSerialNumber.HasValue ? program.IsNeedSerialNumber.Value : false;
            model.SupportEmail = program.SupportEmail;
            model.SupportName = program.SupportName;
            model.IsCTPPEnable = program.IsCTPPEnable.HasValue ? program.IsCTPPEnable.Value : false;
            model.IsHTML5PreviewEnable = program.IsHTML5Enable.HasValue ? program.IsHTML5Enable.Value : false;
            model.FlashOrHTML5 = program.UseFlashOrOther.HasValue ? program.UseFlashOrOther.Value : (int)FlashOrHtml5Enum.FlashOnly;
            model.IsNoCatchUp = program.IsNoCatchUp.HasValue ? program.IsNoCatchUp.Value : false;
            model.IsOrderProgram = program.IsOrderProgram.HasValue ? program.IsOrderProgram.Value : false;
            model.IsHPOrderProgram = program.IsHPOrderProgram.HasValue ? program.IsHPOrderProgram.Value : false;
            model.IsInvisibleStartButton = program.IsNotShowStartButton.HasValue ? program.IsNotShowStartButton.Value : false;
            model.IsInvisibleDayAndSetMenu = program.IsNotShowDayAndSetMenu.HasValue ? program.IsNotShowDayAndSetMenu.Value : false;
            model.IsSupportTimeZone = program.IsSupportTimeZone.HasValue ? program.IsSupportTimeZone.Value : false;
            model.TimeZone = program.TimeZone.HasValue ? program.TimeZone.Value : 0;
            model.EnableHTML5NewUI = program.EnableHtml5NewUI.HasValue ? program.EnableHtml5NewUI.Value : false;
            model.NameForDeveloper = !string.IsNullOrEmpty(program.NameByDeveloper) ? program.NameByDeveloper : string.Empty;
            model.OrderProgramText = !string.IsNullOrEmpty(program.OrderProgramText) ? program.OrderProgramText : string.Empty;
            model.ShortName = !string.IsNullOrEmpty(program.ShortName) ? program.ShortName: string.Empty;
            if (!program.ResourceReference.IsLoaded)
            {
                program.ResourceReference.Load();
            }
            if (program.Resource != null)
            {
                model.ProgramLogo = new ResourceModel
                {
                    Extension = program.Resource.FileExtension,
                    ID = program.Resource.ResourceGUID,
                    Name = program.Resource.Name,
                    NameOnServer = program.Resource.NameOnServer,
                    Type = program.Resource.Type,
                };
            }
            return model;
        }

        public void UpdateProgramProperty(ProgramPropertyModel model)
        {
            Program programentity = Resolve<IProgramRepository>().GetProgramByGuid(model.ProgramGUID);
            programentity.IsNeedPinCode = model.IsNeedPinCode;
            programentity.IsWithPay = model.IsNeedPayment;
            programentity.NeedCutConnect = model.IsNeedCutConnect;
            programentity.IsContainTwoParts = model.IsContainTwoParts;
            programentity.IsSupportHttps = model.IsSupportHttps;
            programentity.SeparateGender = model.SeparateGender;
            programentity.IsNeedSerialNumber = model.IsNeedSerialNumber;
            programentity.SupportName = model.SupportName;
            programentity.SupportEmail = model.SupportEmail;
            programentity.UseFlashOrOther = model.FlashOrHTML5;
            programentity.IsNoCatchUp = model.IsNoCatchUp;
            programentity.IsOrderProgram = model.IsOrderProgram;
            programentity.IsHPOrderProgram = model.IsHPOrderProgram;
            programentity.IsNotShowStartButton = model.IsInvisibleStartButton;
            programentity.IsNotShowDayAndSetMenu = model.IsInvisibleDayAndSetMenu;
            //----timezone
            programentity.IsSupportTimeZone = model.IsSupportTimeZone;
            programentity.TimeZone = model.TimeZone;
            
            if (!string.IsNullOrEmpty(model.Weeks))
            {
                programentity.CutConnectWeek = Convert.ToInt32(model.Weeks);
            }
            if (!string.IsNullOrEmpty(model.Price))
            {
                programentity.Price = Convert.ToInt32(model.Price);
            }
            programentity.SwitchDay = model.SwitchDay;
            programentity.IsCTPPEnable = model.IsCTPPEnable;
            programentity.IsHTML5Enable = model.IsHTML5PreviewEnable;
            programentity.EnableHtml5NewUI = model.EnableHTML5NewUI;
            programentity.OrderProgramText = model.OrderProgramText;
            programentity.ShortName = model.ShortName;
            if (model.ProgramLogo.Name != "")
            {
                programentity.Resource = new Resource
                {
                    ResourceGUID = model.ProgramLogo.ID,
                    Name = model.ProgramLogo.Name,
                    Type = model.ProgramLogo.Type,
                    FileExtension = model.ProgramLogo.Extension,
                    NameOnServer = model.ProgramLogo.ID + model.ProgramLogo.Extension,
                    ResourceCategory = Resolve<IResourceCategoryRepository>().GetResourceCategory("Default"),
                };
            }
            Resolve<IProgramRepository>().Update(programentity);
        }

        public List<SimpleProgramModel> GetSimpleProgramUserHasPermission(Guid userGuid)
        {
            IQueryable<ProgramUser> programUserEntityList = Resolve<IProgramUserRepository>().GetProgramUserListByUserGuid(userGuid);
            List<ProgramUser> programUserList = programUserEntityList.Where(p => !p.Program.DefaultGUID.HasValue).ToList();
            List<SimpleProgramModel> models = new List<SimpleProgramModel>();
            foreach (ProgramUser pu in programUserList)
            {
                if (!pu.ProgramReference.IsLoaded)
                {
                    pu.ProgramReference.Load();
                }
                SimpleProgramModel model = new SimpleProgramModel
                {
                    ProgramGuid = pu.Program.ProgramGUID,
                    ProgramName = pu.Program.Name
                };
                models.Add(model);
            }

            return models;
        }

        public bool IsValidShortName(Guid programGuid, string shortName)
        {
            bool flag = true;
            if (!string.IsNullOrEmpty(shortName))
            {
                List<Program> programList = Resolve<IProgramRepository>().GetProgramsByShortName(shortName).ToList();
                foreach (Program programEntity in programList)
                {
                    if (programEntity.ProgramGUID != programGuid)
                    {
                        flag = false;
                    }
                }
            }
            return flag;
        }

        public void SetProgramCodeForProgram(Guid programGuid)
        {
            Program programEntity = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
            programEntity.Code = GenerateProgramCode();

            Resolve<IProgramRepository>().Update(programEntity);
        }

        public Guid GetProgramGUIDByProgramCode(string code)
        {
            Program programEntity = Resolve<IProgramRepository>().GetProgramByProgramCode(code).FirstOrDefault();
            return programEntity.ProgramGUID;
        }

        public bool IsProgramSeparateGender(Guid programGuid)
        {
            Program programEntity = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
            return programEntity.SeparateGender.HasValue ? programEntity.SeparateGender.Value : false;
        }

        public bool IsSupportHttps(Guid programGuid)
        {
            Program programEntity = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
            return programEntity.IsSupportHttps.HasValue ? programEntity.IsSupportHttps.Value : false;
        }

        public string GenerateProgramCode()
        {
            string code = string.Empty;
            do
            {
                code = Ethos.Utility.StringUtility.GenerateCheckCode(6);
            }
            while (IsProgramCodeExisted(code));

            return code;
        }

        public int GetAllNumberOfProgram(string keyword)
        {
            IQueryable<Program> programList = Resolve<IProgramRepository>().GetAllPrograms();
            if (!string.IsNullOrEmpty(keyword))
            {
                programList = programList.Where(p => p.Name.Contains(keyword));
            }

            return programList.Count();
        }

        public List<SimpleProgramModel> GetSimplePrograms(int pageNumber, int pageSize, string keyword)
        {
            IQueryable<Program> entityList = Resolve<IProgramRepository>().GetAllPrograms();
            if (!string.IsNullOrEmpty(keyword))
            {
                entityList = entityList.Where(p => p.Name.Contains(keyword));
            }
            List<Program> programList = entityList.OrderBy(p => p.Name).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            List<SimpleProgramModel> modelList = new List<SimpleProgramModel>();

            foreach (Program programEntity in programList)
            {
                SimpleProgramModel model = new SimpleProgramModel
                {
                    Description = programEntity.Description,
                    ProgramGuid = programEntity.ProgramGUID,
                    ProgramName = programEntity.Name
                };
                modelList.Add(model);
            }

            return modelList;
        }

        public int? GetProgramDailySMSTime(Guid programGuid)
        {
            int? dailySMSTime = null;
            Program programEntity = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
            if (programEntity != null)
            {
                dailySMSTime = programEntity.DailySMSTime;
            }
            return dailySMSTime;
        }

        public void UpdateProgramDailySMSTime(Guid programGuid, int? dailySMSTime)
        {
            Program programEntity = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
            if (programEntity != null)
            {
                programEntity.DailySMSTime = dailySMSTime;
            }
            Resolve<IProgramRepository>().Update(programEntity);
        }

        #region the services provider for ctpp
        public List<CTPPSessionPageBodyModel> GetAllPageBodyList(Guid ProgramGUID)
        {
            List<CTPPSessionPageBodyModel> ctppSessionPageBodyList = new List<CTPPSessionPageBodyModel>();
            List<SessionPageBody> sessionPageBodyList = Resolve<IStoreProcedure>().GetAllPageBodyList(ProgramGUID);
            foreach (SessionPageBody sPageBody in sessionPageBodyList)
            {
                CTPPSessionPageBodyModel model = new CTPPSessionPageBodyModel();
                model.SessionGUID = sPageBody.SessionGUID;
                model.PageBody = sPageBody.PageBody;
                ctppSessionPageBodyList.Add(model);
            }
            return ctppSessionPageBodyList;
        }

        public List<CTPPSessionPageMediaResourceModel> GetAllPageMediaResourceList(Guid ProgramGUID)
        {
            List<CTPPSessionPageMediaResourceModel> sPageMediaResourceModelList = new List<CTPPSessionPageMediaResourceModel>();
            List<SessionPageMediaResource> sPageMedalResourceList = Resolve<IStoreProcedure>().GetAllPageMediaResource(ProgramGUID);
            foreach (SessionPageMediaResource sPageMedalResource in sPageMedalResourceList)
            {
                CTPPSessionPageMediaResourceModel model = new CTPPSessionPageMediaResourceModel();
                model.SessionGUID = sPageMedalResource.SessionGUID;
                model.MediaGUID = sPageMedalResource.MediaGUID;
                model.Name = sPageMedalResource.Name;
                model.NameOnServer = sPageMedalResource.NameOnServer;
                model.Type = sPageMedalResource.Type;
                sPageMediaResourceModelList.Add(model);
            }
            return sPageMediaResourceModelList;
        }
        #endregion

        #endregion

        #region private methods

        private bool IsProgramCodeExisted(string code)
        {
            bool flag = false;
            if (Resolve<IProgramRepository>().GetProgramByProgramCode(code).Count() > 0)
            {
                flag = true;
            }
            return flag;
        }

        //TODO: there is something wrong , the logic is strange.
        private void CheckTipMessage(Guid programGuid)
        {
            List<TipMessage> tipMessagesOfDefaultLanguage = Resolve<ITipMessageRepository>().GetTipMessageByProgram(programGuid).ToList();

            foreach (TipMessage tipMessage in tipMessagesOfDefaultLanguage)
            {
                if (!tipMessage.TipMessageTypeReference.IsLoaded)
                {
                    tipMessage.TipMessageTypeReference.Load();
                }

                TipMessage tipMessageOfNewLanguage = Resolve<ITipMessageRepository>().GetTipMessage(tipMessage.TipMessageType.TipMessageTypeGUID, programGuid);
                if (tipMessageOfNewLanguage == null)
                {
                    tipMessageOfNewLanguage = new TipMessage();
                    tipMessageOfNewLanguage.BackButtonName = tipMessage.BackButtonName;
                    tipMessageOfNewLanguage.Program = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
                    tipMessageOfNewLanguage.Message = tipMessage.Message;
                    tipMessageOfNewLanguage.TipMessageGUID = Guid.NewGuid();
                    tipMessageOfNewLanguage.ParentTipMessageGUID = tipMessage.TipMessageGUID;
                    if (!tipMessage.TipMessageTypeReference.IsLoaded)
                    {
                        tipMessage.TipMessageTypeReference.Load();
                    }
                    tipMessageOfNewLanguage.TipMessageType = tipMessage.TipMessageType;
                    tipMessageOfNewLanguage.Title = tipMessage.Title;
                    Resolve<ITipMessageRepository>().Insert(tipMessageOfNewLanguage);
                }
            }
        }

        private void CheckSpecialString(Guid languageGuid)
        {
            List<SpecialString> specialStrings = Resolve<ISpecialStringRepository>().GetSpecialStringListOfLanguage(new Guid("fb5ae1dc-4caf-4613-9739-7397429ddf25")).ToList();
            foreach (SpecialString specialString in specialStrings)
            {
                SpecialString specialStringOfNewLanguage = Resolve<ISpecialStringRepository>().GetSpecialString(languageGuid, specialString.Name);
                if (specialStringOfNewLanguage == null)
                {
                    specialStringOfNewLanguage = new SpecialString();
                    specialStringOfNewLanguage.Language = Resolve<ILanguageRepository>().GetLanguage(languageGuid);
                    specialStringOfNewLanguage.Name = specialString.Name;
                    specialStringOfNewLanguage.Value = specialString.Value;
                    Resolve<ISpecialStringRepository>().AddSpecialString(specialStringOfNewLanguage);
                }
            }
        }

        private void GetSpecialStrings(List<TranslationModel> models, Guid guid)
        {
            List<SpecialString> strings = Resolve<ISpecialStringRepository>().GetSpecialStringListOfLanguage(guid).ToList();
            foreach (SpecialString str in strings)
            {
                if (!string.IsNullOrEmpty(str.Value))
                {
                    models.Add(new TranslationModel
                    {
                        ID = str.Name,
                        Object = "SpecialString",
                        Text = str.Value,
                        Type = "Value",
                        MaxLength = "50"
                    });
                }
            }
        }

        private void GetTipMessage(List<TranslationModel> models, Guid guid)
        {
            List<TipMessage> tipmessages = Resolve<ITipMessageRepository>().GetTipMessageByProgram(guid).ToList();
            foreach (TipMessage message in tipmessages)
            {
                if (!string.IsNullOrEmpty(message.Title))
                {
                    models.Add(new TranslationModel
                    {
                        ID = message.TipMessageGUID.ToString(),
                        Object = "TipMessage",
                        Text = message.Title,
                        Type = "Title",
                        MaxLength = "500"
                    });
                }
                if (!string.IsNullOrEmpty(message.Message))
                {
                    models.Add(new TranslationModel
                    {
                        ID = message.TipMessageGUID.ToString(),
                        Object = "TipMessage",
                        Text = message.Message,
                        Type = "Message"
                    });
                }
                if (!string.IsNullOrEmpty(message.BackButtonName))
                {
                    models.Add(new TranslationModel
                    {
                        ID = message.TipMessageGUID.ToString(),
                        Object = "TipMessage",
                        Text = message.BackButtonName,
                        Type = "BackButtonName",
                        MaxLength = "50"
                    });
                }
            }
        }

        private void GetPreference(List<TranslationModel> models, Guid programGuid)
        {
            List<Preferences> preferences = Resolve<IPreferencesRepository>().GetPreferenceByProgramGuid(programGuid).ToList();
            foreach (Preferences preference in preferences)
            {
                if (!string.IsNullOrEmpty(preference.Name))
                {
                    TranslationModel model = new TranslationModel();
                    model.ID = preference.PreferencesGUID.ToString();
                    model.Object = "Preferences";
                    model.Text = preference.Name;
                    model.Type = "Name";
                    model.MaxLength = "50";
                    if (!preference.PageReference.IsLoaded)
                    {
                        preference.PageReference.Load();
                    }
                    model.ParentGUID = preference.Page.PageGUID;
                    models.Add(model);
                }
                if (!string.IsNullOrEmpty(preference.Description))
                {
                    TranslationModel model = new TranslationModel();
                    model.ID = preference.PreferencesGUID.ToString();
                    model.Object = "Preferences";
                    model.Text = preference.Description;
                    model.Type = "Description";
                    model.MaxLength = "200";
                    if (!preference.PageReference.IsLoaded)
                    {
                        preference.PageReference.Load();
                    }
                    model.ParentGUID = preference.Page.PageGUID;
                    models.Add(model);
                }
                if (!string.IsNullOrEmpty(preference.AnswerText))
                {
                    TranslationModel model = new TranslationModel();
                    model.ID = preference.PreferencesGUID.ToString();
                    model.Object = "Preferences";
                    model.Text = preference.AnswerText;
                    model.Type = "AnswerText";
                    model.MaxLength = "200";
                    if (!preference.PageReference.IsLoaded)
                    {
                        preference.PageReference.Load();
                    }
                    model.ParentGUID = preference.Page.PageGUID;
                    models.Add(model);
                }
                if (!string.IsNullOrEmpty(preference.ButtonName))
                {
                    TranslationModel model = new TranslationModel();
                    model.ID = preference.PreferencesGUID.ToString();
                    model.Object = "Preferences";
                    model.Text = preference.ButtonName;
                    model.Type = "ButtonName";
                    model.MaxLength = "200";
                    if (!preference.PageReference.IsLoaded)
                    {
                        preference.PageReference.Load();
                    }
                    model.ParentGUID = preference.Page.PageGUID;
                    models.Add(model);
                }
            }
        }

        private void GetSessions(List<TranslationModel> models, Guid programGuid, int startDay, int endDay)
        {
            List<Session> sessions = Resolve<ISessionRepository>().GetSessionByProgramGuid(programGuid).Where(s => s.Day.Value >= startDay && s.Day.Value <= endDay).ToList();
            foreach (Session session in sessions)
            {
                //if(session.Day >= startDay &&
                //    session.Day <= endDay)
                //{
                models.Add(new TranslationModel
                {
                    ID = session.SessionGUID.ToString(),
                    Object = "Session",
                    Text = session.Name,
                    Type = "Name",
                    MaxLength = "500"
                });
                models.Add(new TranslationModel
                {
                    ID = session.SessionGUID.ToString(),
                    Object = "Session",
                    Text = session.Description,
                    Type = "Description",
                    MaxLength = "1000"
                });
                //}
            }
        }

        private void GetUserMenus(List<TranslationModel> models, Guid programGuid)
        {
            List<UserMenu> menus = Resolve<IUserMenuRepository>().GetUserMenuOfProgram(programGuid).ToList();
            foreach (UserMenu menu in menus)
            {
                if (!string.IsNullOrEmpty(menu.Name))
                {
                    models.Add(new TranslationModel
                    {
                        ID = menu.MenuItemGUID.ToString(),
                        Object = "UserMenu",
                        Text = menu.Name,
                        Type = "Name",
                        MaxLength = "50"
                    });
                }
                if (!string.IsNullOrEmpty(menu.MenuText))
                {
                    models.Add(new TranslationModel
                    {
                        ID = menu.MenuItemGUID.ToString(),
                        Object = "UserMenu",
                        Text = menu.MenuText,
                        Type = "MenuText",
                        MaxLength = "250"
                    });
                }
                if (!string.IsNullOrEmpty(menu.MenuFormTitle))
                {
                    models.Add(new TranslationModel
                    {
                        ID = menu.MenuItemGUID.ToString(),
                        Object = "UserMenu",
                        Text = menu.MenuFormTitle,
                        Type = "MenuFormTitle",
                        MaxLength = "1024"
                    });
                }
                if (!string.IsNullOrEmpty(menu.MenuFormText))
                {
                    models.Add(new TranslationModel
                    {
                        ID = menu.MenuItemGUID.ToString(),
                        Object = "UserMenu",
                        Text = menu.MenuFormText,
                        Type = "MenuFormText"
                    });
                }
                if (!string.IsNullOrEmpty(menu.MenuFormBackButtonName))
                {
                    models.Add(new TranslationModel
                    {
                        ID = menu.MenuItemGUID.ToString(),
                        Object = "UserMenu",
                        Text = menu.MenuFormBackButtonName,
                        Type = "MenuFormBackButtonName",
                        MaxLength = "250"
                    });
                }
                if (!string.IsNullOrEmpty(menu.MenuFormSubmitButtonName))
                {
                    models.Add(new TranslationModel
                    {
                        ID = menu.MenuItemGUID.ToString(),
                        Object = "UserMenu",
                        Text = menu.MenuFormSubmitButtonName,
                        Type = "MenuFormSubmitButtonName",
                        MaxLength = "250"
                    });
                }
            }
        }

        private void GetHelpItems(List<TranslationModel> models, Guid programGuid)
        {
            List<HelpItem> items = Resolve<IHelpItemRepository>().GetItemByProgram(programGuid).ToList();
            foreach (HelpItem item in items)
            {
                if (!string.IsNullOrEmpty(item.Question))
                {
                    models.Add(new TranslationModel
                    {
                        ID = item.HelpItemGUID.ToString(),
                        Object = "HelpItem",
                        Text = item.Question,
                        Type = "Question"
                    });
                }
                if (!string.IsNullOrEmpty(item.Answer))
                {
                    models.Add(new TranslationModel
                    {
                        ID = item.HelpItemGUID.ToString(),
                        Object = "HelpItem",
                        Text = item.Answer,
                        Type = "Answer"
                    });
                }
            }
        }

        private void GetEmailTemplates(List<TranslationModel> models, Guid programGuid)
        {
            List<EmailTemplate> emailTemplates = Resolve<IEmailTemplateRepository>().GetEmailTemplateList(programGuid).ToList();
            foreach (EmailTemplate emailTemplate in emailTemplates)
            {
                if (!string.IsNullOrEmpty(emailTemplate.Body))
                {
                    models.Add(new TranslationModel
                    {
                        ID = emailTemplate.EmailTemplateGUID.ToString(),
                        Object = "EmailTemplate",
                        Text = emailTemplate.Body,
                        Type = "Body"
                    });
                }
                if (!string.IsNullOrEmpty(emailTemplate.Name))
                {
                    models.Add(new TranslationModel
                    {
                        ID = emailTemplate.EmailTemplateGUID.ToString(),
                        Object = "EmailTemplate",
                        Text = emailTemplate.Name,
                        Type = "Name",
                        MaxLength = "50"
                    });
                }
                if (!string.IsNullOrEmpty(emailTemplate.Subject))
                {
                    models.Add(new TranslationModel
                    {
                        ID = emailTemplate.EmailTemplateGUID.ToString(),
                        Object = "EmailTemplate",
                        Text = emailTemplate.Subject,
                        Type = "Subject",
                        MaxLength = "300"
                    });
                }
            }
        }

        private void GetProgramAccessory(List<TranslationModel> models, Guid programGuid)
        {
            List<AccessoryTemplate> accessorys = Resolve<IProgramAccessoryRepository>().GetAccessoryList(programGuid).ToList();
            foreach (AccessoryTemplate accessory in accessorys)
            {
                if (!string.IsNullOrEmpty(accessory.Heading))
                {
                    models.Add(new TranslationModel
                    {
                        ID = accessory.AccessoryTemplateGUID.ToString(),
                        Object = "AccessoryTemplate",
                        Text = accessory.Heading,
                        Type = "Heading",
                        MaxLength = "500"
                    });
                }
                if (!string.IsNullOrEmpty(accessory.PasswordText))
                {
                    models.Add(new TranslationModel
                    {
                        ID = accessory.AccessoryTemplateGUID.ToString(),
                        Object = "AccessoryTemplate",
                        Text = accessory.PasswordText,
                        Type = "PasswordText",
                        MaxLength = "100"
                    });
                }
                if (!string.IsNullOrEmpty(accessory.PrimaryButtonText))
                {
                    models.Add(new TranslationModel
                    {
                        ID = accessory.AccessoryTemplateGUID.ToString(),
                        Object = "AccessoryTemplate",
                        Text = accessory.PrimaryButtonText,
                        Type = "PrimaryButtonText",
                        MaxLength = "200"
                    });
                }
                if (!string.IsNullOrEmpty(accessory.SecondaryButtonText))
                {
                    models.Add(new TranslationModel
                    {
                        ID = accessory.AccessoryTemplateGUID.ToString(),
                        Object = "AccessoryTemplate",
                        Text = accessory.SecondaryButtonText,
                        Type = "SecondaryButtonText",
                        MaxLength = "200"
                    });
                }
                if (!string.IsNullOrEmpty(accessory.Text))
                {
                    models.Add(new TranslationModel
                    {
                        ID = accessory.AccessoryTemplateGUID.ToString(),
                        Object = "AccessoryTemplate",
                        Text = accessory.Text,
                        Type = "Text"
                    });
                }
                if (!string.IsNullOrEmpty(accessory.UserNameText))
                {
                    models.Add(new TranslationModel
                    {
                        ID = accessory.AccessoryTemplateGUID.ToString(),
                        Object = "AccessoryTemplate",
                        Text = accessory.UserNameText,
                        Type = "UserNameText",
                        MaxLength = "100"
                    });
                }
            }
        }

        private void GetProgramRoom(List<TranslationModel> models, Guid programGuid)
        {
            List<ProgramRoom> prooms = Resolve<IProgramRoomRepository>().GetRoomsByProgram(programGuid).ToList();
            foreach (ProgramRoom room in prooms)
            {
                if (!string.IsNullOrEmpty(room.Name))
                {
                    models.Add(new TranslationModel
                    {
                        ID = room.ProgramRoomGUID.ToString(),
                        Object = "ProgramRoom",
                        Text = room.Name,
                        Type = "Name",
                        MaxLength = "50"
                    });
                }
                if (!string.IsNullOrEmpty(room.Description))
                {
                    models.Add(new TranslationModel
                    {
                        ID = room.ProgramRoomGUID.ToString(),
                        Object = "ProgramRoom",
                        Text = room.Description,
                        Type = "Description",
                        MaxLength = "1024"
                    });
                }
            }
        }

        private void GetGraphItemContent(List<TranslationModel> models, Guid programGuid, int startDay, int endDay)
        {
            List<GraphItemContent> giContents = Resolve<IGraphItemContentRepository>().GetByProgram(programGuid, startDay, endDay).ToList();
            foreach (GraphItemContent giContent in giContents)
            {
                if (!string.IsNullOrEmpty(giContent.Name))
                {
                    TranslationModel model = new TranslationModel();
                    model.ID = giContent.GraphItemGUID.ToString();
                    model.Object = "GraphItemContent";
                    model.Text = giContent.Name;
                    model.Type = "Name";
                    model.MaxLength = "200";
                    if (!giContent.GraphItemReference.IsLoaded)
                    {
                        giContent.GraphItemReference.Load();
                    }
                    if (!giContent.GraphItem.GraphReference.IsLoaded)
                    {
                        giContent.GraphItem.GraphReference.Load();
                    }
                    model.ParentGUID = giContent.GraphItem.Graph.GraphGUID;
                    models.Add(model);
                }
            }
        }

        private void GetGraphContent(List<TranslationModel> models, Guid programGuid, int startDay, int endDay)
        {
            List<GraphContent> graphcontents = Resolve<IGraphContentRepository>().GetGraphsByProgram(programGuid, startDay, endDay).ToList();
            foreach (GraphContent gcontent in graphcontents)
            {
                if (!string.IsNullOrEmpty(gcontent.Caption))
                {
                    TranslationModel model = new TranslationModel();
                    model.ID = gcontent.GraphGUID.ToString();
                    model.Object = "GraphContent";
                    model.Text = gcontent.Caption;
                    model.Type = "Caption";
                    model.MaxLength = "200";
                    if (!gcontent.GraphReference.IsLoaded)
                    {
                        gcontent.GraphReference.Load();
                    }
                    if (!gcontent.Graph.PageReference.IsLoaded)
                    {
                        gcontent.Graph.PageReference.Load();
                    }
                    model.ParentGUID = gcontent.Graph.Page.PageGUID;
                    models.Add(model);
                }
            }
        }

        private void GetPageQuestionItemContent(List<TranslationModel> models, Guid programGuid, int startDay, int endDay)
        {
            List<PageQuestionItemContent> pqicontents = Resolve<IPageQuestionItemContentRepository>().GetPageQuestionItemContentByProgram(programGuid, startDay, endDay).ToList();
            foreach (PageQuestionItemContent pqicontent in pqicontents)
            {
                if (!string.IsNullOrEmpty(pqicontent.Item))
                {
                    TranslationModel model = new TranslationModel();
                    model.ID = pqicontent.PageQuestionItemGUID.ToString();
                    model.Object = "PageQuestionItemContent";
                    model.Text = pqicontent.Item;
                    model.Type = "Item";
                    model.MaxLength = "1024";
                    if (!pqicontent.PageQuestionItemReference.IsLoaded)
                    {
                        pqicontent.PageQuestionItemReference.Load();
                    }
                    if (!pqicontent.PageQuestionItem.PageQuestionReference.IsLoaded)
                    {
                        pqicontent.PageQuestionItem.PageQuestionReference.Load();
                    }
                    model.ParentGUID = pqicontent.PageQuestionItem.PageQuestion.PageQuestionGUID;
                    models.Add(model);
                }
                if (!string.IsNullOrEmpty(pqicontent.Feedback))
                {
                    TranslationModel model = new TranslationModel();
                    model.ID = pqicontent.PageQuestionItemGUID.ToString();
                    model.Object = "PageQuestionItemContent";
                    model.Text = pqicontent.Feedback;
                    model.Type = "Feedback";
                    model.MaxLength = "1024";
                    if (!pqicontent.PageQuestionItemReference.IsLoaded)
                    {
                        pqicontent.PageQuestionItemReference.Load();
                    }
                    if (!pqicontent.PageQuestionItem.PageQuestionReference.IsLoaded)
                    {
                        pqicontent.PageQuestionItem.PageQuestionReference.Load();
                    }
                    model.ParentGUID = pqicontent.PageQuestionItem.PageQuestion.PageQuestionGUID;
                    models.Add(model);
                }
            }
        }

        private void GetPageQuestionContent(List<TranslationModel> models, Guid programGuid, int startDay, int endDay)
        {
            List<PageQuestionContent> pqcontents = Resolve<IPageQuestionContentRepository>().GetPageQuestionContentByProgram(programGuid, startDay, endDay).ToList();
            foreach (PageQuestionContent pqcontent in pqcontents)
            {
                if (!string.IsNullOrEmpty(pqcontent.Caption))
                {
                    TranslationModel model = new TranslationModel();
                    model.ID = pqcontent.PageQuestionGUID.ToString();
                    model.Object = "PageQuestionContent";
                    model.Text = pqcontent.Caption;
                    model.Type = "Caption";
                    model.MaxLength = "1024";

                    if (!pqcontent.PageQuestionReference.IsLoaded)
                    {
                        pqcontent.PageQuestionReference.Load();
                    }
                    if (!pqcontent.PageQuestion.PageReference.IsLoaded)
                    {
                        pqcontent.PageQuestion.PageReference.Load();
                    }
                    model.ParentGUID = pqcontent.PageQuestion.Page.PageGUID;
                    models.Add(model);
                }
                if (!string.IsNullOrEmpty(pqcontent.DisableCheckBox))
                {
                    TranslationModel model = new TranslationModel();
                    model.ID = pqcontent.PageQuestionGUID.ToString();
                    model.Object = "PageQuestionContent";
                    model.Text = pqcontent.DisableCheckBox;
                    model.Type = "DisableCheckBox";
                    model.MaxLength = "250";

                    if (!pqcontent.PageQuestionReference.IsLoaded)
                    {
                        pqcontent.PageQuestionReference.Load();
                    }
                    if (!pqcontent.PageQuestion.PageReference.IsLoaded)
                    {
                        pqcontent.PageQuestion.PageReference.Load();
                    }
                    model.ParentGUID = pqcontent.PageQuestion.Page.PageGUID;
                    models.Add(model);
                }
            }
        }

        private void GetRelapsePageSequence(List<TranslationModel> models, Guid programGuid)
        {
            List<Relapse> relapsePageSequences = Resolve<IRelapseRepository>().GetRelapseList(programGuid).ToList();
            foreach (Relapse relapseEntity in relapsePageSequences)
            {
                if (!relapseEntity.PageSequenceReference.IsLoaded)
                {
                    relapseEntity.PageSequenceReference.Load();
                }
                if (!(relapseEntity.PageSequence.IsDeleted.HasValue && relapseEntity.PageSequence.IsDeleted.Value))
                {
                    TranslationModel model = new TranslationModel();
                    model.ID = relapseEntity.PageSequence.PageSequenceGUID.ToString();
                    model.Object = "Relapse";
                    model.Text = relapseEntity.PageSequence.Name;
                    model.Type = "Name";
                    //model.ParentGUID = sessionContent.Session.SessionGUID;
                    models.Add(model);

                    model = new TranslationModel();
                    model.ID = relapseEntity.PageSequence.PageSequenceGUID.ToString();
                    model.Object = "Relapse";
                    model.Text = relapseEntity.PageSequence.Description;
                    model.Type = "Description";
                    //model.ParentGUID = sessionContent.Session.SessionGUID;
                    models.Add(model);
                }
            }
        }

        private void GetPageSequence(List<TranslationModel> models, Guid programGuid, int startDay, int endDay)
        {
            List<SessionContent> sessionContents = Resolve<ISessionContentRepository>().GetSessionContentOfProgram(programGuid).Where(s => s.Session.Day >= startDay && s.Session.Day <= endDay).OrderBy(s => s.PageSequenceOrderNo).ToList();

            foreach (SessionContent sessionContent in sessionContents)
            {
                if (!sessionContent.PageSequenceReference.IsLoaded)
                {
                    sessionContent.PageSequenceReference.Load();
                }
                if (!(sessionContent.PageSequence.IsDeleted.HasValue && sessionContent.PageSequence.IsDeleted.Value))
                {
                    TranslationModel model = new TranslationModel();
                    model.ID = sessionContent.PageSequence.PageSequenceGUID.ToString();
                    model.Object = "PageSequence";
                    model.Text = sessionContent.PageSequence.Name;
                    model.Type = "Name";
                    model.ParentGUID = sessionContent.Session.SessionGUID;
                    models.Add(model);

                    model = new TranslationModel();
                    model.ID = sessionContent.PageSequence.PageSequenceGUID.ToString();
                    model.Object = "PageSequence";
                    model.Text = sessionContent.PageSequence.Description;
                    model.Type = "Description";
                    model.ParentGUID = sessionContent.Session.SessionGUID;
                    models.Add(model);
                }
            }
        }

        private void GetPageContent(List<TranslationModel> models, Guid programGuid, int startDay, int endDay)
        {
            List<PageContent> pagecontents = Resolve<IPageContentRepository>().GetByProgramGUID(programGuid, startDay, endDay).ToList();
            foreach (PageContent pagecontent in pagecontents)
            {
                if (!pagecontent.PageReference.IsLoaded)
                {
                    pagecontent.PageReference.Load();
                }
                //if (!string.IsNullOrEmpty(pagecontent.Heading))
                //{
                TranslationModel model = new TranslationModel();
                model.ID = pagecontent.PageGUID.ToString();
                model.Object = "PageContent";
                model.Text = pagecontent.Heading;
                model.Type = "Heading";
                if (!pagecontent.PageReference.IsLoaded)
                {
                    pagecontent.PageReference.Load();
                }
                if (!pagecontent.Page.PageSequenceReference.IsLoaded)
                {
                    pagecontent.Page.PageSequenceReference.Load();
                }
                model.ParentGUID = pagecontent.Page.PageSequence.PageSequenceGUID;
                models.Add(model);
                //}
                //if (!string.IsNullOrEmpty(pagecontent.Body))
                //{
                model = new TranslationModel();
                model.ID = pagecontent.PageGUID.ToString();
                model.Object = "PageContent";
                model.Text = pagecontent.Body;
                model.Type = "Body";
                if (!pagecontent.PageReference.IsLoaded)
                {
                    pagecontent.PageReference.Load();
                }
                if (!pagecontent.Page.PageSequenceReference.IsLoaded)
                {
                    pagecontent.Page.PageSequenceReference.Load();
                }
                model.ParentGUID = pagecontent.Page.PageSequence.PageSequenceGUID;
                models.Add(model);
                //}
                //if (!string.IsNullOrEmpty(pagecontent.FooterText))
                //{
                model = new TranslationModel();
                model.ID = pagecontent.PageGUID.ToString();
                model.Object = "PageContent";
                model.Text = pagecontent.FooterText;
                model.Type = "FooterText";
                if (!pagecontent.PageReference.IsLoaded)
                {
                    pagecontent.PageReference.Load();
                }
                if (!pagecontent.Page.PageSequenceReference.IsLoaded)
                {
                    pagecontent.Page.PageSequenceReference.Load();
                }
                model.ParentGUID = pagecontent.Page.PageSequence.PageSequenceGUID;
                models.Add(model);
                //}
                //if (!string.IsNullOrEmpty(pagecontent.PrimaryButtonCaption))
                //{
                model = new TranslationModel();
                model.ID = pagecontent.PageGUID.ToString();
                model.Object = "PageContent";
                model.Text = pagecontent.PrimaryButtonCaption;
                model.Type = "PrimaryButtonCaption";
                model.MaxLength = "80";
                if (!pagecontent.PageReference.IsLoaded)
                {
                    pagecontent.PageReference.Load();
                }
                if (!pagecontent.Page.PageSequenceReference.IsLoaded)
                {
                    pagecontent.Page.PageSequenceReference.Load();
                }
                model.ParentGUID = pagecontent.Page.PageSequence.PageSequenceGUID;
                models.Add(model);
                //}
            }
        }

        private List<ProgramModel> ConvertProgramListToProgramsModel(List<Program> programList)
        {
            List<ProgramModel> programsModel = new List<ProgramModel>();
            foreach (Program program in programList)
            {
                ProgramModel programModel = new ProgramModel();
                if (!string.IsNullOrEmpty(program.NameByDeveloper))
                {
                    programModel.ProgramName = program.NameByDeveloper;
                }
                else
                {
                    programModel.ProgramName = program.Name;
                }
                programModel.Description = program.Description;
                programModel.Guid = program.ProgramGUID;
                programModel.Created = program.Created.Value;
                programModel.LastUpdated = program.LastUpdated.HasValue ? program.LastUpdated.Value : DateTime.UtcNow;
                programModel.NumberOfUsers = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuid(program.ProgramGUID).Count();

                // for project manager
                if (!program.UserReference.IsLoaded)
                {
                    program.UserReference.Load();
                }
                if (program.User != null)
                {
                    programModel.ProjectManager = program.User.Email;
                }
                else
                {
                    programModel.ProjectManager = "Assign";
                }
                //In this ProgramModel have many property have not voluation.
                programsModel.Add(programModel);
            }

            return programsModel;
        }


        private ProgramsModel ConvertProgramsToProgramsModel(List<Program> list)
        {
            List<ProgramModel> listProgramModel = new List<ProgramModel>();
            ProgramsModel programsModel = null;

            List<Program> porgramList = list;

            foreach (Program program in porgramList)
            {
                ProgramModel programModel = new ProgramModel();
                if (!string.IsNullOrEmpty(program.NameByDeveloper))
                {
                    programModel.ProgramName = program.NameByDeveloper;
                }
                else
                {
                    programModel.ProgramName = program.Name;
                }
                programModel.Description = program.Description;
                programModel.Guid = program.ProgramGUID;
                programModel.Created = program.Created.Value;
                programModel.LastUpdated = program.LastUpdated.HasValue ? program.LastUpdated.Value : DateTime.UtcNow;
                programModel.NumberOfUsers = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuid(program.ProgramGUID).Count();

                // for project manager
                if (!program.UserReference.IsLoaded)
                {
                    program.UserReference.Load();
                }
                if (program.User != null)
                {
                    programModel.ProjectManager = program.User.Email;
                }
                else
                {
                    programModel.ProjectManager = "Assign";
                }
                //In this ProgramModel have many property have not voluation.

                listProgramModel.Add(programModel);
            }

            programsModel = new ProgramsModel(listProgramModel);

            return programsModel;
        }

        private void SetFullPermissionForProgram(Guid programGuid, Guid userGuid)
        {
            int fullPermission = (int)PermissionEnum.ProgramAdmin | (int)PermissionEnum.ProgramCreate | (int)PermissionEnum.ProgramDelete | (int)PermissionEnum.ProgramEdit | (int)PermissionEnum.ProgramView;
            DateTime setCurrentTimeByTimeZone = Resolve<IProgramUserService>().GetCurrentTimeByTimeZone(programGuid, userGuid, DateTime.UtcNow);
            ProgramUser pu = new ProgramUser();
            pu.ProgramUserGUID = Guid.NewGuid();
            pu.Security = fullPermission;
            pu.Program = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
            pu.User = Resolve<IUserRepository>().GetUserByGuid(userGuid);
            pu.MailTime = 0;
            pu.StartDate = setCurrentTimeByTimeZone; //DateTime.UtcNow;
            //ps.Language = Resolve<ILanguageRepository>().GetLanguage(languageGuid);
            pu.Status = ProgramUserStatusEnum.Registered.ToString();
            pu.Day = 0;
            pu.LastFinishDate = setCurrentTimeByTimeZone; //DateTime.UtcNow;
            pu.LastUpdatedBy = userGuid;
            Resolve<IProgramUserRepository>().Insert(pu);
        }

        public ProgramModel GetProgramByProgramGUIDAndLanguageGUID(Guid programGUID, Guid languageGUID)
        {
            ProgramModel programModel = new ProgramModel();

            Program programEntity = GetParentProgram(programGUID);
            //Resolve<IProgramRepository>().GetProgramByGuid(programGUID);
            if (!programEntity.LanguageReference.IsLoaded)
            {
                programEntity.LanguageReference.Load();
            }

            if (programEntity.Language.LanguageGUID != languageGUID)
            {
                programEntity = Resolve<IProgramRepository>().GetProgramByProgramDefaultGUIDAndLanguageGUID(programEntity.ProgramGUID, languageGUID);

                //if (relatedProgramEntity == null)
                //{
                //    programEntity = Resolve<IProgramRepository>().GetProgramByGuid(programEntity.ParentProgramGUID.Value);
                //}
                //else
                //{
                //    programEntity = relatedProgramEntity;
                //}
            }

            if (!programEntity.LanguageReference.IsLoaded)
            {
                programEntity.LanguageReference.Load();
            }

            programModel.Guid = programEntity.ProgramGUID;
            programModel.ProgramName = programEntity.Name;
            programModel.Description = programEntity.Description;
            programModel.DefaultLanguage = programEntity.Language.LanguageGUID;
            programModel.DefaultLanguageName = programEntity.Language.Name;
            programModel.Code = programEntity.Code;
            programModel.IsNoCatchUp = programEntity.IsNoCatchUp.HasValue ? programEntity.IsNoCatchUp.Value : false;
            programModel.IsOrderProgram = programEntity.IsOrderProgram.HasValue ? programEntity.IsOrderProgram.Value : false;
            programModel.IsHPOrderProgram = programEntity.IsHPOrderProgram.HasValue ? programEntity.IsHPOrderProgram.Value : false;
            programModel.IsSupportTimeZone = programEntity.IsSupportTimeZone.HasValue ? programEntity.IsSupportTimeZone.Value : false;
            programModel.TimeZone = programEntity.TimeZone.HasValue ? programEntity.TimeZone.Value : 0;
            //Note:---------------In this  many properties don't set value ------------------

            if (!programEntity.Session.IsLoaded)
            {
                programEntity.Session.Load();
            }
            programModel.DaysCount = programEntity.Session.Where(s => !(s.IsDeleted.HasValue && s.IsDeleted.Value)).Count();
            if (programModel.DaysCount > 0)
            {
                programModel.StartDay = (int)programEntity.Session.Where(s => !(s.IsDeleted.HasValue && s.IsDeleted.Value)).OrderBy(s => s.Day).FirstOrDefault().Day;
            }
            else
            {
                programModel.StartDay = int.MinValue;
            }
            return programModel;
        }

        private Program GetParentProgram(Guid programGUID)
        {
            Program programentity = Resolve<IProgramRepository>().GetProgramByGuid(programGUID);
            if (programentity.DefaultGUID.HasValue)
            {
                programentity = Resolve<IProgramRepository>().GetProgramByGuid(programentity.DefaultGUID.Value);
            }

            return programentity;
        }

        public List<SimpleProgramModel> GetHPOrderProgramsByLanguageGuid(Guid languageGuid)
        {
            List<SimpleProgramModel> programModels = new List<SimpleProgramModel>();
            List<Program> programListByLanguage = Resolve<IProgramRepository>().GetProgramsByLanguageGuid(languageGuid).Where(p => p.IsHPOrderProgram.HasValue && p.IsHPOrderProgram.Value == true).ToList();
            if (programListByLanguage.Count > 0)
            {
                SimpleProgramModel programModel = null;
                foreach (Program programEntity in programListByLanguage)
                {
                    programModel = new SimpleProgramModel();
                    ConvertProgramToSimpleProgramModel(programEntity, programModel);
                    programModels.Add(programModel);
                }
            }
            return programModels;
        }

        private void DeletePageSequenceOfProgram(EntityCollection<SessionContent> sesssionContentCollection)
        {
            foreach (SessionContent sessionContentEntity in sesssionContentCollection)
            {
                if (!sessionContentEntity.PageSequenceReference.IsLoaded)
                {
                    sessionContentEntity.PageSequenceReference.Load();
                }

                if (sessionContentEntity.PageSequence != null)
                {
                    Resolve<IPageSequenceService>().DeletePageSequence(sessionContentEntity.PageSequence.PageSequenceGUID);
                }
            }
        }

        private void CopyPageVariable(Program program, Program newProgram)
        {
            // Copy pagevariable
            newProgram = ServiceUtility.ClonePageVariableForProgram(program, newProgram);
            Resolve<IProgramRepository>().Update(newProgram);

            // Update pagevariable with the new one
            if (!program.PageVariable.IsLoaded)
            {
                program.PageVariable.Load();
            }
            foreach (ChangeTech.Entities.PageVariable variable in program.PageVariable)
            {
                Resolve<IStoreProcedure>().UpdatePageVariableAfterCopyProgram(program.ProgramGUID, newProgram.ProgramGUID, variable.Name, variable.PageVariableType);
            }
        }

        private void SetProgramLanguage(Program program, Program insertProgram)
        {
            if (!program.LanguageReference.IsLoaded)
            {
                program.LanguageReference.Load();
            }
            insertProgram.Language = program.Language;

            // Program language
            ProgramLanguage cloneProgramLanguage = new ProgramLanguage
            {
                ProgramLanguageGUID = Guid.NewGuid(),
                Language = program.Language
            };
            insertProgram.ProgramLanguage.Add(cloneProgramLanguage);
        }

        private void SetProgramLanguage(Program newProgram, Program originalProgram, Guid languageGuid)
        {
            Language languageEntity = Resolve<ILanguageRepository>().GetLanguage(languageGuid);
            newProgram.Language = languageEntity;

            ProgramLanguage programLanguageEntity = new ProgramLanguage
            {
                ProgramLanguageGUID = Guid.NewGuid(),
                Language = languageEntity
            };
            originalProgram.ProgramLanguage.Add(programLanguageEntity);
        }

        private void DisPlayContent(XmlNode xmlNode, StringBuilder sb, ref int rowcount)
        {
            if (xmlNode.HasChildNodes)
            {
                foreach (XmlNode node in xmlNode.ChildNodes)
                {
                    sb.AppendLine("<Row>");
                    if (node.Attributes["Order"] != null)
                    {
                        sb.AppendFormat("<Cell><Data ss:Type=\"String\">{0}</Data></Cell>", node.Name + node.Attributes["Order"].Value);
                    }
                    else
                    {
                        sb.AppendFormat("<Cell><Data ss:Type=\"String\">{0}</Data></Cell>", node.Name);
                    }
                    //sb.Append(GetEndTag(node.Name));
                    sb.AppendLine("</Row>");
                    rowcount++;

                    foreach (XmlAttribute attri in node.Attributes)
                    {
                        if (!attri.Name.Equals("Order"))
                        {
                            sb.AppendLine("<Row>");
                            sb.AppendFormat("<Cell><Data ss:Type=\"String\">{0}</Data></Cell>", attri.Name + ": " + node.Attributes[attri.Name].Value);
                            sb.AppendLine("</Row>");
                            rowcount++;
                        }
                    }
                    DisPlayContent(node, sb, ref rowcount);
                }
            }
        }

        public List<SimpleProgramModel> GetProgramsByLanguageGuid(Guid languageGuid)
        {
            List<SimpleProgramModel> programModels = new List<SimpleProgramModel>();
            List<Program> programListByLanguage = Resolve<IProgramRepository>().GetProgramsByLanguageGuid(languageGuid).ToList();
            if (programListByLanguage.Count>0)
            {
                SimpleProgramModel programModel = null;
                foreach (Program programEntity in programListByLanguage)
                {
                    programModel = new SimpleProgramModel();
                    ConvertProgramToSimpleProgramModel(programEntity, programModel);
                    programModels.Add(programModel);
                }
            }
            return programModels;
        }

        public List<SimpleProgramModel> GetOrderProgramsByLanguageGuid(Guid languageGuid)
        {
            List<SimpleProgramModel> programModels = new List<SimpleProgramModel>();
            List<Program> programListByLanguage = Resolve<IProgramRepository>().GetProgramsByLanguageGuid(languageGuid).Where(p=>p.IsOrderProgram.HasValue&&p.IsOrderProgram.Value==true).ToList();
            if (programListByLanguage.Count > 0)
            {
                SimpleProgramModel programModel = null;
                foreach (Program programEntity in programListByLanguage)
                {
                    programModel = new SimpleProgramModel();
                    ConvertProgramToSimpleProgramModel(programEntity, programModel);
                    programModels.Add(programModel);
                }
            }
            return programModels;
        }

        public List<SimpleProgramModel> GetOrderProgramsByLanguageGuidAndProgramPublishStatusGuid(Guid languageGuid)
        {
            List<SimpleProgramModel> programModels = new List<SimpleProgramModel>();
            //List<Program> programListByLanguage = Resolve<IProgramRepository>().GetProgramsByLanguageGuid(languageGuid).Where(p => p.ProgramStatus.ProgramStatusGUID == new Guid(PROGRAM_STATUS_PUBLISH) && (p.IsOrderProgram.HasValue && p.IsOrderProgram.Value == true)).ToList();
            List<Program> programListByLanguage = Resolve<IProgramRepository>().GetProgramsByLanguageGuid(languageGuid).Where(p => (p.IsOrderProgram.HasValue && p.IsOrderProgram.Value == true)).ToList();
            if (programListByLanguage.Count > 0)
            {
                SimpleProgramModel programModel = null;
                foreach (Program programEntity in programListByLanguage)
                {
                    programModel = new SimpleProgramModel();
                    ConvertProgramToSimpleProgramModel(programEntity, programModel);
                    programModels.Add(programModel);
                }
            }
            return programModels;
        }

        public List<SimpleProgramModel> GetSimpleProgramsByLanguageGuid(Guid languageGuid)
        {
            List<SimpleProgramModel> programModels = new List<SimpleProgramModel>();
            //program's status is publish.
            //List<Program> programListByLanguage = Resolve<IProgramRepository>().GetProgramsByLanguageGuid(languageGuid).Where(p => p.ProgramStatus.ProgramStatusGUID == new Guid(PROGRAM_STATUS_PUBLISH)).ToList();
            List<Program> programListByLanguage = Resolve<IProgramRepository>().GetProgramsByLanguageGuid(languageGuid).ToList();
            if (programListByLanguage.Count > 0)
            {
                SimpleProgramModel programModel = null;
                foreach (Program programEntity in programListByLanguage)
                {
                    programModel = new SimpleProgramModel();
                    ConvertProgramToSimpleProgramModel(programEntity, programModel);
                    programModels.Add(programModel);
                }
            }
            return programModels;
        }

        private SimpleProgramModel ConvertProgramToSimpleProgramModel(Program programEntity,SimpleProgramModel simpleProgramModel)
        {
            if (programEntity!=null)
            {
                simpleProgramModel.ProgramGuid = programEntity.ProgramGUID;
                simpleProgramModel.ProgramName = programEntity.Name;
                simpleProgramModel.Description = programEntity.Description;
            }
            return simpleProgramModel;
        }
        #endregion


        #region provider for Win8Service's functions 
        

        public ProgramContentModel GetProgramInfoModelByProgramGuid(string windowsLiveId, string applicationId, Guid programGuid)
        {
            //through windowsLiveId to find end user.
            ProgramContentModel programContentModel = null;
            IQueryable<Win8ProgramUser> windowsLiveEntities = Resolve<IWin8ProgramUserRepository>().GetWindowsLiveByWindowsLiveId(windowsLiveId);//
            Win8ProgramUser win8ProgramUserEntity = windowsLiveEntities.Where(wl => wl.ProgramUser.Program.ProgramGUID == programGuid).FirstOrDefault();

            if (win8ProgramUserEntity != null)
            {
                if (!win8ProgramUserEntity.ProgramUserReference.IsLoaded) win8ProgramUserEntity.ProgramUserReference.Load();
                //ProgramUser pu = Resolve<IProgramUserService>().GetProgramUserByProgramGuidAndUserGuid(programGuid, userGuid);
                Program programEntity = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
                if (programEntity != null)
                {
                    programContentModel = new ProgramContentModel
                    {
                        ProgramGuid = programEntity.ProgramGUID,
                        ProgramName = programEntity.Name,
                        ProgramDescription = programEntity.Description,
                        ProgramImage = programEntity.ProgramImageUrl,
                        Price = programEntity.Price.HasValue ? programEntity.Price.Value : 0,
                        ReportUrl = GetReportButtonLinkAddress(win8ProgramUserEntity.ProgramUser),
                        HelpUrl = GetHelpButtonLinkAddress(win8ProgramUserEntity.ProgramUser),
                        NextSession = win8ProgramUserEntity.ProgramUser.Day.Value + 1,
                        SubTitle = programEntity.ShortName,
                        OfferToken = programEntity.OfferToken,
                        //CurrentSessionNumber = win8ProgramUserEntity.ProgramUser.Day.Value,
                        //CurrentSessionUrl = Resolve<ISessionService>().GetRunSessionUrl(win8ProgramUserEntity.ProgramUser, win8ProgramUserEntity.ProgramUser.Day.Value+1),//?
                        ProgramPrimaryColor = ChangeColorToRGBFormat(programEntity.ProgramPrimaryColor),
                        ProgramSecondaryColor = ChangeColorToRGBFormat(programEntity.ProgramSecondaryColor),
                        ProgramPurpose = programEntity.ProgramPurpose,
                        ProgramFunction = programEntity.ProgramFunction,

                        ProductDescription = programEntity.ProductDescription,
                        ProductImage = programEntity.ProductImage,
                        ProductImageLarge = programEntity.ProductImageLarge,
                        ProductImageSmall = programEntity.ProductImageSmall,
                        ProductImagePresenter = programEntity.ProductImagePresenter,
                        ProductInstructorImage = programEntity.ProductInstructorImage,
                        Screenshot1 = programEntity.Screenshot1,
                        Screenshot2 = programEntity.Screenshot2,
                        Screenshot3 = programEntity.Screenshot3,
                        SessionText = programEntity.SessionText,

                        NotificationTime = programEntity.DailySMSTime
                    };
                    //ctpp programPresentImage
                    programContentModel.ProgramPresenterImage = GetProgramPresenterImageUrl(programGuid);
                    CTPPModel ctppModel = Resolve<ICTPPService>().GetCTPP(programGuid);
                    if (ctppModel != null)
                    {
                        programContentModel.ProgramDescriptionTitleFromCTPP = ctppModel.ProgramDescriptionTitle;
                        programContentModel.ProgramDescriptionFromCTPP = ctppModel.ProgramDescription;
                        programContentModel.ProgramDescriptionTitleForMobileFromCTPP = ctppModel.ProgramDescriptionTitleForMobile;
                        programContentModel.ProgramDescriptionForMobileFromCTPP = ctppModel.ProgramDescriptionForMobile;
                        programContentModel.FactHeader1 = ctppModel.FactHeader1;
                        programContentModel.FactHeader2 = ctppModel.FactHeader2;
                        programContentModel.FactHeader3 = ctppModel.FactHeader3;
                        programContentModel.FactHeader4 = ctppModel.FactHeader4;
                        programContentModel.FactContent1 = ctppModel.FactContent1;
                        programContentModel.FactContent2 = ctppModel.FactContent2;
                        programContentModel.FactContent3 = ctppModel.FactContent3;
                        programContentModel.FactContent4 = ctppModel.FactContent4;
                        programContentModel.ProgramPresenterSmallImageUrl = Resolve<ICTPPRepository>().GetCTPPByProgramGuid(programGuid).ProgramPresenterSmallImageUrl;
                    }
                    //CurrentSessionNumber,CurrentSessionUrl
                    Dictionary<int, string> currentSessionInfo = Resolve<IProgramCategoryService>().GetCurrentSessionUrl(win8ProgramUserEntity, programGuid); 
                    foreach (KeyValuePair<int, string> currentSession in currentSessionInfo)
                    {
                        programContentModel.CurrentSessionNumber = currentSession.Key;
                        programContentModel.CurrentSessionUrl = currentSession.Value;
                    }

                    int numberOfSessions = Resolve<ISessionService>().GetLastSessionDay(programGuid);
                    if (programContentModel.CurrentSessionNumber < numberOfSessions)
                    {
                        programContentModel.IsCompleted = false;
                    }
                    else
                    {
                        programContentModel.IsCompleted = true;
                    }

                    //Sessions
                    programContentModel.Sessions = Resolve<ISessionService>().GetSessionInfoModelsByProgramGuid(programGuid, win8ProgramUserEntity.ProgramUser);
                }
            }
            else
            {
                Program programEntity = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
                if (programEntity != null)
                {
                    programContentModel = new ProgramContentModel
                    {
                        ProgramGuid = programEntity.ProgramGUID,
                        ProgramName = programEntity.Name,
                        ProgramDescription = programEntity.Description,
                        ProgramImage = programEntity.ProgramImageUrl,
                        Price = programEntity.Price.HasValue ? programEntity.Price.Value : 0,
                        ReportUrl = string.Empty,
                        HelpUrl = string.Empty,
                        CurrentSessionNumber = 0,
                        NextSession = 1,
                        SubTitle = programEntity.ShortName,
                        OfferToken = programEntity.OfferToken,
                        CurrentSessionUrl = GetCurrentSessionUrl(programGuid),
                        ProgramPrimaryColor = ChangeColorToRGBFormat(programEntity.ProgramPrimaryColor),
                        ProgramSecondaryColor = ChangeColorToRGBFormat(programEntity.ProgramSecondaryColor),
                        ProgramPurpose = programEntity.ProgramPurpose,
                        ProgramFunction = programEntity.ProgramFunction,

                        ProductDescription = programEntity.ProductDescription,
                        ProductImage = programEntity.ProductImage,
                        ProductImageLarge = programEntity.ProductImageLarge,
                        ProductImageSmall = programEntity.ProductImageSmall,
                        ProductImagePresenter = programEntity.ProductImagePresenter,
                        ProductInstructorImage = programEntity.ProductInstructorImage,
                        Screenshot1 = programEntity.Screenshot1,
                        Screenshot2 = programEntity.Screenshot2,
                        Screenshot3 = programEntity.Screenshot3,
                        SessionText = programEntity.SessionText,

                        NotificationTime = programEntity.DailySMSTime
                    };

                    //ctpp programPresentImage
                    programContentModel.ProgramPresenterImage = GetProgramPresenterImageUrl(programGuid);
                    CTPPModel ctppModel = Resolve<ICTPPService>().GetCTPP(programGuid);
                    if (ctppModel != null)
                    {
                        programContentModel.ProgramDescriptionTitleFromCTPP = ctppModel.ProgramDescriptionTitle;
                        programContentModel.ProgramDescriptionFromCTPP = ctppModel.ProgramDescription;
                        programContentModel.ProgramDescriptionTitleForMobileFromCTPP = ctppModel.ProgramDescriptionTitleForMobile;
                        programContentModel.ProgramDescriptionForMobileFromCTPP = ctppModel.ProgramDescriptionForMobile;
                        programContentModel.FactHeader1 = ctppModel.FactHeader1;
                        programContentModel.FactHeader2 = ctppModel.FactHeader2;
                        programContentModel.FactHeader3 = ctppModel.FactHeader3;
                        programContentModel.FactHeader4 = ctppModel.FactHeader4;
                        programContentModel.FactContent1 = ctppModel.FactContent1;
                        programContentModel.FactContent2 = ctppModel.FactContent2;
                        programContentModel.FactContent3 = ctppModel.FactContent3;
                        programContentModel.FactContent4 = ctppModel.FactContent4;
                        programContentModel.ProgramPresenterSmallImageUrl = Resolve<ICTPPRepository>().GetCTPPByProgramGuid(programGuid).ProgramPresenterSmallImageUrl;
                    }
                    programContentModel.IsCompleted = false;
                    //Sessions
                    programContentModel.Sessions = Resolve<ISessionService>().GetSessionInfoModelsByProgramGuid(programGuid, null);
                }
            }
            return programContentModel;
        }

        //private functions
        private string GetCurrentSessionUrl(Guid programGuid)
        {
            //string securityStr = StringUtility.MD5Encrypt(string.Format("{0};{1}", userEntity.Email, userEntity.Password), MD5_KEY);
            //https://program.changetech.no/ChangeTech5.html?Mode=Trial&P=8H664J
            string serverPath = string.Empty;
            string currentSessionUrl = string.Empty;
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
            currentSessionUrl = string.Format("{0}ChangeTech5.html?Mode=Trial&P={1}", serverPath, programCode);
            currentSessionUrl = currentSessionUrl.Replace("#", "");

            return currentSessionUrl;
        }

        public string GetHelpButtonLinkAddress(ProgramUser pu)
        {
            string url = string.Empty;
            string serverPath = string.Empty;
            if (!pu.ProgramReference.IsLoaded) pu.ProgramReference.Load();
            if (!pu.UserReference.IsLoaded) pu.UserReference.Load();
            ProgramPropertyModel proPovertyModel = Resolve<IProgramService>().GetProgramProperty(pu.Program.ProgramGUID);
            if (proPovertyModel.IsCTPPEnable)
            {
                if (proPovertyModel.IsSupportHttps)
                {
                    serverPath = Resolve<ISystemSettingService>().GetSettingValue("WebServerPathHttps");
                }
                else
                {
                    serverPath = Resolve<ISystemSettingService>().GetSettingValue("WebServerPath");
                }
                CTPPModel ctppModel = Resolve<ICTPPService>().GetCTPP(pu.Program.ProgramGUID);
                string programCode = proPovertyModel.ProgramCode;
                if (ctppModel.HelpButtonRelapsePageSequenceGuid != Guid.Empty && ctppModel.HelpButtonRelapsePageSequenceGuid != null)
                {
                    string securityStr = StringUtility.MD5Encrypt(string.Format("{0};{1};{2};{3}", pu.User.Email, pu.User.Password, UserTaskTypeEnum.HelpInCTPP.ToString(), ctppModel.HelpButtonRelapsePageSequenceGuid.ToString()), MD5_KEY);
                    url = string.Format("{0}ChangeTech.html?P={1}&Mode=Live&Security={2}", serverPath, programCode, securityStr);
                    url = url.Replace("#", "");
                }
            }
            return url;
        }

        public string GetReportButtonLinkAddress(ProgramUser pu)
        {
            string url = string.Empty;
            string serverPath = string.Empty;
            if (!pu.ProgramReference.IsLoaded) pu.ProgramReference.Load();
            if (!pu.UserReference.IsLoaded) pu.UserReference.Load();
            ProgramPropertyModel proPovertyModel = Resolve<IProgramService>().GetProgramProperty(pu.Program.ProgramGUID);
            if (proPovertyModel.IsCTPPEnable)
            {
                if (proPovertyModel.IsSupportHttps)
                {
                    serverPath = Resolve<ISystemSettingService>().GetSettingValue("WebServerPathHttps");
                }
                else
                {
                    serverPath = Resolve<ISystemSettingService>().GetSettingValue("WebServerPath");
                }

                CTPPModel ctppModel = Resolve<ICTPPService>().GetCTPP(pu.Program.ProgramGUID);
                string programCode = proPovertyModel.ProgramCode;
                if (ctppModel.ReportButtonRelapsePageSequenceGuid != Guid.Empty && ctppModel.ReportButtonRelapsePageSequenceGuid!=null)
                {
                    string securityStr = StringUtility.MD5Encrypt(string.Format("{0};{1};{2};{3}", pu.User.Email, pu.User.Password, UserTaskTypeEnum.ReportInCTPP.ToString(), ctppModel.ReportButtonRelapsePageSequenceGuid.ToString()), MD5_KEY);
                    url = string.Format("{0}ChangeTech5.html?P={1}&Mode=Live&Security={2}", serverPath, programCode, securityStr);
                    url = url.Replace("#", "");
                }
            }

            return url;
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
            if (!string.IsNullOrEmpty(colorStr) && colorStr.Contains("0x"))
            {
                rgbColorFormat = "#" + colorStr.Substring(2);
            }

            return rgbColorFormat;
        }
        #endregion

    }
}
