using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Contracts;
using System.Xml;
using Ethos.DependencyInjection;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;
using System.Globalization;
using ChangeTech.Models;
using System.IO;

namespace ChangeTech.Services
{
    public class XMLService : ServiceBase, IXMLService
    {
        private static readonly string DefaultFilePath = "Files/TimeZoneOpts.txt";
        private string TimeZoneFilePath
        {
            get
            {
                string filePath = System.Configuration.ConfigurationManager.AppSettings["TimeZoneFilePath"];
                return string.IsNullOrEmpty(filePath) ? DefaultFilePath : filePath;
            }
        }

        public string PraseGraphPage(string originalXMLStr, Guid userGUID, Guid sessionGuid, Guid languageGuid, bool isRetake)
        {
            string resultXML = originalXMLStr;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(originalXMLStr);

            XmlNodeList nodeList = xmlDoc.SelectNodes("//Graphs[Graph]");

            if(nodeList.Count > 0)
            {
                Session sessionEntity = Resolve<ISessionRepository>().GetSessionBySessionGuid(sessionGuid);
                if(!sessionEntity.ProgramReference.IsLoaded)
                {
                    sessionEntity.ProgramReference.Load();
                }
                Guid programGUID = sessionEntity.Program.ProgramGUID;

                
                foreach(XmlNode node in nodeList)
                {
                    string timeRange = node.FirstChild.Attributes["TimeRange"].Value;
                    string timeUnit = node.FirstChild.Attributes["TimeUnit"].Value;
                    string[] timeRangeArray = timeRange.Split(new char[] { '-' });
                    int start = Convert.ToInt32(timeRangeArray[0]);
                    int end = Convert.ToInt32(timeRangeArray[1]);
                    //check the value of "end"
                    end = GetEndData(timeUnit, sessionEntity.Day, end);

                    XmlNodeList graphItemNodes = node.SelectNodes("Graph/Items[Item]/Item");

                    foreach(XmlNode graphItemNode in graphItemNodes)
                    {
                        string dataItemExpression = graphItemNode.Attributes["Expression"].Value;
                        List<string> variables = ParseVariablesFromExpresion(dataItemExpression);
                        string value = string.Empty;
                        for(int index = start; index <= end; index++)
                        {
                            string indexDataValueExpression = dataItemExpression;
                            if (indexDataValueExpression == null)
                                indexDataValueExpression = string.Empty;
                            foreach(string variableName in variables)
                            {
                                double variableValue = 0;
                                switch(timeUnit)
                                {
                                    case "Day":
                                        variableValue = Resolve<IUserPageVariablePerDayService>().GetPageVariableValueOnDay(userGUID, programGUID, variableName, index);
                                        if(variableValue > -1)
                                        {
                                            indexDataValueExpression = indexDataValueExpression.Replace("{V:" + variableName + "}", variableValue.ToString());
                                        }
                                        else
                                        {
                                            if(sessionEntity.Day != index)
                                            {
                                                indexDataValueExpression = string.Empty;
                                            }
                                            else
                                            {
                                                if(!IsOperatedInCurrentSession(userGUID, programGUID, variableName, sessionGuid))
                                                {
                                                    indexDataValueExpression = string.Empty;
                                                }
                                            }
                                        }
                                        break;
                                    case "Week":
                                        bool isOperatedInCurrentSession = IsOperatedInCurrentSession(userGUID, programGUID, variableName, sessionGuid);
                                        string weekVariableValue = Resolve<IUserPageVariablePerDayService>().GetPageVariableValueOnWeek(userGUID, programGUID, variableName, index, Convert.ToInt32(sessionEntity.Day), end, isOperatedInCurrentSession);

                                        if(!string.IsNullOrEmpty(weekVariableValue))
                                        {
                                            if(weekVariableValue != "LastWeek")
                                            {
                                                indexDataValueExpression = indexDataValueExpression.Replace("{V:" + variableName + "}", weekVariableValue);
                                            }
                                        }
                                        else
                                        {
                                            indexDataValueExpression = string.Empty;
                                        }

                                        break;
                                }

                                //if (variableValue > -1)
                                //{
                                //    indexDataValueExpression = dataItemExpression.Replace("{V:" + variableName + "}", variableValue.ToString());
                                //}
                                //else
                                //{
                                //    indexDataValueExpression = string.Empty;
                                //    break;
                                //}
                            }
                            if(!string.IsNullOrEmpty(indexDataValueExpression))
                            {
                                value += indexDataValueExpression + ";";
                            }
                        }
                        if(!string.IsNullOrEmpty(value))
                        {
                            value = value.Remove(value.Length - 1);
                        }
                        graphItemNode.Attributes["Values"].Value = value;
                    }

                    //multi language support               
                    node.FirstChild.Attributes["TimeUnit"].Value = GetSpecialString(languageGuid, node.FirstChild.Attributes["TimeUnit"].Value) + " ";
                    node.FirstChild.Attributes["TimeBaselineUnit"].Value = GetSpecialString(languageGuid, node.FirstChild.Attributes["TimeBaselineUnit"].Value);
                }
            }

            //Tip mesage catching up early day
            xmlDoc = PraseCatchingUpEarlyDayTipMessage(xmlDoc);
            //add TimeZoneOpts to XML
            xmlDoc = AddTimeZoneElementToXML(xmlDoc);
            xmlDoc.FirstChild.Attributes["IsRetake"].Value = isRetake.ToString();

            return xmlDoc.InnerXml;
        }

