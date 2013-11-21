using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class TranslationJobTranslatorRepository : RepositoryBase, ITranslationJobTranslatorRepository
    {
        public IQueryable<TranslationJobTranslator> GetTranslatorsByTranslationJobGUID(Guid TranslationJobGUID)
        {
            return GetEntities<TranslationJobTranslator>(s => s.TranslationJobGUID == TranslationJobGUID);
        }


        public void Insert(TranslationJobTranslator translationJobTranslatorEntity)
        {
            InsertEntity(translationJobTranslatorEntity);
        }

        public void Delete(Guid translationJobTranslatorGuid)
        {
            DeleteEntity<TranslationJobTranslator>(s => s.TranslationJobTranslatorGUID == translationJobTranslatorGuid, Guid.Empty);
        }
    }
}
