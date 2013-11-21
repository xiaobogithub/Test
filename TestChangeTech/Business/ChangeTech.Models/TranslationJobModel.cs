using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeTech.Models
{
    public class TranslationJobModel
    {
        public Guid TranslationJobGUID { get; set; }
        public ProgramBaseModel Program { get; set; }
        public LanguageBaseModel FromLanguage { get; set; }
        public LanguageBaseModel ToLanguage { get; set; }
        public string Translators { get; set; }
        public int FinishedElements { get; set; }
        public int Elements { get; set; }
        public int Words { get; set; }
        public string Completed { get; set; }
        public List<TranslationJobContentModel> TranslationJobContents { get; set; }

        public string Order { get; set; }
        public int DefaultTranslatedContent { get; set; }
        public string TextOfDefaultTranslatedContent
        {
            get
            {
                if (DefaultTranslatedContent == 2)
                {
                    return DefaultContentInTranslatedFieldEnum.GoogleTranslation.ToString();
                }
                else if (DefaultTranslatedContent == 3)
                {
                    return DefaultContentInTranslatedFieldEnum.Nothing.ToString();
                }
                else
                {
                    return DefaultContentInTranslatedFieldEnum.OriginalText.ToString();
                }
            }
        }
    }

    public class TranslationJobTranslatorModel
    {
        public Guid TranslationJobTranslatorGUID { get; set; }
        public Guid TranslationJobGUID { get; set; }
        public Guid TranslatorGUID { get; set; }

        public string TranslatorName { get; set; }
    }

    public class TranslationJobContentModel
    {
        public Guid TranslationJobContentGUID { get; set; }
        public Guid TranslationJobGUID { get; set; }
        public string ContentName { get; set; }
        public string Note { get; set; }
        public int FinishedElements { get; set; }
        public int Elements { get; set; }
        public int Words { get; set; }
        public string Completed { get; set; }
        public List<TranslationJobElementModel> TranslationJobElements { get; set; }
    }

    public class TranslationJobElementModel
    {
        public Guid TranslationJobElementGUID { get; set; }
        public Guid TranslationJobContentGUID { get; set; }
        public string FromObjectGUID { get; set; }
        public string ToObjectGUID { get; set; }
        public string Object { get; set; }
        public string Position { get; set; }
        public int? MaxLength { get; set; }
        public int StatusID { get; set; }
        public string Original { get; set; }
        public string GoogleTranslate { get; set; }
        public string Translated { get; set; }
        public int Words { get; set; }

        public string Order { get; set; }//****-****-****-****   day-psOrder-pOrder-PageElementOrder
        public int DefaultTranslatedContent { get; set; }
    }

    public class TranslationJobElementValueModel
    {
        public string Object { get; set; }
        public string ObjectGUID { get; set; }
        public string Position { get; set; }
        public Guid languageGuid { get; set; }
        public string Translated { get; set; }
    }

    public class TranslationJobFromContentModel
    {//used for get from object guid and from content.
        public string fromObjectGuid { get; set; }
        public string fromContent { get; set; }
    }

    public class TranslationJobElementPagePreviewModel
    {
        public string SessionGuid { get; set; }
        public string PageSequenceGuid { get; set; }
        public string PageGuid { get; set; }
        public string LanguageGuid { get; set; }
        public string UserGuid { get; set; }
    }
}
