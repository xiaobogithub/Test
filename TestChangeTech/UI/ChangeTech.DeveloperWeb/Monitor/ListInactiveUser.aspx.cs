using System;
using ChangeTech.Contracts;
using ChangeTech.Models;
using Ethos.DependencyInjection;

namespace ChangeTech.DeveloperWeb
{
    public partial class ListInactiveUser : PageBase<UserModel>
    {
        private static readonly int ONE_DAY = 1;
        private static readonly int SEVEN_DAY = 7;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindInactiveUser();
            }
        }

        private void BindInactiveUser()
        {
            ltlInactiveOneDay.Text = Resolve<IProgramUserService>().GetInactiveUserNumber(ONE_DAY).ToString();
            ltlInactiveOneWeek.Text = Resolve<IProgramUserService>().GetInactiveUserNumber(SEVEN_DAY).ToString();
            System.TimeSpan TS = new System.TimeSpan(DateTime.UtcNow.Ticks - DateTime.UtcNow.AddMonths(-1).Ticks);
            ltlInactiveOneMonth.Text = Resolve<IProgramUserService>().GetInactiveUserNumber(TS.Days).ToString();

            //rpInactiveOneDay.DataSource = Resolve<IProgramUserService>().GetInactiveUsers(1);
            //rpInactiveOneDay.DataBind();
        }
    }
}