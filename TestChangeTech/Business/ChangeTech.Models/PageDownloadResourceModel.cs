using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ChangeTech.Models
{
    [DataContract]
    public class PageDownloadResourceModel
    {
        //[DataMember]
        //public Guid CTPPGUID { get; set; }
        [DataMember]
        public Guid DownLoadResourceGUID { get; set; }

        [DataMember]
        public Guid PageGUID { get; set; }

        [DataMember]
        public Guid PageSequenceGUID { get; set; }

        [DataMember]
        public Guid SessionGUID { get; set; }
        [DataMember]
        public Guid ProgramGUID { get; set; }

        [DataMember]
        public string MediaType { get; set; }

        [DataMember]
        public string MediaLink { get; set; }

        [DataMember]
        public Guid SetResourceGUID { get; set; }

        [DataMember]
        public int ResourceOrder { get; set; }



    }
}
