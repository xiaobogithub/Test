using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;
using ChangeTech.Models;
using ChangeTech.IDataRepository;
using ChangeTech.Entities;

namespace ChangeTech.Services
{
    public class UserAnswerService : ServiceBase, IUserAnswerService
    {
        #region implement interface

        public void SaveUserAnswer(List<UserAnswerModel> answerList, string programMode)
        {
            foreach(UserAnswerModel answer in answerList)
            {
                //FormatAnswer(answer);
                SaveQuestionAnswer(answer);
            }
        }
        #endregion

        #region private method
        private void SaveQuestionAnswer(UserAnswerModel answer)
        {
            if (string.IsNullOrEmpty(answer.SessionGuid))
            {
                SaveQuestionAnswerWithNoSession(answer);
            }
            else
            {
                SaveQuestionAnswerInSession(answer);
            }
        }

        private void SaveQuestionAnswerInSession(UserAnswerModel answer)
        {
            QuestionAnswer saveQuestionAnswer = new QuestionAnswer();
            //question answer in session.
            SessionContent sessionContent = Resolve<ISessionContentRepository>().GetSessionContentBySessionGuidAndOrderNO(new Guid(answer.SessionGuid), answer.PageSequenceOrder);
            if (sessionContent != null)
            {
                if (string.IsNullOrEmpty(answer.PageQuestionGUID))
                {
                    if (string.IsNullOrEmpty(answer.RelapsePageSequenceGuid) || string.IsNullOrEmpty(answer.RelapsePageGuid))
                    {
                        saveQuestionAnswer = Resolve<IQuestionAnswerRepository>().GetQuestionAnswerByUserPgPmSCNotForRelapse(new Guid(answer.UserGuid), new Guid(answer.PageGuid), new Guid(answer.ProgramGuid), sessionContent.SessionContentGUID);
                    }
                    else
                    {
                        saveQuestionAnswer = Resolve<IQuestionAnswerRepository>().GetQuestionAnswerByUserPgPmSCForRelapse(new Guid(answer.UserGuid), new Guid(answer.PageGuid), new Guid(answer.ProgramGuid), sessionContent.SessionContentGUID, new Guid(answer.RelapsePageSequenceGuid), new Guid(answer.RelapsePageGuid));
                    }

                    if (saveQuestionAnswer != null)
                    {
                        UpdatePageAnswer(saveQuestionAnswer, answer);
                        SavePageVariable(saveQuestionAnswer.QuestionAnswerGUID, answer);
                        //SavePageVariablePerDay(saveQuestionAnswer.QuestionAnswerGUID, answer);
                    }
                    else
                    {
                        Guid questionAnswerGuid = Guid.NewGuid();
                        InsertQuestionAnswerByPage(answer, questionAnswerGuid);
                        SavePageVariable(questionAnswerGuid, answer);
                        //SavePageVariablePerDay(questionAnswerGuid, answer);
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(answer.RelapsePageSequenceGuid) || string.IsNullOrEmpty(answer.RelapsePageGuid))
                    {
                        saveQuestionAnswer = Resolve<IQuestionAnswerRepository>().GetQuestionAnswerByUserQuePmSCNotForRelapse(new Guid(answer.UserGuid), new Guid(answer.PageQuestionGUID), new Guid(answer.ProgramGuid), sessionContent.SessionContentGUID);
                    }
                    else
                    {
                        saveQuestionAnswer = Resolve<IQuestionAnswerRepository>().GetQuestionAnswerByUserQuePmSCForRelapse(new Guid(answer.UserGuid), new Guid(answer.PageQuestionGUID), new Guid(answer.ProgramGuid), sessionContent.SessionContentGUID, new Guid(answer.RelapsePageSequenceGuid), new Guid(answer.RelapsePageGuid));
                    }

                    if (saveQuestionAnswer != null)
                    {
                        UpdateQuestionAnswer(saveQuestionAnswer, answer);
                        SavePageVariable(saveQuestionAnswer.QuestionAnswerGUID, answer);
                        //SavePageVariablePerDay(saveQuestionAnswer.QuestionAnswerGUID, answer);
                    }
                    else
                    {
                        Guid questionAnswerGuid = Guid.NewGuid();
                        InsertQuestionAnswerByQuestion(answer, questionAnswerGuid);
                        SavePageVariable(questionAnswerGuid, answer);
                        //SavePageVariablePerDay(questionAnswerGuid, answer);
                    }
                }
            }
        }

