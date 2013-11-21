using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class TranslationJobRepository : RepositoryBase, ITranslationJobRepository
    {
        public IQueryable<TranslationJob> GetAllTranslationJob()
        {
            return GetEntities<TranslationJob>(t => t.IsDeleted.HasValue ? t.IsDeleted == false : true).OrderBy(t => t.Program.Name);
        }

        public TranslationJob GetTranslationJobByGuid(Guid translationJobGuid)
        {
            return GetEntities<TranslationJob>(s => s.TranslationJobGUID == translationJobGuid).FirstOrDefault();
        }

        public void Insert(TranslationJob translationJob)
        {
            InsertEntity(translationJob);
        }

        public void Delete(Guid translationJobGuid)
        {
            DeleteEntity<TranslationJob>(s => s.TranslationJobGUID == translationJobGuid, Guid.Empty);
        }

        public void Update(TranslationJob entity)
        {
            UpdateEntity(entity);
        }
    }
}
