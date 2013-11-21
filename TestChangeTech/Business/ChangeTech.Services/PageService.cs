using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using ChangeTech.Contracts;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;
using ChangeTech.Models;
using Ethos.Utility;
using Ethos.DependencyInjection;
using System.Reflection;
using System.Text;

namespace ChangeTech.Services
{
    public class PageService : ServiceBase, IPageService
    {
        #region const of download resource
        public const string LINK_A_START = "<A";
        public const string LINK_A_END = "</A>";
        public const string ORIGINAL_IMAGE_DIRECTORY = "originalimagecontainer/";
        public const string LI_START = "<LI";
        public const string LI_END = "</LI>";
        public const string CLASS_VIDEO = "video";
        public const string CLASS_AUDIO = "audio";
        public const string CLASS_DOCUMENT = "document";
        public const string CLASS_IMAGE = "image";
        public const string REQUEST_RESOURCE = "RequestResource.aspx?target=";
        public const string MEDIA = "&media=";
        #endregion
        #region IPageService Members

        public Guid GetPageGuidByPageSequenceAndOrder(Guid pageSequenceGuid, int order)
        {
            Guid pageGuid = Guid.Empty;
            Page pageEntity = Resolve<IPageRepository>().GetPageByPageSequenceAndOrderNo(pageSequenceGuid, order);
            if(pageEntity != null)
            {
                pageGuid = pageEntity.PageGUID;
            }
            return pageGuid;
        }

        public Guid GetPageGuidByPageSequenceAndOldPageGuid(Guid pageSequence, Guid oldPageGuid)
        {
            Guid pageGuid = Guid.Empty;
            Page oldPage = Resolve<IPageRepository>().GetPageByPageGuid(oldPageGuid);
            if(oldPage != null)
            {
                Page newPage = Resolve<IPageRepository>().GetPageByPageSequenceAndOrderNo(pageSequence, oldPage.PageOrderNo);
                if(newPage != null)
                {
                    pageGuid = newPage.PageGUID;
                }
            }
            return pageGuid;
        }

        public Guid GetLastPageGuidOfSession(Guid sessionGuid)
        {
            Guid lastPageGuid = Guid.Empty;
            try
            {
                Session session = Resolve<ISessionRepository>().GetSessionBySessionGuid(sessionGuid);
                if(!session.SessionContent.IsLoaded)
                {
                    session.SessionContent.Load();
                }
                SessionContent sessionContent = session.SessionContent.OrderBy(s => s.PageSequenceOrderNo).Last();
                if(!sessionContent.PageSequenceReference.IsLoaded)
                {
                    sessionContent.PageSequenceReference.Load();
                }
                if(!sessionContent.PageSequence.Page.IsLoaded)
                {
                    sessionContent.PageSequence.Page.Load();
                }

                Page lastPage = sessionContent.PageSequence.Page.OrderByDescending(p => p.PageOrderNo).FirstOrDefault();
                lastPageGuid = lastPage.PageGUID;
            }
            catch(Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("{0}:{1}", MethodBase.GetCurrentMethod().Name, sessionGuid));
                throw ex;
            }
            return lastPageGuid;
        }