        private void SaveQuestionAnswerWithNoSession(UserAnswerModel answer)
        {
            QuestionAnswer saveQuestionAnswer = new QuestionAnswer();
            //DTD-1544: question answer in relpase doesn't belong to any session.

            if (string.IsNullOrEmpty(answer.PageQuestionGUID))
            {
                saveQuestionAnswer = Resolve<IQuestionAnswerRepository>().GetQuestionAnswer()
                   .Where(qa => qa.User.UserGUID == new Guid(answer.UserGuid) &&
                   qa.Program.ProgramGUID == new Guid(answer.ProgramGuid) &&
                   qa.SessionContent == null &&
                   qa.PageQuestion == null &&
                   qa.Page.PageGUID == new Guid(answer.PageGuid) &&
                   qa.RelapsePageSequenceGUID == new Guid(answer.RelapsePageSequenceGuid) &&
                   qa.RelapsePageGUID == new Guid(answer.RelapsePageGuid))
                   .OrderByDescending(qa => qa.LastUpdated)
                   .FirstOrDefault();

                if (saveQuestionAnswer != null)
                {
                    UpdatePageAnswer(saveQuestionAnswer, answer);
                    SavePageVariable(saveQuestionAnswer.QuestionAnswerGUID, answer);
                    //Why not call SavePageVariablePerDay? don't understand.
                    //SavePageVariablePerDay(saveQuestionAnswer.QuestionAnswerGUID, answer);
                }
                else
                {
                    Guid questionAnswerGuid = Guid.NewGuid();
                    InsertQuestionAnswerByPage(answer, questionAnswerGuid);
                    SavePageVariable(questionAnswerGuid, answer);
                    //SavePageVariablePerDay(questionAnswerGuid, answer);
                }
            }
            else {
                saveQuestionAnswer = Resolve<IQuestionAnswerRepository>().GetQuestionAnswer()
                    .Where(qa => qa.User.UserGUID == new Guid(answer.UserGuid) &&
                    qa.Program.ProgramGUID == new Guid(answer.ProgramGuid) &&
                    qa.SessionContent == null &&
                    qa.PageQuestion.PageQuestionGUID == new Guid(answer.PageQuestionGUID) &&
                    qa.Page.PageGUID == new Guid(answer.PageGuid) &&
                    qa.RelapsePageSequenceGUID == new Guid(answer.RelapsePageSequenceGuid) &&
                    qa.RelapsePageGUID == new Guid(answer.RelapsePageGuid))
                    .OrderByDescending(qa => qa.LastUpdated)
                    .FirstOrDefault();

                if (saveQuestionAnswer != null)
                {
                    UpdateQuestionAnswer(saveQuestionAnswer, answer);
                    SavePageVariable(saveQuestionAnswer.QuestionAnswerGUID, answer);
                    //SavePageVariablePerDay(saveQuestionAnswer.QuestionAnswerGUID, answer);
                }
                else
                {
                    Guid questionAnswerGuid = Guid.NewGuid();
                    InsertQuestionAnswerByQuestion(answer, questionAnswerGuid);
                    SavePageVariable(questionAnswerGuid, answer);
                    //SavePageVariablePerDay(questionAnswerGuid, answer);
                }
            }
        }

        private void UpdateQuestionAnswer(QuestionAnswer qa, UserAnswerModel answer)
        {
            if(!qa.PageQuestionReference.IsLoaded)
            {
                qa.PageQuestionReference.Load();
            }
            if(!qa.PageQuestion.QuestionReference.IsLoaded)
            {
                qa.PageQuestion.QuestionReference.Load();
            }
            if(!qa.QuestionAnswerValue.IsLoaded)
            {
                qa.QuestionAnswerValue.Load();
            }

            if(qa.PageQuestion != null)
            {
                // update question answer value
                if(qa.PageQuestion.Question.HasSubItem)
                {
                    // delete old value need test

                    Resolve<IQuestionAnswerValueRepository>().Delete(qa.QuestionAnswerValue);
                    qa.QuestionAnswerValue.Clear();

                    string[] items = answer.QuestionValue.Split(';');
                    foreach(string str in items)
                    {
                        if(!string.IsNullOrEmpty(str))
                        {
                            QuestionAnswerValue answervalue = new QuestionAnswerValue();
                            answervalue.PageQuestionItem = Resolve<IPageQuestionItemRepository>().Get(new Guid(str));
                            answervalue.QuestionAnswerValueGuid = Guid.NewGuid();
                            qa.QuestionAnswerValue.Add(answervalue);
                        }
                    }
                }
                else
                {
                    foreach(QuestionAnswerValue updateqav in qa.QuestionAnswerValue)
                    {
                        updateqav.UserInput = answer.QuestionValue;
                    }
                }
            }

            Resolve<IQuestionAnswerRepository>().UpdateQuestionAnswer(qa);
            //SavePageVariable(qa.QuestionAnswerGUID, answer);
        }

