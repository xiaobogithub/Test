using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Contracts;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;
using Ethos.DependencyInjection;
using Ethos.Utility;
using System.Globalization;

namespace ChangeTech.Services
{
    public class UserPageVariablePerDayService : ServiceBase, IUserPageVariablePerDayService
    {
        public double GetPageVariableValueOnDay(Guid userGUID, Guid programGUID, string pageVariableName, int day)
        {
            double value = 0;
            PageVariable pageVariableEntity = Resolve<IPageVaribleRepository>().GetVariableByProgramGuidAndVariableName(programGUID, pageVariableName);
            Session sessionEntity = Resolve<ISessionRepository>().GetSessionByProgramGuidAndDay(programGUID, day);

            if (pageVariableEntity != null && sessionEntity != null)
            {
                UserPageVariablePerDay userPageVariablePerDayEntity = Resolve<IUserPageVariablePerDayRepository>().GetUserPageVariable(userGUID, pageVariableEntity.PageVariableGUID, sessionEntity.SessionGUID);
                if (userPageVariablePerDayEntity != null)
                {
                    if (!userPageVariablePerDayEntity.QuestionAnswerReference.IsLoaded)
                    {
                        userPageVariablePerDayEntity.QuestionAnswerReference.Load();
                    }

                    if (userPageVariablePerDayEntity.QuestionAnswer != null) {
                        if (!userPageVariablePerDayEntity.QuestionAnswer.PageQuestionReference.IsLoaded) {
                            userPageVariablePerDayEntity.QuestionAnswer.PageQuestionReference.Load();
                        }

                        if (userPageVariablePerDayEntity.QuestionAnswer.PageQuestion != null) {
                            if (!userPageVariablePerDayEntity.QuestionAnswer.QuestionAnswerValue.IsLoaded) {
                                userPageVariablePerDayEntity.QuestionAnswer.QuestionAnswerValue.Load();
                            }

                            if (userPageVariablePerDayEntity.QuestionAnswer.QuestionAnswerValue.Count > 0) {
                                value = 0;
                                foreach (QuestionAnswerValue questionAnswerValue in userPageVariablePerDayEntity.QuestionAnswer.QuestionAnswerValue) {
                                    if (!questionAnswerValue.PageQuestionItemReference.IsLoaded) {
                                        questionAnswerValue.PageQuestionItemReference.Load();
                                    }
                                    if (questionAnswerValue.PageQuestionItem != null) {
                                        value += questionAnswerValue.PageQuestionItem.Score.HasValue ? questionAnswerValue.PageQuestionItem.Score.Value : 0;
                                    }
                                    else {
                                        int userInput = 0;
                                        int.TryParse(questionAnswerValue.UserInput, out userInput);
                                        value += userInput;
                                    }
                                }
                            }
                            //else {
                            //    value = -1;
                            //}
                        }
                    }
                    else 
                    {
                        NumberFormatInfo provider = new NumberFormatInfo();
                        provider.NumberDecimalSeparator = ".";
                        value = Convert.ToDouble(userPageVariablePerDayEntity.Value, provider);                        
                    }
                }
                //else
                //{
                //    value = -1;
                //}
            }
            //else
            //{
            //    value = -1;
            //}
            return value;
        }

        //public int GetPageVariableValueOnWeek(Guid userGUID, Guid programGUID, string pageVariableName, int week)
        //{
        //    int value = 0;

        //    int startDay = (week-1) * 7 + 1;
        //    int endDay = week * 7;

        //    int sumDayValueOfWeek = 0;
        //    int nonZeroValueDays = 0;
        //    int noValueDays = 0;
        //    for (int day = startDay; day <= endDay; day++)
        //    {
        //        int dayValue = GetPageVariableValueOnDay(userGUID, programGUID, pageVariableName, day);
        //        if (dayValue > 0)
        //        {
        //            sumDayValueOfWeek += dayValue;
        //            nonZeroValueDays++;
        //        }
        //        else if (dayValue == -1)
        //        {
        //            noValueDays++;
        //        }
        //    }

