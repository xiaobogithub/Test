using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface ITranslationJobRepository
    {
        IQueryable<TranslationJob> GetAllTranslationJob();
        TranslationJob GetTranslationJobByGuid(Guid translationJobGuid);

        void Insert(TranslationJob translationJob);
        void Delete(Guid translationJobGuid);
        void Update(TranslationJob translationJob);
    }
}
