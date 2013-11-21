using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.IDataRepository;
using ChangeTech.Entities;

namespace ChangeTech.SQLServerRepository
{
    public class ProgramUserRepository : RepositoryBase, IProgramUserRepository
    {
        #region IProgramPermissionRepository Members
        public IQueryable<ProgramUser> GetProgramUserForSendMailBeforeMailtime(int mailtime)
        {
            return GetEntities<ProgramUser>(p => p.MailTime <= mailtime && !p.User.Email.StartsWith("ChangeTechTemp") && p.User.UserType.Value != 3);//3 = Tester
        }
        public void Insert(ProgramUser ps)
        {
            InsertEntity(ps);
        }

        public IQueryable<ProgramUser> GetProgramUserListByUserGuid(Guid userGuid)
        {

            return GetEntities<ProgramUser>(p => p.User.UserGUID == userGuid);
        }

        public IQueryable<ProgramUser> GetProgramUserListByProgramGuid(Guid programGuid)
        {
            return GetEntities<ProgramUser>(p => p.Program.ProgramGUID == programGuid && !p.User.Email.Contains("ChangeTechTemp") && (p.User.UserType == 1 || p.User.UserType == 3));
        }

        public IQueryable<ProgramUser> GetProgramUserByProgramGuid(Guid programGuid)
        {
            return GetEntities<ProgramUser>(p => p.Program.ProgramGUID == programGuid && !p.User.Email.Contains("ChangeTechTemp"));
        }

        public int GetProgramUserCount(Guid programGuid)
        {
            return GetEntities<ProgramUser>(p => p.Program.ProgramGUID == programGuid).Count();
        }

        public IQueryable<ProgramUser> GetProgramUser(Guid programGuid)
        {
            return GetEntities<ProgramUser>(p => p.Program.ProgramGUID == programGuid);
        }

        public IQueryable<ProgramUser> GetProgramUserForSendMail()
        {
            //DateTime date = DateTime.UtcNow.Date.AddDays(-1);
            //return GetEntities<ProgramUser>(p => p.MailTime.Value == DateTime.UtcNow.Hour);
            return GetEntities<ProgramUser>(pu => !pu.User.Email.StartsWith("ChangeTechTemp") && pu.User.UserType.Value != 3);
        }

        public IQueryable<ProgramUser> GetProgramUserByProgramAndStatus(Guid programGuid, string status)
        {
            return GetEntities<ProgramUser>(p => p.Status == status && p.Program.ProgramGUID == programGuid);
        }

        public IQueryable<ProgramUser> GetProgramUserByProgramAndDay(Guid program, int? day)
        {
            return GetEntities<ProgramUser>(p => p.Program.ProgramGUID == program && p.Day == day);
        }

        public IQueryable<ProgramUser> GetProgramUserByProgramAndNullDay(Guid programGuid)
        {
            return GetEntities<ProgramUser>(p => p.Program.ProgramGUID == programGuid && !p.Day.HasValue);
        }

        public IQueryable<ProgramUser> GetProgramUserByProgramAndNoneStatus(Guid programGuid, string status)
        {
            return GetEntities<ProgramUser>(p => p.Program.ProgramGUID == programGuid && p.Status != status);
        }

        public void Update(ProgramUser ps)
        {
            UpdateEntity(ps);
        }

        public IQueryable<ProgramUser> GetProgramPermissionUserListByProgramGuid(Guid programGuid, int permission)
        {
            return GetEntities<ProgramUser>(p => p.Program.ProgramGUID == programGuid && p.Security == permission);
        }

        public void DeleteProgramUserByProgramGuid(Guid programGuid)
        {
            DeleteEntity<ProgramUser>(p => p.Program.ProgramGUID == programGuid, Guid.Empty);
        }

        public ProgramUser GetProgramUserByProgramGuidAndUserGuid(Guid programGuid, Guid userGuid)
        {
            return GetEntities<ProgramUser>(p => p.Program.ProgramGUID == programGuid && p.User.UserGUID == userGuid).FirstOrDefault();
        }

        public void DeleteProgramUserByProgramGuidAndUserGuid(Guid progrmaGuid, Guid userGuid)
        {
            DeleteEntity<ProgramUser>(p => p.Program.ProgramGUID == progrmaGuid && p.User.UserGUID == userGuid, Guid.Empty);
        }

        public IQueryable<ProgramUser> GetProgramUserListByProgramGuid(Guid programGuid, string email)
        {
            return GetEntities<ProgramUser>(p => p.Program.ProgramGUID == programGuid && p.User.Email.Contains(email) && (p.User.UserType == 1 || p.User.UserType == 3));
        }

        public IQueryable<ProgramUser> GetProgramUserForSendMail(int mailtime)
        {
            return GetEntities<ProgramUser>(p => p.MailTime == mailtime && !p.User.Email.StartsWith("ChangeTechTemp") && p.User.UserType.Value != 3);
        }

        public ProgramUser GetProgramUserByProgramUserGuid(Guid programuserguid)
        {
            return GetEntities<ProgramUser>(p => p.ProgramUserGUID == programuserguid).FirstOrDefault();
        }

        public void DeleteProgramUser(Guid programuserguid)
        {
            DeleteEntity<ProgramUser>(p => p.ProgramUserGUID == programuserguid, Guid.Empty);
        }

        public int GetProgramUserCountNotCompleteScreen(Guid program)
        {
            return GetEntities<ProgramUser>(p => p.Program.ProgramGUID == program && p.User.Email.StartsWith("ChangeTechTemp")).Count();
        }

        public IQueryable<ProgramUser> GetProgramUserNotCompleteScreen(Guid program)
        {
            return GetEntities<ProgramUser>(p => p.Program.ProgramGUID == program && p.User.Email.StartsWith("ChangeTechTemp"));
        }

        public IQueryable<ProgramUser> GetAllProgramUser()
        {
            return GetEntities<ProgramUser>();
        }

        public IQueryable<ProgramUser> GetProgramUserByProgramAndCompany(Guid programGuid, Guid companyGuid)
        {
            return GetEntities<ProgramUser>().Where(pu => pu.Program.ProgramGUID == programGuid && pu.Company.CompanyGUID == companyGuid);
        }

        public ProgramUser GetProgramUserByProgramShortNameAndUserMobile(string programShortName, string userMobile)
        {
            return GetEntities<ProgramUser>(pu => pu.Program.ShortName.Trim().ToLower().Equals(programShortName.ToLower()) && pu.User.MobilePhone.Trim().Equals(userMobile) && pu.User.UserType == 1 && (!pu.User.IsDeleted.HasValue || pu.User.IsDeleted.HasValue && pu.User.IsDeleted != true)).FirstOrDefault();
        }

        public IQueryable<ProgramUser> GetProgramUsersForTwoPartProgram()
        {
            return GetEntities<ProgramUser>(pu => (pu.Program.IsContainTwoParts == true) && (pu.Status == "Active") && (!pu.SwitchMessageTime.HasValue) && (!pu.User.IsDeleted.HasValue || pu.User.IsDeleted.HasValue && pu.User.IsDeleted.Value == false));
        }
        #endregion
    }
}
