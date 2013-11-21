using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ChangeTech.Models
{
    [DataContract]
    public class ProgramProductModel
    {
        [DataMember]
        public Guid ProgramProductGuid { get; set; }
        [DataMember]
        public Guid ProgramGuid { get; set; }
        [DataMember]
        public string ProductImageUrl { get; set; }
        [DataMember]
        public string ProductDescription { get; set; }
        [DataMember]
        public string ProductInstructorImageUrl { get; set; }
        [DataMember]
        public List<ProgramProductScreenshotModel> ProgramProductScreenshot { get; set; }
    }

    [DataContract]
    public class ProgramProductScreenshotModel
    {
        [DataMember]
        public Guid ProgramProductGuid { get; set; }
        [DataMember]
        public string Screenshot { get; set; }
    }
}
