using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IStudyUserRepository
    {
        long Add(StudyUser entity);
        IQueryable<StudyUser> GetStudyUserBeforeCurrentID(Guid studyGuid, long sUserID);
        void Update(StudyUser entity);
        StudyUser Get(long sUserID);
    }
}
