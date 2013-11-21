using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

namespace ChangeTech.Models
{
    [DataContract]
    public class ResourceListModel
    {
        [DataMember]
        public List<ResourceModel> Resources { get; set; }
        [DataMember]
        public ResourceModel LastSelectedResource { get; set; }
    }

    [DataContract]
    public class ResourceModel
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public string Extension { get; set; }
        [DataMember]
        public string NameOnServer { get; set; }
        [DataMember]
        public Guid ResourceCategoryGUID { get; set; }
        [DataMember]
        public DateTime LastUpdated { get; set; }
        [DataMember]
        public string LastUpdatedBy { get; set; }
        [DataMember]
        public List<ProgramImageReference> ProgramImageReference { get; set; }
        [DataMember]
        public ResourceModel BigResource { get; set; }
        [DataMember]
        public bool HasProcessed { get; set; }
        [DataMember]
        public Guid CropFromResourceGUID { get; set; }
    }

    [DataContract]
    public class ResourceInBlobStorageModel
    {
        [DataMember]
        public Stream ResourceStream { get; set; }
        [DataMember]
        public Guid FileGuid { get; set; }
        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public ResourceTypeEnum ResourceType { get; set; }
        [DataMember]
        public Guid CategoryGUID { get; set; }
        [DataMember]
        public Guid CropFromResourceGUID { get; set; }
    }

    [DataContract]
    public class CropImageModel
    {
        [DataMember]
        public string NormalURL { get; set; }
        [DataMember]
        public string BigImageURL { get; set; }
        [DataMember]
        public Guid FileGuid { get; set; }
        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public int Height { get; set; }
        [DataMember]
        public int Width { get; set; }
        [DataMember]
        public int XSet { get; set; }
        [DataMember]
        public int YSet { get; set; }
        [DataMember]
        public Guid CategoryGUID { get; set; }
    }

    [DataContract]
    public class ProgramImageReference
    {
        [DataMember]
        public Guid ProgramGUID { get; set; }
        [DataMember]
        public string ProgramName { get; set; }
        [DataMember]
        public List<SessionImageReference> SessionImageReference { get; set; }
    }

    [DataContract]
    public class SessionImageReference
    {
        [DataMember]
        public Guid SessionGUID { get; set; }
        [DataMember]
        public int Day { get; set; }
        [DataMember]
        public int ReferenceCount { get; set; }
    }
}
