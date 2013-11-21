using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Contracts;
using Ethos.DependencyInjection;
using ChangeTech.Models;
using System.Globalization;
using System.IO;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;
using Microsoft.WindowsAzure.StorageClient;
using Ethos.Utility;
using NPOI.HSSF;
using NPOI.Util;
using NPOI.POIFS;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.HPSF;
using System.Xml;
using System.Data;

namespace ChangeTech.Services
{
    public class ExportService : ServiceBase, IExportService
    {
        public const int ExportExcelColumnSize = 255;
        public const int ExportExcelColumnDefultColumnNum = 2;//The user name(email) and the company.
        public const int ExportExcelColumnDefultColumnNumExtension = 7;//The user name(email) and the company.// the new 5 is Status,CurrentDay,RegisterDate,Gender,LastProgramActivityDate

        public const string STATUSQUEUEMAKT = "s";//"status" need be changed to only 1 char, to avoid the queuename beyond 63

        private string versionNumber
        {
            get
            {
                string version = System.Configuration.ConfigurationManager.AppSettings["ProjectVersionWithoutDot"];
                return string.IsNullOrEmpty(version) ? "" : version.ToLower();
            }
        }

        public string AddExportProgramCommand(string fileName, Guid programGUID, Guid lanagueGUID, string startDay, string endDay, bool includeRelapse, bool includeProgramRoom,
        bool includeAccessoryTemplate, bool includeEmailTemplate, bool includeHelpItem, bool includeUserMenu, bool includeTipMessage, bool includeSpecialString)
        {
            string returnMsg = string.Empty;
            string statusQueueName = string.Format("{0}{1}{2}", STATUSQUEUEMAKT, fileName.ToLower().Replace('.', '0'), versionNumber);
            CloudQueue operationQueue = Resolve<IAzureQueueService>().GetCloudQueue(string.Format("operationqueue{0}", versionNumber));

            operationQueue.CreateIfNotExist();

            //IEnumerable<CloudQueueMessage> operationMessagesInQueue = operationQueue.PeekMessages(32);
            //string operationMsgStr = string.Format("{0};{1};{2};{3}", "ExportProgram", programGUID, lanagueGUID, fileName);
            //bool hasOperationOfThisProgram = false;
            //foreach (CloudQueueMessage operationMessage in operationMessagesInQueue)
            //{
            //    if (operationMessage.AsString.StartsWith(operationMsgStr))
            //    {
            //        hasOperationOfThisProgram = true;
            //        break;
            //    }
            //}

            //if (!hasOperationOfThisProgram)
            //{
            string operationMsg = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13}", "ExportProgram", programGUID, lanagueGUID, fileName, startDay, endDay,
                includeRelapse, includeProgramRoom, includeAccessoryTemplate, includeEmailTemplate,
                includeHelpItem, includeUserMenu, includeTipMessage, includeSpecialString);
            Resolve<IAzureQueueService>().AddQueueMessage(operationQueue, operationMsg, false);

