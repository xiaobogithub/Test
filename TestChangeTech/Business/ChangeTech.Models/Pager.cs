using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeTech.Models
{
    public class Pager
    {
        public int PageSize;
        public int PageNumber;
        public int CurrentPageNumber;
        public string SortExpression;

        public Pager(int pageSize, int pageNumber, int currentPageNumber, string sortExpression)
        {
            this.PageSize = pageSize;
            this.PageNumber = pageNumber;
            this.CurrentPageNumber = currentPageNumber;
            this.SortExpression = sortExpression;
        }
    }
}
