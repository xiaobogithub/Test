using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ChangeTech.Models
{
    [DataContract]
    public class ResourceCategoryModel
    {
        [DataMember]
        public Guid CategoryGuid { get; set; }
        [DataMember]
        public string CategoryName { get; set; }
    }

    [DataContract]
    public class ResourceCategoriesModel
    {
        [DataMember]
        public List<ResourceCategoryModel> Categories { get; set; }
        [DataMember]
        public Guid LastSelectedResourceCategory { get; set; }
        [DataMember]
        public string LastSelectedResourceType { get; set; }
    }
}
