using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;

namespace ChangeTech.Contracts
{
    public interface IStudyService
    {
        List<StudyModel> GetStudyList(int pageNumber, int pageSize);
        int GetNumberOfStudy();
        void AddNewStudy(StudyModel model);
        void UpdateStudy(StudyModel model);
        void DeleteStudy(Guid studyGuid);
        StudyModel GetStudyByGUID(Guid studyGuid);
        Guid GetStudyGUIDByShortName(string shortName);
        //StudyContentModel RandomProgram(Guid studyGuid, long studyUserID);
        StudyContentModel RandomProgram(string studyShortName);
    }
}
