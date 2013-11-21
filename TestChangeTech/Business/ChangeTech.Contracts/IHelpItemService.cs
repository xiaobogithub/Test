using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;
using ChangeTech.Entities;

namespace ChangeTech.Contracts
{
    public interface IHelpItemService
    {
        void Update(HelpItemModel itemModel);
        void Insert(HelpItemModel itemModel);
        void Delete(Guid itemGuid);
        void MakeHelpItemOrderUp(Guid programGuid, Guid helpItemGuid);
        void MakeHelpItemOrderDown(Guid programGuid, Guid helpItemGuid);
        HelpItemModel GetHelpItemModel(Guid itemGuid);
        List<HelpItemModel> GetHelpItemModelList(Guid programGuid);
        int GetHelpItemCount(Guid programGuid);
        HelpItem CloneHelpItem(HelpItem helpItem);
        HelpItem SetDefaultGuidForHelpItem(HelpItem needSetDefaultEntity, CloneProgramParameterModel cloneParameterModel);
    }
}
