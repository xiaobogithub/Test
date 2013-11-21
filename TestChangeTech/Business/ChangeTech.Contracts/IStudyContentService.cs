using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;

namespace ChangeTech.Contracts
{
    public interface IStudyContentService
    {
        void AddNewStudyContent(InsertStudyContentModel model);
        void DeleteStudyContentByGUID(Guid studyContentGuid);
        void UpdateStudyContent(StudyContentModel model);
        StudyContentModel GetStudyContentByGuid(Guid studyContentGuid);
        List<StudyContentModel> GetStudyContentList(Guid studyGuid);
    }
}
