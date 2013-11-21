using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ChangeTech.Models
{
    [DataContract]
    public class RelapseModel
    {
        [DataMember]
        public Guid RelapseGUID { get; set; }
        [DataMember]
        public Guid PageSequenceGUID { get; set; }
        [DataMember]
        public string PageSequenceName { get; set; }
        [DataMember]
        public string PageSequenceDescription { get; set; }
        [DataMember]
        public Guid ProgramRoomGUID { get; set; }
    }
}
