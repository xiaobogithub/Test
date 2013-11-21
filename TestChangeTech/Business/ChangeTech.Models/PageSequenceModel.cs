using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ChangeTech.Models
{
    public class PageSequenceModel
    {
        public Guid PageSequenceID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public Guid SessionContentID { get; set; }
        public Guid InterventID { get; set; }
        public KeyValuePair<Guid, string> Predictor { get; set; }
        public KeyValuePair<Guid, string> InterventCategory { get; set; }
        public int CountOfPages { get; set; }
        public string UsedInProgram { get; set; }
        public string ProgramRoom { get; set; }
        public UserModel LastUpdateBy { get; set; }
    }

    public class EditPageSequenceModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<SimplePageContentModel> Pages { get; set; }
        public string ProgramName { get; set; }
        public Guid ProgramGuid { get; set; }
        public string SessionName { get; set; }
        public Guid SessionID { get; set; }
        public Guid ProgramRoomGuid { get; set; }
        public Guid ProgramStatusGuid { get; set; }
        public bool IsLiveProgram { get; set; }
    }

    public class PageSequenceUsedByProgramInfoModel
    {
        public Guid ProgramID { get; set; }
        public List<PageSequenceUsedBySessionInfoModel> SessionIDs { get; set; }
    }

    public class PageSequenceUsedBySessionInfoModel
    {
        public Guid SessionID{get;set;}
        public List<Guid> SessionContentIDs{get;set;}
    }

    public class PageSequenceUsedInfo
    {
        public Guid ProgramID { get; set; }
        public Guid SessionID { get; set; }
        public Guid SessionContentID { get; set; }
    }

    [DataContract]
    public class PageSequenceReportModel
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int Order { get; set; }
        [DataMember]
        public List<PageReportModel> Pages { get; set; }
    }

    [DataContract]
    public class RelapsePageSequenceModel
    {
        [DataMember]
        public Guid PageSequenceGUID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
}
