using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeTech.Contracts
{
    public interface ITranslationService
    {
        void TranslatePageContent(Guid pageGUID, string type, string translationText);
        void TranslatePageQuestionContent(Guid pageQuestionGUID, string type, string translationText);
        void TranslatePageQuestionItemContent(Guid pageQuestionItemGUID, string type, string translationText);
        void TranslateProgram(Guid emailTemplateGUID, string type, string translationText);
        void TranslateCTPP(Guid programGUID, string type, string translationText);
        void TranslateProgramRoom(Guid programRoomGUID, string type, string translationText);
        void TranslateGraphContent(Guid graphGUID, string type, string translationText);
        void TranslateEmailTemplate(Guid emailTemplateGUID, string type, string translationText);
        void TranslateGraphItemContent(Guid graphItemContentGUID, string type, string translationText);
        void TranslateHelpItem(Guid helpItemGUID, string type, string translationText);
        void TranslatePreference(Guid preferenceGUID, string type, string translationText);
        void TranslateSession(Guid sessionGUID, string type, string translationText);
        void TranslateSepcialString(string name, Guid languageGUID, string type, string translationText);
        void TranslateTipMessage(Guid tipMessageGUID, string type, string translationText);
        void TranslateUserMenu(Guid menuItemGUID, string type, string translationText);
        void TranslateAccessoryTemplate(Guid accessoryTemplateGUID, string type, string translationText);
        void TranslatePageSequence(Guid pageSequenceGUID, string type, string translationText);
        void TranslateScreenResultTemplatePageLine(Guid pageLineGUID, string type, string translationText);

        string GetPageContent(Guid pageGUID, string type);
        string GetPageQuestionContent(Guid pageQuestionGUID, string type);
        string GetPageQuestionItemContent(Guid pageQuestionItemGUID, string type);
        string GetProgramRoom(Guid programRoomGUID, string type);
        string GetGraphContent(Guid graphGUID, string type);
        string GetEmailTemplate(Guid emailTemplateGUID, string type);
        string GetGraphItemContent(Guid graphItemContentGUID, string type);
        string GetHelpItem(Guid helpItemGUID, string type);
        string GetPreference(Guid preferenceGUID, string type);
        string GetSession(Guid sessionGUID, string type);
        string GetSepcialString(string name, Guid languageGUID, string type);
        string GetTipMessage(Guid tipMessageGUID, string type);
        string GetUserMenu(Guid menuItemGUID, string type);
        string GetAccessoryTemplate(Guid accessoryTemplateGUID, string type);
        string GetPageSequence(Guid pageSequenceGUID, string type);
        string GetScreenResultTemplatePageLine(Guid pageLineGUID, string type);



        void UpdateElementFromPageContent(Guid pageGUID);
        void UpdateElementFromPageQuestionContent(Guid pageQuestionGUID);
        void UpdateElementFromPageQuestionItemContent(Guid pageQuestionItemGUID);
        void UpdateElementFromProgramRoom(Guid programRoomGUID);
        void UpdateElementFromGraphContent(Guid graphGUID);
        void UpdateElementFromEmailTemplate(Guid emailTemplateGUID);
        void UpdateElementFromGraphItemContent(Guid graphItemContentGUID);
        void UpdateElementFromHelpItem(Guid helpItemGUID);
        void UpdateElementFromPreference(Guid preferenceGUID);
        void UpdateElementFromSession(Guid sessionGUID);
        void UpdateElementFromSepcialString(string name, Guid languageGuid);
        void UpdateElementFromTipMessage(Guid tipMessageGUID);
        void UpdateElementFromUserMenu(Guid menuItemGUID);
        void UpdateElementFromAccessoryTemplate(Guid accessoryTemplateGUID);
        void UpdateElementFromPageSequence(Guid pageSequenceGUID);
        void UpdateElementFromScreenResultTemplatePageLine(Guid pageLineGuid);
    }
}
