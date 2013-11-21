using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ChangeTech.Contracts;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;
using ChangeTech.Models;
using Ethos.DependencyInjection;
using Ethos.Utility;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.StorageClient;
using System.IO;
using System.Security.Cryptography;
using System.Web;
using System.Web.Configuration;
using System.Drawing.Imaging;

namespace ChangeTech.Services
{
    public class ResourceService : ServiceBase, IResourceService
    {
        #region const of download resource
        public const string LINK_A_START = "<A";
        public const string LINK_A_END = "</A>";
        public const string ORIGINAL_IMAGE_DIRECTORY = "originalimagecontainer/";
        public const string TARGET = "target";
        public const string HTTP = "http";
        #endregion

        public ResourceListModel GetResourceNameByCategoryGuid(Guid categoryGuid, ResourceTypeEnum type)
        {
            ResourceListModel resourceList = new ResourceListModel();
            resourceList.Resources = new List<ResourceModel>();
            try
            {
                List<Resource> Resources = Resolve<IResourceRepository>().GetResourcesOfCategory(categoryGuid, type.ToString()).OrderBy(r => r.Name).ToList();
                foreach (Resource res in Resources)
                {
                    //TODO:need to change mode to enum, then remove .ToString()
                    if (!string.IsNullOrEmpty(res.Mode) && !res.Mode.Equals(PictureModeEnum.Big.ToString()))
                    {
                        ResourceModel rm = ServiceUtility.ParaseResourceModel(res);
                        if (res.Mode.Equals(PictureModeEnum.Normal.ToString()) && res.Type.Equals(ResourceTypeEnum.Image.ToString()))
                        {
                            Resource bigImage = Resources.Where(r => r.ParentResourceGUID.HasValue && r.ParentResourceGUID == res.ResourceGUID && r.Mode.Equals(PictureModeEnum.Big.ToString())).FirstOrDefault();
                            if (bigImage != null)
                            {
                                rm.BigResource = ServiceUtility.ParaseResourceModel(bigImage);
                            }
                        }
                        if (res.HasProcessd.HasValue && res.HasProcessd.Value)
                        {
                            rm.HasProcessed = true;
                        }
                        else
                        {
                            rm.HasProcessed = false;
                        }
                        resourceList.Resources.Add(rm);
                    }
                }
                
                Guid lastSelectResource = GetCurrentUserLastSelectedResource();
                if (lastSelectResource != Guid.Empty)
                {
                    Resource res = Resolve<IResourceRepository>().GetResource(lastSelectResource);
                    if (res != null)
                    {
                        resourceList.LastSelectedResource = ServiceUtility.ParaseResourceModel(res);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method Name:{0}, Resource Category GUID: {1}, Type {2}", System.Reflection.MethodBase.GetCurrentMethod().Name, categoryGuid, type));
                throw ex;
            }
            return resourceList;
        }

        private Guid GetCurrentUserLastSelectedResource()
        {
            Guid resourceGUID = Guid.Empty;
            User currentuser = Resolve<IUserRepository>().GetUserByGuid(Resolve<IUserService>().GetCurrentUser().UserGuid);
            if (currentuser != null && currentuser.LastSelectedResource != null)
            {
                resourceGUID = currentuser.LastSelectedResource.Value;
            }

            return resourceGUID;
        }

        public void SaveResource(ResourceModel resourceModel)
        {
            try
            {
                Resource resource = new Resource();
                resource.ResourceCategory = Resolve<IResourceCategoryRepository>().GetResourceCategory(resourceModel.ResourceCategoryGUID);
                resource.ResourceGUID = resourceModel.ID;
                resource.Name = resourceModel.Name;
                resource.Type = resourceModel.Type;
                resource.Mode = PictureModeEnum.Normal.ToString();
                string[] str = resourceModel.Name.Split('.');
                resource.FileExtension = "." + str.Last();
                resource.NameOnServer = resource.ResourceGUID + resource.FileExtension;
                resource.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
                resource.HasProcessd = true;
                resource.CropFromResourceGUID = resourceModel.CropFromResourceGUID;
                Resolve<IResourceRepository>().AddResource(resource);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method Name:{0}, Resource GUID : {1}, Resource Category GUID: {2}, Resource Name: {3}, Type {4}", System.Reflection.MethodBase.GetCurrentMethod().Name, resourceModel.ID, resourceModel.ResourceCategoryGUID, resourceModel.Name, resourceModel.Type));
                throw ex;
            }
        }

        public void SaveResource(Guid resourceGuid, Guid categoryGuid, string resourceName, string type)
        {
            try
            {
                Resource resource = new Resource();
                resource.ResourceCategory = Resolve<IResourceCategoryRepository>().GetResourceCategory(categoryGuid);
                resource.ResourceGUID = resourceGuid;
                resource.Name = resourceName;
                resource.Type = type;
                resource.Mode = PictureModeEnum.Normal.ToString();
                string[] str = resourceName.Split('.');
                resource.FileExtension = "." + str.Last();
                resource.NameOnServer = resource.ResourceGUID + resource.FileExtension;
                resource.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
                resource.HasProcessd = true;
                Resolve<IResourceRepository>().AddResource(resource);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method Name:{0}, Resource GUID : {1}, Resource Category GUID: {2}, Resource Name: {3}, Type {4}", System.Reflection.MethodBase.GetCurrentMethod().Name, resourceGuid, categoryGuid, resourceName, type));
                throw ex;
            }
        }

        public void UpdateResourceCategory(Guid resourceGuid, Guid resourceCategortGuid)
        {
            try
            {
                Resource resourceEntity = Resolve<IResourceRepository>().GetResource(resourceGuid);
                ResourceCategory resourceCategoryEntity = Resolve<IResourceCategoryRepository>().GetResourceCategory(resourceCategortGuid);
                resourceEntity.ResourceCategory = resourceCategoryEntity;
                resourceEntity.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
                Resolve<IResourceRepository>().UpdateResource(resourceEntity);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method Name:{0}, Resource GUID : {1}, Resource Category GUID: {2}", System.Reflection.MethodBase.GetCurrentMethod().Name, resourceGuid, resourceCategortGuid));
                throw ex;
            }
        }

        public void DeleteResource(Guid resourceGuid)
        {
            //TODO: need confirm whether the image is used
            Resolve<IResourceRepository>().RemoveResource(resourceGuid);
        }

        public List<ProgramImageReference> GetReferencesInfoOfImage(Guid resourceGuid)
        {
            Resource resourceEntity = Resolve<IResourceRepository>().GetResource(resourceGuid);
            return ServiceUtility.GetResourceReferenceInfo(resourceEntity);
        }

        /// <summary>
        /// Save resource to azure blob storage, so far only use for Crop Image
        /// </summary>
        /// <param name="resourceInBlobStorageModel"></param>
        public void SaveResourceToAzureBlobStorage(ResourceInBlobStorageModel resourceInBlobStorageModel)
        {
            try
            {
                // Create a blob in container and upload image bytes to it
                CloudBlob blob = null;

                string[] strType = resourceInBlobStorageModel.FileName.Split('.');
                string fileExtentsion = "." + strType.Last();

                switch (resourceInBlobStorageModel.ResourceType)
                {
                    case ResourceTypeEnum.Audio:
                        blob = GetContainer(ChangeTech.Models.BlobContainerTypeEnum.AudioContainer).GetBlobReference(string.Format("{0}.{1}", resourceInBlobStorageModel.FileGuid.ToString(), fileExtentsion));
                        blob.ServiceClient.Timeout = new TimeSpan(1, 0, 0);
                        blob.Properties.ContentType = FileUtility.GetMIMEType(fileExtentsion);
                        blob.UploadFromStream(resourceInBlobStorageModel.ResourceStream);
                        SaveResource(resourceInBlobStorageModel.FileGuid, resourceInBlobStorageModel.CategoryGUID, resourceInBlobStorageModel.FileName, resourceInBlobStorageModel.ResourceType.ToString());
                        break;
                    case ResourceTypeEnum.Video:
                        blob = GetContainer(ChangeTech.Models.BlobContainerTypeEnum.VideoContainer).GetBlobReference(string.Format("{0}{1}", resourceInBlobStorageModel.FileGuid.ToString(), fileExtentsion));
                        blob.ServiceClient.Timeout = new TimeSpan(1, 0, 0);
                        blob.Properties.ContentType = FileUtility.GetMIMEType(fileExtentsion);
                        blob.UploadFromStream(resourceInBlobStorageModel.ResourceStream);
                        SaveResource(resourceInBlobStorageModel.FileGuid, resourceInBlobStorageModel.CategoryGUID, resourceInBlobStorageModel.FileName, resourceInBlobStorageModel.ResourceType.ToString());
                        break;
                    case ResourceTypeEnum.Image:
                        resourceInBlobStorageModel.ResourceStream.Position = 0;
                        Image image = Image.FromStream(resourceInBlobStorageModel.ResourceStream);
                        // Big size image
                        if (image.Height > 550)
                        {
                            // Store normal size image of big image
                            resourceInBlobStorageModel.ResourceStream.Position = 0;
                            blob = GetContainer(ChangeTech.Models.BlobContainerTypeEnum.OriginalImageContainer).GetBlobReference(string.Format("{0}{1}", resourceInBlobStorageModel.FileGuid.ToString(), fileExtentsion));
                            blob.ServiceClient.Timeout = new TimeSpan(1, 0, 0);
                            blob.Properties.ContentType = FileUtility.GetMIMEType(fileExtentsion);
                            Stream normalImageStream = ImageUtility.GetResizeImageFile(resourceInBlobStorageModel.ResourceStream, 550);
                            blob.UploadFromStream(normalImageStream);
                            ResourceModel resourceModel = new ResourceModel {
                                ID = resourceInBlobStorageModel.FileGuid,
                                Name = resourceInBlobStorageModel.FileName,
                                Type = resourceInBlobStorageModel.ResourceType.ToString(),
                                ResourceCategoryGUID = resourceInBlobStorageModel.CategoryGUID,
                                CropFromResourceGUID = resourceInBlobStorageModel.CropFromResourceGUID
                            };
                            SaveResource(resourceModel);

                            // Store big size of image
                            resourceInBlobStorageModel.ResourceStream.Position = 0;
                            Guid biggerImageGuid = Guid.NewGuid();
                            blob = GetContainer(ChangeTech.Models.BlobContainerTypeEnum.OriginalImageContainer).GetBlobReference(string.Format("{0}{1}", biggerImageGuid.ToString(), fileExtentsion));
                            blob.ServiceClient.Timeout = new TimeSpan(1, 0, 0);
                            blob.Properties.ContentType = FileUtility.GetMIMEType(fileExtentsion);
                            blob.UploadFromStream(resourceInBlobStorageModel.ResourceStream);
                            CreateResourceModel(biggerImageGuid, resourceInBlobStorageModel.FileGuid, PictureModeEnum.Big.ToString());
                        }
                        // Normal size image
                        else
                        {
                            resourceInBlobStorageModel.ResourceStream.Position = 0;
                            blob = GetContainer(ChangeTech.Models.BlobContainerTypeEnum.OriginalImageContainer).GetBlobReference(string.Format("{0}{1}", resourceInBlobStorageModel.FileGuid.ToString(), fileExtentsion));
                            blob.ServiceClient.Timeout = new TimeSpan(1, 0, 0);
                            blob.Properties.ContentType = FileUtility.GetMIMEType(fileExtentsion);
                            blob.UploadFromStream(resourceInBlobStorageModel.ResourceStream);
                            ResourceModel resourceModel = new ResourceModel
                            {
                                ID = resourceInBlobStorageModel.FileGuid,
                                Name = resourceInBlobStorageModel.FileName,
                                Type = resourceInBlobStorageModel.ResourceType.ToString(),
                                ResourceCategoryGUID = resourceInBlobStorageModel.CategoryGUID,
                                CropFromResourceGUID = resourceInBlobStorageModel.CropFromResourceGUID
                            };
                            SaveResource(resourceModel);
                        }

                        // Upload image with thumnail size
                        blob = GetContainer(ChangeTech.Models.BlobContainerTypeEnum.ThumnailContainer).GetBlobReference(string.Format("{0}{1}", resourceInBlobStorageModel.FileGuid, fileExtentsion));
                        blob.ServiceClient.Timeout = new TimeSpan(1, 0, 0);
                        blob.Properties.ContentType = FileUtility.GetMIMEType(fileExtentsion);
                        resourceInBlobStorageModel.ResourceStream.Position = 0;
                        blob.UploadFromStream(ImageUtility.CreateThumnailImage(Image.FromStream(resourceInBlobStorageModel.ResourceStream), 40, 40));
                        break;
                    case ResourceTypeEnum.Document:
                        blob = GetContainer(ChangeTech.Models.BlobContainerTypeEnum.DocumentContainer).GetBlobReference(string.Format("{0}{1}", resourceInBlobStorageModel.FileGuid.ToString(), fileExtentsion));
                        blob.ServiceClient.Timeout = new TimeSpan(1, 0, 0);
                        blob.Properties.ContentType = FileUtility.GetMIMEType(fileExtentsion);
                        blob.UploadFromStream(resourceInBlobStorageModel.ResourceStream);
                        SaveResource(resourceInBlobStorageModel.FileGuid, resourceInBlobStorageModel.CategoryGUID, resourceInBlobStorageModel.FileName, resourceInBlobStorageModel.ResourceType.ToString());
                        break;
                    case ResourceTypeEnum.Logo:
                        blob = GetContainer(ChangeTech.Models.BlobContainerTypeEnum.LogoContainer).GetBlobReference(string.Format("{0}{1}", resourceInBlobStorageModel.FileGuid.ToString(), fileExtentsion));
                        blob.ServiceClient.Timeout = new TimeSpan(1, 0, 0);
                        blob.Properties.ContentType = FileUtility.GetMIMEType(fileExtentsion);
                        blob.UploadFromStream(resourceInBlobStorageModel.ResourceStream);
                        //Why not call SaveResource here? other types have already called it. Need to refactor.
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method Name:{0}, Resource GUID : {1}", System.Reflection.MethodBase.GetCurrentMethod().Name, resourceInBlobStorageModel.FileGuid));
                throw ex;
            }
        }

        /// <summary>
        /// Need to call SaveResourceToAzureBlobStorage(ResourceInBlobStorageModel resourceInBlobStorageModel) instead this method
        /// </summary>
        /// <param name="resourceStream"></param>
        /// <param name="fileGuid"></param>
        /// <param name="fileName"></param>
        /// <param name="resourceType"></param>
        /// <param name="categoryGUID"></param>
        public void SaveResourceToAzureBlobStorage(System.IO.Stream resourceStream, string fileGuid, string fileName, string resourceType, Guid categoryGUID)
        {
            try
            {
                // Create a blob in container and upload image bytes to it
                CloudBlob blob = null;

                string[] strType = fileName.Split('.');
                string fileExtentsion = "." + strType.Last();

                switch (resourceType)
                {
                    case "Audio":
                        blob = GetContainer(ChangeTech.Models.BlobContainerTypeEnum.AudioContainer).GetBlobReference(string.Format("{0}.{1}", fileGuid, fileExtentsion));
                        blob.ServiceClient.Timeout = new TimeSpan(1, 0, 0);
                        blob.Properties.ContentType = FileUtility.GetMIMEType(fileExtentsion);
                        blob.UploadFromStream(resourceStream);
                        SaveResource(new Guid(fileGuid), categoryGUID, fileName, resourceType);
                        break;
                    case "Video":
                        blob = GetContainer(ChangeTech.Models.BlobContainerTypeEnum.VideoContainer).GetBlobReference(string.Format("{0}{1}", fileGuid, fileExtentsion));
                        blob.ServiceClient.Timeout = new TimeSpan(1, 0, 0);
                        blob.Properties.ContentType = FileUtility.GetMIMEType(fileExtentsion);
                        blob.UploadFromStream(resourceStream);
                        SaveResource(new Guid(fileGuid), categoryGUID, fileName, resourceType);
                        break;
                    case "Image":
                        resourceStream.Position = 0;
                        Image image = Image.FromStream(resourceStream);
                        // Big size image
                        if (image.Height > 550)
                        {
                            // Store normal size image of big image
                            resourceStream.Position = 0;
                            blob = GetContainer(ChangeTech.Models.BlobContainerTypeEnum.OriginalImageContainer).GetBlobReference(string.Format("{0}{1}", fileGuid, fileExtentsion));
                            blob.ServiceClient.Timeout = new TimeSpan(1, 0, 0);
                            blob.Properties.ContentType = FileUtility.GetMIMEType(fileExtentsion);
                            Stream normalImageStream = ImageUtility.GetResizeImageFile(resourceStream, 550);
                            blob.UploadFromStream(normalImageStream);
                            SaveResource(new Guid(fileGuid), categoryGUID, fileName, resourceType);

                            // Store big size of image
                            resourceStream.Position = 0;
                            Guid biggerImageGuid = Guid.NewGuid();
                            blob = GetContainer(ChangeTech.Models.BlobContainerTypeEnum.OriginalImageContainer).GetBlobReference(string.Format("{0}{1}", biggerImageGuid.ToString(), fileExtentsion));
                            blob.ServiceClient.Timeout = new TimeSpan(1, 0, 0);
                            blob.Properties.ContentType = FileUtility.GetMIMEType(fileExtentsion);
                            blob.UploadFromStream(resourceStream);
                            CreateResourceModel(biggerImageGuid, new Guid(fileGuid), PictureModeEnum.Big.ToString());
                        }                 
                        // Normal size image
                        else
                        {
                            resourceStream.Position = 0;
                            Guid biggerImageGuid = Guid.NewGuid();
                            blob = GetContainer(ChangeTech.Models.BlobContainerTypeEnum.OriginalImageContainer).GetBlobReference(string.Format("{0}{1}", fileGuid.ToString(), fileExtentsion));
                            blob.ServiceClient.Timeout = new TimeSpan(1, 0, 0);
                            blob.Properties.ContentType = FileUtility.GetMIMEType(fileExtentsion);
                            blob.UploadFromStream(resourceStream);
                            SaveResource(new Guid(fileGuid), categoryGUID, fileName, resourceType);
                        }

                        // Upload image with thumnail size
                        blob = GetContainer(ChangeTech.Models.BlobContainerTypeEnum.ThumnailContainer).GetBlobReference(string.Format("{0}{1}", fileGuid, fileExtentsion));
                        blob.ServiceClient.Timeout = new TimeSpan(1, 0, 0);
                        blob.Properties.ContentType = FileUtility.GetMIMEType(fileExtentsion);
                        resourceStream.Position = 0;
                        blob.UploadFromStream(ImageUtility.CreateThumnailImage(Image.FromStream(resourceStream), 40, 40));
                        break;
                    case "Document":
                        blob = GetContainer(ChangeTech.Models.BlobContainerTypeEnum.DocumentContainer).GetBlobReference(string.Format("{0}{1}", fileGuid, fileExtentsion));
                        blob.ServiceClient.Timeout = new TimeSpan(1, 0, 0);
                        blob.Properties.ContentType = FileUtility.GetMIMEType(fileExtentsion);
                        blob.UploadFromStream(resourceStream);
                        SaveResource(new Guid(fileGuid), categoryGUID, fileName, resourceType);
                        break;
                    case "Logo":
                        blob = GetContainer(ChangeTech.Models.BlobContainerTypeEnum.LogoContainer).GetBlobReference(string.Format("{0}{1}", fileGuid, fileExtentsion));
                        blob.ServiceClient.Timeout = new TimeSpan(1, 0, 0);
                        blob.Properties.ContentType = FileUtility.GetMIMEType(fileExtentsion);
                        blob.UploadFromStream(resourceStream);
                        //Why not call SaveResource here? other types have already called it. Need to refactor.
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method Name:{0}, Resource GUID : {1}", System.Reflection.MethodBase.GetCurrentMethod().Name, fileGuid));
                throw ex;
            }
        }

        public void ConvertResourceToJpg(ResourceModel rm, Image originalImage)
        {
            try
            {
                MemoryStream jpgImageMs = new MemoryStream();
                // Encoder parameter for image quality
                EncoderParameter qualityParam = new EncoderParameter(Encoder.Quality, 100L);
                // Get image codecs for all image formats
                ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
                // Jpeg image codec
                ImageCodecInfo jpegCodec = null;

                // Find the correct image codec
                for (int i = 0; i < codecs.Length; i++)
                {
                    if (codecs[i].MimeType == "image/jpeg")
                    {
                        jpegCodec = codecs[i];
                    }
                }

                EncoderParameters encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = qualityParam;

                originalImage.Save(jpgImageMs, jpegCodec, encoderParams);
                jpgImageMs.Position = 0;
                CloudBlob blob = null;
                blob = GetContainer(ChangeTech.Models.BlobContainerTypeEnum.OriginalImageContainer).GetBlobReference(string.Format("{0}", rm.NameOnServer));
                blob.ServiceClient.Timeout = new TimeSpan(1, 0, 0);
                blob.Properties.ContentType = FileUtility.GetMIMEType(".jpg");
                blob.UploadFromStream(jpgImageMs);

                //Resource imageEntity = Resolve<IResourceRepository>().GetResource(rm.ID);
                //imageEntity.FileExtension = ".jpg";
                //imageEntity.NameOnServer = imageEntity.NameOnServer.Replace("png", "jpg");
                //Resolve<IResourceRepository>().UpdateResource(imageEntity);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method Name:{0}, Resource GUID : {1}", System.Reflection.MethodBase.GetCurrentMethod().Name, rm.ID));
                throw ex;
            }
        }

        public string GetBlob(string blobName)
        {
            string data = string.Empty;
            try
            {

                string accountName = Resolve<ISystemSettingRepository>().GetSettingValue("AzureStorageAccountName");
                string accountKey = Resolve<ISystemSettingRepository>().GetSettingValue("AzureStorageAccountKey");
                StorageCredentialsAccountAndKey securityKey = new StorageCredentialsAccountAndKey(accountName,
                    accountKey);
                CloudStorageAccount account = new CloudStorageAccount(securityKey, false);
                var client = account.CreateCloudBlobClient();
                CloudBlob blob = client.GetBlobReference(blobName);
                data = blob.DownloadText();
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method Name:{0}, Resource GUID : {1}", System.Reflection.MethodBase.GetCurrentMethod().Name, blobName));
                throw ex;
            }
            return data;
        }

        public void SaveExportFile(string data, string fileGuid, string fileExtentsion)
        {
            try
            {
                // Create a blob in container and upload image bytes to it
                CloudBlob blob = null;
                blob = GetContainer(ChangeTech.Models.BlobContainerTypeEnum.ExportContainer).GetBlobReference(string.Format("{0}", fileGuid));
                blob.ServiceClient.Timeout = new TimeSpan(1, 0, 0);
                blob.Properties.ContentType = FileUtility.GetMIMEType(fileExtentsion);
                blob.UploadText(data);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method Name:{0}, Resource GUID : {1}", System.Reflection.MethodBase.GetCurrentMethod().Name, fileGuid));
                throw ex;
            }
        }

        public void SaveUserVariableFile(Stream fileStream, string fileName)
        {
            try
            {
                CloudBlob blob = null;
                fileStream.Position = 0;
                blob = GetContainer(ChangeTech.Models.BlobContainerTypeEnum.ExportContainer).GetBlobReference(string.Format("{0}", fileName));
                blob.ServiceClient.Timeout = new TimeSpan(1, 0, 0);
                blob.Properties.ContentType = FileUtility.GetMIMEType(".xls");
                blob.UploadFromStream(fileStream);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method Name:{0}, File Name : {1}", System.Reflection.MethodBase.GetCurrentMethod().Name, fileName));
                throw ex;
            }
        }

        public string GetBlobURLWithSharedWriteAccess(string resourceType, string fileGUID)
        {
            string URL = string.Empty;
            switch (resourceType)
            {
                case "Audio":
                    //container = GetContainer(ChangeTech.Models.BlobContainerType.AudioContainer);
                    URL = GetSharedAccessSignature(BlobContainerTypeEnum.AudioContainer, fileGUID);
                    break;
                case "Video":
                    //container = GetContainer(ChangeTech.Models.BlobContainerType.VideoContainer);
                    URL = GetSharedAccessSignature(BlobContainerTypeEnum.VideoContainer, fileGUID);
                    break;
                case "OriginalImage":
                    //container = GetContainer(ChangeTech.Models.BlobContainerType.OriginalImageContainer);
                    URL = GetSharedAccessSignature(BlobContainerTypeEnum.OriginalImageContainer, fileGUID);
                    break;
                case "ThumnailImage":
                    //container = GetContainer(ChangeTech.Models.BlobContainerType.ThumnailContainer);
                    URL = GetSharedAccessSignature(BlobContainerTypeEnum.ThumnailContainer, fileGUID);
                    break;
                case "Document":
                    //container = GetContainer(ChangeTech.Models.BlobContainerType.DocumentContainer);
                    URL = GetSharedAccessSignature(BlobContainerTypeEnum.DocumentContainer, fileGUID);
                    break;
                case "Logo":
                    //container = GetContainer(ChangeTech.Models.BlobContainerType.LogoContainer);
                    URL = GetSharedAccessSignature(BlobContainerTypeEnum.LogoContainer, fileGUID);
                    break;
            }
            return URL;
        }

        private String GetSharedAccessSignature(BlobContainerTypeEnum containType, String blobName)
        {
            var container = GetContainer(containType);

            // Get blob
            CloudBlob blob = container.GetBlobReference(blobName);

            // Create a policy
            SharedAccessPolicy policy = new SharedAccessPolicy();
            policy.Permissions = SharedAccessPermissions.Write | SharedAccessPermissions.Read;
            policy.SharedAccessStartTime = DateTime.UtcNow - TimeSpan.FromMinutes(10);
            policy.SharedAccessExpiryTime = DateTime.UtcNow + TimeSpan.FromMinutes(50);

            // Create signature for the blob from the policy
            string sSignature = container.GetSharedAccessSignature(policy);

            return blob.Uri.AbsoluteUri + sSignature;
        }

        private CloudBlobContainer GetContainer(BlobContainerTypeEnum containerType)
        {
            string accountName = Resolve<ISystemSettingRepository>().GetSettingValue("AzureStorageAccountName");
            string accountKey = Resolve<ISystemSettingRepository>().GetSettingValue("AzureStorageAccountKey");
            StorageCredentialsAccountAndKey securityKey = new StorageCredentialsAccountAndKey(accountName,
                accountKey);
            CloudStorageAccount account = new CloudStorageAccount(securityKey, false);
            var client = account.CreateCloudBlobClient();
            //CreateSilverlightPolicy(client);
            return client.GetContainerReference(containerType.ToString().ToLower());
        }

        private void CreateSilverlightPolicy(CloudBlobClient blobs)
        {
            blobs.GetContainerReference("$root").CreateIfNotExist();
            blobs.GetContainerReference("$root").SetPermissions(
                new BlobContainerPermissions()
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });
            var blob = blobs.GetBlobReference("clientaccesspolicy.xml");
            blob.Properties.ContentType = "text/xml";
            blob.UploadText(@"<?xml version=""1.0"" encoding=""utf-8""?>
                    <access-policy>
                      <cross-domain-access>
                        <policy>
                          <allow-from http-methods=""*"" http-request-headers=""*"">
                            <domain uri=""*"" />
                            <domain uri=""http://*"" />
                          </allow-from>
                          <grant-to>
                            <resource path=""/"" include-subpaths=""true"" />
                          </grant-to>
                        </policy>
                      </cross-domain-access>
                    </access-policy>");
        }

        public bool CheckThumnailImageWhetherExist(string imageName)
        {
            bool exist = false;
            CloudBlobContainer container = GetContainer(ChangeTech.Models.BlobContainerTypeEnum.ThumnailContainer);
            CloudBlob blob = container.GetBlobReference(imageName);
            try
            {
                blob.FetchAttributes();
                exist = true;
            }
            catch (StorageClientException ex)
            {
                if (ex.ErrorCode == StorageErrorCode.ResourceNotFound)
                {
                    exist = false;
                }
            }
            return exist;
        }

        public ResourceModel CropAndSaveImage(CropImageModel cropImageModel)
        {
            Guid newresourceguid = Guid.NewGuid();
            //CreateResourceModel(newresourceguid, new Guid(normalFileGuid), PictureMode.Crop.ToString());

            Stream imageStream = ImageUtility.CropImage(cropImageModel.NormalURL, cropImageModel.BigImageURL, cropImageModel.Width, cropImageModel.Height, cropImageModel.XSet, cropImageModel.YSet);
            ResourceInBlobStorageModel resourceInBlobStorageModel = new ResourceInBlobStorageModel {
                ResourceStream = imageStream,
                FileGuid = newresourceguid,
                FileName = cropImageModel.FileName,
                ResourceType = ResourceTypeEnum.Image,
                CategoryGUID = cropImageModel.CategoryGUID,
                CropFromResourceGUID = cropImageModel.FileGuid
            };
            SaveResourceToAzureBlobStorage(resourceInBlobStorageModel);

            return GetResourceModelByGuid(newresourceguid);
        }

        public Guid CreateResourceModel(Guid fileGuid, Guid normalFileGuid, string mode)
        {
            Resource parentResource = Resolve<IResourceRepository>().GetResource(normalFileGuid);
            if (!parentResource.ResourceCategoryReference.IsLoaded)
            {
                parentResource.ResourceCategoryReference.Load();
            }

            Resource newresource = new Resource();
            newresource.ResourceGUID = fileGuid;
            newresource.ResourceCategory = parentResource.ResourceCategory;
            newresource.Name = parentResource.Name;
            newresource.Type = parentResource.Type;
            newresource.FileExtension = parentResource.FileExtension;
            newresource.NameOnServer = newresource.ResourceGUID + newresource.FileExtension;
            newresource.Mode = mode;
            newresource.ParentResourceGUID = normalFileGuid;
            newresource.HasProcessd = true;
            newresource.CropFromResourceGUID = parentResource.CropFromResourceGUID;
            Resolve<IResourceRepository>().AddResource(newresource);

            return newresource.ResourceGUID;
        }

        public void UpdateResource(Guid resourceGUID, string mode, Guid parentResourceGUID)
        {
            Resource resource = Resolve<IResourceRepository>().GetResource(resourceGUID);
            resource.Mode = mode;
            resource.HasProcessd = true;
            if (parentResourceGUID != Guid.Empty)
            {
                resource.ParentResourceGUID = parentResourceGUID;
            }
            Resolve<IResourceRepository>().UpdateResource(resource);
        }

        public void UpdateResource(ResourceModel resourceModel)
        {
            Resource resourceEntity = Resolve<IResourceRepository>().GetResource(resourceModel.ID);
            if (resourceEntity != null)
            {
                if (!resourceEntity.ResourceCategoryReference.IsLoaded) resourceEntity.ResourceCategoryReference.Load();
                resourceEntity.ResourceCategory = Resolve<IResourceCategoryRepository>().GetResourceCategory(resourceModel.ResourceCategoryGUID);
                resourceEntity.Name = resourceModel.Name;
                resourceEntity.Type = resourceModel.Type;
                resourceEntity.FileExtension = !string.IsNullOrEmpty(resourceModel.Extension) ? resourceModel.Extension : string.Empty;
                resourceEntity.NameOnServer = !string.IsNullOrEmpty(resourceModel.NameOnServer) ? resourceModel.NameOnServer : string.Empty;
                resourceEntity.HasProcessd = resourceModel.HasProcessed;
                resourceEntity.LastUpdatedBy = !string.IsNullOrEmpty(resourceModel.LastUpdatedBy) ? new Guid(resourceModel.LastUpdatedBy) : Guid.Empty;
                resourceEntity.CropFromResourceGUID = resourceModel.CropFromResourceGUID;
                Resolve<IResourceRepository>().UpdateResource(resourceEntity);
            }
        }

        public ResourceModel GetResourceModelByGuid(Guid resourceGuid)
        {
            Resource resource = Resolve<IResourceRepository>().GetResource(resourceGuid);
            ResourceModel model = new ResourceModel
            {
                ID = resource.ResourceGUID,
                Name = resource.Name,
                NameOnServer = resource.NameOnServer,
                Extension = resource.FileExtension,
                Type = resource.Type
            };
            return model;
        }

        //public List<Resource> GetResourceModelListBySessionGUID(Guid sessionGUID)
        //{
        //    return Resolve<IResourceRepository>().GetResourcesBySessionGUID(sessionGUID).ToList();
        //}

        public List<ResourceInfoModel> GetResourcesBySessionGuid(Guid sessionGUID)
        {
            List<ResourceInfoModel> sessionResource = new List<ResourceInfoModel>();
            try
            {
                Dictionary<string, string> resourceDic = Resolve<IPageService>().GetResourcesBySessionGuid(sessionGUID);

                foreach (KeyValuePair<string, string> resource in resourceDic)
                {
                    string link = resource.Key;
                    string resourceType = resource.Value == string.Empty ? string.Empty : resource.Value.ToLower();

                    int indexOfResourceName = link.IndexOf('>') + 1;
                    int indexOfResourceNameEnd = (link.ToLower()).IndexOf(LINK_A_END.ToLower()) - 1;
                    string resourceName = link.Substring(indexOfResourceName, indexOfResourceNameEnd - indexOfResourceName + 1);
                    int indexOfResourceNameExtension = resourceName.LastIndexOf('.');
                    if (indexOfResourceNameExtension > -1)
                    {
                        resourceName = resourceName.Remove(indexOfResourceNameExtension);
                    }
                    link = link.Substring(link.IndexOf(HTTP), link.LastIndexOf(TARGET) - link.IndexOf(HTTP) - 2);
                    
                    ResourceInfoModel resourceModel = new ResourceInfoModel
                    {
                        ResourceName = resourceName,
                        ResourceType = resourceType,
                        ReourceURL = link
                    };
                    if (!sessionResource.Contains(resourceModel))
                    {
                        sessionResource.Add(resourceModel);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }

            return sessionResource;
        }

        //public List<ResourceInfoModel> GetResourcesBySessionGuid(Guid sessionGUID)
        //{
        //    List<ResourceInfoModel> sessionResource = new List<ResourceInfoModel>();
        //    try
        //    {
        //        Dictionary<string, string> resourceDic = Resolve<IPageService>().GetResourcesBySessionGuid(sessionGUID);

        //        foreach (KeyValuePair<string, string> resource in resourceDic)
        //        {
        //            string link = resource.Key;
        //            link = link.Substring(link.IndexOf(HTTP), link.LastIndexOf(TARGET) - link.IndexOf(HTTP) - 2);
        //            string resourceType = resource.Value == string.Empty ? string.Empty : resource.Value.ToLower();

        //            string className = "";
        //            if (resourceType == ResourceTypeEnum.Image.ToString().ToLower())
        //                className = CLASS_IMAGE;
        //            else if (resourceType == ResourceTypeEnum.Document.ToString().ToLower())
        //                className = CLASS_DOCUMENT;
        //            else if (resourceType == ResourceTypeEnum.Video.ToString().ToLower())
        //                className = CLASS_VIDEO;
        //            else if (resourceType == ResourceTypeEnum.Audio.ToString().ToLower())
        //                className = CLASS_AUDIO;

        //            int indexOfResourceName = link.IndexOf('>') + 1;
        //            int indexOfResourceNameEnd = (link.ToLower()).IndexOf(LINK_A_END.ToLower()) - 1;
        //            string resourceName = link.Substring(indexOfResourceName, indexOfResourceNameEnd - indexOfResourceName + 1);
        //            int indexOfResourceNameExtension = resourceName.LastIndexOf('.');
        //            if (indexOfResourceNameExtension > -1)
        //            {
        //                resourceName = resourceName.Remove(indexOfResourceNameExtension);
        //            }
        //            link = link.Remove(indexOfResourceName) + resourceName + LINK_A_END;
        //            link = LI_START + " class=\"{0}\" >" + link + LI_END;
        //            link = string.Format(link, className);

        //            ResourceInfoModel resourceModel = new ResourceInfoModel
        //            {
        //                ResourceName = resourceName,
        //                ResourceType = resourceType,
        //                ReourceURL = link
        //            };
        //            if (sessionResource.Contains(resourceModel) && className != CLASS_IMAGE)
        //            {
        //                sessionResource.Add(resourceModel);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
        //        throw ex;
        //    }

        //    return sessionResource;
        //}
    }
}
