using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Xml;
using System.Text;


namespace ChangeTech.DeveloperWeb.Monitor
{
    public partial class MonitorSendGrid : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string vchar = " ";
            string vchar2 = " ";
            
            byte[] bt = Encoding.ASCII.GetBytes(vchar);
            bt = Encoding.ASCII.GetBytes(vchar2);
            //HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://sendgrid.com/api/stats.get.xml?api_user=changetechmail@gmail.com&api_key=PianoChair18");
            //webRequest.AllowAutoRedirect = false;
            //HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
            //System.IO.Stream stream= webResponse.GetResponseStream();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("https://sendgrid.com/api/stats.get.xml?api_user=changetechmail@gmail.com&api_key=PianoChair18");
            XmlNode deliveredNode = xmlDoc.SelectSingleNode("//stats/day/delivered");
            string sendCount = deliveredNode.InnerText;//7
            XmlNode invalid_emailNode = xmlDoc.SelectSingleNode("//stats/day/invalid_email");
            XmlNode bouncesNode = xmlDoc.SelectSingleNode("//stats/day/bounces");
            XmlNode dateNode = xmlDoc.SelectSingleNode("//stats/day/date");
            XmlNode requestsNode = xmlDoc.SelectSingleNode("//stats/day/requests");
            
        }
    }
}