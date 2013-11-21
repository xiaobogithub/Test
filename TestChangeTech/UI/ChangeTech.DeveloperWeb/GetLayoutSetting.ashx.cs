using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using Ethos.DependencyInjection;
using ChangeTech.Contracts;
using Ethos.Utility;
using System.Xml;
namespace ChangeTech.DeveloperWeb
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GetLayoutSetting : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string xmlSetting = string.Empty;
                IContainerContext containerContext = ContainerManager.GetContainer("container");

                string programGuid = context.Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID];

                if(string.IsNullOrEmpty(programGuid)&&!string.IsNullOrEmpty(context.Request.QueryString[Constants.QUERYSTR_PROGRAM_CODE]))
                {
                    programGuid = containerContext.Resolve<IProgramService>().GetProgramGUIDByProgramCode(context.Request.QueryString[Constants.QUERYSTR_PROGRAM_CODE]).ToString();
                }

                if(!string.IsNullOrEmpty(programGuid))
                {
                    xmlSetting = containerContext.Resolve<IProgramService>().GetProgramSetting(new Guid(programGuid));
                }
                else if(context.Request.QueryString[Constants.QUERYSTR_SESSION_GUID] != null)
                {
                    xmlSetting = containerContext.Resolve<IProgramService>().GetProgramSettingBySessinGUID(new Guid(context.Request.QueryString[Constants.QUERYSTR_SESSION_GUID]));
                }
                //XmlDocument doc = new XmlDocument();
                //doc.LoadXml(xmlSetting);
                context.Response.ContentType = "text/plain";
                context.Response.Write(xmlSetting);
            }
            catch(Exception ex)
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
