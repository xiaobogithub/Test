using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeTech.Models
{
    public class TipMessageModel
    {
        public Guid TipMessageGUID { get; set; }
        public Guid ProgramGUID { get; set; }
        public Guid TipMessageTypeGUID { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string BackButtonName { get; set; }
        public string Explanation { get; set; }
    }

    public class TipMessageListModel
    {
        public List<TipMessageModel> TipMessageModelList { get; set; }
    }

    public class TipMessageTypeModel
    {
        public Guid TipMessageTypeGUID { get; set; }
        public string TipMessageTypeName { get; set; }
    }
}
