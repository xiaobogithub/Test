using System;
using ChangeTech.Contracts;
using ChangeTech.Models;
using Ethos.DependencyInjection;

namespace ChangeTech.DeveloperWeb
{
    public partial class ListMissedClassUser : PageBase<MissedClassUsersModel>
    {
        private static readonly int PAGE_SIZE = 15;
        private static readonly int ONE_DAY = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindMissedClassUser();
            }
        }

        private void BindMissedClassUser()
        {
            Model = Resolve<IProgramUserService>().GetMissedClassUsers(ONE_DAY, CurrentPageNumber, PAGE_SIZE);
            int numberOfProgram = Model.TotalCount;
            InitialPageInfo((int)Math.Ceiling(Convert.ToDouble(numberOfProgram) / PAGE_SIZE), "UserName", "asc");
            PagingString = Ethos.Utility.PagingSortingService.InitialPagingString(Request.Url.PathAndQuery, PageNumber, CurrentPageNumber, Resources.Share.Previous, Resources.Share.Next);
            rpMissedClassToday.DataSource = Model.MissedClassUsers;
            rpMissedClassToday.DataBind();

        }
    }
}