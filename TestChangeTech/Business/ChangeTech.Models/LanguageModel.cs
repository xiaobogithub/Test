using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ChangeTech.Models
{
    [DataContract]
    public class LanguageBaseModel
    {
        [DataMember]
        public Guid LanguageGUID { get; set; }
        [DataMember]
        public string Name { get; set; }
    }

    [DataContract]
    public class LanguageModel
    {
        [DataMember]
        public Guid LanguageGUID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public bool IsDefaultLanguage { get; set; }
        [DataMember]
        public int DaysCount { get; set; }
        [DataMember]
        public int StartDay { get; set; }
    }

    public class LanguagesModel : List<LanguageModel>
    {
    }

    public class ManageLanguageModel
    {
        public Guid LanguageGUID { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
    }
}
