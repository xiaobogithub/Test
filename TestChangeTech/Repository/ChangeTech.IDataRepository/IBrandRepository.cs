using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IBrandRepository
    {
        IQueryable<Brand> GetAllBrand();
        Brand GetBrandByGuid(Guid brandGuid);

        void Insert(Brand brand);
        void Delete(Guid brandGuid);
        void Update(Brand brand);
    }
}
