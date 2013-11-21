using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IStudyContentRepository
    {
        void Insert(StudyContent entity);
        void Delete(Guid studyContentGuid);
        IQueryable<StudyContent> GetStudyContents(Guid studyGuid);
        StudyContent Get(Guid studyContentGuid);
        void Update(StudyContent entity);
    }
}
