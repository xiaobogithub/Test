using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;
using ChangeTech.Entities;

namespace ChangeTech.Contracts
{
    public interface IProgramCategoryService
    {
        List<ProgramCategoryModel> GetProgramCategories(string windowsLiveId, string applicationId);

        List<ProgramInfoModel> GetProgramsByCategoryGuid(string windowsLiveId, Guid programCategoryGuid);
        Dictionary<int, string> GetCurrentSessionUrl(Win8ProgramUser win8ProgramUserEntity, Guid programGuid);
    }
}
