using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ethos.DependencyInjection;
using ChangeTech.Contracts;
using ChangeTech.Models;
using ChangeTech.IDataRepository;
using ChangeTech.Entities;
using System.Xml;
using System.Data;
using System.Web;
using Ethos.Utility;

namespace ChangeTech.Services
{
    public class PageVariableService : ServiceBase, IPageVariableService
    {
        public const int GENERAL_VARIABLE_COUNT = 5;
        public const string BALANCE_PROGRAM_GUID = "11261DB0-F93C-40BE-A5EC-FB94941ECEBA";
        public const string GENERAL_VARIABLE_FIRSTNAME_GUID = "8350F01D-B1C4-4B8B-94F6-4508754E4596";
        public const string GENERAL_VARIABLE_LASTNAME_GUID = "31D878DF-72D0-4A9C-AF10-0318E0D24A77";
        public const string GENERAL_VARIABLE_GENDER_GUID = "524FB3F2-B16B-49C2-A753-5E7679403DF7";
        public const string GENERAL_VARIABLE_MOBILEPHONE_GUID = "83395457-495E-4083-813E-B5716501C87E";
        public const string GENERAL_VARIABLE_USERNAME_GUID = "26E93E60-9C5D-4CBE-88EA-6418FEAB33A0";

        public List<string> CheckPageVariableServiceConditionOfProgram(Guid programGuid)
        {
            List<string> variableDefinedInProgram = Resolve<IPageVaribleRepository>().GetAllPageVariableNameListByProgramGUID(programGuid);

            List<string> variableBindByPage = Resolve<IPageVaribleRepository>().GetPageVariableBindByPageOfProgramGUID(programGuid);
            List<string> variableBindByQuestion = Resolve<IPageVaribleRepository>().GetPageVariableBindByQuestionOfProgram(programGuid);

            List<string> expressionsContainsVariable = Resolve<IExpressionRepository>().GetExpressionPageVariableUsedInExpressionByProgram(programGuid);
            List<string> variableInExpression = GetVariableInExpression(expressionsContainsVariable);

            variableInExpression.AddRange(variableBindByPage);
            variableInExpression.AddRange(variableBindByQuestion);
            variableInExpression = variableInExpression.Distinct().ToList();

            return CheckVariableUseCondition(variableDefinedInProgram, variableInExpression);
        }

        private List<string> CheckVariableUseCondition(List<string> variableDefinedInProgram, List<string> variableInExpression)
        {
            List<string> result = new List<string>();
            foreach (string variable in variableDefinedInProgram)
            {
                if (!variableInExpression.Contains(variable))
                {
                    result.Add(variable);
                }
            }

            return result;
        }

        private List<string> GetVariableInExpression(List<string> expressionsContainsVariable)
        {
            string start = "{V:";
            string end = "}";
            string result = string.Empty;

            List<string> variableList = new List<string>();
            foreach (string expression in expressionsContainsVariable)
            {
                string expressionInstance = expression;
                while (expressionInstance.Contains(start))
                {
                    expressionInstance = expressionInstance.Substring(expressionInstance.IndexOf(start) + 3);
                    result = expressionInstance.Substring(0, expressionInstance.IndexOf(end));
                    variableList.Add(result);
                }
            }

            return variableList;
        }

        public void SaveSetPageVariable(List<SetPageVariableModel> setVariableList)
        {
            foreach (SetPageVariableModel setVariable in setVariableList)
            {
                SaveUserPageVariable(setVariable);
                SaveUserPageVariablePerDay(setVariable);
            }
        }

        public int GetPageVariableCount(Guid programGUID, VariableTypeEnum variableType, Guid groupGUID)
        {
            int count = 0;
            switch (variableType)
            {
                case VariableTypeEnum.General:
                    //need to change count if general variables changed
                    count = GENERAL_VARIABLE_COUNT;
                    break;
                case VariableTypeEnum.Program:
                    string variableType_Program = VariableTypeEnum.Program.ToString();
                    if (groupGUID == Guid.Empty)
                        count = Resolve<IPageVaribleRepository>().GetPageVariableByProgram(programGUID).Where(pv => pv.PageVariableType == variableType_Program).Count();
                    else
                        count = Resolve<IPageVaribleRepository>().GetPageVariableByProgram(programGUID).Where(pv => pv.PageVariableGroup.PageVariableGroupGUID == groupGUID && pv.PageVariableType == variableType_Program).Count();
                    break;
            }
            return count;
        }

