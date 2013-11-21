using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.IDataRepository;
using ChangeTech.Entities;
using System.Data.Objects.DataClasses;

namespace ChangeTech.SQLServerRepository
{
    public class PageQuestionItemRepository: RepositoryBase, IPageQuestionItemRepository
    {
        #region IPageQuestionItemRepository Members

        public IQueryable<PageQuestionItem> GetItemOfQuestion(Guid pageQuestionGUID)
        {
            return GetEntities<PageQuestionItem>(qi => qi.PageQuestion.PageQuestionGUID == pageQuestionGUID && 
                (qi.IsDeleted.HasValue? qi.IsDeleted.Value == false: true)).OrderBy(i=>i.Order);
        }

        public PageQuestionItem GetByPageQuestionGuidAndOrder(Guid pageQuestionGuid, int order)
        {
            return GetEntities<PageQuestionItem>(p => p.PageQuestion.PageQuestionGUID == pageQuestionGuid &&
                p.Order == order && (p.IsDeleted.HasValue ? p.IsDeleted.Value == false : true)).FirstOrDefault();
        }

        public PageQuestionItem Get(Guid pageQuestionItemGUID)
        {
            return GetEntities<PageQuestionItem>(p => p.PageQuestionItemGUID == pageQuestionItemGUID && 
                (p.IsDeleted.HasValue? p.IsDeleted.Value == false: true)).FirstOrDefault();
        }

        public void AddQuestionItem(PageQuestionItem item2Add)
        {
            InsertEntity(item2Add);
        }

        public void DeleteQuestionItem(Guid item2Delete)
        {
            DeleteEntity<PageQuestionItem>(p => p.PageQuestionItemGUID == item2Delete, Guid.Empty);
        }

        public void UpdateQuestionItem(PageQuestionItem item2Update)
        {
            UpdateEntity(item2Update);
        }

        public void DeleteQuestionItems(EntityCollection<PageQuestionItem> questionItems)
        {
            DeleteEntities<PageQuestionItem>(questionItems, Guid.Empty);
        }

        public void DeleteQuestionItemsByQuestionID(Guid questionID)
        {
            DeleteEntities<PageQuestionItem>(p => p.PageQuestion.PageQuestionGUID == questionID, Guid.Empty);
        }
        #endregion
    }
}