        public string PraseGraphForOnePage(string originalXMLStr, Guid userGUID, Guid sessionGuid, Guid languageGuid)
        {
            string resultXML = originalXMLStr;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(originalXMLStr);

            XmlNodeList nodeList = xmlDoc.SelectNodes("//Graphs[Graph]");

            if (nodeList.Count > 0)
            {
                Session sessionEntity = Resolve<ISessionRepository>().GetSessionBySessionGuid(sessionGuid);
                if (!sessionEntity.ProgramReference.IsLoaded)
                {
                    sessionEntity.ProgramReference.Load();
                }
                Guid programGUID = sessionEntity.Program.ProgramGUID;

                XmlNode node = nodeList[0];
                string timeRange = node.FirstChild.Attributes["TimeRange"].Value;
                string timeUnit = node.FirstChild.Attributes["TimeUnit"].Value;
                string[] timeRangeArray = timeRange.Split(new char[] { '-' });
                int start = Convert.ToInt32(timeRangeArray[0]);
                int end = Convert.ToInt32(timeRangeArray[1]);
                switch (timeUnit)
                {
                    case "Day":
                        if (end < sessionEntity.Day)
                        {
                            return "OK";
                        }
                        break;
                    case "Week":
                        if (end * 7 < sessionEntity.Day)
                        {
                            return "OK";
                        }
                        break;
                }
                //check the value of "end",only end == sessionEntity.Day 
                end = GetEndData(timeUnit, sessionEntity.Day, end);
                XmlNodeList graphItemNodes = node.SelectNodes("Graph/Items[Item]/Item");
                foreach (XmlNode graphItemNode in graphItemNodes)
                {
                    string dataItemExpression = graphItemNode.Attributes["Expression"].Value;
                    List<string> variables = ParseVariablesFromExpresion(dataItemExpression);
                    string value = string.Empty;
                    //only get current day,it should be end day
                    int index = end;
                    string indexDataValueExpression = dataItemExpression;
                    if (indexDataValueExpression == null)
                        indexDataValueExpression = string.Empty;
                    foreach (string variableName in variables)
                    {
                        double variableValue = 0;
                        switch (timeUnit)
                        {
                            case "Day":
                                variableValue = Resolve<IUserPageVariablePerDayService>().GetPageVariableValueOnDay(userGUID, programGUID, variableName, index);
                                if (variableValue > -1)
                                {
                                    indexDataValueExpression = indexDataValueExpression.Replace("{V:" + variableName + "}", variableValue.ToString());
                                }
                                else
                                {
                                    if (sessionEntity.Day != index)
                                    {
                                        indexDataValueExpression = string.Empty;
                                    }
                                    else
                                    {
                                        if (!IsOperatedInCurrentSession(userGUID, programGUID, variableName, sessionGuid))
                                        {
                                            indexDataValueExpression = string.Empty;
                                        }
                                    }
                                }
                                break;
                            case "Week":
                                bool isOperatedInCurrentSession = IsOperatedInCurrentSession(userGUID, programGUID, variableName, sessionGuid);
                                string weekVariableValue = Resolve<IUserPageVariablePerDayService>().GetPageVariableValueOnWeek(userGUID, programGUID, variableName, index, Convert.ToInt32(sessionEntity.Day), end, isOperatedInCurrentSession);

                                if (!string.IsNullOrEmpty(weekVariableValue))
                                {
                                    if (weekVariableValue != "LastWeek")
                                    {
                                        indexDataValueExpression = indexDataValueExpression.Replace("{V:" + variableName + "}", weekVariableValue);
                                    }
                                }
                                else
                                {
                                    indexDataValueExpression = string.Empty;
                                }

                                break;
                        }

                    }
                    if (!string.IsNullOrEmpty(indexDataValueExpression))
                    {
                        value = indexDataValueExpression;
                    }

                    graphItemNode.Attributes["Values"].Value = value;
                }

                //multi language support               
                node.FirstChild.Attributes["TimeUnit"].Value = GetSpecialString(languageGuid, node.FirstChild.Attributes["TimeUnit"].Value) + " ";
                node.FirstChild.Attributes["TimeBaselineUnit"].Value = GetSpecialString(languageGuid, node.FirstChild.Attributes["TimeBaselineUnit"].Value);

            }

            return xmlDoc.InnerXml;
        }

