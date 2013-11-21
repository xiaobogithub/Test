using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface ITranslationJobElementRepository
    {
        TranslationJobElement GetTranslationJobElementByGuid(Guid translationJobElementGuid);
        IQueryable<TranslationJobElement> GetTranslationJobElementsByJobGuid(Guid translationJobGuid);
        IQueryable<TranslationJobElement> GetTranslationJobElementByContentGuid(Guid translationJobContentGuid);
        void Insert(TranslationJobElement translationJobElement);
        void Update(TranslationJobElement entity);
        IQueryable<TranslationJobElement> GetTranslationJobElementByFromObjectGuidAndPosition(string fromObjectGuid, string position);
        IQueryable<TranslationJobElement> GetTranslationJobElementByFromObjectGuid(string fromObjectGuid);
        IQueryable<TranslationJobElement> GetTranslationJobElementByFromObjectGuidForSpeString(string fromObjectGuid, Guid languageGuid);
    }
}