        public List<EditPageVariableModel> GetPageVariableByProgram(Guid programGUID, VariableTypeEnum variableType, Guid userGUID, Guid groupGUID, int pageSize, int pageIndex)
        {
            List<EditPageVariableModel> list = new List<EditPageVariableModel>();
            List<Entities.PageVariable> entities;
            switch (variableType)
            {
                case VariableTypeEnum.General:
                    //string variableType_General = VariableTypeEnum.General.ToString();
                    //entities = Resolve<IPageVaribleRepository>().GetPageVariableByProgram(new Guid(BALANCE_PROGRAM_GUID)).Where(pv => pv.PageVariableType == variableType_General).OrderBy(p => p.Name).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();                   
                    list = GetGeneralVariableByProgramUser(programGUID, userGUID);
                    break;
                case VariableTypeEnum.Program:
                    string variableType_Program = VariableTypeEnum.Program.ToString();
                    if (groupGUID == Guid.Empty)
                        entities = Resolve<IPageVaribleRepository>().GetPageVariableByProgram(programGUID).Where(pv => pv.PageVariableType == variableType_Program).OrderBy(p => p.Name).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                    else
                        entities = Resolve<IPageVaribleRepository>().GetPageVariableByProgram(programGUID).Where(pv => pv.PageVariableGroup.PageVariableGroupGUID == groupGUID && pv.PageVariableType == variableType_Program).OrderBy(p => p.Name).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                    foreach (Entities.PageVariable entity in entities)
                    {
                        if (!entity.ProgramReference.IsLoaded)
                        {
                            entity.ProgramReference.Load();
                        }

                        string userPageVariableValue = string.Empty;

                        UserPageVariable userPageVariable = Resolve<IUserPageVariableRepository>().GetUserPageVariableByUserAndVariable(userGUID, entity.PageVariableGUID);
                        if (userPageVariable != null)
                        {
                            if (!string.IsNullOrEmpty(userPageVariable.Value))
                            {
                                userPageVariableValue = userPageVariable.Value;
                            }
                            else
                            {
                                if (!userPageVariable.QuestionAnswerReference.IsLoaded)
                                {
                                    userPageVariable.QuestionAnswerReference.Load();
                                }
                                if (!userPageVariable.QuestionAnswer.QuestionAnswerValue.IsLoaded)
                                {
                                    userPageVariable.QuestionAnswer.QuestionAnswerValue.Load();
                                }

                                if (userPageVariable.QuestionAnswer.QuestionAnswerValue.Count > 0)
                                {
                                    QuestionAnswerValue questionAnswerValueEntity = userPageVariable.QuestionAnswer.QuestionAnswerValue.FirstOrDefault();
                                    if (!questionAnswerValueEntity.PageQuestionItemReference.IsLoaded)
                                    {
                                        questionAnswerValueEntity.PageQuestionItemReference.Load();
                                    }

                                    if (questionAnswerValueEntity.PageQuestionItem != null)
                                    {
                                        int sumScore = 0;
                                        foreach (QuestionAnswerValue questionAnswerValue in userPageVariable.QuestionAnswer.QuestionAnswerValue)
                                        {
                                            PageQuestionItem questionItem = questionAnswerValue.PageQuestionItem;
                                            if (questionItem != null) sumScore += questionItem.Score.HasValue ? questionItem.Score.Value : 0;
                                        }
                                        userPageVariableValue = sumScore.ToString();
                                    }
                                    else
                                    {
                                        userPageVariableValue = questionAnswerValueEntity.UserInput;
                                    }
                                }
                            }
                        }
                        //Get pageVariable usedTimes.
                        int usedTimes = 0;
                        DataTable pageVariableUsedTimeTable = Resolve<IStoreProcedure>().GetPageVariableUsedTimesByProgramGuid(programGUID, entity.PageVariableGUID, entity.Name);
                        if (pageVariableUsedTimeTable!=null)
                        {
                            usedTimes = Convert.ToInt32(pageVariableUsedTimeTable.Rows[0][0]);
                        }

                        #region Old follows 
                        //int usedTimes = Resolve<IPageRepository>().GetPageVariableCountByProgramGuidAndPageVariableGuid(programGUID, entity.PageVariableGUID).ToList().Count;
                        //List<PageContent> pageContents = Resolve<IPageContentRepository>().GetPageContentsByProgramGUID(programGUID).ToList();
                        //foreach (PageContent pageContentEntity in pageContents)
                        //{
                        //    string pageVariableName = entity.Name;
                        //    usedTimes+= GetVariableCountByVariableName(pageContentEntity, pageVariableName);
                        //} 
                        #endregion
                        EditPageVariableModel pageVariable = new EditPageVariableModel
                        {
                            PageVariableGUID = entity.PageVariableGUID,
                            ProgramGUID = entity.Program.ProgramGUID,
                            Name = entity.Name,
                            ValueType = entity.ValueType,
                            Description = entity.Description,
                            PageVariableType = entity.PageVariableType,
                            Value = userPageVariableValue,
                            modelStatus = ModelStatus.ModelNoChange,
                            UsedTimes = usedTimes
                        };
                        if (!entity.PageVariableGroupReference.IsLoaded)
                        {
                            entity.PageVariableGroupReference.Load();
                        }
                        if (entity.PageVariableGroup != null)
                        {
                            pageVariable.PageVariableGroupGUID = entity.PageVariableGroup.PageVariableGroupGUID;
                        }
                        IQueryable<PageQuestion> pageQuestionsOfVariable = Resolve<IPageQuestionRepository>().GetPageQuestionOfVariable(entity.PageVariableGUID);
                        pageVariable.Questions = Resolve<IPageService>().ParasePageQuestionModel(pageQuestionsOfVariable);

                        list.Add(pageVariable);
                    }
                    break;
            }
            return list;
        }

