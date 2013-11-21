using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IProgramCategoryRepository
    {
        IQueryable<ProgramCategory> GetProgramCategories();
        IQueryable<ProgramCategoryProgram> GetProgramsByCategoryGuid(Guid programCategoryGuid);
    }
}
