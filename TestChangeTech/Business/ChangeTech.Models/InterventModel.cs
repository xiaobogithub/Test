using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeTech.Models
{
    public class InterventModel
    {
        public Guid InterventGUID { get; set; }
        public string InterventName { get; set; }
        public string Description { get; set; }
        public string InterventCategoryName { get; set; }
        public Guid InterventCategoryGUID { get; set; }
    }

    public class InterventListModel:List<InterventModel>
    {
        public InterventListModel(List<InterventModel> list)
        {
            AddRange(list);
        }
    }

}
