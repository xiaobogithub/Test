using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ChangeTech.Models;
using Ethos.DependencyInjection;
using System.Xml;
using ChangeTech.Contracts;

namespace ChangeTech.DeveloperWeb
{
    public partial class ProgramReport : PageBase<ActivityLogModels>
    {
        string reslut = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetReports(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
            }
        }
        
        private void GetReports(Guid programguid)
        {
            //XmlDocument doc = Resolve<IProgramService>().GetProgramReport(programguid);
            
            //if (doc.HasChildNodes)
            //{
            //    reslut += "<b>" + doc.FirstChild.Name + "</b><br />";
            //    foreach (XmlAttribute attri in doc.FirstChild.Attributes)
            //    {
            //        if (!attri.Name.Equals("Order"))
            //        {
            //            reslut += attri.Name;
            //            reslut += ": ";
            //            reslut += doc.FirstChild.Attributes[attri.Name].Value;
            //            reslut += "<br />";
            //        }
            //    }

            //    DisPlayContent(doc.FirstChild);
            //}

            //Response.Write(reslut);
        }

        private void DisPlayContent(XmlNode xmlNode)
        {
            if (xmlNode.HasChildNodes)
            {
                foreach (XmlNode node in xmlNode.ChildNodes)
                {
                    reslut += GetBeginTag(node.Name) + node.Name;
                    if (node.Attributes["Order"] != null)
                    {
                        reslut += node.Attributes["Order"].Value;
                    }
                    reslut += GetEndTag(node.Name) + "<br />";
                    foreach (XmlAttribute attri in node.Attributes)
                    {
                        if (!attri.Name.Equals("Order"))
                        {
                            reslut += attri.Name;
                            reslut += ": ";
                            reslut += node.Attributes[attri.Name].Value;
                            reslut += "<br />";
                        }
                    }
                    DisPlayContent(node);
                }
            }
        }

        private string GetBeginTag(string name)
        {
            string result = string.Empty;
            switch (name)
            {
                case "Session": result = "<b><u>"; break;
                case "PageSequence": result = "<b>"; break;
                case "Page": result = "<b><em>"; break;
            }
            return result;
        }

        private string GetEndTag(string name)
        {
            string result = string.Empty;
            switch (name)
            {
                case "Session": result = "</u></b>"; break;
                case "PageSequence": result = "</b>"; break;
                case "Page": result = "</em></b>"; break;
            }
            return result;
        }
    }
}
