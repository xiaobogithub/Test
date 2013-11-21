using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ChangeTech.Models
{
    //[DataContract]
    //public class EditPageVariableGroupModel : PageVariableGroupModel
    //{
    //    [DataMember]
    //    public ModelStatus ModelStatus { get; set; }
    //}

    [DataContract]
    public class EditPageVariableGroupModel
    {
        [DataMember]
        public Guid ProgramGUID { get; set; }
        [DataMember]
        public string ProgramName { get; set; }
        [DataMember]
        public List<PageVariableGroupModel> VariableGroupModels { get; set; }
        [DataMember]
        public SortedList<Guid, ModelStatus> ObjectStatus { get; set; }
        [DataMember]
        public EditPageVariableModel LastSelectedPageVariable { get; set; }
    }

    public class SimplePageVariableModel
    {
        public Guid PageVariableGuid { get; set; }
        public string Name { get; set; }
    }

    public class SetPageVariableModel
    {
        public string VariableName { get; set; }
        public string VariableValue { get; set; }
        public Guid ProgramGUID { get; set; }
        public Guid SessionGUID { get;set; }
        public Guid UserGUID { get; set; }
    }
}
