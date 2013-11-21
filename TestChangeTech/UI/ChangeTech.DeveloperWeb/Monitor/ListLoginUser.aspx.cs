using System;
using ChangeTech.Contracts;
using ChangeTech.Models;
using Ethos.DependencyInjection;

namespace ChangeTech.DeveloperWeb
{
    public partial class ListLoginUser : PageBase<UserModel>
    {
        private static readonly int FIVE_MINUTE = 5;
        private static readonly int ONE_DAY = 60 * 24;
        private static readonly int SEVEN_DAY = 60* 24 * 7;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindLoginUser();
            }
        }

        private void BindLoginUser()
        {
            ltlLogin5Minutes.Text = Resolve<IProgramUserService>().GetLoginUserNumber(FIVE_MINUTE).ToString();
            ltlLoginOneDay.Text = Resolve<IProgramUserService>().GetLoginUserNumber(ONE_DAY).ToString();
            ltlLoginOneWeek.Text = Resolve<IProgramUserService>().GetLoginUserNumber(SEVEN_DAY).ToString();
            System.TimeSpan TS = new System.TimeSpan(DateTime.UtcNow.Ticks - DateTime.UtcNow.AddMonths(-1).Ticks);
            ltlLoginOneMonth.Text = Resolve<IProgramUserService>().GetLoginUserNumber(ONE_DAY * TS.Days).ToString();
        }
    }
}