        public string ParseBeforeAfterShowExpression(string originalXMLStr, Guid sessionGUID, string objects, string programGUID, string userGUID)
        {
            string resultXML = originalXMLStr;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(originalXMLStr);

            XmlNodeList nodeList = xmlDoc.SelectNodes("//Page");
            Session sessionEntity = Resolve<ISessionRepository>().GetSessionBySessionGuid(sessionGUID);

            foreach(XmlNode node in nodeList)
            {
                if (node.Attributes["BeforeExpression"] != null)
                {
                    //switch (objects)
                    //{
                    //    case "Page":
                    //        node.Attributes["BeforeExpression"].Value = string.Empty;
                    //        break;
                    //    default:
                    string beforeExpression = node.Attributes["BeforeExpression"].Value;
                    if (!string.IsNullOrEmpty(beforeExpression))
                    {
                        beforeExpression = beforeExpression.Replace("{V:CurrentDay}", sessionEntity.Day.Value.ToString());
                        beforeExpression = ReplacePageGUIDWithPageNOInExpression(beforeExpression, sessionGUID);
                    }
                    node.Attributes["BeforeExpression"].Value = beforeExpression;
                    //        break;
                    //}
                }

                if (node.Attributes["AfterExpression"] != null)
                {
                    //switch (objects)
                    //{
                    //    case "Page":
                    //        node.Attributes["AfterExpression"].Value = string.Empty;
                    //        break;
                    //    default:
                    string afterExpression = node.Attributes["AfterExpression"].Value;
                    if (!string.IsNullOrEmpty(afterExpression))
                    {
                        afterExpression = afterExpression.Replace("{V:CurrentDay}", sessionEntity.Day.Value.ToString());
                        afterExpression = ReplacePageGUIDWithPageNOInExpression(afterExpression, sessionGUID);
                    }
                    node.Attributes["AfterExpression"].Value = afterExpression;
                    //        break;
                    //}
                }

                //if (node.Attributes["Text"] != null)
                //{
                //    if (!string.IsNullOrEmpty(node.Attributes["Text"].Value) &&
                //        node.Attributes["Text"].Value.Contains("<paylink>") && 
                //        node.Attributes["Text"].Value.Contains("</paylink>"))
                //    {
                //        node.Attributes["Text"].Value = IntegratePaymentLink(node.Attributes["Text"].Value.ToString(), programGUID, userGUID);                            
                //    }
                //} 
            }

            return xmlDoc.InnerXml;
        }

