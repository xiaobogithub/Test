using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using ChangeTech.Models;

namespace ChangeTech.Contracts
{
    public interface IPageVariableService
    {
        int GetPageVariableCount(Guid ProgramGUID, VariableTypeEnum PageVariableType, Guid GroupGUID);
        List<EditPageVariableModel> GetPageVariableByProgram(Guid ProgramGUID, VariableTypeEnum PageVariableType, Guid userGUID, Guid GroupGUID, int pageSize, int pageIndex);
        List<string> CheckPageVariableServiceConditionOfProgram(Guid programGuid);
        void Delete(Guid PageVariableGUID);
        void Edit(Models.PageVariableModel pageVariable);
        void Add(Models.PageVariableModel pageVariable);
        int BeforeDeletePageVariable(Guid pageVariableGuid);
        void SaveSetPageVariable(List<SetPageVariableModel> setVariableList);
        DataTable GetProgramUserPageVariable(Guid programGuid);
        DataTable GetProgramUserPageVariableExtension(Guid programGuid);
    }
}
