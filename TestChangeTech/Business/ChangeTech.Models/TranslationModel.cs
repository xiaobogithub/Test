using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ChangeTech.Models
{
    [DataContract]
    public class TranslationModel
    {
        [DataMember]
        public string ID { get; set; }
        [DataMember]
        public string Object { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public string MaxLength { get; set; }
        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public string Translation { get; set; }

        //[DataMember]
        //public Guid SessionGUID { get; set; }
        //[DataMember]
        //public Guid PageSequenceGUID { get; set; }
        //[DataMember]
        //public Guid PageGUID { get; set; }
        //[DataMember]
        //public Guid QuestionGUID { get; set; }

        [DataMember]
        public Guid ParentGUID { get; set; }
    }
}
