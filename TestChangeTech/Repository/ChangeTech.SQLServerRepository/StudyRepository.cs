using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class StudyRepository : RepositoryBase, IStudyRepository
    {
        public IQueryable<Study> GetAllStudy()
        {
            return GetEntities<Study>().OrderBy(s => s.Name);
        }

        public Study GetStudyByShortName(string shortName)
        {
            return GetEntities<Study>(s => s.ShortName == shortName).FirstOrDefault();
        }

        public Study GetStudyByGuid(Guid studyGuid)
        {
            return GetEntities<Study>(s => s.StudyGUID == studyGuid).FirstOrDefault();
        }

        public void Insert(Study study)
        {
            InsertEntity(study);
        }

        public void Delete(Guid studyGuid)
        {
            DeleteEntity<Study>(s => s.StudyGUID == studyGuid, Guid.Empty);
        }

        public void Update(Study entity)
        {
            UpdateEntity(entity);
        }
    }
}
