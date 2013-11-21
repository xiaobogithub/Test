using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface ITranslationJobContentRepository
    {
        IQueryable<TranslationJobContent> GetTranslationJobContentByJobGuid(Guid translationJobGuid);
        TranslationJobContent GetTranslationJobContentByGuid(Guid translationJobContentGuid);
        void Insert(TranslationJobContent translationJobContent);
        void Delete(Guid translationJobContentGUID);
        void Update(TranslationJobContent entity);
    }
}
