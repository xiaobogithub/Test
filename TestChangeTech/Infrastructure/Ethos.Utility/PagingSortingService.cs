using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Collections;
using System.Linq.Dynamic;

namespace Ethos.Utility
{
    public class PagingSortingService
    {
        public static IQueryable<T> GetCurrentPage<T>(List<T> list, int CurrentPageNumber, int PageNumber, int PageSize, string SortExpression)
        {
            IQueryable<T> querylist = list.AsQueryable<T>();
            if (CurrentPageNumber > PageNumber)
            {
                throw new Exception("It is the last page!");
            }
            if (!string.IsNullOrEmpty(SortExpression))
            {
                return DynamicQueryable.OrderBy<T>(querylist, SortExpression, new object()).Skip((CurrentPageNumber - 1) * PageSize).Take(PageSize);
            }
            else
            {
                return querylist.Skip((CurrentPageNumber - 1) * PageSize).Take(PageSize);
            }
        }

        public static string InitialSortingString(string url, string[] sortexpression, string[] title)
        {
            StringBuilder sb = new StringBuilder();
            url = UrlSubString(url, "SortExpression="); 
           
            url = UrlSubString(url, "Page=");
           
            if (url.Contains("?"))
            {
                for (int i = 0; i < sortexpression.Length; i++)
                {
                    if (sortexpression[i] != "")
                    {
                        string[] sortStr = sortexpression[i].Split(' ');
                        sortexpression[i] = "<th><a href='" + url + "&SortExpression=" + sortStr[0] + "&SortOrder=" + sortStr[1] + "'>" + title[i] + "</a></th>";
                    }
                    else
                    {
                        sortexpression[i] = "<th>" + title[i] + "</th>";
                    }
                    sb.Append(sortexpression[i]);
                }
            }
            else
            {
                for (int i = 0; i < sortexpression.Length; i++)
                {
                    if (sortexpression[i] != "")
                    {
                        string[] sortStr = sortexpression[i].Split(' ');
                        sortexpression[i] = "<th><a href='" + url + "?SortExpression=" + sortStr[0] + "&SortOrder=" + sortStr[1] + "'>" + title[i] + "</a></th>";
                    }
                    else
                    {
                        sortexpression[i] = "<th>" + title[i] + "</th>";
                    }
                    sb.Append(sortexpression[i]);
                }
            }

            return sb.ToString();
        }

        public static string InitialPagingString(string url,int sumPage, int currentPage,string pre,string next)
        {
            StringBuilder sb = new StringBuilder();
            url = UrlSubString(url, "Page=");

            if (url.Contains("?"))
            {
                if (currentPage > 1)
                {
                    sb.Append("<a href='" + url + "&Page=" + (currentPage - 1) + "'>" + pre + "</a>");
                }
                
                // before current page
                if (currentPage > 10)
                {
                    for (int i = currentPage - 10; i < currentPage; i++)
                    {
                        sb.Append(" <a href='" + url + "&Page=" + i + "'>" + i + "</a> ");
                    }
                }
                else
                {
                    for (int i = 1; i < currentPage; i++)
                    {
                        sb.Append(" <a href='" + url + "&Page=" + i + "'>" + i + "</a> ");
                    }
                }

                // current page
                sb.Append(" <a href='" + url + "?Page=" + currentPage + "'  class='active'>" + currentPage + "</a> ");
                //sb.Append(" " + currentPage + " ");

                // after current page
                for (int i = 1; i < 10; i++)
                {
                    if (i + currentPage > sumPage)
                    {
                        break;
                    }
                    sb.Append(" <a href='" + url + "&Page=" + (i + currentPage) + "'>" + (i + currentPage) + "</a> ");
                }

                if (currentPage < sumPage)
                {
                    sb.Append("<a href='" + url + "&Page=" + (currentPage + 1) + "'>" + next + "</a>");
                }              
            }
            else
            {
                if (currentPage > 1)
                {
                    sb.Append("<a href='" + url + "?Page=" + (currentPage - 1) + "'>" + pre + "</a>");
                }
               
                // before current page
                if (currentPage > 10)
                {
                    for (int i = currentPage - 10; i < currentPage; i++)
                    {
                        sb.Append(" <a href='" + url + "?Page=" + i + "'>" + i + "</a> ");
                    }
                }
                else
                {
                    for (int i = 1; i < currentPage; i++)
                    {
                        sb.Append(" <a href='" + url + "?Page=" + i + "'>" + i + "</a> ");
                    }
                }

                // current page
                sb.Append(" <a href='" + url + "?Page=" + currentPage + "'  class='active'>" + currentPage + "</a> ");

                // after current page
                for (int i = 1; i < 10; i++)
                {
                    if (i + currentPage > sumPage)
                    {
                        break;
                    }
                    sb.Append(" <a href='" + url + "?Page=" + (i + currentPage) + "'>" + (i + currentPage) + "</a> ");
                }

                if (currentPage < sumPage)
                {
                    sb.Append("<a href='" + url + "?Page=" + (currentPage + 1) + "'>" + next + "</a>");
                }               
            }

            if (sumPage == 1)
            {
                return string.Empty;
            }

            return sb.ToString();
        }

        private static string UrlSubString(string url, string cutString)
        {
            if (url.Contains(cutString))
            {
                int index = url.IndexOf(cutString);
                url = url.Substring(0, index - 1);
            }
            return url;
        }
    }
}
