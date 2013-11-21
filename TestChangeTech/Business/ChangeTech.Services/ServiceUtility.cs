using System.Collections.Generic;
using ChangeTech.Contracts;
using ChangeTech.Models;
using Ethos.DependencyInjection;
using System.Linq;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;
using System;
using System.Data.Objects.DataClasses;
using Microsoft.WindowsAzure.StorageClient;
using System.Web;
using Ethos.Utility;
using System.Text.RegularExpressions;

namespace ChangeTech.Services
{
    public class ServiceUtility
    {
        public static readonly string MD5_KEY = "psycholo";

        #region old clone program, no use now
        public static Program CloneProgram(Program program, string programCode)
        {
            try
            {
                Program cloneProgram = new Program();
                cloneProgram.ProgramGUID = Guid.NewGuid();
                cloneProgram.Name = string.Format("Copy of {0} on {1}", program.Name, DateTime.UtcNow.ToString());
                cloneProgram.Description = program.Description;
                cloneProgram.Code = programCode;
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
                    cloneProgram.EmailTemplate.Add(CloneEmailTemplate(template));
                }

                // program room
                if (!program.ProgramRoom.IsLoaded)
                {
                    program.ProgramRoom.Load();
                }
                foreach (ProgramRoom room in program.ProgramRoom)
                {
                    cloneProgram.ProgramRoom.Add(CloneProgramRoom(room));
                }

                // page variable group
                if (!program.PageVariableGroup.IsLoaded)
                {
                    program.PageVariableGroup.Load();
                }
                foreach (PageVariableGroup group in program.PageVariableGroup)
                {
                    cloneProgram.PageVariableGroup.Add(CloneVariableGroup(group));
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
                    cloneProgram.AccessoryTemplate.Add(CloneAccessoryTemplate(accessoryTemplate));
                }

                // Help item
                if (!program.HelpItem.IsLoaded)
                {
                    program.HelpItem.Load();
                }
                foreach (HelpItem helpItem in program.HelpItem)
                {
                    cloneProgram.HelpItem.Add(CloneHelpItem(helpItem));
                }

                // User menu
                if (!program.UserMenu.IsLoaded)
                {
                    program.UserMenu.Load();
                }
                foreach (UserMenu userMenu in program.UserMenu)
                {
                    cloneProgram.UserMenu.Add(CloneUserMenu(userMenu));
                }

                List<KeyValuePair<string, string>> relpaseGUIDPairList = new List<KeyValuePair<string, string>>();
                // relapse
                if (!program.Relapse.IsLoaded)
                {
                    program.Relapse.Load();
                }
                foreach (Relapse rel in program.Relapse)
                {
                    Relapse cloneRelapse = CloneRelapse(rel);
                    cloneProgram.Relapse.Add(cloneRelapse);
                    relpaseGUIDPairList.Add(new KeyValuePair<string, string>(rel.PageSequence.PageSequenceGUID.ToString().ToUpper(), cloneRelapse.PageSequence.PageSequenceGUID.ToString().ToUpper()));
                }

                // schedule
                if (!program.ProgramSchedule.IsLoaded)
                {
                    program.ProgramSchedule.Load();
                }
                foreach (ProgramSchedule schedule in program.ProgramSchedule)
                {
                    cloneProgram.ProgramSchedule.Add(CloneProgramSchedule(schedule));
                }

                // tip message
                if (!program.TipMessage.IsLoaded)
                {
                    program.TipMessage.Load();
                }
                foreach (TipMessage message in program.TipMessage)
                {
                    cloneProgram.TipMessage.Add(CloneTipMessage(message));
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
                    Session newSession = CloneSession(session, relpaseGUIDPairList);
                    cloneProgram.Session.Add(newSession);
                }
                return cloneProgram;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private static TipMessage CloneTipMessage(TipMessage message)
        {
            try
            {
                if (!message.TipMessageTypeReference.IsLoaded)
                {
                    message.TipMessageTypeReference.Load();
                }
                if (!message.ProgramReference.IsLoaded)
                {
                    message.ProgramReference.Load();
                }

                TipMessage clonedMessage = new TipMessage
                {
                    BackButtonName = message.BackButtonName,
                    Message = message.Message,
                    TipMessageGUID = Guid.NewGuid(),
                    Title = message.Title,
                    //Program = message.Program,
                    TipMessageType = message.TipMessageType,
                    ParentTipMessageGUID = message.TipMessageGUID
                };

                return clonedMessage;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private static ProgramSchedule CloneProgramSchedule(ProgramSchedule schedule)
        {
            try
            {
                ProgramSchedule clonedSchedule = new ProgramSchedule
                {
                    Week = schedule.Week,
                    WeekDay = schedule.WeekDay,
                    ParentProgramScheduleGUID = schedule.ID
                };

                return clonedSchedule;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private static Relapse CloneRelapse(Relapse rel)
        {
            try
            {
                if (!rel.PageSequenceReference.IsLoaded)
                {
                    rel.PageSequenceReference.Load();
                }

                Relapse clonerelapse = new Relapse
                {
                    RelapseGUID = Guid.NewGuid(),
                    PageSequence = ClonePageSequence(rel.PageSequence, new List<KeyValuePair<string, string>>()),
                    ParentRelapseGUID = rel.RelapseGUID
                };

                return clonerelapse;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private static UserMenu CloneUserMenu(UserMenu userMenu)
        {
            try
            {
                UserMenu cloneUserMenu = new UserMenu
                {
                    MenuFormBackButtonName = userMenu.MenuFormBackButtonName,
                    MenuFormSubmitButtonName = userMenu.MenuFormSubmitButtonName,
                    MenuFormText = userMenu.MenuFormText,
                    MenuFormTitle = userMenu.MenuFormTitle,
                    MenuItemGUID = Guid.NewGuid(),
                    MenuText = userMenu.MenuText,
                    Name = userMenu.Name,
                    Order = userMenu.Order,
                    ParentUserMenuGUID = userMenu.MenuItemGUID
                };
                return cloneUserMenu;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private static HelpItem CloneHelpItem(HelpItem helpItem)
        {
            try
            {
                HelpItem cloneHelpItem = new HelpItem
                {
                    Answer = helpItem.Answer,
                    HelpItemGUID = Guid.NewGuid(),
                    Order = helpItem.Order,
                    Question = helpItem.Question,
                    ParentHelpItemGUID = helpItem.HelpItemGUID
                };
                return cloneHelpItem;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private static AccessoryTemplate CloneAccessoryTemplate(AccessoryTemplate accessoryTemplate)
        {
            try
            {
                AccessoryTemplate cloneAccessoryTemplate = new AccessoryTemplate
                {
                    AccessoryTemplateGUID = Guid.NewGuid(),
                    Heading = accessoryTemplate.Heading,
                    Order = accessoryTemplate.Order,
                    PasswordText = accessoryTemplate.PasswordText,
                    PrimaryButtonText = accessoryTemplate.PrimaryButtonText,
                    SecondaryButtonText = accessoryTemplate.SecondaryButtonText,
                    Text = accessoryTemplate.Text,
                    Type = accessoryTemplate.Type,
                    UserNameText = accessoryTemplate.UserNameText,
                    ParentAccessoryTemplateGUID = accessoryTemplate.AccessoryTemplateGUID
                };
                return cloneAccessoryTemplate;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private static PageVariableGroup CloneVariableGroup(PageVariableGroup group)
        {
            try
            {
                PageVariableGroup cloneGroup = new PageVariableGroup
                {
                    PageVariableGroupGUID = Guid.NewGuid(),
                    Name = group.Name,
                    Description = group.Description,
                    LastUpdated = DateTime.UtcNow,
                    ParentPageVariableGroupGUID = group.PageVariableGroupGUID
                };

                return cloneGroup;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private static ProgramRoom CloneProgramRoom(ProgramRoom room)
        {
            try
            {
                ProgramRoom cloneRoom = new ProgramRoom
                {
                    ProgramRoomGUID = Guid.NewGuid(),
                    Name = room.Name,
                    Description = room.Description,
                    PrimaryThemeColor = room.PrimaryThemeColor,
                    SecondaryThemeColor = room.SecondaryThemeColor,
                    LastUpdated = DateTime.UtcNow,
                    ParentProgramRoomGUID = room.ProgramRoomGUID
                };

                return cloneRoom;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private static EmailTemplate CloneEmailTemplate(EmailTemplate template)
        {
            try
            {
                if (!template.EmailTemplateTypeReference.IsLoaded)
                {
                    template.EmailTemplateTypeReference.Load();
                }
                EmailTemplate emailTemplate = new EmailTemplate
                {
                    EmailTemplateGUID = Guid.NewGuid(),
                    EmailTemplateType = template.EmailTemplateType,
                    Name = template.Name,
                    Subject = template.Subject,
                    Body = template.Body,
                    LastUpdated = DateTime.UtcNow,
                    ParentEmailTemplateGUID = template.EmailTemplateGUID
                };

                return emailTemplate;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public static Session CloneSession(Session session, List<KeyValuePair<string, string>> cloneRelapseGUIDList)
        {
            try
            {
                Session cloneSession = new Session();
                cloneSession.SessionGUID = Guid.NewGuid();
                cloneSession.Name = string.Format("Copy of {0} on {1}", session.Name, DateTime.UtcNow.ToString());
                cloneSession.Day = session.Day;
                cloneSession.Description = session.Description;
                cloneSession.ParentSessionGUID = session.SessionGUID;
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
                    SessionContent newSessionContent = CloneSessionContent(sessionContent, cloneRelapseGUIDList, pageDictionary);
                    cloneSession.SessionContent.Add(newSessionContent);
                }
                UpdatePageContent(cloneSession, pageDictionary);
                return cloneSession;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public static Session CloneSessionWithExistPageSequence(Session session)
        {
            try
            {
                Session cloneSession = new Session();
                cloneSession.SessionGUID = Guid.NewGuid();
                cloneSession.Name = string.Format("Copy of {0} on {1}", session.Name, DateTime.UtcNow.ToString());
                cloneSession.Day = session.Day;
                cloneSession.Description = session.Description;
                cloneSession.ParentSessionGUID = session.SessionGUID;
                if (!session.SessionContent.IsLoaded)
                {
                    session.SessionContent.Load();
                }

                List<SessionContent> sessionContents = session.SessionContent.Where(s => s.IsDeleted != true).ToList();

                foreach (SessionContent sessionContent in sessionContents)
                {
                    if (!sessionContent.PageSequenceReference.IsLoaded)
                    {
                        sessionContent.PageSequenceReference.Load();
                    }
                    SessionContent newSessionContent = CloneSessionContentWithExistPageSequence(sessionContent);
                    cloneSession.SessionContent.Add(newSessionContent);
                }

                return cloneSession;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public static SessionContent CloneSessionContentWithExistPageSequence(SessionContent sessionContent)
        {
            try
            {
                if (!sessionContent.PageSequenceReference.IsLoaded)
                {
                    sessionContent.PageSequenceReference.Load();
                }
                if (!sessionContent.ProgramRoomReference.IsLoaded)
                {
                    sessionContent.ProgramRoomReference.Load();
                }
                SessionContent cloneSessionContent = new SessionContent();

                cloneSessionContent.SessionContentGUID = Guid.NewGuid();
                cloneSessionContent.PageSequence = sessionContent.PageSequence;
                cloneSessionContent.PageSequenceOrderNo = sessionContent.PageSequenceOrderNo;
                cloneSessionContent.ProgramRoom = sessionContent.ProgramRoom;

                return cloneSessionContent;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public static SessionContent CloneSessionContent(SessionContent sessionContent, List<KeyValuePair<string, string>> cloneRelapseGUIDList, Dictionary<Guid, Guid> pageDictionary)
        {
            try
            {
                if (!sessionContent.PageSequenceReference.IsLoaded)
                {
                    sessionContent.PageSequenceReference.Load();
                }
                if (!sessionContent.ProgramRoomReference.IsLoaded)
                {
                    sessionContent.ProgramRoomReference.Load();
                }
                SessionContent cloneSessionContent = new SessionContent();

                cloneSessionContent.SessionContentGUID = Guid.NewGuid();
                cloneSessionContent.ParentSessionContentGUID = sessionContent.SessionContentGUID;
                cloneSessionContent.PageSequence = ClonePageSequence(sessionContent.PageSequence, cloneRelapseGUIDList, pageDictionary);
                cloneSessionContent.PageSequenceOrderNo = sessionContent.PageSequenceOrderNo;
                cloneSessionContent.ProgramRoom = sessionContent.ProgramRoom;

                return cloneSessionContent;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public static PageSequence ClonePageSequence(PageSequence pageSeq, List<KeyValuePair<string, string>> cloneRelapseGUIDList)
        {
            try
            {
                if (!pageSeq.InterventReference.IsLoaded)
                {
                    pageSeq.InterventReference.Load();
                }
                if (!pageSeq.Page.IsLoaded)
                {
                    pageSeq.Page.Load();
                }
                PageSequence newPageSeq = new PageSequence();
                newPageSeq.PageSequenceGUID = Guid.NewGuid();
                newPageSeq.Name = string.Format("Copy of {0} on {1}", pageSeq.Name, DateTime.UtcNow.ToString());
                newPageSeq.Description = pageSeq.Description;
                newPageSeq.Intervent = pageSeq.Intervent;
                newPageSeq.IsDeleted = false;
                newPageSeq.ParentPageSequenceGUID = pageSeq.PageSequenceGUID;

                List<Page> pages = pageSeq.Page.Where(p => p.IsDeleted != true).ToList();
                foreach (Page page in pages)
                {
                    Page newPage = ClonePage(page, cloneRelapseGUIDList);
                    newPageSeq.Page.Add(newPage);
                }
                return newPageSeq;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public static PageSequence ClonePageSequence(PageSequence pageSeq, List<KeyValuePair<string, string>> cloneRelapseGUIDList, Dictionary<Guid, Guid> pageDictionary)
        {
            try
            {
                if (!pageSeq.InterventReference.IsLoaded)
                {
                    pageSeq.InterventReference.Load();
                }
                if (!pageSeq.Page.IsLoaded)
                {
                    pageSeq.Page.Load();
                }
                PageSequence newPageSeq = new PageSequence();
                newPageSeq.PageSequenceGUID = Guid.NewGuid();
                newPageSeq.Name = string.Format("Copy of {0} on {1}", pageSeq.Name, DateTime.UtcNow.ToString());
                newPageSeq.Description = pageSeq.Description;
                newPageSeq.Intervent = pageSeq.Intervent;
                newPageSeq.IsDeleted = false;
                newPageSeq.ParentPageSequenceGUID = pageSeq.PageSequenceGUID;

                List<Page> pages = pageSeq.Page.Where(p => p.IsDeleted != true).ToList();
                foreach (Page page in pages)
                {
                    Page newPage = ClonePage(page, cloneRelapseGUIDList);
                    newPageSeq.Page.Add(newPage);
                    if (!pageDictionary.ContainsKey(page.PageGUID))
                        pageDictionary.Add(page.PageGUID, newPage.PageGUID);
                }
                return newPageSeq;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private static void UpdatePageContent(Session newSession, Dictionary<Guid, Guid> pageDictionary)
        {
            try
            {
                foreach (SessionContent newSessionContent in newSession.SessionContent)
                {
                    foreach (Page newPage in newSessionContent.PageSequence.Page)
                    {
                        foreach (KeyValuePair<Guid, Guid> pagePair in pageDictionary)
                        {
                            if (newPage.PageContent != null && !string.IsNullOrEmpty(newPage.PageContent.AfterShowExpression) && newPage.PageContent.AfterShowExpression.Contains(pagePair.Key.ToString()))
                            {
                                newPage.PageContent.AfterShowExpression = newPage.PageContent.AfterShowExpression.Replace(pagePair.Key.ToString(), pagePair.Value.ToString());
                            }
                            if (newPage.PageContent != null && !string.IsNullOrEmpty(newPage.PageContent.BeforeShowExpression) && newPage.PageContent.BeforeShowExpression.Contains(pagePair.Key.ToString()))
                            {
                                newPage.PageContent.BeforeShowExpression = newPage.PageContent.BeforeShowExpression.Replace(pagePair.Key.ToString(), pagePair.Value.ToString());
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public static Page ClonePage(Page page, List<KeyValuePair<string, string>> cloneRelapseGUIDList)
        {
            try
            {
                Page newPage = new Page();
                newPage.PageGUID = Guid.NewGuid();
                newPage.PageOrderNo = page.PageOrderNo;
                newPage.LastUpdated = page.LastUpdated;
                UserService us = new UserService();
                //newPage.LastUpdatedBy = us.GetCurrentUser().UserGuid;
                newPage.Created = page.Created;
                newPage.Wait = page.Wait;
                newPage.MaxPreferences = page.MaxPreferences;
                newPage.ParentPageGUID = page.PageGUID;
                if (!page.PageVariableReference.IsLoaded)
                {
                    page.PageVariableReference.Load();
                }
                newPage.PageVariable = page.PageVariable;
                if (!page.PageTemplateReference.IsLoaded)
                {
                    page.PageTemplateReference.Load();
                }
                newPage.PageTemplate = page.PageTemplate;
                if (!page.PageContentReference.IsLoaded)
                {
                    page.PageContentReference.Load();
                }
                //List<PageContent> pageContents = page.PageContent.Where(p => p.IsDeleted != true).ToList();
                if (page.PageContent != null)
                {
                    newPage.PageContent = ClonePageContent(page.PageContent, cloneRelapseGUIDList);
                }

                if (!page.PageQuestion.IsLoaded)
                {
                    page.PageQuestion.Load();
                }
                List<PageQuestion> pageQuestions = page.PageQuestion.Where(p => p.IsDeleted != true).ToList();
                foreach (PageQuestion pageQuestion in pageQuestions)
                {
                    newPage.PageQuestion.Add(ClonePageQuestion(pageQuestion));
                }
                if (!page.PageMediaReference.IsLoaded)
                {
                    page.PageMediaReference.Load();
                }

                //List<PageMedia> pageMedias = page.PageMedia.Where(p => p.IsDeleted != true).ToList();
                //foreach (PageMedia pm in pageMedias)
                //{
                if (page.PageMedia != null)
                {
                    newPage.PageMedia = ClonePageMedia(page.PageMedia);
                }
                //}
                if (!page.Preferences.IsLoaded)
                {
                    page.Preferences.Load();
                }
                List<Preferences> preferences = page.Preferences.Where(p => p.IsDeleted != true).ToList();
                foreach (Preferences pre in preferences)
                {
                    newPage.Preferences.Add(ClonePreferences(pre));
                }
                if (!page.Graph.IsLoaded)
                {
                    page.Graph.Load();
                }
                List<Graph> graphes = page.Graph.Where(g => g.IsDeleted != true).ToList();
                foreach (Graph graph in graphes)
                {
                    newPage.Graph.Add(CloneGraph(graph));
                }
                return newPage;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private static Graph CloneGraph(Graph graph)
        {
            try
            {
                Graph cloneGraph = new Graph();
                cloneGraph.BadScoreRange = graph.BadScoreRange;
                //cloneGraph.Caption = graph.Caption;
                cloneGraph.GoodScoreRange = graph.GoodScoreRange;
                cloneGraph.GraphGUID = Guid.NewGuid();
                cloneGraph.MediumRange = graph.MediumRange;
                cloneGraph.ScoreRange = graph.ScoreRange;
                cloneGraph.TimeRange = graph.TimeRange;
                cloneGraph.TimeUnit = graph.TimeUnit;
                cloneGraph.Type = graph.Type;
                cloneGraph.ParentGraphGUID = graph.GraphGUID;
                if (!graph.GraphContentReference.IsLoaded)
                {
                    graph.GraphContentReference.Load();
                }
                //List<GraphContent> graphContents = graph.GraphContent.Where(g => g.IsDeleted != true).ToList();
                if (graph.GraphContent != null)
                {
                    cloneGraph.GraphContent = CloneGraphContent(graph.GraphContent);
                }
                if (!graph.GraphItem.IsLoaded)
                {
                    graph.GraphItem.Load();
                }

                List<GraphItem> graphItems = graph.GraphItem.Where(g => g.IsDeleted != true).ToList();
                foreach (GraphItem item in graphItems)
                {
                    cloneGraph.GraphItem.Add(CloneGraphItem(item));
                }

                return cloneGraph;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private static GraphContent CloneGraphContent(GraphContent graphContent)
        {
            try
            {
                GraphContent cloneGraphContent = new GraphContent
                {
                    ParentGraphContentGUID = graphContent.GraphGUID,
                    Caption = graphContent.Caption,
                    //Language = graphContent.Language,
                    TimeUnit = graphContent.TimeUnit
                };
                return cloneGraphContent;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private static GraphItem CloneGraphItem(GraphItem item)
        {
            try
            {
                GraphItem cloneGraphItem = new GraphItem();
                cloneGraphItem.GraphItemGUID = Guid.NewGuid();
                //cloneGraphItem.Name = item.Name;
                cloneGraphItem.PointType = item.PointType;
                cloneGraphItem.DataItemExpression = item.DataItemExpression;
                cloneGraphItem.Color = item.Color;
                cloneGraphItem.ParentGraphItemGUID = item.GraphItemGUID;
                if (!item.GraphItemContentReference.IsLoaded)
                {
                    item.GraphItemContentReference.Load();
                }

                cloneGraphItem.GraphItemContent = CloneGraphItemContent(item.GraphItemContent);
                return cloneGraphItem;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private static GraphItemContent CloneGraphItemContent(GraphItemContent itemcontent)
        {
            try
            {
                GraphItemContent cloneGraphItemContent = new GraphItemContent
                {
                    ParentGraphItemContentGUID = itemcontent.GraphItemGUID,
                    //Language = itemcontent.Language,
                    Name = itemcontent.Name
                };

                return cloneGraphItemContent;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private static Preferences ClonePreferences(Preferences preferences)
        {
            try
            {
                Preferences clonePreferences = new Preferences();
                //if(!preferences.LanguageReference.IsLoaded)
                //{
                //    preferences.LanguageReference.Load();
                //}
                //clonePreferences.Language = preferences.Language;
                if (!preferences.ResourceReference.IsLoaded)
                {
                    preferences.ResourceReference.Load();
                }
                if (!preferences.PageVariableReference.IsLoaded)
                {
                    preferences.PageVariableReference.Load();
                }
                clonePreferences.PreferencesGUID = Guid.NewGuid();
                clonePreferences.Resource = preferences.Resource;
                clonePreferences.Name = preferences.Name;
                clonePreferences.AnswerText = preferences.AnswerText;
                clonePreferences.Description = preferences.Description;
                clonePreferences.ButtonName = preferences.ButtonName;
                clonePreferences.PageVariable = preferences.PageVariable;
                clonePreferences.ParentPreferencesGUID = preferences.PreferencesGUID;

                return clonePreferences;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public static PageMedia ClonePageMedia(PageMedia pageMedia)
        {
            try
            {
                PageMedia clonePageMedia = new PageMedia();
                //if (!pageMedia.LanguageReference.IsLoaded)
                //{
                //    pageMedia.LanguageReference.Load();
                //}
                //clonePageMedia.Language = pageMedia.Language;
                if (!pageMedia.ResourceReference.IsLoaded)
                {
                    pageMedia.ResourceReference.Load();
                }
                clonePageMedia.Resource = pageMedia.Resource;
                clonePageMedia.Type = pageMedia.Type;
                clonePageMedia.ParentPageMediaItemGUID = pageMedia.PageGUID;

                return clonePageMedia;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public static PageContent ClonePageContent(PageContent pageContent, List<KeyValuePair<string, string>> cloneRelapseGUIDList)
        {
            try
            {
                PageContent clonePageContent = new PageContent();
                clonePageContent.ParentPageContentGUID = pageContent.PageGUID;
                clonePageContent.Heading = pageContent.Heading;
                clonePageContent.Body = pageContent.Body;
                clonePageContent.PrimaryButtonActionParameter = pageContent.PrimaryButtonActionParameter;
                //clonePageContent.SecondaryButtonActionParameter = pageContent.SecondaryButtonActionParameter;
                clonePageContent.PrimaryButtonCaption = pageContent.PrimaryButtonCaption;
                //clonePageContent.SecondaryButtonCaption = pageContent.SecondaryButtonCaption;
                //clonePageContent.Wait = pageContent.Wait;
                clonePageContent.PresenterImagePosition = pageContent.PresenterImagePosition;
                //clonePageContent.MaxPreferences = pageContent.MaxPreferences;

                clonePageContent.AfterShowExpression = pageContent.AfterShowExpression;
                clonePageContent.BeforeShowExpression = pageContent.BeforeShowExpression;
                //if(!string.IsNullOrEmpty(clonePageContent.AfterShowExpression)&&string.IsNullOrEmpty(clonePageContent.BeforeShowExpression))
                foreach (KeyValuePair<string, string> relapsePair in cloneRelapseGUIDList)
                {
                    if (!string.IsNullOrEmpty(clonePageContent.AfterShowExpression) && clonePageContent.AfterShowExpression.Contains(relapsePair.Key))
                    {
                        clonePageContent.AfterShowExpression = clonePageContent.AfterShowExpression.Replace(relapsePair.Key, relapsePair.Value);
                    }
                    if (!string.IsNullOrEmpty(clonePageContent.BeforeShowExpression) && clonePageContent.BeforeShowExpression.Contains(relapsePair.Key))
                    {
                        clonePageContent.BeforeShowExpression = clonePageContent.BeforeShowExpression.Replace(relapsePair.Key, relapsePair.Value);
                    }
                }

                if (!pageContent.Resource_PresenterImageReference.IsLoaded)
                {
                    pageContent.Resource_PresenterImageReference.Load();
                }
                clonePageContent.Resource_PresenterImage = pageContent.Resource_PresenterImage;

                if (!pageContent.Resource_BackgroundImageReference.IsLoaded)
                {
                    pageContent.Resource_BackgroundImageReference.Load();
                }
                clonePageContent.Resource_BackgroundImage = pageContent.Resource_BackgroundImage;

                //if (!pageContent.PageVariableReference.IsLoaded)
                //{
                //    pageContent.PageVariableReference.Load();
                //}
                //clonePageContent.PageVariable = pageContent.PageVariable;

                //if (!pageContent.LanguageReference.IsLoaded)
                //{
                //    pageContent.LanguageReference.Load();
                //}
                //clonePageContent.Language = pageContent.Language;
                return clonePageContent;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public static PageQuestion ClonePageQuestion(PageQuestion pageQuestion)
        {
            try
            {
                PageQuestion clonePageQuestion = new PageQuestion();
                clonePageQuestion.Order = pageQuestion.Order;
                clonePageQuestion.PageQuestionGUID = Guid.NewGuid();
                clonePageQuestion.IsRequired = pageQuestion.IsRequired;

                clonePageQuestion.ParentPageQuestionGUID = pageQuestion.PageQuestionGUID;
                if (!pageQuestion.QuestionReference.IsLoaded)
                {
                    pageQuestion.QuestionReference.Load();
                }
                clonePageQuestion.Question = pageQuestion.Question;

                //Clone page question content
                if (!pageQuestion.PageQuestionContentReference.IsLoaded)
                {
                    pageQuestion.PageQuestionContentReference.Load();
                }

                //List<PageQuestionContent> pageQuestionContents = pageQuestion.PageQuestionContent.Where(p => p.IsDeleted != true).ToList();
                if (pageQuestion.PageQuestionContent != null)
                {
                    clonePageQuestion.PageQuestionContent = ClonePageQuestionContent(pageQuestion.PageQuestionContent);
                }

                // Clone page question's items
                if (!pageQuestion.PageQuestionItem.IsLoaded)
                {
                    pageQuestion.PageQuestionItem.Load();
                }

                List<PageQuestionItem> pageQuestionItems = pageQuestion.PageQuestionItem.Where(p => p.IsDeleted != true).ToList();
                foreach (PageQuestionItem questionItem in pageQuestionItems)
                {
                    clonePageQuestion.PageQuestionItem.Add(ClonePageQuestionItem(questionItem));
                }

                // Clone page variable
                if (!pageQuestion.PageVariableReference.IsLoaded)
                {
                    pageQuestion.PageVariableReference.Load();
                }
                clonePageQuestion.PageVariable = pageQuestion.PageVariable;

                return clonePageQuestion;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public static PageQuestionContent ClonePageQuestionContent(PageQuestionContent questionContent)
        {
            try
            {
                PageQuestionContent cloneQuestionContent = new PageQuestionContent();
                //if (!questionContent.LanguageReference.IsLoaded)
                //{
                //    questionContent.LanguageReference.Load();
                //}
                //cloneQuestionContent.Language = questionContent.Language;
                cloneQuestionContent.ParentPageQuestionContentGUID = questionContent.PageQuestionGUID;
                cloneQuestionContent.Caption = questionContent.Caption;
                return cloneQuestionContent;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public static PageQuestionItem ClonePageQuestionItem(PageQuestionItem questionItem)
        {
            try
            {
                PageQuestionItem cloneQuestionItem = new PageQuestionItem();
                cloneQuestionItem.PageQuestionItemGUID = Guid.NewGuid();
                cloneQuestionItem.Order = questionItem.Order;
                cloneQuestionItem.ParentPageQuestionItemGUID = questionItem.PageQuestionItemGUID;
                if (!questionItem.PageQuestionItemContentReference.IsLoaded)
                {
                    questionItem.PageQuestionItemContentReference.Load();
                }
                //List<PageQuestionItemContent> pageQuestionItemContents = questionItem.PageQuestionItemContent.Where(p => p.IsDeleted != true).ToList();
                if (questionItem.PageQuestionItemContent != null)
                {
                    cloneQuestionItem.PageQuestionItemContent = ClonePageQuestionItemContent(questionItem.PageQuestionItemContent);
                }
                cloneQuestionItem.Score = questionItem.Score;
                return cloneQuestionItem;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public static PageQuestionItemContent ClonePageQuestionItemContent(PageQuestionItemContent pageQuestionItemContent)
        {
            try
            {
                PageQuestionItemContent clonePageQuestionItemContent = new PageQuestionItemContent();
                clonePageQuestionItemContent.ParentPageQuestionItemContentGUID = pageQuestionItemContent.PageQuestionItemGUID;
                clonePageQuestionItemContent.Feedback = pageQuestionItemContent.Feedback;
                clonePageQuestionItemContent.Item = pageQuestionItemContent.Item;
                //if (!pageQuestionItemContent.LanguageReference.IsLoaded)
                //{
                //    pageQuestionItemContent.LanguageReference.Load();
                //}
                //clonePageQuestionItemContent.Language = pageQuestionItemContent.Language;
                //if (!pageQuestionItemContent.PageQuestionItemReference.IsLoaded)
                //{
                //    pageQuestionItemContent.PageQuestionItemReference.Load();
                //}
                return clonePageQuestionItemContent;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }
        #endregion

        #region new temp clone program, no use now
        public static Program CloneProgramNotIncludeParentGuid(Program program, string programCode)
        {
            try
            {
                Program cloneProgram = new Program();
                cloneProgram.ProgramGUID = Guid.NewGuid();
                cloneProgram.Name = string.Format("Copy of {0} on {1}", program.Name, DateTime.UtcNow.ToString());
                cloneProgram.Description = program.Description;
                cloneProgram.Code = programCode;
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
                    cloneProgram.EmailTemplate.Add(CloneEmailTemplateNotIncludeParentGuid(template));
                }

                // program room
                if (!program.ProgramRoom.IsLoaded)
                {
                    program.ProgramRoom.Load();
                }
                foreach (ProgramRoom room in program.ProgramRoom)
                {
                    cloneProgram.ProgramRoom.Add(CloneProgramRoomNotIncludeParentGuid(room));
                }

                // page variable group
                if (!program.PageVariableGroup.IsLoaded)
                {
                    program.PageVariableGroup.Load();
                }
                foreach (PageVariableGroup group in program.PageVariableGroup)
                {
                    cloneProgram.PageVariableGroup.Add(CloneVariableGroupNotIncludeParentGuid(group));
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
                    cloneProgram.AccessoryTemplate.Add(CloneAccessoryTemplateNotIncludeParentGuid(accessoryTemplate));
                }

                // Help item
                if (!program.HelpItem.IsLoaded)
                {
                    program.HelpItem.Load();
                }
                foreach (HelpItem helpItem in program.HelpItem)
                {
                    cloneProgram.HelpItem.Add(CloneHelpItemNotIncludeParentGuid(helpItem));
                }

                // User menu
                if (!program.UserMenu.IsLoaded)
                {
                    program.UserMenu.Load();
                }
                foreach (UserMenu userMenu in program.UserMenu)
                {
                    cloneProgram.UserMenu.Add(CloneUserMenuNotIncludeParentGuid(userMenu));
                }

                List<KeyValuePair<string, string>> relpaseGUIDPairList = new List<KeyValuePair<string, string>>();
                // relapse
                if (!program.Relapse.IsLoaded)
                {
                    program.Relapse.Load();
                }
                foreach (Relapse rel in program.Relapse)
                {
                    Relapse cloneRelapse = CloneRelapseNotIncludeParentGuid(rel);
                    cloneProgram.Relapse.Add(cloneRelapse);
                    relpaseGUIDPairList.Add(new KeyValuePair<string, string>(rel.PageSequence.PageSequenceGUID.ToString().ToUpper(), cloneRelapse.PageSequence.PageSequenceGUID.ToString().ToUpper()));
                }

                // schedule
                if (!program.ProgramSchedule.IsLoaded)
                {
                    program.ProgramSchedule.Load();
                }
                foreach (ProgramSchedule schedule in program.ProgramSchedule)
                {
                    cloneProgram.ProgramSchedule.Add(CloneProgramScheduleNotIncludeParentGuid(schedule));
                }

                // tip message
                if (!program.TipMessage.IsLoaded)
                {
                    program.TipMessage.Load();
                }
                foreach (TipMessage message in program.TipMessage)
                {
                    cloneProgram.TipMessage.Add(CloneTipMessageNotIncludeParentGuid(message));
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
                    Session newSession = CloneSessionNotIncludeParentGuid(session, relpaseGUIDPairList);
                    cloneProgram.Session.Add(newSession);
                }
                return cloneProgram;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private static TipMessage CloneTipMessageNotIncludeParentGuid(TipMessage message)
        {
            try
            {
                if (!message.TipMessageTypeReference.IsLoaded)
                {
                    message.TipMessageTypeReference.Load();
                }
                if (!message.ProgramReference.IsLoaded)
                {
                    message.ProgramReference.Load();
                }

                TipMessage clonedMessage = new TipMessage
                {
                    BackButtonName = message.BackButtonName,
                    Message = message.Message,
                    TipMessageGUID = Guid.NewGuid(),
                    Title = message.Title,
                    //Program = message.Program,
                    TipMessageType = message.TipMessageType
                };

                return clonedMessage;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private static ProgramSchedule CloneProgramScheduleNotIncludeParentGuid(ProgramSchedule schedule)
        {
            try
            {
                ProgramSchedule clonedSchedule = new ProgramSchedule
                {
                    Week = schedule.Week,
                    WeekDay = schedule.WeekDay
                };

                return clonedSchedule;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private static Relapse CloneRelapseNotIncludeParentGuid(Relapse rel)
        {
            try
            {
                if (!rel.PageSequenceReference.IsLoaded)
                {
                    rel.PageSequenceReference.Load();
                }

                Relapse clonerelapse = new Relapse
                {
                    RelapseGUID = Guid.NewGuid(),
                    PageSequence = ClonePageSequenceNotIncludeParentGuid(rel.PageSequence, new List<KeyValuePair<string, string>>())
                };

                return clonerelapse;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private static UserMenu CloneUserMenuNotIncludeParentGuid(UserMenu userMenu)
        {
            try
            {
                UserMenu cloneUserMenu = new UserMenu
                {
                    MenuFormBackButtonName = userMenu.MenuFormBackButtonName,
                    MenuFormSubmitButtonName = userMenu.MenuFormSubmitButtonName,
                    MenuFormText = userMenu.MenuFormText,
                    MenuFormTitle = userMenu.MenuFormTitle,
                    MenuItemGUID = Guid.NewGuid(),
                    MenuText = userMenu.MenuText,
                    Name = userMenu.Name,
                    Order = userMenu.Order
                };
                return cloneUserMenu;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private static HelpItem CloneHelpItemNotIncludeParentGuid(HelpItem helpItem)
        {
            try
            {
                HelpItem cloneHelpItem = new HelpItem
                {
                    Answer = helpItem.Answer,
                    HelpItemGUID = Guid.NewGuid(),
                    Order = helpItem.Order,
                    Question = helpItem.Question
                };
                return cloneHelpItem;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private static AccessoryTemplate CloneAccessoryTemplateNotIncludeParentGuid(AccessoryTemplate accessoryTemplate)
        {
            try
            {
                AccessoryTemplate cloneAccessoryTemplate = new AccessoryTemplate
                {
                    AccessoryTemplateGUID = Guid.NewGuid(),
                    Heading = accessoryTemplate.Heading,
                    Order = accessoryTemplate.Order,
                    PasswordText = accessoryTemplate.PasswordText,
                    PrimaryButtonText = accessoryTemplate.PrimaryButtonText,
                    SecondaryButtonText = accessoryTemplate.SecondaryButtonText,
                    Text = accessoryTemplate.Text,
                    Type = accessoryTemplate.Type,
                    UserNameText = accessoryTemplate.UserNameText
                };
                return cloneAccessoryTemplate;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private static PageVariableGroup CloneVariableGroupNotIncludeParentGuid(PageVariableGroup group)
        {
            try
            {
                PageVariableGroup cloneGroup = new PageVariableGroup
                {
                    PageVariableGroupGUID = Guid.NewGuid(),
                    Name = group.Name,
                    Description = group.Description,
                    LastUpdated = DateTime.UtcNow
                };

                return cloneGroup;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private static ProgramRoom CloneProgramRoomNotIncludeParentGuid(ProgramRoom room)
        {
            try
            {
                ProgramRoom cloneRoom = new ProgramRoom
                {
                    ProgramRoomGUID = Guid.NewGuid(),
                    Name = room.Name,
                    Description = room.Description,
                    PrimaryThemeColor = room.PrimaryThemeColor,
                    SecondaryThemeColor = room.SecondaryThemeColor,
                    LastUpdated = DateTime.UtcNow
                };

                return cloneRoom;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private static EmailTemplate CloneEmailTemplateNotIncludeParentGuid(EmailTemplate template)
        {
            try
            {
                if (!template.EmailTemplateTypeReference.IsLoaded)
                {
                    template.EmailTemplateTypeReference.Load();
                }
                EmailTemplate emailTemplate = new EmailTemplate
                {
                    EmailTemplateGUID = Guid.NewGuid(),
                    EmailTemplateType = template.EmailTemplateType,
                    Name = template.Name,
                    Subject = template.Subject,
                    Body = template.Body,
                    LastUpdated = DateTime.UtcNow
                };

                return emailTemplate;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public static Session CloneSessionNotIncludeParentGuid(Session session, List<KeyValuePair<string, string>> cloneRelapseGUIDList)
        {
            try
            {
                Session cloneSession = new Session();
                cloneSession.SessionGUID = Guid.NewGuid();
                cloneSession.Name = string.Format("Copy of {0} on {1}", session.Name, DateTime.UtcNow.ToString());
                cloneSession.Day = session.Day;
                cloneSession.Description = session.Description;
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
                    SessionContent newSessionContent = CloneSessionContentNotIncludeParentGuid(sessionContent, cloneRelapseGUIDList, pageDictionary);
                    cloneSession.SessionContent.Add(newSessionContent);
                }
                UpdatePageContentNotIncludeParentGuid(cloneSession, pageDictionary);
                return cloneSession;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public static Session CloneSessionWithExistPageSequenceNotIncludeParentGuid(Session session)
        {
            try
            {
                Session cloneSession = new Session();
                cloneSession.SessionGUID = Guid.NewGuid();
                cloneSession.Name = string.Format("Copy of {0} on {1}", session.Name, DateTime.UtcNow.ToString());
                cloneSession.Day = session.Day;
                cloneSession.Description = session.Description;
                if (!session.SessionContent.IsLoaded)
                {
                    session.SessionContent.Load();
                }

                List<SessionContent> sessionContents = session.SessionContent.Where(s => s.IsDeleted != true).ToList();

                foreach (SessionContent sessionContent in sessionContents)
                {
                    if (!sessionContent.PageSequenceReference.IsLoaded)
                    {
                        sessionContent.PageSequenceReference.Load();
                    }
                    SessionContent newSessionContent = CloneSessionContentWithExistPageSequenceNotIncludeParentGuid(sessionContent);
                    cloneSession.SessionContent.Add(newSessionContent);
                }

                return cloneSession;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public static SessionContent CloneSessionContentWithExistPageSequenceNotIncludeParentGuid(SessionContent sessionContent)
        {
            try
            {
                if (!sessionContent.PageSequenceReference.IsLoaded)
                {
                    sessionContent.PageSequenceReference.Load();
                }
                if (!sessionContent.ProgramRoomReference.IsLoaded)
                {
                    sessionContent.ProgramRoomReference.Load();
                }
                SessionContent cloneSessionContent = new SessionContent();

                cloneSessionContent.SessionContentGUID = Guid.NewGuid();
                cloneSessionContent.PageSequence = sessionContent.PageSequence;
                cloneSessionContent.PageSequenceOrderNo = sessionContent.PageSequenceOrderNo;
                cloneSessionContent.ProgramRoom = sessionContent.ProgramRoom;

                return cloneSessionContent;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public static SessionContent CloneSessionContentNotIncludeParentGuid(SessionContent sessionContent, List<KeyValuePair<string, string>> cloneRelapseGUIDList, Dictionary<Guid, Guid> pageDictionary)
        {
            try
            {
                if (!sessionContent.PageSequenceReference.IsLoaded)
                {
                    sessionContent.PageSequenceReference.Load();
                }
                if (!sessionContent.ProgramRoomReference.IsLoaded)
                {
                    sessionContent.ProgramRoomReference.Load();
                }
                SessionContent cloneSessionContent = new SessionContent();

                cloneSessionContent.SessionContentGUID = Guid.NewGuid();
                cloneSessionContent.PageSequence = ClonePageSequenceNotIncludeParentGuid(sessionContent.PageSequence, cloneRelapseGUIDList, pageDictionary);
                cloneSessionContent.PageSequenceOrderNo = sessionContent.PageSequenceOrderNo;
                cloneSessionContent.ProgramRoom = sessionContent.ProgramRoom;

                return cloneSessionContent;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public static PageSequence ClonePageSequenceNotIncludeParentGuid(PageSequence pageSeq, List<KeyValuePair<string, string>> cloneRelapseGUIDList)
        {
            try
            {
                if (!pageSeq.InterventReference.IsLoaded)
                {
                    pageSeq.InterventReference.Load();
                }
                if (!pageSeq.Page.IsLoaded)
                {
                    pageSeq.Page.Load();
                }
                PageSequence newPageSeq = new PageSequence();
                newPageSeq.PageSequenceGUID = Guid.NewGuid();
                newPageSeq.Name = string.Format("Copy of {0} on {1}", pageSeq.Name, DateTime.UtcNow.ToString());
                newPageSeq.Description = pageSeq.Description;
                newPageSeq.Intervent = pageSeq.Intervent;
                newPageSeq.IsDeleted = false;

                List<Page> pages = pageSeq.Page.Where(p => p.IsDeleted != true).ToList();
                foreach (Page page in pages)
                {
                    Page newPage = ClonePageNotIncludeParentGuid(page, cloneRelapseGUIDList);
                    newPageSeq.Page.Add(newPage);
                }
                return newPageSeq;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public static PageSequence ClonePageSequenceNotIncludeParentGuid(PageSequence pageSeq, List<KeyValuePair<string, string>> cloneRelapseGUIDList, Dictionary<Guid, Guid> pageDictionary)
        {
            try
            {
                if (!pageSeq.InterventReference.IsLoaded)
                {
                    pageSeq.InterventReference.Load();
                }
                if (!pageSeq.Page.IsLoaded)
                {
                    pageSeq.Page.Load();
                }
                PageSequence newPageSeq = new PageSequence();
                newPageSeq.PageSequenceGUID = Guid.NewGuid();
                newPageSeq.Name = string.Format("Copy of {0} on {1}", pageSeq.Name, DateTime.UtcNow.ToString());
                newPageSeq.Description = pageSeq.Description;
                newPageSeq.Intervent = pageSeq.Intervent;
                newPageSeq.IsDeleted = false;

                List<Page> pages = pageSeq.Page.Where(p => p.IsDeleted != true).ToList();
                foreach (Page page in pages)
                {
                    Page newPage = ClonePageNotIncludeParentGuid(page, cloneRelapseGUIDList);
                    newPageSeq.Page.Add(newPage);
                    if (!pageDictionary.ContainsKey(page.PageGUID))
                        pageDictionary.Add(page.PageGUID, newPage.PageGUID);
                }
                return newPageSeq;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private static void UpdatePageContentNotIncludeParentGuid(Session newSession, Dictionary<Guid, Guid> pageDictionary)
        {
            try
            {
                foreach (SessionContent newSessionContent in newSession.SessionContent)
                {
                    foreach (Page newPage in newSessionContent.PageSequence.Page)
                    {
                        foreach (KeyValuePair<Guid, Guid> pagePair in pageDictionary)
                        {
                            if (newPage.PageContent != null && !string.IsNullOrEmpty(newPage.PageContent.AfterShowExpression) && newPage.PageContent.AfterShowExpression.Contains(pagePair.Key.ToString()))
                            {
                                newPage.PageContent.AfterShowExpression = newPage.PageContent.AfterShowExpression.Replace(pagePair.Key.ToString(), pagePair.Value.ToString());
                            }
                            if (newPage.PageContent != null && !string.IsNullOrEmpty(newPage.PageContent.BeforeShowExpression) && newPage.PageContent.BeforeShowExpression.Contains(pagePair.Key.ToString()))
                            {
                                newPage.PageContent.BeforeShowExpression = newPage.PageContent.BeforeShowExpression.Replace(pagePair.Key.ToString(), pagePair.Value.ToString());
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public static Page ClonePageNotIncludeParentGuid(Page page, List<KeyValuePair<string, string>> cloneRelapseGUIDList)
        {
            try
            {
                Page newPage = new Page();
                newPage.PageGUID = Guid.NewGuid();
                newPage.PageOrderNo = page.PageOrderNo;
                newPage.LastUpdated = page.LastUpdated;
                UserService us = new UserService();
                //newPage.LastUpdatedBy = us.GetCurrentUser().UserGuid;
                newPage.Created = page.Created;
                newPage.Wait = page.Wait;
                newPage.MaxPreferences = page.MaxPreferences;
                if (!page.PageVariableReference.IsLoaded)
                {
                    page.PageVariableReference.Load();
                }
                newPage.PageVariable = page.PageVariable;
                if (!page.PageTemplateReference.IsLoaded)
                {
                    page.PageTemplateReference.Load();
                }
                newPage.PageTemplate = page.PageTemplate;
                if (!page.PageContentReference.IsLoaded)
                {
                    page.PageContentReference.Load();
                }
                //List<PageContent> pageContents = page.PageContent.Where(p => p.IsDeleted != true).ToList();
                if (page.PageContent != null)
                {
                    newPage.PageContent = ClonePageContentNotIncludeParentGuid(page.PageContent, cloneRelapseGUIDList);
                }

                if (!page.PageQuestion.IsLoaded)
                {
                    page.PageQuestion.Load();
                }
                List<PageQuestion> pageQuestions = page.PageQuestion.Where(p => p.IsDeleted != true).ToList();
                foreach (PageQuestion pageQuestion in pageQuestions)
                {
                    newPage.PageQuestion.Add(ClonePageQuestionNotIncludeParentGuid(pageQuestion));
                }
                if (!page.PageMediaReference.IsLoaded)
                {
                    page.PageMediaReference.Load();
                }

                //List<PageMedia> pageMedias = page.PageMedia.Where(p => p.IsDeleted != true).ToList();
                //foreach (PageMedia pm in pageMedias)
                //{
                if (page.PageMedia != null)
                {
                    newPage.PageMedia = ClonePageMediaNotIncludeParentGuid(page.PageMedia);
                }
                //}
                if (!page.Preferences.IsLoaded)
                {
                    page.Preferences.Load();
                }
                List<Preferences> preferences = page.Preferences.Where(p => p.IsDeleted != true).ToList();
                foreach (Preferences pre in preferences)
                {
                    newPage.Preferences.Add(ClonePreferencesNotIncludeParentGuid(pre));
                }
                if (!page.Graph.IsLoaded)
                {
                    page.Graph.Load();
                }
                List<Graph> graphes = page.Graph.Where(g => g.IsDeleted != true).ToList();
                foreach (Graph graph in graphes)
                {
                    newPage.Graph.Add(CloneGraphNotIncludeParentGuid(graph));
                }
                return newPage;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private static Graph CloneGraphNotIncludeParentGuid(Graph graph)
        {
            try
            {
                Graph cloneGraph = new Graph();
                cloneGraph.BadScoreRange = graph.BadScoreRange;
                //cloneGraph.Caption = graph.Caption;
                cloneGraph.GoodScoreRange = graph.GoodScoreRange;
                cloneGraph.GraphGUID = Guid.NewGuid();
                cloneGraph.MediumRange = graph.MediumRange;
                cloneGraph.ScoreRange = graph.ScoreRange;
                cloneGraph.TimeRange = graph.TimeRange;
                cloneGraph.TimeUnit = graph.TimeUnit;
                cloneGraph.Type = graph.Type;
                if (!graph.GraphContentReference.IsLoaded)
                {
                    graph.GraphContentReference.Load();
                }
                //List<GraphContent> graphContents = graph.GraphContent.Where(g => g.IsDeleted != true).ToList();

                if (graph.GraphContent != null)
                {
                    cloneGraph.GraphContent = CloneGraphContentNotIncludeParentGuid(graph.GraphContent);
                }
                if (!graph.GraphItem.IsLoaded)
                {
                    graph.GraphItem.Load();
                }

                List<GraphItem> graphItems = graph.GraphItem.Where(g => g.IsDeleted != true).ToList();
                foreach (GraphItem item in graphItems)
                {
                    cloneGraph.GraphItem.Add(CloneGraphItemNotIncludeParentGuid(item));
                }

                return cloneGraph;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private static GraphContent CloneGraphContentNotIncludeParentGuid(GraphContent graphContent)
        {
            try
            {
                GraphContent cloneGraphContent = new GraphContent
                {
                    Caption = graphContent.Caption,
                    //Language = graphContent.Language,
                    TimeUnit = graphContent.TimeUnit
                };
                return cloneGraphContent;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private static GraphItem CloneGraphItemNotIncludeParentGuid(GraphItem item)
        {
            try
            {
                GraphItem cloneGraphItem = new GraphItem();
                cloneGraphItem.GraphItemGUID = Guid.NewGuid();
                //cloneGraphItem.Name = item.Name;
                cloneGraphItem.PointType = item.PointType;
                cloneGraphItem.DataItemExpression = item.DataItemExpression;
                cloneGraphItem.Color = item.Color;
                if (!item.GraphItemContentReference.IsLoaded)
                {
                    item.GraphItemContentReference.Load();
                }

                cloneGraphItem.GraphItemContent = CloneGraphItemContentNotIncludeParentGuid(item.GraphItemContent);
                return cloneGraphItem;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private static GraphItemContent CloneGraphItemContentNotIncludeParentGuid(GraphItemContent itemcontent)
        {
            try
            {
                GraphItemContent cloneGraphItemContent = new GraphItemContent
                {
                    //Language = itemcontent.Language,
                    Name = itemcontent.Name
                };

                return cloneGraphItemContent;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private static Preferences ClonePreferencesNotIncludeParentGuid(Preferences preferences)
        {
            try
            {
                Preferences clonePreferences = new Preferences();
                //if(!preferences.LanguageReference.IsLoaded)
                //{
                //    preferences.LanguageReference.Load();
                //}
                //clonePreferences.Language = preferences.Language;
                if (!preferences.ResourceReference.IsLoaded)
                {
                    preferences.ResourceReference.Load();
                }
                if (!preferences.PageVariableReference.IsLoaded)
                {
                    preferences.PageVariableReference.Load();
                }
                clonePreferences.PreferencesGUID = Guid.NewGuid();
                clonePreferences.Resource = preferences.Resource;
                clonePreferences.Name = preferences.Name;
                clonePreferences.AnswerText = preferences.AnswerText;
                clonePreferences.Description = preferences.Description;
                clonePreferences.ButtonName = preferences.ButtonName;
                clonePreferences.PageVariable = preferences.PageVariable;

                return clonePreferences;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public static PageMedia ClonePageMediaNotIncludeParentGuid(PageMedia pageMedia)
        {
            try
            {
                PageMedia clonePageMedia = new PageMedia();
                //if (!pageMedia.LanguageReference.IsLoaded)
                //{
                //    pageMedia.LanguageReference.Load();
                //}
                //clonePageMedia.Language = pageMedia.Language;
                if (!pageMedia.ResourceReference.IsLoaded)
                {
                    pageMedia.ResourceReference.Load();
                }
                clonePageMedia.Resource = pageMedia.Resource;
                clonePageMedia.Type = pageMedia.Type;

                return clonePageMedia;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public static PageContent ClonePageContentNotIncludeParentGuid(PageContent pageContent, List<KeyValuePair<string, string>> cloneRelapseGUIDList)
        {
            try
            {
                PageContent clonePageContent = new PageContent();
                clonePageContent.Heading = pageContent.Heading;
                clonePageContent.Body = pageContent.Body;
                clonePageContent.PrimaryButtonActionParameter = pageContent.PrimaryButtonActionParameter;
                //clonePageContent.SecondaryButtonActionParameter = pageContent.SecondaryButtonActionParameter;
                clonePageContent.PrimaryButtonCaption = pageContent.PrimaryButtonCaption;
                //clonePageContent.SecondaryButtonCaption = pageContent.SecondaryButtonCaption;
                //clonePageContent.Wait = pageContent.Wait;
                clonePageContent.PresenterImagePosition = pageContent.PresenterImagePosition;
                //clonePageContent.MaxPreferences = pageContent.MaxPreferences;

                clonePageContent.AfterShowExpression = pageContent.AfterShowExpression;
                clonePageContent.BeforeShowExpression = pageContent.BeforeShowExpression;
                //if(!string.IsNullOrEmpty(clonePageContent.AfterShowExpression)&&string.IsNullOrEmpty(clonePageContent.BeforeShowExpression))
                foreach (KeyValuePair<string, string> relapsePair in cloneRelapseGUIDList)
                {
                    if (!string.IsNullOrEmpty(clonePageContent.AfterShowExpression) && clonePageContent.AfterShowExpression.Contains(relapsePair.Key))
                    {
                        clonePageContent.AfterShowExpression = clonePageContent.AfterShowExpression.Replace(relapsePair.Key, relapsePair.Value);
                    }
                    if (!string.IsNullOrEmpty(clonePageContent.BeforeShowExpression) && clonePageContent.BeforeShowExpression.Contains(relapsePair.Key))
                    {
                        clonePageContent.BeforeShowExpression = clonePageContent.BeforeShowExpression.Replace(relapsePair.Key, relapsePair.Value);
                    }
                }

                if (!pageContent.Resource_PresenterImageReference.IsLoaded)
                {
                    pageContent.Resource_PresenterImageReference.Load();
                }
                clonePageContent.Resource_PresenterImage = pageContent.Resource_PresenterImage;

                if (!pageContent.Resource_BackgroundImageReference.IsLoaded)
                {
                    pageContent.Resource_BackgroundImageReference.Load();
                }
                clonePageContent.Resource_BackgroundImage = pageContent.Resource_BackgroundImage;

                //if (!pageContent.PageVariableReference.IsLoaded)
                //{
                //    pageContent.PageVariableReference.Load();
                //}
                //clonePageContent.PageVariable = pageContent.PageVariable;

                //if (!pageContent.LanguageReference.IsLoaded)
                //{
                //    pageContent.LanguageReference.Load();
                //}
                //clonePageContent.Language = pageContent.Language;
                return clonePageContent;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public static PageQuestion ClonePageQuestionNotIncludeParentGuid(PageQuestion pageQuestion)
        {
            try
            {
                PageQuestion clonePageQuestion = new PageQuestion();
                clonePageQuestion.Order = pageQuestion.Order;
                clonePageQuestion.PageQuestionGUID = Guid.NewGuid();
                clonePageQuestion.IsRequired = pageQuestion.IsRequired;

                if (!pageQuestion.QuestionReference.IsLoaded)
                {
                    pageQuestion.QuestionReference.Load();
                }
                clonePageQuestion.Question = pageQuestion.Question;

                //Clone page question content
                if (!pageQuestion.PageQuestionContentReference.IsLoaded)
                {
                    pageQuestion.PageQuestionContentReference.Load();
                }

                //List<PageQuestionContent> pageQuestionContents = pageQuestion.PageQuestionContent.Where(p => p.IsDeleted != true).ToList();
                if (pageQuestion.PageQuestionContent != null)
                {
                    clonePageQuestion.PageQuestionContent = ClonePageQuestionContentNotIncludeParentGuid(pageQuestion.PageQuestionContent);
                }

                // Clone page question's items
                if (!pageQuestion.PageQuestionItem.IsLoaded)
                {
                    pageQuestion.PageQuestionItem.Load();
                }

                List<PageQuestionItem> pageQuestionItems = pageQuestion.PageQuestionItem.Where(p => p.IsDeleted != true).ToList();
                foreach (PageQuestionItem questionItem in pageQuestionItems)
                {
                    clonePageQuestion.PageQuestionItem.Add(ClonePageQuestionItemNotIncludeParentGuid(questionItem));
                }

                // Clone page variable
                if (!pageQuestion.PageVariableReference.IsLoaded)
                {
                    pageQuestion.PageVariableReference.Load();
                }

                return clonePageQuestion;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public static PageQuestionContent ClonePageQuestionContentNotIncludeParentGuid(PageQuestionContent questionContent)
        {
            try
            {
                PageQuestionContent cloneQuestionContent = new PageQuestionContent();
                //if (!questionContent.LanguageReference.IsLoaded)
                //{
                //    questionContent.LanguageReference.Load();
                //}
                //cloneQuestionContent.Language = questionContent.Language;
                cloneQuestionContent.Caption = questionContent.Caption;
                return cloneQuestionContent;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public static PageQuestionItem ClonePageQuestionItemNotIncludeParentGuid(PageQuestionItem questionItem)
        {
            try
            {
                PageQuestionItem cloneQuestionItem = new PageQuestionItem();
                cloneQuestionItem.PageQuestionItemGUID = Guid.NewGuid();
                cloneQuestionItem.Order = questionItem.Order;
                if (!questionItem.PageQuestionItemContentReference.IsLoaded)
                {
                    questionItem.PageQuestionItemContentReference.Load();
                }
                //List<PageQuestionItemContent> pageQuestionItemContents = questionItem.PageQuestionItemContent.Where(p => p.IsDeleted != true).ToList();

                if (questionItem.PageQuestionItemContent != null)
                {
                    cloneQuestionItem.PageQuestionItemContent = ClonePageQuestionItemContentNotIncludeParentGuid(questionItem.PageQuestionItemContent);
                }
                cloneQuestionItem.Score = questionItem.Score;
                return cloneQuestionItem;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public static PageQuestionItemContent ClonePageQuestionItemContentNotIncludeParentGuid(PageQuestionItemContent pageQuestionItemContent)
        {
            try
            {
                PageQuestionItemContent clonePageQuestionItemContent = new PageQuestionItemContent();
                clonePageQuestionItemContent.Feedback = pageQuestionItemContent.Feedback;
                clonePageQuestionItemContent.Item = pageQuestionItemContent.Item;
                //if (!pageQuestionItemContent.LanguageReference.IsLoaded)
                //{
                //    pageQuestionItemContent.LanguageReference.Load();
                //}
                //clonePageQuestionItemContent.Language = pageQuestionItemContent.Language;
                //if (!pageQuestionItemContent.PageQuestionItemReference.IsLoaded)
                //{
                //    pageQuestionItemContent.PageQuestionItemReference.Load();
                //}
                return clonePageQuestionItemContent;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        #endregion

        //This function must be used after the PageVariableGroup has been copied.
        public static Program ClonePageVariableForProgram(Program oldProgram, Program newProgram)
        {
            try
            {
                if (!newProgram.PageVariableGroup.IsLoaded) newProgram.PageVariableGroup.Load();
                if (!newProgram.PageVariable.IsLoaded) newProgram.PageVariable.Load();
                if (!oldProgram.PageVariable.IsLoaded) oldProgram.PageVariable.Load();

                foreach (ChangeTech.Entities.PageVariable variable in oldProgram.PageVariable)
                {
                    if (newProgram.PageVariable.Where(p => p.Name == variable.Name).Count() == 0)
                    {
                        if (isVariableTypeGeneralByName(variable.Name))
                        {
                            variable.PageVariableType = VariableTypeEnum.General.ToString();
                        }
                        if (!variable.PageVariableGroupReference.IsLoaded)
                        {
                            variable.PageVariableGroupReference.Load();
                        }
                        ChangeTech.Entities.PageVariable cloneVariable = new ChangeTech.Entities.PageVariable();
                        cloneVariable.PageVariableGUID = Guid.NewGuid();
                        if (variable.PageVariableGroup != null)
                        {
                            cloneVariable.PageVariableGroup = newProgram.PageVariableGroup.Where(p => p.Name.Equals(variable.PageVariableGroup.Name)).FirstOrDefault();
                        }
                        cloneVariable.Name = variable.Name;
                        cloneVariable.Description = variable.Description;
                        cloneVariable.PageVariableType = variable.PageVariableType;
                        cloneVariable.ValueType = variable.ValueType;
                        newProgram.PageVariable.Add(cloneVariable);
                    }
                }

                return newProgram;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        ////if This five General is changed, change the Switch Case in the function of GetPageVariableByProgram in PageVariableService.cs
        public static bool isVariableTypeGeneralByName(string VariableName)
        {
            bool isGeneral = false;
            if (!string.IsNullOrEmpty(VariableName))
            {
                switch (VariableName)
                {
                    case "FirstName":
                    case "LastName":
                    case "Gender":
                    case "UserName":
                    case "MobilePhone":
                        isGeneral = true;
                        break;
                }
            }
            return isGeneral;
        }

        public static ResourceModel ParaseResourceModel(Resource res)
        {
            try
            {
                ResourceModel resourceModel = new ResourceModel();
                resourceModel.ID = res.ResourceGUID;
                resourceModel.Name = res.Name;
                resourceModel.Type = res.Type;
                resourceModel.Extension = res.FileExtension;
                resourceModel.NameOnServer = res.NameOnServer;
                resourceModel.LastUpdated = res.LastUpdated.HasValue ? res.LastUpdated.Value : new DateTime(2009, 1, 1);

                if (!res.ResourceCategoryReference.IsLoaded)
                {
                    res.ResourceCategoryReference.Load();
                }
                resourceModel.ResourceCategoryGUID = res.ResourceCategory.ResourceCategoryGUID;
                //TODO:need to change type to enum, then remove .ToString()
                if (res.Type.Equals(ResourceTypeEnum.Image.ToString()))
                {
                    resourceModel.ProgramImageReference = new List<ProgramImageReference>();
                    //CheckIsThereImagesInPageContent(res, resourceModel);
                    //CheckIsThereImagesInPageContent1(res, resourceModel);
                    //CheckIsThereImagesInPageMedia(res, resourceModel);
                    //CheckIsThereImagesInPreference(res, resourceModel);
                }

                return resourceModel;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public static List<ProgramImageReference> GetResourceReferenceInfo(Resource res)
        {
            try
            {
                if (res != null)
                {
                    ResourceModel resourceModel = new ResourceModel();
                    resourceModel.ProgramImageReference = new List<ProgramImageReference>();
                    CheckIsThereImagesInPageContent(res, resourceModel);
                    CheckIsThereImagesInPageContent1(res, resourceModel);
                    CheckIsThereImagesInPageMedia(res, resourceModel);
                    CheckIsThereImagesInPreference(res, resourceModel);
                    return resourceModel.ProgramImageReference;
                }
                else
                {
                    return new List<ProgramImageReference>();
                }
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private static void CheckIsThereImagesInPageContent(Resource res, ResourceModel resourceModel)
        {
            try
            {
                if (res.PageContent != null)
                {
                    if (!res.PageContent.IsLoaded)
                    {
                        res.PageContent.Load();
                    }

                    foreach (PageContent pageContent in res.PageContent)
                    {
                        if (!pageContent.PageReference.IsLoaded)
                        {
                            pageContent.PageReference.Load();
                        }

                        if (!pageContent.Page.PageSequenceReference.IsLoaded)
                        {
                            pageContent.Page.PageSequenceReference.Load();
                        }

                        if (pageContent.Page.PageSequence.SessionContent != null)
                        {
                            if (!pageContent.Page.PageSequence.SessionContent.IsLoaded)
                            {
                                pageContent.Page.PageSequence.SessionContent.Load();
                            }

                            if (!(pageContent.IsDeleted.HasValue && pageContent.IsDeleted.Value) &&
                                !(pageContent.Page.IsDeleted.HasValue && pageContent.Page.IsDeleted.Value) &&
                                !(pageContent.Page.PageSequence.IsDeleted.HasValue && pageContent.Page.PageSequence.IsDeleted.Value)
                               )
                            {
                                CaculateImageReferenceCount(resourceModel, pageContent.Page.PageSequence.SessionContent);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private static void CheckIsThereImagesInPageContent1(Resource res, ResourceModel resourceModel)
        {
            try
            {
                if (res.PageContent1 != null)
                {
                    if (!res.PageContent1.IsLoaded)
                    {
                        res.PageContent1.Load();
                    }

                    foreach (PageContent pageContent in res.PageContent1)
                    {
                        if (!pageContent.PageReference.IsLoaded)
                        {
                            pageContent.PageReference.Load();
                        }

                        if (!pageContent.Page.PageSequenceReference.IsLoaded)
                        {
                            pageContent.Page.PageSequenceReference.Load();
                        }

                        if (pageContent.Page.PageSequence.SessionContent != null)
                        {
                            if (!pageContent.Page.PageSequence.SessionContent.IsLoaded)
                            {
                                pageContent.Page.PageSequence.SessionContent.Load();
                            }

                            if (!(pageContent.IsDeleted.HasValue && pageContent.IsDeleted.Value) &&
                               !(pageContent.Page.IsDeleted.HasValue && pageContent.Page.IsDeleted.Value) &&
                               !(pageContent.Page.PageSequence.IsDeleted.HasValue && pageContent.Page.PageSequence.IsDeleted.Value)
                               )
                            {
                                CaculateImageReferenceCount(resourceModel, pageContent.Page.PageSequence.SessionContent);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private static void CheckIsThereImagesInPageMedia(Resource res, ResourceModel resourceModel)
        {
            try
            {
                if (res.PageMedia != null)
                {
                    if (!res.PageMedia.IsLoaded)
                    {
                        res.PageMedia.Load();
                    }

                    foreach (PageMedia pageMedia in res.PageMedia)
                    {
                        if (!pageMedia.PageReference.IsLoaded)
                        {
                            pageMedia.PageReference.Load();
                        }

                        if (!pageMedia.Page.PageSequenceReference.IsLoaded)
                        {
                            pageMedia.Page.PageSequenceReference.Load();
                        }

                        if (!pageMedia.Page.PageSequence.SessionContent.IsLoaded)
                        {
                            pageMedia.Page.PageSequence.SessionContent.Load();
                        }

                        if (!(pageMedia.IsDeleted.HasValue && pageMedia.IsDeleted.Value) &&
                            !(pageMedia.Page.IsDeleted.HasValue && pageMedia.Page.IsDeleted.Value) &&
                            !(pageMedia.Page.PageSequence.IsDeleted.HasValue && pageMedia.Page.PageSequence.IsDeleted.Value)
                            )
                        {
                            CaculateImageReferenceCount(resourceModel, pageMedia.Page.PageSequence.SessionContent);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private static void CheckIsThereImagesInPreference(Resource res, ResourceModel resourceModel)
        {
            try
            {
                if (res.Preferences != null)
                {
                    if (!res.Preferences.IsLoaded)
                    {
                        res.Preferences.Load();
                    }

                    foreach (Preferences preferences in res.Preferences)
                    {
                        if (!preferences.PageReference.IsLoaded)
                        {
                            preferences.PageReference.Load();
                        }

                        if (!preferences.Page.PageSequenceReference.IsLoaded)
                        {
                            preferences.Page.PageSequenceReference.Load();
                        }

                        if (!preferences.Page.PageSequence.SessionContent.IsLoaded)
                        {
                            preferences.Page.PageSequence.SessionContent.Load();
                        }

                        if (!(preferences.IsDeleted.HasValue && preferences.IsDeleted.Value) &&
                            !(preferences.Page.IsDeleted.HasValue && preferences.Page.IsDeleted.Value) &&
                            !(preferences.Page.PageSequence.IsDeleted.HasValue && preferences.Page.PageSequence.IsDeleted.Value)
                           )
                        {
                            CaculateImageReferenceCount(resourceModel, preferences.Page.PageSequence.SessionContent);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private static void CaculateImageReferenceCount(ResourceModel resourceModel, EntityCollection<SessionContent> sessionContents)
        {
            try
            {
                foreach (SessionContent sessionContent in sessionContents)
                {
                    if (!sessionContent.SessionReference.IsLoaded)
                    {
                        sessionContent.SessionReference.Load();
                    }

                    if (!sessionContent.Session.ProgramReference.IsLoaded)
                    {
                        sessionContent.Session.ProgramReference.Load();
                    }

                    if (!(sessionContent.IsDeleted.HasValue && sessionContent.IsDeleted.Value) &&
                        !(sessionContent.Session.IsDeleted.HasValue && sessionContent.Session.IsDeleted.Value) &&
                        !(sessionContent.Session.Program.IsDeleted.HasValue && sessionContent.Session.Program.IsDeleted.Value))
                    {

                        int indexOfProgramImageReference = -1;
                        foreach (ProgramImageReference pi in resourceModel.ProgramImageReference)
                        {
                            if (pi.ProgramGUID == sessionContent.Session.Program.ProgramGUID)
                            {
                                indexOfProgramImageReference = resourceModel.ProgramImageReference.IndexOf(pi);
                                break;
                            }
                        }

                        if (indexOfProgramImageReference > -1)
                        {
                            int indexOfSessionImageReference = -1;
                            foreach (SessionImageReference si in resourceModel.ProgramImageReference[indexOfProgramImageReference].SessionImageReference)
                            {
                                if (si.SessionGUID == sessionContent.Session.SessionGUID)
                                {
                                    indexOfSessionImageReference = resourceModel.ProgramImageReference[indexOfProgramImageReference].SessionImageReference.IndexOf(si);
                                    break;
                                }
                            }

                            if (indexOfSessionImageReference > -1)
                            {
                                resourceModel.ProgramImageReference[indexOfProgramImageReference].SessionImageReference[indexOfSessionImageReference].ReferenceCount++;
                            }
                            else
                            {
                                SessionImageReference sessionImageReference = new SessionImageReference();
                                sessionImageReference.SessionGUID = sessionContent.Session.SessionGUID;
                                sessionImageReference.Day = sessionContent.Session.Day.Value;
                                sessionImageReference.ReferenceCount = 1;
                                resourceModel.ProgramImageReference[indexOfProgramImageReference].SessionImageReference.Add(sessionImageReference);
                            }
                        }
                        else
                        {
                            ProgramImageReference programImageReference = new ProgramImageReference();
                            programImageReference.ProgramGUID = sessionContent.Session.Program.ProgramGUID;
                            programImageReference.ProgramName = sessionContent.Session.Program.Name;
                            programImageReference.SessionImageReference = new List<SessionImageReference>();

                            SessionImageReference sessionImageReference = new SessionImageReference();
                            sessionImageReference.SessionGUID = sessionContent.Session.SessionGUID;
                            sessionImageReference.Day = sessionContent.Session.Day.Value;
                            sessionImageReference.ReferenceCount = 1;

                            programImageReference.SessionImageReference.Add(sessionImageReference);
                            resourceModel.ProgramImageReference.Add(programImageReference);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public static string GetBlobPath(string azureStorageAccount)
        {
            try
            {
                return string.Format("https://{0}.blob.core.windows.net/", azureStorageAccount);
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public static string GetQueuePath(string azureStorageAccount)
        {
            try
            {
                return string.Format("https://{0}.queue.core.windows.net/", azureStorageAccount);
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public static string GetTablePath(string azureStorageAccount)
        {
            try
            {
                return string.Format("https://{0}.table.core.windows.net/", azureStorageAccount);
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public static string GetPinCode()
        {
            try
            {
                string pinCode = string.Empty;
                Random random = new Random();
                for (int i = 0; i < 4; i++)
                {
                    pinCode += random.Next(9).ToString();
                }

                return pinCode;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public static void ResetObjectContext()
        {
            try
            {
                RepositoryBase.ResetContainer();
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        // need to return a model which our developer can easy understand the return value.
        // also need to move this method to another service, it is not belongs to this service.
        public static string GetValidateUserMessageForNotRetakeAfterLogin(string userName, Guid userGuid, Guid programGuid, int day, HttpContext context, Guid languageGuid, bool isFromPincode)
        {
            string returnMessage = string.Empty;
            IContainerContext containerContext = ContainerManager.GetContainer("container");
            string userStatus = containerContext.Resolve<IUserService>().GetUserStatus(userName, programGuid);
            if (userStatus.Equals(ProgramUserStatusEnum.Paused.ToString()))
            {
                returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString()+";" + programGuid + ";" + userGuid.ToString() + ";" + LoginFailedTypeEnum.ProgramIsPaused;
            }
            else
            {
                //set user's currenttime according to TimeZone.
                DateTime setCurrentTimeByTimeZone = containerContext.Resolve<IProgramUserService>().GetCurrentTimeByTimeZone(programGuid, userGuid, DateTime.UtcNow);
                int isThereClassReturnCode = containerContext.Resolve<IProgramUserService>().IsThereClassToday(userGuid, programGuid, setCurrentTimeByTimeZone);
                if (isThereClassReturnCode == 0)
                {
                    if (!isFromPincode && containerContext.Resolve<IProgramService>().IsProgramNeedPinCode(programGuid))
                    {
                        // if need pin code, go to pincode validate page else go through session
                        returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";" + containerContext.Resolve<IProgramAccessoryService>().GetPinCodePageModelAsXML(programGuid, languageGuid, userGuid);
                    }
                    else
                    {
                        returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";" + GetCurrentSessionXML(userGuid, programGuid, languageGuid, day, context, false);
                    }
                }
                else if (isThereClassReturnCode == 1)
                {
                    //responseResult = string.Format(FAKE_PAGE_XML, containerContext.Resolve<IProgramService>().GetProgramLogo(new Guid(programGuid)), "Information", "Welcome you, your class will begin from next Monday.", "OK");
                    returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString()+";" + programGuid + ";" + userGuid.ToString() + ";" + LoginFailedTypeEnum.WaitUntilNextModay;
                    InsertLogModel model = new InsertLogModel
                    {
                        ActivityLogType = (int)LogTypeEnum.Login,
                        Browser = context.Request.UserAgent,
                        IP = context.Request.UserHostAddress,
                        Message = "Wait for next monday",
                        ProgramGuid = programGuid,
                        SessionGuid = Guid.Empty,
                        UserGuid = userGuid,
                    };
                    containerContext.Resolve<IActivityLogService>().Insert(model);
                }
                else if (isThereClassReturnCode == 5)
                {
                    if (!isFromPincode && containerContext.Resolve<IProgramService>().IsProgramNeedPinCode(programGuid))
                    {
                        returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";" + containerContext.Resolve<IProgramAccessoryService>().GetPinCodePageModelAsXML(programGuid, languageGuid, userGuid);
                    }
                    else
                    {
                        // should do outline work DTD-1001
                        int outlineday = containerContext.Resolve<IProgramUserService>().GetOutlineDay(setCurrentTimeByTimeZone);
                        returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";" + GetCurrentSessionXML(userGuid, programGuid, languageGuid, outlineday, context, false);
                    }
                }
                else if (isThereClassReturnCode == 6)
                {
                    // should pay for program
                    returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";" + containerContext.Resolve<IProgramAccessoryService>().GetPaymentModelAsXML(programGuid, languageGuid, userGuid);
                }
                else if (isThereClassReturnCode == 7)
                {
                    returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString()+";" + programGuid + ";" + userGuid.ToString() + ";" + LoginFailedTypeEnum.NoSchedule;
                }
                else if (isThereClassReturnCode == 8)
                {
                    returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString()+";" + programGuid + ";" + userGuid.ToString() + ";" + LoginFailedTypeEnum.NoScheduleForCurrentSession;
                }
                else if (isThereClassReturnCode == -1)// have error
                {
                    returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString()+";" + programGuid + ";" + userGuid.ToString() + ";" + LoginFailedTypeEnum.ErrorExist;
                }
                else
                {
                    returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString()+";" + programGuid + ";" + userGuid.ToString() + ";" + LoginFailedTypeEnum.HaveFinishedTodaysClass;
                    InsertLogModel model = new InsertLogModel
                    {
                        ActivityLogType = (int)LogTypeEnum.Login,
                        Browser = context.Request.UserAgent,
                        IP = context.Request.UserHostAddress,
                        Message = "Have completed all class",
                        ProgramGuid = programGuid,
                        SessionGuid = Guid.Empty,
                        UserGuid = userGuid
                    };
                    containerContext.Resolve<IActivityLogService>().Insert(model);
                }
            }

            // Get tip message
            string[] loginFailedString = returnMessage.Split(';');
            if (loginFailedString[0].Equals("0"))
            {
                ProgramModel programModel = containerContext.Resolve<IProgramService>().GetProgramByGUID(programGuid);
                if (programModel.IsCTPPEnable)
                {
                    //returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString()+";" + StringUtility.MD5Encrypt(returnMessage, MD5_KEY);
                    returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.CtppCode).ToString() + ";" + StringUtility.MD5Encrypt(returnMessage, MD5_KEY);
                }
                else
                {
                    returnMessage = containerContext.Resolve<ITipMessageService>().GetTipMessageText(programGuid, loginFailedString[3]);
                    returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";" + string.Format(containerContext.Resolve<ISessionService>().GetEmptySessionXML(programGuid, Guid.Empty, languageGuid), returnMessage);
                }
            }
            return returnMessage;
        }


        private static string GetCurrentSessionXML(Guid userguid, Guid programguid, Guid languageguid, int day, HttpContext context, bool isRetake)
        {
            IContainerContext containerContext = ContainerManager.GetContainer("container");
            string returnMessage = string.Empty;
            returnMessage = containerContext.Resolve<ISessionService>().GetLiveSessionModelAsXML(userguid, programguid, languageguid, day);
            Guid sessionGuid = containerContext.Resolve<ISessionService>().GetSessionGuidByProgarmAndDay(programguid, day);
            returnMessage = containerContext.Resolve<IXMLService>().PraseGraphPage(returnMessage, userguid, sessionGuid, languageguid, isRetake);
            returnMessage = containerContext.Resolve<IXMLService>().ParseBeforeAfterShowExpression(returnMessage, sessionGuid, "Session", programguid.ToString(), userguid.ToString());
            returnMessage = containerContext.Resolve<IXMLService>().ParseTimePickerQuestion(returnMessage, languageguid);
            // add log
            InsertLogModel model = new InsertLogModel
            {
                ActivityLogType = (int)LogTypeEnum.Login,
                Browser = context.Request.UserAgent,
                IP = context.Request.UserHostAddress,
                Message = "Login Sucessful",
                ProgramGuid = programguid,
                SessionGuid = sessionGuid,
                UserGuid = userguid
            };
            containerContext.Resolve<IActivityLogService>().Insert(model);
            model.Message = "Session Begin";
            model.ActivityLogType = (int)LogTypeEnum.StartDay;
            containerContext.Resolve<IActivityLogService>().Insert(model);

            return returnMessage;
        }

        public static string GetPromotionCode()
        {
            try
            {
                string promotionCode = string.Empty;
                Random random = new Random();
                for (int i = 0; i < 4; i++)
                {
                    promotionCode += random.Next(9).ToString();
                }

                return promotionCode;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        #region judge whether the device to visit our web is smart phone
        private static List<String> sMobileHints = new List<string>(new string[] {
            "midp", "j2me", "avant", "docomo", 
            "novarra", "palmos", "palmsource", 
            "240x320", "opwv", "chtml",
            "pda", "windows ce", "mmp/", 
            "blackberry", "mib/", "symbian", 
            "wireless", "nokia", "hand", "mobi",
            "phone", "cdm", "up.b", "audio", 
            "SIE-", "SEC-", "samsung", "HTC", 
            "mot-", "mitsu", "sagem", "sony",
            "alcatel", "lg", "eric", "vx", 
            "NEC", "philips", "mmm", "xx", 
            "panasonic", "sharp", "wap", "sch",
            "rover", "pocket", "benq", "java", 
            "pt", "pg", "vox", "amoi", 
            "bird", "compal", "kg", "voda",
            "sany", "kdd", "dbt", "sendo", 
            "sgh", "gradi", "jb", "dddi", 
            "moto", "sonyericsson",
            "iphone","android","wp","windows phone"});

        public static bool IsMobileDevice
        {
            get
            {
                bool isMobile = false;
                if (null == HttpContext.Current) return false;

                var serverVariable = HttpContext.Current.Request.ServerVariables["HTTP_ACCEPT"];
                var wapHeader = HttpContext.Current.Request.Headers["HTTP_X_WAP_PROFILE"];

                isMobile = HttpContext.Current.Request.Browser.IsMobileDevice
                || wapHeader != null
                || (serverVariable != null && serverVariable.ToLower().Contains("wap"));

                //As the sMobileHints will cause a bug(DTD-22 in hopper), I modified the old codes.
                //http://detectmobilebrowsers.com/
                //http://detectmobilebrowsers.com/mobile
                //http://www.useragentstring.com/pages/useragentstring.php
                if (!isMobile)
                {
                    string agent = HttpContext.Current.Request.UserAgent;
                    Regex b = new Regex(@"android.+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|meego.+mobile|midp|mmp|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                    Regex v = new Regex(@"1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(di|rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                    if ((b.IsMatch(agent) || v.IsMatch(agent.Substring(0, 4))))
                    {
                        isMobile = true;
                    }
                }
                return isMobile;
            }
        }
        #endregion

    }
}