        private void UpdatePageAnswer(QuestionAnswer qa, UserAnswerModel answer)
        {
            if(!qa.PageReference.IsLoaded)
            {
                qa.PageReference.Load();
            }
            if(!qa.QuestionAnswerValue.IsLoaded)
            {
                qa.QuestionAnswerValue.Load();
            }

            if(qa.Page != null)
            {
                if(!qa.Page.PageTemplateReference.IsLoaded)
                {
                    qa.Page.PageTemplateReference.Load();
                }
            }

            if(qa.Page.PageTemplate.Name == "Choose preferences")
            {
                // delete old value   need test    
                Resolve<IQuestionAnswerValueRepository>().Delete(qa.QuestionAnswerValue);
                qa.QuestionAnswerValue.Clear();

                string[] items = answer.QuestionValue.Split(';');
                foreach(string str in items)
                {
                    if(!string.IsNullOrEmpty(str))
                    {
                        QuestionAnswerValue answervalue = new QuestionAnswerValue();
                        answervalue.Resource = Resolve<IResourceRepository>().GetResource(new Guid(str));
                        answervalue.QuestionAnswerValueGuid = Guid.NewGuid();
                        qa.QuestionAnswerValue.Add(answervalue);
                    }
                }
                SaveChoosePreferencePageVariable(answer);
            }
            else
            {
                foreach(QuestionAnswerValue updateqav in qa.QuestionAnswerValue)
                {
                    updateqav.UserInput = answer.QuestionValue;
                }
            }
            Resolve<IQuestionAnswerRepository>().UpdateQuestionAnswer(qa);
            //SavePageVariable(qa.QuestionAnswerGUID, answer);
        }

        private void SaveChoosePreferencePageVariable(UserAnswerModel answer)
        {
            List<Preferences> preferenceList = Resolve<IPreferencesRepository>().GetPreferenceByPageGuid(new Guid(answer.PageGuid)).ToList<Preferences>();
            foreach(Preferences reference in preferenceList)
            {
                if(!reference.PageVariableReference.IsLoaded)
                {
                    reference.PageVariableReference.Load();
                }
                if(reference.PageVariable != null)
                {
                    //save userpagevariable
                    UserPageVariable userVariable = Resolve<IUserPageVariableRepository>().GetUserPageVariableByUserAndVariable(new Guid(answer.UserGuid), reference.PageVariable.PageVariableGUID);

                    if(userVariable != null)
                    {

                        if(!userVariable.QuestionAnswerReference.IsLoaded)
                        {
                            userVariable.QuestionAnswerReference.Load();
                        }

                        userVariable.Value = answer.QuestionValue.IndexOf(reference.PreferencesGUID.ToString(), StringComparison.CurrentCultureIgnoreCase) != -1 ? "1" : "0";
                        userVariable.QuestionAnswer = null;
                        Resolve<IUserPageVariableRepository>().UpdateUserPageVariable(userVariable);
                    }
                    else
                    {
                        userVariable = new UserPageVariable();
                        userVariable.UserPageVariableGUID = Guid.NewGuid();
                        userVariable.PageVariable = reference.PageVariable;
                        userVariable.User = Resolve<IUserRepository>().GetUserByGuid(new Guid(answer.UserGuid));
                        userVariable.Value = answer.QuestionValue.IndexOf(reference.PreferencesGUID.ToString(), StringComparison.CurrentCultureIgnoreCase) != -1 ? "1" : "0";
                        Resolve<IUserPageVariableRepository>().AddUserPageVariable(userVariable);
                    }

                    //save userpagevariableperday
                    UserPageVariablePerDay userVariablePerDay;
                    //DTD-1544: if question answer in relpase doesn't belong to any session, the sessioncontentguid should be null.
                    if (!string.IsNullOrEmpty(answer.SessionGuid))
                        userVariablePerDay = Resolve<IUserPageVariablePerDayRepository>().GetUserPageVariable(new Guid(answer.UserGuid), reference.PageVariable.PageVariableGUID, new Guid(answer.SessionGuid));
                    else
                        userVariablePerDay = Resolve<IUserPageVariablePerDayRepository>().GetUserPageVariable(new Guid(answer.UserGuid), reference.PageVariable.PageVariableGUID, Guid.Empty);

                    if(userVariablePerDay != null)
                    {
                        userVariablePerDay.Value = answer.QuestionValue.IndexOf(reference.PreferencesGUID.ToString(), StringComparison.CurrentCultureIgnoreCase) != -1 ? "1" : "0";
                        userVariablePerDay.QuestionAnswer = null;
                        Resolve<IUserPageVariablePerDayRepository>().UpdateUserPageVariable(userVariablePerDay);
                    }
                    else
                    {
                        userVariablePerDay = new UserPageVariablePerDay();
                        userVariablePerDay.UserPageVariablePerDayGUID = Guid.NewGuid();
                        userVariablePerDay.PageVariable = reference.PageVariable;
                        userVariablePerDay.User = Resolve<IUserRepository>().GetUserByGuid(new Guid(answer.UserGuid));
                        userVariablePerDay.Value = answer.QuestionValue.IndexOf(reference.PreferencesGUID.ToString(), StringComparison.CurrentCultureIgnoreCase) != -1 ? "1" : "0";
                        //DTD-1544: if question answer in relpase doesn't belong to any session, the sessioncontentguid should be null.
                        if (!string.IsNullOrEmpty(answer.SessionGuid))
                            userVariablePerDay.SessionGUID = new Guid(answer.SessionGuid);
                        else
                            userVariablePerDay.SessionGUID = Guid.Empty;
                        Resolve<IUserPageVariablePerDayRepository>().InsertUserPageVariable(userVariablePerDay);
                    }
                }
            }
        }