        private List<EditPageVariableModel> GetGeneralVariableByProgramUser(Guid programGUID, Guid userGUID)
        {
            List<EditPageVariableModel> list = new List<EditPageVariableModel>();
            User userEntity = Resolve<IUserRepository>().GetUserByGuid(userGUID);
            //This 5 name belongs to General PageVariableType//
            //if This five General is changed, change the isVariableTypeGeneralByName in ServiceUtility.cs
            list.Add(new EditPageVariableModel
            {
                PageVariableGUID = new Guid(GENERAL_VARIABLE_FIRSTNAME_GUID),
                ProgramGUID = programGUID,
                Name = "FirstName",
                ValueType = "String",
                Description = VariableTypeEnum.General.ToString(),
                PageVariableType = VariableTypeEnum.General.ToString(),
                Value = userEntity.FirstName,
                modelStatus = ModelStatus.ModelNoChange
            });
            list.Add(new EditPageVariableModel
            {
                PageVariableGUID = new Guid(GENERAL_VARIABLE_LASTNAME_GUID),
                ProgramGUID = programGUID,
                Name = "LastName",
                ValueType = "String",
                Description = VariableTypeEnum.General.ToString(),
                PageVariableType = VariableTypeEnum.General.ToString(),
                Value = userEntity.LastName,
                modelStatus = ModelStatus.ModelNoChange
            });
            list.Add(new EditPageVariableModel
            {
                PageVariableGUID = new Guid(GENERAL_VARIABLE_GENDER_GUID),
                ProgramGUID = programGUID,
                Name = "Gender",
                ValueType = "String",
                Description = VariableTypeEnum.General.ToString(),
                PageVariableType = VariableTypeEnum.General.ToString(),
                Value = userEntity.Gender,
                modelStatus = ModelStatus.ModelNoChange
            });
            list.Add(new EditPageVariableModel
            {
                PageVariableGUID = new Guid(GENERAL_VARIABLE_USERNAME_GUID),
                ProgramGUID = programGUID,
                Name = "UserName",
                ValueType = "String",
                Description = VariableTypeEnum.General.ToString(),
                PageVariableType = VariableTypeEnum.General.ToString(),
                Value = string.Format("{0} {1}", userEntity.FirstName, userEntity.LastName),
                modelStatus = ModelStatus.ModelNoChange
            });
            list.Add(new EditPageVariableModel
            {
                PageVariableGUID = new Guid(GENERAL_VARIABLE_MOBILEPHONE_GUID),
                ProgramGUID = programGUID,
                Name = "MobilePhone",
                ValueType = "String",
                Description = VariableTypeEnum.General.ToString(),
                PageVariableType = VariableTypeEnum.General.ToString(),
                Value = userEntity.MobilePhone,
                modelStatus = ModelStatus.ModelNoChange
            });
            return list;
        }

