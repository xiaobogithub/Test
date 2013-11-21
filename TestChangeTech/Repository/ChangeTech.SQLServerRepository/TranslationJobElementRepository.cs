using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class TranslationJobElementRepository:RepositoryBase,ITranslationJobElementRepository
    {
        public TranslationJobElement GetTranslationJobElementByGuid(Guid translationJobElementGuid)
        {
            return GetEntities<TranslationJobElement>(s => s.TranslationJobElementGUID == translationJobElementGuid).FirstOrDefault();
        }

        public IQueryable<TranslationJobElement> GetTranslationJobElementsByJobGuid(Guid translationJobGuid)
        {
            return GetEntities<TranslationJobElement>(s => s.TranslationJobContent.TranslationJobGUID == translationJobGuid);
        }

        public IQueryable<TranslationJobElement> GetTranslationJobElementByContentGuid(Guid translationJobContentGuid)
        {
            return GetEntities<TranslationJobElement>(s => s.TranslationJobContentGUID == translationJobContentGuid);
        }

        public IQueryable<TranslationJobElement> GetTranslationJobElementByFromObjectGuidAndPosition(string fromObjectGuid, string position)
        {
            return GetEntities<TranslationJobElement>(s => s.FromObjectGUID.ToLower() == fromObjectGuid.ToLower() && s.Position.ToLower() == position.ToLower());
        }

        public IQueryable<TranslationJobElement> GetTranslationJobElementByFromObjectGuid(string fromObjectGuid)
        {
            return GetEntities<TranslationJobElement>(s => s.FromObjectGUID.ToLower() == fromObjectGuid.ToLower());
        }

        public IQueryable<TranslationJobElement> GetTranslationJobElementByFromObjectGuidForSpeString(string fromObjectGuid,Guid languageGuid)
        {
            return GetEntities<TranslationJobElement>(s => s.FromObjectGUID.ToLower() == fromObjectGuid.ToLower() && s.TranslationJobContent.TranslationJob.FromLanguageGUID == languageGuid);
        }

        public void Insert(TranslationJobElement translationJobElement)
        {
            InsertEntity(translationJobElement);
        }

        public void Update(TranslationJobElement entity)
        {
            UpdateEntity(entity);
        }
    }
}
