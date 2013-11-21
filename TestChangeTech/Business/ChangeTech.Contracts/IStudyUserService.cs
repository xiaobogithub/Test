using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;

namespace ChangeTech.Contracts
{
    public interface IStudyUserService
    {
        long AddStudyUser(StudyUserModel model);
        //void UpdateStudyUser(Guid userGuid, long sUserID);
    }
}
