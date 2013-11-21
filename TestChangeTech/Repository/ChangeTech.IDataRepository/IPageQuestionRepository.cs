using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IPageQuestionRepository
    {
        IQueryable<PageQuestion> GetPageQuestionOfPage(Guid pageGUID);
        IQueryable<PageQuestion> GetPageQuestionOfVariable(Guid variableGUID);
        PageQuestion GetPageQuestionByPageGuidAndQuesOrder(Guid pageGuid, int orderNum);
        PageQuestion Get(Guid pageQuestion);
        void AddPageQuestion(PageQuestion pageQuestion2Add);
        void DeletePageQuestion(Guid pageQuestion2Delete);
        void DeletePageQuestions(EntityCollection<PageQuestion> pageQuestions);
        void UpdatePageQuestion(PageQuestion pageQuestion2Update);
        IQueryable<PageQuestion> GetAllPageQuestinByVariable(Guid variableGUID);
    }
}
