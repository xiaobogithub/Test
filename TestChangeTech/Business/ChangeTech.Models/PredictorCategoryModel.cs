using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeTech.Models
{
    public class PredictorCategoryModel
    {
        public Guid CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
    }

    public class PredictorCategorysModel : List<PredictorCategoryModel>
    {
        public PredictorCategorysModel(List<PredictorCategoryModel> predictorCategorys)
        {
            AddRange(predictorCategorys);
        }
    }
}
