using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeTech.Models
{
    public class PredictorModel
    {
        public Guid PredictorID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public Guid CatagoryID { get; set; }
    }

    public class PredictorsModel : List<PredictorModel>
    {
        public PredictorsModel(List<PredictorModel> list)
        {
            AddRange(list);
        } 
    }
}