        public string ParseTimePickerQuestion(string originalXMLStr, Guid languageGuid)
        {
            string resultXML = originalXMLStr;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(originalXMLStr);

            XmlNodeList timerPickerHourMinuteNodeList = xmlDoc.SelectNodes("//Question[@Type = \"TimePicker\"]");
            if(timerPickerHourMinuteNodeList.Count > 0)
            {
                SpecialString hourString = Resolve<ISpecialStringRepository>().GetSpecialString(languageGuid, "HOUR_STRING");
                SpecialString hourRange = Resolve<ISpecialStringRepository>().GetSpecialString(languageGuid, "HOUR_RANGE");
                SpecialString minuteString = Resolve<ISpecialStringRepository>().GetSpecialString(languageGuid, "MINUTE_STRING");
                SpecialString minuteRange = Resolve<ISpecialStringRepository>().GetSpecialString(languageGuid, "MINUTE_RANGE");

                foreach(XmlNode node in timerPickerHourMinuteNodeList)
                {
                    node.Attributes["Type"].Value = "TimePicker";
                    XmlNode newNode = xmlDoc.CreateElement("Item");

                    XmlAttribute itemAttribute = xmlDoc.CreateAttribute("Item");
                    itemAttribute.Value = hourString.Value;

                    XmlAttribute rangeAttribute = xmlDoc.CreateAttribute("Range");
                    rangeAttribute.Value = hourRange.Value;

                    newNode.Attributes.Append(itemAttribute);
                    newNode.Attributes.Append(rangeAttribute);
                    node.ChildNodes[0].AppendChild(newNode);

                    newNode = xmlDoc.CreateElement("Item");
                    itemAttribute = xmlDoc.CreateAttribute("Item");
                    itemAttribute.Value = minuteString.Value;
                    rangeAttribute = xmlDoc.CreateAttribute("Range");
                    rangeAttribute.Value = minuteRange.Value;

                    newNode.Attributes.Append(itemAttribute);
                    newNode.Attributes.Append(rangeAttribute);
                    node.ChildNodes[0].AppendChild(newNode);
                }
            }
            return xmlDoc.InnerXml;
        }

        /// <summary>
        /// Get TimeZoneOpts from File
        /// </summary>
        /// <returns></returns>
        public string GetTimeZoneOpts(Guid programGuid)
        {
            Program program = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
            string defaultTimeZoneOpt = program.TimeZone.HasValue ? program.TimeZone.Value.ToString() : "0.00";
            
            string timeZoneOpts = string.Empty;
            string fileFullPath = System.Web.HttpContext.Current.Server.MapPath(TimeZoneFilePath);
            using (FileStream stream = new FileStream(fileFullPath, FileMode.OpenOrCreate, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    timeZoneOpts = reader.ReadToEnd();
                }
            }
            if (program.IsSupportTimeZone==true)
            {
                timeZoneOpts = timeZoneOpts.Replace(" selected='selected'", "");
                timeZoneOpts = timeZoneOpts.Replace('"' + defaultTimeZoneOpt + '"', '"' + defaultTimeZoneOpt + '"' + " selected='selected'");
            }
            return timeZoneOpts;
        }
        //-----------------------------------------
        #region Private methods
        private List<string> ParseVariablesFromExpresion(string expression)
        {
            List<string> variables = new List<string>();

            string[] expressionItems = expression.Split(new char[] { '{', '}' });
            foreach(string expressionItem in expressionItems)
            {
                if(expressionItem.StartsWith("V:"))
                {
                    variables.Add(expressionItem.Substring(2, expressionItem.Length - 2));
                }
            }

            return variables;
        }

        private bool IsOperatedInCurrentSession(Guid userGUID, Guid programGUID, string variableName, Guid sessionGuid)
        {
            bool flag = false;
            ChangeTech.Entities.PageVariable variable = Resolve<IPageVaribleRepository>().GetVariableByProgramGuidAndVariableName(programGUID, variableName);
            if(variable != null)
            {
                UserPageVariablePerDay variablePerDay = Resolve<IUserPageVariablePerDayRepository>().GetUserPageVariable(userGUID, variable.PageVariableGUID, sessionGuid);
                if(variablePerDay != null)
                {
                    flag = true;
                }
            }

            return flag;
        }

