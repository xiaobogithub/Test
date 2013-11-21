using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class StudyUserRepository : RepositoryBase, IStudyUserRepository
    {
        public long Add(StudyUser entity)
        {
            InsertEntity(entity);
            return entity.StudyUserID;
        }

        public void Update(StudyUser entity)
        {
            UpdateEntity(entity);
        }

        public StudyUser Get(long sUserID)
        {
            return GetEntities<StudyUser>(s => s.StudyUserID == sUserID).FirstOrDefault();
        }

        public IQueryable<StudyUser> GetStudyUserBeforeCurrentID(Guid studyGuid, long sUserID)
        {
            return GetEntities<StudyUser>(s => s.Study.StudyGUID == studyGuid && s.StudyUserID <= sUserID);
        }
    }
}
