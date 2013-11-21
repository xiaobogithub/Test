using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class BrandRepository : RepositoryBase, IBrandRepository
    {
        //IQueryable<Brand> GetAllBrand();
        //Brand GetBrandByGuid(Guid brandGuid);

        //void Insert(Brand brand);
        //void Delete(Guid brandGuid);
        //void Update(Brand brand);

        public IQueryable<Brand> GetAllBrand()
        {
            return GetEntities<Brand>().OrderBy(s => s.BrandName);
        }


        public Brand GetBrandByGuid(Guid brandGuid)
        {
            return GetEntities<Brand>(s => s.BrandGUID == brandGuid).FirstOrDefault();
        }

        public void Insert(Brand brand)
        {
            InsertEntity(brand);
        }

        public void Delete(Guid brandGuid)
        {
            DeleteEntity<Brand>(s => s.BrandGUID == brandGuid, Guid.Empty);
        }

        public void Update(Brand entity)
        {
            UpdateEntity(entity);
        }
    }
}
