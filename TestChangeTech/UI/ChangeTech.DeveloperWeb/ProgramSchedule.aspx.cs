using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ethos.DependencyInjection;
using ChangeTech.Models;
using ChangeTech.Services;
using ChangeTech.Contracts;

namespace ChangeTech.DeveloperWeb
{
    public partial class ProgramSchedule : PageBase<EditProgramModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]))
            {
                if (!IsPostBack)
                {
                    //InitialProgramDropDownList();
                    InitialPage();
                    BindListView();
                   ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format(string.Format("EditProgram.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
                }
            }
            else
            {
                Response.Redirect("InvalidUrl.aspx");
            }
        }

        private void InitialPage()
        {
            ProgramModel program = Resolve<IProgramService>().GetProgramByGUID(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
            programNameLabel.Text = program.ProgramName;
        }

        private void BindListView()
        {
            //ProgramModel program = Resolve<IProgramService>().GetProgramByProgramGUIDAndLanguageGUID(new Guid(programDropDownList.SelectedValue), new Guid(languageDropDownList.SelectedValue));
            //ViewState[Constants.QUERYSTR_PROGRAM_GUID] = program.Guid;
            //if (program != null)
            //{
            scheduleListView.DataSource = Resolve<IProgramScheduleService>().GetProgramScheduleByProgram(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
            scheduleListView.DataBind();
            //}
        }

        //private void InitialProgramDropDownList()
        //{
        //    programDropDownList.DataSource = Resolve<IProgramService>().GetSimpleProgramsModel();
        //    programDropDownList.DataTextField = "ProgramName";
        //    programDropDownList.DataValueField = "ProgramGuid";
        //    programDropDownList.DataBind();
        //    BindLanguageDropDown();
        //}

        //private void BindLanguageDropDown()
        //{
        //    if (!string.IsNullOrEmpty(programDropDownList.SelectedValue))
        //    {
        //        languageDropDownList.DataSource = Resolve<IProgramLanguageService>().GetLanguagesSupportByProgram(new Guid(programDropDownList.SelectedValue));
        //        languageDropDownList.DataTextField = "Name";
        //        languageDropDownList.DataValueField = "LanguageGUID";
        //        languageDropDownList.DataBind();
        //    }
        //}

        protected void scheduleListView_ItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            scheduleListView.EditIndex = -1;
            BindListView();
        }

        protected void scheduleListView_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
            ProgramScheduleModel scheduleModel = new ProgramScheduleModel();
            scheduleModel.week = Convert.ToInt32(((TextBox)e.Item.FindControl("weekTextBox")).Text);
            scheduleModel.monday = ((CheckBox)e.Item.FindControl("monCheckBox")).Checked;
            scheduleModel.tuesday = ((CheckBox)e.Item.FindControl("tueCheckBox")).Checked;
            scheduleModel.wednesday = ((CheckBox)e.Item.FindControl("wedCheckBox")).Checked;
            scheduleModel.thursday = ((CheckBox)e.Item.FindControl("thursCheckBox")).Checked;
            scheduleModel.friday = ((CheckBox)e.Item.FindControl("friCheckBox")).Checked;
            scheduleModel.saterday = ((CheckBox)e.Item.FindControl("satCheckBox")).Checked;
            scheduleModel.sunday = ((CheckBox)e.Item.FindControl("sunCheckBox")).Checked;

            Resolve<IProgramScheduleService>().SaveProgramSchedule(scheduleModel, new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
            scheduleListView.InsertItemPosition = InsertItemPosition.None;
            BindListView();
        }

        protected void scheduleListView_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            ProgramScheduleModel scheduleModel = new ProgramScheduleModel();
            scheduleModel.week = Convert.ToInt32(((TextBox)scheduleListView.EditItem.FindControl("weekTextBox")).Text);
            scheduleModel.monday = ((CheckBox)scheduleListView.EditItem.FindControl("monCheckBox")).Checked;
            scheduleModel.tuesday = ((CheckBox)scheduleListView.EditItem.FindControl("tueCheckBox")).Checked;
            scheduleModel.wednesday = ((CheckBox)scheduleListView.EditItem.FindControl("wedCheckBox")).Checked;
            scheduleModel.thursday = ((CheckBox)scheduleListView.EditItem.FindControl("thursCheckBox")).Checked;
            scheduleModel.friday = ((CheckBox)scheduleListView.EditItem.FindControl("friCheckBox")).Checked;
            scheduleModel.saterday = ((CheckBox)scheduleListView.EditItem.FindControl("satCheckBox")).Checked;
            scheduleModel.sunday = ((CheckBox)scheduleListView.EditItem.FindControl("sunCheckBox")).Checked;

            Resolve<IProgramScheduleService>().SaveProgramSchedule(scheduleModel, new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
            scheduleListView.EditIndex = -1;
            BindListView();
        }

        protected void scheduleListView_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            scheduleListView.EditIndex = e.NewEditIndex;
            scheduleListView.InsertItemPosition = InsertItemPosition.None;
            BindListView();
            int week = Convert.ToInt32(scheduleListView.DataKeys[e.NewEditIndex].Value.ToString());
            ProgramScheduleModel scheduleModel = Resolve<IProgramScheduleService>().GetProgramScheduleByProgramAndWeek(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]), week);
            ((TextBox)scheduleListView.EditItem.FindControl("weekTextBox")).Text = scheduleModel.week.ToString();
            ((CheckBox)scheduleListView.EditItem.FindControl("monCheckBox")).Checked = scheduleModel.monday;
            ((CheckBox)scheduleListView.EditItem.FindControl("tueCheckBox")).Checked = scheduleModel.tuesday;
            ((CheckBox)scheduleListView.EditItem.FindControl("wedCheckBox")).Checked = scheduleModel.wednesday;
            ((CheckBox)scheduleListView.EditItem.FindControl("thursCheckBox")).Checked = scheduleModel.thursday;
            ((CheckBox)scheduleListView.EditItem.FindControl("friCheckBox")).Checked = scheduleModel.friday;
            ((CheckBox)scheduleListView.EditItem.FindControl("satCheckBox")).Checked = scheduleModel.saterday;
            ((CheckBox)scheduleListView.EditItem.FindControl("sunCheckBox")).Checked = scheduleModel.sunday;
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            scheduleListView.InsertItemPosition = InsertItemPosition.LastItem;
            BindListView();
        }

        protected void insertCancelButton_Click(object sender, EventArgs e)
        {
            scheduleListView.InsertItemPosition = InsertItemPosition.None;
            BindListView();
        }

        protected void deleteButton_Click(object sender, EventArgs e)
        {
            int week = Convert.ToInt32(((Button)sender).CommandArgument);
            Resolve<IProgramService>().DeleteProgramScheduleByProgramAndWeek(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]), week);
            BindListView();
        }

        //protected void programDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    BindLanguageDropDown();
        //    BindListView();
        //}

        //protected void languageDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    BindListView();
        //}
    }
}
