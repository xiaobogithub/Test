using System;
using ChangeTech.Contracts;
using ChangeTech.Models;
using Ethos.DependencyInjection;

namespace ChangeTech.DeveloperWeb
{
    public partial class ListRegisteredUser: PageBase<UserModel>
    {
        private static readonly int ONE_DAY = 1;
        private static readonly int SEVEN_DAY = 7;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRegisteredUser();
            }
        }

        private void BindRegisteredUser()
        {
            ltlRegisteredToday.Text = Resolve<IProgramUserService>().GetRegisteredUserNumber(ONE_DAY).ToString();
            ltlRegisteredOneWeek.Text = Resolve<IProgramUserService>().GetRegisteredUserNumber(SEVEN_DAY).ToString();
            System.TimeSpan TS = new System.TimeSpan(DateTime.UtcNow.Ticks - DateTime.UtcNow.AddMonths(-1).Ticks);
            ltlRegisteredOneMonth.Text = Resolve<IProgramUserService>().GetRegisteredUserNumber(TS.Days).ToString();
        }
    }
}