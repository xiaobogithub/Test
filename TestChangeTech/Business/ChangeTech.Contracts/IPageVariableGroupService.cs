using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;
using ChangeTech.Entities;

namespace ChangeTech.Contracts
{
    public interface IPageVariableGroupService
    {
        EditPageVariableGroupModel GetPageVariableGroupByProgram(Guid programGuid);
        void SavePageVariabeGroup(EditPageVariableGroupModel groupModel);
        PageVariableGroup CloneVariableGroup(PageVariableGroup group);
        PageVariableGroup SetDefaultGuidForPageVariableGroup(PageVariableGroup needSetDefaultEntity, CloneProgramParameterModel cloneParameterModel);
    }
}
