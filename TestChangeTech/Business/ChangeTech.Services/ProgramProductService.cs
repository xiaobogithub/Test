using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;
using ChangeTech.Models;
using ChangeTech.IDataRepository;
using ChangeTech.Entities;

namespace ChangeTech.Services
{
    public class ProgramProductService : ServiceBase, IProgramProductService
    {
        public ProgramProductModel GetProgramProductsByProgramGuid(Guid programGuid)
        {
            ProgramProductModel programProductModel = null;
            ProgramProduct programProductEntity = Resolve<IProgramProductRepository>().GetProgramProductsByProgramGuid(programGuid);
            if (programProductEntity != null)
            {
                programProductModel = new ProgramProductModel
                {
                    ProgramProductGuid = programProductEntity.ProgramProductGUID,
                    ProgramGuid = programProductEntity.ProgramGUID,
                    ProductDescription = programProductEntity.ProductDescription,
                    ProductImageUrl = programProductEntity.ProductImage,
                    ProductInstructorImageUrl = programProductEntity.ProductInstructorImage,
                    ProgramProductScreenshot = GetScreenshotsByProgramProductGuid(programProductEntity.ProgramProductGUID)
                };
            }
            else
            {
                programProductModel = new ProgramProductModel
                {
                    ProgramProductGuid = Guid.Empty,
                    ProgramGuid = programGuid,
                    ProductDescription = string.Empty,
                    ProductImageUrl = string.Empty,
                    ProductInstructorImageUrl = string.Empty,
                    ProgramProductScreenshot = new List<ProgramProductScreenshotModel>()
                };
            }
            return programProductModel;
        }

        public List<ProgramProductScreenshotModel> GetScreenshotsByProgramProductGuid(Guid programProductGuid)
        {
            List<ProgramProductScreenshotModel> screenshotModels = new List<ProgramProductScreenshotModel>();
            List<ProgramProductScreenshot> screenshotEntities = Resolve<IProgramProductRepository>().GetScreenshotsByProgramProduct(programProductGuid).ToList();
            foreach (ProgramProductScreenshot screenshotEnitity in screenshotEntities)
            {
                ProgramProductScreenshotModel screenshotModel = new ProgramProductScreenshotModel
                {
                    ProgramProductGuid = screenshotEnitity.ProgramProductGUID.Value,
                    Screenshot = screenshotEnitity.ScreenshotUrl
                };
                if (!screenshotModels.Contains(screenshotModel))
                {
                    screenshotModels.Add(screenshotModel);
                }
            }
            return screenshotModels;
        }
    }
}
