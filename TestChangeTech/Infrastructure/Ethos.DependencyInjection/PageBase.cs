
using System;
using System.Collections.Generic;
namespace Ethos.DependencyInjection
{
    public class PageBase<TModel> : System.Web.UI.Page
    {
        protected string sortExpression = "SortExpression";
        protected string sortOrder = "SortOrder";
        protected string currentPage = "Page";
        public string HeaderString = string.Empty;
        public string PagingString = string.Empty;
        public TModel Model { get; set; }
        public int PageNumber
        {
            get { return Convert.ToInt32(ViewState["PageNumber"]); }
            set { ViewState["PageNumber"] = value; }
        }
        public int PageSize
        {
            get { return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"]); }
        }
        public int CurrentPageNumber
        {
            get
            {
                if (ViewState["CurrentPageNumber"] == null)
                {
                    int currentPageNumber;
                    Int32.TryParse(Request.QueryString[currentPage], out currentPageNumber);
                    if (currentPageNumber == 0)
                    {
                        currentPageNumber = 1;
                    }
                    ViewState["CurrentPageNumber"] = currentPageNumber;
                }
                return Convert.ToInt32(ViewState["CurrentPageNumber"]);

            }
            set { ViewState["CurrentPageNumber"] = value; }
        }
        public string SortExpression
        {
            get
            {
                if (ViewState["SortExpression"] != null)
                {
                    return ViewState["SortExpression"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set { ViewState["SortExpression"] = value; }
        }
        public string SortOrder
        {
            get
            {
                if (ViewState["SortOrder"] != null)
                {
                    return ViewState["SortOrder"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set { ViewState["SortOrder"] = value; }
        }

        protected string[] FittingSortExpression(string[] expression)
        {
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] != "")
                {
                    if (expression[i] == SortExpression)
                    {
                        expression[i] += SortOrder == "asc" ? " desc" : " asc";
                    }
                    else
                    {
                        expression[i] += " asc";
                    }
                }
            }
            return expression;
        }

        protected void InitialPageInfo(int sumPages, string sortExpressDefault, string sortOrderDefault)
        {
            if (sumPages == 0)
            {
                PageNumber = 1;
            }
            else
            {
                PageNumber = sumPages;
            }

            int currentPageNumber;
            Int32.TryParse(Request.QueryString[currentPage], out currentPageNumber);
            if (currentPageNumber > 0)
            {
                CurrentPageNumber = currentPageNumber;
                if (CurrentPageNumber > PageNumber)
                {
                    CurrentPageNumber = PageNumber;
                }
            }
            else
            {
                CurrentPageNumber = 1;
            }

            if (Request.QueryString[sortExpression] != null)
            {
                SortExpression = Request.QueryString[sortExpression];
            }
            else
            {
                SortExpression = sortExpressDefault;
            }

            if (Request.QueryString[sortOrder] != null)
            {
                SortOrder = Request.QueryString[sortOrder];
            }
            else
            {
                SortOrder = sortOrderDefault;
            }
        }

        public virtual T Resolve<T>()
        {
            IContainerContext context = ContainerManager.GetContainer("container");
            return context.Resolve<T>();
        }

        protected override void OnLoad(System.EventArgs e)
        {
            Title = Convert.ToString(GetLocalResourceObject("Title"));
            base.OnLoad(e);
        }
    }
}