        private XmlDocument PraseCatchingUpEarlyDayTipMessage(XmlDocument xmlDoc)
        {
            if(xmlDoc.FirstChild.Attributes["Mode"].Value.Equals("Live"))
            {
                Guid programGuid = new Guid(xmlDoc.FirstChild.Attributes["ProgramGUID"].Value);
                Guid userGuid = new Guid(xmlDoc.FirstChild.Attributes["UserGUID"].Value);
                if(Resolve<IProgramUserService>().IsUserMissedDay(userGuid, programGuid))
                {
                    xmlDoc.FirstChild.Attributes["MissedDay"].Value = "true";
                }
            }
            return xmlDoc;
        }

        /// <summary>
        /// Add TimeZoneOptsXml Element
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <returns></returns>
        private XmlDocument AddTimeZoneElementToXML(XmlDocument xmlDoc)
        {
            Guid programGuid= new Guid(xmlDoc.FirstChild.Attributes["ProgramGUID"].Value);
            Program program = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
            if (program.IsSupportTimeZone.HasValue && program.IsSupportTimeZone==true )
                {
                    XmlNode root = xmlDoc.SelectSingleNode("XMLModel");
                    XmlNode timeZoneNode = xmlDoc.CreateNode(XmlNodeType.Element, "TimeZoneList", "");
                    timeZoneNode.InnerXml = GetTimeZoneOpts(programGuid);
                    root.AppendChild(timeZoneNode);
                }
            return xmlDoc;
        }

        private string GetSpecialString(Guid languageGuid, string name)
        {
            string result = string.Empty;
            SpecialString sString = Resolve<ISpecialStringRepository>().GetSpecialString(languageGuid, name);
            if(sString != null)
            {
                result = sString.Value.Trim();
            }
            return result;
        }

        private string ReplacePageGUIDWithPageNOInExpression(string expression, Guid sessionGUID)
        {
            string markupPrefix = "{Page:";

            int pageGUIDIndex = 0;
            do
            {
                if (expression == null)
                    expression = string.Empty;
                pageGUIDIndex = expression.IndexOf(markupPrefix);
                if(pageGUIDIndex > 0)
                {
                    string pageGUID = expression.Substring(pageGUIDIndex + markupPrefix.Length, 36);

                    Page pageEntity = Resolve<IPageRepository>().GetPageByPageGuid(new Guid(pageGUID));
                    if(!pageEntity.PageSequenceReference.IsLoaded)
                    {
                        pageEntity.PageSequenceReference.Load();
                    }

                    SessionContent sessionContentEntity = Resolve<ISessionContentRepository>().GetSessionContentBySessionGuidPageSequenceGuid(sessionGUID, pageEntity.PageSequence.PageSequenceGUID);
                    if(sessionContentEntity != null)
                    {
                        expression = expression.Replace(markupPrefix + pageGUID + "}", sessionContentEntity.PageSequenceOrderNo + "." + pageEntity.PageOrderNo);
                    }
                    else
                    {
                        pageGUIDIndex = 0;
                    }
                }
            } while(pageGUIDIndex > 0);

            return expression;
        }

        /// <summary>
        /// get the end value
        /// </summary>
        /// <param name="timeUnit">time unit</param>
        /// <param name="currentday">current day</param>
        /// <param name="end">end day for user setting</param>
        /// <returns></returns>
        private int GetEndData(string timeUnit, int? currentday, int end)
        {
            int realEnd = end;
            if (timeUnit.CompareTo("Day") == 0)
            {
                if (currentday.HasValue && (Convert.ToInt32(currentday)) < end)
                {
                    realEnd = Convert.ToInt32(currentday);
                }
            }
            else if (timeUnit.CompareTo("Week") == 0)
            {
                if (currentday.HasValue && (Convert.ToInt32(currentday)) < end * 7)
                {
                    realEnd = Convert.ToInt32(currentday) / 7;
                }
            }
            return realEnd;
        }
        #endregion
    }
}
