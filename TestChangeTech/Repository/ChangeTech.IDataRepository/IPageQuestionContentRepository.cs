using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IPageQuestionContentRepository
    {
        //PageQuestionContent GetPageQuestionContent(Guid pageQuestionGuid, Guid languageGuid);
        PageQuestionContent GetPageQuestionContentByPageQuestionGuid(Guid pageQuestionGuid);
        IQueryable<PageQuestionContent> GetPageQuestionContent(Guid pageQuestionGuid);
        IQueryable<PageQuestionContent> GetPageQuestionContentByProgram(Guid programGuid, int startDay, int endDay);
        void InsertPageQuestionContent(PageQuestionContent pageQuestionContent);
        void InsertPageQuestionContent(List<PageQuestionContent> list);
        void DeletePageQuestionContent(Guid pageQuestionGuid);
        //void DeletePageQuestionContent(Guid pageQuestionGuid, Guid languageGuid);
        //void DeletePageQuestionContents(EntityCollection<PageQuestionContent> pageQuestionContents);
        void UpdatePageQuestionContent(PageQuestionContent pageQuestionContent);
    }
}
