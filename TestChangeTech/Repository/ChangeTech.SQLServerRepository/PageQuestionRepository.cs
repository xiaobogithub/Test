using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.IDataRepository;
using ChangeTech.Entities;
using System.Data.Objects.DataClasses;

namespace ChangeTech.SQLServerRepository
{
    public class PageQuestionRepository : RepositoryBase, IPageQuestionRepository
    {
        #region IPageQuestionRepository Members

        public IQueryable<PageQuestion> GetPageQuestionOfPage(Guid pageGUID)
        {
            return GetEntities<PageQuestion>(p => p.Page.PageGUID == pageGUID && 
                (p.IsDeleted.HasValue? p.IsDeleted.Value == false: true)).OrderBy(p=>p.Order);
        }

        public PageQuestion GetPageQuestionByPageGuidAndQuesOrder(Guid pageGuid, int orderNum)
        {
            return GetEntities<PageQuestion>(p => p.Page.PageGUID == pageGuid &&
                (p.IsDeleted.HasValue ? p.IsDeleted.Value == false : true) &&
                p.Order == orderNum).FirstOrDefault();
        }

        public IQueryable<PageQuestion> GetPageQuestionOfVariable(Guid variableGUID)
        {
            return GetEntities<PageQuestion>(p => p.PageVariable.PageVariableGUID == variableGUID && 
                (p.IsDeleted.HasValue? p.IsDeleted.Value == false: true));
        }

        public IQueryable<PageQuestion> GetAllPageQuestinByVariable(Guid variableGUID)
        {
            return GetEntities<PageQuestion>(p => p.PageVariable.PageVariableGUID == variableGUID);
        }

        public void AddPageQuestion(PageQuestion pageQuestion2Add)
        {
            InsertEntity(pageQuestion2Add);
        }

        public void DeletePageQuestion(Guid pageQuestion2Delete)
        {
           DeleteEntity<PageQuestion>(p=>p.PageQuestionGUID == pageQuestion2Delete, Guid.Empty);
        }

        public void UpdatePageQuestion(PageQuestion pageQuestion2Update)
        {
           UpdateEntity(pageQuestion2Update);
        }

        public PageQuestion Get(Guid pageQuestion)
        {
            return GetEntities<PageQuestion>(q=>q.PageQuestionGUID == pageQuestion && 
                (q.IsDeleted.HasValue ? q.IsDeleted.Value == false: true)).FirstOrDefault();
        }

        public void DeletePageQuestions(EntityCollection<PageQuestion> pageQuestions)
        {
            DeleteEntities<PageQuestion>(pageQuestions, Guid.Empty);
        }
        #endregion
    }
}
