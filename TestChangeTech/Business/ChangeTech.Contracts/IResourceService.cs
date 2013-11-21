using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;
using System.IO;

using ChangeTech.Entities;
using System.Drawing;

namespace ChangeTech.Contracts
{
    public interface IResourceService
    {
        ResourceListModel GetResourceNameByCategoryGuid(Guid categoryGuid, ResourceTypeEnum type);
        List<ProgramImageReference> GetReferencesInfoOfImage(Guid resourceGuid);
        void SaveResource(Guid resourceGuid, Guid categoryGuid, string resourceName, string type);
        void UpdateResourceCategory(Guid resourceGuid, Guid resourceCategortGuid);
        void DeleteResource(Guid resourceGuid);
        void SaveResourceToAzureBlobStorage(ResourceInBlobStorageModel resourceInBlobStorageModel);
        void SaveResourceToAzureBlobStorage(Stream resourceStream, string fileGuid, string fileName, string resourceType, Guid categoryGUID);
        void SaveExportFile(string data, string fileGuid, string fileExtension);
        string GetBlobURLWithSharedWriteAccess(string resourceType, string fileGUID);
        bool CheckThumnailImageWhetherExist(string imageName);
        string GetBlob(string blobName);
        void UpdateResource(Guid resourceGUID, string mode, Guid parentResourceGUID);
        void UpdateResource(ResourceModel resourceModel);
        ResourceModel CropAndSaveImage(CropImageModel cropImageModel);
        Guid CreateResourceModel(Guid fileGuid, Guid parentResourceGUID, string mode);
        ResourceModel GetResourceModelByGuid(Guid resourceGuid);
        //void UploadImageToOriginalContainer(string fileGuid, string fileExtension, Stream imageStream);
        void ConvertResourceToJpg(ResourceModel rm, Image originalImage);
        void SaveUserVariableFile(Stream fileStream, string fileName);
        //List<Resource> GetResourceModelListBySessionGUID(Guid sessionGUID);

        //for Win8Service
        List<ResourceInfoModel> GetResourcesBySessionGuid(Guid sessionGUID);
    }
}