        private void InsertQuestionAnswerByQuestion(UserAnswerModel answer, Guid questionAnswerGuid)
        {
            //save question answer
            QuestionAnswer questionanswer = new QuestionAnswer();
            questionanswer.QuestionAnswerGUID = questionAnswerGuid;
            questionanswer.PageQuestion = Resolve<IPageQuestionRepository>().Get(new Guid(answer.PageQuestionGUID));
            questionanswer.User = Resolve<IUserRepository>().GetUserByGuid(new Guid(answer.UserGuid));
            //DTD-1544: if question answer in relpase doesn't belong to any session, the sessioncontentguid should be null.
            if (!string.IsNullOrEmpty(answer.SessionGuid))
                questionanswer.SessionContent = Resolve<ISessionContentRepository>().GetSessionContentBySessionGuidAndOrderNO(new Guid(answer.SessionGuid), answer.PageSequenceOrder);
            questionanswer.Program = Resolve<IProgramRepository>().GetProgramByGuid(new Guid(answer.ProgramGuid));
            
            if (!string.IsNullOrEmpty(answer.RelapsePageSequenceGuid) && !string.IsNullOrEmpty(answer.RelapsePageGuid))
            {
                questionanswer.RelapsePageSequenceGUID = new Guid(answer.RelapsePageSequenceGuid);
                questionanswer.RelapsePageGUID = new Guid(answer.RelapsePageGuid);
            }

            if(!questionanswer.PageQuestion.QuestionReference.IsLoaded)
            {
                questionanswer.PageQuestion.QuestionReference.Load();
            }

            if(questionanswer.PageQuestion.Question.HasSubItem)
            {
                string[] items = answer.QuestionValue.Split(';');
                foreach(string str in items)
                {
                    if(!string.IsNullOrEmpty(str))
                    {
                        QuestionAnswerValue answervalue = new QuestionAnswerValue();
                        answervalue.PageQuestionItem = Resolve<IPageQuestionItemRepository>().Get(new Guid(str));
                        answervalue.QuestionAnswerValueGuid = Guid.NewGuid();
                        questionanswer.QuestionAnswerValue.Add(answervalue);
                    }
                }
            }
            else
            {
                QuestionAnswerValue answervalue = new QuestionAnswerValue();
                answervalue.UserInput = answer.QuestionValue;
                answervalue.QuestionAnswerValueGuid = Guid.NewGuid();
                questionanswer.QuestionAnswerValue.Add(answervalue);
            }
            Resolve<IQuestionAnswerRepository>().AddQuestionAnswer(questionanswer);
        }

