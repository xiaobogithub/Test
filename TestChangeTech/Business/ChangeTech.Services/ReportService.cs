using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using ChangeTech.Contracts;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;
using ChangeTech.Models;
using Ethos.DependencyInjection;

namespace ChangeTech.Services
{
    public class ReportService : ServiceBase, IReportService
    {
        public DataTable GetActivityLogReport(string emaillistStr, string fileds, string days, string programGuid)
        {
            return Resolve<IActivityLogRepository>().GetActivityLogReport(emaillistStr, fileds, days, programGuid);
        }

        public List<ProgramUserReportModel> GetProgramUserReport()
        {
            List<ProgramUserReportModel> programUserReportList = new List<ProgramUserReportModel>();
            DataTable programUserReportTable = Resolve<IStoreProcedure>().GetProgramUserReport();
            for (int i = 0; i < programUserReportTable.Rows.Count; i++)
            {
                ProgramUserReportModel model = new ProgramUserReportModel();
                string logoName = programUserReportTable.Rows[i]["NameOnServer"].ToString();
                string accountName = Resolve<ISystemSettingService>().GetSettingValue("AzureStorageAccountName");
                string bolbPath = ServiceUtility.GetBlobPath(accountName);
                if (!string.IsNullOrEmpty(logoName))
                {
                    model.ProgramLogoUrl = bolbPath + BlobContainerTypeEnum.LogoContainer.ToString().ToLower() + "/" + logoName;
                }
                model.ProgramName = programUserReportTable.Rows[i]["Name"].ToString();
                model.AllUser = int.Parse(programUserReportTable.Rows[i]["AllUserSV"].ToString());
                model.NotCompleteScreening = int.Parse(programUserReportTable.Rows[i]["NotCompleteSCR"].ToString());
                model.CompleteScreening = int.Parse(programUserReportTable.Rows[i]["CompleteSCR"].ToString());
                model.RegisteredUser = int.Parse(programUserReportTable.Rows[i]["Registered"].ToString());
                model.UsersInProgramme = int.Parse(programUserReportTable.Rows[i]["UsersInProg"].ToString());
                model.CompleteUser = int.Parse(programUserReportTable.Rows[i]["Completed"].ToString());
                model.TerminateUser = int.Parse(programUserReportTable.Rows[i]["Terminated"].ToString());
                model.RegisteredLast24Hours = int.Parse(programUserReportTable.Rows[i]["RegisteredLast24Hours"].ToString());
                model.RegisteredLastWeek = int.Parse(programUserReportTable.Rows[i]["RegisteredLastWeek"].ToString());
                model.RegisteredLastMonth = int.Parse(programUserReportTable.Rows[i]["RegisteredLastMonth"].ToString());
                programUserReportList.Add(model);
            }
            return programUserReportList;
        }

        public ReportModel GetFormsData(Guid programGuid, Guid usergroupGuid, string gender, bool containOnCompany)
        {
            ReportModel model = new ReportModel();
            model.AllUser = GetAllProgramUserNumber(programGuid, usergroupGuid, gender, containOnCompany);
            model.ActiveUser = GetAcitveUserNumber(programGuid, usergroupGuid, gender, containOnCompany);
            model.CompleteScreening = GetCompleteScreeningNumber(programGuid, usergroupGuid, gender, containOnCompany);
            model.CompleteUser = GetCompleteUserNumber(programGuid, usergroupGuid, gender, containOnCompany);
            model.InActiveUser = GetInactiveUserNumber(programGuid, usergroupGuid, gender, containOnCompany);
            model.NotCompleteScreening = GetNotCompleteScreeningUserNumber(programGuid, usergroupGuid, gender, containOnCompany);
            model.RegisteredUser = GetRegisteredUserNumber(programGuid, usergroupGuid, gender, containOnCompany);
            model.TerminateUser = GetTerminateUserNumber(programGuid, usergroupGuid, gender, containOnCompany);
            model.PauseUser = GetPauseUserNumber(programGuid, usergroupGuid, gender, containOnCompany);
            //model.UsersInProgramme = GetUserInProgramNumber(programGuid, usergroupGuid, gender, containOnCompany);
            //for dtd-1389, UsersInProgramme = RegisteredUser + ActiveUser + TerminateUser + CompleteUser + PausedUser. 
            model.UsersInProgramme = model.RegisteredUser + model.ActiveUser + model.TerminateUser + model.CompleteUser + model.PauseUser;
            return model;
        }

