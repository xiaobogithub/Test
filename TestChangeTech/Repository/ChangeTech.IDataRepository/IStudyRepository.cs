using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IStudyRepository
    {
        IQueryable<Study> GetAllStudy();
        Study GetStudyByShortName(string shortName);
        Study GetStudyByGuid(Guid studyGuid);

        void Insert(Study study);
        void Delete(Guid studyGuid);
        void Update(Study entity);
    }
}
