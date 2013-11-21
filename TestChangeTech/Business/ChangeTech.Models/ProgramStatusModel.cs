using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeTech.Models
{
    public class ProgramStatusModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class ProgramStatusListModel : List<ProgramStatusModel>
    {
        public ProgramStatusListModel(List<ProgramStatusModel> list)
        {
            AddRange(list);
        }
    }
}
