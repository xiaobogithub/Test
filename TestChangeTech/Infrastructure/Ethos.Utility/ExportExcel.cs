using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.IO;

namespace Ethos.Utility
{
    public class ExportExcel
    {
        public static void ExportExcelFromGridView(System.Web.UI.WebControls.GridView gv, string excelName)
        {
            
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.Buffer = true;
            System.Web.HttpContext.Current.Response.HeaderEncoding = Encoding.GetEncoding("utf-8");
            System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + excelName + ".xls");
            System.Web.HttpContext.Current.Response.Charset = "";
            System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
            gv.EnableViewState = false;
            System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("en-US", true);
            System.IO.StringWriter stringWrite = new System.IO.StringWriter(myCItrad);

            System.IO.StringWriter sw = new System.IO.StringWriter(myCItrad);

            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            gv.RenderControl(htmlWrite);

            System.Web.HttpContext.Current.Response.Write(stringWrite.ToString());
            System.Web.HttpContext.Current.Response.Flush();
            System.Web.HttpContext.Current.Response.End();
        }

        public static void ExportExcelFromDataSource(string datas, string excelName, System.Web.UI.Page Page)
        {
            System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("en-US", true);
            StringWriter sw = new StringWriter(myCItrad);
            sw.WriteLine("<html xmlns:x=\"urn:schemas-microsoft-com:office:excel\">");
            sw.WriteLine("<head>");
            sw.WriteLine("<meta http-equiv=Content-Type content=text/html;charset=utf-8>");
            sw.WriteLine("<!--[if gte mso 9]>");
            sw.WriteLine("<xml>");
            sw.WriteLine(" <x:ExcelWorkbook>");
            sw.WriteLine(" <x:ExcelWorksheets>");
            sw.WriteLine(" <x:ExcelWorksheet>");
            sw.WriteLine(" <x:Name>sheet1</x:Name>");
            sw.WriteLine(" <x:WorksheetOptions>");
            sw.WriteLine(" <x:Print>");
            sw.WriteLine(" <x:ValidPrinterInfo />");
            sw.WriteLine(" </x:Print>");
            sw.WriteLine(" </x:WorksheetOptions>");
            sw.WriteLine(" </x:ExcelWorksheet>");
            sw.WriteLine(" </x:ExcelWorksheets>");
            sw.WriteLine("</x:ExcelWorkbook>");
            sw.WriteLine("</xml>");
            sw.WriteLine("<![endif]-->");
            sw.WriteLine("</head>");
            sw.WriteLine("<body>");
            sw.WriteLine("<table>");
            sw.WriteLine(" <tr>");
            sw.WriteLine("<td>ID</td>");
            sw.WriteLine("<td>Object</td>");
            sw.WriteLine("<td>Type</td>");
            sw.WriteLine("<td>MaxLength</td>");
            sw.WriteLine("<td>Text</td>");
            sw.WriteLine("<td>Translation</td>");
            sw.WriteLine(" </tr>");
            sw.WriteLine(datas);
            sw.WriteLine("</table>");
            sw.WriteLine("</body>");
            sw.WriteLine("</html>");
            sw.Close();
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.Buffer = true;
            System.Web.HttpContext.Current.Response.HeaderEncoding = Encoding.GetEncoding("utf-8");
            System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + excelName + ".xls");
            System.Web.HttpContext.Current.Response.Charset = "";
            System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            System.Web.HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //System.Web.HttpContext.Current.Response.Write("<meta http-equiv=Content-Type content=text/html;charset=utf-8>");
            Page.EnableViewState = false;
            System.Web.HttpContext.Current.Response.Write(sw.ToString());
            System.Web.HttpContext.Current.Response.End(); 
        }
    }
}