        // 1:the program is published 2:there are entities used the pagevariable
        public int BeforeDeletePageVariable(Guid pageVariableGuid)
        {
            int result = 0;
            ChangeTech.Entities.PageVariable variable = Resolve<IPageVaribleRepository>().GetItem(pageVariableGuid);
            if (variable != null)
            {
                if (!variable.ProgramReference.IsLoaded)
                {
                    variable.ProgramReference.Load();
                }
                if (!variable.Program.ProgramStatusReference.IsLoaded)
                {
                    variable.Program.ProgramStatusReference.Load();
                }

                if (variable.Program.ProgramStatus.Name == "Publish")
                {
                    result = 1;
                }
                else
                {
                    if (IsPageVariableUsed(pageVariableGuid))
                    {
                        result = 2;
                    }
                    else
                    {
                        Delete(pageVariableGuid);
                    }
                }
            }
            return result;
        }

        public void Delete(Guid PageVariableGUID)
        {
            ChangeTech.Entities.PageVariable variable = Resolve<IPageVaribleRepository>().GetItem(PageVariableGUID);
            // update page if page have a pagevarible           
            List<Page> pages = Resolve<IPageRepository>().GetPagesByPageVariable(PageVariableGUID).ToList<Page>();
            foreach (Page timepage in pages)
            {
                timepage.PageVariable = null;
                Resolve<IPageRepository>().UpdatePage(timepage);
            }

            //update page question 
            List<PageQuestion> questions = Resolve<IPageQuestionRepository>().GetAllPageQuestinByVariable(PageVariableGUID).ToList<PageQuestion>();
            //IEnumerable<PageQuestion> questionList = variable.PageQuestion.Where(q => q.IsDeleted != true);
            foreach (PageQuestion question in questions)
            {
                question.PageVariable = null;
                Resolve<IPageQuestionRepository>().UpdatePageQuestion(question);
            }

            //delete userpagevariable
            if (!variable.UserPageVariable.IsLoaded)
            {
                variable.UserPageVariable.Load();
            }
            if (variable.UserPageVariable.Count > 0)
            {
                Resolve<IUserPageVariableRepository>().Delete(variable.UserPageVariable);
            }

            //delete userpagevariableperday
            if (!variable.UserPageVariablePerDay.IsLoaded)
            {
                variable.UserPageVariablePerDay.Load();
            }
            if (variable.UserPageVariablePerDay.Count > 0)
            {
                Resolve<IUserPageVariablePerDayRepository>().Delete(variable.UserPageVariablePerDay);
            }

            if (!variable.Preferences.IsLoaded)
            {
                variable.Preferences.Load();
            }

            List<Preferences> preferencesUseThisVariable = variable.Preferences.ToList();
            foreach (Preferences preferences in preferencesUseThisVariable)
            {
                preferences.PageVariable = null;
                Resolve<IPreferencesRepository>().UpdatePreference(preferences);
            }

            Resolve<IPageVaribleRepository>().Delete(PageVariableGUID);
        }

