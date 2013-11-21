using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IProgramProductRepository
    {
        ProgramProduct GetProgramProductsByProgramGuid(Guid programGuid);
        IQueryable<ProgramProductScreenshot> GetScreenshotsByProgramProduct(Guid programProductGuid);
    }
}
