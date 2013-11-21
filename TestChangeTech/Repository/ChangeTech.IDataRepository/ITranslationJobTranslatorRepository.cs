using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface ITranslationJobTranslatorRepository
    {
        IQueryable<TranslationJobTranslator> GetTranslatorsByTranslationJobGUID(Guid TranslationJobGUID);

        void Insert(TranslationJobTranslator translationJobTranslatorEntity);

        void Delete(Guid translationJobTranslatorGuid);
    }
}