        public void Edit(PageVariableModel pageVariableModel)
        {
            // Edit PageVariable
            PageVariable item = Resolve<IPageVaribleRepository>().GetItem(pageVariableModel.PageVariableGUID);
            string oldVariableName = item.Name;
            item.Description = pageVariableModel.Description;
            item.ValueType = pageVariableModel.ValueType;
            item.PageVariableGroup = Resolve<IPageVariableGroupRepository>().Get(pageVariableModel.PageVariableGroupGUID);
            item.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;

            // add log
            InsertLogModel model = new InsertLogModel
            {
                ActivityLogType = (int)LogTypeEnum.ModifyPageVariable,
                Browser = HttpContext.Current.Request.UserAgent,
                IP = HttpContext.Current.Request.UserHostAddress,
                Message = "Edit PageVariable",
                ProgramGuid = pageVariableModel.ProgramGUID,
                PageGuid = Guid.Empty,
                PageSequenceGuid = Guid.Empty,
                SessionGuid = Guid.Empty,
                UserGuid = item.LastUpdatedBy.HasValue?item.LastUpdatedBy.Value:Guid.Empty,
                From = string.Empty
            };
            Resolve<IPageVaribleRepository>().Edit(item);

            if (item.Name != pageVariableModel.Name)
            {
                try
                {
                    DataTable messageTable = Resolve<IStoreProcedure>().UpdatePageVariableNameByProgramGuidAndPageVariableGuid(pageVariableModel.ProgramGUID, pageVariableModel.PageVariableGUID, pageVariableModel.Name, oldVariableName);
                    if (messageTable != null && messageTable.Rows[0][0].ToString().Split(';')[0] == "0")
                    {
                        //return "0;UpdateError" 
                        // add log
                        InsertLogModel logModel = new InsertLogModel
                        {
                            ActivityLogType = (int)LogTypeEnum.ModifyPageVariable,
                            Browser = HttpContext.Current.Request.UserAgent,
                            IP = HttpContext.Current.Request.UserHostAddress,
                            Message = "Error : Update PageVariable Name.",
                            ProgramGuid = pageVariableModel.ProgramGUID,
                            PageGuid = Guid.Empty,
                            PageSequenceGuid = Guid.Empty,
                            SessionGuid = Guid.Empty,
                            UserGuid = item.LastUpdatedBy.HasValue ? item.LastUpdatedBy.Value : Guid.Empty,
                            From = string.Empty
                        };
                    }
                }
                catch (Exception ex)
                {
                    // add log
                    InsertLogModel logModel = new InsertLogModel
                    {
                        ActivityLogType = (int)LogTypeEnum.ModifyPageVariable,
                        Browser = HttpContext.Current.Request.UserAgent,
                        IP = HttpContext.Current.Request.UserHostAddress,
                        Message = ex.Message,
                        ProgramGuid = pageVariableModel.ProgramGUID,
                        PageGuid = Guid.Empty,
                        PageSequenceGuid = Guid.Empty,
                        SessionGuid = Guid.Empty,
                        UserGuid = item.LastUpdatedBy.HasValue ? item.LastUpdatedBy.Value : Guid.Empty,
                        From = string.Empty
                    };
                    LogUtility.LogUtilityIntance.LogException(ex, String.Format("Method Name: {0}, Page Variable Name: {1}", System.Reflection.MethodBase.GetCurrentMethod().Name, pageVariableModel.Name));
                    throw ex;
                }
            }
        }


        public void Add(Models.PageVariableModel pageVariable)
        {
            Program programEntity = Resolve<IProgramRepository>().GetProgramByGuid(pageVariable.ProgramGUID);
            if (!programEntity.PageVariable.IsLoaded) programEntity.PageVariable.Load();
            //Add logic that, if there is a pageVariable which has the same name and type, can't add it.
            if (programEntity.PageVariable.Where(p => p.PageVariableType == pageVariable.PageVariableType && p.Name == pageVariable.Name).Count() == 0)
            {
                Entities.PageVariable item = new ChangeTech.Entities.PageVariable()
                {
                    Description = pageVariable.Description,
                    Name = pageVariable.Name,
                    Program = programEntity,
                    PageVariableGUID = pageVariable.PageVariableGUID,
                    PageVariableType = pageVariable.PageVariableType,
                    ValueType = pageVariable.ValueType,
                    LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid,
                };
                if (pageVariable.PageVariableGroupGUID != Guid.Empty)
                {
                    item.PageVariableGroup = Resolve<IPageVariableGroupRepository>().Get(pageVariable.PageVariableGroupGUID);
                }
                Resolve<IPageVaribleRepository>().Add(item);
            }

        }

        public DataTable GetProgramUserPageVariable(Guid programGuid)
        {
            try
            {
                DataTable programUserPageVariable = Resolve<IStoreProcedure>().GetProgramUserPageVariable(programGuid);
                return programUserPageVariable;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method Name:{0}, ProgramGuid : {1}", System.Reflection.MethodBase.GetCurrentMethod().Name, programGuid));
                throw ex;
            }
        }

        public DataTable GetProgramUserPageVariableExtension(Guid programGuid)
        {
            try
            {
                DataTable programUserPageVariable = Resolve<IStoreProcedure>().GetProgramUserPageVariableExtension(programGuid);
                return programUserPageVariable;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method Name:{0}, ProgramGuid : {1}", System.Reflection.MethodBase.GetCurrentMethod().Name, programGuid));
                throw ex;
            }
        }

        //private DataTable MakeTable(Guid programGuid)
        //{
        //    DataTable variableTable = new DataTable();
        //    List<ChangeTech.Entities.PageVariable> variableList = Resolve<IPageVaribleRepository>().GetPageVariableByProgram(programGuid).Where(pv => pv.PageVariableType == VariableTypeEnum.Program.ToString()).OrderBy(p => p.Name).ToList();

