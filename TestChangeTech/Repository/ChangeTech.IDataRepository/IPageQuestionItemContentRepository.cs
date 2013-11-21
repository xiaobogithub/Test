using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IPageQuestionItemContentRepository
    {
        //PageQuestionItemContent GetPageQuestionItemContent(Guid pageQuestionItemGuid, Guid languageGuid);
        PageQuestionItemContent GetPageQuestionItemContentByPageQuestionItemGuid(Guid pageQuestionItemGuid);
        IQueryable<PageQuestionItemContent> GetPageQuestionItemContent(Guid pageQuestionItemGuid);
        void InsertPageQuestionItemContent(PageQuestionItemContent pageQuestionItemContent);
        void InsertPageQuestionItemContent(List<PageQuestionItemContent> list);
        void UpdatePageQuestionItemContent(PageQuestionItemContent pageQuestionItemContent);
        void DeletePageQuestionItemContent(Guid pageQuestionItemGuid);
        //void DeletePageQuestionItemContents(EntityCollection<PageQuestionItemContent> pageQuestionItemContents);
        void DeletePageQuestionItemContentByQuestionGUID(Guid pageQuestionGUID);
        IQueryable<PageQuestionItemContent> GetPageQuestionItemContentByProgram(Guid programGUID, int startDay, int endDay);
    }
}