        //    if (nonZeroValueDays > 0)
        //    {
        //        value = sumDayValueOfWeek / nonZeroValueDays;
        //    }
        //    else if (noValueDays == 7)
        //    {
        //        value = -1;
        //    }

        //    return value;
        //}

        //public string GetPageVariableValueOnWeek(Guid userGUID, Guid programGUID, string pageVariableName, int week , int currentDay, bool isOperatedIncurrentSession)
        //{
        //    string result = string.Empty;
        //    if (week == 0)
        //    {
        //        if (currentDay == 0)
        //        {
        //            result += "{V:" + pageVariableName + "}";
        //        }
        //        else
        //        {
        //            CultureInfo ci = new CultureInfo("en-us");
        //            result = GetPageVariableValueOnDay(userGUID, programGUID, pageVariableName, 0).ToString(ci);
        //        }
        //    }
        //    else
        //    {
        //        int startDay = (week - 1) * 7 + 1;
        //        int endDay = week * 7;
        //        int hasValueDays = 0;
        //        for (int day = startDay; day <= endDay && day <= currentDay; day++)
        //        {
        //            if (currentDay == day)
        //            {
        //                if (isOperatedIncurrentSession)
        //                {
        //                    if (string.IsNullOrEmpty(result))
        //                    {
        //                        result += "{V:" + pageVariableName + "}";
        //                    }
        //                    else
        //                    {
        //                        result += "+" + "{V:" + pageVariableName + "}";
        //                    }
        //                    hasValueDays++;
        //                }
        //            }
        //            else
        //            {
        //                double dayValue = GetPageVariableValueOnDay(userGUID, programGUID, pageVariableName, day);
        //                if (dayValue > 0)
        //                {
        //                    CultureInfo ci = new CultureInfo("en-us");
        //                    if (string.IsNullOrEmpty(result))
        //                    {
                                
        //                        result += dayValue.ToString(ci);
        //                    }
        //                    else
        //                    {
        //                        result += "+" + dayValue.ToString(ci);
        //                    }
        //                    hasValueDays++;
        //                }
        //            }
        //        }

        //        if (hasValueDays > 0)
        //        {
        //            result = "(" + result + ")" + "/" + hasValueDays;
        //        }
        //    }

        //    return result;
        //}
       
        public string GetPageVariableValueOnWeek(Guid userGUID, Guid programGUID, string pageVariableName, int week, int currentDay, int lastWeek, bool isOperatedIncurrentSession)
        {
            string result = string.Empty;
            if (week == 0)
            {
                if (currentDay == 0)
                {
                    result += "{V:" + pageVariableName + "}";
                }
                else
                {
                    CultureInfo ci = new CultureInfo("en-us");
                    result = GetPageVariableValueOnDay(userGUID, programGUID, pageVariableName, 0).ToString(ci);
                }
            }
            else if (week == lastWeek)
            {
                if (currentDay > (week - 1) * 7 + 1)
                {
                    result += "{V:" + pageVariableName + "}";
                }               
            }
            else
            {    
                if (currentDay/7 == week)
                {
                    if (currentDay > week * 7 + 1)
                    {
                        double dayValue = GetPageVariableValueOnDay(userGUID, programGUID, pageVariableName, week * 7 + 1);
                        CultureInfo ci = new CultureInfo("en-us");
                        result += dayValue.ToString(ci);
                    }
                    else
                    {
                        result += "{V:" + pageVariableName + "}";
                    }
                }
                else if (currentDay/7  > week)
                {
                    double dayValue = GetPageVariableValueOnDay(userGUID, programGUID, pageVariableName, week*7 +1);
                    CultureInfo ci = new CultureInfo("en-us");
                    result += dayValue.ToString(ci);
                }
            }
            return result;
        }
    }
}