        //    DataColumn column = new DataColumn("User");
        //    column.DefaultValue = "";
        //    column.DataType = System.Type.GetType("System.String");
        //    variableTable.Columns.Add(column);

        //    DataColumn companyColumn = new DataColumn("Company");
        //    column.DefaultValue = "";
        //    column.DataType = System.Type.GetType("System.String");
        //    variableTable.Columns.Add(companyColumn);

        //    foreach (ChangeTech.Entities.PageVariable variable in variableList)
        //    {
        //        DataColumn lcolumn = new DataColumn(variable.Name);
        //        column.DefaultValue = "";
        //        column.DataType = System.Type.GetType("System.String");
        //        variableTable.Columns.Add(lcolumn);
        //    }

        //    return variableTable;
        //}

        //private DataTable MakeTableExtension(Guid programGuid)
        //{
        //    DataTable variableTable = new DataTable();
        //    List<ChangeTech.Entities.PageVariable> variableList = Resolve<IPageVaribleRepository>().GetPageVariableByProgram(programGuid).Where(pv => pv.PageVariableType == VariableTypeEnum.Program.ToString()).OrderBy(p => p.Name).ToList();

        //    DataColumn column = new DataColumn("User");
        //    column.DefaultValue = "";
        //    column.DataType = System.Type.GetType("System.String");
        //    variableTable.Columns.Add(column);

        //    DataColumn companyColumn = new DataColumn("Company");
        //    column.DefaultValue = "";
        //    column.DataType = System.Type.GetType("System.String");
        //    variableTable.Columns.Add(companyColumn);

        //    //newRow["Status"] = userVariableValueTable.Rows[i]["Status"].ToString();
        //    //newRow["CurrentDay"] = userVariableValueTable.Rows[i]["CurrentDay"].ToString();
        //    //newRow["RegisterDate"] = userVariableValueTable.Rows[i]["RegisterDate"].ToString();
        //    //newRow["Gender"] = userVariableValueTable.Rows[i]["Gender"].ToString();
        //    //newRow["LastProgramActivityDate"] = userVariableValueTable.Rows[i]["LastProgramActivityDate"].ToString();
        //    DataColumn StatusColumn = new DataColumn("Status");
        //    StatusColumn.DefaultValue = "";
        //    StatusColumn.DataType = System.Type.GetType("System.String");
        //    variableTable.Columns.Add(StatusColumn);

        //    DataColumn CurrentDayColumn = new DataColumn("CurrentDay");
        //    CurrentDayColumn.DefaultValue = "";
        //    CurrentDayColumn.DataType = System.Type.GetType("System.String");
        //    variableTable.Columns.Add(CurrentDayColumn);

        //    DataColumn RegisterDateColumn = new DataColumn("RegisterDate");
        //    RegisterDateColumn.DefaultValue = "";
        //    RegisterDateColumn.DataType = System.Type.GetType("System.String");
        //    variableTable.Columns.Add(RegisterDateColumn);

        //    DataColumn GenderColumn = new DataColumn("UserGender");
        //    GenderColumn.DefaultValue = "";
        //    GenderColumn.DataType = System.Type.GetType("System.String");
        //    variableTable.Columns.Add(GenderColumn);

        //    DataColumn LastProgramActivityDateColumn = new DataColumn("LastProgramActivityDate");
        //    LastProgramActivityDateColumn.DefaultValue = "";
        //    LastProgramActivityDateColumn.DataType = System.Type.GetType("System.String");
        //    variableTable.Columns.Add(LastProgramActivityDateColumn);


        //    foreach (ChangeTech.Entities.PageVariable variable in variableList)
        //    {
        //        DataColumn lcolumn = new DataColumn(variable.Name);
        //        column.DefaultValue = "";
        //        column.DataType = System.Type.GetType("System.String");
        //        variableTable.Columns.Add(lcolumn);
        //    }

        //    return variableTable;
        //}

