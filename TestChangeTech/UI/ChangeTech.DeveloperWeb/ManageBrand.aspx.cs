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
    public partial class ManageBrand : PageBase<BrandModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindBrandList();
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = "Home.aspx";
            }
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddBrand.aspx");
        }

        protected void editButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("EditBrand.aspx?{0}={1}", Constants.QUERYSTR_BRAND_GUID, ((Button)sender).CommandArgument));
        }

        protected void deleteButton_Click(object sender, EventArgs e)
        {
            Guid brandGuid = new Guid(((Button)sender).CommandArgument);
            Resolve<IBrandService>().DeleteBrand(brandGuid);

            Response.Redirect(Request.Url.PathAndQuery);
        }

        private void BindBrandList()
        {
            brandRepeater.DataSource = Resolve<IBrandService>().GetBrandList();
            brandRepeater.DataBind();
        }
    }
}