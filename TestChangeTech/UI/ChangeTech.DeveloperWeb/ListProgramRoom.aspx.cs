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
    public partial class ListProgramRoom : PageBase<ProgramRoomsModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID] != null)
            {
                if (!IsPostBack)
                {
                    try
                    {
                        FormatRepeater();
                        ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format("~/EditProgram.aspx?{0}={1}",Constants.QUERYSTR_PROGRAM_GUID,Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]);
                    }
                    catch (Exception ex)
                    {
                        LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                        throw ex;
                    }
                }
            }
            else
            {
                Response.Redirect("InvalidUrl.aspx");
            }
        }

        private void FormatRepeater()
        {
            int programSecuirty = ContextService.CurrentAccount.Security;
                // program security
                if (Resolve<IUserService>().HasPermission((PermissionEnum)programSecuirty, PermissionEnum.ApplicationCreate, (PermissionEnum)programSecuirty))
                {
                    Button addProgramRoom = (Button)Master.FindControl("ContentPlaceHolder1").FindControl("btnAdd");
                    addProgramRoom.Enabled = true;
                }
            Model = Resolve<IProgramRoomService>().GetRoomByProgram(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));

            InitialPageInfo((int)Math.Ceiling(Convert.ToDouble(Model.Count) / PageSize), "Name", "asc");

            string url = Request.Url.ToStringWithoutPort();
            string[] header = { (string)Resources.Share.Name, (string)Resources.Share.Description, (string)Resources.Share.PredictorCategory };
            string[] sortExpression = { "Name", "Description" };

            PagingString = Ethos.Utility.PagingSortingService.InitialPagingString(Request.Url.ToStringWithoutPort(), PageNumber, CurrentPageNumber, Resources.Share.Previous, Resources.Share.Next);

            HeaderString = Ethos.Utility.PagingSortingService.InitialSortingString(Request.Url.ToStringWithoutPort(), FittingSortExpression(sortExpression), header);

            rpRoom.DataSource = Ethos.Utility.PagingSortingService.GetCurrentPage<ProgramRoomModel>(Model, CurrentPageNumber, PageNumber, PageSize, SortExpression + " " + SortOrder);
            rpRoom.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("~/AddProgramRoom.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID,Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("~/EditProgramRoom.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_ROOM_GUID,((Button)sender).CommandArgument,Constants.QUERYSTR_PROGRAM_GUID,Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Guid ProgramRoomGuid = new Guid(((Button)sender).CommandArgument);
                if (Resolve<IProgramRoomService>().CanDelete(ProgramRoomGuid))
                {
                    Resolve<IProgramRoomService>().Delete(ProgramRoomGuid);
                    FormatRepeater();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('The program room is referenced by program!');", true);
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }  
        }

        protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FormatRepeater();
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }  
        }

        protected void rpRoom_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            int programSecuirty = ContextService.CurrentAccount.Security;
            if (e.Item.ItemType == ListItemType.Header)
            {
                //// program security
                //if (Resolve<IUserService>().HasPermission((PermissionEnum)programSecuirty, PermissionEnum.ApplicationCreate, (PermissionEnum)programSecuirty))
                //{
                //    ((Button)e.Item.FindControl("btnAdd")).Enabled = true;
                //}
            }
            if (e.Item.DataItem != null)
            {
                // program security

                if (Resolve<IUserService>().HasPermission((PermissionEnum)programSecuirty, PermissionEnum.ApplicationDelete, (PermissionEnum)programSecuirty))
                {
                    ((Button)e.Item.FindControl("btnDelete")).Enabled = true;
                }
                if (Resolve<IUserService>().HasPermission((PermissionEnum)programSecuirty, PermissionEnum.ApplicationEdit, (PermissionEnum)programSecuirty))
                {
                    ((Button)e.Item.FindControl("btnEdit")).Enabled = true;
                }
            }
        }
    }
}
