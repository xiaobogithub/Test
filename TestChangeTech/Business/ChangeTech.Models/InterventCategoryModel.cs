using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeTech.Models
{
    public class InterventCategoryModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CategoryGUID { get; set; }
        public Guid PredictorID { get; set; }
        public string PredictorName { get; set; }
    }

    public class IntervnetCategoryListModel:List<InterventCategoryModel>
    {
        public IntervnetCategoryListModel(List<InterventCategoryModel> list)
        {
            AddRange(list);
        }
    }

}
