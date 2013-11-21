using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class StudyContentRepository : RepositoryBase, IStudyContentRepository
    {
        public void Insert(StudyContent entity)
        {
            InsertEntity(entity);
        }

        public void Delete(Guid studyContentGuid)
        {
            DeleteEntity<StudyContent>(sc => sc.StudyContentGUID == studyContentGuid, Guid.Empty);
        }

        public IQueryable<StudyContent> GetStudyContents(Guid studyGuid)
        {
            return GetEntities<StudyContent>(sc => sc.Study.StudyGUID == studyGuid);
        }

        public StudyContent Get(Guid studyContentGuid)
        {
            return GetEntities<StudyContent>(sc => sc.StudyContentGUID == studyContentGuid).FirstOrDefault();
        }

        public void Update(StudyContent entity)
        {
            UpdateEntity(entity);
        }
    }
}
