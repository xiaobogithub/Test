using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ethos.DependencyInjection;
using ChangeTech.Models;
using ChangeTech.Contracts;

namespace ChangeTech.DeveloperWeb
{
    public partial class EditStudy : PageBase<StudyContentModel>
    {
        private string ServerPath
        {
            get
            {
                if(ViewState["ServerPath"] == null)
                {
                    ViewState["ServerPath"] = Resolve<ISystemSettingService>().GetSettingValue("WebServerPath");
                }

                return ViewState["ServerPath"].ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindStudy();
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = "ListStudy.aspx";
            }
        }

        private void BindStudy()
        {
            StudyModel studyModel = Resolve<IStudyService>().GetStudyByGUID(new Guid(Request.QueryString[Constants.QUERYSTR_STUDY_GUID]));
            nameTextBox.Text = studyModel.Name;
            descriptionTextBox.Text = studyModel.Description;
            string studyURL = string.Format(ServerPath + "RandomStudy.aspx?{0}={1}", Constants.QUERYSTR_STUDY_SHORT_NAME, studyModel.ShortName);
            studyHyperLink.Text = studyURL;
            studyHyperLink.NavigateUrl = studyURL;
            studyRepeater.DataSource = Resolve<IStudyContentService>().GetStudyContentList(new Guid(Request.QueryString[Constants.QUERYSTR_STUDY_GUID]));
            studyRepeater.DataBind();
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("AddStudyContent.aspx?{0}={1}", Constants.QUERYSTR_STUDY_GUID, Request.QueryString[Constants.QUERYSTR_STUDY_GUID]));
        }

        protected void updateButton_Click(object sender, EventArgs e)
        {
            StudyModel sModel = new StudyModel
            {
                Description = descriptionTextBox.Text.Trim(),
                Name = nameTextBox.Text.Trim(),
                StudyGUID = new Guid(Request.QueryString[Constants.QUERYSTR_STUDY_GUID])
            };
            Resolve<IStudyService>().UpdateStudy(sModel);

            Response.Redirect(Request.Url.PathAndQuery);
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListStudy.aspx");
        }

        protected void deleteButton_Click(object sender, EventArgs e)
        {
            Guid studyContentGuid = new Guid(((Button)sender).CommandArgument);
            Resolve<IStudyContentService>().DeleteStudyContentByGUID(studyContentGuid);

            Response.Redirect(Request.Url.PathAndQuery);
        }

        protected void editButton_Click(object sender, EventArgs e)
        {
            string studyContentGuid = ((Button)sender).CommandArgument;
            Response.Redirect(string.Format("EditStudyContent.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_STUDY_GUID, Request.QueryString[Constants.QUERYSTR_STUDY_GUID], Constants.QUERYSTR_STUDY_CONTENT_GUID, studyContentGuid));
        }

        //protected void studyRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    if(e.Item.DataItem != null)
        //    {
        //        HyperLink link = e.Item.FindControl("urlHyperLink") as HyperLink;
        //        string url = string.Format(ServerPath + "ChangeTech.html?Mode=Trial&{0}={1}", Constants.QUERYSTR_PROGRAM_CODE, (e.Item.DataItem as StudyContentModel));

        //        link.NavigateUrl = url;
        //        link.Text = url;
        //    }
        //}
    }
}