            CloudQueue statusQueue = Resolve<IAzureQueueService>().GetCloudQueue(statusQueueName);
            string statusMsg = string.Format("{0}", "Initializing");
            Resolve<IAzureQueueService>().AddQueueMessage(statusQueue, statusMsg, true);
            //}
            //else
            //{
            //    returnMsg = "There is another operation of this program is waiting for processing, please wait for a moment and retry!";
            //}
            return returnMsg;
        }

        public string AddReportProgramCommand(string fileName, Guid programGUID, Guid lanagueGUID)
        {
            string returnMsg = string.Empty;
            string statusQueueName = string.Format("{0}{1}{2}", STATUSQUEUEMAKT, fileName.ToLower().Replace('.', '0'), versionNumber);
            CloudQueue operationQueue = Resolve<IAzureQueueService>().GetCloudQueue(string.Format("operationqueue{0}", versionNumber));

            operationQueue.CreateIfNotExist();

            //IEnumerable<CloudQueueMessage> operationMessagesInQueue = operationQueue.PeekMessages(32);
            //string operationMsgStr = string.Format("{0};{1}", "ReportProgram", programGUID);
            //bool hasOperationOfThisProgram = false;
            //foreach (CloudQueueMessage operationMessage in operationMessagesInQueue)
            //{
            //    if (operationMessage.AsString.StartsWith(operationMsgStr))
            //    {
            //        hasOperationOfThisProgram = true;
            //        break;
            //    }
            //}

            //if (!hasOperationOfThisProgram)
            //{
            string operationMsg = string.Format("{0};{1};{2};{3}", "ReportProgram", programGUID, lanagueGUID, fileName);
            Resolve<IAzureQueueService>().AddQueueMessage(operationQueue, operationMsg, false);

            CloudQueue statusQueue = Resolve<IAzureQueueService>().GetCloudQueue(statusQueueName);
            string statusMsg = string.Format("{0}", "Initializing");
            Resolve<IAzureQueueService>().AddQueueMessage(statusQueue, statusMsg, true);
            //}
            //else
            //{
            //    returnMsg = "There is another operation of this program is waiting for processing, please wait for a moment and retry!";
            //}
            return returnMsg;
        }

        public void ExportProgram(string fileName, Guid programGUID, Guid languageGUID, int startDay, int endDay,
            bool includeRelapse, bool includeProgramRoom, bool includeAccessoryTemplate, bool includeEmailTemplate,
            bool includeHelpItem, bool includeUserMenu, bool includeTipMessage, bool includeSpecialString)
        {
            ProgramModel programModel = Resolve<IProgramService>().GetProgramByProgramGUIDAndLanguageGUID(programGUID, languageGUID);

            string statusMsg = string.Format("{0}", "Get program data");
            string statusQueueName = string.Format("{0}{1}{2}", STATUSQUEUEMAKT, fileName.ToLower().Replace('.', '0'), versionNumber);
            AddStatusMessage(statusQueueName, statusMsg);

            string translationDataStr = Resolve<IStoreProcedure>().GetTranslationData(programModel.Guid, startDay, endDay,
                includeRelapse, includeProgramRoom, includeAccessoryTemplate, includeEmailTemplate,
                includeHelpItem, includeUserMenu, includeTipMessage, includeSpecialString);

            string data = string.Empty;
            StringBuilder rowdataBuilder = new StringBuilder();
            StringBuilder worksheetBuilder = new StringBuilder();

            // Get export work sheet template file
            string exportWorkSheetTemplate = string.Empty;
            string accountName = Resolve<ISystemSettingRepository>().GetSettingValue("AzureStorageAccountName");
            string blobPathRoot = ServiceUtility.GetBlobPath(accountName);
            exportWorkSheetTemplate = Resolve<IResourceService>().GetBlob(string.Format("{0}$root/ExportWorksheetTemplatenew.xml", blobPathRoot));

            // Get session, page seqeunce, page, page question, page question item, preference, graph, graph item
            ParaseSessionData(statusQueueName, translationDataStr, programModel, startDay, exportWorkSheetTemplate, ref worksheetBuilder);

            if (includeRelapse)
            {
                ConstrcutRelapseWorksheet(statusQueueName, programModel, translationDataStr, exportWorkSheetTemplate, ref worksheetBuilder);
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(translationDataStr);
            if (includeProgramRoom)
            {
                XmlNodeList programRoomList = xmlDoc.SelectNodes(string.Format("//ProgramRooms/ProgramRoom"));
                worksheetBuilder.Append(ConstructWorkSheet(exportWorkSheetTemplate, "Program Room", programRoomList, programModel.ProgramName, programModel.DefaultLanguageName, "ProgramRoom"));
            }

            if (includeAccessoryTemplate)
            {
                XmlNodeList accessoryTemplateList = xmlDoc.SelectNodes(string.Format("//AccessoryTemplates/AccessoryTemplate"));
                worksheetBuilder.Append(ConstructWorkSheet(exportWorkSheetTemplate, "Program Template", accessoryTemplateList, programModel.ProgramName, programModel.DefaultLanguageName, "AccessoryTemplate"));
            }

            if (includeEmailTemplate)
            {
                XmlNodeList emailTemplateList = xmlDoc.SelectNodes(string.Format("//EmailTemplates/EmailTemplate"));
                worksheetBuilder.Append(ConstructWorkSheet(exportWorkSheetTemplate, "Email Template", emailTemplateList, programModel.ProgramName, programModel.DefaultLanguageName, "EmailTemplate"));
            }

            if (includeHelpItem)
            {
                XmlNodeList helpItemList = xmlDoc.SelectNodes(string.Format("//HelpItems/HelpItem"));
                worksheetBuilder.Append(ConstructWorkSheet(exportWorkSheetTemplate, "Help Items", helpItemList, programModel.ProgramName, programModel.DefaultLanguageName, "HelpItem"));
            }

            if (includeUserMenu)
            {
                XmlNodeList userMenuList = xmlDoc.SelectNodes(string.Format("//UserMenus/UserMenu"));
                worksheetBuilder.Append(ConstructWorkSheet(exportWorkSheetTemplate, "User Menus", userMenuList, programModel.ProgramName, programModel.DefaultLanguageName, "UserMenu"));
            }

            if (includeTipMessage)
            {
                XmlNodeList tipMessageList = xmlDoc.SelectNodes(string.Format("//TipMessages/TipMessage"));
                worksheetBuilder.Append(ConstructWorkSheet(exportWorkSheetTemplate, "Tip Messages", tipMessageList, programModel.ProgramName, programModel.DefaultLanguageName, "TipMessage"));
            }

            if (includeSpecialString)
            {
                XmlNodeList specialStringList = xmlDoc.SelectNodes(string.Format("//SpecialStrings/SpecialString"));
                worksheetBuilder.Append(ConstructWorkSheet(exportWorkSheetTemplate, "Special Strings", specialStringList, programModel.ProgramName, programModel.DefaultLanguageName, "SpecialString"));
            }

            data = Resolve<IResourceService>().GetBlob(string.Format("{0}$root/ExportWorkBookTemplatenew.xml", blobPathRoot));
            if (data == null)
                data = string.Empty;
            data = data.Replace("[ProgramGUID]", programGUID.ToString());
            data = data.Replace("[LanguageGUID]", languageGUID.ToString());
            data = data.Replace("[Version]", "1.1");
            data = data.Replace("[Worksheets]", worksheetBuilder.ToString());

            //write it out    
            statusMsg = string.Format("{0}", "Writing data");
            AddStatusMessage(statusQueueName, statusMsg);

            Resolve<IResourceService>().SaveExportFile(data, fileName, ".xls");

            statusMsg = string.Format("{0}", "Complete");
            AddStatusMessage(statusQueueName, statusMsg);
        }

        public void ReportProgram(string fileName, Guid programGUID, Guid languageGUID)
        {
            ProgramModel programModel = Resolve<IProgramService>().GetProgramByProgramGUIDAndLanguageGUID(programGUID, languageGUID);
            string statusQueueName = string.Format("{0}{1}{2}", STATUSQUEUEMAKT, fileName.ToLower().Replace('.', '0'), versionNumber);
            CloudQueue statusQueue = Resolve<IAzureQueueService>().GetCloudQueue(statusQueueName);
            string statusMsg = string.Format("{0}", "Get program data");
            Resolve<IAzureQueueService>().AddQueueMessage(statusQueue, statusMsg, true);

            ProgramReportModel reportModel = Resolve<IProgramService>().GetProgramReportModel(programModel.Guid);

            string data = String.Empty;

            string accountName = Resolve<ISystemSettingRepository>().GetSettingValue("AzureStorageAccountName");
            string blobPathRoot = ServiceUtility.GetBlobPath(accountName);

            StringBuilder reportProgramBuilder = new StringBuilder();
            int rowCout = 2;

            foreach (SessionReportModel sessionModel in reportModel.Sessions)
            {
                reportProgramBuilder.Append(ConstructReportRow("Session" + sessionModel.Order, sessionModel.Name, sessionModel.Description));
                rowCout++;

                foreach (PageSequenceReportModel pagesequencemodel in sessionModel.PageSequences)
                {
                    reportProgramBuilder.Append(ConstructReportRow("PageSeuquence" + pagesequencemodel.Order, pagesequencemodel.Name, pagesequencemodel.Description));
                    rowCout++;

                    foreach (PageReportModel pagemodel in pagesequencemodel.Pages)
                    {
                        reportProgramBuilder.Append(ConstructReportRow("", "Page" + pagemodel.Order, "Type: " + pagemodel.Type));
                        rowCout++;

                        reportProgramBuilder.Append(ConstructReportRow("", "Heading", pagemodel.Title));
                        rowCout++;

                        reportProgramBuilder.Append(ConstructReportRow("", "Body", pagemodel.Text));
                        rowCout++;

                        reportProgramBuilder.Append(ConstructReportRow("", "FooterText", pagemodel.FooterText));
                        rowCout++;

                        reportProgramBuilder.Append(ConstructReportRow("", "ButtonCaption", pagemodel.ButtonCaption));
                        rowCout++;

                        reportProgramBuilder.Append(ConstructReportRow("", "BeforeShowExpression", pagemodel.BeforeShowExpression));
                        rowCout++;

                        reportProgramBuilder.Append(ConstructReportRow("", "AfterShowExpression", pagemodel.AfterShowExpression));
                        rowCout++;
                    }
                }
            }
            data = Resolve<IResourceService>().GetBlob(string.Format("{0}$root/ProgramReportTemplate.xml", blobPathRoot));
            if (data == null)
                data = string.Empty;
            data = data.Replace("[ROWCOUNT]", rowCout.ToString());
            data = data.Replace("[DateTime]", DateTime.UtcNow.ToString(new CultureInfo("nb-NO")));
            data = data.Replace("[ProgramName]", reportModel.Name);
            data = data.Replace("[LanguageName]", reportModel.Langeuage);
            data = data.Replace("[ProgramDescription]", reportModel.Description);
            data = data.Replace("[ROWDATA]", reportProgramBuilder.ToString());

            statusMsg = string.Format("{0}", "Writing data");
            Resolve<IAzureQueueService>().AddQueueMessage(statusQueue, statusMsg, true);

            Resolve<IResourceService>().SaveExportFile(data, fileName, ".xls");

            statusMsg = string.Format("{0}", "Complete");
            Resolve<IAzureQueueService>().AddQueueMessage(statusQueue, statusMsg, true);
        }

        public string AddReportProgramUserVariableCommand(string fileName, Guid programGUID)
        {
            string returnMsg = string.Empty;
            string statusQueueName = string.Format("{0}{1}{2}", programGUID, "statusqueue", versionNumber);
            CloudQueue operationQueue = Resolve<IAzureQueueService>().GetCloudQueue(string.Format("operationqueue{0}", versionNumber));

            operationQueue.CreateIfNotExist();

            IEnumerable<CloudQueueMessage> operationMessagesInQueue = operationQueue.PeekMessages(32);
            //string operationMsgStr = string.Format("{0};{1}", "ProgramUserVariable", programGUID);
            //bool hasOperationOfThisProgram = false;
            //foreach (CloudQueueMessage operationMessage in operationMessagesInQueue)
            //{
            //    if (operationMessage.AsString.StartsWith(operationMsgStr))
            //    {
            //        hasOperationOfThisProgram = true;
            //        break;
            //    }
            //}

            //if (!hasOperationOfThisProgram)
            //{
            string operationMsg = string.Format("{0};{1};{2}", "ProgramUserVariable", programGUID, fileName);
            Resolve<IAzureQueueService>().AddQueueMessage(operationQueue, operationMsg, false);

            CloudQueue statusQueue = Resolve<IAzureQueueService>().GetCloudQueue(statusQueueName);
            string statusMsg = string.Format("{0}", "Initializing");
            Resolve<IAzureQueueService>().AddQueueMessage(statusQueue, statusMsg, true);
            //}
            //else
            //{
            //    returnMsg = "There is another operation of this program is waiting for processing, please wait for a moment and retry!";
            //}
            return returnMsg;
        }

        public string AddReportProgramUserVariableExtensionCommand(string fileName, Guid programGUID)
        {
            string returnMsg = string.Empty;
            string statusQueueName = string.Format("{0}{1}{2}", programGUID, "statusqueue", versionNumber);
            CloudQueue operationQueue = Resolve<IAzureQueueService>().GetCloudQueue(string.Format("operationqueue{0}", versionNumber));

            operationQueue.CreateIfNotExist();

            IEnumerable<CloudQueueMessage> operationMessagesInQueue = operationQueue.PeekMessages(32);
            //string operationMsgStr = string.Format("{0};{1}", "ProgramUserVariable", programGUID);
            //bool hasOperationOfThisProgram = false;
            //foreach (CloudQueueMessage operationMessage in operationMessagesInQueue)
            //{
            //    if (operationMessage.AsString.StartsWith(operationMsgStr))
            //    {
            //        hasOperationOfThisProgram = true;
            //        break;
            //    }
            //}

            //if (!hasOperationOfThisProgram)
            //{
            string operationMsg = string.Format("{0};{1};{2}", "ProgramUserVariableExtension", programGUID, fileName);
            Resolve<IAzureQueueService>().AddQueueMessage(operationQueue, operationMsg, false);

            CloudQueue statusQueue = Resolve<IAzureQueueService>().GetCloudQueue(statusQueueName);
            string statusMsg = string.Format("{0}", "Initializing");
            Resolve<IAzureQueueService>().AddQueueMessage(statusQueue, statusMsg, true);
            //}
            //else
            //{
            //    returnMsg = "There is another operation of this program is waiting for processing, please wait for a moment and retry!";
            //}
            return returnMsg;
        }

        //this function is not used anywhere, maybe replaced by ExportUserPageVariable
        public void ExportProgramUserVariable(string fileName, Guid programGUID)
        {
            string statusMsg = string.Format("{0}", "Getting user variable data");
            string statusQueueName = string.Format("{0}{1}", fileName, versionNumber);
            AddStatusMessage(statusQueueName, statusMsg);

            HSSFWorkbook hssfworkbook = new HSSFWorkbook();

            //create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "ChangeTech As";
            hssfworkbook.DocumentSummaryInformation = dsi;
            //create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "Program Variable Data";
            hssfworkbook.SummaryInformation = si;
            Row row = null;
            Cell cell = null;
            Sheet sheet = null;

            string variableType_Program = VariableTypeEnum.Program.ToString();
            List<ChangeTech.Entities.PageVariable> variables = Resolve<IPageVaribleRepository>().GetPageVariableByProgram(programGUID).Where(pv => pv.PageVariableType == variableType_Program).OrderBy(p => p.Name).ToList();
            List<ProgramUser> users = Resolve<IProgramUserRepository>().GetProgramUser(programGUID).ToList();

            int rowIndex = 0;
            int columnIndex = 0;
            int sheetIndex = 0;
            int sheetCount = variables.Count / 10;

            for (sheetIndex = 0; sheetIndex <= sheetCount; sheetIndex++)
            {
                sheet = hssfworkbook.CreateSheet(string.Format("Variable Value {0}", sheetIndex));
            }

            sheetIndex = 0;
            foreach (ChangeTech.Entities.PageVariable variable in variables)
            {
                if (columnIndex % 11 == 0)
                {
                    sheet = hssfworkbook.GetSheetAt(sheetIndex);
                    row = sheet.CreateRow(rowIndex);
                    columnIndex = 0;
                    sheetIndex++;
                    columnIndex++;
                }

                cell = row.CreateCell(columnIndex);
                cell.SetCellValue(variable.Name);
                int columnWidth = (Encoding.GetEncoding(20285).GetBytes(variable.Name).Length + 1) * 256;
                sheet.SetColumnWidth(columnIndex, columnWidth);
                columnIndex++;
            }
            rowIndex = 0;
            foreach (ProgramUser user in users)
            {
                if (!user.Status.Equals("Screening"))
                {
                    rowIndex++;

                    if (!user.UserReference.IsLoaded)
                    {
                        user.UserReference.Load();
                    }
                    columnIndex = 0;
                    sheetIndex = 0;

                    statusMsg = string.Format("Getting {0}'s data", user.User.Email);
                    AddStatusMessage(statusQueueName, statusMsg);

                    foreach (ChangeTech.Entities.PageVariable variable in variables)
                    {
                        if (columnIndex % 11 == 0)
                        {
                            sheet = hssfworkbook.GetSheetAt(sheetIndex);
                            row = sheet.CreateRow(rowIndex);
                            sheetIndex++;

                            columnIndex = 0;
                            cell = row.CreateCell(columnIndex);
                            cell.SetCellValue(user.User.Email);
                            int columnWidth = (Encoding.GetEncoding(20285).GetBytes(user.User.Email).Length + 1) * 256;
                            sheet.SetColumnWidth(columnIndex, columnWidth);
                            columnIndex++;
                        }

                        cell = row.CreateCell(columnIndex);
                        columnIndex++;
                        UserPageVariable userPageVariable = Resolve<IUserPageVariableRepository>().GetUserPageVariableByUserAndVariable(user.User.UserGUID, variable.PageVariableGUID);
                        if (userPageVariable != null)
                        {
                            if (!string.IsNullOrEmpty(userPageVariable.Value))
                            {
                                cell.SetCellValue(userPageVariable.Value);
                                cell.SetCellType(CellType.STRING);
                            }
                            else
                            {
                                if (!userPageVariable.QuestionAnswerReference.IsLoaded)
                                {
                                    userPageVariable.QuestionAnswerReference.Load();
                                }

                                if (userPageVariable.QuestionAnswer != null)
                                {
                                    if (!userPageVariable.QuestionAnswer.PageQuestionReference.IsLoaded)
                                    {
                                        userPageVariable.QuestionAnswer.PageQuestionReference.Load();
                                    }
                                    if (!userPageVariable.QuestionAnswer.PageQuestion.QuestionReference.IsLoaded)
                                    {
                                        userPageVariable.QuestionAnswer.PageQuestion.QuestionReference.Load();
                                    }
                                    if (!userPageVariable.QuestionAnswer.PageQuestion.PageReference.IsLoaded)
                                    {
                                        userPageVariable.QuestionAnswer.PageQuestion.PageReference.Load();
                                    }
                                    if (!userPageVariable.QuestionAnswer.QuestionAnswerValue.IsLoaded)
                                    {
                                        userPageVariable.QuestionAnswer.QuestionAnswerValue.Load();
                                    }

                                    if (userPageVariable.QuestionAnswer.PageQuestion.Page != null)
                                    {
                                        if (userPageVariable.QuestionAnswer.QuestionAnswerValue.Count() > 0)
                                        {
                                            string answerValue = userPageVariable.QuestionAnswer.QuestionAnswerValue.First().UserInput;
                                            if (!string.IsNullOrEmpty(answerValue))
                                            {
                                                cell.SetCellValue(answerValue);
                                            }
                                            else
                                            {
                                                cell.SetCellValue("");
                                            }
                                            cell.SetCellType(CellType.STRING);
                                        }
                                    }
                                    else if (userPageVariable.QuestionAnswer.PageQuestion.Question.HasSubItem)
                                    {
                                        int score = 0;
                                        foreach (QuestionAnswerValue answerValue in userPageVariable.QuestionAnswer.QuestionAnswerValue)
                                        {
                                            if (!answerValue.PageQuestionItemReference.IsLoaded)
                                            {
                                                answerValue.PageQuestionItemReference.Load();
                                            }
                                            score += answerValue.PageQuestionItem.Score.HasValue ? answerValue.PageQuestionItem.Score.Value : 0;
                                        }
                                        cell.SetCellValue(score);
                                        cell.SetCellType(CellType.NUMERIC);
                                    }
                                    else if (userPageVariable.QuestionAnswer.PageQuestion.Question.Name.EndsWith("Numeric") ||
                                        userPageVariable.QuestionAnswer.PageQuestion.Question.Name.EndsWith("TimePicker"))
                                    {
                                        string answerValue = userPageVariable.QuestionAnswer.QuestionAnswerValue.First().UserInput;
                                        if (!string.IsNullOrEmpty(answerValue))
                                        {
                                            cell.SetCellValue(Convert.ToInt32(answerValue));
                                        }
                                        cell.SetCellType(CellType.NUMERIC);
                                    }
                                    else if (userPageVariable.QuestionAnswer.PageQuestion.Question.Name.EndsWith("Singleline") ||
                                        userPageVariable.QuestionAnswer.PageQuestion.Question.Name.EndsWith("Multiline"))
                                    {
                                        string answerValue = userPageVariable.QuestionAnswer.QuestionAnswerValue.First().UserInput;
                                        if (!string.IsNullOrEmpty(answerValue))
                                        {
                                            cell.SetCellValue(answerValue);
                                        }
                                        cell.SetCellType(CellType.STRING);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            using (MemoryStream ms = new MemoryStream())
            {
                hssfworkbook.Write(ms);
                Resolve<IResourceService>().SaveUserVariableFile(ms, fileName);
            }

            statusMsg = string.Format("{0}", "Complete");
            AddStatusMessage(statusQueueName, statusMsg);
        }

        //when update ExportUserPageVariable ,pls consider the ExportUserPageVariableExtension whether to change.
        public void ExportUserPageVariable(string fileName, Guid programGUID)
        {
            string statusQueueName = string.Format("{0}{1}{2}", programGUID, "statusqueue", versionNumber);
            string statusMsg = string.Format("{0}", "Getting user variable data");
            AddStatusMessage(statusQueueName, statusMsg);
            try
            {
                DataTable db = Resolve<IPageVariableService>().GetProgramUserPageVariable(programGUID);

                if (db != null)
                {
                    HSSFWorkbook hssfworkbook = new HSSFWorkbook();

                    //create a entry of DocumentSummaryInformation
                    DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                    dsi.Company = "ChangeTech As";
                    hssfworkbook.DocumentSummaryInformation = dsi;
                    //create a entry of SummaryInformation
                    SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                    si.Subject = "Program Variable Data";
                    hssfworkbook.SummaryInformation = si;

                    //begin 
                    if (db.Columns.Count < 256)
                    {
                        Sheet variableSheet = hssfworkbook.CreateSheet("Page variable");

                        //create header
                        int rowNumber = 0;
                        Row row1 = variableSheet.CreateRow(rowNumber);
                        rowNumber++;
                        int columnNumber = 0;
                        foreach (DataColumn dataColumn1 in db.Columns)
                        {
                            row1.CreateCell(columnNumber).SetCellValue(dataColumn1.ColumnName);
                            columnNumber++;
                        }

                        foreach (DataRow dataRow1 in db.Rows)
                        {
                            columnNumber = 0;
                            Row excelRow = variableSheet.CreateRow(rowNumber);
                            foreach (DataColumn dataColumn2 in db.Columns)
                            {
                                excelRow.CreateCell(columnNumber).SetCellValue(dataRow1[dataColumn2.ColumnName].ToString());
                                columnNumber++;
                            }
                            rowNumber++;
                        }
                    }
                    else//columns > 255, create more sheet to store it.
                    {
                        //int sheetCount = db.Columns.Count / 255 + (db.Columns.Count % 255 == 0 ? 0 : 1);
                        int sheetCount = ((db.Columns.Count - ExportExcelColumnDefultColumnNum) + (ExportExcelColumnSize - ExportExcelColumnDefultColumnNum - 1)) / (ExportExcelColumnSize - ExportExcelColumnDefultColumnNum);
                        for (int i = 0; i < sheetCount; i++)
                        {
                            int SheetNum = i + 1;
                            Sheet variableSheet = hssfworkbook.CreateSheet("Page variable " + SheetNum);
                            if (i == 0)
                            {
                                //create header
                                int rowNumber = 0;
                                Row startRow = variableSheet.CreateRow(rowNumber);
                                rowNumber++;
                                int columnNumber = 0;
                                for (int j = i * 255; j < ((i * 255 + 255) < db.Columns.Count ? (i * 255 + 255) : db.Columns.Count); j++)
                                {
                                    startRow.CreateCell(columnNumber).SetCellValue(db.Columns[j].ColumnName);
                                    columnNumber++;
                                }

                                foreach (DataRow dataRow1 in db.Rows)
                                {
                                    columnNumber = 0;
                                    Row excelRow = variableSheet.CreateRow(rowNumber);

                                    for (int j = i * 255; j < ((i * 255 + 255) < db.Columns.Count ? (i * 255 + 255) : db.Columns.Count); j++)
                                    {
                                        excelRow.CreateCell(columnNumber).SetCellValue(dataRow1[db.Columns[j].ColumnName].ToString());
                                        columnNumber++;
                                    }

                                    rowNumber++;
                                }
                            }
                            else//from the second sheet, need add two columns of email and company.
                            {
                                //create header
                                int rowNumber = 0;
                                Row startRow = variableSheet.CreateRow(rowNumber);
                                rowNumber++;
                                int columnNumber = 0;
                                for (int j = i * 253 + 2; j < ((i * 253 + 2 + 253) < db.Columns.Count ? (i * 253 + 2 + 253) : db.Columns.Count); j++)
                                {
                                    if (columnNumber < 2)
                                    {
                                        startRow.CreateCell(columnNumber).SetCellValue(db.Columns[columnNumber].ColumnName);
                                        j--;
                                    }
                                    else
                                    {
                                        startRow.CreateCell(columnNumber).SetCellValue(db.Columns[j].ColumnName);
                                    }
                                    columnNumber++;
                                }

                                foreach (DataRow dataRow1 in db.Rows)
                                {
                                    columnNumber = 0;
                                    Row excelRow = variableSheet.CreateRow(rowNumber);

                                    for (int j = i * 253 + 2; j < ((i * 253 + 2 + 253) < db.Columns.Count ? (i * 253 + 2 + 253) : db.Columns.Count); j++)
                                    {
                                        if (columnNumber < 2)
                                        {
                                            excelRow.CreateCell(columnNumber).SetCellValue(dataRow1[db.Columns[columnNumber].ColumnName].ToString());
                                            j--;
                                        }
                                        else
                                        {
                                            excelRow.CreateCell(columnNumber).SetCellValue(dataRow1[db.Columns[j].ColumnName].ToString());
                                        }

                                        columnNumber++;
                                    }

                                    rowNumber++;
                                }
                            }
                        }
                    }

                    //end

                    using (MemoryStream ms = new MemoryStream())
                    {
                        hssfworkbook.Write(ms);
                        Resolve<IResourceService>().SaveUserVariableFile(ms, fileName);
                    }

                    statusMsg = string.Format("{0}", "Complete");
                    AddStatusMessage(statusQueueName, statusMsg);
                }
            }
            catch (Exception ex)
            {
                statusMsg = ex.ToString();
                AddStatusMessage(statusQueueName, statusMsg);
            }
        }

        public void ExportUserPageVariableExtension(string fileName, Guid programGUID)
        {
            string statusQueueName = string.Format("{0}{1}{2}", programGUID, "statusqueue", versionNumber);
            string statusMsg = string.Format("{0}", "Getting user variable data");
            AddStatusMessage(statusQueueName, statusMsg);
            try
            {
                DataTable db = Resolve<IPageVariableService>().GetProgramUserPageVariableExtension(programGUID);

                if (db != null)
                {
                    HSSFWorkbook hssfworkbook = new HSSFWorkbook();

                    //create a entry of DocumentSummaryInformation
                    DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                    dsi.Company = "ChangeTech As";
                    hssfworkbook.DocumentSummaryInformation = dsi;
                    //create a entry of SummaryInformation
                    SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                    si.Subject = "Program Variable Data";
                    hssfworkbook.SummaryInformation = si;

                    //begin 
                    if (db.Columns.Count < 256)
                    {
                        Sheet variableSheet = hssfworkbook.CreateSheet("Page variable");

                        //create header
                        int rowNumber = 0;
                        Row row1 = variableSheet.CreateRow(rowNumber);
                        rowNumber++;
                        int columnNumber = 0;
                        foreach (DataColumn dataColumn1 in db.Columns)
                        {
                            row1.CreateCell(columnNumber).SetCellValue(dataColumn1.ColumnName);
                            columnNumber++;
                        }

                        foreach (DataRow dataRow1 in db.Rows)
                        {
                            columnNumber = 0;
                            Row excelRow = variableSheet.CreateRow(rowNumber);
                            foreach (DataColumn dataColumn2 in db.Columns)
                            {
                                excelRow.CreateCell(columnNumber).SetCellValue(dataRow1[dataColumn2.ColumnName].ToString());
                                columnNumber++;
                            }
                            rowNumber++;
                        }
                    }
                    else//columns > 255, create more sheet to store it.
                    {
                        int sheetCount = ((db.Columns.Count - ExportExcelColumnDefultColumnNumExtension) + (ExportExcelColumnSize - ExportExcelColumnDefultColumnNumExtension - 1)) / (ExportExcelColumnSize - ExportExcelColumnDefultColumnNumExtension);
                        for (int i = 0; i < sheetCount; i++)
                        {
                            int SheetNum = i + 1;
                            Sheet variableSheet = hssfworkbook.CreateSheet("Page variable " + SheetNum);
                            if (i == 0)
                            {
                                //create header
                                int rowNumber = 0;
                                Row startRow = variableSheet.CreateRow(rowNumber);
                                rowNumber++;
                                int columnNumber = 0;
                                for (int j = i * 255; j < ((i * 255 + 255) < db.Columns.Count ? (i * 255 + 255) : db.Columns.Count); j++)
                                {
                                    startRow.CreateCell(columnNumber).SetCellValue(db.Columns[j].ColumnName);
                                    columnNumber++;
                                }

                                foreach (DataRow dataRow1 in db.Rows)
                                {
                                    columnNumber = 0;
                                    Row excelRow = variableSheet.CreateRow(rowNumber);

                                    for (int j = i * 255; j < ((i * 255 + 255) < db.Columns.Count ? (i * 255 + 255) : db.Columns.Count); j++)
                                    {
                                        excelRow.CreateCell(columnNumber).SetCellValue(dataRow1[db.Columns[j].ColumnName].ToString());
                                        columnNumber++;
                                    }

                                    rowNumber++;
                                }
                            }
                            else//from the second sheet, need add 7 columns of email and company ,and ...
                            {
                                //create header
                                int rowNumber = 0;
                                Row startRow = variableSheet.CreateRow(rowNumber);
                                rowNumber++;
                                int columnNumber = 0;
                                for (int j = i * 248 + 7; j < ((i * 248 + 7 + 248) < db.Columns.Count ? (i * 248 + 7 + 248) : db.Columns.Count); j++)
                                {
                                    if (columnNumber < 7)
                                    {
                                        startRow.CreateCell(columnNumber).SetCellValue(db.Columns[columnNumber].ColumnName);
                                        j--;
                                    }
                                    else
                                    {
                                        startRow.CreateCell(columnNumber).SetCellValue(db.Columns[j].ColumnName);
                                    }
                                    columnNumber++;
                                }

                                foreach (DataRow dataRow1 in db.Rows)
                                {
                                    columnNumber = 0;
                                    Row excelRow = variableSheet.CreateRow(rowNumber);

                                    for (int j = i * 248 + 7; j < ((i * 248 + 7 + 248) < db.Columns.Count ? (i * 248 + 7 + 248) : db.Columns.Count); j++)
                                    {
                                        if (columnNumber < 7)
                                        {
                                            excelRow.CreateCell(columnNumber).SetCellValue(dataRow1[db.Columns[columnNumber].ColumnName].ToString());
                                            j--;
                                        }
                                        else
                                        {
                                            excelRow.CreateCell(columnNumber).SetCellValue(dataRow1[db.Columns[j].ColumnName].ToString());
                                        }

                                        columnNumber++;
                                    }

                                    rowNumber++;
                                }
                            }
                        }
                    }

                    //end

                    using (MemoryStream ms = new MemoryStream())
                    {
                        hssfworkbook.Write(ms);
                        Resolve<IResourceService>().SaveUserVariableFile(ms, fileName);
                    }

                    statusMsg = string.Format("{0}", "Complete");
                    AddStatusMessage(statusQueueName, statusMsg);
                }
            }
            catch (Exception ex)
            {
                statusMsg = ex.ToString();
                AddStatusMessage(statusQueueName, statusMsg);
            }
        }

        private void ParaseSessionData(string statusQueueName, string translationDataStr, ProgramModel programModel, int startDay, string exportWorkSheetTemplate, ref StringBuilder worksheetBuilder)
        {
            string statusMsg = string.Format("{0}", "Parase data");
            AddStatusMessage(statusQueueName, statusMsg);

            StringBuilder rowdataBuilder = new StringBuilder();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(translationDataStr);
            XmlNodeList sessionNodes = xmlDoc.SelectNodes("//Sessions/Session");

            int rowCount = 0;
            //The from day is not always 0
            int dayCount = startDay;
            foreach (XmlNode sessionModel in sessionNodes)
            {
                string sessionID = sessionModel.Attributes["ID"].Value;
                AddStatusMessage(statusQueueName, statusMsg);

                rowdataBuilder = new StringBuilder();
                rowCount = 0;

                rowdataBuilder.Append(ConstructRow(sessionModel, "Session", "Name", "500", true));
                rowCount++;

                rowdataBuilder.Append(ConstructRow(sessionModel, "Session", "Description", "1000", false));
                rowCount++;

                ParasePageSequenceData(translationDataStr, sessionID, ref rowdataBuilder, ref rowCount, false);

                string sessionWorksheet = exportWorkSheetTemplate;
                if (sessionWorksheet == null)
                    sessionWorksheet = string.Empty;
                sessionWorksheet = sessionWorksheet.Replace("[RowCount]", (rowCount + 3).ToString());
                sessionWorksheet = sessionWorksheet.Replace("[DateTime]", DateTime.UtcNow.ToString(new CultureInfo("nb-NO")));
                sessionWorksheet = sessionWorksheet.Replace("[ProgramName]", programModel.ProgramName);
                sessionWorksheet = sessionWorksheet.Replace("[LanguageName]", programModel.DefaultLanguageName);
                sessionWorksheet = sessionWorksheet.Replace("[WorksheetName]", string.Format("Day {0}", dayCount++));
                sessionWorksheet = sessionWorksheet.Replace("[RowData]", rowdataBuilder.ToString());
                worksheetBuilder.Append(sessionWorksheet);
            }
        }

        private void ConstrcutRelapseWorksheet(string statusQueueName, ProgramModel programModel, string translationDataStr, string exportWorkSheetTemplate, ref StringBuilder worksheetBuilder)
        {
            string statusMsg = string.Format("{0}", "Parase Relapse");
            AddStatusMessage(statusQueueName, statusMsg);
            StringBuilder rowdataBuilder = new StringBuilder();
            int rowCount = 0;
            ParasePageSequenceData(translationDataStr, "0", ref rowdataBuilder, ref rowCount, true);

            string sessionWorksheet = exportWorkSheetTemplate;
            if (sessionWorksheet == null)
                sessionWorksheet = string.Empty;
            sessionWorksheet = sessionWorksheet.Replace("[RowCount]", (rowCount + 3).ToString());
            sessionWorksheet = sessionWorksheet.Replace("[DateTime]", DateTime.UtcNow.ToString(new CultureInfo("nb-NO")));
            sessionWorksheet = sessionWorksheet.Replace("[ProgramName]", programModel.ProgramName);
            sessionWorksheet = sessionWorksheet.Replace("[LanguageName]", programModel.DefaultLanguageName);
            sessionWorksheet = sessionWorksheet.Replace("[WorksheetName]", string.Format("Relapse"));
            sessionWorksheet = sessionWorksheet.Replace("[RowData]", rowdataBuilder.ToString());
            worksheetBuilder.Append(sessionWorksheet);
        }

        private void ParasePageSequenceData(string translationDataStr, string sessionID, ref StringBuilder rowdataBuilder, ref int rowCount, bool isRelapse)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(translationDataStr);
            XmlNodeList pageSequenceNodes = null;
            if (isRelapse)
            {
                pageSequenceNodes = xmlDoc.SelectNodes(string.Format("//Relapses/Relapse[@ParentGUID=\"{0}\"]", sessionID));
            }
            else
            {
                pageSequenceNodes = xmlDoc.SelectNodes(string.Format("//PageSequences/PageSequence[@ParentGUID=\"{0}\"]", sessionID));
            }

            foreach (XmlNode pageSequenceModel in pageSequenceNodes)
            {
                rowdataBuilder.Append(ConstructRow(pageSequenceModel, "PageSequence", "Name", "", true));
                rowCount++;

                rowdataBuilder.Append(ConstructRow(pageSequenceModel, "PageSequence", "Description", "", false));
                rowCount++;

                XmlNodeList pageNodes = xmlDoc.SelectNodes(string.Format("//PageContents/PageContent[@ParentGUID=\"{0}\"]", pageSequenceModel.Attributes["ID"].Value));
                //List<TranslationModel> pageList = translationModelList.Where(s => s.Object.Equals("PageContent") && s.ParentGUID.ToString().Equals(pageSequenceModel.ID) && s.Type.Equals("Heading")).ToList();
                foreach (XmlNode pageModel in pageNodes)
                {
                    rowdataBuilder.Append(ConstructRow(pageModel, "PageContent", "Heading", "", true));
                    rowCount++;

                    rowdataBuilder.Append(ConstructRow(pageModel, "PageContent", "Body", "", false));
                    rowCount++;

                    rowdataBuilder.Append(ConstructRow(pageModel, "PageContent", "FooterText", "", false));
                    rowCount++;

                    rowdataBuilder.Append(ConstructRow(pageModel, "PageContent", "PrimaryButtonCaption", "80", false));
                    rowCount++;

                    XmlNodeList pageQuestionNodes = xmlDoc.SelectNodes(string.Format("//PageQuestionContents/PageQuestionContent[@ParentGUID=\"{0}\"]", pageModel.Attributes["ID"].Value));
                    //List<TranslationModel> questionList = translationModelList.Where(s => s.Object.Equals("PageQuestionContent") && s.ParentGUID.ToString().Equals(pageModel.ID) && s.Type.Equals("Caption")).ToList();
                    foreach (XmlNode questionModel in pageQuestionNodes)
                    {
                        rowdataBuilder.Append(ConstructRow(questionModel, "PageQuestionContent", "Caption", "1024", true));
                        rowCount++;

                        rowdataBuilder.Append(ConstructRow(questionModel, "PageQuestionContent", "DisableCheckBox", "250", true));
                        rowCount++;

                        XmlNodeList questionItemNodes = xmlDoc.SelectNodes(string.Format("//PageQuestionItemContents/PageQuestionItemContent[@ParentGUID=\"{0}\"]", questionModel.Attributes["ID"].Value));
                        //List<TranslationModel> questionItemList = translationModelList.Where(s => s.Object.Equals("PageQuestionItemContent") && s.ParentGUID.ToString().Equals(questionModel.ID)).ToList();
                        foreach (XmlNode questionItemModel in questionItemNodes)
                        {
                            rowdataBuilder.Append(ConstructRow(questionItemModel, "PageQuestionItemContent", "Item", "1024", false));
                            rowCount++;

                            rowdataBuilder.Append(ConstructRow(questionItemModel, "PageQuestionItemContent", "Feedback", "1024", false));
                            rowCount++;
                        }
                    }

                    XmlNodeList graphNodes = xmlDoc.SelectNodes(string.Format("//GraphContents/GraphContent[@ParentGUID=\"{0}\"]", pageModel.Attributes["ID"].Value));
                    //List<TranslationModel> graphList = translationModelList.Where(s => s.Object.Equals("GraphContent") && s.ParentGUID.ToString().Equals(pageModel.ID)).ToList();
                    foreach (XmlNode graphModel in graphNodes)
                    {
                        rowdataBuilder.Append(ConstructRow(graphModel, "GraphContent", "Caption", "200", true));
                        rowCount++;

                        //List<TranslationModel> graphItemList = translationModelList.Where(s => s.Object.Equals("GraphItemContent") && s.ParentGUID.ToString().Equals(graphModel.ID)).ToList();
                        XmlNodeList graphItemNodes = xmlDoc.SelectNodes(string.Format("//GraphItemContents/GraphItemContent[@ParentGUID=\"{0}\"]", graphModel.Attributes["ID"].Value));
                        foreach (XmlNode graphItemModel in graphItemNodes)
                        {
                            rowdataBuilder.Append(ConstructRow(graphItemModel, "GraphItemContent", "Name", "200", false));
                            rowCount++;
                        }
                    }

                    XmlNodeList preferenceNodes = xmlDoc.SelectNodes(string.Format("//Preferences/Preference[@ParentGUID=\"{0}\"]", pageModel.Attributes["ID"].Value));
                    //List<TranslationModel> preferenceList = translationModelList.Where(s => s.Object.Equals("Preferences") && s.ParentGUID.ToString().Equals(pageModel.ID)).ToList();
                    foreach (XmlNode preferenceModel in preferenceNodes)
                    {
                        rowdataBuilder.Append(ConstructRow(preferenceModel, "Preferences", "Name", "50", true));
                        rowCount++;

                        rowdataBuilder.Append(ConstructRow(preferenceModel, "Preferences", "Description", "200", false));
                        rowCount++;

                        rowdataBuilder.Append(ConstructRow(preferenceModel, "Preferences", "AnswerText", "200", false));
                        rowCount++;

                        rowdataBuilder.Append(ConstructRow(preferenceModel, "Preferences", "ButtonName", "200", false));
                        rowCount++;
                    }
                }
            }
        }

        private string ConstructReportRow(string entity, string name, string text)
        {
            StringBuilder rowbuilder = new StringBuilder();
            string styleId = "s62";
            if (entity.StartsWith("Session"))
            {
                styleId = "s150";
            }
            if (entity.StartsWith("PageSeuquence"))
            {
                styleId = "s151";
            }
            if (name.StartsWith("Page"))
            {
                styleId = "s152";
            }

            rowbuilder.AppendLine(string.Format("<Row ss:Height=\"{0}\">", GetHeightReport(text)));
            if (entity.StartsWith("Session") || entity.StartsWith("PageSeuquence"))
            {
                rowbuilder.AppendFormat("<Cell ss:StyleID=\"{0}\"><Data ss:Type=\"String\">{1}</Data></Cell>", styleId, entity);
            }
            else
            {
                rowbuilder.AppendFormat("<Cell ss:StyleID=\"{0}\"><Data ss:Type=\"String\">{1}</Data></Cell>", "s62", entity);
            }
            rowbuilder.AppendFormat("<Cell ss:StyleID=\"{0}\"><Data ss:Type=\"String\">{1}</Data></Cell>", styleId, name);
            if (!string.IsNullOrEmpty(text))
            {
                text = text.Replace("\r\n", "&#10;").Replace("\r", "&#10;");
            }
            rowbuilder.AppendFormat("<Cell ss:StyleID=\"{0}\"><Data ss:Type=\"String\">{1}</Data></Cell>", styleId, text);
            rowbuilder.AppendLine("</Row>");
            return rowbuilder.ToString();
        }

        private string ConstructRow(TranslationModel model, bool displayObject)
        {
            StringBuilder rowdataBuilder = new StringBuilder();
            rowdataBuilder.AppendLine(string.Format("<Row ss:AutoFitHeight=\"1\" ID=\"{0}\" Object=\"{1}\" Type=\"{2}\" ss:Height=\"{3}\">", model.ID, model.Object, model.Type, GetHeight(model.Text)));
            rowdataBuilder.AppendLine(string.Format("<Cell ss:StyleID=\"s68\"><Data ss:Type=\"String\">{0}</Data></Cell>", model.ID));
            //if (displayObject)
            //{
            rowdataBuilder.AppendLine(string.Format("<Cell ss:StyleID=\"s68\"><Data ss:Type=\"String\">{0}</Data></Cell>", model.Object));
            //}
            //else
            //{
            //    rowdataBuilder.AppendLine(string.Format("<Cell ss:StyleID=\"s68\"><Data ss:Type=\"String\">{0}</Data></Cell>", ""));
            //}
            rowdataBuilder.AppendLine(string.Format("<Cell ss:StyleID=\"s68\"><Data ss:Type=\"String\">{0}</Data></Cell>", model.Type));
            if (!string.IsNullOrEmpty(model.Text))
            {
                model.Text = model.Text.Replace("& ", "&amp; ").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;");
                model.Text = model.Text.Replace("\n", "&#10;").Replace("\r", "&#13;").Replace("\t", "&#9;");
                model.Text = model.Text.Replace("<B>", "<b>").Replace("<MEDIUM>", "medium").Replace("</B>", "</b>").Replace("<MEDIUM>", "</MEDIUM>");
            }
            rowdataBuilder.AppendLine(string.Format("<Cell ss:StyleID=\"s69\"><Data ss:Type=\"String\">{0}</Data></Cell>", model.Text));
            rowdataBuilder.AppendLine(string.Format("<Cell ss:StyleID=\"s69\"><Data ss:Type=\"String\">{0}</Data></Cell>", model.Text));
            rowdataBuilder.AppendLine(string.Format("<Cell ss:StyleID=\"s69\"><Data ss:Type=\"String\">{0}</Data></Cell>", model.MaxLength));
            rowdataBuilder.AppendLine("</Row>");
            return rowdataBuilder.ToString();
        }

        private string ConstructRow(XmlNode node, string objectName, string filedName, string maxLength, bool displayObject)
        {
            StringBuilder rowdataBuilder = new StringBuilder();
            string text = string.Empty;
            if (node.Attributes[filedName] != null)
            {
                text = node.Attributes[filedName].InnerXml;
                //don't know why comment this line.
                text = text.Replace("& ", "&amp; ").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;");
                text = text.Replace("\n", "&#10;").Replace("\r", "&#13;").Replace("\t", "&#9;");
                text = text.Replace("<B>", "<b>").Replace("<MEDIUM>", "medium").Replace("</B>", "</b>").Replace("<MEDIUM>", "</MEDIUM>");
            }
            rowdataBuilder.AppendLine(string.Format("<Row ss:AutoFitHeight=\"1\" ID=\"{0}\" Object=\"{1}\" Type=\"{2}\" ss:Height=\"{3}\">", node.Attributes["ID"].Value, objectName, filedName, GetHeight(text)));
            rowdataBuilder.AppendLine(string.Format("<Cell ss:StyleID=\"s68\"><Data ss:Type=\"String\">{0}</Data></Cell>", node.Attributes["ID"].Value));

            //if (displayObject)
            //{
            rowdataBuilder.AppendLine(string.Format("<Cell ss:StyleID=\"s68\"><Data ss:Type=\"String\">{0}</Data></Cell>", objectName));
            //}
            //else
            //{
            //    rowdataBuilder.AppendLine(string.Format("<Cell ss:StyleID=\"s68\"><Data ss:Type=\"String\">{0}</Data></Cell>", ""));
            //}
            rowdataBuilder.AppendLine(string.Format("<Cell ss:StyleID=\"s68\"><Data ss:Type=\"String\">{0}</Data></Cell>", filedName));

            rowdataBuilder.AppendLine(string.Format("<Cell ss:StyleID=\"s69\"><Data ss:Type=\"String\">{0}</Data></Cell>", text));
            rowdataBuilder.AppendLine(string.Format("<Cell ss:StyleID=\"s69\"><Data ss:Type=\"String\">{0}</Data></Cell>", text));
            rowdataBuilder.AppendLine(string.Format("<Cell ss:StyleID=\"s69\"><Data ss:Type=\"String\">{0}</Data></Cell>", maxLength));
            rowdataBuilder.AppendLine("</Row>");
            return rowdataBuilder.ToString();
        }

        private string ConstructWorkSheet(string exportWorkSheetTemplate, string workSheetName, List<TranslationModel> dataList, string programName, string languageName)
        {
            if (dataList.Count > 0)
            {
                int rowCount = 0;
                StringBuilder rowdataBuilder = new StringBuilder();
                foreach (TranslationModel model in dataList)
                {
                    rowCount++;
                    rowdataBuilder.Append(ConstructRow(model, true));
                }
                string workSheet = exportWorkSheetTemplate;
                if (workSheet == null)
                    workSheet = string.Empty;
                workSheet = workSheet.Replace("[RowCount]", (rowCount + 3).ToString());
                workSheet = workSheet.Replace("[DateTime]", DateTime.UtcNow.ToString(new CultureInfo("nb-NO")));
                workSheet = workSheet.Replace("[ProgramName]", programName);
                workSheet = workSheet.Replace("[LanguageName]", languageName);
                workSheet = workSheet.Replace("[WorksheetName]", workSheetName);
                workSheet = workSheet.Replace("[RowData]", rowdataBuilder.ToString());
                return workSheet;
            }
            else
            {
                return string.Empty;
            }
        }

        private string ConstructWorkSheet(string exportWorkSheetTemplate, string workSheetName, XmlNodeList dataList, string programName, string languageName, string objectName)
        {
            if (dataList.Count > 0)
            {
                int rowCount = 0;
                StringBuilder rowdataBuilder = new StringBuilder();
                foreach (XmlNode model in dataList)
                {
                    foreach (XmlAttribute attr in model.Attributes)
                    {
                        if (!attr.Name.Equals("ID"))
                        {
                            rowdataBuilder.Append(ConstructRow(model, objectName, attr.Name, "", true));
                            rowCount++;
                        }
                    }
                }
                string workSheet = exportWorkSheetTemplate;
                if (workSheet == null)
                    workSheet = string.Empty;
                workSheet = workSheet.Replace("[RowCount]", (rowCount + 3).ToString());
                workSheet = workSheet.Replace("[DateTime]", DateTime.UtcNow.ToString(new CultureInfo("nb-NO")));
                workSheet = workSheet.Replace("[ProgramName]", programName);
                workSheet = workSheet.Replace("[LanguageName]", languageName);
                workSheet = workSheet.Replace("[WorksheetName]", workSheetName);
                workSheet = workSheet.Replace("[RowData]", rowdataBuilder.ToString());
                return workSheet;
            }
            else
            {
                return string.Empty;
            }
        }

        private string GetHeight(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                string[] lineArrary = text.Split(new string[] { "\r" }, StringSplitOptions.None);
                int extralLineCount = 1 + lineArrary.Length;
                return ((int)((text.Length / 70 + extralLineCount) * 20)).ToString();
            }
            else
            {
                return "20";
            }
        }

        private string GetHeightReport(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                string[] lineArrary = text.Split(new string[] { "\r" }, StringSplitOptions.None);
                int extralLineCount = 1 + lineArrary.Length;
                return ((int)((text.Length / 105 + extralLineCount) * 20)).ToString();
            }
            else
            {
                return "20";
            }
        }

        private void AddStatusMessage(string statusQueueName, string statusMsg)
        {
            CloudQueue statusQueue = Resolve<IAzureQueueService>().GetCloudQueue(statusQueueName);
            Resolve<IAzureQueueService>().AddQueueMessage(statusQueue, statusMsg, true);
        }
    }
}
