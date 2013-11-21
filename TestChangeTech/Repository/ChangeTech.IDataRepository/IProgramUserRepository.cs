using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IProgramUserRepository
    {
        IQueryable<ProgramUser> GetProgramUserForSendMailBeforeMailtime(int mailtime);
        void Insert(ProgramUser programuser);
        IQueryable<ProgramUser> GetProgramUserListByUserGuid(Guid userGuid);
        IQueryable<ProgramUser> GetProgramUserListByProgramGuid(Guid programGuid);
        IQueryable<ProgramUser> GetProgramPermissionUserListByProgramGuid(Guid programGuid, int perission);
        IQueryable<ProgramUser> GetProgramUserByProgramGuid(Guid programGuid);
        IQueryable<ProgramUser> GetProgramUserForSendMail();
        IQueryable<ProgramUser> GetProgramUserForSendMail(int mailtime);
        IQueryable<ProgramUser> GetProgramUserByProgramAndStatus(Guid programGuid, string status);
        IQueryable<ProgramUser> GetProgramUserByProgramAndNoneStatus(Guid programGuid, string status);
        IQueryable<ProgramUser> GetProgramUserByProgramAndDay(Guid program, int? day);
        IQueryable<ProgramUser> GetProgramUserByProgramAndNullDay(Guid program);
        IQueryable<ProgramUser> GetProgramUserNotCompleteScreen(Guid program);
        IQueryable<ProgramUser> GetProgramUser(Guid programGuid);
        int GetProgramUserCountNotCompleteScreen(Guid program);
        int GetProgramUserCount(Guid programGuid);
        void Update(ProgramUser ps);
        void DeleteProgramUserByProgramGuid(Guid programGuid);
        void DeleteProgramUserByProgramGuidAndUserGuid(Guid progrmaGuid, Guid userGuid);
        void DeleteProgramUser(Guid programuserguid);
        ProgramUser GetProgramUserByProgramGuidAndUserGuid(Guid programGuid, Guid userGuid);
        ProgramUser GetProgramUserByProgramUserGuid(Guid programuserguid);
        IQueryable<ProgramUser> GetAllProgramUser();
        IQueryable<ProgramUser> GetProgramUserByProgramAndCompany(Guid programGuid, Guid companyGuid);
        ProgramUser GetProgramUserByProgramShortNameAndUserMobile(string programShortName, string userMobile);
        IQueryable<ProgramUser> GetProgramUsersForTwoPartProgram();
    }
}
