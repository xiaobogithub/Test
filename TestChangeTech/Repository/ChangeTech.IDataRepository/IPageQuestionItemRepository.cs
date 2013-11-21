using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IPageQuestionItemRepository
    {
        IQueryable<PageQuestionItem> GetItemOfQuestion(Guid pageQuestionGUID);
        PageQuestionItem GetByPageQuestionGuidAndOrder(Guid pageQuestionGuid, int order);
        PageQuestionItem Get(Guid pageQuestionItemGUID);
        void AddQuestionItem(PageQuestionItem item2Add);
        void DeleteQuestionItem(Guid item2Delete);
        void DeleteQuestionItems(EntityCollection<PageQuestionItem> questionItems);
        void DeleteQuestionItemsByQuestionID(Guid questionID);
        void UpdateQuestionItem(PageQuestionItem item2Update);
    }
}