        private bool IsPageVariableUsed(Guid pageVariableGuid)
        {
            bool flag = false;
            ChangeTech.Entities.PageVariable variable = Resolve<IPageVaribleRepository>().GetItem(pageVariableGuid);
            if (!variable.Page.IsLoaded)
            {
                variable.Page.Load();
            }
            if (variable.Page.Count > 0)
            {
                flag = true;
            }
            else
            {
                if (!variable.PageQuestion.IsLoaded)
                {
                    variable.PageQuestion.Load();
                }
                if (variable.PageQuestion.Count > 0)
                {
                    flag = true;
                }
            }
            return flag;
        }

        private void SaveUserPageVariablePerDay(SetPageVariableModel setVariable)
        {
            ChangeTech.Entities.PageVariable variable = Resolve<IPageVaribleRepository>().GetVariableByProgramGuidAndVariableName(setVariable.ProgramGUID, setVariable.VariableName);
            if (variable != null)
            {
                UserPageVariablePerDay userVariabePerDay = Resolve<IUserPageVariablePerDayRepository>().GetUserPageVariable(setVariable.UserGUID, variable.PageVariableGUID, setVariable.SessionGUID);
                if (userVariabePerDay != null)
                {
                    userVariabePerDay.QuestionAnswer = null;
                    userVariabePerDay.Value = setVariable.VariableValue;
                    Resolve<IUserPageVariablePerDayRepository>().UpdateUserPageVariable(userVariabePerDay);
                }
                else
                {
                    userVariabePerDay = new UserPageVariablePerDay
                    {
                        UserPageVariablePerDayGUID = Guid.NewGuid(),
                        User = Resolve<IUserRepository>().GetUserByGuid(setVariable.UserGUID),
                        PageVariable = variable,
                        Value = setVariable.VariableValue,
                        SessionGUID = setVariable.SessionGUID
                    };
                    Resolve<IUserPageVariablePerDayRepository>().InsertUserPageVariable(userVariabePerDay);
                }
            }
        }

        private void SaveUserPageVariable(SetPageVariableModel setVariable)
        {
            ChangeTech.Entities.PageVariable variable = Resolve<IPageVaribleRepository>().GetVariableByProgramGuidAndVariableName(setVariable.ProgramGUID, setVariable.VariableName);
            if (variable != null)
            {
                VariableTypeEnum VariableType;
                System.Enum.TryParse(variable.PageVariableType, out VariableType);

                switch (VariableType)
                {
                    case (int)VariableTypeEnum.Program: //VariableTypeEnum.Program:
                        UserPageVariable userVariable = Resolve<IUserPageVariableRepository>().GetUserPageVariableByUserAndVariable(setVariable.UserGUID, variable.PageVariableGUID);

                        if (userVariable != null)
                        {
                            userVariable.QuestionAnswer = null;
                            userVariable.Value = setVariable.VariableValue;
                            Resolve<IUserPageVariableRepository>().UpdateUserPageVariable(userVariable);
                        }
                        else
                        {
                            userVariable = new UserPageVariable
                            {
                                UserPageVariableGUID = Guid.NewGuid(),
                                User = Resolve<IUserRepository>().GetUserByGuid(setVariable.UserGUID),
                                Value = setVariable.VariableValue,
                                PageVariable = variable
                            };
                            Resolve<IUserPageVariableRepository>().AddUserPageVariable(userVariable);
                        }
                        break;
                    case VariableTypeEnum.General:
                        User userEntity = Resolve<IUserRepository>().GetUserByGuid(setVariable.UserGUID);
                        if (userEntity != null)
                        {
                            switch (variable.Name)
                            {
                                case "FirstName":
                                    userEntity.FirstName = setVariable.VariableValue;
                                    break;
                                case "LastName":
                                    userEntity.LastName = setVariable.VariableValue;
                                    break;
                                case "Gender":
                                    userEntity.Gender = setVariable.VariableValue;
                                    break;
                                case "MobilePhone":
                                    userEntity.MobilePhone = setVariable.VariableValue;
                                    break;
                            }
                        }
                        break;
                }
            }
        }

        #region Old follow
        ////Get contains Variable's count in the content.
        //private int GetContainsVariableCount(string content,string variableFormat)
        //{
        //    int count = 0;
        //    int index = 0;
        //    while (content.Contains(variableFormat))
        //    {
        //         index = content.IndexOf(variableFormat, index);
        //        if (index > -1)
        //        {
        //            count++;
        //            content = content.Substring(index + variableFormat.Length);
        //            index = content.IndexOf(variableFormat);
        //        }
        //    }
        //    return count;
        //}
        #endregion
    }
}
