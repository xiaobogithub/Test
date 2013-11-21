using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeTech.Models
{
    public class UserMenuModel
    {
        public Guid MenuItemGUID { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string FormTitle { get; set; }
        public string FormText { get; set; }
        public string FormBackButtonText { get; set; }
        public string FormSubmitButtonText { get; set; }
        public bool Available { get; set; }
    }

    public class UserMenuModelCollection : List<UserMenuModel>
    {

    }
}