        public List<ReportItem> GetReportItems(Guid programGuid, Guid usergroupGuid, string gender, bool containOnCompany)
        {
            ReportModel model = GetFormsData(programGuid, usergroupGuid, gender, containOnCompany);
            Type type = model.GetType();
            List<ReportItem> items = new List<ReportItem>();
            foreach(PropertyInfo pi in type.GetProperties())
            {
                object value = pi.GetValue(model, null);
                string name = pi.Name;
                ReportItem item = new ReportItem
                {
                    Name = name,
                    Value = Convert.ToInt32(value)
                };
                items.Add(item);
            }

            return items;
        }

        private int GetUserInProgramNumber(Guid programGuid, Guid companyGuid, string gender, bool containOnCompany)
        {
            IQueryable<ProgramUser> puList = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuid(programGuid).Where(p => p.User.UserType == (int)ChangeTech.Models.UserTypeEnum.User && !p.User.Email.StartsWith("ChangeTechTemp"));
            if(containOnCompany == true)
            {
                if(companyGuid != Guid.Empty)
                {
                    puList = puList.Where(p => p.Company.CompanyGUID == companyGuid);
                }
            }
            else
            {
                puList = puList.Where(p => p.Company == null);
            }

            if(!string.IsNullOrEmpty(gender))
            {
                puList = puList.Where(p => p.User.Gender == gender);
            }

            return puList.Count();
        }

        private int GetTerminateUserNumber(Guid programGuid, Guid usergroupGuid, string gender, bool containOnCompany)
        {
            IQueryable<ProgramUser> puList = Resolve<IProgramUserRepository>().GetProgramUserByProgramAndStatus(programGuid, ProgramUserStatusEnum.Terminated.ToString());

            if(containOnCompany == true)
            {
                if(usergroupGuid != Guid.Empty)
                {
                    puList = puList.Where(p => p.Company.CompanyGUID == usergroupGuid);
                }
            }
            else
            {
                puList = puList.Where(p => p.Company == null);
            }

            if(!string.IsNullOrEmpty(gender))
            {
                puList = puList.Where(p => p.User.Gender == gender);
            }

            return puList.Count();
        }

        private int GetRegisteredUserNumber(Guid programGuid, Guid usergroupGuid, string gender, bool containOnCompany)
        {
            IQueryable<ProgramUser> puList = Resolve<IProgramUserRepository>().GetProgramUserByProgramAndStatus(programGuid, ProgramUserStatusEnum.Registered.ToString());
            if(containOnCompany == true)
            {
                if(usergroupGuid != Guid.Empty)
                {
                    puList = puList.Where(p => p.Company.CompanyGUID == usergroupGuid);
                }
            }
            else
            {
                puList = puList.Where(p => p.Company == null);
            }

            if(!string.IsNullOrEmpty(gender))
            {
                puList = puList.Where(p => p.User.Gender == gender);
            }

            return puList.Count();
        }

        private int GetNotCompleteScreeningUserNumber(Guid programGuid, Guid usergroupGuid, string gender, bool containOnCompany)
        {
            IQueryable<ProgramUser> puList = Resolve<IProgramUserRepository>().GetProgramUserNotCompleteScreen(programGuid);
            if(containOnCompany == true)
            {
                if(usergroupGuid != Guid.Empty)
                {
                    puList = puList.Where(p => p.Company.CompanyGUID == usergroupGuid);
                }
            }
            else
            {
                puList = puList.Where(p => p.Company == null);
            }

            if(!string.IsNullOrEmpty(gender))
            {
                puList = puList.Where(p => p.User.Gender == gender);
            }
            return puList.Count();
        }