        public void AdjustPageOrderUp(Guid pageSequenceGuid, Guid pageGuid)
        {
            try
            {
                Page adjustPage = Resolve<IPageRepository>().GetPageByPageGuid(pageGuid);
                Page onAdjustPage = Resolve<IPageRepository>().GetPageByPageSequenceAndOrderNo(pageSequenceGuid, adjustPage.PageOrderNo - 1);

                if(adjustPage != null)
                {
                    adjustPage.PageOrderNo--;
                    Resolve<IPageRepository>().UpdatePage(adjustPage);
                }
                if(onAdjustPage != null)
                {
                    onAdjustPage.PageOrderNo++;
                    Resolve<IPageRepository>().UpdatePage(onAdjustPage);
                }
            }
            catch(Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("{0}:{1},{2}", MethodBase.GetCurrentMethod().Name, pageSequenceGuid, pageGuid));
                throw ex;
            }
        }

        public void AdjustPageOrderDown(Guid pageSequenceGuid, Guid pageGuid)
        {
            try
            {
                Page adjustPage = Resolve<IPageRepository>().GetPageByPageGuid(pageGuid);
                Page downAdjustPage = Resolve<IPageRepository>().GetPageByPageSequenceAndOrderNo(pageSequenceGuid, adjustPage.PageOrderNo + 1);

                if(adjustPage != null)
                {
                    adjustPage.PageOrderNo++;
                    Resolve<IPageRepository>().UpdatePage(adjustPage);
                }

                if(downAdjustPage != null)
                {
                    downAdjustPage.PageOrderNo--;
                    Resolve<IPageRepository>().UpdatePage(downAdjustPage);
                }
            }
            catch(Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("{0}:{1},{2}", MethodBase.GetCurrentMethod().Name, pageSequenceGuid, pageGuid));
                throw ex;
            }
        }

        public void AdjustPageOrderForPageReview(PageUpdateForPageReviewModel originalModel,PageUpdateForPageReviewModel swapToModel)
        {
            try
            {
                //preLoad original
                Guid orginPageSequenceGuid = Guid.Empty;
                if (originalModel.IsEditPageSequenceSelf)
                {
                    orginPageSequenceGuid = originalModel.PageSequenceGUID;
                }
                else
                {
                    orginPageSequenceGuid = Resolve<IPageSequenceService>().BeforeEditPageSequenceAfterRefactoring(originalModel.SessionGUID, originalModel.PageSequenceGUID, originalModel.IsUpdatePageSequence);
                }
                Guid orginPageGuid = Resolve<IPageService>().GetPageGuidByPageSequenceAndOrder(orginPageSequenceGuid, originalModel.PageOrder);

                //preload swapTo
                Guid swapPageSequenceGuid = Guid.Empty;
                if (originalModel.PageSequenceGUID == swapToModel.PageSequenceGUID)
                {
                    /*
                     *  orginPageSequenceGuid may exist a new GUID when originalModel.IsEditPageSequenceSelf==false
                     *  in this case, if originalModel.PageSequenceGUID == swapToModel.PageSequenceGUID3
                     *  then their pageSequence should be updated to new one together
                     */
                    swapPageSequenceGuid = orginPageSequenceGuid;
                }
                else
                {
                    if (swapToModel.IsEditPageSequenceSelf)
                    {
                        swapPageSequenceGuid = swapToModel.PageSequenceGUID;
                    }
                    else
                    {
                        swapPageSequenceGuid = Resolve<IPageSequenceService>().BeforeEditPageSequenceAfterRefactoring(swapToModel.SessionGUID, swapToModel.PageSequenceGUID, swapToModel.IsUpdatePageSequence);
                    }
                }
                Guid swapPageGuid = Resolve<IPageService>().GetPageGuidByPageSequenceAndOrder(swapPageSequenceGuid, swapToModel.PageOrder);


                //begin to swap to each other
                int tempPageOrder = -1;
                PageSequence tempPageSequence = null;
                Page originalPage = Resolve<IPageRepository>().GetPageByPageGuid(orginPageGuid);
                Page swapToPage = Resolve<IPageRepository>().GetPageByPageGuid(swapPageGuid);
                if (orginPageSequenceGuid == swapPageSequenceGuid)//in the same page sequence
                {
                    tempPageOrder = originalPage.PageOrderNo;

                    originalPage.PageOrderNo = swapToPage.PageOrderNo;
                    Resolve<IPageRepository>().UpdatePage(originalPage);

                    swapToPage.PageOrderNo = tempPageOrder;
                    Resolve<IPageRepository>().UpdatePage(swapToPage);
                }
                else//page in different pageSequence swap to each other
                {
                    tempPageOrder = originalPage.PageOrderNo;

                    if (!originalPage.PageSequenceReference.IsLoaded)
                    {
                        originalPage.PageSequenceReference.Load();
                    }
                    if (originalPage.PageSequence != null)
                    {
                        tempPageSequence = originalPage.PageSequence;
                    }

                    if (!swapToPage.PageSequenceReference.IsLoaded)
                    {
                        swapToPage.PageSequenceReference.Load();
                    }
                    if (swapToPage.PageSequence != null)
                    {
                        originalPage.PageSequence = swapToPage.PageSequence;
                    }
                    else
                    {
                        originalPage.PageSequence = null;
                    }

                    originalPage.PageOrderNo = swapToPage.PageOrderNo;
                    Resolve<IPageRepository>().UpdatePage(originalPage);

                    swapToPage.PageSequence = tempPageSequence;
                    swapToPage.PageOrderNo = tempPageOrder;
                    Resolve<IPageRepository>().UpdatePage(swapToPage);
                }
            }
            catch(Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }


        public bool IsPageHasMoreReference(Guid SessionGuid, Guid PageSequenceGuid)
        {
            try
            {
                bool flag = false;
                if (SessionGuid == Guid.Empty)
                {
                    flag = Resolve<IPageSequenceService>().PageSequenceReferenced(PageSequenceGuid);
                }
                else
                {
                    flag = Resolve<IPageSequenceService>().PageSequenceInMoreSession(SessionGuid, PageSequenceGuid);
                }

                return flag;
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }
        public void DeletePageForPageReview(PageUpdateForPageReviewModel deleteModel)
        {
            try
            {
                Guid pageSequenceGuid = Guid.Empty;
                if (deleteModel.IsEditPageSequenceSelf)
                {
                    pageSequenceGuid = deleteModel.PageSequenceGUID;
                }
                else
                {
                    pageSequenceGuid = Resolve<IPageSequenceService>().BeforeEditPageSequenceAfterRefactoring(deleteModel.SessionGUID, deleteModel.PageSequenceGUID, deleteModel.IsUpdatePageSequence);
                }
                Guid pageGuid = Resolve<IPageService>().GetPageGuidByPageSequenceAndOrder(pageSequenceGuid, deleteModel.PageOrder);
                DeletePage(pageGuid);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }


        public void DeletePage(Guid pageGuid)
        {
            try
            {
                Page delPage = Resolve<IPageRepository>().GetPageByPageGuid(pageGuid);
                AdjustPage(delPage);
                Resolve<IPageContentRepository>().Delete(pageGuid);

                if(!delPage.PageQuestion.IsLoaded)
                {
                    delPage.PageQuestion.Load();
                }

                if(delPage.PageQuestion != null)
                {
                    foreach(PageQuestion pageQuestion in delPage.PageQuestion)
                    {

                        Resolve<IPageQuestionContentRepository>().DeletePageQuestionContent(pageQuestion.PageQuestionGUID);

                        if(!pageQuestion.PageQuestionItem.IsLoaded)
                        {
                            pageQuestion.PageQuestionItem.Load();
                        }

                        //foreach (PageQuestionItem questionItem in pageQuestion.PageQuestionItem)
                        //{                            
                        //    Resolve<IPageQuestionItemContentRepository>().DeletePageQuestionItemContent(questionItem
                        //}

                        // delete question item content
                        Resolve<IPageQuestionItemContentRepository>().DeletePageQuestionItemContentByQuestionGUID(pageQuestion.PageQuestionGUID);
                        // delete question item
                        Resolve<IPageQuestionItemRepository>().DeleteQuestionItems(pageQuestion.PageQuestionItem);
                    }
                    Resolve<IPageQuestionRepository>().DeletePageQuestions(delPage.PageQuestion);
                }

                Resolve<IPageMediaRepository>().DeletePageMedia(pageGuid);

                if(!delPage.Preferences.IsLoaded)
                {
                    delPage.Preferences.Load();
                }
                if(delPage.Preferences != null)
                {
                    Resolve<IPreferencesRepository>().DeletePreferences(delPage.Preferences);
                }

                if(!delPage.Graph.IsLoaded)
                {
                    delPage.Graph.Load();
                }
                if(delPage.Graph != null)
                {
                    foreach(Graph graph in delPage.Graph)
                    {
                        Resolve<IGraphContentRepository>().Delete(graph.GraphGUID);

                        if(!graph.GraphItem.IsLoaded)
                        {
                            graph.GraphItem.Load();
                        }
                        if(graph.GraphItem != null)
                        {
                            //foreach (GraphItem item in graph.GraphItem)
                            //{
                            //    Resolve<IGraphItemContentRepository>().Delete(item.GraphItemGUID);
                            //}

                            Resolve<IGraphItemContentRepository>().DeleteByGraphGUID(graph.GraphGUID);
                            Resolve<IGraphItemRepository>().DeleteByGraphGuid(graph.GraphGUID);
                        }
                    }
                }

                //delete download resource from PageDownloadResource datatable
                Resolve<IPageResourceRepository>().DeletePageResource(pageGuid);


                Resolve<IPageRepository>().DeletePage(pageGuid);
            }
            catch(Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("{0}:{1}", MethodBase.GetCurrentMethod().Name, pageGuid));
                throw ex;
            }
        }

        /// <summary>
        /// In this function. The copied Page is in the same program with the original Page.
        /// </summary>
        /// <param name="pageGuid"></param>
        public void MakeCopyPage(Guid pageGuid)
        {
            try
            {
                //int pageOrderNo = 1;
                Page copiedPage = Resolve<IPageRepository>().GetPageByPageGuid(pageGuid);
                if (!copiedPage.PageSequenceReference.IsLoaded)
                {
                    copiedPage.PageSequenceReference.Load();
                }
                if (!copiedPage.PageSequence.Page.IsLoaded)
                {
                    copiedPage.PageSequence.Page.Load();
                }
                //Page lastPage = Resolve<IPageRepository>().GetLastPageOfPageSequence(copiedPage.PageSequence.PageSequenceGUID);
                //if (lastPage != null)
                //{
                //    pageOrderNo = lastPage.PageOrderNo + 1;
                //}
                CloneProgramParameterModel cloneParameterModel = new CloneProgramParameterModel
                {
                    ProgramGuidOfCopiedToProgramsInDefaultLanguage = Guid.Empty,
                    source = DefaultGuidSourceEnum.FromNull,
                };
                Page newPage = ClonePage(copiedPage, new List<KeyValuePair<string, string>>(), cloneParameterModel); //ServiceUtility.ClonePageNotIncludeParentGuid(copiedPage, new List<KeyValuePair<string, string>>());//previous is :ClonePage
                newPage = SetDefaultGuidForPage(newPage, cloneParameterModel);

                newPage.PageOrderNo++;
                foreach (Page page in copiedPage.PageSequence.Page)
                {
                    if (page.PageOrderNo > copiedPage.PageOrderNo)
                    {
                        page.PageOrderNo++;
                    }
                }

                copiedPage.PageSequence.Page.Add(newPage);
                //newPage.PageSequence = copiedPage.PageSequence;
                //Resolve<IPageRepository>().InstertPage(newPage);

                Resolve<IPageSequenceRepository>().UpdatePageSequence(copiedPage.PageSequence);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("{0}:{1}", MethodBase.GetCurrentMethod().Name, pageGuid));
                throw ex;
            }
        }

        public EditPageModel GetEditPageModel(Guid pageGuid, Guid programGuid)
        {
            EditPageModel editPageModel = new EditPageModel();
            try
            {
                Page page = Resolve<IPageRepository>().GetPageByPageGuid(pageGuid);
                if (!page.PageSequenceReference.IsLoaded)
                {
                    page.PageSequenceReference.Load();
                }
                Program program = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
                editPageModel.Page = GetEditPageModel(pageGuid);
                editPageModel.PageMaterials = GetPageMaterials();

                editPageModel.IsPageSequenceUsedInOtherSession = false;
                editPageModel.ProgramGUID = programGuid;
                editPageModel.ProgramLanguages = GetProgramLanguages(programGuid);
                editPageModel.ProgramName = program.Name;
                editPageModel.PageSequenceName = page.PageSequence.Name;
            }
            catch(Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("{0}:{1},{2}", MethodBase.GetCurrentMethod().Name, pageGuid, programGuid));
                throw ex;
            }

            return editPageModel;
        }

        public EditPageModel GetEditPageModelOfRelapsePageSeqence(Guid pageGuid, Guid relapsePageSequenceGuid, Guid programGuid)
        {
            EditPageModel editPageModel = new EditPageModel();
            try
            {
                Program programEntity = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
                PageSequence pageSequenceEntity = Resolve<IPageSequenceRepository>().GetPageSequenceByGuid(relapsePageSequenceGuid);

                editPageModel.ProgramName = programEntity.Name;
                editPageModel.SessionName = "";
                editPageModel.PageSequenceName = pageSequenceEntity.Name;
                // Don't know which one is the name, will change later when know.
                editPageModel.Name = "";
                editPageModel.Page = GetEditPageModel(pageGuid);
                editPageModel.PageMaterials = GetPageMaterials();

                editPageModel.IsPageSequenceUsedInOtherSession = false;
                editPageModel.ProgramGUID = programEntity.ProgramGUID;                
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("{0}:{1},{2},{3}", MethodBase.GetCurrentMethod().Name, pageGuid, relapsePageSequenceGuid, programGuid));
                throw ex;
            }

            return editPageModel;
        }

        public EditPageModel GetEditPageModel(Guid pageGuid, Guid sessionGuid, Guid pageSequenceGuid)
        {
            EditPageModel editPageModel = new EditPageModel();
            try
            {
                Session sessionEntity = Resolve<ISessionRepository>().GetSessionBySessionGuid(sessionGuid);
                if(!sessionEntity.ProgramReference.IsLoaded)
                {
                    sessionEntity.ProgramReference.Load();
                }
                PageSequence pageSequenceEntity = Resolve<IPageSequenceRepository>().GetPageSequenceByGuid(pageSequenceGuid);

                editPageModel.ProgramName = sessionEntity.Program.Name;
                editPageModel.SessionName = sessionEntity.Name;
                editPageModel.PageSequenceName = pageSequenceEntity.Name;
                // Don't know which one is the name, will change later when know.
                editPageModel.Name = "";
                editPageModel.Page = GetEditPageModel(pageGuid);
                editPageModel.PageMaterials = GetPageMaterials();

                editPageModel.IsPageSequenceUsedInOtherSession = Resolve<IPageSequenceService>().PageSequenceInMoreSession(sessionGuid, pageSequenceGuid);
                editPageModel.ProgramGUID = sessionEntity.Program.ProgramGUID;
                editPageModel.ProgramLanguages = GetProgramLanguages(sessionEntity.Program.ProgramGUID);
                
                // replase absolutely postion expression
                if(!string.IsNullOrEmpty(editPageModel.Page.BeforeExpression))
                {
                    editPageModel.Page.BeforeExpression = ReplacePageGUIDWithPageNOInExpression(editPageModel.Page.BeforeExpression, sessionGuid);
                }
                if(!string.IsNullOrEmpty(editPageModel.Page.AfterExpression))
                {
                    editPageModel.Page.AfterExpression = ReplacePageGUIDWithPageNOInExpression(editPageModel.Page.AfterExpression, sessionGuid);
                }
            }
            catch(Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("{0}:{1},{2},{3}", MethodBase.GetCurrentMethod().Name, pageGuid, sessionGuid, pageSequenceGuid));
                throw ex;
            }

            return editPageModel;
        }

        private string ReplacePageNOWithPageGUIDInExpression(string expression, Guid sessionGUID)
        {
            if(!string.IsNullOrEmpty(expression))
            {
                string markupPrefix = "[Page:";

                int pageNOIndex = 0;
                do
                {
                    pageNOIndex = expression.IndexOf(markupPrefix);
                    if(pageNOIndex > 0)
                    {
                        string pageNO = expression.Substring(pageNOIndex + markupPrefix.Length, (expression.IndexOf(']') - pageNOIndex - markupPrefix.Length));

                        string[] pageNOArray = pageNO.Split('.');
                        int pagesequenceNO = Convert.ToInt32(pageNOArray[0].Trim());
                        int pageOrder = Convert.ToInt32(pageNOArray[1].Trim());

                        SessionContent sessioncontent = Resolve<ISessionContentRepository>().GetSessionContentBySessionGuidAndOrderNO(sessionGUID, pagesequenceNO);
                        if(!sessioncontent.PageSequenceReference.IsLoaded)
                        {
                            sessioncontent.PageSequenceReference.Load();
                        }

                        Page pageEntity = Resolve<IPageRepository>().GetPageByPageSequenceAndOrderNo(sessioncontent.PageSequence.PageSequenceGUID, pageOrder);
                        expression = expression.Replace(markupPrefix + pageNO + "]", "{Page:" + pageEntity.PageGUID + "}");
                    }

                } while(pageNOIndex > 0);
            }

            return expression;
        }

        private string ReplacePageGUIDWithPageNOInExpression(string expression, Guid sessionGUID)
        {
            if(!string.IsNullOrEmpty(expression))
            {
                string markupPrefix = "{Page:";

                int pageGUIDIndex = 0;
                do
                {
                    pageGUIDIndex = expression.IndexOf(markupPrefix);
                    if(pageGUIDIndex > 0)
                    {
                        string pageGUID = expression.Substring(pageGUIDIndex + markupPrefix.Length, 36);

                        Page pageEntity = Resolve<IPageRepository>().GetPageByPageGuid(new Guid(pageGUID));
                        if(!pageEntity.PageSequenceReference.IsLoaded)
                        {
                            pageEntity.PageSequenceReference.Load();
                        }

                        SessionContent sessionContentEntity = Resolve<ISessionContentRepository>().GetSessionContentBySessionGuidPageSequenceGuid(sessionGUID, pageEntity.PageSequence.PageSequenceGUID);
                        expression = expression.Replace(markupPrefix + pageGUID + "}", "[Page:" + sessionContentEntity.PageSequenceOrderNo + "." + pageEntity.PageOrderNo + "]");
                    }
                } while(pageGUIDIndex > 0);
            }
            return expression;
        }

        public string GetPagePreviewModel(Guid pageGuid, Guid sessionGuid, Guid pageSequenceGuid, Guid userGuid)
        {
            string resultXML = string.Empty;
            try
            {
                resultXML = Resolve<IStoreProcedure>().GetPreviewPageModelAsXML(pageGuid, Guid.Empty, sessionGuid, pageSequenceGuid, userGuid);
            }
            catch(Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("{0}:{1},{2},{3},{4}}", MethodBase.GetCurrentMethod().Name, pageGuid, sessionGuid, pageSequenceGuid, userGuid));
                throw ex;
            }
            return resultXML;
        }

        public PageMaterials GetPageMaterials()
        {
            PageMaterials pageMaterials = new PageMaterials();
            try
            {
                pageMaterials.PageTemplates = new List<PageTemplateModel>();
                List<PageTemplate> PageTemplates = Resolve<IPageThemplateRepository>().GetAllPageTemplate().OrderBy(t => t.Name).ToList();
                foreach(PageTemplate theme in PageTemplates)
                {
                    pageMaterials.PageTemplates.Add(new PageTemplateModel
                    {
                        Guid = theme.PageTemplateGUID,
                        Name = theme.Name
                    });
                }

                pageMaterials.Questions = GetQuestionsModel();

            }
            catch(Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("{0}", MethodBase.GetCurrentMethod().Name));
                throw ex;
            }
            return pageMaterials;
        }

        public List<PageTemplateModel> GetPageTemplates()
        {
            List<PageTemplateModel> pageTemplatesModel = new List<PageTemplateModel>();
            try
            {
                List<PageTemplate> PageTemplates = Resolve<IPageThemplateRepository>().GetAllPageTemplate().OrderBy(t => t.Name).ToList();
                foreach (PageTemplate theme in PageTemplates)
                {
                    pageTemplatesModel.Add(new PageTemplateModel
                    {
                        Guid = theme.PageTemplateGUID,
                        Name = theme.Name
                    });
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("{0}", MethodBase.GetCurrentMethod().Name));
                throw ex;
            }
            return pageTemplatesModel;
        }

        public Guid SaveChangedToStandardTemplatePage(Guid oldPageGuid, StandardTemplatePageContentModel newPage)
        {
            newPage.PageOrder = GetPageOrderNumber(oldPageGuid);
            newPage.LastUpdateBy = new UserModel
            {
                UserName = Resolve<IUserService>().GetCurrentUser().UserName,
            };
            DeletePage(oldPageGuid);
            return SaveStandardTemplatePage(newPage);
        }

        public Guid  SaveStandardTemplatePage(StandardTemplatePageContentModel newStandardPage)
        {
            Guid newPageGuid = InsertPage(newStandardPage.PageSequenceGUID, newStandardPage.TemplateGUID, newStandardPage.PageOrder, "0", 0, Guid.Empty);
            SaveStandardPage(newStandardPage, newPageGuid);
            //SaveStandardPageDownloadResource(newStandardPage, newPageGuid);//insert into PageDownloadResource datatable
            return newPageGuid;
        }

        public Guid SaveChangedToGetInfoTemplatePage(Guid oldPageGuid, GetInfoTemplatePageContentModel newpage)
        {
            newpage.PageOrder = GetPageOrderNumber(oldPageGuid);
            DeletePage(oldPageGuid);
            return SaveGetInfoTemplatePage(newpage);
        }


        public Guid SaveGetInfoTemplatePage(GetInfoTemplatePageContentModel newGetInfoPage)
        {
            Guid newPageGuid = InsertPage(newGetInfoPage.PageSequenceGUID, newGetInfoPage.TemplateGUID, newGetInfoPage.PageOrder, "0", 0, Guid.Empty);
            SaveGetInfoPage(newGetInfoPage, newPageGuid);
            return newPageGuid;
        }

        public Guid SaveChangedToAccountCreationTemplatePage(Guid oldPageGuid, AccountCreationTemplatePageContentModel newPage)
        {
            newPage.PageOrder = GetPageOrderNumber(oldPageGuid);
            DeletePage(oldPageGuid);
            return SaveAccountCreationTemplatePage(newPage);
        }

        public Guid SaveAccountCreationTemplatePage(AccountCreationTemplatePageContentModel newPage)
        {
            Guid newPageGuid = InsertPage(newPage.PageSequenceGUID, newPage.TemplateGUID, newPage.PageOrder, "0", 0, newPage.PageVariableGUID);
            SaveAccountCreationPage(newPage, newPageGuid);
            return newPageGuid;
        }

        public Guid SaveChangedToPushPicturesTemplatePage(Guid oldPageGuid, PushPictureTemplatePageContentModel newPage)
        {
            newPage.PageOrder = GetPageOrderNumber(oldPageGuid);
            DeletePage(oldPageGuid);
            return SavePushPictureTemplatePage(newPage);
        }

        public Guid SavePushPictureTemplatePage(PushPictureTemplatePageContentModel newPushPicPage)
        {
            Guid newPageGuid = InsertPage(newPushPicPage.PageSequenceGUID, newPushPicPage.TemplateGUID, newPushPicPage.PageOrder, newPushPicPage.Wait, 0, Guid.Empty);
            SavePushPicturePage(newPushPicPage, newPageGuid);
            return newPageGuid;
        }

        public Guid SaveChangedToTimerTemplatePage(Guid oldPageGuid, TimerTemplatePageContentModel newPage)
        {
            newPage.PageOrder = GetPageOrderNumber(oldPageGuid);
            DeletePage(oldPageGuid);
            return SaveTimerTemplatePage(newPage);
        }

        public Guid SaveTimerTemplatePage(TimerTemplatePageContentModel newTimerPage)
        {
            Guid newPageGuid = InsertPage(newTimerPage.PageSequenceGUID, newTimerPage.TemplateGUID, newTimerPage.PageOrder, "0", 0, newTimerPage.PageVariableGUID);
            SaveTimerPage(newTimerPage, newPageGuid);
            return newPageGuid;
        }

        public Guid SaveChangedToChoosePreferenceTemplatePage(Guid oldPageGuid, ChoosePreferencesTemplatePageContentModel newpage)
        {
            newpage.PageOrder = GetPageOrderNumber(oldPageGuid);
            DeletePage(oldPageGuid);
            return SaveChoosePreferenceTemplatePage(newpage);
        }

        public Guid SaveChoosePreferenceTemplatePage(ChoosePreferencesTemplatePageContentModel choosePreferencePage)
        {
            Guid choosePreferencePageGuid = InsertPage(choosePreferencePage.PageSequenceGUID, choosePreferencePage.TemplateGUID, choosePreferencePage.PageOrder, "0", choosePreferencePage.MaxPreferences, Guid.Empty);
            Program program = GetProgram(choosePreferencePage.ProgramGuid);

            PageContent pagecontent = new PageContent();
            pagecontent.Heading = "";
            pagecontent.Page = Resolve<IPageRepository>().GetPageByPageGuid(choosePreferencePageGuid);
            pagecontent.PrimaryButtonCaption = choosePreferencePage.PrimaryButtonName;
            pagecontent.AfterShowExpression = ReplacePageNOWithPageGUIDInExpression(choosePreferencePage.AfterExpression, choosePreferencePage.SessionGUID);
            pagecontent.BeforeShowExpression = ReplacePageNOWithPageGUIDInExpression(choosePreferencePage.BeforeExpression, choosePreferencePage.SessionGUID);

            Resolve<IPageContentRepository>().Add(pagecontent);
            //foreach (Language language in program.Language1)
            //{
            foreach(PreferenceItemModel preferenceItem in choosePreferencePage.Preferences)
            {
                SavePagePreferenceItem(preferenceItem, choosePreferencePageGuid);
            }
            //}
            return choosePreferencePageGuid;
        }

        public Guid SaveChangedToGraphTemplatePageContentModel(Guid oldPageGuid, GraphTemplatePageContentModel graphPage)
        {
            graphPage.PageOrder = GetPageOrderNumber(oldPageGuid);
            DeletePage(oldPageGuid);
            return SaveGraphTemplatePage(graphPage);
        }

        public Guid SaveGraphTemplatePage(GraphTemplatePageContentModel graphPage)
        {
            Guid graphPageGuid = InsertPage(graphPage.PageSequenceGUID, graphPage.TemplateGUID, graphPage.PageOrder, "0", 0, Guid.Empty);
            SaveGraphPageContent(graphPage, graphPageGuid);
            return graphPageGuid;
        }

        public Guid SaveSMSTemplatePage(SMSTemplatePageContentModel smsPage)
        {
            Guid smsPageGuid = InsertPage(smsPage.PageSequenceGUID, smsPage.TemplateGUID, smsPage.PageOrder, "0", 0, smsPage.PageVariableGUID);
            SaveSMSPageContent(smsPage, smsPageGuid);
            return smsPageGuid;
        }

        public Guid SaveChangedToSMSTemplatePage(Guid oldPageGuid, SMSTemplatePageContentModel smsPage)
        {
            smsPage.PageOrder = GetPageOrderNumber(oldPageGuid);
            DeletePage(oldPageGuid);
            return SaveSMSTemplatePage(smsPage);
        }

        public List<SimplePageContentModel> GetPagesOfPageSequence(Guid pageSequenceGUID)
        {
            List<SimplePageContentModel> pageModels = new List<SimplePageContentModel>();
            List<Page> pageEntities = Resolve<IPageRepository>().GetPagesByPageSequenceGuid(pageSequenceGUID);
            foreach(Page pageEntity in pageEntities)
            {
                SimplePageContentModel pageModel = new SimplePageContentModel();
                pageModel.Order = pageEntity.PageOrderNo;
                pageModel.ID = pageEntity.PageGUID;

                if(!pageEntity.PageSequenceReference.IsLoaded)
                {
                    pageEntity.PageSequenceReference.Load();
                }
                if(!pageEntity.PageSequence.SessionContent.IsLoaded)
                {
                    pageEntity.PageSequence.SessionContent.Load();
                }
                if(pageEntity.PageSequence.SessionContent.Count > 0)
                {
                    pageModel.SequenceOrder = pageEntity.PageSequence.SessionContent.FirstOrDefault().PageSequenceOrderNo;
                }
                else
                {
                    // for relapse page sequence
                    pageModel.SequenceOrder = 0;
                }

                PageContent pageContentEntity = Resolve<IPageContentRepository>().GetPageContentByPageGuid(pageEntity.PageGUID);
                if(pageContentEntity != null)
                {
                    pageModel.Heading = pageContentEntity.Heading;
                    pageModel.Body = pageContentEntity.Body;
                    pageModel.Footer = pageContentEntity.FooterText;
                    pageModel.PrimaryButtonCaption = pageContentEntity.PrimaryButtonCaption;
                }
                pageModels.Add(pageModel);
            }
            return pageModels;
        }

        public List<SimplePageContentModel> GetPagesOfSession(Guid sessionGuid)
        {
            List<SimplePageContentModel> pageModels = new List<SimplePageContentModel>();
            List<SessionContent> sessionContentEntities = Resolve<ISessionContentRepository>().GetSessionContentBySessionGuid(sessionGuid).ToList();
            foreach (SessionContent sessionContentEntity in sessionContentEntities)
            {
                if (!sessionContentEntity.PageSequenceReference.IsLoaded)
                {
                    sessionContentEntity.PageSequenceReference.Load();
                }
                List<Page> pageEntities = Resolve<IPageRepository>().GetPagesByPageSequenceGuid(sessionContentEntity.PageSequence.PageSequenceGUID);
                foreach (Page pageEntity in pageEntities)
                {
                    SimplePageContentModel pageModel = new SimplePageContentModel();
                    pageModel.Order = pageEntity.PageOrderNo;
                    pageModel.ID = pageEntity.PageGUID;
                    pageModel.PageSequenceGUID = sessionContentEntity.PageSequence.PageSequenceGUID;

                    pageModel.SequenceOrder = sessionContentEntity.PageSequenceOrderNo;

                    if (!pageEntity.PageTemplateReference.IsLoaded)
                    {
                        pageEntity.PageTemplateReference.Load();
                    }
                    if (pageEntity.PageTemplate != null)
                    {
                        pageModel.TemplateGUID = pageEntity.PageTemplate.PageTemplateGUID;
                        pageModel.TemplateName = pageEntity.PageTemplate.Name;
                    }
                    PageContent pageContentEntity = Resolve<IPageContentRepository>().GetPageContentByPageGuid(pageEntity.PageGUID);
                    if (pageContentEntity != null)
                    {
                        pageModel.Heading = pageContentEntity.Heading;
                        pageModel.Body = pageContentEntity.Body;
                        pageModel.PrimaryButtonCaption = pageContentEntity.PrimaryButtonCaption;

                        if (!pageEntity.PageContentReference.IsLoaded)
                        {
                            pageEntity.PageContentReference.Load();
                        }
                        if (pageEntity.PageContent != null)
                        {
                            pageModel.AfterShowExpression = pageEntity.PageContent.AfterShowExpression;
                            pageModel.BeforeShowExpression = pageEntity.PageContent.BeforeShowExpression;
                            if (!pageEntity.PageContent.Resource_PresenterImageReference.IsLoaded)
                            {
                                pageEntity.PageContent.Resource_PresenterImageReference.Load();
                            }
                            if (pageEntity.PageContent.Resource_PresenterImage!= null)
                            {
                                string accountName = Resolve<ISystemSettingService>().GetSettingValue("AzureStorageAccountName");
                                string bolbPath = ServiceUtility.GetBlobPath(accountName);
                                string ImageUrl = "";
                                if (!string.IsNullOrEmpty(pageEntity.PageContent.Resource_PresenterImage.NameOnServer))
                                {
                                    ImageUrl = bolbPath + BlobContainerTypeEnum.ThumnailContainer.ToString().ToLower() + "/" + pageEntity.PageContent.Resource_PresenterImage.NameOnServer;
                                }
                                pageModel.PresenterImageUrl = ImageUrl;
                            }

                        }

                    }
                    pageModels.Add(pageModel);
                }

            }

            return pageModels.OrderBy(p => p.SequenceOrder).ThenBy(p => p.Order).ToList();
        }

        public List<StandardTemplatePageContentModel> GetPagesDownResOfPageSequence(Guid pageSequenceGUID)//used in ctpp download resource 
        {
            List<StandardTemplatePageContentModel> pageModels = new List<StandardTemplatePageContentModel>();
            List<Page> pageEntities = Resolve<IPageRepository>().GetPagesByPageSequenceGuid(pageSequenceGUID);
            foreach (Page pageEntity in pageEntities)
            {
                StandardTemplatePageContentModel pageModel = new StandardTemplatePageContentModel();
                //pageModel.Order = pageEntity.PageOrderNo;
                pageModel.TemplateGUID = pageEntity.PageGUID;

                if (!pageEntity.PageSequenceReference.IsLoaded)
                {
                    pageEntity.PageSequenceReference.Load();
                }
                if (!pageEntity.PageSequence.SessionContent.IsLoaded)
                {
                    pageEntity.PageSequence.SessionContent.Load();
                }
                //if (pageEntity.PageSequence.SessionContent.Count > 0)
                //{
                //    pageModel.SequenceOrder = pageEntity.PageSequence.SessionContent.FirstOrDefault().PageSequenceOrderNo;
                //}
                //else
                //{
                //    // for relapse page sequence
                //    pageModel.SequenceOrder = 0;
                //}

                PageContent pageContentEntity = Resolve<IPageContentRepository>().GetPageContentByPageGuid(pageEntity.PageGUID);
                if (pageContentEntity != null)
                {
                    pageModel.Heading = pageContentEntity.Heading;
                    pageModel.Body = pageContentEntity.Body;
                }


                if (!pageEntity.PageMediaReference.IsLoaded)
                {
                    pageEntity.PageMediaReference.Load();
                }
                if (pageEntity.PageMedia != null)
                {
                    if (!pageEntity.PageMedia.ResourceReference.IsLoaded)
                    {
                        pageEntity.PageMedia.ResourceReference.Load();
                    }
                    if (pageEntity.PageMedia.Resource != null)
                    {
                        //pageEntity.PageMedia
                    }

                    //bm.BrandLogo = new ResourceModel
                    //{
                    //    Extension = brandEntity.Resource.FileExtension,
                    //    ID = brandEntity.Resource.ResourceGUID,
                    //    Name = brandEntity.Resource.Name,
                    //    NameOnServer = brandEntity.Resource.NameOnServer,
                    //    Type = brandEntity.Resource.Type,
                    //};
                }

                //pageModel.VideoGUID=pageEntity.PageMedia.Page.PageMedia.t
                
                pageModels.Add(pageModel);
            }
            return pageModels;

        }

        public void UpdateStandardTemplatePage(StandardTemplatePageContentModel standardPage, Guid pageGuid)
        {
            try
            {
                PageContent pageContentEntity = Resolve<IPageContentRepository>().GetPageContentByPageGuid(pageGuid);
                pageContentEntity.Heading = standardPage.Heading;
                pageContentEntity.Body = standardPage.Body;
                pageContentEntity.PrimaryButtonCaption = standardPage.PrimaryButtonCaption;
                pageContentEntity.PrimaryButtonActionParameter = standardPage.PrimaryButtonAction;
                pageContentEntity.BeforeShowExpression = ReplacePageNOWithPageGUIDInExpression(standardPage.BeforeExpression, standardPage.SessionGUID);
                pageContentEntity.AfterShowExpression = ReplacePageNOWithPageGUIDInExpression(standardPage.AfterExpression, standardPage.SessionGUID);
                //pageContentEntity.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid.ToString();
                //pageContentEntity.SecondaryButtonCaption = standardPage.SecondaryButtonCaption;
                //pageContentEntity.SecondaryButtonActionParameter = standardPage.SecondaryButtonAction;

                //FullScreen image
                if(standardPage.BackgroundImageGUID == Guid.Empty)
                {
                    pageContentEntity.Resource_BackgroundImage = null;
                }
                else
                {
                    Resource backgroudResource = Resolve<IResourceRepository>().GetResource(standardPage.BackgroundImageGUID);
                    pageContentEntity.Resource_BackgroundImage = backgroudResource;
                    pageContentEntity.ImageMode = "Fullscreen";

                    //Update resource category's last access time
                    UpdateResourceCatgeoryLastAccessTime(backgroudResource);
                }

                //Preseter image
                if(standardPage.PresenterImageGUID == Guid.Empty)
                {
                    pageContentEntity.Resource_PresenterImage = null;
                }
                else
                {
                    Resource presenterResource = Resolve<IResourceRepository>().GetResource(standardPage.PresenterImageGUID);
                    pageContentEntity.Resource_PresenterImage = presenterResource;
                    pageContentEntity.ImageMode = "Preseter";
                    pageContentEntity.PresenterImagePosition = standardPage.PresenterImagePosition;
                    pageContentEntity.PresenterMode = standardPage.PresenterMode;

                    //Update resource category's last access time
                    UpdateResourceCatgeoryLastAccessTime(presenterResource);
                }

                bool hasPageMediaBefore = false;
                bool hasDifference = false;
                PageMedia pageMediaEntity = Resolve<IPageMediaRepository>().GetPageMediaByPageGuid(pageGuid);
                if(pageMediaEntity != null)
                {
                    hasPageMediaBefore = true;

                    if(!pageMediaEntity.ResourceReference.IsLoaded)
                    {
                        pageMediaEntity.ResourceReference.Load();
                    }
                }
                else
                {
                    pageMediaEntity = new PageMedia();
                }

                Resource referencedResource = null;

                //Illustration image
                if (standardPage.IllustrationImageGUID == Guid.Empty)
                {
                    pageContentEntity.IllustrationImageGUID = null;
                }
                else
                {
                    Resource illustrationResource = Resolve<IResourceRepository>().GetResource(standardPage.IllustrationImageGUID);
                    pageContentEntity.IllustrationImageGUID = illustrationResource.ResourceGUID;
                    pageContentEntity.ImageMode = "Illustration";
                    //update pageMedia entity
                    pageMediaEntity.Page = Resolve<IPageRepository>().GetPageByPageGuid(pageGuid);
                    //pageMediaEntity.Language = Resolve<ILanguageRepository>().GetLanguage(standardPage.LanguageGUID);
                    pageMediaEntity.Resource = illustrationResource;
                    pageMediaEntity.Type = "Illustration";
                    hasDifference = true;
                    referencedResource = illustrationResource;

                    //Update resource category's last access time
                    UpdateResourceCatgeoryLastAccessTime(illustrationResource);
                }
                Resolve<IPageContentRepository>().Update(pageContentEntity);
                Resolve<ITranslationJobService>().UpdateElementWhenFromUpdated("PageContent", pageContentEntity.PageGUID.ToString(), Guid.Empty);


                #region IllustrationImage old follows
                //if(standardPage.IllustrationImageGUID != Guid.Empty &&
                //    (pageMediaEntity.Resource == null ||
                //    standardPage.IllustrationImageGUID != pageMediaEntity.Resource.ResourceGUID))
                //{
                //    Resource illustrationResource = Resolve<IResourceRepository>().GetResource(standardPage.IllustrationImageGUID);
                //    pageMediaEntity.Page = Resolve<IPageRepository>().GetPageByPageGuid(pageGuid);
                //    //pageMediaEntity.Language = Resolve<ILanguageRepository>().GetLanguage(standardPage.LanguageGUID);
                //    pageMediaEntity.Resource = illustrationResource;
                //    pageMediaEntity.Type = "Illustration";
                //    hasDifference = true;
                //    referencedResource = illustrationResource;
                //    //UpdateResourceCatgeoryLastAccessTime(illustrationResource);
                //} 
                #endregion
                if(standardPage.VideoGUID != Guid.Empty &&
                    (pageMediaEntity.Resource == null ||
                    standardPage.VideoGUID != pageMediaEntity.Resource.ResourceGUID))
                {
                    Resource videoResource = Resolve<IResourceRepository>().GetResource(standardPage.VideoGUID);
                    pageMediaEntity.Page = Resolve<IPageRepository>().GetPageByPageGuid(pageGuid);
                    //pageMediaEntity.Language = Resolve<ILanguageRepository>().GetLanguage(standardPage.LanguageGUID);
                    pageMediaEntity.Resource = videoResource;
                    pageMediaEntity.Type = "Video";
                    hasDifference = true;

                    referencedResource = videoResource;
                    //UpdateResourceCatgeoryLastAccessTime(videoResource);
                }
                if(standardPage.RadioGUID != Guid.Empty &&
                    (pageMediaEntity.Resource == null ||
                    standardPage.RadioGUID != pageMediaEntity.Resource.ResourceGUID))
                {
                    Resource radioResource = Resolve<IResourceRepository>().GetResource(standardPage.RadioGUID);
                    pageMediaEntity.Page = Resolve<IPageRepository>().GetPageByPageGuid(pageGuid);
                    //pageMediaEntity.Language = Resolve<ILanguageRepository>().GetLanguage(standardPage.LanguageGUID);
                    pageMediaEntity.Resource = radioResource;
                    pageMediaEntity.Type = "Audio";
                    hasDifference = true;
                    referencedResource = radioResource;
                    //UpdateResourceCatgeoryLastAccessTime(radioResource);
                }

                if(hasDifference)
                {
                    pageMediaEntity.IsDeleted = false;
                    if(hasPageMediaBefore)
                    {
                        Resolve<IPageMediaRepository>().UpdatePageMedia(pageMediaEntity);
                    }
                    else
                    {
                        PageMedia pmEntity = Resolve<IPageMediaRepository>().GetPageMediaByPageGuid(pageMediaEntity.PageGUID);
                        if (pmEntity == null)
                        {
                            Resolve<IPageMediaRepository>().AddPageMedia(pageMediaEntity);

                        }
                        else
                        {
                            Resolve<IPageMediaRepository>().UpdatePageMedia(pageMediaEntity);
                        }
                    }

                    if(referencedResource != null)
                    {
                        UpdateResourceCatgeoryLastAccessTime(referencedResource);
                    }
                }

                if(hasPageMediaBefore
                    && standardPage.IllustrationImageGUID == Guid.Empty
                    &&standardPage.VideoGUID == Guid.Empty 
                    &&standardPage.RadioGUID == Guid.Empty)
                {
                    Resolve<IPageMediaRepository>().DeletePageMedia(pageGuid);
                }
            }
            catch(Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("{0}:PageGUID {1}, BackgroupImageGUID {2}, PresenterImageGUID {3}, IllustrationImageGUID {4}, VideGUID {5}, RadioGUID {6}",
                    MethodBase.GetCurrentMethod().Name, pageGuid, standardPage.BackgroundImageGUID,
                    standardPage.PresenterImageGUID, standardPage.IllustrationImageGUID, standardPage.VideoGUID, standardPage.RadioGUID));
                throw ex;
            }
        }

        public void UpdateStandardTemplatePageOfDownloadResource(StandardTemplatePageContentModel standardPage, Guid pageGuid)
        {
            Resolve<IPageResourceRepository>().DeletePageResource(pageGuid);
            //SaveStandardPageDownloadResource(standardPage, pageGuid);
        }

        public void UpdateGetInfoTemplatePage(GetInfoTemplatePageContentModel getInfoPage, Guid pageGuid)
        {
            Page editPage = Resolve<IPageRepository>().GetPageByPageGuid(pageGuid);
            editPage.LastUpdated = DateTime.UtcNow;
            editPage.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
            Resolve<IPageRepository>().UpdatePage(editPage);
            UpdateGetInfoPageContent(getInfoPage, pageGuid);
            UpdateGetInfoPageQuestions(getInfoPage, pageGuid);
        }

        public void UpdateScreenResultsTemplatePage(ScreenResultTemplatePageContentModel screenResultsPage, Guid pageGuid)
        {
            Page editPage = Resolve<IPageRepository>().GetPageByPageGuid(pageGuid);
            editPage.LastUpdated = DateTime.UtcNow;
            editPage.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
            Resolve<IPageRepository>().UpdatePage(editPage);
            UpdateScreenResultsPageContent(screenResultsPage, pageGuid);
            UpdateScreenPageLines(screenResultsPage, pageGuid);
        }

        private void UpdateScreenPageLines(ScreenResultTemplatePageContentModel screenResultPage, Guid pageGuid)
        {
            Page pageEntity = Resolve<IPageRepository>().GetPageByPageGuid(pageGuid);
            if (!pageEntity.PageSequenceReference.IsLoaded) pageEntity.PageSequenceReference.Load();
            List<ScreenResultTemplatePageLineModel> pageLines = screenResultPage.PageLines.Where(pl => screenResultPage.ObjectStatus.ContainsKey(pl.PageLineGuid)).ToList();

            foreach (ScreenResultTemplatePageLineModel pageLineModel in screenResultPage.PageLines)
            {
                if (screenResultPage.ObjectStatus.ContainsKey(pageLineModel.PageLineGuid))
                {
                    ScreenResultTemplatePageLine pageLineEntity;
                    switch (screenResultPage.ObjectStatus[pageLineModel.PageLineGuid])
                    {
                        case ModelStatus.PageLineAdded:
                            pageLineEntity = new ScreenResultTemplatePageLine
                            {
                                PageLineGUID = pageLineModel.PageLineGuid,
                                PageGUID = pageGuid,
                                Text = pageLineModel.Text,
                                URL = pageLineModel.URL,
                                Order = pageLineModel.Order
                            };

                            if (pageLineModel.PageVariable != null && pageLineModel.PageVariable.Name != "")
                            {
                                pageLineEntity.PageVariable = Resolve<IPageVaribleRepository>().GetItem(pageLineModel.PageVariable.PageVariableGUID);
                            }
                            pageLineEntity.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
                            Resolve<IScreenResultTemplatePageLineRepository>().AddPageLine(pageLineEntity);
                            break;
                        case ModelStatus.PageLineUpdated:
                            //Update the pageLine's property
                            pageLineEntity = Resolve<IScreenResultTemplatePageLineRepository>().GetPageLine(pageLineModel.PageLineGuid);
                            pageLineEntity.Order = pageLineModel.Order;
                            pageLineEntity.Text = pageLineModel.Text;
                            pageLineEntity.URL = pageLineModel.URL;
                            pageLineEntity.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
                            if (pageLineModel.PageVariable != null && !string.IsNullOrEmpty(pageLineModel.PageVariable.Name))
                            {
                                pageLineEntity.PageVariable = Resolve<IPageVaribleRepository>().GetItem(pageLineModel.PageVariable.PageVariableGUID);
                            }
                            //Update PageLine Entity.
                            Resolve<IScreenResultTemplatePageLineRepository>().UpdatePageLine(pageLineEntity);
                            Resolve<ITranslationJobService>().UpdateElementWhenFromUpdated("ScreenResultTemplatePageLine", pageLineEntity.PageLineGUID.ToString(), Guid.Empty);
                            break;
                        //case ModelStatus.PageLineDeleted:
                        //    pageLineEntity = Resolve<IScreenResultTemplatePageLineRepository>().GetPageLine(pageLineModel.PageLineGuid);
                        //    Resolve<IScreenResultTemplatePageLineRepository>().DeletePageLine(pageLineEntity.PageLineGUID);
                        //    break;
                        default:
                            break;
                    }
                }
            }

            foreach (KeyValuePair<Guid, ModelStatus> item in screenResultPage.ObjectStatus)
            {
                if (item.Value == ModelStatus.PageLineDeleted)
                {
                    ScreenResultTemplatePageLine pageLineEntity = Resolve<IScreenResultTemplatePageLineRepository>().GetPageLine(item.Key);
                    Resolve<IScreenResultTemplatePageLineRepository>().DeletePageLine(pageLineEntity.PageLineGUID);
                }
            }
        }
    
        public void UpdateGraphTemplatePage(GraphTemplatePageContentModel graphPage, Guid pageGuid)
        {
            Page editPage = Resolve<IPageRepository>().GetPageByPageGuid(pageGuid);
            editPage.LastUpdated = DateTime.UtcNow;
            editPage.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
            Resolve<IPageRepository>().UpdatePage(editPage);
            UpdateGraphPageContent(graphPage, pageGuid);
        }

        public void UpdatePushPicTemplatePage(PushPictureTemplatePageContentModel pushPicPage, Guid pageGuid)
        {
            Page editPage = Resolve<IPageRepository>().GetPageByPageGuid(pageGuid);
            editPage.LastUpdated = DateTime.UtcNow;
            editPage.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
            editPage.Wait = pushPicPage.Wait;

            // update media
            if(!editPage.PageMediaReference.IsLoaded)
            {
                editPage.PageMediaReference.Load();
            }
            if(editPage.PageMedia != null)
            {
                if(pushPicPage.VoiceGUID != Guid.Empty)
                {
                    editPage.PageMedia.Resource = Resolve<IResourceRepository>().GetResource(pushPicPage.VoiceGUID);
                }
                else
                {
                    editPage.PageMedia = null;
                }
            }
            else
            {
                if(pushPicPage.VoiceGUID != Guid.Empty)
                {
                    editPage.PageMedia = new PageMedia
                    {
                        Resource = Resolve<IResourceRepository>().GetResource(pushPicPage.VoiceGUID),
                        Type = "Audio"
                    };
                }
            }

            editPage.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
            Resolve<IPageRepository>().UpdatePage(editPage);
            UpdatePushPicPageContent(pushPicPage, pageGuid);
        }

        public void UpdateTimerTemplatePage(TimerTemplatePageContentModel timerPage, Guid pageGuid)
        {
            Page editPage = Resolve<IPageRepository>().GetPageByPageGuid(pageGuid);
            editPage.LastUpdated = DateTime.UtcNow;
            editPage.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
            if(timerPage.PageVariableGUID != Guid.Empty)
            {
                editPage.PageVariable = Resolve<IPageVaribleRepository>().GetItem(timerPage.PageVariableGUID);
            }
            Resolve<IPageRepository>().UpdatePage(editPage);
            UpdateTimerPageContent(timerPage, pageGuid);
        }

        public void UpdateAccoutCrationTemplatePage(AccountCreationTemplatePageContentModel accountPage, Guid pageGuid)
        {
            Page editPage = Resolve<IPageRepository>().GetPageByPageGuid(pageGuid);
            if(accountPage.PageVariableGUID != Guid.Empty)
            {
                editPage.PageVariable = Resolve<IPageVaribleRepository>().GetItem(accountPage.PageVariableGUID);
            }
            editPage.LastUpdated = DateTime.UtcNow;
            editPage.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
            Resolve<IPageRepository>().UpdatePage(editPage);

            PageContent editPageContent = Resolve<IPageContentRepository>().GetPageContentByPageGuid(pageGuid);
            editPageContent.Heading = accountPage.Heading;
            //editPageContent.Body = accountPage.Body;
            //TODO: Need to seperate to different fields.
            editPageContent.Body = accountPage.Body + ";" + accountPage.UserName + ";" + accountPage.Password + ";" + accountPage.RepeatPassword + ";" + accountPage.Mobile + ";" + accountPage.CheckBoxText + ";" + accountPage.SNText;
            editPageContent.PrimaryButtonCaption = accountPage.PrimaryButtonCaption;
            //editPageContent.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid.ToString();
            Resolve<IPageContentRepository>().Update(editPageContent);
            Resolve<ITranslationJobService>().UpdateElementWhenFromUpdated("PageContent", editPageContent.PageGUID.ToString(), Guid.Empty);
        }

        public void UpdateChoosePreferenceTemplatePage(ChoosePreferencesTemplatePageContentModel choosePreferencePage, Guid pageGuid)
        {
            Page editPage = Resolve<IPageRepository>().GetPageByPageGuid(pageGuid);
            editPage.LastUpdated = DateTime.UtcNow;
            editPage.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
            editPage.MaxPreferences = choosePreferencePage.MaxPreferences;
            Resolve<IPageRepository>().UpdatePage(editPage);

            PageContent editPageContent = Resolve<IPageContentRepository>().GetPageContentByPageGuid(pageGuid);
            editPageContent.BeforeShowExpression = ReplacePageNOWithPageGUIDInExpression(choosePreferencePage.BeforeExpression, choosePreferencePage.SessionGUID);
            editPageContent.AfterShowExpression = ReplacePageNOWithPageGUIDInExpression(choosePreferencePage.AfterExpression, choosePreferencePage.SessionGUID);
            editPageContent.PrimaryButtonCaption = choosePreferencePage.PrimaryButtonName;
            //editPageContent.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid.ToString();
            Resolve<IPageContentRepository>().Update(editPageContent);
            Resolve<ITranslationJobService>().UpdateElementWhenFromUpdated("PageContent", editPageContent.PageGUID.ToString(), Guid.Empty);

            // Add & update preference item
            foreach(PreferenceItemModel preferenceItemModel in choosePreferencePage.Preferences)
            {
                Resource preferenceResource = null;
                switch(choosePreferencePage.PreferenceStatus[preferenceItemModel.PreferenceGUID])
                {
                    case ModelStatus.ModelAdd:
                        Preferences preferenceEntity = new Preferences();
                        preferenceEntity.PreferencesGUID = preferenceItemModel.PreferenceGUID;
                        preferenceEntity.Name = preferenceItemModel.Name;
                        preferenceEntity.AnswerText = preferenceItemModel.AnswerText;
                        preferenceEntity.ButtonName = preferenceItemModel.ButtonName;
                        preferenceEntity.Description = preferenceItemModel.Description;
                        //preferenceEntity.Language = Resolve<ILanguageRepository>().GetLanguage(choosePreferencePage.LanguageGUID);
                        preferenceEntity.Page = editPage;
                        if(preferenceItemModel.Variable != null && preferenceItemModel.Variable.PageVariableGuid != Guid.Empty)
                        {
                            preferenceEntity.PageVariable = Resolve<IPageVaribleRepository>().GetItem(preferenceItemModel.Variable.PageVariableGuid);
                        }
                        preferenceResource = Resolve<IResourceRepository>().GetResource(preferenceItemModel.Resource.ID);
                        preferenceEntity.Resource = preferenceResource;
                        preferenceEntity.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
                        Resolve<IPreferencesRepository>().InsertPreference(preferenceEntity);

                        UpdateResourceCatgeoryLastAccessTime(preferenceResource);
                        break;
                    case ModelStatus.ModelEdit:
                        Preferences editPreferenceEntity = Resolve<IPreferencesRepository>().GetPreference(preferenceItemModel.PreferenceGUID);
                        editPreferenceEntity.Name = preferenceItemModel.Name;
                        editPreferenceEntity.Description = preferenceItemModel.Description;
                        editPreferenceEntity.AnswerText = preferenceItemModel.AnswerText;
                        editPreferenceEntity.ButtonName = preferenceItemModel.ButtonName;
                        preferenceResource = Resolve<IResourceRepository>().GetResource(preferenceItemModel.Resource.ID);
                        editPreferenceEntity.Resource = preferenceResource;
                        if(preferenceItemModel.Variable != null && preferenceItemModel.Variable.PageVariableGuid != Guid.Empty)
                        {
                            editPreferenceEntity.PageVariable = Resolve<IPageVaribleRepository>().GetItem(preferenceItemModel.Variable.PageVariableGuid);
                        }
                        editPreferenceEntity.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
                        Resolve<IPreferencesRepository>().UpdatePreference(editPreferenceEntity);
                        Resolve<ITranslationJobService>().UpdateElementWhenFromUpdated("Preferences", editPreferenceEntity.PreferencesGUID.ToString(), Guid.Empty);

                        UpdateResourceCatgeoryLastAccessTime(preferenceResource);
                        break;
                    //case ModelStatus.ModelDelete:
                    //    Resolve<IPreferencesRepository>().DeletePreferences(pageGuid, choosePreferencePage.LanguageGUID);
                    //    break;
                    case ModelStatus.ModelNoChange:
                        break;
                }
            }

            // Delete preference item
            foreach(Guid preferenceGUID in choosePreferencePage.PreferenceStatus.Keys)
            {
                if(choosePreferencePage.PreferenceStatus[preferenceGUID] == ModelStatus.ModelDelete)
                {
                    Resolve<IPreferencesRepository>().DeletePreferences(preferenceGUID);
                }
            }
        }

        public void UpdateSMSTemplatePage(SMSTemplatePageContentModel smsPage, Guid pageGuid)
        {
            Page editPage = Resolve<IPageRepository>().GetPageByPageGuid(pageGuid);
            editPage.LastUpdated = DateTime.UtcNow;
            editPage.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
            if(smsPage.PageVariableGUID != Guid.Empty)
            {
                editPage.PageVariable = Resolve<IPageVaribleRepository>().GetItem(smsPage.PageVariableGUID);
            }
            Resolve<IPageRepository>().UpdatePage(editPage);

            PageContent editPageContent = Resolve<IPageContentRepository>().GetPageContentByPageGuid(pageGuid);
            editPageContent.SendTime = smsPage.Time;
            editPageContent.Body = smsPage.Text;
            editPageContent.DaysToSend = smsPage.DaysToSend;
            Resolve<IPageContentRepository>().Update(editPageContent);
            Resolve<ITranslationJobService>().UpdateElementWhenFromUpdated("PageContent", editPageContent.PageGUID.ToString(), Guid.Empty);
        }

        public void UpdatePageContentForPageReview(PageUpdateForPageReviewModel pageUpdateForPageReview)
        {
            Guid pageSequenceGuid = Guid.Empty;
            if (pageUpdateForPageReview.IsEditPageSequenceSelf)
            {
                pageSequenceGuid = pageUpdateForPageReview.PageSequenceGUID;
            }
            else
            {
                pageSequenceGuid = Resolve<IPageSequenceService>().BeforeEditPageSequenceAfterRefactoring(pageUpdateForPageReview.SessionGUID, pageUpdateForPageReview.PageSequenceGUID, pageUpdateForPageReview.IsUpdatePageSequence);
            }
            Guid pageGuid = Resolve<IPageService>().GetPageGuidByPageSequenceAndOrder(pageSequenceGuid, pageUpdateForPageReview.PageOrder);



            PageContent editPageContent = Resolve<IPageContentRepository>().GetPageContentByPageGuid(pageGuid);
            if (pageUpdateForPageReview.Heading != null)
                editPageContent.Heading = pageUpdateForPageReview.Heading;
            if (pageUpdateForPageReview.Body != null)
                editPageContent.Body = pageUpdateForPageReview.Body;
            if (pageUpdateForPageReview.PrimaryButtonCaption != null)
                editPageContent.PrimaryButtonCaption = pageUpdateForPageReview.PrimaryButtonCaption;
            if (pageUpdateForPageReview.PresenterImageGUID != Guid.Empty)
            {
                Resource presenterResource = Resolve<IResourceRepository>().GetResource(pageUpdateForPageReview.PresenterImageGUID);
                editPageContent.Resource_PresenterImage = presenterResource;
                UpdateResourceCatgeoryLastAccessTime(presenterResource);
            }
            Resolve<IPageContentRepository>().Update(editPageContent);
            Resolve<ITranslationJobService>().UpdateElementWhenFromUpdated("PageContent", editPageContent.PageGUID.ToString(), Guid.Empty);
        }

        public void UpdatePageContentForCloneSessionContent(Session newSession, Dictionary<Guid, Guid> pageDictionary)
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

        public void UpdatePageContentForCloneRelapse(Relapse newRelapse, List<KeyValuePair<string, string>> pageDictionary)
        {
            try
            {
                //    if (!newRelapse.PageSequenceReference.IsLoaded)
                //    {
                //        newRelapse.PageSequenceReference.Load();
                //    }
                //    if (!newRelapse.PageSequence.Page.IsLoaded)
                //    {
                //        newRelapse.PageSequence.Page.Load();
                //    }
                foreach (Page newPage in newRelapse.PageSequence.Page)
                {
                    foreach (KeyValuePair<string, string> pagePair in pageDictionary)
                    {
                        if (newPage.PageContent != null && !string.IsNullOrEmpty(newPage.PageContent.AfterShowExpression) && newPage.PageContent.AfterShowExpression.ToUpper().Contains(pagePair.Key))
                        {
                            newPage.PageContent.AfterShowExpression = newPage.PageContent.AfterShowExpression.Replace(pagePair.Key, pagePair.Value);
                        }
                        if (newPage.PageContent != null && !string.IsNullOrEmpty(newPage.PageContent.BeforeShowExpression) && newPage.PageContent.BeforeShowExpression.ToUpper().Contains(pagePair.Key))
                        {
                            newPage.PageContent.BeforeShowExpression = newPage.PageContent.BeforeShowExpression.Replace(pagePair.Key, pagePair.Value);
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

        public Page ClonePage(Page page, List<KeyValuePair<string, string>> cloneRelapseGUIDList, CloneProgramParameterModel cloneParameterModel)
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
                newPage.DefaultGUID = page.DefaultGUID;
                newPage.IsDeleted = page.IsDeleted;
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
                    PageContent clonedPageContent = ClonePageContent(page.PageContent, cloneRelapseGUIDList);
                    clonedPageContent = SetDefaultGuidForPageContent(clonedPageContent, cloneParameterModel);

                    newPage.PageContent = clonedPageContent;
                }

                if (!page.PageQuestion.IsLoaded)
                {
                    page.PageQuestion.Load();
                }
                List<PageQuestion> pageQuestions = page.PageQuestion.Where(p => p.IsDeleted != true).ToList();
                foreach (PageQuestion pageQuestion in pageQuestions)
                {
                    PageQuestion clonedPageQuestion = ClonePageQuestion(pageQuestion, cloneParameterModel);
                    clonedPageQuestion = SetDefaultGuidForPageQuestion(clonedPageQuestion, cloneParameterModel);

                    newPage.PageQuestion.Add(clonedPageQuestion);
                }
                if (!page.ScreenResultTemplatePageLine.IsLoaded)
                {
                    page.ScreenResultTemplatePageLine.Load();
                }

                //TODO:Copy PageLine.
                List<ScreenResultTemplatePageLine> pageLines = page.ScreenResultTemplatePageLine.Where(pl => pl.IsDeleted != true).ToList();
                //List<ScreenResultTemplatePageLine> pageLines = Resolve<IScreenResultTemplatePageLineRepository>().GetPageLinesByPageGuid(page.PageGUID).ToList(); //page.ScreenResultTemplatePageLine.Where(pl => pl.IsDeleted != true).ToList();
                foreach (ScreenResultTemplatePageLine pageLine in pageLines)
                {
                    ScreenResultTemplatePageLine clonedPageLine = ClonePageLine(pageLine, cloneParameterModel);
                    clonedPageLine = SetDefaultGuidForPageLine(clonedPageLine, cloneParameterModel);
                    newPage.ScreenResultTemplatePageLine.Add(clonedPageLine);
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
                    #region MyRegion
                    //if (!page.PageSequenceReference.IsLoaded) page.PageSequenceReference.Load();
                    //if (!page.PageMedia.ResourceReference.IsLoaded) page.PageMedia.ResourceReference.Load();
                    //PageResource pageResourceEntity = Resolve<IPageResourceRepository>().GetPageResource(page.PageGUID, page.PageSequence.PageSequenceGUID);
                    //if (pageResourceEntity != null)
                    //{
                    //    PageMedia clonedPageMedia = ClonePageMedia(page.PageMedia);
                    //    clonedPageMedia = SetDefaultGuidForPageMedia(clonedPageMedia, cloneParameterModel);

                    //    newPage.PageMedia = clonedPageMedia;
                    //} 
                    #endregion
                    //PageMedia pageMediaEntity = Resolve<IPageMediaRepository>().GetPageMediaByPageGuid(page.PageMedia.PageGUID);
                    //if (pageMediaEntity!=null)
                    //{
                        PageMedia clonedPageMedia = ClonePageMedia(page.PageMedia);
                        clonedPageMedia = SetDefaultGuidForPageMedia(clonedPageMedia, cloneParameterModel);

                        newPage.PageMedia = clonedPageMedia;
                    //}
                }
                //}
                if (!page.Preferences.IsLoaded)
                {
                    page.Preferences.Load();
                }
                List<Preferences> preferences = page.Preferences.Where(p => p.IsDeleted != true).ToList();
                foreach (Preferences pre in preferences)
                {
                    Preferences clonedPreferences = ClonePreferences(pre);
                    clonedPreferences = SetDefaultGuidForPreferences(clonedPreferences, cloneParameterModel);

                    newPage.Preferences.Add(clonedPreferences);
                }
                if (!page.Graph.IsLoaded)
                {
                    page.Graph.Load();
                }
                List<Graph> graphes = page.Graph.Where(g => g.IsDeleted != true).ToList();
                foreach (Graph graph in graphes)
                {
                    Graph clonedGraph = CloneGraph(graph, cloneParameterModel);
                    clonedGraph = SetDefaultGuidForGraph(clonedGraph, cloneParameterModel);

                    newPage.Graph.Add(clonedGraph);
                }
                return newPage;
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
                    Message = string.Format("ClonePageException:{0}", ex),
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

        public Page SetDefaultGuidForPage(Page needSetDefaultEntity, CloneProgramParameterModel cloneParameterModel)
        {
            Page newEntity = new Page();

            newEntity = needSetDefaultEntity;
            switch (cloneParameterModel.source)
            {
                case DefaultGuidSourceEnum.FromDefaultGuid:
                    //newEntity.DefaultGUID = needSetDefaultEntity.DefaultGUID;//This is default value, don't need assign again.
                    break;
                case DefaultGuidSourceEnum.FromMatchDefaultGuidFunction:
                    bool isMatchDefaultGuidSuccessful = false;//whether default guid can be matched, if not ,set null.

                    Guid fromPageGuid = newEntity.ParentPageGUID == null ? Guid.Empty : (Guid)newEntity.ParentPageGUID;
                    Page fromPage = Resolve<IPageRepository>().GetPageByPageGuid(fromPageGuid);
                    if (fromPage != null)
                    {
                        if (!fromPage.PageSequenceReference.IsLoaded) fromPage.PageSequenceReference.Load();
                        Guid fromPageSequenceGuid = fromPage.PageSequence.PageSequenceGUID;

                        SessionContent fromSessionContentEntity = Resolve<ISessionContentRepository>().GetSessionContentByPageSeqGuid(fromPageSequenceGuid).FirstOrDefault();//FirstOrDefault has some problems if more than one session content has the same pagesequence.
                        if (fromSessionContentEntity != null)
                        {
                            if (!fromSessionContentEntity.SessionReference.IsLoaded) fromSessionContentEntity.SessionReference.Load();

                            Guid fromSessionGuid = fromSessionContentEntity.Session == null ? Guid.Empty : fromSessionContentEntity.Session.SessionGUID; //newEntity.ParentSessionGUID == null ? Guid.Empty : (Guid)newEntity.ParentSessionGUID;
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
                                                SessionContent toDefaultSessionContent = Resolve<ISessionContentRepository>().GetSessionContentBySessionGuidAndOrderNO(toDefaultSession.SessionGUID, fromSessionContentEntity.PageSequenceOrderNo);
                                                if (!toDefaultSessionContent.PageSequenceReference.IsLoaded) toDefaultSessionContent.PageSequenceReference.Load();
                                                Page toDefaultPage = Resolve<IPageRepository>().GetPageByPageSequenceAndOrderNo(toDefaultSessionContent.PageSequence.PageSequenceGUID, fromPage.PageOrderNo);
                                                newEntity.DefaultGUID = toDefaultPage.PageGUID;
                                            }
                                            catch(Exception ex)
                                            {
                                                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                                                isMatchDefaultGuidSuccessful = false;
                                            }
                                        }
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
                    newEntity.DefaultGUID = needSetDefaultEntity.ParentPageGUID;
                    break;
                default:
                    throw new Exception("Error: Dont't find the DefaultGuidSource for Page Entity.");
                    //break;
            }

            return newEntity;
        }
        //DONE
        private PageContent SetDefaultGuidForPageContent(PageContent needSetDefaultEntity, CloneProgramParameterModel cloneParameterModel)
        {
            PageContent newEntity = new PageContent();

            newEntity = needSetDefaultEntity;
            switch (cloneParameterModel.source)
            {
                case DefaultGuidSourceEnum.FromDefaultGuid:
                    //newEntity.DefaultGUID = needSetDefaultEntity.DefaultGUID;//This is default value, don't need assign again.
                    break;
                case DefaultGuidSourceEnum.FromMatchDefaultGuidFunction:
                    bool isMatchDefaultGuidSuccessful = false;//whether default guid can be matched, if not ,set null.
                    Guid fromPageContentGuid = newEntity.ParentPageContentGUID == null ? Guid.Empty : (Guid)newEntity.ParentPageContentGUID;

                    Guid fromPageGuid = fromPageContentGuid;//pageGuid always equals pageContentGuid because pagecontentguid is PK && FK(PageGuid).
                    Page fromPage = Resolve<IPageRepository>().GetPageByPageGuid(fromPageGuid);
                    if (fromPage != null)
                    {
                        if (!fromPage.PageSequenceReference.IsLoaded) fromPage.PageSequenceReference.Load();
                        Guid fromPageSequenceGuid = fromPage.PageSequence.PageSequenceGUID;

                        SessionContent fromSessionContentEntity = Resolve<ISessionContentRepository>().GetSessionContentByPageSeqGuid(fromPageSequenceGuid).FirstOrDefault();//FirstOrDefault has some problems if more than one session content has the same pagesequence.
                        if (fromSessionContentEntity != null)
                        {
                            if (!fromSessionContentEntity.SessionReference.IsLoaded) fromSessionContentEntity.SessionReference.Load();

                            Guid fromSessionGuid = fromSessionContentEntity.Session == null ? Guid.Empty : fromSessionContentEntity.Session.SessionGUID; //newEntity.ParentSessionGUID == null ? Guid.Empty : (Guid)newEntity.ParentSessionGUID;
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
                                                SessionContent toDefaultSessionContent = Resolve<ISessionContentRepository>().GetSessionContentBySessionGuidAndOrderNO(toDefaultSession.SessionGUID, fromSessionContentEntity.PageSequenceOrderNo);
                                                if (!toDefaultSessionContent.PageSequenceReference.IsLoaded) toDefaultSessionContent.PageSequenceReference.Load();
                                                Page toDefaultPage = Resolve<IPageRepository>().GetPageByPageSequenceAndOrderNo(toDefaultSessionContent.PageSequence.PageSequenceGUID, fromPage.PageOrderNo);
                                                newEntity.DefaultGUID = toDefaultPage.PageGUID;//it's equal to PageContentGuid
                                            }
                                            catch(Exception ex)
                                            {
                                                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                                                isMatchDefaultGuidSuccessful = false;
                                            }
                                        }
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
                    newEntity.DefaultGUID = needSetDefaultEntity.ParentPageContentGUID;
                    break;
                default:
                    throw new Exception("Error: Dont't find the DefaultGuidSource for PageContent Entity.");
                    //break;
            }


            return newEntity;
        }

        private ScreenResultTemplatePageLine SetDefaultGuidForPageLine(ScreenResultTemplatePageLine needSetDefaultEntity, CloneProgramParameterModel cloneParameterModel)
        {
            ScreenResultTemplatePageLine newEntity = new ScreenResultTemplatePageLine();

            newEntity = needSetDefaultEntity;
            switch (cloneParameterModel.source)
            {
                case DefaultGuidSourceEnum.FromDefaultGuid:
                    //newEntity.DefaultGUID = needSetDefaultEntity.DefaultGUID;//This is default value, don't need assign again.
                    break;
                case DefaultGuidSourceEnum.FromMatchDefaultGuidFunction:
                    bool isMatchDefaultGuidSuccessful = false;//whether default guid can be matched, if not ,set null.
                    Guid fromPageLineGuid = newEntity.ParentPageLineGUID == null ? Guid.Empty : (Guid)newEntity.ParentPageLineGUID;
                    ScreenResultTemplatePageLine fromPageLineEntity = Resolve<IScreenResultTemplatePageLineRepository>().GetPageLine(fromPageLineGuid);
                    if (fromPageLineEntity != null)
                    {
                        if (!fromPageLineEntity.PageReference.IsLoaded) fromPageLineEntity.PageReference.Load();
                        Page fromPage = fromPageLineEntity.Page;
                        if (fromPage != null)
                        {
                            if (!fromPage.PageSequenceReference.IsLoaded) fromPage.PageSequenceReference.Load();
                            Guid fromPageSequenceGuid = fromPage.PageSequence.PageSequenceGUID;

                            SessionContent fromSessionContentEntity = Resolve<ISessionContentRepository>().GetSessionContentByPageSeqGuid(fromPageSequenceGuid).FirstOrDefault();//FirstOrDefault has some problems if more than one session content has the same pagesequence.
                            if (fromSessionContentEntity != null)
                            {
                                if (!fromSessionContentEntity.SessionReference.IsLoaded) fromSessionContentEntity.SessionReference.Load();

                                Guid fromSessionGuid = fromSessionContentEntity.Session == null ? Guid.Empty : fromSessionContentEntity.Session.SessionGUID; //newEntity.ParentSessionGUID == null ? Guid.Empty : (Guid)newEntity.ParentSessionGUID;
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
                                                    SessionContent toDefaultSessionContent = Resolve<ISessionContentRepository>().GetSessionContentBySessionGuidAndOrderNO(toDefaultSession.SessionGUID, fromSessionContentEntity.PageSequenceOrderNo);
                                                    if (!toDefaultSessionContent.PageSequenceReference.IsLoaded) toDefaultSessionContent.PageSequenceReference.Load();
                                                    Page toDefaultPage = Resolve<IPageRepository>().GetPageByPageSequenceAndOrderNo(toDefaultSessionContent.PageSequence.PageSequenceGUID, fromPage.PageOrderNo);
                                                    ScreenResultTemplatePageLine toDefaultPageLine = Resolve<IScreenResultTemplatePageLineRepository>().GetPageLineByPageGuidAndPageLineOrder(toDefaultPage.PageGUID, fromPageLineEntity.Order.Value);
                                                    newEntity.DefaultGUID = toDefaultPageLine.PageLineGUID;
                                                }
                                                catch(Exception ex)
                                                {
                                                    Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                                                    isMatchDefaultGuidSuccessful = false;
                                                }
                                            }
                                        }
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
                    newEntity.DefaultGUID = needSetDefaultEntity.ParentPageLineGUID;
                    break;
                default:
                    throw new Exception("Error: Dont't find the DefaultGuidSource for PageQuestion Entity.");
                    //break;
            }

            return newEntity;
        }


        //DONE
        private PageQuestion SetDefaultGuidForPageQuestion(PageQuestion needSetDefaultEntity, CloneProgramParameterModel cloneParameterModel)
        {
            PageQuestion newEntity = new PageQuestion();

            newEntity = needSetDefaultEntity;
            switch (cloneParameterModel.source)
            {
                case DefaultGuidSourceEnum.FromDefaultGuid:
                    //newEntity.DefaultGUID = needSetDefaultEntity.DefaultGUID;//This is default value, don't need assign again.
                    break;
                case DefaultGuidSourceEnum.FromMatchDefaultGuidFunction:
                    bool isMatchDefaultGuidSuccessful = false;//whether default guid can be matched, if not ,set null.
                    Guid fromPageQuestionGuid = newEntity.ParentPageQuestionGUID == null ? Guid.Empty : (Guid)newEntity.ParentPageQuestionGUID;
                    PageQuestion fromPageQuestionEntity = Resolve<IPageQuestionRepository>().Get(fromPageQuestionGuid);
                    if (fromPageQuestionEntity != null)
                    {
                        if (!fromPageQuestionEntity.PageReference.IsLoaded) fromPageQuestionEntity.PageReference.Load();
                        Page fromPage = fromPageQuestionEntity.Page;
                        if (fromPage != null)
                        {
                            if (!fromPage.PageSequenceReference.IsLoaded) fromPage.PageSequenceReference.Load();
                            Guid fromPageSequenceGuid = fromPage.PageSequence.PageSequenceGUID;

                            SessionContent fromSessionContentEntity = Resolve<ISessionContentRepository>().GetSessionContentByPageSeqGuid(fromPageSequenceGuid).FirstOrDefault();//FirstOrDefault has some problems if more than one session content has the same pagesequence.
                            if (fromSessionContentEntity != null)
                            {
                                if (!fromSessionContentEntity.SessionReference.IsLoaded) fromSessionContentEntity.SessionReference.Load();

                                Guid fromSessionGuid = fromSessionContentEntity.Session == null ? Guid.Empty : fromSessionContentEntity.Session.SessionGUID; //newEntity.ParentSessionGUID == null ? Guid.Empty : (Guid)newEntity.ParentSessionGUID;
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
                                                    SessionContent toDefaultSessionContent = Resolve<ISessionContentRepository>().GetSessionContentBySessionGuidAndOrderNO(toDefaultSession.SessionGUID, fromSessionContentEntity.PageSequenceOrderNo);
                                                    if (!toDefaultSessionContent.PageSequenceReference.IsLoaded) toDefaultSessionContent.PageSequenceReference.Load();
                                                    Page toDefaultPage = Resolve<IPageRepository>().GetPageByPageSequenceAndOrderNo(toDefaultSessionContent.PageSequence.PageSequenceGUID, fromPage.PageOrderNo);
                                                    PageQuestion toDefaultPageQuestion = Resolve<IPageQuestionRepository>().GetPageQuestionByPageGuidAndQuesOrder(toDefaultPage.PageGUID, fromPageQuestionEntity.Order);
                                                    newEntity.DefaultGUID = toDefaultPageQuestion.PageQuestionGUID;
                                                }
                                                catch(Exception ex)
                                                {
                                                    Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                                                    isMatchDefaultGuidSuccessful = false;
                                                }
                                            }
                                        }
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
                    newEntity.DefaultGUID = needSetDefaultEntity.ParentPageQuestionGUID;
                    break;
                default:
                    throw new Exception("Error: Dont't find the DefaultGuidSource for PageQuestion Entity.");
                    //break;
            }

            return newEntity;
        }
        //DONE
        private PageQuestionContent SetDefaultGuidForPageQuestionContent(PageQuestionContent needSetDefaultEntity, CloneProgramParameterModel cloneParameterModel)
        {
            PageQuestionContent newEntity = new PageQuestionContent();

            newEntity = needSetDefaultEntity;
            switch (cloneParameterModel.source)
            {
                case DefaultGuidSourceEnum.FromDefaultGuid:
                    //newEntity.DefaultGUID = needSetDefaultEntity.DefaultGUID;//This is default value, don't need assign again.
                    break;
                case DefaultGuidSourceEnum.FromMatchDefaultGuidFunction:
                    bool isMatchDefaultGuidSuccessful = false;//whether default guid can be matched, if not ,set null.
                    Guid fromPageQuestionContentGuid = newEntity.ParentPageQuestionContentGUID == null ? Guid.Empty : (Guid)newEntity.ParentPageQuestionContentGUID;

                    Guid fromPageQuestionGuid = fromPageQuestionContentGuid;
                    PageQuestion fromPageQuestionEntity = Resolve<IPageQuestionRepository>().Get(fromPageQuestionGuid);
                    if (fromPageQuestionEntity != null)
                    {
                        if (!fromPageQuestionEntity.PageReference.IsLoaded) fromPageQuestionEntity.PageReference.Load();
                        Page fromPage = fromPageQuestionEntity.Page;
                        if (fromPage != null)
                        {
                            if (!fromPage.PageSequenceReference.IsLoaded) fromPage.PageSequenceReference.Load();
                            Guid fromPageSequenceGuid = fromPage.PageSequence.PageSequenceGUID;

                            SessionContent fromSessionContentEntity = Resolve<ISessionContentRepository>().GetSessionContentByPageSeqGuid(fromPageSequenceGuid).FirstOrDefault();//FirstOrDefault has some problems if more than one session content has the same pagesequence.
                            if (fromSessionContentEntity != null)
                            {
                                if (!fromSessionContentEntity.SessionReference.IsLoaded) fromSessionContentEntity.SessionReference.Load();

                                Guid fromSessionGuid = fromSessionContentEntity.Session == null ? Guid.Empty : fromSessionContentEntity.Session.SessionGUID; //newEntity.ParentSessionGUID == null ? Guid.Empty : (Guid)newEntity.ParentSessionGUID;
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
                                                    SessionContent toDefaultSessionContent = Resolve<ISessionContentRepository>().GetSessionContentBySessionGuidAndOrderNO(toDefaultSession.SessionGUID, fromSessionContentEntity.PageSequenceOrderNo);
                                                    if (!toDefaultSessionContent.PageSequenceReference.IsLoaded) toDefaultSessionContent.PageSequenceReference.Load();
                                                    Page toDefaultPage = Resolve<IPageRepository>().GetPageByPageSequenceAndOrderNo(toDefaultSessionContent.PageSequence.PageSequenceGUID, fromPage.PageOrderNo);
                                                    PageQuestion toDefaultPageQuestion = Resolve<IPageQuestionRepository>().GetPageQuestionByPageGuidAndQuesOrder(toDefaultPage.PageGUID, fromPageQuestionEntity.Order);
                                                    newEntity.DefaultGUID = toDefaultPageQuestion.PageQuestionGUID;
                                                }
                                                catch(Exception ex)
                                                {
                                                    Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                                                    isMatchDefaultGuidSuccessful = false;
                                                }
                                            }
                                        }
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
                    newEntity.DefaultGUID = needSetDefaultEntity.ParentPageQuestionContentGUID;
                    break;
                default:
                    throw new Exception("Error: Dont't find the DefaultGuidSource for PageQuestionContent Entity.");
                    //break;
            }

            return newEntity;
        }
        //DONE
        private PageQuestionItem SetDefaultGuidForPageQuestionItem(PageQuestionItem needSetDefaultEntity, CloneProgramParameterModel cloneParameterModel)
        {
            PageQuestionItem newEntity = new PageQuestionItem();

            newEntity = needSetDefaultEntity;
            switch (cloneParameterModel.source)
            {
                case DefaultGuidSourceEnum.FromDefaultGuid:
                    //newEntity.DefaultGUID = needSetDefaultEntity.DefaultGUID;//This is default value, don't need assign again.
                    break;
                case DefaultGuidSourceEnum.FromMatchDefaultGuidFunction:
                    bool isMatchDefaultGuidSuccessful = false;//whether default guid can be matched, if not ,set null.
                    Guid fromPageQuestionItemGuid = newEntity.ParentPageQuestionItemGUID == null ? Guid.Empty : (Guid)newEntity.ParentPageQuestionItemGUID;
                    PageQuestionItem fromPageQuestionItemEntity = Resolve<IPageQuestionItemRepository>().Get(fromPageQuestionItemGuid);
                    if (fromPageQuestionItemEntity != null)
                    {
                        if (!fromPageQuestionItemEntity.PageQuestionReference.IsLoaded) fromPageQuestionItemEntity.PageQuestionReference.Load();
                        PageQuestion fromPageQuestionEntity = fromPageQuestionItemEntity.PageQuestion;//Resolve<IPageQuestionRepository>().Get(fromPageQuestionGuid);
                        if (fromPageQuestionEntity != null)
                        {
                            if (!fromPageQuestionEntity.PageReference.IsLoaded) fromPageQuestionEntity.PageReference.Load();
                            Page fromPage = fromPageQuestionEntity.Page;
                            if (fromPage != null)
                            {
                                if (!fromPage.PageSequenceReference.IsLoaded) fromPage.PageSequenceReference.Load();
                                Guid fromPageSequenceGuid = fromPage.PageSequence.PageSequenceGUID;

                                SessionContent fromSessionContentEntity = Resolve<ISessionContentRepository>().GetSessionContentByPageSeqGuid(fromPageSequenceGuid).FirstOrDefault();//FirstOrDefault has some problems if more than one session content has the same pagesequence.
                                if (fromSessionContentEntity != null)
                                {
                                    if (!fromSessionContentEntity.SessionReference.IsLoaded) fromSessionContentEntity.SessionReference.Load();

                                    Guid fromSessionGuid = fromSessionContentEntity.Session == null ? Guid.Empty : fromSessionContentEntity.Session.SessionGUID; //newEntity.ParentSessionGUID == null ? Guid.Empty : (Guid)newEntity.ParentSessionGUID;
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
                                                        SessionContent toDefaultSessionContent = Resolve<ISessionContentRepository>().GetSessionContentBySessionGuidAndOrderNO(toDefaultSession.SessionGUID, fromSessionContentEntity.PageSequenceOrderNo);
                                                        if (!toDefaultSessionContent.PageSequenceReference.IsLoaded) toDefaultSessionContent.PageSequenceReference.Load();
                                                        Page toDefaultPage = Resolve<IPageRepository>().GetPageByPageSequenceAndOrderNo(toDefaultSessionContent.PageSequence.PageSequenceGUID, fromPage.PageOrderNo);
                                                        PageQuestion toDefaultPageQuestion = Resolve<IPageQuestionRepository>().GetPageQuestionByPageGuidAndQuesOrder(toDefaultPage.PageGUID, fromPageQuestionEntity.Order);
                                                        PageQuestionItem toDefaultPageQuestionItem = Resolve<IPageQuestionItemRepository>().GetByPageQuestionGuidAndOrder(toDefaultPageQuestion.PageQuestionGUID, (int)fromPageQuestionItemEntity.Order);
                                                        newEntity.DefaultGUID = toDefaultPageQuestionItem.PageQuestionItemGUID;
                                                    }
                                                    catch(Exception ex)
                                                    {
                                                        Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                                                        isMatchDefaultGuidSuccessful = false;
                                                    }
                                                }
                                            }
                                        }
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
                    newEntity.DefaultGUID = needSetDefaultEntity.ParentPageQuestionItemGUID;
                    break;
                default:
                    throw new Exception("Error: Dont't find the DefaultGuidSource for PageQuestionItem Entity.");
                    //break;
            }

            return newEntity;
        }
        //DONE
        private PageQuestionItemContent SetDefaultGuidForPageQuestionItemContent(PageQuestionItemContent needSetDefaultEntity, CloneProgramParameterModel cloneParameterModel)
        {
            PageQuestionItemContent newEntity = new PageQuestionItemContent();

            newEntity = needSetDefaultEntity;
            switch (cloneParameterModel.source)
            {
                case DefaultGuidSourceEnum.FromDefaultGuid:
                    //newEntity.DefaultGUID = needSetDefaultEntity.DefaultGUID;//This is default value, don't need assign again.
                    break;
                case DefaultGuidSourceEnum.FromMatchDefaultGuidFunction:
                    bool isMatchDefaultGuidSuccessful = false;//whether default guid can be matched, if not ,set null.
                    Guid fromPageQuestionItemContentGuid = newEntity.ParentPageQuestionItemContentGUID == null ? Guid.Empty : (Guid)newEntity.ParentPageQuestionItemContentGUID;

                    Guid fromPageQuestionItemGuid = fromPageQuestionItemContentGuid;
                    PageQuestionItem fromPageQuestionItemEntity = Resolve<IPageQuestionItemRepository>().Get(fromPageQuestionItemGuid);
                    if (fromPageQuestionItemEntity != null)
                    {
                        if (!fromPageQuestionItemEntity.PageQuestionReference.IsLoaded) fromPageQuestionItemEntity.PageQuestionReference.Load();
                        PageQuestion fromPageQuestionEntity = fromPageQuestionItemEntity.PageQuestion;//Resolve<IPageQuestionRepository>().Get(fromPageQuestionGuid);
                        if (fromPageQuestionEntity != null)
                        {
                            if (!fromPageQuestionEntity.PageReference.IsLoaded) fromPageQuestionEntity.PageReference.Load();
                            Page fromPage = fromPageQuestionEntity.Page;
                            if (fromPage != null)
                            {
                                if (!fromPage.PageSequenceReference.IsLoaded) fromPage.PageSequenceReference.Load();
                                Guid fromPageSequenceGuid = fromPage.PageSequence.PageSequenceGUID;

                                SessionContent fromSessionContentEntity = Resolve<ISessionContentRepository>().GetSessionContentByPageSeqGuid(fromPageSequenceGuid).FirstOrDefault();//FirstOrDefault has some problems if more than one session content has the same pagesequence.
                                if (fromSessionContentEntity != null)
                                {
                                    if (!fromSessionContentEntity.SessionReference.IsLoaded) fromSessionContentEntity.SessionReference.Load();

                                    Guid fromSessionGuid = fromSessionContentEntity.Session == null ? Guid.Empty : fromSessionContentEntity.Session.SessionGUID; //newEntity.ParentSessionGUID == null ? Guid.Empty : (Guid)newEntity.ParentSessionGUID;
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
                                                        SessionContent toDefaultSessionContent = Resolve<ISessionContentRepository>().GetSessionContentBySessionGuidAndOrderNO(toDefaultSession.SessionGUID, fromSessionContentEntity.PageSequenceOrderNo);
                                                        if (!toDefaultSessionContent.PageSequenceReference.IsLoaded) toDefaultSessionContent.PageSequenceReference.Load();
                                                        Page toDefaultPage = Resolve<IPageRepository>().GetPageByPageSequenceAndOrderNo(toDefaultSessionContent.PageSequence.PageSequenceGUID, fromPage.PageOrderNo);
                                                        PageQuestion toDefaultPageQuestion = Resolve<IPageQuestionRepository>().GetPageQuestionByPageGuidAndQuesOrder(toDefaultPage.PageGUID, fromPageQuestionEntity.Order);
                                                        PageQuestionItem toDefaultPageQuestionItem = Resolve<IPageQuestionItemRepository>().GetByPageQuestionGuidAndOrder(toDefaultPageQuestion.PageQuestionGUID, (int)fromPageQuestionItemEntity.Order);
                                                        newEntity.DefaultGUID = toDefaultPageQuestionItem.PageQuestionItemGUID;
                                                    }
                                                    catch(Exception ex)
                                                    {
                                                        Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                                                        isMatchDefaultGuidSuccessful = false;
                                                    }
                                                }
                                            }
                                        }
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
                    newEntity.DefaultGUID = needSetDefaultEntity.ParentPageQuestionItemContentGUID;
                    break;
                default:
                    throw new Exception("Error: Dont't find the DefaultGuidSource for PageQuestionItemContent Entity.");
                    //break;
            }

            return newEntity;
        }
        //DONE
        private PageMedia SetDefaultGuidForPageMedia(PageMedia needSetDefaultEntity, CloneProgramParameterModel cloneParameterModel)
        {
            PageMedia newEntity = new PageMedia();

            newEntity = needSetDefaultEntity;
            switch (cloneParameterModel.source)
            {
                case DefaultGuidSourceEnum.FromDefaultGuid:
                    //newEntity.DefaultGUID = needSetDefaultEntity.DefaultGUID;//This is default value, don't need assign again.
                    break;
                case DefaultGuidSourceEnum.FromMatchDefaultGuidFunction:
                    bool isMatchDefaultGuidSuccessful = false;//whether default guid can be matched, if not ,set null.
                    Guid fromPageMediaGuid = newEntity.ParentPageMediaItemGUID == null ? Guid.Empty : (Guid)newEntity.ParentPageMediaItemGUID;  //pageMediaGuid===pageGuid  always.
                    Guid fromPageGuid = fromPageMediaGuid;
                    Page fromPage = Resolve<IPageRepository>().GetPageByPageGuid(fromPageGuid);
                    if (fromPage != null)
                    {
                        if (!fromPage.PageSequenceReference.IsLoaded) fromPage.PageSequenceReference.Load();
                        Guid fromPageSequenceGuid = fromPage.PageSequence.PageSequenceGUID;

                        SessionContent fromSessionContentEntity = Resolve<ISessionContentRepository>().GetSessionContentByPageSeqGuid(fromPageSequenceGuid).FirstOrDefault();//FirstOrDefault has some problems if more than one session content has the same pagesequence.
                        if (fromSessionContentEntity != null)
                        {
                            if (!fromSessionContentEntity.SessionReference.IsLoaded) fromSessionContentEntity.SessionReference.Load();

                            Guid fromSessionGuid = fromSessionContentEntity.Session == null ? Guid.Empty : fromSessionContentEntity.Session.SessionGUID; //newEntity.ParentSessionGUID == null ? Guid.Empty : (Guid)newEntity.ParentSessionGUID;
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
                                                SessionContent toDefaultSessionContent = Resolve<ISessionContentRepository>().GetSessionContentBySessionGuidAndOrderNO(toDefaultSession.SessionGUID, fromSessionContentEntity.PageSequenceOrderNo);
                                                if (!toDefaultSessionContent.PageSequenceReference.IsLoaded) toDefaultSessionContent.PageSequenceReference.Load();
                                                Page toDefaultPage = Resolve<IPageRepository>().GetPageByPageSequenceAndOrderNo(toDefaultSessionContent.PageSequence.PageSequenceGUID, fromPage.PageOrderNo);
                                                newEntity.DefaultGUID = toDefaultPage.PageGUID;//=== pageMediaGuid
                                            }
                                            catch(Exception ex)
                                            {
                                                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                                                isMatchDefaultGuidSuccessful = false;
                                            }
                                        }
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
                    newEntity.DefaultGUID = needSetDefaultEntity.ParentPageMediaItemGUID;
                    break;
                default:
                    throw new Exception("Error: Dont't find the DefaultGuidSource for PageMedia Entity.");
                    //break;
            }

            return newEntity;
        }
        //DONE
        private Preferences SetDefaultGuidForPreferences(Preferences needSetDefaultEntity, CloneProgramParameterModel cloneParameterModel)
        {
            Preferences newEntity = new Preferences();

            newEntity = needSetDefaultEntity;
            switch (cloneParameterModel.source)
            {
                case DefaultGuidSourceEnum.FromDefaultGuid:
                    //newEntity.DefaultGUID = needSetDefaultEntity.DefaultGUID;//This is default value, don't need assign again.
                    break;
                case DefaultGuidSourceEnum.FromMatchDefaultGuidFunction:
                    bool isMatchDefaultGuidSuccessful = false;//whether default guid can be matched, if not ,set null.
                    Guid fromPreferencesGuid = newEntity.ParentPreferencesGUID == null ? Guid.Empty : (Guid)newEntity.ParentPreferencesGUID;
                    Preferences fromPreferencesEntity = Resolve<IPreferencesRepository>().GetPreference(fromPreferencesGuid);
                    if (fromPreferencesEntity != null)
                    {
                        if (!fromPreferencesEntity.PageReference.IsLoaded) fromPreferencesEntity.PageReference.Load();
                        Page fromPage = fromPreferencesEntity.Page;
                        if (fromPage != null)
                        {
                            if (!fromPage.PageSequenceReference.IsLoaded) fromPage.PageSequenceReference.Load();
                            Guid fromPageSequenceGuid = fromPage.PageSequence.PageSequenceGUID;

                            SessionContent fromSessionContentEntity = Resolve<ISessionContentRepository>().GetSessionContentByPageSeqGuid(fromPageSequenceGuid).FirstOrDefault();//FirstOrDefault has some problems if more than one session content has the same pagesequence.
                            if (fromSessionContentEntity != null)
                            {
                                if (!fromSessionContentEntity.SessionReference.IsLoaded) fromSessionContentEntity.SessionReference.Load();

                                Guid fromSessionGuid = fromSessionContentEntity.Session == null ? Guid.Empty : fromSessionContentEntity.Session.SessionGUID; //newEntity.ParentSessionGUID == null ? Guid.Empty : (Guid)newEntity.ParentSessionGUID;
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
                                                    SessionContent toDefaultSessionContent = Resolve<ISessionContentRepository>().GetSessionContentBySessionGuidAndOrderNO(toDefaultSession.SessionGUID, fromSessionContentEntity.PageSequenceOrderNo);
                                                    if (!toDefaultSessionContent.PageSequenceReference.IsLoaded) toDefaultSessionContent.PageSequenceReference.Load();
                                                    Page toDefaultPage = Resolve<IPageRepository>().GetPageByPageSequenceAndOrderNo(toDefaultSessionContent.PageSequence.PageSequenceGUID, fromPage.PageOrderNo);
                                                    if (!fromPreferencesEntity.ResourceReference.IsLoaded) fromPreferencesEntity.ResourceReference.Load();
                                                    Preferences toDefaultPreferences = Resolve<IPreferencesRepository>().GetPreferenceByPageGuid(toDefaultPage.PageGUID).Where(p => p.Resource.ResourceGUID == fromPreferencesEntity.Resource.ResourceGUID).FirstOrDefault();
                                                    newEntity.DefaultGUID = toDefaultPreferences.PreferencesGUID;
                                                }
                                                catch(Exception ex)
                                                {
                                                    Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                                                    isMatchDefaultGuidSuccessful = false;
                                                }
                                            }
                                        }
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
                    newEntity.DefaultGUID = needSetDefaultEntity.ParentPreferencesGUID;
                    break;
                default:
                    throw new Exception("Error: Dont't find the DefaultGuidSource for Preferences Entity.");
                    //break;
            }

            return newEntity;
        }
        //DONE
        private Graph SetDefaultGuidForGraph(Graph needSetDefaultEntity, CloneProgramParameterModel cloneParameterModel)
        {
            Graph newEntity = new Graph();

            newEntity = needSetDefaultEntity;
            switch (cloneParameterModel.source)
            {
                case DefaultGuidSourceEnum.FromDefaultGuid:
                    //newEntity.DefaultGUID = needSetDefaultEntity.DefaultGUID;//This is default value, don't need assign again.
                    break;
                case DefaultGuidSourceEnum.FromMatchDefaultGuidFunction:
                    bool isMatchDefaultGuidSuccessful = false;//whether default guid can be matched, if not ,set null.
                    Guid fromGraphGuid = newEntity.ParentGraphGUID == null ? Guid.Empty : (Guid)newEntity.ParentGraphGUID;
                    Graph fromGraphEntity = Resolve<IGraphRepository>().Get(fromGraphGuid);
                    if (fromGraphEntity != null)
                    {
                        if (!fromGraphEntity.PageReference.IsLoaded) fromGraphEntity.PageReference.Load();
                        Page fromPage = fromGraphEntity.Page;
                        if (fromPage != null)
                        {
                            if (!fromPage.PageSequenceReference.IsLoaded) fromPage.PageSequenceReference.Load();
                            Guid fromPageSequenceGuid = fromPage.PageSequence.PageSequenceGUID;

                            SessionContent fromSessionContentEntity = Resolve<ISessionContentRepository>().GetSessionContentByPageSeqGuid(fromPageSequenceGuid).FirstOrDefault();//FirstOrDefault has some problems if more than one session content has the same pagesequence.
                            if (fromSessionContentEntity != null)
                            {
                                if (!fromSessionContentEntity.SessionReference.IsLoaded) fromSessionContentEntity.SessionReference.Load();

                                Guid fromSessionGuid = fromSessionContentEntity.Session == null ? Guid.Empty : fromSessionContentEntity.Session.SessionGUID; //newEntity.ParentSessionGUID == null ? Guid.Empty : (Guid)newEntity.ParentSessionGUID;
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
                                                    SessionContent toDefaultSessionContent = Resolve<ISessionContentRepository>().GetSessionContentBySessionGuidAndOrderNO(toDefaultSession.SessionGUID, fromSessionContentEntity.PageSequenceOrderNo);
                                                    if (!toDefaultSessionContent.PageSequenceReference.IsLoaded) toDefaultSessionContent.PageSequenceReference.Load();
                                                    Page toDefaultPage = Resolve<IPageRepository>().GetPageByPageSequenceAndOrderNo(toDefaultSessionContent.PageSequence.PageSequenceGUID, fromPage.PageOrderNo);
                                                    Graph toDefaultGraph = Resolve<IGraphRepository>().GetGraphByPageGuid(toDefaultPage.PageGUID);
                                                    newEntity.DefaultGUID = toDefaultGraph.GraphGUID;
                                                }
                                                catch(Exception ex)
                                                {
                                                    Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                                                    isMatchDefaultGuidSuccessful = false;
                                                }
                                            }
                                        }
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
                    newEntity.DefaultGUID = needSetDefaultEntity.ParentGraphGUID;
                    break;
                default:
                    throw new Exception("Error: Dont't find the DefaultGuidSource for Graph Entity.");
                    //break;
            }

            return newEntity;
        }
        //DONE
        private GraphContent SetDefaultGuidForGraphContent(GraphContent needSetDefaultEntity, CloneProgramParameterModel cloneParameterModel)
        {
            GraphContent newEntity = new GraphContent();

            newEntity = needSetDefaultEntity;
            switch (cloneParameterModel.source)
            {
                case DefaultGuidSourceEnum.FromDefaultGuid:
                    //newEntity.DefaultGUID = needSetDefaultEntity.DefaultGUID;//This is default value, don't need assign again.
                    break;
                case DefaultGuidSourceEnum.FromMatchDefaultGuidFunction:
                    bool isMatchDefaultGuidSuccessful = false;//whether default guid can be matched, if not ,set null.
                    Guid fromGraphContentGuid = newEntity.ParentGraphContentGUID == null ? Guid.Empty : (Guid)newEntity.ParentGraphContentGUID;
                    Guid fromGraphGuid = fromGraphContentGuid;
                    Graph fromGraphEntity = Resolve<IGraphRepository>().Get(fromGraphGuid);
                    if (fromGraphEntity != null)
                    {
                        if (!fromGraphEntity.PageReference.IsLoaded) fromGraphEntity.PageReference.Load();
                        Page fromPage = fromGraphEntity.Page;
                        if (fromPage != null)
                        {
                            if (!fromPage.PageSequenceReference.IsLoaded) fromPage.PageSequenceReference.Load();
                            Guid fromPageSequenceGuid = fromPage.PageSequence.PageSequenceGUID;

                            SessionContent fromSessionContentEntity = Resolve<ISessionContentRepository>().GetSessionContentByPageSeqGuid(fromPageSequenceGuid).FirstOrDefault();//FirstOrDefault has some problems if more than one session content has the same pagesequence.
                            if (fromSessionContentEntity != null)
                            {
                                if (!fromSessionContentEntity.SessionReference.IsLoaded) fromSessionContentEntity.SessionReference.Load();

                                Guid fromSessionGuid = fromSessionContentEntity.Session == null ? Guid.Empty : fromSessionContentEntity.Session.SessionGUID; //newEntity.ParentSessionGUID == null ? Guid.Empty : (Guid)newEntity.ParentSessionGUID;
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
                                                    SessionContent toDefaultSessionContent = Resolve<ISessionContentRepository>().GetSessionContentBySessionGuidAndOrderNO(toDefaultSession.SessionGUID, fromSessionContentEntity.PageSequenceOrderNo);
                                                    if (!toDefaultSessionContent.PageSequenceReference.IsLoaded) toDefaultSessionContent.PageSequenceReference.Load();
                                                    Page toDefaultPage = Resolve<IPageRepository>().GetPageByPageSequenceAndOrderNo(toDefaultSessionContent.PageSequence.PageSequenceGUID, fromPage.PageOrderNo);
                                                    Graph toDefaultGraph = Resolve<IGraphRepository>().GetGraphByPageGuid(toDefaultPage.PageGUID);
                                                    newEntity.DefaultGUID = toDefaultGraph.GraphGUID;
                                                }
                                                catch(Exception ex)
                                                {
                                                    Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                                                    isMatchDefaultGuidSuccessful = false;
                                                }
                                            }
                                        }
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
                    newEntity.DefaultGUID = needSetDefaultEntity.ParentGraphContentGUID;
                    break;
                default:
                    throw new Exception("Error: Dont't find the DefaultGuidSource for GraphContent Entity.");
                    //break;
            }

            return newEntity;
        }
        //DONE
        private GraphItem SetDefaultGuidForGraphItem(GraphItem needSetDefaultEntity, CloneProgramParameterModel cloneParameterModel)
        {
            GraphItem newEntity = new GraphItem();

            newEntity = needSetDefaultEntity;
            switch (cloneParameterModel.source)
            {
                case DefaultGuidSourceEnum.FromDefaultGuid:
                    //newEntity.DefaultGUID = needSetDefaultEntity.DefaultGUID;//This is default value, don't need assign again.
                    break;
                case DefaultGuidSourceEnum.FromMatchDefaultGuidFunction:
                    bool isMatchDefaultGuidSuccessful = false;//whether default guid can be matched, if not ,set null.
                    Guid fromGraphItemGuid = newEntity.ParentGraphItemGUID == null ? Guid.Empty : (Guid)newEntity.ParentGraphItemGUID;
                    GraphItem fromGraphItemEntity = Resolve<IGraphItemRepository>().get(fromGraphItemGuid);
                    if (fromGraphItemEntity != null)
                    {
                        if (!fromGraphItemEntity.GraphReference.IsLoaded) fromGraphItemEntity.GraphReference.Load();
                        Graph fromGraphEntity = fromGraphItemEntity.Graph;
                        if (fromGraphEntity != null)
                        {
                            if (!fromGraphEntity.PageReference.IsLoaded) fromGraphEntity.PageReference.Load();
                            Page fromPage = fromGraphEntity.Page;
                            if (fromPage != null)
                            {
                                if (!fromPage.PageSequenceReference.IsLoaded) fromPage.PageSequenceReference.Load();
                                Guid fromPageSequenceGuid = fromPage.PageSequence.PageSequenceGUID;

                                SessionContent fromSessionContentEntity = Resolve<ISessionContentRepository>().GetSessionContentByPageSeqGuid(fromPageSequenceGuid).FirstOrDefault();//FirstOrDefault has some problems if more than one session content has the same pagesequence.
                                if (fromSessionContentEntity != null)
                                {
                                    if (!fromSessionContentEntity.SessionReference.IsLoaded) fromSessionContentEntity.SessionReference.Load();

                                    Guid fromSessionGuid = fromSessionContentEntity.Session == null ? Guid.Empty : fromSessionContentEntity.Session.SessionGUID; //newEntity.ParentSessionGUID == null ? Guid.Empty : (Guid)newEntity.ParentSessionGUID;
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
                                                        SessionContent toDefaultSessionContent = Resolve<ISessionContentRepository>().GetSessionContentBySessionGuidAndOrderNO(toDefaultSession.SessionGUID, fromSessionContentEntity.PageSequenceOrderNo);
                                                        if (!toDefaultSessionContent.PageSequenceReference.IsLoaded) toDefaultSessionContent.PageSequenceReference.Load();
                                                        Page toDefaultPage = Resolve<IPageRepository>().GetPageByPageSequenceAndOrderNo(toDefaultSessionContent.PageSequence.PageSequenceGUID, fromPage.PageOrderNo);
                                                        Graph toDefaultGraph = Resolve<IGraphRepository>().GetGraphByPageGuid(toDefaultPage.PageGUID);
                                                        GraphItem toDefaultGraphItem = Resolve<IGraphItemRepository>().GetGraphItemByGraph(toDefaultGraph.GraphGUID).Where(g => g.Name == fromGraphItemEntity.Name).FirstOrDefault();
                                                        newEntity.DefaultGUID = toDefaultGraphItem.GraphItemGUID;
                                                    }
                                                    catch(Exception ex)
                                                    {
                                                        Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                                                        isMatchDefaultGuidSuccessful = false;
                                                    }
                                                }
                                            }
                                        }
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
                    newEntity.DefaultGUID = needSetDefaultEntity.ParentGraphItemGUID;
                    break;
                default:
                    throw new Exception("Error: Dont't find the DefaultGuidSource for GraphItem Entity.");
                    //break;
            }

            return newEntity;
        }
        //DONE
        private GraphItemContent SetDefaultGuidForGraphItemContent(GraphItemContent needSetDefaultEntity, CloneProgramParameterModel cloneParameterModel)
        {
            GraphItemContent newEntity = new GraphItemContent();

            newEntity = needSetDefaultEntity;
            switch (cloneParameterModel.source)
            {
                case DefaultGuidSourceEnum.FromDefaultGuid:
                    //newEntity.DefaultGUID = needSetDefaultEntity.DefaultGUID;//This is default value, don't need assign again.
                    break;
                case DefaultGuidSourceEnum.FromMatchDefaultGuidFunction:
                    bool isMatchDefaultGuidSuccessful = false;//whether default guid can be matched, if not ,set null.
                    Guid fromGraphItemContentGuid = newEntity.ParentGraphItemContentGUID == null ? Guid.Empty : (Guid)newEntity.ParentGraphItemContentGUID;
                    Guid fromGraphItemGuid = fromGraphItemContentGuid;
                    GraphItem fromGraphItemEntity = Resolve<IGraphItemRepository>().get(fromGraphItemGuid);
                    if (fromGraphItemEntity != null)
                    {
                        if (!fromGraphItemEntity.GraphReference.IsLoaded) fromGraphItemEntity.GraphReference.Load();
                        Graph fromGraphEntity = fromGraphItemEntity.Graph;
                        if (fromGraphEntity != null)
                        {
                            if (!fromGraphEntity.PageReference.IsLoaded) fromGraphEntity.PageReference.Load();
                            Page fromPage = fromGraphEntity.Page;
                            if (fromPage != null)
                            {
                                if (!fromPage.PageSequenceReference.IsLoaded) fromPage.PageSequenceReference.Load();
                                Guid fromPageSequenceGuid = fromPage.PageSequence.PageSequenceGUID;

                                SessionContent fromSessionContentEntity = Resolve<ISessionContentRepository>().GetSessionContentByPageSeqGuid(fromPageSequenceGuid).FirstOrDefault();//FirstOrDefault has some problems if more than one session content has the same pagesequence.
                                if (fromSessionContentEntity != null)
                                {
                                    if (!fromSessionContentEntity.SessionReference.IsLoaded) fromSessionContentEntity.SessionReference.Load();

                                    Guid fromSessionGuid = fromSessionContentEntity.Session == null ? Guid.Empty : fromSessionContentEntity.Session.SessionGUID; //newEntity.ParentSessionGUID == null ? Guid.Empty : (Guid)newEntity.ParentSessionGUID;
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
                                                        SessionContent toDefaultSessionContent = Resolve<ISessionContentRepository>().GetSessionContentBySessionGuidAndOrderNO(toDefaultSession.SessionGUID, fromSessionContentEntity.PageSequenceOrderNo);
                                                        if (!toDefaultSessionContent.PageSequenceReference.IsLoaded) toDefaultSessionContent.PageSequenceReference.Load();
                                                        Page toDefaultPage = Resolve<IPageRepository>().GetPageByPageSequenceAndOrderNo(toDefaultSessionContent.PageSequence.PageSequenceGUID, fromPage.PageOrderNo);
                                                        Graph toDefaultGraph = Resolve<IGraphRepository>().GetGraphByPageGuid(toDefaultPage.PageGUID);
                                                        GraphItem toDefaultGraphItem = Resolve<IGraphItemRepository>().GetGraphItemByGraph(toDefaultGraph.GraphGUID).Where(g => g.Name == fromGraphItemEntity.Name).FirstOrDefault();
                                                        newEntity.DefaultGUID = toDefaultGraphItem.GraphItemGUID;
                                                    }
                                                    catch(Exception ex)
                                                    {
                                                        Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                                                        isMatchDefaultGuidSuccessful = false;
                                                    }
                                                }
                                            }
                                        }
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
                    newEntity.DefaultGUID = needSetDefaultEntity.ParentGraphItemContentGUID;
                    break;
                default:
                    throw new Exception("Error: Dont't find the DefaultGuidSource for GraphItemContent Entity.");
                    //break;
            }

            return newEntity;
        }

        private PageContent ClonePageContent(PageContent pageContent, List<KeyValuePair<string, string>> cloneRelapseGUIDList)
        {
            try
            {
                PageContent clonePageContent = new PageContent();
                clonePageContent.ParentPageContentGUID = pageContent.PageGUID;
                clonePageContent.DefaultGUID = pageContent.DefaultGUID;
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
                clonePageContent.IsDeleted = pageContent.IsDeleted;
                clonePageContent.ImageMode = pageContent.ImageMode;

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

                if (!pageContent.Resource_BackgroundImageReference.IsLoaded)
                {
                    pageContent.Resource_BackgroundImageReference.Load();
                }
                clonePageContent.Resource_BackgroundImage = pageContent.Resource_BackgroundImage;

                if (!pageContent.Resource_IllustrationImageReference.IsLoaded)
                {
                    pageContent.Resource_IllustrationImageReference.Load();
                }
                clonePageContent.Resource_IllustrationImage = pageContent.Resource_IllustrationImage;

                if (!pageContent.Resource_PresenterImageReference.IsLoaded)
                {
                    pageContent.Resource_PresenterImageReference.Load();
                }
                clonePageContent.Resource_PresenterImage = pageContent.Resource_PresenterImage;

                if (!pageContent.Resource_PageGraphic1Reference.IsLoaded)
                {
                    pageContent.Resource_PageGraphic1Reference.Load();
                }
                clonePageContent.Resource_PageGraphic1 = pageContent.Resource_PageGraphic1;
                clonePageContent.PageGraphic1GUID = pageContent.PageGraphic1GUID;

                if (!pageContent.Resource_PageGraphic2Reference.IsLoaded)
                {
                    pageContent.Resource_PageGraphic2Reference.Load();
                }
                clonePageContent.Resource_PageGraphic2 = pageContent.Resource_PageGraphic2;
                clonePageContent.PageGraphic2GUID = pageContent.PageGraphic2GUID;

                if (!pageContent.Resource_PageGraphic3Reference.IsLoaded)
                {
                    pageContent.Resource_PageGraphic3Reference.Load();
                }
                clonePageContent.Resource_PageGraphic3 = pageContent.Resource_PageGraphic3;
                clonePageContent.PageGraphic2GUID = pageContent.PageGraphic2GUID;

                 
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

        //ClonePageLine
        private ScreenResultTemplatePageLine ClonePageLine(ScreenResultTemplatePageLine pageLine, CloneProgramParameterModel cloneParameterModel)
        {
            try
            {
                ScreenResultTemplatePageLine clonePageLine = new ScreenResultTemplatePageLine();
                clonePageLine.Order = pageLine.Order;
                clonePageLine.PageLineGUID = Guid.NewGuid();
                clonePageLine.Text = pageLine.Text;
                clonePageLine.URL = pageLine.URL;
                clonePageLine.PageVariableGUID = pageLine.PageVariableGUID;
                clonePageLine.ParentPageLineGUID = pageLine.PageLineGUID;
                clonePageLine.DefaultGUID = pageLine.DefaultGUID;
                clonePageLine.IsDeleted = pageLine.IsDeleted;
                // Clone page variable
                if (!pageLine.PageVariableReference.IsLoaded)
                {
                    pageLine.PageVariableReference.Load();
                }
                clonePageLine.PageVariable = pageLine.PageVariable;

                return clonePageLine;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private PageQuestion ClonePageQuestion(PageQuestion pageQuestion, CloneProgramParameterModel cloneParameterModel)
        {
            try
            {
                PageQuestion clonePageQuestion = new PageQuestion();
                clonePageQuestion.Order = pageQuestion.Order;
                clonePageQuestion.PageQuestionGUID = Guid.NewGuid();
                clonePageQuestion.IsRequired = pageQuestion.IsRequired;

                clonePageQuestion.ParentPageQuestionGUID = pageQuestion.PageQuestionGUID;
                clonePageQuestion.DefaultGUID = pageQuestion.DefaultGUID;
                clonePageQuestion.IsDeleted = pageQuestion.IsDeleted;
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
                    PageQuestionContent clonedPageQuestionContent = ClonePageQuestionContent(pageQuestion.PageQuestionContent);
                    clonedPageQuestionContent = SetDefaultGuidForPageQuestionContent(clonedPageQuestionContent, cloneParameterModel);


                    clonePageQuestion.PageQuestionContent = clonedPageQuestionContent;
                }

                // Clone page question's items
                if (!pageQuestion.PageQuestionItem.IsLoaded)
                {
                    pageQuestion.PageQuestionItem.Load();
                }

                List<PageQuestionItem> pageQuestionItems = pageQuestion.PageQuestionItem.Where(p => p.IsDeleted != true).ToList();
                foreach (PageQuestionItem questionItem in pageQuestionItems)
                {
                    PageQuestionItem clonedQuestionItem = ClonePageQuestionItem(questionItem, cloneParameterModel);
                    clonedQuestionItem = SetDefaultGuidForPageQuestionItem(clonedQuestionItem, cloneParameterModel);

                    clonePageQuestion.PageQuestionItem.Add(clonedQuestionItem);
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

        private PageQuestionContent ClonePageQuestionContent(PageQuestionContent questionContent)
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
                cloneQuestionContent.DefaultGUID = questionContent.DefaultGUID;
                cloneQuestionContent.Caption = questionContent.Caption;
                return cloneQuestionContent;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private PageQuestionItem ClonePageQuestionItem(PageQuestionItem questionItem, CloneProgramParameterModel cloneParameterModel)
        {
            try
            {
                PageQuestionItem cloneQuestionItem = new PageQuestionItem();
                cloneQuestionItem.PageQuestionItemGUID = Guid.NewGuid();
                cloneQuestionItem.Order = questionItem.Order;
                cloneQuestionItem.ParentPageQuestionItemGUID = questionItem.PageQuestionItemGUID;
                cloneQuestionItem.DefaultGUID = questionItem.DefaultGUID;
                if (!questionItem.PageQuestionItemContentReference.IsLoaded)
                {
                    questionItem.PageQuestionItemContentReference.Load();
                }
                //List<PageQuestionItemContent> pageQuestionItemContents = questionItem.PageQuestionItemContent.Where(p => p.IsDeleted != true).ToList();
                if (questionItem.PageQuestionItemContent != null)
                {
                    PageQuestionItemContent clonedPageQuestionItemContent = ClonePageQuestionItemContent(questionItem.PageQuestionItemContent);
                    clonedPageQuestionItemContent = SetDefaultGuidForPageQuestionItemContent(clonedPageQuestionItemContent, cloneParameterModel);

                    cloneQuestionItem.PageQuestionItemContent = clonedPageQuestionItemContent;
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

        private PageQuestionItemContent ClonePageQuestionItemContent(PageQuestionItemContent pageQuestionItemContent)
        {
            try
            {
                PageQuestionItemContent clonePageQuestionItemContent = new PageQuestionItemContent();
                clonePageQuestionItemContent.ParentPageQuestionItemContentGUID = pageQuestionItemContent.PageQuestionItemGUID;
                clonePageQuestionItemContent.DefaultGUID = pageQuestionItemContent.DefaultGUID;
                clonePageQuestionItemContent.Feedback = pageQuestionItemContent.Feedback;
                clonePageQuestionItemContent.Item = pageQuestionItemContent.Item;

                return clonePageQuestionItemContent;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private PageMedia ClonePageMedia(PageMedia pageMedia)
        {
            try
            {
                PageMedia clonePageMedia = new PageMedia();
                if (!pageMedia.ResourceReference.IsLoaded)
                {
                    pageMedia.ResourceReference.Load();
                }
                clonePageMedia.Resource = pageMedia.Resource;
                clonePageMedia.Type = pageMedia.Type;
                clonePageMedia.ParentPageMediaItemGUID = pageMedia.PageGUID;
                clonePageMedia.DefaultGUID = pageMedia.DefaultGUID;
                clonePageMedia.IsDeleted = pageMedia.IsDeleted;

                return clonePageMedia;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private Preferences ClonePreferences(Preferences preferences)
        {
            try
            {
                Preferences clonePreferences = new Preferences();

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
                clonePreferences.DefaultGUID = preferences.DefaultGUID;
                clonePreferences.IsDeleted = preferences.IsDeleted;

                return clonePreferences;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private Graph CloneGraph(Graph graph, CloneProgramParameterModel cloneParameterModel)
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
                cloneGraph.DefaultGUID = graph.DefaultGUID;
                cloneGraph.IsDeleted = graph.IsDeleted;

                if (!graph.GraphContentReference.IsLoaded)
                {
                    graph.GraphContentReference.Load();
                }
                //List<GraphContent> graphContents = graph.GraphContent.Where(g => g.IsDeleted != true).ToList();
                if (graph.GraphContent != null)
                {
                    GraphContent clonedGraphContent = CloneGraphContent(graph.GraphContent);
                    clonedGraphContent = SetDefaultGuidForGraphContent(clonedGraphContent, cloneParameterModel);

                    cloneGraph.GraphContent = clonedGraphContent;
                }
                if (!graph.GraphItem.IsLoaded)
                {
                    graph.GraphItem.Load();
                }

                List<GraphItem> graphItems = graph.GraphItem.Where(g => g.IsDeleted != true).ToList();
                foreach (GraphItem item in graphItems)
                {
                    GraphItem clonedGraphItem = CloneGraphItem(item, cloneParameterModel);
                    clonedGraphItem = SetDefaultGuidForGraphItem(clonedGraphItem, cloneParameterModel);

                    cloneGraph.GraphItem.Add(clonedGraphItem);
                }

                return cloneGraph;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private GraphContent CloneGraphContent(GraphContent graphContent)
        {
            try
            {
                GraphContent cloneGraphContent = new GraphContent
                {
                    ParentGraphContentGUID = graphContent.GraphGUID,
                    DefaultGUID = graphContent.DefaultGUID,
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

        private GraphItem CloneGraphItem(GraphItem item, CloneProgramParameterModel cloneParameterModel)
        {
            try
            {
                GraphItem cloneGraphItem = new GraphItem();
                cloneGraphItem.GraphItemGUID = Guid.NewGuid();
                cloneGraphItem.Name = item.Name;
                cloneGraphItem.PointType = item.PointType;
                cloneGraphItem.DataItemExpression = item.DataItemExpression;
                cloneGraphItem.Color = item.Color;
                cloneGraphItem.ParentGraphItemGUID = item.GraphItemGUID;
                cloneGraphItem.DefaultGUID = item.DefaultGUID;
                if (!item.GraphItemContentReference.IsLoaded)
                {
                    item.GraphItemContentReference.Load();
                }

                GraphItemContent clonedGraphItemContent = CloneGraphItemContent(item.GraphItemContent);
                clonedGraphItemContent = SetDefaultGuidForGraphItemContent(clonedGraphItemContent, cloneParameterModel);

                cloneGraphItem.GraphItemContent = clonedGraphItemContent;
                return cloneGraphItem;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private GraphItemContent CloneGraphItemContent(GraphItemContent itemcontent)
        {
            try
            {
                GraphItemContent cloneGraphItemContent = new GraphItemContent
                {
                    ParentGraphItemContentGUID = itemcontent.GraphItemGUID,
                    DefaultGUID = itemcontent.DefaultGUID,
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

        public string GetPageGraphData(Guid pageGuid)
        {
            return Resolve<IStoreProcedure>().GetPageGraphAsXML(pageGuid);
        }

        #endregion

        #region Private methods
        private List<ProgramLanguageModel> GetProgramLanguages(Guid programGuid)
        {
            List<ProgramLanguageModel> programLanguages = new List<ProgramLanguageModel>();
            List<ProgramLanguage> programLanguageEntities = Resolve<IProgramLanguageRepository>().GetLanguagesOfProgram(programGuid).ToList();
            foreach(ProgramLanguage programLanguageEntity in programLanguageEntities)
            {
                if(!programLanguageEntity.LanguageReference.IsLoaded)
                {
                    programLanguageEntity.LanguageReference.Load();
                }

                programLanguages.Add(new ProgramLanguageModel
                {
                    language = new LanguageModel { LanguageGUID = programLanguageEntity.Language.LanguageGUID, Name = programLanguageEntity.Language.Name },
                });
            }
            return programLanguages;
        }

        private void SaveSMSPageContent(SMSTemplatePageContentModel smsPage, Guid smsPageGuid)
        {
            try
            {
                PageContent pageContent = new PageContent();
                pageContent.Heading = "";
                pageContent.SendTime = smsPage.Time;
                pageContent.Body = smsPage.Text;
                pageContent.DaysToSend = smsPage.DaysToSend;
                pageContent.Page = Resolve<IPageRepository>().GetPageByPageGuid(smsPageGuid);
                Resolve<IPageContentRepository>().Add(pageContent);
            }
            catch(Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("{0}:{1}", MethodBase.GetCurrentMethod().Name, smsPageGuid));
                throw ex;
            }
        }

        private void SaveGraphPageContent(GraphTemplatePageContentModel graphPage, Guid graphPageGuid)
        {
            try
            {
                Program program = GetProgram(graphPage.ProgramGUID);

                PageContent pageContent = new PageContent();
                pageContent.Heading = graphPage.Heading;
                pageContent.Body = graphPage.Body;
                pageContent.PrimaryButtonCaption = graphPage.PrimaryButtonCaption;
                pageContent.Page = Resolve<IPageRepository>().GetPageByPageGuid(graphPageGuid);

                Resolve<IPageContentRepository>().Add(pageContent);
                SaveGraph(graphPage, graphPageGuid, program);
            }
            catch(Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("{0}:{1}", MethodBase.GetCurrentMethod().Name, graphPageGuid));
                throw ex;
            }
        }

        private List<QuestionModel> GetQuestionsModel()
        {
            List<QuestionModel> questionsModel = new List<QuestionModel>();
            List<Question> questions = Resolve<IQuestionRepository>().GetQuestions().OrderBy(q => q.Name).ToList();
            foreach(Question question in questions)
            {
                QuestionModel qm = new QuestionModel();
                qm.Guid = question.QuestionGUID;
                qm.Name = question.Name;
                qm.HasSubItem = question.HasSubItem;
                questionsModel.Add(qm);
            }
            return questionsModel;
        }

        private void UpdateResourceCatgeoryLastAccessTime(Resource resource)
        {
            if(resource != null)
            {
                if(!resource.ResourceCategoryReference.IsLoaded)
                {
                    resource.ResourceCategoryReference.Load();
                }

                User operater = Resolve<IUserRepository>().GetUserByGuid(Resolve<IUserService>().GetCurrentUser().UserGuid);
                operater.LastSelectedResource = resource.ResourceGUID;
                operater.LastSelectedResourceCategory = resource.ResourceCategory.ResourceCategoryGUID;
                operater.LastSelectedResourceType = resource.Type;
                Resolve<IUserRepository>().UpdateUser(operater);

                resource.ResourceCategory.LastAccessed = DateTime.UtcNow;
                resource.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
                resource.LastUpdated = DateTime.UtcNow;
                Resolve<IResourceCategoryRepository>().UpdateResourceCategory(resource.ResourceCategory);
            }
        }

        public void UpdatePageVariableLastAccessTime(Guid pageVariableGUID)
        {
            UpdatePageVariableLastAccessTime(Resolve<IPageVaribleRepository>().GetItem(pageVariableGUID));
        }
        
        private void UpdatePageVariableLastAccessTime(ChangeTech.Entities.PageVariable pageVariable)
        {
            if (pageVariable != null)
            {
                if (!pageVariable.PageVariableGroupReference.IsLoaded)
                {
                    pageVariable.PageVariableGroupReference.Load();
                }

                User operater = Resolve<IUserRepository>().GetUserByGuid(Resolve<IUserService>().GetCurrentUser().UserGuid);
                operater.LastSelectedPageVariable = pageVariable.PageVariableGUID;
                if (pageVariable.PageVariableGroup == null)
                    operater.LastSelectedPageVariableGroup = null;
                else
                    operater.LastSelectedPageVariableGroup = pageVariable.PageVariableGroup.PageVariableGroupGUID;
                Resolve<IUserRepository>().UpdateUser(operater);
            }
        }

        private void SaveGraph(GraphTemplatePageContentModel graphPage, Guid graphPageGuid, Program program)
        {
            try
            {
                Graph graph = new Graph();
                graph.Page = Resolve<IPageRepository>().GetPageByPageGuid(graphPageGuid);
                graph.GraphGUID = Guid.NewGuid();
                graph.Type = graphPage.GraphType;
                //graph.Caption = graphPage.GraphCaption;
                graph.ScoreRange = graphPage.ScoreRange;
                graph.GoodScoreRange = graphPage.GoodScoreRange;
                graph.MediumRange = graphPage.MediumScoreRange;
                graph.BadScoreRange = graphPage.BadScoreRange;
                graph.TimeUnit = graphPage.TimeUnit;
                graph.TimeRange = graphPage.TimeRange;
                //foreach (GraphItemModel gitem in graphPage.GraphItem)
                //{
                //    graph.GraphItem.Add(new GraphItem { GraphItemGUID = Guid.NewGuid(), Color = gitem.Color, DataItemExpression = gitem.Expression, PointType = gitem.PointType });
                //}

                // save graph content
                //foreach (Language language in program.Language1)
                //{
                GraphContent graphcontent = new GraphContent()
                {
                    Caption = graphPage.GraphCaption,
                    //Language = Resolve<ILanguageRepository>().GetLanguage(graphPage.LanguageGUID),
                    Graph = Resolve<IGraphRepository>().Get(graph.GraphGUID),
                    TimeUnit = graph.TimeUnit
                };
                graph.GraphContent = graphcontent;
                //}
                Resolve<IGraphRepository>().Instert(graph);

                // save graph item
                SaveGraphItem(graphPage.GraphItem, graph.GraphGUID);
            }
            catch(Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method name:{0},GraphType {1}, Type {2}, Caption {3}, ScoreRange {4}, GoodScoreRange {5}, MediumScoreRange {6}, BadScoreRange {7}, TimeUnit {8}, TimeRange {9}",
                    MethodBase.GetCurrentMethod().Name, graphPageGuid, graphPage.GraphType, graphPage.GraphCaption, graphPage.ScoreRange, graphPage.GoodScoreRange,
                    graphPage.MediumScoreRange, graphPage.BadScoreRange, graphPage.TimeUnit, graphPage.TimeRange));
                throw ex;
            }
        }

        private void SaveGraphItem(List<GraphItemModel> list, Guid guid)
        {
            foreach(GraphItemModel itemModel in list)
            {
                GraphItem newGraphItem = new GraphItem()
                {
                    GraphItemGUID = Guid.NewGuid(),
                    Color = itemModel.Color,
                    DataItemExpression = itemModel.Expression,
                    PointType = itemModel.PointType,
                    Graph = Resolve<IGraphRepository>().Get(guid)
                };

                // save graph item content
                //foreach (Language language in languages)
                //{
                GraphItemContent graphItemContentEntity = new GraphItemContent()
                {
                    GraphItem = newGraphItem,
                    //Language = Resolve<ILanguageRepository>().GetLanguage(languageGuid),
                    Name = itemModel.Name
                };
                newGraphItem.GraphItemContent = graphItemContentEntity;
                //}

                Resolve<IGraphItemRepository>().Insert(newGraphItem);
            }
        }

        private void UpdateGraphPageContent(GraphTemplatePageContentModel graphPage, Guid pageGuid)
        {
            PageContent pagecontententity = Resolve<IPageContentRepository>().GetPageContentByPageGuid(pageGuid);
            pagecontententity.PrimaryButtonCaption = graphPage.PrimaryButtonCaption;
            pagecontententity.Heading = graphPage.Heading;
            pagecontententity.Body = graphPage.Body;
            pagecontententity.LastUpdated = DateTime.UtcNow;
            //pagecontententity.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid.ToString();
            Resolve<IPageContentRepository>().Update(pagecontententity);
            Resolve<ITranslationJobService>().UpdateElementWhenFromUpdated("PageContent", pagecontententity.PageGUID.ToString(), Guid.Empty);
            UpdateGraph(pageGuid, graphPage);
        }

        private void UpdateGraph(Guid pageGuid, GraphTemplatePageContentModel graphPage)
        {
            Graph updateGraph = Resolve<IGraphRepository>().GetGraphByPageGuid(pageGuid);
            updateGraph.BadScoreRange = graphPage.BadScoreRange;
            //updateGraph.Caption = graphPage.GraphCaption;
            updateGraph.MediumRange = graphPage.MediumScoreRange;
            updateGraph.GoodScoreRange = graphPage.GoodScoreRange;
            updateGraph.ScoreRange = graphPage.ScoreRange;
            updateGraph.TimeUnit = graphPage.TimeUnit;
            updateGraph.TimeRange = graphPage.TimeRange;
            updateGraph.Type = graphPage.GraphType;
            Resolve<IGraphRepository>().Update(updateGraph);

            GraphContent updateGraphContent = Resolve<IGraphContentRepository>().Get(updateGraph.GraphGUID);
            updateGraphContent.Caption = graphPage.GraphCaption;
            //TODO: temp need update 
            updateGraphContent.TimeUnit = graphPage.TimeUnit;
            Resolve<IGraphContentRepository>().Update(updateGraphContent);
            Resolve<ITranslationJobService>().UpdateElementWhenFromUpdated("GraphContent", updateGraphContent.GraphGUID.ToString(), Guid.Empty);

            // update graph item
            foreach(GraphItemModel itemModel in graphPage.GraphItem)
            {
                if(graphPage.ObjectStatus.ContainsKey(itemModel.GraphItemModelGUID))
                {
                    switch(graphPage.ObjectStatus[itemModel.GraphItemModelGUID])
                    {
                        case ModelStatus.GraphItemUpdated:
                            GraphItem gitem = Resolve<IGraphItemRepository>().get(itemModel.GraphItemModelGUID);
                            //gitem.Name = itemModel.Name;
                            gitem.PointType = itemModel.PointType;
                            gitem.DataItemExpression = itemModel.Expression;
                            gitem.Color = itemModel.Color;
                            Resolve<IGraphItemRepository>().Update(gitem);
                            // update graph item content
                            GraphItemContent gitemContent = Resolve<IGraphItemContentRepository>().Get(itemModel.GraphItemModelGUID);
                            gitemContent.Name = itemModel.Name;
                            Resolve<IGraphItemContentRepository>().Update(gitemContent);
                            Resolve<ITranslationJobService>().UpdateElementWhenFromUpdated("GraphItemContent", gitemContent.GraphItemGUID.ToString(), Guid.Empty);
                            break;
                        case ModelStatus.GraphItemAdded:
                            GraphItem item = new GraphItem
                            {
                                GraphItemGUID = itemModel.GraphItemModelGUID,
                                //Name = itemModel.Name,
                                PointType = itemModel.PointType,
                                DataItemExpression = itemModel.Expression,
                                Color = itemModel.Color,
                            };
                            item.Graph = Resolve<IGraphRepository>().Get(updateGraph.GraphGUID);
                            GraphItemContent itemContent = new GraphItemContent()
                            {
                                GraphItem = item,
                                //Language = Resolve<ILanguageRepository>().GetLanguage(graphPage.LanguageGUID),
                                Name = itemModel.Name
                            };
                            item.GraphItemContent = itemContent;
                            Resolve<IGraphItemRepository>().Insert(item);
                            break;
                    }
                }
            }

            // delete graph item
            foreach(KeyValuePair<Guid, ModelStatus> keyStatus in graphPage.ObjectStatus)
            {
                if(keyStatus.Value == ModelStatus.GraphItemDeleted)
                {
                    GraphItem item = Resolve<IGraphItemRepository>().get(keyStatus.Key);
                    Resolve<IGraphItemContentRepository>().Delete(item.GraphItemGUID);
                    Resolve<IGraphItemRepository>().Delete(keyStatus.Key);
                }
            }
        }

        public Guid SavePage(SimplePageContentModel simplePageContentModel) {
            Guid newPageGuid = InsertPage(simplePageContentModel.PageSequenceGUID, simplePageContentModel.TemplateGUID, simplePageContentModel.Order, "0", 0, Guid.Empty);
            SavePage(simplePageContentModel, newPageGuid);
            return newPageGuid;
        }

        private void SavePage(SimplePageContentModel simplePageContentModel, Guid pageGuid)
        {
            PageContent pageContent = new PageContent();
            pageContent.Heading = simplePageContentModel.Heading;
            pageContent.Body = simplePageContentModel.Body;
            pageContent.PrimaryButtonCaption = simplePageContentModel.PrimaryButtonCaption;
            pageContent.Page = Resolve<IPageRepository>().GetPageByPageGuid(pageGuid);

            Resolve<IPageContentRepository>().Add(pageContent);
        }

        private Guid InsertPage(Guid pageSequenceGuid, Guid templateGuid, int pageOrder, string wait, int maxPreferences, Guid pageVariableGUID)
        {
            Page insertPage = new Page();

            insertPage.PageGUID = Guid.NewGuid();
            PageSequence sequence = Resolve<IPageSequenceRepository>().GetPageSequenceByGuid(pageSequenceGuid);
            if(!sequence.Page.IsLoaded)
            {
                sequence.Page.Load();
            }
            foreach(Page page in sequence.Page)
            {
                if(page.PageOrderNo >= pageOrder)
                {
                    page.PageOrderNo++;
                }
            }

            insertPage.PageOrderNo = pageOrder;
            insertPage.Created = DateTime.UtcNow;
            insertPage.LastUpdated = DateTime.UtcNow;
            insertPage.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
            insertPage.PageTemplate = Resolve<IPageThemplateRepository>().Get(templateGuid);
            if(pageVariableGUID != Guid.Empty)
            {
                insertPage.PageVariable = Resolve<IPageVaribleRepository>().GetItem(pageVariableGUID);
            }
            insertPage.Wait = wait;
            insertPage.MaxPreferences = maxPreferences;
            insertPage.IsDeleted = false;
            sequence.Page.Add(insertPage);
            Resolve<IPageSequenceRepository>().UpdatePageSequence(sequence);
            return insertPage.PageGUID;
        }

        
        //private void SaveStandardPageDownloadResource(StandardTemplatePageContentModel standardPage, Guid pageGuid)
        //{
        //    //save download resource from the Body
        //    string body = standardPage.Body;
        //    while (body.IndexOf(LINK_A_START) > -1 && body.IndexOf(LINK_A_END) > -1 && body.IndexOf(LINK_A_END) + LINK_A_END.Length - body.IndexOf(LINK_A_START) > 0)
        //    {
        //        string link = body.Substring(body.IndexOf(LINK_A_START), body.IndexOf(LINK_A_END) + LINK_A_END.Length - body.IndexOf(LINK_A_START));
        //        string resourceType = "";
        //        if (link.IndexOf(REQUEST_RESOURCE) > -1 && link.IndexOf(MEDIA) > -1 && link.IndexOf(MEDIA) - link.IndexOf(REQUEST_RESOURCE) - REQUEST_RESOURCE.Length > -1)
        //        {
        //            resourceType = link.Substring(link.IndexOf(REQUEST_RESOURCE) + REQUEST_RESOURCE.Length, link.IndexOf(MEDIA) - link.IndexOf(REQUEST_RESOURCE) - REQUEST_RESOURCE.Length);
        //        }
        //        else
        //        {
        //            if (link.ToLower().IndexOf(BlobContainerTypeEnum.AudioContainer.ToString().ToLower()) > -1)
        //            {
        //                resourceType = ResourceTypeEnum.Audio.ToString();
        //            }
        //            else if (link.ToLower().IndexOf(BlobContainerTypeEnum.DocumentContainer.ToString().ToLower()) > -1)
        //            {
        //                resourceType = ResourceTypeEnum.Document.ToString();
        //            }
        //            else if (link.ToLower().IndexOf(BlobContainerTypeEnum.OriginalImageContainer.ToString().ToLower()) > -1)
        //            {
        //                resourceType = ResourceTypeEnum.Image.ToString();
        //            }
        //            else if (link.ToLower().IndexOf(BlobContainerTypeEnum.VideoContainer.ToString().ToLower()) > -1)
        //            {
        //                resourceType = ResourceTypeEnum.Video.ToString();
        //            }
        //            else//error file//need to be deleted when publish
        //            {
        //                //link = "<a href='###'>Error File</a>";
        //                resourceType = ResourceTypeEnum.Image.ToString();
        //            }
        //        }
        //        PageResource newPageDownloadEntity = new PageResource();
        //        newPageDownloadEntity.PageResourceGUID = Guid.NewGuid();
        //        newPageDownloadEntity.MediaLinkATag = link;
        //        newPageDownloadEntity.MediaType = resourceType;
        //        newPageDownloadEntity.PageGUID = pageGuid;
        //        newPageDownloadEntity.PageSequenceGUID = standardPage.PageSequenceGUID;
        //        newPageDownloadEntity.SessionGUID = standardPage.SessionGUID;
        //        newPageDownloadEntity.ResourceTypeOrigin = 1;
        //        newPageDownloadEntity.MediaLinkATagOriginal = link;
        //        // newPageDownloadEntity might not have resourceGuid.

        //        Resolve<IPageResourceRepository>().AddPageResource(newPageDownloadEntity);

        //        body = body.Substring(body.IndexOf(LINK_A_END) + LINK_A_END.Length);
        //    }

        //    //save download resource from the set up at the bottom
        //    //TODO: add Illustration type PageResource
        //    string serverPath = Resolve<ISystemSettingService>().GetSettingValue("WebServerPath");
        //    if (standardPage.VideoGUID != Guid.Empty)
        //    {
        //        Resource videoResource = Resolve<IResourceRepository>().GetResource(standardPage.VideoGUID);
        //        string resourceURL = string.Empty;
        //        string fordownloadresourceURL = serverPath + "RequestResource.aspx?target={0}&media={1}&name={2}";
        //        string downloadLink = LINK_A_START + " href='{0}' target='_blank'>{1}" + LINK_A_END;
        //        resourceURL = string.Format(fordownloadresourceURL, ResourceTypeEnum.Video, videoResource.NameOnServer, videoResource.Name);
        //        string link = string.Format(downloadLink, resourceURL, videoResource.Name);

        //        PageResource setDownloadResourceEntity = new PageResource();
        //        setDownloadResourceEntity.PageResourceGUID = Guid.NewGuid();
        //        setDownloadResourceEntity.ResourceTypeOrigin = 2;
        //        setDownloadResourceEntity.MediaLinkATag = link;
        //        setDownloadResourceEntity.MediaType = ResourceTypeEnum.Video.ToString();
        //        setDownloadResourceEntity.PageGUID = pageGuid;
        //        setDownloadResourceEntity.PageSequenceGUID = standardPage.PageSequenceGUID;
        //        setDownloadResourceEntity.SessionGUID = standardPage.SessionGUID;
        //        setDownloadResourceEntity.ResourceGUID = videoResource.ResourceGUID;

        //        Resolve<IPageResourceRepository>().AddPageResource(setDownloadResourceEntity);
        //    }

        //    if (standardPage.RadioGUID != Guid.Empty)
        //    {
        //        Resource radioResource = Resolve<IResourceRepository>().GetResource(standardPage.RadioGUID);
        //        string resourceURL = string.Empty;
        //        string fordownloadresourceURL = serverPath + "RequestResource.aspx?target={0}&media={1}&name={2}";
        //        string downloadLink = LINK_A_START + " href='{0}' target='_blank'>{1}" + LINK_A_END;
        //        resourceURL = string.Format(fordownloadresourceURL, ResourceTypeEnum.Audio, radioResource.NameOnServer, radioResource.Name);
        //        string link = string.Format(downloadLink, resourceURL, radioResource.Name);

        //        PageResource setDownloadResourceEntity = new PageResource();
        //        setDownloadResourceEntity.PageResourceGUID = Guid.NewGuid();
        //        setDownloadResourceEntity.ResourceTypeOrigin = 3;
        //        setDownloadResourceEntity.MediaLinkATag = link;
        //        setDownloadResourceEntity.MediaType = ResourceTypeEnum.Audio.ToString();
        //        setDownloadResourceEntity.PageGUID = pageGuid;
        //        setDownloadResourceEntity.PageSequenceGUID = standardPage.PageSequenceGUID;
        //        setDownloadResourceEntity.SessionGUID = standardPage.SessionGUID;
        //        setDownloadResourceEntity.ResourceGUID = radioResource.ResourceGUID;

        //        Resolve<IPageResourceRepository>().AddPageResource(setDownloadResourceEntity);
        //    }
        //}

        public Dictionary<string, string> GetResourcesBySessionGuid(Guid sessionGuid)
        {
            Dictionary<string, string> resourceLinks = new Dictionary<string, string>();
            try
            {
                List<string> pageContentBodyList = Resolve<IStoreProcedure>().GetPageContentBySessionGUID(sessionGuid);
                foreach (string pageContentEntity in pageContentBodyList)
                {
                    string body = pageContentEntity;
                    while (body.IndexOf(LINK_A_START) > -1 && body.IndexOf(LINK_A_END) > -1 && body.IndexOf(LINK_A_END) + LINK_A_END.Length - body.IndexOf(LINK_A_START) > 0)
                    {
                        string link = body.Substring(body.IndexOf(LINK_A_START), body.IndexOf(LINK_A_END) + LINK_A_END.Length - body.IndexOf(LINK_A_START));
                        string resourceType = "";
                        if (link.IndexOf(REQUEST_RESOURCE) > -1 && link.IndexOf(MEDIA) > -1 && link.IndexOf(MEDIA) - link.IndexOf(REQUEST_RESOURCE) - REQUEST_RESOURCE.Length > -1)
                        {
                            resourceType = link.Substring(link.IndexOf(REQUEST_RESOURCE) + REQUEST_RESOURCE.Length, link.IndexOf(MEDIA) - link.IndexOf(REQUEST_RESOURCE) - REQUEST_RESOURCE.Length);
                        }
                        else
                        {
                            if (link.ToLower().IndexOf(BlobContainerTypeEnum.AudioContainer.ToString().ToLower()) > -1)
                            {
                                resourceType = ResourceTypeEnum.Audio.ToString();
                            }
                            else if (link.ToLower().IndexOf(BlobContainerTypeEnum.DocumentContainer.ToString().ToLower()) > -1)
                            {
                                resourceType = ResourceTypeEnum.Document.ToString();
                            }
                            else if (link.ToLower().IndexOf(BlobContainerTypeEnum.OriginalImageContainer.ToString().ToLower()) > -1)
                            {
                                resourceType = ResourceTypeEnum.Image.ToString();
                            }
                            else if (link.ToLower().IndexOf(BlobContainerTypeEnum.VideoContainer.ToString().ToLower()) > -1)
                            {
                                resourceType = ResourceTypeEnum.Video.ToString();
                            }
                            else//error file//need to be deleted when publish
                            {
                                //link = "<a href='###'>Error File</a>";
                                resourceType = ResourceTypeEnum.Image.ToString();
                            }
                        }
                        if (!resourceLinks.Keys.Contains(link) && !resourceLinks.Values.Contains(resourceType))
                        {
                            resourceLinks.Add(link, resourceType);
                        }
                        body = body.Substring(body.IndexOf(LINK_A_END) + LINK_A_END.Length);
                    }
                }

                string serverPath = Resolve<ISystemSettingService>().GetSettingValue("WebServerPath");
                List<PageMedia> downLoadMediaList = Resolve<IPageMediaRepository>().GetPageMediasBySessionGuid(sessionGuid).ToList();
                foreach (PageMedia pageMedia in downLoadMediaList)
                {
                    if (!pageMedia.ResourceReference.IsLoaded) pageMedia.ResourceReference.Load();
                    StringBuilder resourceURL = new StringBuilder(1000);
                    string downloadLink = LINK_A_START + " href='{0}' target='_blank'>{1}" + LINK_A_END;
                    string resourceType = pageMedia.Resource.Type;
                    resourceURL = resourceURL.Append(serverPath).AppendFormat("RequestResource.aspx?target={0}&media={1}&name={2}", pageMedia.Resource.Type, pageMedia.Resource.NameOnServer, pageMedia.Resource.Name);
                    string link = string.Format(downloadLink, resourceURL.ToString(), pageMedia.Resource.Name);
                    if (!resourceLinks.Keys.Contains(link) && !resourceLinks.Values.Contains(resourceType))
                    {
                        resourceLinks.Add(link, resourceType);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return resourceLinks;
        }

        #region the services provider for ctpp
        public Dictionary<string, string> GetResourcesBySessionGuid(Guid sessionGuid, string serverPath, List<CTPPSessionPageBodyModel> sPageBodyList, List<CTPPSessionPageMediaResourceModel> sPageMediaResourceList)
        {
            Dictionary<string, string> resourceLinks = new Dictionary<string, string>();
            try
            {
                List<CTPPSessionPageBodyModel> pageBodyList = sPageBodyList.Where(t => t.SessionGUID == sessionGuid).ToList();
                foreach (CTPPSessionPageBodyModel pageBody in pageBodyList)
                {
                    string body = pageBody.PageBody;
                    while (body.IndexOf(LINK_A_START) > -1 && body.IndexOf(LINK_A_END) > -1 && body.IndexOf(LINK_A_END) + LINK_A_END.Length - body.IndexOf(LINK_A_START) > 0)
                    {
                        string link = body.Substring(body.IndexOf(LINK_A_START), body.IndexOf(LINK_A_END) + LINK_A_END.Length - body.IndexOf(LINK_A_START));
                        string resourceType = "";
                        if (link.IndexOf(REQUEST_RESOURCE) > -1 && link.IndexOf(MEDIA) > -1 && link.IndexOf(MEDIA) - link.IndexOf(REQUEST_RESOURCE) - REQUEST_RESOURCE.Length > -1)
                        {
                            resourceType = link.Substring(link.IndexOf(REQUEST_RESOURCE) + REQUEST_RESOURCE.Length, link.IndexOf(MEDIA) - link.IndexOf(REQUEST_RESOURCE) - REQUEST_RESOURCE.Length);
                        }
                        else
                        {
                            if (link.ToLower().IndexOf(BlobContainerTypeEnum.AudioContainer.ToString().ToLower()) > -1)
                            {
                                resourceType = ResourceTypeEnum.Audio.ToString();
                            }
                            else if (link.ToLower().IndexOf(BlobContainerTypeEnum.DocumentContainer.ToString().ToLower()) > -1)
                            {
                                resourceType = ResourceTypeEnum.Document.ToString();
                            }
                            else if (link.ToLower().IndexOf(BlobContainerTypeEnum.OriginalImageContainer.ToString().ToLower()) > -1)
                            {
                                resourceType = ResourceTypeEnum.Image.ToString();
                            }
                            else if (link.ToLower().IndexOf(BlobContainerTypeEnum.VideoContainer.ToString().ToLower()) > -1)
                            {
                                resourceType = ResourceTypeEnum.Video.ToString();
                            }
                            else//error file//need to be deleted when publish
                            {
                                //link = "<a href='###'>Error File</a>";
                                resourceType = ResourceTypeEnum.Image.ToString();
                            }
                        }
                        if (!resourceLinks.Keys.Contains(link) && !resourceLinks.Values.Contains(resourceType))
                        {
                            resourceLinks.Add(link, resourceType);
                        }
                        body = body.Substring(body.IndexOf(LINK_A_END) + LINK_A_END.Length);
                    }
                }

                // Resolve<ISystemSettingService>().GetSettingValue("WebServerPath");
                List<CTPPSessionPageMediaResourceModel> pageMediaResourceList = sPageMediaResourceList.Where(t => t.SessionGUID == sessionGuid).ToList();
                foreach (CTPPSessionPageMediaResourceModel pageMediaResource in pageMediaResourceList)
                {
                    StringBuilder resourceURL = new StringBuilder(1000);
                    string downloadLink = LINK_A_START + " href='{0}' target='_blank'>{1}" + LINK_A_END;
                    string resourceType = pageMediaResource.Type;
                    resourceURL = resourceURL.Append(serverPath).AppendFormat("RequestResource.aspx?target={0}&media={1}&name={2}", pageMediaResource.Type, pageMediaResource.NameOnServer, pageMediaResource.Name);
                    string link = string.Format(downloadLink, resourceURL.ToString(), pageMediaResource.Name);
                    if (!resourceLinks.Keys.Contains(link) && !resourceLinks.Values.Contains(resourceType))
                    {
                        resourceLinks.Add(link, resourceType);
                    }
                }

            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return resourceLinks;
        }
        #endregion

        private Program GetProgram(Guid ProgramGuid)
        {
            Program program = Resolve<IProgramRepository>().GetProgramByGuid(ProgramGuid);
            if(!program.ProgramLanguage.IsLoaded)
            {
                program.ProgramLanguage.Load();
            }
            return program;
        }

        public Guid SaveChangedToScreenResultsTemplatePage(Guid oldPageGuid, ScreenResultTemplatePageContentModel newpage)
        {
            newpage.PageOrder = GetPageOrderNumber(oldPageGuid);
            newpage.LastUpdateBy = new UserModel { UserName = Resolve<IUserService>().GetCurrentUser().UserName };
            DeletePage(oldPageGuid);
            return SaveScreenResultsTemplatePage(newpage);
        }

        public Guid SaveScreenResultsTemplatePage(ScreenResultTemplatePageContentModel newScreenResultsPage)
        {
            Guid newPageGuid = InsertPage(newScreenResultsPage.PageSequenceGUID, newScreenResultsPage.TemplateGUID, newScreenResultsPage.PageOrder, "0", 0, Guid.Empty);
            SaveScreenResultsPage(newScreenResultsPage, newPageGuid);
            return newPageGuid;
        }

        private void SaveScreenResultsPage(ScreenResultTemplatePageContentModel newScreenResultsPage, Guid newPageGuid)
        {
            Program program = GetProgram(newScreenResultsPage.ProgramGuid);

            PageContent pageContent = new PageContent();
            pageContent.Heading = newScreenResultsPage.Heading;
            pageContent.Body = newScreenResultsPage.Body;
            pageContent.PrimaryButtonCaption = newScreenResultsPage.PrimaryButtonCaption;
            pageContent.PrimaryButtonActionParameter = "0";
            pageContent.AfterShowExpression = ReplacePageNOWithPageGUIDInExpression(newScreenResultsPage.AfterExpression, newScreenResultsPage.SessionGUID);
            pageContent.BeforeShowExpression = ReplacePageNOWithPageGUIDInExpression(newScreenResultsPage.BeforeExpression, newScreenResultsPage.SessionGUID);
            pageContent.Page = Resolve<IPageRepository>().GetPageByPageGuid(newPageGuid);
            //PageGraphicImage
            if (newScreenResultsPage.PageGraphic1GUID != Guid.Empty )
            {
                Resource pageGraphic1Resource=Resolve<IResourceRepository>().GetResource(newScreenResultsPage.PageGraphic1GUID);
                pageContent.PageGraphic1GUID = pageGraphic1Resource.ResourceGUID;
                pageContent.Resource_PageGraphic1 = pageGraphic1Resource;
            }
            if (newScreenResultsPage.PageGraphic2GUID != Guid.Empty)
            {
                Resource pageGraphic2Resource = Resolve<IResourceRepository>().GetResource(newScreenResultsPage.PageGraphic2GUID);
                pageContent.PageGraphic2GUID = pageGraphic2Resource.ResourceGUID;
                pageContent.Resource_PageGraphic2 = pageGraphic2Resource;
            }
            if (newScreenResultsPage.PageGraphic3GUID != Guid.Empty)
            {
                Resource pageGraphic3Resource = Resolve<IResourceRepository>().GetResource(newScreenResultsPage.PageGraphic3GUID);
                pageContent.PageGraphic3GUID = pageGraphic3Resource.ResourceGUID;
                pageContent.Resource_PageGraphic3 = pageGraphic3Resource;
            }
            //PresenterImage
            if (newScreenResultsPage.PresenterImageGUID != Guid.Empty)
            {
                Resource presenterResource = Resolve<IResourceRepository>().GetResource(newScreenResultsPage.PresenterImageGUID);
                pageContent.Resource_PresenterImage = presenterResource;
                pageContent.ImageMode = "Preseter";
                pageContent.PresenterImagePosition = newScreenResultsPage.PresenterImagePosition;
                pageContent.PresenterMode = newScreenResultsPage.PresenterMode;
                //UpdateResourceCatgeoryLastAccessTime(presenterResource);
            }

            Resolve<IPageContentRepository>().Add(pageContent);
            if (newScreenResultsPage.PresenterImageGUID != Guid.Empty)
            {
                UpdateResourceCatgeoryLastAccessTime(Resolve<IResourceRepository>().GetResource(newScreenResultsPage.PresenterImageGUID));
            }

            SavePageLines(newScreenResultsPage, newPageGuid, program);
        }

        private void SaveStandardPage(StandardTemplatePageContentModel standardPage, Guid pageGuid)
        {
            Program program = GetProgram(standardPage.ProgramGuid);

            PageContent pageContent = new PageContent();
            pageContent.Heading = standardPage.Heading;
            pageContent.Body = standardPage.Body;
            pageContent.PrimaryButtonCaption = standardPage.PrimaryButtonCaption;
            pageContent.PrimaryButtonActionParameter = standardPage.PrimaryButtonAction;
            pageContent.BeforeShowExpression = ReplacePageNOWithPageGUIDInExpression(standardPage.BeforeExpression, standardPage.SessionGUID);
            pageContent.AfterShowExpression = ReplacePageNOWithPageGUIDInExpression(standardPage.AfterExpression, standardPage.SessionGUID);
            pageContent.Page = Resolve<IPageRepository>().GetPageByPageGuid(pageGuid);


            switch (standardPage.ImageMode)
            {
                case ImageModeEnum.PresenterMode:
                    if (standardPage.PresenterImageGUID != Guid.Empty)
                    {
                        Resource presenterResource = Resolve<IResourceRepository>().GetResource(standardPage.PresenterImageGUID);
                        pageContent.Resource_PresenterImage = presenterResource;
                        pageContent.ImageMode = "Preseter";
                        pageContent.PresenterImagePosition = standardPage.PresenterImagePosition;
                        pageContent.PresenterMode = standardPage.PresenterMode;
                    }
                    pageContent.Resource_BackgroundImage = null;
                    pageContent.IllustrationImageGUID = null;
                    break;
                case ImageModeEnum.IllustrationMode:
                    if (standardPage.IllustrationImageGUID != Guid.Empty)
                    {
                        Resource IllustrationIResource = Resolve<IResourceRepository>().GetResource(standardPage.IllustrationImageGUID);
                        pageContent.IllustrationImageGUID = IllustrationIResource.ResourceGUID;
                        pageContent.ImageMode = "Illustration";
                    }
                    pageContent.Resource_PresenterImage = null;
                    pageContent.Resource_BackgroundImage = null;
                    break;
                case ImageModeEnum.FullscreenMode:
                    if (standardPage.BackgroundImageGUID != Guid.Empty)
                    {
                        Resource backgroundResource = Resolve<IResourceRepository>().GetResource(standardPage.BackgroundImageGUID);
                        pageContent.Resource_BackgroundImage = backgroundResource;
                        pageContent.ImageMode = "Fullscreen";
                    }
                    pageContent.Resource_PresenterImage = null;
                    pageContent.IllustrationImageGUID = null;
                    break;
                default:
                    break;
            }

            Resolve<IPageContentRepository>().Add(pageContent);

            if (standardPage.BackgroundImageGUID != Guid.Empty)
            {
                UpdateResourceCatgeoryLastAccessTime(Resolve<IResourceRepository>().GetResource(standardPage.BackgroundImageGUID));
            }
            if (standardPage.PresenterImageGUID != Guid.Empty)
            {
                UpdateResourceCatgeoryLastAccessTime(Resolve<IResourceRepository>().GetResource(standardPage.PresenterImageGUID));
            }
            if (standardPage.IllustrationImageGUID != Guid.Empty)
            {
                UpdateResourceCatgeoryLastAccessTime(Resolve<IResourceRepository>().GetResource(standardPage.IllustrationImageGUID));
            }

            #region IllustrationImage old follows
            if (standardPage.IllustrationImageGUID != Guid.Empty)
            {
                PageMedia pageMedia = Resolve<IPageMediaRepository>().GetPageMediaByPageGuid(pageGuid);
                Resource illustrationResource = Resolve<IResourceRepository>().GetResource(standardPage.IllustrationImageGUID);
                if (pageMedia == null)
                {
                    pageMedia = new PageMedia();
                    pageMedia.Page = Resolve<IPageRepository>().GetPageByPageGuid(pageGuid);
                    pageMedia.Resource = illustrationResource;
                    pageMedia.Type = "Illustration";
                    Resolve<IPageMediaRepository>().AddPageMedia(pageMedia);
                }
                else
                {
                    pageMedia.Page = Resolve<IPageRepository>().GetPageByPageGuid(pageGuid);
                    pageMedia.Resource = illustrationResource;
                    pageMedia.Type = "Illustration";
                    Resolve<IPageMediaRepository>().UpdatePageMedia(pageMedia);
                }
                UpdateResourceCatgeoryLastAccessTime(illustrationResource);
            }
            #endregion

            if (standardPage.VideoGUID != Guid.Empty)
            {
                PageMedia pageMedia = Resolve<IPageMediaRepository>().GetPageMediaByPageGuid(pageGuid);
                Resource videoResource = Resolve<IResourceRepository>().GetResource(standardPage.VideoGUID);
                if (pageMedia == null)
                {
                    pageMedia = new PageMedia();
                    pageMedia.Page = Resolve<IPageRepository>().GetPageByPageGuid(pageGuid);
                    pageMedia.Resource = videoResource;
                    pageMedia.Type = "Video";
                    Resolve<IPageMediaRepository>().AddPageMedia(pageMedia);
                }
                else
                {
                    pageMedia.Page = Resolve<IPageRepository>().GetPageByPageGuid(pageGuid);
                    pageMedia.Resource = videoResource;
                    pageMedia.Type = "Video";
                    Resolve<IPageMediaRepository>().UpdatePageMedia(pageMedia);
                }

                UpdateResourceCatgeoryLastAccessTime(videoResource);
            }

            if (standardPage.RadioGUID != Guid.Empty)
            {
                PageMedia pageMedia = Resolve<IPageMediaRepository>().GetPageMediaByPageGuid(pageGuid);
                Resource radioResource = Resolve<IResourceRepository>().GetResource(standardPage.RadioGUID);
                if (pageMedia == null)
                {
                    pageMedia = new PageMedia();
                    pageMedia.Page = Resolve<IPageRepository>().GetPageByPageGuid(pageGuid);
                    pageMedia.Resource = radioResource;
                    pageMedia.Type = "Audio";
                    Resolve<IPageMediaRepository>().AddPageMedia(pageMedia);
                }
                else
                {
                    pageMedia.Page = Resolve<IPageRepository>().GetPageByPageGuid(pageGuid);
                    pageMedia.Resource = radioResource;
                    pageMedia.Type = "Audio";
                    Resolve<IPageMediaRepository>().UpdatePageMedia(pageMedia);
                }

                UpdateResourceCatgeoryLastAccessTime(radioResource);
            }
        }

        private void SaveGetInfoPage(GetInfoTemplatePageContentModel newGetInfoPage, Guid newPageGuid)
        {
            Program program = GetProgram(newGetInfoPage.ProgramGuid);

            PageContent pageContent = new PageContent();
            pageContent.Heading = newGetInfoPage.Heading;
            pageContent.Body = newGetInfoPage.Body;
            pageContent.FooterText = newGetInfoPage.FooterText;
            pageContent.PrimaryButtonCaption = newGetInfoPage.PrimaryButtonCaption;
            pageContent.PrimaryButtonActionParameter = "0";
            pageContent.AfterShowExpression = ReplacePageNOWithPageGUIDInExpression(newGetInfoPage.AfterExpression, newGetInfoPage.SessionGUID);
            pageContent.BeforeShowExpression = ReplacePageNOWithPageGUIDInExpression(newGetInfoPage.BeforeExpression, newGetInfoPage.SessionGUID);
            pageContent.Page = Resolve<IPageRepository>().GetPageByPageGuid(newPageGuid);
            //if (newGetInfoPage.PresenterImageGUID != Guid.Empty)
            //{
            //    Resource presenterResource = Resolve<IResourceRepository>().GetResource(newGetInfoPage.PresenterImageGUID);
            //    pageContent.Resource1 = presenterResource;
            //    pageContent.PresenterImagePosition = newGetInfoPage.PresenterImagePosition;
            //    pageContent.PresenterMode = newGetInfoPage.PresenterMode;
            //    //UpdateResourceCatgeoryLastAccessTime(presenterResource);
            //}
            switch (newGetInfoPage.ImageMode)
            {
                case ImageModeEnum.PresenterMode:
                    if (newGetInfoPage.PresenterImageGUID != Guid.Empty)
                    {
                        Resource presenterResource = Resolve<IResourceRepository>().GetResource(newGetInfoPage.PresenterImageGUID);
                        pageContent.Resource_PresenterImage = presenterResource;
                        pageContent.ImageMode = "Preseter";
                        pageContent.PresenterImagePosition = newGetInfoPage.PresenterImagePosition;
                        pageContent.PresenterMode = newGetInfoPage.PresenterMode;
                    }
                    pageContent.Resource_BackgroundImage = null;
                    pageContent.IllustrationImageGUID = null;
                    break;
                case ImageModeEnum.IllustrationMode:
                    if (newGetInfoPage.IllustrationImageGUID != Guid.Empty)
                    {
                        Resource IllustrationIResource = Resolve<IResourceRepository>().GetResource(newGetInfoPage.IllustrationImageGUID);
                        pageContent.IllustrationImageGUID = IllustrationIResource.ResourceGUID;
                        pageContent.ImageMode = "Illustration";
                    }
                    pageContent.Resource_BackgroundImage = null;
                    pageContent.Resource_PresenterImage = null;
                    break;
                case ImageModeEnum.FullscreenMode:
                    if (newGetInfoPage.BackgroundImageGUID != Guid.Empty)
                    {
                        Resource backgroundResource = Resolve<IResourceRepository>().GetResource(newGetInfoPage.BackgroundImageGUID);
                        pageContent.Resource_BackgroundImage = backgroundResource;
                        pageContent.ImageMode = "Fullscreen";
                    }
                    pageContent.Resource_PresenterImage = null;
                    pageContent.IllustrationImageGUID = null;
                    break;
                default:
                    break;
            }
           
            Resolve<IPageContentRepository>().Add(pageContent);
            if(newGetInfoPage.PresenterImageGUID != Guid.Empty)
            {
                UpdateResourceCatgeoryLastAccessTime(Resolve<IResourceRepository>().GetResource(newGetInfoPage.PresenterImageGUID));
            }
            if (newGetInfoPage.PresenterImageGUID != Guid.Empty)
            {
                UpdateResourceCatgeoryLastAccessTime(Resolve<IResourceRepository>().GetResource(newGetInfoPage.PresenterImageGUID));
            }
            if (newGetInfoPage.IllustrationImageGUID != Guid.Empty)
            {
                UpdateResourceCatgeoryLastAccessTime(Resolve<IResourceRepository>().GetResource(newGetInfoPage.IllustrationImageGUID));
            }

            #region IllustrationImage  follows
            if (newGetInfoPage.IllustrationImageGUID != Guid.Empty)
            {
                PageMedia pageMedia = Resolve<IPageMediaRepository>().GetPageMediaByPageGuid(newPageGuid);
                Resource illustrationResource = Resolve<IResourceRepository>().GetResource(newGetInfoPage.IllustrationImageGUID);
                if (pageMedia == null)
                {
                    pageMedia = new PageMedia();
                    pageMedia.Page = Resolve<IPageRepository>().GetPageByPageGuid(newPageGuid);
                    pageMedia.Resource = illustrationResource;
                    pageMedia.Type = "Illustration";
                    Resolve<IPageMediaRepository>().AddPageMedia(pageMedia);
                }
                else
                {
                    pageMedia.Page = Resolve<IPageRepository>().GetPageByPageGuid(newPageGuid);
                    pageMedia.Resource = illustrationResource;
                    pageMedia.Type = "Illustration";
                    Resolve<IPageMediaRepository>().UpdatePageMedia(pageMedia);
                }
                UpdateResourceCatgeoryLastAccessTime(illustrationResource);
            }
            #endregion

            SavePageQuestions(newGetInfoPage, newPageGuid, program);
        }

        private void SaveAccountCreationPage(AccountCreationTemplatePageContentModel newAccountCreationPage, Guid newPageGuid)
        {
            Program program = GetProgram(newAccountCreationPage.ProgramGuid);

            PageContent pageContent = new PageContent();
            pageContent.Heading = newAccountCreationPage.Heading;
            //pageContent.Body = newAccountCreationPage.Body;
            //TODO: Need to seperate to different fields.
            pageContent.Body = newAccountCreationPage.Body + ";" + newAccountCreationPage.UserName + ";" + newAccountCreationPage.Password + ";" + newAccountCreationPage.RepeatPassword + ";" + newAccountCreationPage.Mobile + ";" + newAccountCreationPage.CheckBoxText + ";" + newAccountCreationPage.SNText;
            pageContent.PrimaryButtonCaption = newAccountCreationPage.PrimaryButtonCaption;
            pageContent.PrimaryButtonActionParameter = newAccountCreationPage.PrimaryButtonAction;
            pageContent.Page = Resolve<IPageRepository>().GetPageByPageGuid(newPageGuid);

            Resolve<IPageContentRepository>().Add(pageContent);
        }

        private void SavePushPicturePage(PushPictureTemplatePageContentModel newPushPicPage, Guid newPageGuid)
        {
            Program program = GetProgram(newPushPicPage.ProgramGuid);

            PageContent pagecontent = new PageContent();
            // Temp solution
            pagecontent.Heading = "";
            pagecontent.Body = newPushPicPage.Text;
            //pagecontent.Wait = newPushPicPage.Wait;
            if(newPushPicPage.PresenterImageGUID != Guid.Empty)
            {
                Resource pushPictureResource = Resolve<IResourceRepository>().GetResource(newPushPicPage.PresenterImageGUID);
                pagecontent.Resource_BackgroundImage = pushPictureResource;
            }

            pagecontent.AfterShowExpression = newPushPicPage.AfterExpression;
            pagecontent.BeforeShowExpression = newPushPicPage.BeforeExpression;
            pagecontent.Page = Resolve<IPageRepository>().GetPageByPageGuid(newPageGuid);

            // noise when pushing a picture
            if(newPushPicPage.VoiceGUID != Guid.Empty)
            {
                Resource noise = Resolve<IResourceRepository>().GetResource(newPushPicPage.VoiceGUID);
                PageMedia pageMedia = new PageMedia
                {
                    Resource = noise,
                    Type = "Audio",
                    Page = pagecontent.Page
                };

                Resolve<IPageMediaRepository>().AddPageMedia(pageMedia);
            }

            Resolve<IPageContentRepository>().Add(pagecontent);

            //update resource category
            if(newPushPicPage.PresenterImageGUID != Guid.Empty)
            {
                UpdateResourceCatgeoryLastAccessTime(Resolve<IResourceRepository>().GetResource(newPushPicPage.PresenterImageGUID));
            }
        }

        private void SaveTimerPage(TimerTemplatePageContentModel newTimerPage, Guid newPageGuid)
        {
            Program program = GetProgram(newTimerPage.ProgramGuid);

            PageContent pagecontent = new PageContent();
            pagecontent.Heading = newTimerPage.Title;
            pagecontent.Body = newTimerPage.Text;
            pagecontent.PrimaryButtonCaption = newTimerPage.PrimaryButtonCaption;
            pagecontent.PrimaryButtonActionParameter = newTimerPage.PrimaryButtonAction;
            pagecontent.AfterShowExpression = ReplacePageNOWithPageGUIDInExpression(newTimerPage.AfterExpression, newTimerPage.SessionGUID);
            pagecontent.BeforeShowExpression = ReplacePageNOWithPageGUIDInExpression(newTimerPage.BeforeExpression, newTimerPage.SessionGUID);
            pagecontent.Page = Resolve<IPageRepository>().GetPageByPageGuid(newPageGuid);

            Resolve<IPageContentRepository>().Add(pagecontent);
        }

        private void SavePagePreferenceItem(PreferenceItemModel preference, Guid pageGuid)
        {
            Preferences newPreferenceEntity = new Preferences();
            newPreferenceEntity.PreferencesGUID = Guid.NewGuid();
            newPreferenceEntity.Description = preference.Description;
            newPreferenceEntity.Name = preference.Name;
            newPreferenceEntity.Page = Resolve<IPageRepository>().GetPageByPageGuid(pageGuid);
            //newPreferenceEntity.Language = Resolve<ILanguageRepository>().GetLanguage(languageGuid);
            newPreferenceEntity.AnswerText = preference.AnswerText;
            newPreferenceEntity.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
            newPreferenceEntity.Resource = Resolve<IResourceRepository>().GetResource(preference.Resource.ID);
            //newPreferenceEntity.Resource = preferenceResource;

            if(string.IsNullOrEmpty(preference.ButtonName))
            {
                newPreferenceEntity.ButtonName = string.Empty;
            }
            else
            {
                newPreferenceEntity.ButtonName = preference.ButtonName;
            }
            if(preference.Variable != null)
            {
                newPreferenceEntity.PageVariable = Resolve<IPageVaribleRepository>().GetItem(preference.Variable.PageVariableGuid);
            }
            Resolve<IPreferencesRepository>().InsertPreference(newPreferenceEntity);

            UpdateResourceCatgeoryLastAccessTime(newPreferenceEntity.Resource);
        }

        private void SavePageLines(ScreenResultTemplatePageContentModel pageModel, Guid pageGuid, Program program)
        {
            foreach (ScreenResultTemplatePageLineModel pageLine in pageModel.PageLines)
            {
                ScreenResultTemplatePageLine pageLineEntity = new ScreenResultTemplatePageLine();
                pageLineEntity.PageLineGUID = pageLine.PageLineGuid;
                pageLineEntity.Order = pageLine.Order;
                pageLineEntity.Text = pageLine.Text;
                pageLineEntity.URL = pageLine.URL;
                pageLineEntity.Page = Resolve<IPageRepository>().GetPageByPageGuid(pageGuid);
                pageLineEntity.PageGUID = pageGuid;
                pageLineEntity.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
                if (pageLine.PageVariable != null && !string.IsNullOrEmpty(pageLine.PageVariable.Name))
                {
                    pageLineEntity.PageVariable = Resolve<IPageVaribleRepository>().GetItem(pageLine.PageVariable.PageVariableGUID);
                }
                Resolve<IScreenResultTemplatePageLineRepository>().AddPageLine(pageLineEntity);
            }
        }

        private void SavePageQuestions(GetInfoTemplatePageContentModel pageModel, Guid pageGuid, Program program)
        {
            foreach(PageQuestionModel question in pageModel.PageQuestions)
            {
                PageQuestion pageQuestionEntity = new PageQuestion();
                pageQuestionEntity.PageQuestionGUID = question.QuestionGuid;
                pageQuestionEntity.Order = question.Order;
                pageQuestionEntity.Page = Resolve<IPageRepository>().GetPageByPageGuid(pageGuid);
                pageQuestionEntity.IsRequired = question.IsRequired;
                pageQuestionEntity.Question = Resolve<IQuestionRepository>().GetQuestion(question.Guid);

                if(question.PageVariable != null && question.PageVariable.Name != "")
                {
                    pageQuestionEntity.PageVariable = Resolve<IPageVaribleRepository>().GetItem(question.PageVariable.PageVariableGUID);
                }
                pageQuestionEntity.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
                Resolve<IPageQuestionRepository>().AddPageQuestion(pageQuestionEntity);

                //Save page question content
                List<PageQuestionContent> list = new List<PageQuestionContent>();
                //foreach (Language language in program.Language1)
                //{
                PageQuestionContent pageQuestionContentEntity = new PageQuestionContent();
                if(pageQuestionEntity.Question.Name == "Slider")
                {
                    pageQuestionContentEntity.Caption = question.Caption + ";" + question.BeginContent + ";" + question.EndContent;
                }
                else
                {
                    pageQuestionContentEntity.Caption = question.Caption;
                }
                //Language languageEntity = Resolve<ILanguageRepository>().GetLanguage(pageModel.LanguageGUID);
                //pageQuestionContentEntity.Language = languageEntity;
                pageQuestionContentEntity.PageQuestion = pageQuestionEntity;
                pageQuestionContentEntity.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
                list.Add(pageQuestionContentEntity);
                //}
                Resolve<IPageQuestionContentRepository>().InsertPageQuestionContent(list);
                //foreach (Language language in program.Language1)
                //{
                SavePageQuestionItems(question.SubItems, pageQuestionEntity.PageQuestionGUID);
                //}
            }
        }

        private void SavePageQuestionItems(List<PageQuestionItemModel> questionItems, Guid pageQuestionGuid)
        {
            foreach(PageQuestionItemModel questionItem in questionItems)
            {
                PageQuestionItem questionItemEntity = new PageQuestionItem();
                questionItemEntity.PageQuestionItemGUID = Guid.NewGuid();
                questionItemEntity.PageQuestion = Resolve<IPageQuestionRepository>().Get(pageQuestionGuid);
                questionItemEntity.Score = questionItem.Score;
                questionItemEntity.Order = questionItem.Order;
                questionItemEntity.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
                Resolve<IPageQuestionItemRepository>().AddQuestionItem(questionItemEntity);

                //Saver page question item's content
                //foreach (Language lanaguge in proramLanagues)
                //{
                PageQuestionItemContent pageQuestionItemContent = new PageQuestionItemContent();
                //pageQuestionItemContent.Language = Resolve<ILanguageRepository>().GetLanguage(languageGuid);
                pageQuestionItemContent.PageQuestionItem = questionItemEntity;
                pageQuestionItemContent.Item = questionItem.Item;
                pageQuestionItemContent.Feedback = questionItem.Feedback;
                pageQuestionItemContent.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
                Resolve<IPageQuestionItemContentRepository>().InsertPageQuestionItemContent(pageQuestionItemContent);
                //}
            }
        }

        private void AdjustPage(Page page)
        {
            if(!page.PageSequenceReference.IsLoaded)
            {
                page.PageSequenceReference.Load();
            }
            if(!page.PageSequence.Page.IsLoaded)
            {
                page.PageSequence.Page.Load();
            }
            foreach(Page adjPage in page.PageSequence.Page)
            {
                if(adjPage.PageOrderNo > page.PageOrderNo)
                {
                    adjPage.PageOrderNo--;
                }
            }

            Resolve<IPageSequenceRepository>().UpdatePageSequence(page.PageSequence);
        }

        private List<PageQuestionModel> GetPageQuestionModel(Guid pageGuid)
        {
            IQueryable<PageQuestion> pageQuestionEntities = Resolve<IPageQuestionRepository>().GetPageQuestionOfPage(pageGuid);
            List<PageQuestionModel> pageQuestions = ParasePageQuestionModel(pageQuestionEntities);
            return pageQuestions;
        }

        private List<ScreenResultTemplatePageLineModel> GetPageLineModel(Guid pageGuid)
        {
            List<ScreenResultTemplatePageLine> pageLineEntities = Resolve<IScreenResultTemplatePageLineRepository>().GetPageLinesByPageGuid(pageGuid).ToList();
            List<ScreenResultTemplatePageLineModel> pageLineModels = ParasePageLineModel(pageLineEntities);
            return pageLineModels;
        }

        public List<ScreenResultTemplatePageLineModel> ParasePageLineModel(List<ScreenResultTemplatePageLine> pageLineEntities)
        {
            List<ScreenResultTemplatePageLineModel> pageLineModels = new List<ScreenResultTemplatePageLineModel>();
            foreach (ScreenResultTemplatePageLine pageLineEntity in pageLineEntities)
            {
                ScreenResultTemplatePageLineModel pageLineModel = new ScreenResultTemplatePageLineModel();
                pageLineModel.PageLineGuid = pageLineEntity.PageLineGUID;
                pageLineModel.Order = pageLineEntity.Order.Value;
                pageLineModel.Text = pageLineEntity.Text;
                pageLineModel.URL = pageLineEntity.URL;

                if (!pageLineEntity.PageVariableReference.IsLoaded) pageLineEntity.PageVariableReference.Load();
                if (pageLineEntity.PageVariable != null)
                {
                    if (!pageLineEntity.PageVariable.ProgramReference.IsLoaded)
                    {
                        pageLineEntity.PageVariable.ProgramReference.Load();
                    }
                    pageLineModel.PageVariable = new ChangeTech.Models.PageVariableModel
                    {
                        Description = pageLineEntity.PageVariable.Description,
                        Name = pageLineEntity.PageVariable.Name,
                        PageVariableGUID = pageLineEntity.PageVariable.PageVariableGUID,
                        ProgramGUID = pageLineEntity.PageVariable.Program.ProgramGUID,
                    };
                    pageLineModel.PageVariableGuid = pageLineEntity.PageVariable.PageVariableGUID;
                }

                pageLineModels.Add(pageLineModel);
            }
            return pageLineModels;
        }

        public List<PageQuestionModel> ParasePageQuestionModel(IQueryable<PageQuestion> pageQuestionEntities)
        {
            List<PageQuestionModel> pageQuestions = new List<PageQuestionModel>();
            // for transfer the old data
            int orderindex = 1;
            List<PageQuestion> pageQuestionList = pageQuestionEntities.ToList();
            List<PageQuestion> questionList = pageQuestionEntities.ToList();
            foreach(PageQuestion pageQuestionEntity in questionList)
            {
                PageQuestionModel pageQuestionModel = new PageQuestionModel();
                if(!pageQuestionEntity.QuestionReference.IsLoaded)
                {
                    pageQuestionEntity.QuestionReference.Load();
                }
                pageQuestionModel.QuestionGuid = pageQuestionEntity.PageQuestionGUID;
                pageQuestionModel.Order = orderindex;
                pageQuestionModel.Guid = pageQuestionEntity.Question.QuestionGUID;
                pageQuestionModel.IsRequired = pageQuestionEntity.IsRequired;

                PageQuestionContent pageQuestionContent = Resolve<IPageQuestionContentRepository>().GetPageQuestionContentByPageQuestionGuid(pageQuestionEntity.PageQuestionGUID);
                if(pageQuestionContent != null)
                {
                    pageQuestionModel.Caption = pageQuestionContent.Caption;
                }
                if(Resolve<IQuestionRepository>().GetQuestion(pageQuestionModel.Guid).Name == "Slider")
                {
                    string[] stringSeparators = new string[] { ";" };
                    string[] strCaptions = pageQuestionModel.Caption.Split(stringSeparators, StringSplitOptions.None);
                    if(strCaptions.Count<string>() >= 3)
                    {
                        pageQuestionModel.BeginContent = strCaptions[strCaptions.Count<string>() - 2];
                        pageQuestionModel.EndContent = strCaptions[strCaptions.Count<string>() - 1];
                        pageQuestionModel.Caption = pageQuestionModel.Caption.Substring(0, pageQuestionModel.Caption.IndexOf(";"));
                    }
                }

                pageQuestionModel.SubItems = GetPageQuestionItemsModel(pageQuestionEntity.PageQuestionGUID);

                if(!pageQuestionEntity.PageVariableReference.IsLoaded)
                {
                    pageQuestionEntity.PageVariableReference.Load();
                }
                if(pageQuestionEntity.PageVariable != null)
                {
                    if(!pageQuestionEntity.PageVariable.ProgramReference.IsLoaded)
                    {
                        pageQuestionEntity.PageVariable.ProgramReference.Load();
                    }
                    pageQuestionModel.PageVariable = new ChangeTech.Models.PageVariableModel
                    {
                        Description = pageQuestionEntity.PageVariable.Description,
                        Name = pageQuestionEntity.PageVariable.Name,
                        PageVariableGUID = pageQuestionEntity.PageVariable.PageVariableGUID,
                        ProgramGUID = pageQuestionEntity.PageVariable.Program.ProgramGUID,
                    };
                }
                pageQuestions.Add(pageQuestionModel);
                orderindex++;
            }
            return pageQuestions.ToList<PageQuestionModel>();
        }

        private List<PageQuestionItemModel> GetPageQuestionItemsModel(Guid pageQuestion)
        {
            List<PageQuestionItemModel> pageQuestionItemsModel = new List<PageQuestionItemModel>();
            List<PageQuestionItem> questionItemEntities = Resolve<IPageQuestionItemRepository>().GetItemOfQuestion(pageQuestion).ToList();
            int orderIndex = 1;
            foreach(PageQuestionItem questionItemEntity in questionItemEntities)
            {
                PageQuestionItemModel questionItemModel = new PageQuestionItemModel();
                questionItemModel.Guid = questionItemEntity.PageQuestionItemGUID;
                questionItemModel.Score = questionItemEntity.Score.HasValue ? questionItemEntity.Score.Value : 0;
                questionItemModel.Order = orderIndex;
                PageQuestionItemContent pageQuestionItemContent = Resolve<IPageQuestionItemContentRepository>().GetPageQuestionItemContentByPageQuestionItemGuid(questionItemEntity.PageQuestionItemGUID);
                if(pageQuestionItemContent != null)
                {
                    questionItemModel.Feedback = pageQuestionItemContent.Feedback;
                    questionItemModel.Item = pageQuestionItemContent.Item;
                }
                pageQuestionItemsModel.Add(questionItemModel);
                orderIndex++;
            }

            return pageQuestionItemsModel.ToList<PageQuestionItemModel>();
        }

        private EditPageContentModelBase GetEditPageModel(Guid pageGuid)
        {
            EditPageContentModelBase editPageModel = null;
            Page pageEntity = Resolve<IPageRepository>().GetPageByPageGuid(pageGuid);
            if(!pageEntity.PageTemplateReference.IsLoaded)
            {
                pageEntity.PageTemplateReference.Load();
            }
            switch(pageEntity.PageTemplate.Name)
            {
                case "Standard":
                    editPageModel = GetEditStandardTemplatePageContentModel(pageGuid);
                    break;
                case "Get information":
                    editPageModel = GetEditGetInfoTemplatePageContentModel(pageGuid);
                    break;
                case "Screening results":
                    editPageModel = GetEditScreenResultTemplatePageContentModel(pageGuid);
                    break;
                case "Push pictures":
                    editPageModel = GetEditPushPictureTemplatePageContentModel(pageGuid);
                    break;
                case "Timer":
                    editPageModel = GetEditTimerTemplatePageContentModel(pageGuid);
                    break;
                case "Choose preferences":
                    editPageModel = GetEditChoosePreferencesPageContentModel(pageGuid);
                    break;
                case "Account creation":
                    editPageModel = GetEditAccountCreationTemplatePageContentModel(pageGuid);
                    break;
                case "Graph":
                    editPageModel = GetEditGraphTemplatePageContentModel(pageGuid);
                    break;
                case "SMS":
                    editPageModel = GetEditSMSTemplatePageContentModel(pageGuid);
                    break;
            }
            editPageModel.Template = new PageTemplateModel
            {
                Guid = pageEntity.PageTemplate.PageTemplateGUID,
                Name = pageEntity.PageTemplate.Name
            };
            return editPageModel;
        }

        private EditPageContentModelBase GetEditSMSTemplatePageContentModel(Guid pageGuid)
        {
            EditSMSTemplatePageContentModel editSMSTemplatePageContentModel = new EditSMSTemplatePageContentModel();

            PageContent pagecontententity = Resolve<IPageContentRepository>().GetPageContentByPageGuid(pageGuid);
            editSMSTemplatePageContentModel.Text = pagecontententity.Body;
            editSMSTemplatePageContentModel.Time = pagecontententity.SendTime;
            editSMSTemplatePageContentModel.DaysToSend = pagecontententity.DaysToSend.ToString();

            // page variable
            Page pageEntity = Resolve<IPageRepository>().GetPageByPageGuid(pageGuid);
            if(!pageEntity.PageVariableReference.IsLoaded)
            {
                pageEntity.PageVariableReference.Load();
            }

            if(pageEntity.PageVariable != null)
            {
                editSMSTemplatePageContentModel.PageVariable = new SimplePageVariableModel
                {
                    Name = pageEntity.PageVariable.Name,
                    PageVariableGuid = pageEntity.PageVariable.PageVariableGUID
                };
            }

            return editSMSTemplatePageContentModel;
        }

        private EditPageContentModelBase GetEditGraphTemplatePageContentModel(Guid pageGuid)
        {
            EditGraphTemplatePageContentModel editGraphTemplatePageContentModel = new EditGraphTemplatePageContentModel();

            PageContent pageContentEntity = Resolve<IPageContentRepository>().GetPageContentByPageGuid(pageGuid);
            editGraphTemplatePageContentModel.Heading = pageContentEntity.Heading;
            editGraphTemplatePageContentModel.Body = pageContentEntity.Body;
            editGraphTemplatePageContentModel.PrimaryButtonCaption = pageContentEntity.PrimaryButtonCaption;
            Graph graphEntity = Resolve<IGraphRepository>().GetGraphByPageGuid(pageGuid);
            editGraphTemplatePageContentModel.GraphCaption = GetGraphCaption(graphEntity.GraphGUID);
            editGraphTemplatePageContentModel.BadScoreRange = graphEntity.BadScoreRange;
            editGraphTemplatePageContentModel.MediumScoreRange = graphEntity.MediumRange;
            editGraphTemplatePageContentModel.GoodScoreRange = graphEntity.GoodScoreRange;
            editGraphTemplatePageContentModel.GraphType = graphEntity.Type;
            editGraphTemplatePageContentModel.ScoreRange = graphEntity.ScoreRange;
            editGraphTemplatePageContentModel.TimeRange = graphEntity.TimeRange;
            editGraphTemplatePageContentModel.TimeUnit = graphEntity.TimeUnit;
            editGraphTemplatePageContentModel.GraphItem = GetGraphItem(graphEntity.GraphGUID);

            return editGraphTemplatePageContentModel;
        }

        private string GetGraphCaption(Guid guid)
        {
            string cation = string.Empty;
            GraphContent graphContent = Resolve<IGraphContentRepository>().Get(guid);
            if(graphContent != null)
            {
                cation = graphContent.Caption;
            }
            return cation;
        }

        private List<GraphItemModel> GetGraphItem(Guid guid)
        {
            List<GraphItemModel> itemList = new List<GraphItemModel>();
            List<GraphItem> gItems = Resolve<IGraphItemRepository>().GetGraphItemByGraph(guid).ToList();
            foreach(GraphItem gitem in gItems)
            {
                GraphItemContent itemcontent = Resolve<IGraphItemContentRepository>().Get(gitem.GraphItemGUID);
                itemList.Add(new GraphItemModel
                {
                    Color = gitem.Color,
                    Expression = gitem.DataItemExpression,
                    GraphItemModelGUID = gitem.GraphItemGUID,
                    PointType = Convert.ToInt32(gitem.PointType),
                    Name = itemcontent != null ? itemcontent.Name : string.Empty
                });
            }
            return itemList;
        }

        private EditPageContentModelBase GetEditChoosePreferencesPageContentModel(Guid pageGuid)
        {
            EditChoosePreferencesTemplatePageContentModel editChoosePreferencePageContentModel = new EditChoosePreferencesTemplatePageContentModel();

            Page pageEntity = Resolve<IPageRepository>().GetPageByPageGuid(pageGuid);
            editChoosePreferencePageContentModel.MaxPrefereneces = pageEntity.MaxPreferences.HasValue ? pageEntity.MaxPreferences.Value : 0;

            PageContent pageContent = Resolve<IPageContentRepository>().GetPageContentByPageGuid(pageGuid);
            // Primary button
            editChoosePreferencePageContentModel.PrimaryButtonName = pageContent.PrimaryButtonCaption;
            editChoosePreferencePageContentModel.AfterExpression = pageContent.AfterShowExpression;
            editChoosePreferencePageContentModel.BeforeExpression = pageContent.BeforeShowExpression;

            editChoosePreferencePageContentModel.Preferences = new List<PreferenceItemModel>();
            List<Preferences> preferencesEntities = Resolve<IPreferencesRepository>().GetPreferenceByPageGuid(pageGuid).ToList();
            foreach(Preferences preferenceEntity in preferencesEntities)
            {
                PreferenceItemModel preferenceItemModel = new PreferenceItemModel();
                preferenceItemModel.PreferenceGUID = preferenceEntity.PreferencesGUID;
                preferenceItemModel.Name = preferenceEntity.Name;
                preferenceItemModel.Description = preferenceEntity.Description;
                preferenceItemModel.AnswerText = preferenceEntity.AnswerText;
                preferenceItemModel.ButtonName = preferenceEntity.ButtonName;
                if(!preferenceEntity.ResourceReference.IsLoaded)
                {
                    preferenceEntity.ResourceReference.Load();
                }
                preferenceItemModel.Resource = ServiceUtility.ParaseResourceModel(preferenceEntity.Resource);
                if(!preferenceEntity.PageVariableReference.IsLoaded)
                {
                    preferenceEntity.PageVariableReference.Load();
                }
                if(preferenceEntity.PageVariable != null)
                {
                    preferenceItemModel.Variable = new SimplePageVariableModel
                    {
                        Name = preferenceEntity.PageVariable.Name,
                        PageVariableGuid = preferenceEntity.PageVariable.PageVariableGUID
                    };
                }
                editChoosePreferencePageContentModel.Preferences.Add(preferenceItemModel);
            }

            return editChoosePreferencePageContentModel;
        }

        private EditTimerTemplatePageContentModel GetEditTimerTemplatePageContentModel(Guid pageGuid)
        {
            EditTimerTemplatePageContentModel editTimerPageContentModel = new EditTimerTemplatePageContentModel();
            PageContent pagecontent = Resolve<IPageContentRepository>().GetPageContentByPageGuid(pageGuid);

            //primary button
            editTimerPageContentModel.PrimaryButtonAction = pagecontent.PrimaryButtonActionParameter;
            editTimerPageContentModel.PrimaryButtonCaption = pagecontent.PrimaryButtonCaption;
            editTimerPageContentModel.AfterExpression = pagecontent.AfterShowExpression;
            editTimerPageContentModel.BeforeExpression = pagecontent.BeforeShowExpression;

            editTimerPageContentModel.Text = pagecontent.Body;
            editTimerPageContentModel.Title = pagecontent.Heading;

            // page variable
            Page pageEntity = Resolve<IPageRepository>().GetPageByPageGuid(pageGuid);
            if(!pageEntity.PageVariableReference.IsLoaded)
            {
                pageEntity.PageVariableReference.Load();
            }

            if(pageEntity.PageVariable != null)
            {
                editTimerPageContentModel.PageVariable = new SimplePageVariableModel
                {
                    Name = pageEntity.PageVariable.Name,
                    PageVariableGuid = pageEntity.PageVariable.PageVariableGUID
                };
            }

            return editTimerPageContentModel;
        }

        private EditAccountCreationTemplatePageContentModel GetEditAccountCreationTemplatePageContentModel(Guid pageGuid)
        {
            EditAccountCreationTemplatePageContentModel editAccountCreationPageContentModel = new EditAccountCreationTemplatePageContentModel();
            PageContent pagecontent = Resolve<IPageContentRepository>().GetPageContentByPageGuid(pageGuid);

            //primary button
            editAccountCreationPageContentModel.PrimaryButtonAction = pagecontent.PrimaryButtonActionParameter;
            editAccountCreationPageContentModel.PrimaryButtonCaption = pagecontent.PrimaryButtonCaption;
            editAccountCreationPageContentModel.AfterExpression = pagecontent.AfterShowExpression;
            editAccountCreationPageContentModel.BeforeExpression = pagecontent.BeforeShowExpression;
            
            //editAccountCreationPageContentModel.Body = pagecontent.Body;
            //TODO: need to save to different fields.
            string[] textStr = pagecontent.Body.Split(';');
            editAccountCreationPageContentModel.Body = textStr[0];
            editAccountCreationPageContentModel.UserName = textStr[1];
            editAccountCreationPageContentModel.Password = textStr[2];
            editAccountCreationPageContentModel.RepeatPassword = textStr[3];
            if (textStr.Count() == 5)
            {
                editAccountCreationPageContentModel.CheckBoxText = textStr[4];
            }
            else if (textStr.Count() == 6)
            {
                editAccountCreationPageContentModel.Mobile = textStr[4];
                editAccountCreationPageContentModel.CheckBoxText = textStr[5];
            }
            else if (textStr.Count() > 6)
            {
                editAccountCreationPageContentModel.Mobile = textStr[textStr.Count() - 3];
                editAccountCreationPageContentModel.CheckBoxText = textStr[textStr.Count() - 2];
                editAccountCreationPageContentModel.SNText = textStr[textStr.Count() - 1];
            }
            editAccountCreationPageContentModel.Heading = pagecontent.Heading;

            // page variable
            Page pageEntity = Resolve<IPageRepository>().GetPageByPageGuid(pageGuid);
            if (!pageEntity.PageVariableReference.IsLoaded)
            {
                pageEntity.PageVariableReference.Load();
            }

            if (pageEntity.PageVariable != null)
            {
                editAccountCreationPageContentModel.PageVariable = new SimplePageVariableModel
                {
                    Name = pageEntity.PageVariable.Name,
                    PageVariableGuid = pageEntity.PageVariable.PageVariableGUID
                };
            }

            return editAccountCreationPageContentModel;
        }

        private EditPushPictureTemplatePageContentModel GetEditPushPictureTemplatePageContentModel(Guid pageGuid)
        {
            EditPushPictureTemplatePageContentModel editPushPicTemplatePageContentModel = new EditPushPictureTemplatePageContentModel();

            Page pageEntity = Resolve<IPageRepository>().GetPageByPageGuid(pageGuid);
            editPushPicTemplatePageContentModel.Wait = pageEntity.Wait;

            PageContent pageContent = Resolve<IPageContentRepository>().GetPageContentByPageGuid(pageGuid);
            editPushPicTemplatePageContentModel.AfterExpression = pageContent.AfterShowExpression;
            editPushPicTemplatePageContentModel.BeforeExpression = pageContent.BeforeShowExpression;
            editPushPicTemplatePageContentModel.Text = pageContent.Body;
            // Presenter image
            if (!pageContent.Resource_BackgroundImageReference.IsLoaded)
            {
                pageContent.Resource_BackgroundImageReference.Load();
            }
            if (pageContent.Resource_BackgroundImage != null)
            {
                editPushPicTemplatePageContentModel.PresenterImage = ServiceUtility.ParaseResourceModel(pageContent.Resource_BackgroundImage);
            }

            // page media
            if(!pageEntity.PageMediaReference.IsLoaded)
            {
                pageEntity.PageMediaReference.Load();
            }
            if(pageEntity.PageMedia != null)
            {
                if(!pageEntity.PageMedia.ResourceReference.IsLoaded)
                {
                    pageEntity.PageMedia.ResourceReference.Load();
                }
                ResourceModel resource = new ResourceModel();
                resource.ID = pageEntity.PageMedia.Resource.ResourceGUID;
                resource.Name = pageEntity.PageMedia.Resource.Name;
                resource.NameOnServer = pageEntity.PageMedia.Resource.NameOnServer;

                editPushPicTemplatePageContentModel.Media = new PageMediaModel
                {
                    Resource = resource,
                    Type = "Audio"
                };
            }

            return editPushPicTemplatePageContentModel;
        }

        private EditGetInfoTemplatePageContentModel GetEditGetInfoTemplatePageContentModel(Guid pageGuid)
        {
            EditGetInfoTemplatePageContentModel editGetInfoTemplatePageContentModel = new EditGetInfoTemplatePageContentModel();
            PageContent pageContentEntity = Resolve<IPageContentRepository>().GetPageContentByPageGuid(pageGuid);
            editGetInfoTemplatePageContentModel.Heading = pageContentEntity.Heading;
            editGetInfoTemplatePageContentModel.Body = pageContentEntity.Body;
            editGetInfoTemplatePageContentModel.FooterText = pageContentEntity.FooterText;
            editGetInfoTemplatePageContentModel.AfterExpression = pageContentEntity.AfterShowExpression;
            editGetInfoTemplatePageContentModel.BeforeExpression = pageContentEntity.BeforeShowExpression;
            editGetInfoTemplatePageContentModel.PrimaryButtonAction = pageContentEntity.PrimaryButtonActionParameter;

            //compatible old data
            if(pageContentEntity.PrimaryButtonCaption.Contains(';'))
            {
                string[] buttonText = pageContentEntity.PrimaryButtonCaption.Split(';');
                editGetInfoTemplatePageContentModel.SecondaryButtonCaption = buttonText[0];
                editGetInfoTemplatePageContentModel.PrimaryButtonCaption = buttonText[1];
            }
            else
            {
                editGetInfoTemplatePageContentModel.PrimaryButtonCaption = pageContentEntity.PrimaryButtonCaption;
                editGetInfoTemplatePageContentModel.SecondaryButtonCaption = string.Empty;
            }
            // Presenter image
            if(!pageContentEntity.Resource_PresenterImageReference.IsLoaded)
            {
                pageContentEntity.Resource_PresenterImageReference.Load();
            }
            if (pageContentEntity.Resource_PresenterImage != null)
            {
                editGetInfoTemplatePageContentModel.PresenterImage = ServiceUtility.ParaseResourceModel(pageContentEntity.Resource_BackgroundImage);
                editGetInfoTemplatePageContentModel.PresenterImagePosition = pageContentEntity.PresenterImagePosition;
                editGetInfoTemplatePageContentModel.PresenterMode = pageContentEntity.PresenterMode;
                editGetInfoTemplatePageContentModel.ImageMode = ImageModeEnum.PresenterMode;
            }
            // Background image
            if (!pageContentEntity.Resource_BackgroundImageReference.IsLoaded)
            {
                pageContentEntity.Resource_BackgroundImageReference.Load();
            }
            if (pageContentEntity.Resource_BackgroundImage != null)
            {
                editGetInfoTemplatePageContentModel.BackgroudImage = ServiceUtility.ParaseResourceModel(pageContentEntity.Resource_BackgroundImage);
                editGetInfoTemplatePageContentModel.ImageMode = ImageModeEnum.FullscreenMode;
            }
            //Illustration image
            if (pageContentEntity.IllustrationImageGUID != null)
            {
                Resource IllustrationResource = Resolve<IResourceRepository>().GetResource(pageContentEntity.IllustrationImageGUID.Value);
                editGetInfoTemplatePageContentModel.IllustrationImage = ServiceUtility.ParaseResourceModel(IllustrationResource);
                editGetInfoTemplatePageContentModel.ImageMode = ImageModeEnum.IllustrationMode;
            }

            //Page Questions
            editGetInfoTemplatePageContentModel.PageQuestions = GetPageQuestionModel(pageGuid);
            editGetInfoTemplatePageContentModel.Questions = GetQuestionsModel();
            return editGetInfoTemplatePageContentModel;
        }

        private EditScreenResultsTemplatePageContentModel GetEditScreenResultTemplatePageContentModel(Guid pageGuid)
        {
            EditScreenResultsTemplatePageContentModel editScreenResultTemplatePageContentModel = new EditScreenResultsTemplatePageContentModel();
            PageContent pageContentEntity = Resolve<IPageContentRepository>().GetPageContentByPageGuid(pageGuid);
            editScreenResultTemplatePageContentModel.Heading = pageContentEntity.Heading;
            editScreenResultTemplatePageContentModel.Body = pageContentEntity.Body;
            editScreenResultTemplatePageContentModel.AfterExpression = pageContentEntity.AfterShowExpression;
            editScreenResultTemplatePageContentModel.BeforeExpression = pageContentEntity.BeforeShowExpression;
            editScreenResultTemplatePageContentModel.PrimaryButtonAction = pageContentEntity.PrimaryButtonActionParameter;

            //compatible old data
            if (pageContentEntity.PrimaryButtonCaption.Contains(';'))
            {
                string[] buttonText = pageContentEntity.PrimaryButtonCaption.Split(';');
                editScreenResultTemplatePageContentModel.SecondaryButtonCaption = buttonText[0];
                editScreenResultTemplatePageContentModel.PrimaryButtonCaption = buttonText[1];
            }
            else
            {
                editScreenResultTemplatePageContentModel.PrimaryButtonCaption = pageContentEntity.PrimaryButtonCaption;
                editScreenResultTemplatePageContentModel.SecondaryButtonCaption = string.Empty;
            }
            // Presenter image
            if (!pageContentEntity.Resource_PresenterImageReference.IsLoaded)
            {
                pageContentEntity.Resource_PresenterImageReference.Load();
            }
            if (pageContentEntity.Resource_PresenterImage != null)
            {
                editScreenResultTemplatePageContentModel.PresenterImage = ServiceUtility.ParaseResourceModel(pageContentEntity.Resource_PresenterImage);
                editScreenResultTemplatePageContentModel.ImageMode = ImageModeEnum.PresenterMode;
                editScreenResultTemplatePageContentModel.PresenterImagePosition = pageContentEntity.PresenterImagePosition;
                editScreenResultTemplatePageContentModel.PresenterMode = pageContentEntity.PresenterMode;
            }

            if (!pageContentEntity.Resource_PageGraphic1Reference.IsLoaded) pageContentEntity.Resource_PageGraphic1Reference.Load();
            if (!pageContentEntity.Resource_PageGraphic2Reference.IsLoaded) pageContentEntity.Resource_PageGraphic2Reference.Load();
            if (!pageContentEntity.Resource_PageGraphic3Reference.IsLoaded) pageContentEntity.Resource_PageGraphic3Reference.Load();
            if (pageContentEntity.Resource_PageGraphic1 != null)
            {
                editScreenResultTemplatePageContentModel.PageGraphic1Image = ServiceUtility.ParaseResourceModel(pageContentEntity.Resource_PageGraphic1);
            }
            if (pageContentEntity.Resource_PageGraphic2 != null)
            {
                editScreenResultTemplatePageContentModel.PageGraphic2Image = ServiceUtility.ParaseResourceModel(pageContentEntity.Resource_PageGraphic2);
            }
            if (pageContentEntity.Resource_PageGraphic3 != null)
            {
                editScreenResultTemplatePageContentModel.PageGraphic3Image = ServiceUtility.ParaseResourceModel(pageContentEntity.Resource_PageGraphic3);
            }

            //Page Lines
            editScreenResultTemplatePageContentModel.PageLines = GetPageLineModel(pageGuid);
            return editScreenResultTemplatePageContentModel;
        }

        private EditStandardTemplatePageContentModel GetEditStandardTemplatePageContentModel(Guid pageGuid)
        {
            EditStandardTemplatePageContentModel editStandardTemplatePageContentModel = new EditStandardTemplatePageContentModel();
            PageContent pageContentEntity = Resolve<IPageContentRepository>().GetPageContentByPageGuid(pageGuid);
            editStandardTemplatePageContentModel.Heading = pageContentEntity.Heading;
            editStandardTemplatePageContentModel.Body = pageContentEntity.Body;
            editStandardTemplatePageContentModel.PrimaryButtonCaption = pageContentEntity.PrimaryButtonCaption;
            editStandardTemplatePageContentModel.PrimaryButtonAction = pageContentEntity.PrimaryButtonActionParameter;

            //Before expression
            editStandardTemplatePageContentModel.BeforeExpression = pageContentEntity.BeforeShowExpression;
            //After expression
            editStandardTemplatePageContentModel.AfterExpression = pageContentEntity.AfterShowExpression;

            // Background image
            if(!pageContentEntity.Resource_BackgroundImageReference.IsLoaded)
            {
                pageContentEntity.Resource_BackgroundImageReference.Load();
            }
            if (pageContentEntity.Resource_BackgroundImage != null)
            {
                editStandardTemplatePageContentModel.BackgroudImage = ServiceUtility.ParaseResourceModel(pageContentEntity.Resource_BackgroundImage);
                editStandardTemplatePageContentModel.ImageMode = ImageModeEnum.FullscreenMode;
            }
            // Presenter image
            if(!pageContentEntity.Resource_PresenterImageReference.IsLoaded)
            {
                pageContentEntity.Resource_PresenterImageReference.Load();
            }
            if (pageContentEntity.Resource_PresenterImage != null)
            {
                editStandardTemplatePageContentModel.PresenterImage = ServiceUtility.ParaseResourceModel(pageContentEntity.Resource_PresenterImage);
                editStandardTemplatePageContentModel.PresenterImagePosition = pageContentEntity.PresenterImagePosition;
                editStandardTemplatePageContentModel.PresenterImageMode = pageContentEntity.PresenterMode;
                editStandardTemplatePageContentModel.ImageMode = ImageModeEnum.PresenterMode;
            }
            //Illustration image
            if (pageContentEntity.IllustrationImageGUID!=null)
            {
                Resource IllustrationResource = Resolve<IResourceRepository>().GetResource(pageContentEntity.IllustrationImageGUID.Value);
                editStandardTemplatePageContentModel.IllustrationImage = ServiceUtility.ParaseResourceModel(IllustrationResource);
                editStandardTemplatePageContentModel.ImageMode = ImageModeEnum.IllustrationMode;
            }

            PageMedia pageMediaEntity = Resolve<IPageMediaRepository>().GetPageMediaByPageGuid(pageGuid);
            if(pageMediaEntity != null)
            {
                if(!pageMediaEntity.ResourceReference.IsLoaded)
                {
                    pageMediaEntity.ResourceReference.Load();
                }

                if(!(pageMediaEntity.IsDeleted.HasValue && pageMediaEntity.IsDeleted == true))
                {
                    editStandardTemplatePageContentModel.Media = new PageMediaModel
                    {
                        Type = pageMediaEntity.Type,
                        Resource = ServiceUtility.ParaseResourceModel(pageMediaEntity.Resource)
                    };
                }
            }

            return editStandardTemplatePageContentModel;
        }

        private void UpdateGetInfoPageQuestions(GetInfoTemplatePageContentModel getInfoPage, Guid pageGuid)
        {
            Page upatePageEntity = Resolve<IPageRepository>().GetPageByPageGuid(pageGuid);
            if(!upatePageEntity.PageSequenceReference.IsLoaded)
            {
                upatePageEntity.PageSequenceReference.Load();
            }
            //Language languageEntity = Resolve<ILanguageRepository>().GetLanguage(getInfoPage.LanguageGUID);
            List<PageQuestionModel> pagequestions = getInfoPage.PageQuestions.Where(p => getInfoPage.ObjectStatus.ContainsKey(p.Guid)).ToList();

            foreach(PageQuestionModel model in pagequestions)
            {

            }

            foreach(PageQuestionModel questionModel in getInfoPage.PageQuestions)
            {
                if(getInfoPage.ObjectStatus.ContainsKey(questionModel.QuestionGuid))
                {
                    PageQuestion question;
                    PageQuestionContent pageQuestionContent;
                    switch(getInfoPage.ObjectStatus[questionModel.QuestionGuid])
                    {
                        case ModelStatus.QuestionAdded:
                            question = new PageQuestion
                            {
                                PageQuestionGUID = questionModel.QuestionGuid,
                                Order = questionModel.Order,
                                Page = Resolve<IPageRepository>().GetPageByPageGuid(pageGuid),
                                //Language = Resolve<ILanguageRepository>().GetLanguage(getInfoPage.LanguageGUID),
                                Question = Resolve<IQuestionRepository>().GetQuestion(questionModel.Guid),
                                //Caption = questionModel.Caption,
                                IsRequired = questionModel.IsRequired,
                            };

                            if(questionModel.PageVariable != null && questionModel.PageVariable.Name != "")
                            {
                                question.PageVariable = Resolve<IPageVaribleRepository>().GetItem(questionModel.PageVariable.PageVariableGUID);
                            }
                            pageQuestionContent = new PageQuestionContent
                            {
                                Caption = questionModel.Caption,
                                //Language = languageEntity,
                            };
                            if(question.Question.Name == "Slider")
                            {
                                pageQuestionContent.Caption += ";" + questionModel.BeginContent + ";" + questionModel.EndContent;
                            }
                            question.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
                            question.PageQuestionContent = pageQuestionContent;

                            foreach(PageQuestionItemModel itemModel in questionModel.SubItems)
                            {
                                PageQuestionItem item = new PageQuestionItem
                                {
                                    PageQuestionItemGUID = itemModel.Guid,
                                    Score = itemModel.Score,
                                    Order = itemModel.Order,
                                };

                                PageQuestionItemContent itemContent = new PageQuestionItemContent
                                {
                                    Feedback = itemModel.Feedback,
                                    Item = itemModel.Item,
                                    //Language = languageEntity,
                                };
                                item.PageQuestionItemContent = itemContent;
                                item.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
                                question.PageQuestionItem.Add(item);
                            }
                            Resolve<IPageQuestionRepository>().AddPageQuestion(question);
                            break;
                        case ModelStatus.QuestionUpdated:
                            //Update the quesiton's property
                            question = Resolve<IPageQuestionRepository>().Get(questionModel.QuestionGuid);
                            question.Question = Resolve<IQuestionRepository>().GetQuestion(questionModel.Guid);
                            question.Order = questionModel.Order;
                            question.IsRequired = questionModel.IsRequired;
                            if(questionModel.PageVariable != null && questionModel.PageVariable.Name != "")
                            {
                                question.PageVariable = Resolve<IPageVaribleRepository>().GetItem(questionModel.PageVariable.PageVariableGUID);
                            }
                            Resolve<IPageQuestionRepository>().UpdatePageQuestion(question);
                            //Update question's content
                            pageQuestionContent = Resolve<IPageQuestionContentRepository>().GetPageQuestionContentByPageQuestionGuid(questionModel.QuestionGuid);
                            pageQuestionContent.Caption = questionModel.Caption;
                            if(question.Question.Name == "Slider")
                            {
                                pageQuestionContent.Caption += ";" + questionModel.BeginContent + ";" + questionModel.EndContent;
                            }
                            pageQuestionContent.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
                            Resolve<IPageQuestionContentRepository>().UpdatePageQuestionContent(pageQuestionContent);
                            Resolve<ITranslationJobService>().UpdateElementWhenFromUpdated("PageQuestionContent", pageQuestionContent.PageQuestionGUID.ToString(), Guid.Empty);

                            //Update the quesiton's items
                            if(questionModel.SubItems != null)
                            {
                                foreach(PageQuestionItemModel itemModel in questionModel.SubItems)
                                {
                                    if(getInfoPage.ObjectStatus.ContainsKey(itemModel.Guid))
                                    {
                                        switch(getInfoPage.ObjectStatus[itemModel.Guid])
                                        {
                                            case ModelStatus.QuestionItemAdded:
                                                PageQuestionItem item = new PageQuestionItem
                                                {
                                                    PageQuestionItemGUID = itemModel.Guid,
                                                    Order = itemModel.Order,
                                                    PageQuestion = Resolve<IPageQuestionRepository>().Get(questionModel.QuestionGuid),
                                                    Score = itemModel.Score,
                                                };

                                                PageQuestionItemContent itemContent = new PageQuestionItemContent
                                                {
                                                    Item = itemModel.Item,
                                                    Feedback = itemModel.Feedback,
                                                    //Language = languageEntity
                                                };
                                                item.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
                                                item.PageQuestionItemContent = itemContent;
                                                Resolve<IPageQuestionItemRepository>().AddQuestionItem(item);
                                                break;
                                            case ModelStatus.QuestionItemDeleted:
                                                //PageQuestionItem pageQuestionItemEntity = Resolve<IPageQuestionItemRepository>().Get(itemModel.Guid);
                                                //if (!pageQuestionItemEntity.PageQuestionItemContentReference.IsLoaded)
                                                //{
                                                //    pageQuestionItemEntity.PageQuestionItemContent.Load();
                                                //}
                                                Resolve<IPageQuestionItemContentRepository>().DeletePageQuestionItemContent(itemModel.Guid);
                                                Resolve<IPageQuestionItemRepository>().DeleteQuestionItem(itemModel.Guid);
                                                break;
                                            case ModelStatus.QuestionItemUpdated:
                                                PageQuestionItem itemUpdate = Resolve<IPageQuestionItemRepository>().Get(itemModel.Guid);
                                                itemUpdate.Score = itemModel.Score;
                                                itemUpdate.Order = itemModel.Order;
                                                itemUpdate.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
                                                Resolve<IPageQuestionItemRepository>().UpdateQuestionItem(itemUpdate);

                                                PageQuestionItemContent itemContentUpdate = Resolve<IPageQuestionItemContentRepository>().GetPageQuestionItemContentByPageQuestionItemGuid(itemModel.Guid);
                                                itemContentUpdate.Item = itemModel.Item;
                                                itemContentUpdate.Feedback = itemModel.Feedback;
                                                itemContentUpdate.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
                                                Resolve<IPageQuestionItemContentRepository>().UpdatePageQuestionItemContent(itemContentUpdate);
                                                Resolve<ITranslationJobService>().UpdateElementWhenFromUpdated("PageQuestionItemContent", itemContentUpdate.PageQuestionItemGUID.ToString(), Guid.Empty);
                                                break;
                                        }
                                    }
                                }
                            }
                            break;
                    }
                }
            }

            foreach(KeyValuePair<Guid, ModelStatus> item in getInfoPage.ObjectStatus)
            {
                if(item.Value == ModelStatus.QuestionItemDeleted)
                {
                    PageQuestionItem pageQuestionItemEntity = Resolve<IPageQuestionItemRepository>().Get(item.Key);
                    if(pageQuestionItemEntity != null)
                    {
                        if(!pageQuestionItemEntity.QuestionAnswerValue.IsLoaded)
                        {
                            pageQuestionItemEntity.QuestionAnswerValue.Load();
                        }
                        Resolve<IQuestionAnswerValueRepository>().Delete(pageQuestionItemEntity.QuestionAnswerValue);
                        Resolve<IPageQuestionItemContentRepository>().DeletePageQuestionItemContent(item.Key);
                        Resolve<IPageQuestionItemRepository>().DeleteQuestionItem(item.Key);
                    }
                }
                if(item.Value == ModelStatus.QuestionDeleted)
                {
                    PageQuestion pagequestion = Resolve<IPageQuestionRepository>().Get(item.Key);

                    if(!pagequestion.QuestionAnswer.IsLoaded)
                    {
                        pagequestion.QuestionAnswer.Load();
                    }
                    foreach(QuestionAnswer qa in pagequestion.QuestionAnswer)
                    {
                        if(!qa.UserPageVariable.IsLoaded)
                        {
                            qa.UserPageVariable.Load();
                        }
                        Resolve<IUserPageVariableRepository>().Delete(qa.UserPageVariable);
                        if(!qa.UserPageVariablePerDay.IsLoaded)
                        {
                            qa.UserPageVariablePerDay.Load();
                        }
                        Resolve<IUserPageVariablePerDayRepository>().Delete(qa.UserPageVariablePerDay);
                    }

                    Resolve<IQuestionAnswerValueRepository>().DeleteByQuestionGuid(item.Key);
                    Resolve<IQuestionAnswerRepository>().DeleteByQuestion(item.Key);

                    Resolve<IPageQuestionItemContentRepository>().DeletePageQuestionItemContentByQuestionGUID(item.Key);
                    Resolve<IPageQuestionItemRepository>().DeleteQuestionItemsByQuestionID(item.Key);
                    Resolve<IPageQuestionContentRepository>().DeletePageQuestionContent(item.Key);
                    Resolve<IPageQuestionRepository>().DeletePageQuestion(item.Key);
                }
            }
        }

        private void UpdateScreenResultsPageContent(ScreenResultTemplatePageContentModel screenResultsPage, Guid pageGuid)
        {
            PageContent pageContentEntity = Resolve<IPageContentRepository>().GetPageContentByPageGuid(pageGuid);
            pageContentEntity.Heading = screenResultsPage.Heading;
            pageContentEntity.Body = screenResultsPage.Body;
            pageContentEntity.PrimaryButtonCaption = screenResultsPage.PrimaryButtonCaption;
            pageContentEntity.PrimaryButtonActionParameter = screenResultsPage.PrimaryButtonAction;
            pageContentEntity.BeforeShowExpression = ReplacePageNOWithPageGUIDInExpression(screenResultsPage.BeforeExpression, screenResultsPage.SessionGUID);
            pageContentEntity.AfterShowExpression = ReplacePageNOWithPageGUIDInExpression(screenResultsPage.AfterExpression, screenResultsPage.SessionGUID);

            if (screenResultsPage.PageGraphic1GUID!=Guid.Empty)
            {
                Resource pageGraphic1Resoucre = Resolve<IResourceRepository>().GetResource(screenResultsPage.PageGraphic1GUID);
                pageContentEntity.Resource_PageGraphic1 = pageGraphic1Resoucre;
                pageContentEntity.PageGraphic1GUID = pageGraphic1Resoucre.ResourceGUID;
            }
            if (screenResultsPage.PageGraphic2GUID != Guid.Empty)
            {
                Resource pageGraphic2Resoucre = Resolve<IResourceRepository>().GetResource(screenResultsPage.PageGraphic2GUID);
                pageContentEntity.Resource_PageGraphic2 = pageGraphic2Resoucre;
                pageContentEntity.PageGraphic2GUID = pageGraphic2Resoucre.ResourceGUID;
            }
            if (screenResultsPage.PageGraphic3GUID != Guid.Empty)
            {
                Resource pageGraphic3Resoucre = Resolve<IResourceRepository>().GetResource(screenResultsPage.PageGraphic3GUID);
                pageContentEntity.Resource_PageGraphic3 = pageGraphic3Resoucre;
                pageContentEntity.PageGraphic3GUID = pageGraphic3Resoucre.ResourceGUID;
            }

            if (screenResultsPage.PresenterImageGUID == Guid.Empty)
            {
                pageContentEntity.Resource_BackgroundImage = null;
            }
            else
            {
                Resource presenterResource = Resolve<IResourceRepository>().GetResource(screenResultsPage.PresenterImageGUID);
                pageContentEntity.Resource_BackgroundImage = presenterResource;
                pageContentEntity.ImageMode = "Presenter";
                pageContentEntity.PresenterImagePosition = screenResultsPage.PresenterImagePosition;
                pageContentEntity.PresenterMode = screenResultsPage.PresenterMode;
                UpdateResourceCatgeoryLastAccessTime(presenterResource);
            }
            //pageContentEntity.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid.ToString();
            Resolve<IPageContentRepository>().Update(pageContentEntity);
            Resolve<ITranslationJobService>().UpdateElementWhenFromUpdated("PageContent", pageContentEntity.PageGUID.ToString(), Guid.Empty);
        }

        private void UpdateGetInfoPageContent(GetInfoTemplatePageContentModel getInfoPage, Guid pageGuid)
        {
            PageContent pageContentEntity = Resolve<IPageContentRepository>().GetPageContentByPageGuid(pageGuid);
            pageContentEntity.Heading = getInfoPage.Heading;
            pageContentEntity.Body = getInfoPage.Body;
            pageContentEntity.FooterText = getInfoPage.FooterText;
            pageContentEntity.PrimaryButtonCaption = getInfoPage.PrimaryButtonCaption;
            pageContentEntity.PrimaryButtonActionParameter = getInfoPage.PrimaryButtonAction;
            pageContentEntity.BeforeShowExpression = ReplacePageNOWithPageGUIDInExpression(getInfoPage.BeforeExpression, getInfoPage.SessionGUID);
            pageContentEntity.AfterShowExpression = ReplacePageNOWithPageGUIDInExpression(getInfoPage.AfterExpression, getInfoPage.SessionGUID);

            //Preseter image
            if (getInfoPage.ImageMode != ImageModeEnum.PresenterMode && getInfoPage.PresenterImageGUID == Guid.Empty)
            {
                pageContentEntity.Resource_PresenterImage = null;
            }
            else
            {
                Resource presenterResource = Resolve<IResourceRepository>().GetResource(getInfoPage.PresenterImageGUID);
                pageContentEntity.Resource_PresenterImage = presenterResource;
                pageContentEntity.ImageMode = "Preseter";
                pageContentEntity.PresenterImagePosition = getInfoPage.PresenterImagePosition;
                pageContentEntity.PresenterMode = getInfoPage.PresenterMode;
                //Update resource category's last access time
                UpdateResourceCatgeoryLastAccessTime(presenterResource);
            }

            //FullScreen image
            if (getInfoPage.ImageMode != ImageModeEnum.FullscreenMode && getInfoPage.BackgroundImageGUID == Guid.Empty)
            {
                pageContentEntity.Resource_BackgroundImage = null;
            }
            else
            {
                Resource backgroudResource = Resolve<IResourceRepository>().GetResource(getInfoPage.BackgroundImageGUID);
                pageContentEntity.Resource_BackgroundImage = backgroudResource;
                pageContentEntity.ImageMode = "Fullscreen";
                //Update resource category's last access time
                UpdateResourceCatgeoryLastAccessTime(backgroudResource);
            }

            //Illustration image
            if (getInfoPage.ImageMode != ImageModeEnum.IllustrationMode && getInfoPage.IllustrationImageGUID == Guid.Empty)
            {
                pageContentEntity.IllustrationImageGUID = null;
            }
            else
            {
                Resource illustrationResource = Resolve<IResourceRepository>().GetResource(getInfoPage.IllustrationImageGUID);
                pageContentEntity.IllustrationImageGUID = illustrationResource.ResourceGUID;
                pageContentEntity.ImageMode = "Illustration";
               
                //Update resource category's last access time
                UpdateResourceCatgeoryLastAccessTime(illustrationResource);
            }

            bool hasDifference = false;
            bool hasPageMediaBefore = false;
            Resource referencedResource = null;
            PageMedia pageMediaEntity = Resolve<IPageMediaRepository>().GetPageMediaByPageGuid(pageGuid);
            if (pageMediaEntity != null)
            {
                hasPageMediaBefore = true;

                if (!pageMediaEntity.ResourceReference.IsLoaded)
                {
                    pageMediaEntity.ResourceReference.Load();
                }
            }
            else
            {
                pageMediaEntity = new PageMedia();
            }
            #region IllustrationImage old follows
            if (getInfoPage.IllustrationImageGUID != Guid.Empty)
            {
                Resource illustrationResource = Resolve<IResourceRepository>().GetResource(getInfoPage.IllustrationImageGUID);
                pageMediaEntity.Page = Resolve<IPageRepository>().GetPageByPageGuid(pageGuid);
                //pageMediaEntity.Language = Resolve<ILanguageRepository>().GetLanguage(standardPage.LanguageGUID);
                pageMediaEntity.Resource = illustrationResource;
                pageMediaEntity.Type = "Illustration";
                hasDifference = true;
                referencedResource = illustrationResource;
                //UpdateResourceCatgeoryLastAccessTime(illustrationResource);
            } 
            #endregion
          
            if (hasDifference)
            {
                pageMediaEntity.IsDeleted = false;
                if (hasPageMediaBefore)
                {
                    Resolve<IPageMediaRepository>().UpdatePageMedia(pageMediaEntity);
                }
                else
                {
                    PageMedia pmEntity = Resolve<IPageMediaRepository>().GetPageMediaByPageGuid(pageMediaEntity.PageGUID);
                    if (pmEntity == null)
                    {
                        Resolve<IPageMediaRepository>().AddPageMedia(pageMediaEntity);

                    }
                    else
                    {
                        Resolve<IPageMediaRepository>().UpdatePageMedia(pageMediaEntity);
                    }
                }

                if (referencedResource != null)
                {
                    UpdateResourceCatgeoryLastAccessTime(referencedResource);
                }
            }

            if (hasPageMediaBefore && getInfoPage.IllustrationImageGUID == Guid.Empty)
            {
                Resolve<IPageMediaRepository>().DeletePageMedia(pageGuid);
            }

            //pageContentEntity.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid.ToString();
            Resolve<IPageContentRepository>().Update(pageContentEntity);
            Resolve<ITranslationJobService>().UpdateElementWhenFromUpdated("PageContent", pageContentEntity.PageGUID.ToString(), Guid.Empty);
        }

        private void UpdatePushPicPageContent(PushPictureTemplatePageContentModel pushPicPage, Guid pageGuid)
        {
            PageContent pageContentEntity = Resolve<IPageContentRepository>().GetPageContentByPageGuid(pageGuid);
            pageContentEntity.AfterShowExpression = pushPicPage.AfterExpression;
            pageContentEntity.BeforeShowExpression = pushPicPage.BeforeExpression;
            pageContentEntity.Body = pushPicPage.Text;
            //pageContentEntity.Wait = pushPicPage.Wait;
            if(pushPicPage.PresenterImageGUID == Guid.Empty)
            {
                pageContentEntity.Resource_BackgroundImage = null;
            }
            else
            {
                Resource pushPictureResource = Resolve<IResourceRepository>().GetResource(pushPicPage.PresenterImageGUID);
                pageContentEntity.Resource_BackgroundImage = pushPictureResource;

                UpdateResourceCatgeoryLastAccessTime(pushPictureResource);
            }
            //pageContentEntity.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid.ToString();
            Resolve<IPageContentRepository>().Update(pageContentEntity);
            Resolve<ITranslationJobService>().UpdateElementWhenFromUpdated("PageContent", pageContentEntity.PageGUID.ToString(), Guid.Empty);
        }

        private void UpdateTimerPageContent(TimerTemplatePageContentModel timerPage, Guid pageGuid)
        {
            PageContent pageContentEntity = Resolve<IPageContentRepository>().GetPageContentByPageGuid(pageGuid);
            pageContentEntity.Heading = timerPage.Title;
            pageContentEntity.Body = timerPage.Text;
            pageContentEntity.PrimaryButtonCaption = timerPage.PrimaryButtonCaption;
            pageContentEntity.PrimaryButtonActionParameter = timerPage.PrimaryButtonAction;
            pageContentEntity.BeforeShowExpression = ReplacePageNOWithPageGUIDInExpression(timerPage.BeforeExpression, timerPage.SessionGUID);
            pageContentEntity.AfterShowExpression = ReplacePageNOWithPageGUIDInExpression(timerPage.AfterExpression, timerPage.SessionGUID);
            //pageContentEntity.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid.ToString();
            Resolve<IPageContentRepository>().Update(pageContentEntity);
            Resolve<ITranslationJobService>().UpdateElementWhenFromUpdated("PageContent", pageContentEntity.PageGUID.ToString(), Guid.Empty);
        }

        public Guid GetNextPage(Guid currentPageGUID)
        {
            Guid nextPageGuid = Guid.Empty;
            Page currentPageEntity = Resolve<IPageRepository>().GetPageByPageGuid(currentPageGUID);
            if(!currentPageEntity.PageSequenceReference.IsLoaded)
            {
                currentPageEntity.PageSequenceReference.Load();
            }

            Page nextPageEntity = Resolve<IPageRepository>().GetPageByPageSequenceAndOrderNo(currentPageEntity.PageSequence.PageSequenceGUID, currentPageEntity.PageOrderNo + 1);
            if(nextPageEntity != null)
            {
                nextPageGuid = nextPageEntity.PageGUID;
            }

            return nextPageGuid;
        }

        public Guid GetPrevioursPage(Guid currentPageGUID)
        {
            Guid previousPageGuid = Guid.Empty;
            Page currentPageEntity = Resolve<IPageRepository>().GetPageByPageGuid(currentPageGUID);
            if(!currentPageEntity.PageSequenceReference.IsLoaded)
            {
                currentPageEntity.PageSequenceReference.Load();
            }

            Page previoursPageEntity = Resolve<IPageRepository>().GetPageByPageSequenceAndOrderNo(currentPageEntity.PageSequence.PageSequenceGUID, currentPageEntity.PageOrderNo - 1);
            if(previoursPageEntity != null)
            {
                previousPageGuid = previoursPageEntity.PageGUID;
            }

            return previousPageGuid;
        }

        private int GetPageOrderNumber(Guid oldPageGuid)
        {
            Page oldPage = Resolve<IPageRepository>().GetPageByPageGuid(oldPageGuid);
            return oldPage.PageOrderNo;
        }

        public PageContentForCTPPLastPageModel getPageContentForCTPP(Guid pageGuid)
        {
            PageContentForCTPPLastPageModel EpagecontentModel = new PageContentForCTPPLastPageModel();
            PageContent pContent = Resolve<IPageContentRepository>().GetPageContentByPageGuid(pageGuid);
            if (pContent != null)
            {
                EpagecontentModel.Body = pContent.Body;
                EpagecontentModel.Heading = pContent.Heading;

                if (!pContent.Resource_BackgroundImageReference.IsLoaded)
                {
                    pContent.Resource_BackgroundImageReference.Load();
                }
                if (pContent.Resource_BackgroundImage != null)
                {
                    EpagecontentModel.PresenterImage = new ResourceModel
                    {
                        Extension = pContent.Resource_BackgroundImage.FileExtension,
                        ID = pContent.Resource_BackgroundImage.ResourceGUID,
                        Name = pContent.Resource_BackgroundImage.Name,
                        NameOnServer = pContent.Resource_BackgroundImage.NameOnServer,
                        Type = pContent.Resource_BackgroundImage.Type,
                    };
                }
                else
                {
                    EpagecontentModel.PresenterImage = null;
                }
            }
            return EpagecontentModel;
        }
        #endregion

        public SimplePageModel GetSimplePageModel(Guid pageGuid)
        {
            SimplePageModel simplePageModel = new SimplePageModel();
            Page pageEntity = Resolve<IPageRepository>().GetPageByPageGuid(pageGuid);
            if (pageEntity != null)
            {
                if (!pageEntity.PageSequenceReference.IsLoaded) pageEntity.PageSequenceReference.Load();
                if (!pageEntity.PageContentReference.IsLoaded) pageEntity.PageContentReference.Load();
                simplePageModel.PageGUID = pageEntity.PageGUID;
                simplePageModel.PageSequenceGUID = pageEntity.PageSequence.PageSequenceGUID;
                simplePageModel.PageOrderNo = pageEntity.PageOrderNo;
                simplePageModel.Wait = pageEntity.Wait;
                simplePageModel.MaxPreferences = pageEntity.MaxPreferences.HasValue ? pageEntity.MaxPreferences.Value : 0;
                simplePageModel.PageHeading = pageEntity.PageContent.Heading;
            }

            return simplePageModel;
        }
    }
}
