using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class ProgramCategoryRepository : RepositoryBase, IProgramCategoryRepository
    {
        public IQueryable<ProgramCategory> GetProgramCategories()
        {
            return GetEntities<ProgramCategory>().Where(pc => (!pc.IsDeleted.HasValue || pc.IsDeleted.HasValue && pc.IsDeleted.Value == false)).OrderBy(pc => pc.Name);
        }

        public IQueryable<ProgramCategoryProgram> GetProgramsByCategoryGuid(Guid programCategoryGuid)
        {
            return GetEntities<ProgramCategoryProgram>().Where(pcm => pcm.ProgramCategoryGUID == programCategoryGuid && (!pcm.IsDeleted.HasValue || pcm.IsDeleted.HasValue && pcm.IsDeleted.Value == false)).OrderBy(pcm => pcm.ProgramCategoryProgramGUID);
        }
    }
}
