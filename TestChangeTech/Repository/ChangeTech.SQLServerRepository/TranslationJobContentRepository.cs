using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class TranslationJobContentRepository:RepositoryBase,ITranslationJobContentRepository
    {
        public IQueryable<TranslationJobContent> GetTranslationJobContentByJobGuid(Guid translationJobGuid)
        {
            return GetEntities<TranslationJobContent>(s => s.TranslationJobGUID == translationJobGuid);
        }

        public TranslationJobContent GetTranslationJobContentByGuid(Guid translationJobContentGuid)
        {
            return GetEntities<TranslationJobContent>(t => t.TranslationJobContentGUID == translationJobContentGuid).FirstOrDefault();
        }

        public void Insert(TranslationJobContent translationJobContent)
        {
            InsertEntity(translationJobContent);
        }

        public void Delete(Guid translationJobContentGUID)
        {
            DeleteEntity<TranslationJobContent>(t => t.TranslationJobContentGUID == translationJobContentGUID, Guid.Empty);
        }

        public void Update(TranslationJobContent entity)
        {
            UpdateEntity(entity);
        }
    }
}
