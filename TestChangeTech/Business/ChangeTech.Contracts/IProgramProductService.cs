using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;

namespace ChangeTech.Contracts
{
    public interface IProgramProductService
    {
        ProgramProductModel GetProgramProductsByProgramGuid(Guid programGuid);

        List<ProgramProductScreenshotModel> GetScreenshotsByProgramProductGuid(Guid programProductGuid);
    }
}
