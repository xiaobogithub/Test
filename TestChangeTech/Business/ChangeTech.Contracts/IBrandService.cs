using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;

namespace ChangeTech.Contracts
{
    public interface IBrandService
    {
        List<BrandModel> GetBrandList();
        void AddNewBrand(BrandModel model);
        void UpdateBrand(BrandModel model);
        void DeleteBrand(Guid brandGuid);
        BrandModel GetBrandByGUID(Guid brandGuid);
        //Guid GetBrandGUIDByShortName(string shortName);

    }
}
