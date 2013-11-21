using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class ProgramProductRepository : RepositoryBase, IProgramProductRepository
    {
        public ProgramProduct GetProgramProductsByProgramGuid(Guid programGuid)
        {
            return GetEntities<ProgramProduct>().Where(pp => (!pp.IsDeleted.HasValue || pp.IsDeleted.HasValue && pp.IsDeleted.Value == false) && pp.ProgramGUID == programGuid).FirstOrDefault();
        }

        public IQueryable<ProgramProductScreenshot> GetScreenshotsByProgramProduct(Guid programProductGuid)
        {
            return GetEntities<ProgramProductScreenshot>().Where(ss => (!ss.IsDeleted.HasValue || ss.IsDeleted.HasValue && ss.IsDeleted.Value == false) && ss.ProgramProductGUID == programProductGuid);
        }
    }
}