        private void InsertQuestionAnswerByPage(UserAnswerModel answer, Guid questionAnswerGuid)
        {
            //set user's currenttime according TimeZone.
            DateTime setCurrentTimeByTimeZone = Resolve<IProgramUserService>().GetCurrentTimeByTimeZone(new Guid(answer.ProgramGuid), new Guid(answer.UserGuid), DateTime.UtcNow);
            //save question answer
            QuestionAnswer questionanswer = new QuestionAnswer();
            questionanswer.QuestionAnswerGUID = questionAnswerGuid;
            questionanswer.Page = Resolve<IPageRepository>().GetPageByPageGuid(new Guid(answer.PageGuid));
            questionanswer.User = Resolve<IUserRepository>().GetUserByGuid(new Guid(answer.UserGuid));
            //DTD-1544: if question answer in relpase doesn't belong to any session, the sessioncontentguid should be null.
            if (!string.IsNullOrEmpty(answer.SessionGuid))
                questionanswer.SessionContent = Resolve<ISessionContentRepository>().GetSessionContentBySessionGuidAndOrderNO(new Guid(answer.SessionGuid), answer.PageSequenceOrder);
            questionanswer.Program = Resolve<IProgramRepository>().GetProgramByGuid(new Guid(answer.ProgramGuid));
            questionanswer.LastUpdated = setCurrentTimeByTimeZone; //DateTime.UtcNow;
            if (!string.IsNullOrEmpty(answer.RelapsePageSequenceGuid) && !string.IsNullOrEmpty(answer.RelapsePageGuid))
            {
                questionanswer.RelapsePageSequenceGUID = new Guid(answer.RelapsePageSequenceGuid);
                questionanswer.RelapsePageGUID = new Guid(answer.RelapsePageGuid);
            }

            if(!questionanswer.Page.PageTemplateReference.IsLoaded)
            {
                questionanswer.Page.PageTemplateReference.Load();
            }

            if(questionanswer.Page.PageTemplate.Name == "Choose preferences")
            {
                string[] items = answer.QuestionValue.Split(';');
                foreach(string str in items)
                {
                    if(!string.IsNullOrEmpty(str))
                    {
                        QuestionAnswerValue answervalue = new QuestionAnswerValue();
                        answervalue.Resource = Resolve<IResourceRepository>().GetResource(new Guid(str));
                        answervalue.QuestionAnswerValueGuid = Guid.NewGuid();
                        questionanswer.QuestionAnswerValue.Add(answervalue);
                    }
                }

                Resolve<IQuestionAnswerRepository>().AddQuestionAnswer(questionanswer);

                SaveChoosePreferencePageVariable(answer);
            }
            else
            {
                QuestionAnswerValue answervalue = new QuestionAnswerValue();
                answervalue.UserInput = answer.QuestionValue;
                answervalue.QuestionAnswerValueGuid = Guid.NewGuid();
                questionanswer.QuestionAnswerValue.Add(answervalue);

                Resolve<IQuestionAnswerRepository>().AddQuestionAnswer(questionanswer);
            }
        }

