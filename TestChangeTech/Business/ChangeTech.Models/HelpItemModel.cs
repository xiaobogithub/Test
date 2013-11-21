using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeTech.Models
{
    public class HelpItemModel
    {
        public Guid HelpItemGUID { get; set; }
        public Guid ProgramGUID { get; set; }
        public string ProgramName { get; set; }
        //public Guid LanguageGUID { get; set; }
        public string LanguageName { get; set; }
        public int Order { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }

    public class HelpItemListModel
    {
        public HelpItemListModel()
        {
            HelpItemList = new List<HelpItemModel>();
        }
        public List<HelpItemModel> HelpItemList { get; set; }
    }
}