        private int GetInactiveUserNumber(Guid programGuid, Guid usergroupGuid, string gender, bool containOnCompany)
        {
            return GetAllProgramUserNumber(programGuid, usergroupGuid, gender, containOnCompany) - GetAcitveUserNumber(programGuid, usergroupGuid, gender, containOnCompany);
        }

        private int GetCompleteUserNumber(Guid programGuid, Guid usergroupGuid, string gender, bool containOnCompany)
        {
            IQueryable<ProgramUser> puList = Resolve<IProgramUserRepository>().GetProgramUserByProgramAndStatus(programGuid, ProgramUserStatusEnum.Completed.ToString());
            if(containOnCompany == true)
            {
                if(usergroupGuid != Guid.Empty)
                {
                    puList = puList.Where(p => p.Company.CompanyGUID == usergroupGuid);
                }
            }
            else
            {
                puList = puList.Where(p => p.Company == null);
            }

            if(!string.IsNullOrEmpty(gender))
            {
                puList = puList.Where(p => p.User.Gender == gender);
            }

            return puList.Count();
        }

        private int GetCompleteScreeningNumber(Guid programGuid, Guid usergroupGuid, string gender, bool containOnCompany)
        {
            return GetAllProgramUserNumber(programGuid, usergroupGuid, gender, containOnCompany) - GetNotCompleteScreeningUserNumber(programGuid, usergroupGuid, gender, containOnCompany);
        }

        private int GetAcitveUserNumber(Guid programGuid, Guid usergroupGuid, string gender, bool containOnCompany)
        {
            IQueryable<ProgramUser> puList = Resolve<IProgramUserRepository>().GetProgramUserByProgramAndStatus(programGuid, ProgramUserStatusEnum.Active.ToString());
            if(containOnCompany == true)
            {
                if(usergroupGuid != Guid.Empty)
                {
                    puList = puList.Where(p => p.Company.CompanyGUID == usergroupGuid);
                }
            }
            else
            {
                puList = puList.Where(p => p.Company == null);
            }

            if(!string.IsNullOrEmpty(gender))
            {
                puList = puList.Where(p => p.User.Gender == gender);
            }
            return puList.Count();
        }

        private int GetPauseUserNumber(Guid programGuid, Guid usergroupGuid, string gender, bool containOnCompany)
        {
            IQueryable<ProgramUser> puList = Resolve<IProgramUserRepository>().GetProgramUserByProgramAndStatus(programGuid, ProgramUserStatusEnum.Paused.ToString());
            if (containOnCompany == true)
            {
                if (usergroupGuid != Guid.Empty)
                {
                    puList = puList.Where(p => p.Company.CompanyGUID == usergroupGuid);
                }
            }
            else
            {
                puList = puList.Where(p => p.Company == null);
            }

            if (!string.IsNullOrEmpty(gender))
            {
                puList = puList.Where(p => p.User.Gender == gender);
            }
            return puList.Count();
        }

        private int GetAllProgramUserNumber(Guid programGuid, Guid usergroupGuid, string gender, bool containOnCompany)
        {
            IQueryable<ProgramUser> puList = Resolve<IProgramUserRepository>().GetProgramUser(programGuid);
            if(containOnCompany == true)
            {
                if(usergroupGuid != Guid.Empty)
                {
                    puList = puList.Where(p => p.Company.CompanyGUID == usergroupGuid);
                }
            }
            else
            {
                puList = puList.Where(p => p.Company == null);
            }

            if(!string.IsNullOrEmpty(gender))
            {
                puList = puList.Where(p => p.User.Gender == gender);
            }

            return puList.Count();
        }
    }
}
