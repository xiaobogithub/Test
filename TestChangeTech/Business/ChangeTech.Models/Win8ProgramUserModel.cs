using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ChangeTech.Models
{
    [DataContract]
    public class Win8ProgramUserModel
    {
        [DataMember]
        public Guid Win8ProgramUserGuid { get; set; }
        [DataMember]
        public Guid ProgramUserGuid { get; set; }
        [DataMember]
        public string WindowsLiveId { get; set; }
    }
}
