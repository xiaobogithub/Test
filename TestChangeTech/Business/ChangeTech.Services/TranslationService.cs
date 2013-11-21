using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Contracts;
using Ethos.DependencyInjection;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;
using Ethos.Utility;

namespace ChangeTech.Services
{
    public class TranslationService : ServiceBase, ITranslationService
    {
        #region translate
        public void TranslatePageContent(Guid pageGUID, string type, string translationText)
        {
            try
            {
                PageContent pageContent = Resolve<IPageContentRepository>().Get(pageGUID).FirstOrDefault();
                if (pageContent != null)
                {
                    switch (type)
                    {
                        case "Heading":
                            pageContent.Heading = translationText;
                            break;
                        case "Body":
                            pageContent.Body = translationText;
                            break;
                        case "PrimaryButtonCaption":
                            pageContent.PrimaryButtonCaption = translationText;
                            break;
                        case "FooterText":
                            pageContent.FooterText = translationText;
                            break;
                        case "AfterShowExpression":
                            pageContent.AfterShowExpression = translationText;
                            break;
                        case "BeforeShowExpression":
                            pageContent.BeforeShowExpression = translationText;
                            break;
                    }
                    Resolve<IPageContentRepository>().Update(pageContent);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void TranslateScreenResultTemplatePageLine(Guid pageLineGUID, string type, string translationText)
        {
            try
            {
                ScreenResultTemplatePageLine pageLine = Resolve<IScreenResultTemplatePageLineRepository>().GetPageLine(pageLineGUID);
                if (pageLine != null)
                {
                    switch (type)
                    {
                        case "Text":
                            pageLine.Text = translationText;
                            break;
                    }
                    Resolve<IScreenResultTemplatePageLineRepository>().UpdatePageLine(pageLine);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void TranslatePageQuestionContent(Guid pageQuestionGUID, string type, string translationText)
        {
            PageQuestionContent questionContent = Resolve<IPageQuestionContentRepository>().GetPageQuestionContent(pageQuestionGUID).FirstOrDefault();
            if (questionContent != null)
            {
                switch (type)
                {
                    case "Caption":
                        questionContent.Caption = translationText;
                        break;
                    case "DisableCheckBox":
                        questionContent.DisableCheckBox = translationText;
                        break;
                }
                Resolve<IPageQuestionContentRepository>().UpdatePageQuestionContent(questionContent);
            }
        }

        public void TranslateProgram(Guid programGUID, string type, string translationText)
        {
            Program programEntity = Resolve<IProgramRepository>().GetProgramByGuid(programGUID);
            if (programEntity != null)
            {
                switch (type)
                {
                    case "Description":
                        programEntity.Description = translationText;
                        break;
                    case "OrderProgramText":
                        programEntity.OrderProgramText = translationText;
                        break;
                }
                programEntity.LastUpdated = DateTime.UtcNow;
                Resolve<IProgramRepository>().Update(programEntity);
            }
        }

        public void TranslateCTPP(Guid programGUID, string type, string translationText)
        {
            CTPP ctppEntity = Resolve<ICTPPRepository>().GetCTPPByProgramGuid(programGUID);
            if (ctppEntity != null)
            {
                switch (type)
                {
                    case "ProgramDescription":
                        ctppEntity.ProgramDescription = translationText;
                        break;
                    case "ProgramDescriptionTitle":
                        ctppEntity.ProgramDescriptionTitle = translationText;
                        break;
                    case "ProgramDescriptionForMobile":
                        ctppEntity.ProgramDescriptionForMobile = translationText;
                        break;
                    case "ProgramDescriptionTitleForMobile":
                        ctppEntity.ProgramDescriptionTitleForMobile = translationText;
                        break;
                }
                Resolve<ICTPPRepository>().Update(ctppEntity);
            }
        }

        public void TranslatePageQuestionItemContent(Guid pageQuestionItemGUID, string type, string translationText)
        {
            PageQuestionItemContent itemContent = Resolve<IPageQuestionItemContentRepository>().GetPageQuestionItemContent(pageQuestionItemGUID).FirstOrDefault();
            if (itemContent != null)
            {
                switch (type)
                {
                    case "Item":
                        itemContent.Item = translationText;
                        break;
                    case "Feedback":
                        itemContent.Feedback = translationText;
                        break;
                }
                Resolve<IPageQuestionItemContentRepository>().UpdatePageQuestionItemContent(itemContent);
            }
        }

        public void TranslateProgramRoom(Guid programRoomGUID, string type, string translationText)
        {
            ProgramRoom room = Resolve<IProgramRoomRepository>().GetRoom(programRoomGUID);
            if (room != null)
            {
                switch (type)
                {
                    case "Description":
                        room.Description = translationText;
                        break;
                    case "Name":
                        room.Name = translationText;
                        break;
                }
                room.LastUpdated = DateTime.UtcNow;
                Resolve<IProgramRoomRepository>().Update(room);
            }
        }

        public void TranslateGraphContent(Guid graphGUID, string type, string translationText)
        {
            GraphContent graphContent = Resolve<IGraphContentRepository>().Get(graphGUID);
            if (graphContent != null)
            {
                switch (type)
                {
                    case "Caption":
                        graphContent.Caption = translationText;
                        break;
                }
                Resolve<IGraphContentRepository>().Update(graphContent);
            }
        }

        public void TranslateEmailTemplate(Guid emailTemplateGUID, string type, string translationText)
        {
            EmailTemplate emailTemplate = Resolve<IEmailTemplateRepository>().GetEmailTemplate(emailTemplateGUID);
            if (emailTemplate != null)
            {
                switch (type)
                {
                    case "Body":
                        emailTemplate.Body = translationText;
                        break;
                    case "Name":
                        emailTemplate.Name = translationText;
                        break;
                    case "Subject":
                        emailTemplate.Subject = translationText;
                        break;
                    case "LinkText":
                        emailTemplate.LinkText = translationText;
                        break;
                }
                emailTemplate.LastUpdated = DateTime.UtcNow;
                Resolve<IEmailTemplateRepository>().Update(emailTemplate);
            }
        }

        public void TranslateGraphItemContent(Guid graphItemContentGUID, string type, string translationText)
        {
            GraphItemContent itemContent = Resolve<IGraphItemContentRepository>().Get(graphItemContentGUID);
            if (itemContent != null)
            {
                switch (type)
                {
                    case "Name":
                        itemContent.Name = translationText;
                        break;
                }
                
                Resolve<IGraphItemContentRepository>().Update(itemContent);
            }
        }

        public void TranslateHelpItem(Guid helpItemGUID, string type, string translationText)
        {
            HelpItem helpItem = Resolve<IHelpItemRepository>().GetItem(helpItemGUID);
            if (helpItem != null)
            {
                switch (type)
                {
                    case "Question":
                        helpItem.Question = translationText;
                        break;
                    case "Answer":
                        helpItem.Answer = translationText;
                        break;
                }
                Resolve<IHelpItemRepository>().Update(helpItem);
            }
        }

        public void TranslatePreference(Guid preferenceGUID, string type, string translationText)
        {
            Preferences preference = Resolve<IPreferencesRepository>().GetPreference(preferenceGUID);
            if (preference != null)
            {
                switch (type)
                {
                    case "AnswerText":
                        preference.AnswerText = translationText;
                        break;
                    case "ButtonName":
                        preference.ButtonName = translationText;
                        break;
                    case "Description":
                        preference.Description = translationText;
                        break;
                    case "Name":
                        preference.Name = translationText;
                        break;
                }
                Resolve<IPreferencesRepository>().UpdatePreference(preference);
            }
        }

        public void TranslateSession(Guid sessionGUID, string type, string translationText)
        {
            Session session = Resolve<ISessionRepository>().GetSessionBySessionGuid(sessionGUID);
            if (session != null)
            {
                switch (type)
                {
                    case "Description":
                        session.Description = translationText;
                        break;
                    case "Name":
                        session.Name = translationText;
                        break;
                }
                Resolve<ISessionRepository>().UpdateSession(session);
            }
        }

        public void TranslateSepcialString(string name, Guid languageGUID, string type, string translationText)
        {
            SpecialString specialString = Resolve<ISpecialStringRepository>().GetSpecialString(languageGUID, name);
            if (specialString != null)
            {
                switch (type)
                {
                    case "Value":
                        specialString.Value = translationText;
                        break;
                }
                Resolve<ISpecialStringRepository>().Update(specialString);
            }
        }

        public void TranslateTipMessage(Guid tipMessageGUID, string type, string translationText)
        {
            TipMessage tipMessage = Resolve<ITipMessageRepository>().GetTipMessage(tipMessageGUID);
            if (tipMessage != null)
            {
                switch (type)
                {
                    case "BackButtonName":
                        tipMessage.BackButtonName = translationText;
                        break;
                    case "Message":
                        tipMessage.Message = translationText;
                        break;
                    case "Title":
                        tipMessage.Title = translationText;
                        break;
                }
                Resolve<ITipMessageRepository>().UpdateTipMessage(tipMessage);
            }
        }

        public void TranslateUserMenu(Guid menuItemGUID, string type, string translationText)
        {
            UserMenu menu = Resolve<IUserMenuRepository>().GetUserMenu(menuItemGUID);
            if (menu != null)
            {
                switch (type)
                {
                    case "MenuFormBackButtonName":
                        menu.MenuFormBackButtonName = translationText;
                        break;
                    case "MenuFormSubmitButtonName":
                        menu.MenuFormSubmitButtonName = translationText;
                        break;
                    case "MenuFormText":
                        menu.MenuFormText = translationText;
                        break;
                    case "MenuFormTitle":
                        menu.MenuFormTitle = translationText;
                        break;
                    case "MenuText":
                        menu.MenuText = translationText;
                        break;
                }
                Resolve<IUserMenuRepository>().UpdateUserMenu(menu);
            }
        }

        public void TranslateAccessoryTemplate(Guid accessoryTemplateGUID, string type, string translationText)
        {
            AccessoryTemplate accessoryTemplate = Resolve<IProgramAccessoryRepository>().GetAccessory(accessoryTemplateGUID);
            if (accessoryTemplate != null)
            {
                switch (type)
                {
                    case "Heading":
                        accessoryTemplate.Heading = translationText;
                        break;
                    case "PasswordText":
                        accessoryTemplate.PasswordText = translationText;
                        break;
                    case "PrimaryButtonText":
                        accessoryTemplate.PrimaryButtonText = translationText;
                        break;
                    case "SecondaryButtonText":
                        accessoryTemplate.SecondaryButtonText = translationText;
                        break;
                    case "Text":
                        accessoryTemplate.Text = translationText;
                        break;
                    case "UserNameText":
                        accessoryTemplate.UserNameText = translationText;
                        break;
                }
                Resolve<IProgramAccessoryRepository>().Update(accessoryTemplate);
            }
        }

        public void TranslatePageSequence(Guid pageSequenceGUID, string type, string translationText)
        {
            PageSequence pageSequenceEntity = Resolve<IPageSequenceRepository>().GetPageSequenceByGuid(pageSequenceGUID);
            if (pageSequenceEntity != null)
            {
                switch (type)
                {
                    case "Name":
                        pageSequenceEntity.Name = translationText;
                        break;
                    case "Description":
                        pageSequenceEntity.Description = translationText;
                        break;
                }
                Resolve<IPageSequenceRepository>().UpdatePageSequence(pageSequenceEntity);
            }
        }
        #endregion

        #region getTranslationElement
        public string GetPageContent(Guid pageGUID, string type)
        {
            try
            {
                string translateValue = string.Empty;

                PageContent pageContent = Resolve<IPageContentRepository>().Get(pageGUID).FirstOrDefault();
                if (pageContent != null)
                {
                    switch (type)
                    {
                        case "Heading":
                            translateValue = pageContent.Heading;
                            break;
                        case "Body":
                            translateValue = pageContent.Body;// = translationText;
                            break;
                        case "PrimaryButtonCaption":
                            translateValue = pageContent.PrimaryButtonCaption;//= translationText;
                            break;
                        case "FooterText":
                            translateValue = pageContent.FooterText;// = translationText;
                            break;
                    }
                }
                return translateValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetPageQuestionContent(Guid pageQuestionGUID, string type)
        {
            string translateValue = string.Empty;
            PageQuestionContent questionContent = Resolve<IPageQuestionContentRepository>().GetPageQuestionContent(pageQuestionGUID).FirstOrDefault();
            if (questionContent != null)
            {
                switch (type)
                {
                    case "Caption":
                        translateValue = questionContent.Caption; //= translationText;
                        break;
                    case "DisableCheckBox":
                        translateValue = questionContent.DisableCheckBox;// = translationText;
                        break;
                }
            }
            return translateValue;
        }

        public string GetPageQuestionItemContent(Guid pageQuestionItemGUID, string type)
        {
            string translateValue = string.Empty;
            PageQuestionItemContent itemContent = Resolve<IPageQuestionItemContentRepository>().GetPageQuestionItemContent(pageQuestionItemGUID).FirstOrDefault();
            if (itemContent != null)
            {
                switch (type)
                {
                    case "Item":
                        translateValue = itemContent.Item;// = translationText;
                        break;
                    case "Feedback":
                        translateValue = itemContent.Feedback;//= translationText;
                        break;
                }
            }
            return translateValue;
        }

        public string GetProgramRoom(Guid programRoomGUID, string type)
        {
            string translateValue = string.Empty;

            ProgramRoom room = Resolve<IProgramRoomRepository>().GetRoom(programRoomGUID);
            if (room != null)
            {
                switch (type)
                {
                    case "Description":
                        translateValue = room.Description;//= translationText;
                        break;
                    case "Name":
                        translateValue = room.Name;//= translationText;
                        break;
                }
            }
            return translateValue;
        }

        public string GetGraphContent(Guid graphGUID, string type)
        {
            string translateValue = string.Empty;

            GraphContent graphContent = Resolve<IGraphContentRepository>().Get(graphGUID);
            if (graphContent != null)
            {
                switch (type)
                {
                    case "Caption":
                        translateValue = graphContent.Caption;//= translationText;
                        break;
                }
            }
            return translateValue;
        }

        public string GetEmailTemplate(Guid emailTemplateGUID, string type)
        {
            string translateValue = string.Empty;

            EmailTemplate emailTemplate = Resolve<IEmailTemplateRepository>().GetEmailTemplate(emailTemplateGUID);
            if (emailTemplate != null)
            {
                switch (type)
                {
                    case "Body":
                        translateValue = emailTemplate.Body;//= translationText;
                        break;
                    case "Name":
                        translateValue = emailTemplate.Name;//= translationText;
                        break;
                    case "Subject":
                        translateValue = emailTemplate.Subject;//= translationText;
                        break;
                }
            }
            return translateValue;
        }

        public string GetGraphItemContent(Guid graphItemContentGUID, string type)
        {
            string translateValue = string.Empty;

            GraphItemContent itemContent = Resolve<IGraphItemContentRepository>().Get(graphItemContentGUID);
            if (itemContent != null)
            {
                switch (type)
                {
                    case "Name":
                        translateValue = itemContent.Name;//= translationText;
                        break;
                }
            }
            return translateValue;
        }

        public string GetHelpItem(Guid helpItemGUID, string type)
        {
            string translateValue = string.Empty;

            HelpItem helpItem = Resolve<IHelpItemRepository>().GetItem(helpItemGUID);
            if (helpItem != null)
            {
                switch (type)
                {
                    case "Question":
                        translateValue = helpItem.Question;//= translationText;
                        break;
                    case "Answer":
                        translateValue = helpItem.Answer;//= translationText;
                        break;
                }
            }
            return translateValue;
        }

        public string GetPreference(Guid preferenceGUID, string type)
        {
            string translateValue = string.Empty;

            Preferences preference = Resolve<IPreferencesRepository>().GetPreference(preferenceGUID);
            if (preference != null)
            {
                switch (type)
                {
                    case "AnswerText":
                        translateValue = preference.AnswerText;//= translationText;
                        break;
                    case "ButtonName":
                        translateValue = preference.ButtonName;//= translationText;
                        break;
                    case "Description":
                        translateValue = preference.Description;//= translationText;
                        break;
                    case "Name":
                        translateValue = preference.Name;//= translationText;
                        break;
                }
            }
            return translateValue;
        }

        public string GetSession(Guid sessionGUID, string type)
        {
            string translateValue = string.Empty;

            Session session = Resolve<ISessionRepository>().GetSessionBySessionGuid(sessionGUID);
            if (session != null)
            {
                switch (type)
                {
                    case "Description":
                        translateValue = session.Description;//= translationText;
                        break;
                    case "Name":
                        translateValue = session.Name;//= translationText;
                        break;
                }
            }
            return translateValue;
        }

        public string GetSepcialString(string name, Guid languageGUID, string type)
        {
            string translateValue = string.Empty;

            SpecialString specialString = Resolve<ISpecialStringRepository>().GetSpecialString(languageGUID, name);
            if (specialString != null)
            {
                switch (type)
                {
                    case "Value":
                        translateValue = specialString.Value;//= translationText;
                        break;
                }
            }
            return translateValue;
        }

        public string GetTipMessage(Guid tipMessageGUID, string type)
        {
            string translateValue = string.Empty;

            TipMessage tipMessage = Resolve<ITipMessageRepository>().GetTipMessage(tipMessageGUID);
            if (tipMessage != null)
            {
                switch (type)
                {
                    case "BackButtonName":
                        translateValue = tipMessage.BackButtonName;//= translationText;
                        break;
                    case "Message":
                        translateValue = tipMessage.Message;//= translationText;
                        break;
                    case "Title":
                        translateValue = tipMessage.Title;//= translationText;
                        break;
                }
            }
            return translateValue;
        }

        public string GetUserMenu(Guid menuItemGUID, string type)
        {
            string translateValue = string.Empty;

            UserMenu menu = Resolve<IUserMenuRepository>().GetUserMenu(menuItemGUID);
            if (menu != null)
            {
                switch (type)
                {
                    case "MenuFormBackButtonName":
                        translateValue = menu.MenuFormBackButtonName;//= translationText;
                        break;
                    case "MenuFormSubmitButtonName":
                        translateValue = menu.MenuFormSubmitButtonName;//= translationText;
                        break;
                    case "MenuFormText":
                        translateValue = menu.MenuFormText;//= translationText;
                        break;
                    case "MenuFormTitle":
                        translateValue = menu.MenuFormTitle;//= translationText;
                        break;
                    case "MenuText":
                        translateValue = menu.MenuText;//= translationText;
                        break;
                }
            }
            return translateValue;
        }

        public string GetAccessoryTemplate(Guid accessoryTemplateGUID, string type)
        {
            string translateValue = string.Empty;
            AccessoryTemplate accessoryTemplate = Resolve<IProgramAccessoryRepository>().GetAccessory(accessoryTemplateGUID);
            if (accessoryTemplate != null)
            {
                switch (type)
                {
                    case "Heading":
                        translateValue = accessoryTemplate.Heading;//= translationText;
                        break;
                    case "PasswordText":
                        translateValue = accessoryTemplate.PasswordText;//= translationText;
                        break;
                    case "PrimaryButtonText":
                        translateValue = accessoryTemplate.PrimaryButtonText;//= translationText;
                        break;
                    case "SecondaryButtonText":
                        translateValue = accessoryTemplate.SecondaryButtonText;//= translationText;
                        break;
                    case "Text":
                        translateValue = accessoryTemplate.Text;//= translationText;
                        break;
                    case "UserNameText":
                        translateValue = accessoryTemplate.UserNameText;//= translationText;
                        break;
                }
            }
            return translateValue;
        }

        public string GetPageSequence(Guid pageSequenceGUID, string type)
        {
            string translateValue = string.Empty;

            PageSequence pageSequenceEntity = Resolve<IPageSequenceRepository>().GetPageSequenceByGuid(pageSequenceGUID);
            if (pageSequenceEntity != null)
            {
                switch (type)
                {
                    case "Name":
                        translateValue = pageSequenceEntity.Name;//= translationText;
                        break;
                    case "Description":
                        translateValue = pageSequenceEntity.Description;//= translationText;
                        break;
                }
            }
            return translateValue;
        }

        /// <summary>
        /// Get ScreenResultTemplatePageLine Entity By pageLineGuid.
        /// </summary>
        public string GetScreenResultTemplatePageLine(Guid pageLineGUID, string type)
        {
            try
            {
                string translateValue = string.Empty;
                ScreenResultTemplatePageLine pageLine = Resolve<IScreenResultTemplatePageLineRepository>().GetPageLine(pageLineGUID);
                if (pageLine != null)
                {
                    switch (type)
                    {
                        case "Text":
                            translateValue = pageLine.Text;
                            break;
                    }
                }
                return translateValue;
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }
        #endregion

        #region updateElement when from table update
        public void UpdateElementFromPageContent(Guid pageGUID)
        {
            try
            {
                PageContent pageContent = Resolve<IPageContentRepository>().Get(pageGUID).FirstOrDefault();
                List<TranslationJobElement> elementEntities = Resolve<ITranslationJobElementRepository>().GetTranslationJobElementByFromObjectGuid(pageGUID.ToString()).ToList();
                if (pageContent != null && elementEntities != null && elementEntities.Count>0)
                {
                    foreach (TranslationJobElement element in elementEntities)
                    {
                        switch (element.Position)
                        {
                            case "Heading":
                                element.FromContent = pageContent.Heading;
                                element.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Changed;
                                Resolve<ITranslationJobElementRepository>().Update(element);
                                break;
                            case "Body":
                                element.FromContent = pageContent.Body;
                                element.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Changed;
                                Resolve<ITranslationJobElementRepository>().Update(element);
                                break;
                            case "PrimaryButtonCaption":
                                element.FromContent = pageContent.PrimaryButtonCaption;
                                element.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Changed;
                                Resolve<ITranslationJobElementRepository>().Update(element);
                                break;
                            case "FooterText":
                                element.FromContent = pageContent.FooterText;
                                element.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Changed;
                                Resolve<ITranslationJobElementRepository>().Update(element);
                                break;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateElementFromScreenResultTemplatePageLine(Guid pageLineGuid)
        {
            try
            {
                ScreenResultTemplatePageLine pageLine = Resolve<IScreenResultTemplatePageLineRepository>().GetPageLine(pageLineGuid);
                List<TranslationJobElement> elementEntities = Resolve<ITranslationJobElementRepository>().GetTranslationJobElementByFromObjectGuid(pageLineGuid.ToString()).ToList();
                if (pageLine != null && elementEntities != null && elementEntities.Count > 0)
                {
                    foreach (TranslationJobElement element in elementEntities)
                    {
                        switch (element.Position)
                        {
                            case "Text":
                                element.FromContent = pageLine.Text;
                                element.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Changed;
                                Resolve<ITranslationJobElementRepository>().Update(element);
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateElementFromPageQuestionContent(Guid pageQuestionGUID)
        {
            PageQuestionContent questionContent = Resolve<IPageQuestionContentRepository>().GetPageQuestionContent(pageQuestionGUID).FirstOrDefault();
            List<TranslationJobElement> elementEntities = Resolve<ITranslationJobElementRepository>().GetTranslationJobElementByFromObjectGuid(pageQuestionGUID.ToString()).ToList();
            if (questionContent != null && elementEntities != null && elementEntities.Count > 0)
            {
                foreach (TranslationJobElement element in elementEntities)
                {
                    switch (element.Position)
                    {
                        case "Caption":
                            element.FromContent = questionContent.Caption;
                            element.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Changed;
                            Resolve<ITranslationJobElementRepository>().Update(element);
                            break;
                        case "DisableCheckBox":
                            element.FromContent = questionContent.DisableCheckBox;
                            element.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Changed;
                            Resolve<ITranslationJobElementRepository>().Update(element);
                            break;
                    }
                }
            }
        }

        public void UpdateElementFromPageQuestionItemContent(Guid pageQuestionItemGUID)
        {
            string translateValue = string.Empty;
            PageQuestionItemContent itemContent = Resolve<IPageQuestionItemContentRepository>().GetPageQuestionItemContent(pageQuestionItemGUID).FirstOrDefault();
            List<TranslationJobElement> elementEntities = Resolve<ITranslationJobElementRepository>().GetTranslationJobElementByFromObjectGuid(pageQuestionItemGUID.ToString()).ToList();

            if (itemContent != null && elementEntities != null && elementEntities.Count > 0)
            {
                foreach (TranslationJobElement element in elementEntities)
                {
                    switch (element.Position)
                    {
                        case "Item":
                            element.FromContent = itemContent.Item;
                            element.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Changed;
                            Resolve<ITranslationJobElementRepository>().Update(element);
                            break;
                        case "Feedback":
                            element.FromContent = itemContent.Feedback;
                            element.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Changed;
                            Resolve<ITranslationJobElementRepository>().Update(element);
                            break;
                    }
                }
            }
        }

        public void UpdateElementFromProgramRoom(Guid programRoomGUID)
        {
            ProgramRoom room = Resolve<IProgramRoomRepository>().GetRoom(programRoomGUID);
            List<TranslationJobElement> elementEntities = Resolve<ITranslationJobElementRepository>().GetTranslationJobElementByFromObjectGuid(programRoomGUID.ToString()).ToList();

            if (room != null && elementEntities != null && elementEntities.Count > 0)
            {
                foreach (TranslationJobElement element in elementEntities)
                {
                    switch (element.Position)
                    {
                        case "Description":
                            element.FromContent = room.Description;
                            element.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Changed;
                            Resolve<ITranslationJobElementRepository>().Update(element);
                            break;
                        case "Name":
                            element.FromContent = room.Name;
                            element.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Changed;
                            Resolve<ITranslationJobElementRepository>().Update(element);
                            break;
                    }
                }
            }
        }

        public void UpdateElementFromGraphContent(Guid graphGUID)
        {
            GraphContent graphContent = Resolve<IGraphContentRepository>().Get(graphGUID);
            List<TranslationJobElement> elementEntities = Resolve<ITranslationJobElementRepository>().GetTranslationJobElementByFromObjectGuid(graphGUID.ToString()).ToList();

            if (graphContent != null && elementEntities != null && elementEntities.Count > 0)
            {
                foreach (TranslationJobElement element in elementEntities)
                {
                    switch (element.Position)
                    {
                        case "Caption":
                            element.FromContent = graphContent.Caption;
                            element.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Changed;
                            Resolve<ITranslationJobElementRepository>().Update(element);
                            break;
                    }
                }
            }
        }

        public void UpdateElementFromEmailTemplate(Guid emailTemplateGUID)
        {
            EmailTemplate emailTemplate = Resolve<IEmailTemplateRepository>().GetEmailTemplate(emailTemplateGUID);
            List<TranslationJobElement> elementEntities = Resolve<ITranslationJobElementRepository>().GetTranslationJobElementByFromObjectGuid(emailTemplateGUID.ToString()).ToList();

            if (emailTemplate != null && elementEntities != null && elementEntities.Count > 0)
            {
                foreach (TranslationJobElement element in elementEntities)
                {
                    switch (element.Position)
                    {
                        case "Body":
                            element.FromContent = emailTemplate.Body;
                            element.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Changed;
                            Resolve<ITranslationJobElementRepository>().Update(element);
                            break;
                        case "Name":
                            element.FromContent = emailTemplate.Name;
                            element.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Changed;
                            Resolve<ITranslationJobElementRepository>().Update(element);
                            break;
                        case "Subject":
                            element.FromContent = emailTemplate.Subject;
                            element.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Changed;
                            Resolve<ITranslationJobElementRepository>().Update(element);
                            break;
                        case "LinkText":
                            element.FromContent = emailTemplate.LinkText;
                            element.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Changed;
                            Resolve<ITranslationJobElementRepository>().Update(element);
                            break;
                    }
                }
            }
        }

        public void UpdateElementFromGraphItemContent(Guid graphItemContentGUID)
        {
            GraphItemContent itemContent = Resolve<IGraphItemContentRepository>().Get(graphItemContentGUID);
            List<TranslationJobElement> elementEntities = Resolve<ITranslationJobElementRepository>().GetTranslationJobElementByFromObjectGuid(graphItemContentGUID.ToString()).ToList();

            if (itemContent != null && elementEntities != null && elementEntities.Count > 0)
            {
                foreach (TranslationJobElement element in elementEntities)
                {
                    switch (element.Position)
                    {
                        case "Name":
                            element.FromContent = itemContent.Name;
                            element.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Changed;
                            Resolve<ITranslationJobElementRepository>().Update(element);
                            break;
                    }
                }
            }
        }

        public void UpdateElementFromHelpItem(Guid helpItemGUID)
        {
            HelpItem helpItem = Resolve<IHelpItemRepository>().GetItem(helpItemGUID);
            List<TranslationJobElement> elementEntities = Resolve<ITranslationJobElementRepository>().GetTranslationJobElementByFromObjectGuid(helpItemGUID.ToString()).ToList();

            if (helpItem != null && elementEntities != null && elementEntities.Count > 0)
            {
                foreach (TranslationJobElement element in elementEntities)
                {
                    switch (element.Position)
                    {
                        case "Question":
                            element.FromContent = helpItem.Question;
                            element.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Changed;
                            Resolve<ITranslationJobElementRepository>().Update(element);
                            break;
                        case "Answer":
                            element.FromContent = helpItem.Answer;
                            element.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Changed;
                            Resolve<ITranslationJobElementRepository>().Update(element);
                            break;
                    }
                }
            }
        }

        public void UpdateElementFromPreference(Guid preferenceGUID)
        {
            Preferences preference = Resolve<IPreferencesRepository>().GetPreference(preferenceGUID);
            List<TranslationJobElement> elementEntities = Resolve<ITranslationJobElementRepository>().GetTranslationJobElementByFromObjectGuid(preferenceGUID.ToString()).ToList();
            if (preference != null && elementEntities != null && elementEntities.Count > 0)
            {
                foreach (TranslationJobElement element in elementEntities)
                {
                    switch (element.Position)
                    {
                        case "AnswerText":
                            element.FromContent = preference.AnswerText;
                            element.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Changed;
                            Resolve<ITranslationJobElementRepository>().Update(element);
                            break;
                        case "ButtonName":
                            element.FromContent = preference.ButtonName;
                            element.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Changed;
                            Resolve<ITranslationJobElementRepository>().Update(element);
                            break;
                        case "Description":
                            element.FromContent = preference.Description;
                            element.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Changed;
                            Resolve<ITranslationJobElementRepository>().Update(element);
                            break;
                        case "Name":
                            element.FromContent = preference.Name;
                            element.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Changed;
                            Resolve<ITranslationJobElementRepository>().Update(element);
                            break;
                    }
                }
            }
        }

        public void UpdateElementFromSession(Guid sessionGUID)
        {
            Session session = Resolve<ISessionRepository>().GetSessionBySessionGuid(sessionGUID);
            List<TranslationJobElement> elementEntities = Resolve<ITranslationJobElementRepository>().GetTranslationJobElementByFromObjectGuid(sessionGUID.ToString()).ToList();
            if (session != null && elementEntities != null && elementEntities.Count > 0)
            {
                foreach (TranslationJobElement element in elementEntities)
                {
                    switch (element.Position)
                    {
                        case "Description":
                            element.FromContent = session.Description;
                            element.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Changed;
                            Resolve<ITranslationJobElementRepository>().Update(element);
                            break;
                        case "Name":
                            element.FromContent = session.Name;
                            element.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Changed;
                            Resolve<ITranslationJobElementRepository>().Update(element);
                            break;
                    }
                }
            }
        }


        public void UpdateElementFromSepcialString(string name, Guid languageGuid)
        {
            SpecialString specialString = Resolve<ISpecialStringRepository>().GetSpecialString(languageGuid, name);
            List<TranslationJobElement> elementEntities = Resolve<ITranslationJobElementRepository>().GetTranslationJobElementByFromObjectGuidForSpeString(name, languageGuid).ToList();
            if (specialString != null && elementEntities != null && elementEntities.Count > 0)
            {
                foreach (TranslationJobElement element in elementEntities)
                {
                    switch (element.Position)
                    {
                        case "Value":
                            element.FromContent = specialString.Value;
                            element.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Changed;
                            Resolve<ITranslationJobElementRepository>().Update(element);
                            break;

                    }
                }
            }
        }

        public void UpdateElementFromTipMessage(Guid tipMessageGUID)
        {
            TipMessage tipMessage = Resolve<ITipMessageRepository>().GetTipMessage(tipMessageGUID);
            List<TranslationJobElement> elementEntities = Resolve<ITranslationJobElementRepository>().GetTranslationJobElementByFromObjectGuid(tipMessageGUID.ToString()).ToList();
            if (tipMessage != null && elementEntities != null && elementEntities.Count > 0)
            {
                foreach (TranslationJobElement element in elementEntities)
                {
                    switch (element.Position)
                    {
                        case "BackButtonName":
                            element.FromContent = tipMessage.BackButtonName;
                            element.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Changed;
                            Resolve<ITranslationJobElementRepository>().Update(element);
                            break;
                        case "Message":
                            element.FromContent = tipMessage.Message;
                            element.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Changed;
                            Resolve<ITranslationJobElementRepository>().Update(element);
                            break;
                        case "Title":
                            element.FromContent = tipMessage.Title;
                            element.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Changed;
                            Resolve<ITranslationJobElementRepository>().Update(element);
                            break;
                    }
                }
            }
        }

        public void UpdateElementFromUserMenu(Guid menuItemGUID)
        {
            UserMenu menu = Resolve<IUserMenuRepository>().GetUserMenu(menuItemGUID);
            List<TranslationJobElement> elementEntities = Resolve<ITranslationJobElementRepository>().GetTranslationJobElementByFromObjectGuid(menuItemGUID.ToString()).ToList();
            if (menu != null && elementEntities != null && elementEntities.Count > 0)
            {
                foreach (TranslationJobElement element in elementEntities)
                {
                    switch (element.Position)
                    {
                        case "MenuFormBackButtonName":
                            element.FromContent = menu.MenuFormBackButtonName;
                            element.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Changed;
                            Resolve<ITranslationJobElementRepository>().Update(element);
                            break;
                        case "MenuFormSubmitButtonName":
                            element.FromContent = menu.MenuFormSubmitButtonName;
                            element.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Changed;
                            Resolve<ITranslationJobElementRepository>().Update(element);
                            break;
                        case "MenuFormText":
                            element.FromContent = menu.MenuFormText;
                            element.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Changed;
                            Resolve<ITranslationJobElementRepository>().Update(element);
                            break;
                        case "MenuFormTitle":
                            element.FromContent = menu.MenuFormTitle;
                            element.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Changed;
                            Resolve<ITranslationJobElementRepository>().Update(element);
                            break;
                        case "MenuText":
                            element.FromContent = menu.MenuText;
                            element.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Changed;
                            Resolve<ITranslationJobElementRepository>().Update(element);
                            break;
                    }
                }
            }
        }

        public void UpdateElementFromAccessoryTemplate(Guid accessoryTemplateGUID)
        {
            AccessoryTemplate accessoryTemplate = Resolve<IProgramAccessoryRepository>().GetAccessory(accessoryTemplateGUID);
            List<TranslationJobElement> elementEntities = Resolve<ITranslationJobElementRepository>().GetTranslationJobElementByFromObjectGuid(accessoryTemplateGUID.ToString()).ToList();
            if (accessoryTemplate != null && elementEntities != null && elementEntities.Count > 0)
            {
                foreach (TranslationJobElement element in elementEntities)
                {
                    switch (element.Position)
                    {
                        case "Heading":
                            element.FromContent = accessoryTemplate.Heading;
                            element.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Changed;
                            Resolve<ITranslationJobElementRepository>().Update(element);
                            break;
                        case "PasswordText":
                            element.FromContent = accessoryTemplate.PasswordText;
                            element.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Changed;
                            Resolve<ITranslationJobElementRepository>().Update(element);
                            break;
                        case "PrimaryButtonText":
                            element.FromContent = accessoryTemplate.PrimaryButtonText;
                            element.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Changed;
                            Resolve<ITranslationJobElementRepository>().Update(element);
                            break;
                        case "SecondaryButtonText":
                            element.FromContent = accessoryTemplate.SecondaryButtonText;
                            element.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Changed;
                            Resolve<ITranslationJobElementRepository>().Update(element);
                            break;
                        case "Text":
                            element.FromContent = accessoryTemplate.Text;
                            element.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Changed;
                            Resolve<ITranslationJobElementRepository>().Update(element);
                            break;
                        case "UserNameText":
                            element.FromContent = accessoryTemplate.UserNameText;
                            element.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Changed;
                            Resolve<ITranslationJobElementRepository>().Update(element);
                            break;
                    }
                }
            }
        }

        public void UpdateElementFromPageSequence(Guid pageSequenceGUID)
        {
            PageSequence pageSequenceEntity = Resolve<IPageSequenceRepository>().GetPageSequenceByGuid(pageSequenceGUID);
            List<TranslationJobElement> elementEntities = Resolve<ITranslationJobElementRepository>().GetTranslationJobElementByFromObjectGuid(pageSequenceGUID.ToString()).ToList();

            if (pageSequenceEntity != null && elementEntities != null && elementEntities.Count > 0)
            {
                foreach (TranslationJobElement element in elementEntities)
                {
                    switch (element.Position)
                    {
                        case "Name":
                            element.FromContent = pageSequenceEntity.Name;
                            element.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Changed;
                            Resolve<ITranslationJobElementRepository>().Update(element);
                            break;
                        case "Description":
                            element.FromContent = pageSequenceEntity.Description;
                            element.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Changed;
                            Resolve<ITranslationJobElementRepository>().Update(element);
                            break;
                    }
                }
            }
        }
        #endregion
    }
}
