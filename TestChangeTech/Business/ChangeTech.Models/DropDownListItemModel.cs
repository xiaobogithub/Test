using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeTech.Models
{
    public class DropDownListItemModel
    {
        public string TextField { get; set; }
        public string DataField { get; set; }

        public DropDownListItemModel(string textField, string dataField)
        {
            this.TextField = textField;
            this.DataField = dataField;
        }
    }
}
