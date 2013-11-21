using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeTech.Models
{
    public class ResultResponseModel
    {
        public ResultTypeEnum ResultType { get; set; }
        public Object Content { get; set; }
    }

    public class ResultLineModel
    {
        public DateTime ResultDateTime { get; set; }
        public List<ResultVariableModel> ResultVaribles { get; set; }
    }

    public class ResultVariableModel
    {
        public string VariableName{ get; set; }
        public string VariableValue{ get; set; }
    }

    
}
