using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;
using Ethos.Utility;
using ChangeTech.Models;

namespace ChangeTech.DeveloperWeb
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://changetech.no/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GetPreviewModelXML : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                IContainerContext containerContext = ContainerManager.GetContainer("container");
                string pagePreviewXMl = string.Empty;
                Guid userGuid = Guid.Empty;
                Guid programGuid = Guid.Empty;
                Guid sessionGuid = Guid.Empty;
                Guid languageGuid = Guid.Empty;
                string ctpp = context.Request.QueryString["CTPP"];
                if (!string.IsNullOrEmpty(context.Request.QueryString[Constants.QUERYSTR_SESSION_GUID]))
                {
                    sessionGuid = new Guid(context.Request.QueryString[Constants.QUERYSTR_SESSION_GUID]);
                }
                if (!string.IsNullOrEmpty(context.Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]))
                {
                    programGuid = new Guid(context.Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]);
                }
                if (sessionGuid != Guid.Empty)
                {
                    languageGuid = containerContext.Resolve<IProgramLanguageService>().GetLanguageOfProgramBySessionGUID(sessionGuid);
                }
                if (ctpp != null)
                {
                    //Get XML 
                    pagePreviewXMl = containerContext.Resolve<ICTPPService>().GetCTPPModelAsXML(programGuid);
                }
                else
                {
                    if (context.Request.QueryString["Object"].Equals("Page"))
                    {
                        Guid pageGuid = new Guid(context.Request.QueryString[Constants.QUERYSTR_PAGE_GUID]);
                        Guid pageSequenceGuid = new Guid(context.Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_GUID]);
                        userGuid = new Guid(context.Request.QueryString[Constants.QUERYSTR_USER_GUID]);
                        pagePreviewXMl = containerContext.Resolve<IPageService>().GetPagePreviewModel(pageGuid, sessionGuid, pageSequenceGuid, userGuid);
                        pagePreviewXMl = containerContext.Resolve<IXMLService>().PraseGraphPage(pagePreviewXMl, userGuid, sessionGuid, languageGuid, false);
                    }
                    else if (context.Request.QueryString["Object"].Equals("PageSequence"))
                    {
                        sessionGuid = new Guid(context.Request.QueryString[Constants.QUERYSTR_SESSION_GUID]);
                        Guid pageSequenceGuid = new Guid(context.Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_GUID]);
                        userGuid = new Guid(context.Request.QueryString[Constants.QUERYSTR_USER_GUID]);
                        pagePreviewXMl = containerContext.Resolve<IPageSequenceService>().GetPreviewPageSequenceModelAsXML(languageGuid, sessionGuid, pageSequenceGuid, userGuid);
                        pagePreviewXMl = containerContext.Resolve<IXMLService>().PraseGraphPage(pagePreviewXMl, userGuid, sessionGuid, languageGuid, false);
                    }
                    else if (context.Request.QueryString["Object"].Equals("Relapse"))
                    {
                        //sessionGuid = new Guid(context.Request.QueryString[Constants.QUERYSTR_SESSION_GUID]);
                        Guid pageSequenceGuid = new Guid(context.Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_GUID]);
                        userGuid = new Guid(context.Request.QueryString[Constants.QUERYSTR_USER_GUID]);
                        languageGuid = containerContext.Resolve<IProgramLanguageService>().GetLanguageOfProgramByProgramGUID(programGuid);
                        pagePreviewXMl = containerContext.Resolve<IPageSequenceService>().GetRelapsePreviewModelAsXML(languageGuid, programGuid, pageSequenceGuid, userGuid);
                        sessionGuid = containerContext.Resolve<ISessionService>().GetFirstSessionGUID(programGuid);
                        pagePreviewXMl = containerContext.Resolve<IXMLService>().PraseGraphPage(pagePreviewXMl, userGuid, sessionGuid, languageGuid, false);
                        //pagePreviewXMl = containerContext.Resolve<IXMLService>().ParseBeforeAfterShowExpression(pagePreviewXMl, sessionGuid, "Session", programGuid.ToString(), userGuid.ToString());
                        //pagePreviewXMl = containerContext.Resolve<IXMLService>().ParseTimePickerQuestion(pagePreviewXMl, languageGuid);
                    }
                    else if (context.Request.QueryString["Object"].Equals("TempPageSequence"))
                    {
                        //sessionGuid = new Guid(context.Request.QueryString[Constants.QUERYSTR_SESSION_GUID]);
                        Guid pageSequenceGuid = new Guid(context.Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_GUID]);
                        userGuid = new Guid(context.Request.QueryString[Constants.QUERYSTR_USER_GUID]);
                        pagePreviewXMl = containerContext.Resolve<IPageSequenceService>().GetTempPreviewPageSequenceModelAsXML(languageGuid, sessionGuid, pageSequenceGuid, userGuid);
                        pagePreviewXMl = containerContext.Resolve<IXMLService>().PraseGraphPage(pagePreviewXMl, userGuid, sessionGuid, languageGuid, false);
                    }
                    else if (context.Request.QueryString["Object"].Equals("Session"))
                    {
                        //sessionGuid = new Guid(context.Request.QueryString[Constants.QUERYSTR_SESSION_GUID]);
                        userGuid = new Guid(context.Request.QueryString[Constants.QUERYSTR_USER_GUID]);
                        pagePreviewXMl = containerContext.Resolve<ISessionService>().GetSessionPreviewModelAsXML(languageGuid, sessionGuid, userGuid);
                        pagePreviewXMl = containerContext.Resolve<IXMLService>().PraseGraphPage(pagePreviewXMl, userGuid, sessionGuid, languageGuid, false);
                    }

                    pagePreviewXMl = containerContext.Resolve<IXMLService>().ParseBeforeAfterShowExpression(pagePreviewXMl, sessionGuid, context.Request.QueryString["Object"], programGuid.ToString(), userGuid.ToString());
                    pagePreviewXMl = containerContext.Resolve<IXMLService>().ParseTimePickerQuestion(pagePreviewXMl, languageGuid);

                }
                context.Response.ContentType = "text/xml";
                context.Response.Write(Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";" + pagePreviewXMl);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                context.Response.ContentType = "text/xml";
                context.Response.Write(ex.ToString());
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