        private void SavePageVariable(Guid questionAnswerGuid, UserAnswerModel answer)
        {
            QuestionAnswer questionanswer = Resolve<IQuestionAnswerRepository>().GetQuestionAnswerByGuid(questionAnswerGuid);

            //get page variable guid
            ChangeTech.Entities.PageVariable pageVariable = GetPageVariable(answer);

            if(pageVariable != null)
            {
                UserPageVariable userpagevarialbe = Resolve<IUserPageVariableRepository>().GetUserPageVariableByUserAndVariable(new Guid(answer.UserGuid), pageVariable.PageVariableGUID);
                if(userpagevarialbe != null)
                {
                    // update user page variable
                    userpagevarialbe.QuestionAnswer = questionanswer;
                    userpagevarialbe.Value = null;
                    Resolve<IUserPageVariableRepository>().UpdateUserPageVariable(userpagevarialbe);
                }
                else
                {
                    // insert user page variable
                    userpagevarialbe = new UserPageVariable();
                    userpagevarialbe.UserPageVariableGUID = Guid.NewGuid();
                    userpagevarialbe.PageVariable = pageVariable;
                    userpagevarialbe.User = Resolve<IUserRepository>().GetUserByGuid(new Guid(answer.UserGuid));
                    userpagevarialbe.QuestionAnswer = questionanswer;

                    Resolve<IUserPageVariableRepository>().AddUserPageVariable(userpagevarialbe);
                }

                UserPageVariablePerDay userpagevarialbeperday;
                //DTD-1544: if question answer in relpase doesn't belong to any session, the sessioncontentguid should be null.
                if (!string.IsNullOrEmpty(answer.SessionGuid))
                    userpagevarialbeperday = Resolve<IUserPageVariablePerDayRepository>().GetUserPageVariable(new Guid(answer.UserGuid), pageVariable.PageVariableGUID, new Guid(answer.SessionGuid));
                else
                    userpagevarialbeperday = Resolve<IUserPageVariablePerDayRepository>().GetUserPageVariable(new Guid(answer.UserGuid), pageVariable.PageVariableGUID, Guid.Empty);
                if(userpagevarialbeperday != null)
                {
                    // update user page variable
                    userpagevarialbeperday.QuestionAnswer = questionanswer;
                    userpagevarialbeperday.Value = null;
                    Resolve<IUserPageVariablePerDayRepository>().UpdateUserPageVariable(userpagevarialbeperday);
                }
                else
                {
                    // insert user page variable
                    userpagevarialbeperday = new UserPageVariablePerDay();
                    userpagevarialbeperday.UserPageVariablePerDayGUID = Guid.NewGuid();
                    userpagevarialbeperday.PageVariable = pageVariable;
                    userpagevarialbeperday.User = Resolve<IUserRepository>().GetUserByGuid(new Guid(answer.UserGuid));
                    userpagevarialbeperday.QuestionAnswer = questionanswer;
                    //DTD-1544: if question answer in relpase doesn't belong to any session, the sessioncontentguid should be null.
                    if (!string.IsNullOrEmpty(answer.SessionGuid))
                        userpagevarialbeperday.SessionGUID = new Guid(answer.SessionGuid);
                    else
                        userpagevarialbeperday.SessionGUID = Guid.Empty;

                    Resolve<IUserPageVariablePerDayRepository>().InsertUserPageVariable(userpagevarialbeperday);
                }


                //Alert: Find that most pagevariable of Gender's type is not General but Program.
                //Don't know why not insert variable of "Gender" with the existed General one.
                //So here, I add a option that the name like "gender".It also will update the db of user.
                if (pageVariable.PageVariableType.Equals(VariableTypeEnum.General.ToString(), StringComparison.OrdinalIgnoreCase) || pageVariable.Name.Trim().ToLower() == "gender")
                {
                    User userEntity = Resolve<IUserRepository>().GetUserByGuid(new Guid(answer.UserGuid));
                    switch (pageVariable.Name)
                    {
                        case "Email":
                            userEntity.Email = answer.QuestionValue;
                            break;
                        case "FirstName":
                            userEntity.FirstName = answer.QuestionValue;
                            break;
                        case "LastName":
                            userEntity.LastName = answer.QuestionValue;
                            break;
                        case "MobilePhone":
                            userEntity.MobilePhone = answer.QuestionValue;
                            break;
                        case "Gender":
                            PageQuestionItem questionItem = Resolve<IPageQuestionItemRepository>().Get(new Guid(answer.QuestionValue));
                            int gen = questionItem.Score.Value;
                            switch (gen)
                            {
                                case 1: userEntity.Gender = "Female"; break;
                                case 0: userEntity.Gender = "Male"; break;
                            }
                            break;
                    }
                    Resolve<IUserRepository>().UpdateUser(userEntity);
                }
            }
        }

        private ChangeTech.Entities.PageVariable GetPageVariable(UserAnswerModel answer)
        {
            ChangeTech.Entities.PageVariable variable = new ChangeTech.Entities.PageVariable();
            if(string.IsNullOrEmpty(answer.PageQuestionGUID))
            {
                //if RelapsePageGuid is not null, that means in Relapse page, the PageGuid is entrance Page to Relapse.
                Page page = Resolve<IPageRepository>().GetPageByPageGuid(new Guid(string.IsNullOrEmpty(answer.RelapsePageGuid) ? answer.PageGuid : answer.RelapsePageGuid));
                if (!page.PageVariableReference.IsLoaded)
                {
                    page.PageVariableReference.Load();
                }
                variable = page.PageVariable;
            }
            else
            {
                PageQuestion question = Resolve<IPageQuestionRepository>().Get(new Guid(answer.PageQuestionGUID));
                if(!question.PageVariableReference.IsLoaded)
                {
                    question.PageVariableReference.Load();
                }
                variable = question.PageVariable;
            }

            return variable;
        }
        #endregion
    }
}
