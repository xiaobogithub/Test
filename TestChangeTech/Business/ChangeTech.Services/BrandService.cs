using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;
using ChangeTech.Models;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.Services
{
    public class BrandService : ServiceBase, IBrandService
    {
        public List<BrandModel> GetBrandList()
        {
            List<Brand> brandList = Resolve<IBrandRepository>().GetAllBrand().ToList();
            List<BrandModel> modelList = new List<BrandModel>();
            foreach (Brand brandEntity in brandList)
            {
                BrandModel model = new BrandModel
                {
                    BrandGUID=brandEntity.BrandGUID,
                    BrandName = brandEntity.BrandName + "-" + brandEntity.BrandURL,
                    BrandURL=brandEntity.BrandURL
                };
                modelList.Add(model);
            }

            return modelList;
        }

        //TODO: BrandURL,Name unique.
        public void AddNewBrand(BrandModel model)
        {
            Brand brandEntity = new Brand();
            brandEntity.BrandGUID = Guid.NewGuid();
            brandEntity.BrandName = model.BrandName;
            brandEntity.BrandURL = model.BrandURL;
            if (model.BrandLogo != null)
            {
                brandEntity.Resource = new Resource
                {
                    ResourceGUID = model.BrandLogo.ID,
                    Name = model.BrandLogo.Name,
                    Type = model.BrandLogo.Type,
                    FileExtension = model.BrandLogo.Extension,
                    NameOnServer = model.BrandLogo.NameOnServer,
                    ResourceCategory = Resolve<IResourceCategoryRepository>().GetResourceCategory("Default"),
                };
            }
            Resolve<IBrandRepository>().Insert(brandEntity);
        }

        public void DeleteBrand(Guid brandGuid)
        {
            Resolve<IBrandRepository>().Delete(brandGuid);
        }

        //TODO: BrandURL,Name unique.
        public void UpdateBrand(BrandModel model)
        {
            Brand entity = Resolve<IBrandRepository>().GetBrandByGuid(model.BrandGUID);

            string LogoExtension = model.BrandLogo.Extension;
            if (LogoExtension != "")
            {
                entity.Resource = new Resource
                {
                    ResourceGUID = model.BrandLogo.ID,
                    Name = model.BrandLogo.Name,
                    Type = model.BrandLogo.Type,
                    FileExtension = model.BrandLogo.Extension,
                    NameOnServer = model.BrandLogo.NameOnServer,
                    ResourceCategory = Resolve<IResourceCategoryRepository>().GetResourceCategory("Default"),
                };
            }
            entity.BrandName = model.BrandName;
            entity.BrandURL = model.BrandURL;
            Resolve<IBrandRepository>().Update(entity);
        }

        public BrandModel GetBrandByGUID(Guid brandGuid)
        {
            BrandModel bm = new BrandModel();
            Brand brandEntity = Resolve<IBrandRepository>().GetBrandByGuid(brandGuid);
            bm.BrandGUID = brandGuid;
            bm.BrandName = brandEntity.BrandName;
            bm.BrandURL = brandEntity.BrandURL;
            if (!brandEntity.ResourceReference.IsLoaded)
            {
                brandEntity.ResourceReference.Load();
            }
            if (brandEntity.Resource != null)
            {
                bm.BrandLogo = new ResourceModel
                {
                    Extension = brandEntity.Resource.FileExtension,
                    ID = brandEntity.Resource.ResourceGUID,
                    Name = brandEntity.Resource.Name,
                    NameOnServer = brandEntity.Resource.NameOnServer,
                    Type = brandEntity.Resource.Type,
                };
            }
            return bm;

        }

        public string GetBrandLogo(Guid BrandGuid)
        {
            Brand brand = Resolve<IBrandRepository>().GetBrandByGuid(BrandGuid);
            string result = string.Empty;
            if (brand != null)
            {
                if (!brand.ResourceReference.IsLoaded)
                {
                    brand.ResourceReference.Load();
                }
                if (brand.Resource != null)
                {
                    result = brand.Resource.NameOnServer;
                }
            }
            return result;
        }

    }
}
