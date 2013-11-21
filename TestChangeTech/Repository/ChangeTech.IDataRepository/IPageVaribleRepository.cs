using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IPageVaribleRepository
    {
        void Add(PageVariable pageVariable);
        void Edit(PageVariable pageVariable);
        void Delete(Guid pageVariableGUID);
        void DeleteVariableOfProgram(Guid programGuid);
        int GetPageVariableCountByProgramGuidAndPageVaribleGuid(Guid programGuid, Guid pageVaribleGuid);


        IQueryable<PageVariable> GetPageVariableByProgram(Guid programGUID);
        //IQueryable<PageVariable> GetPageVariableByProgram(Guid programGUID, string pageVariableType);
        //IQueryable<PageVariable> GetVariableByProgramGuidAndGroupGuid(Guid programGuid, Guid groupGuid);
        PageVariable GetItem(Guid pageVariableGUID);
        PageVariable GetVariableByProgramGuidAndVariableName(Guid programGuid, string variableName);
        PageVariable GetPageVariableByProgramGuidAndParentPageVariableGuid(Guid programGuid, Guid parentPageVariableGuid);

        List<string> GetAllPageVariableNameListByProgramGUID(Guid programGUID);
        List<string> GetPageVariableBindByQuestionOfProgram(Guid programGUID);
        List<string> GetPageVariableBindByPageOfProgramGUID(Guid programGUID);
    }
}
