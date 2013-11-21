using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;
using ChangeTech.Models;
using ChangeTech.IDataRepository;

namespace ChangeTech.Services
{
    public class StudyUserService : ServiceBase, IStudyUserService
    {
        public long AddStudyUser(StudyUserModel model)
        {
            StudyUser sUser = new StudyUser();
            sUser.Study = Resolve<IStudyRepository>().GetStudyByGuid(model.StudyGUID);
            return Resolve<IStudyUserRepository>().Add(sUser);
        }

        //public void UpdateStudyUser(Guid userGuid, long sUserID)
        //{
        //    StudyUser sUser = Resolve<IStudyUserRepository>().Get(sUserID);
        //    sUser.User = Resolve<IUserRepository>().GetUserByGuid(userGuid);

        //    Resolve<IStudyUserRepository>().Update(sUser);
        //}
    }
}
