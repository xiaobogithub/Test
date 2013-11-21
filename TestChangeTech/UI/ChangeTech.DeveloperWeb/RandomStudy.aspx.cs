using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ethos.DependencyInjection;
using ChangeTech.Models;
using ChangeTech.Contracts;
using Ethos.Utility;

namespace ChangeTech.DeveloperWeb
{
    public partial class RandomStudy : PageBase<StudyModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Guid studyGuid = Resolve<IStudyService>().GetStudyGUIDByShortName(Request.QueryString[Constants.QUERYSTR_STUDY_SHORT_NAME]);

                //long studyUserID = Resolve<IStudyUserService>().AddStudyUser(new StudyUserModel { StudyGUID = studyGuid });
                //StudyContentModel scModel = Resolve<IStudyService>().RandomProgram(studyGuid, studyUserID);
                StudyContentModel scModel = Resolve<IStudyService>().RandomProgram(Request.QueryString[Constants.QUERYSTR_STUDY_SHORT_NAME]);
                Response.Redirect(IntegrateURL(scModel));
            }
            catch(Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private string IntegrateURL(StudyContentModel scModel)
        {
            string url = string.Empty;
            if(scModel.RouteURL.Contains("?"))
            {
                url = string.Format(scModel.RouteURL + "&{0}={1}", Constants.QUERYSTR_STUDY_CONTENT_GUID, scModel.StudContentGUID);
            }
            else
            {
                url = string.Format(scModel.RouteURL + "?{0}={1}", Constants.QUERYSTR_STUDY_CONTENT_GUID, scModel.StudContentGUID);
            }

            return url;
        }
    }
}
