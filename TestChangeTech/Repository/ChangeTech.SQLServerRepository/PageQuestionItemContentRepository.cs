using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;
using System.Data.Objects.DataClasses;

namespace ChangeTech.SQLServerRepository
{
    public class PageQuestionItemContentRepository: RepositoryBase, IPageQuestionItemContentRepository
    {
        #region IPageQuestionItemContentRepository Members

        //public PageQuestionItemContent GetPageQuestionItemContent(Guid pageQuestionItemGuid, Guid languageGuid)
        //{
        //    return GetEntities<PageQuestionItemContent>(pq => pq.PageQuestionItemGUID == pageQuestionItemGuid && 
        //        pq.LanguageGUID == languageGuid && 
        //        (pq.IsDeleted.HasValue? pq.IsDeleted.Value == false: true)).FirstOrDefault();
        //}

        public IQueryable<PageQuestionItemContent> GetPageQuestionItemContentByProgram(Guid programGUID, int startDay, int endDay)
        {
            return GetEntities<PageQuestionItemContent>(p => 
                p.PageQuestionItem.PageQuestion.Page.PageSequence.SessionContent.Where(s=>
                    s.Session.Program.ProgramGUID == programGUID &&
                    (s.Session.Day.Value >= startDay && s.Session.Day.Value <= endDay) &&
                    !(s.IsDeleted.HasValue && s.IsDeleted.Value) &&
                    !(s.Session.IsDeleted.HasValue && s.Session.IsDeleted.Value) &&
                    !(s.Session.Program.IsDeleted.HasValue && s.Session.Program.IsDeleted.Value)).Count() > 0 &&
                !(p.IsDeleted.HasValue && p.IsDeleted.Value) &&
                !(p.PageQuestionItem.IsDeleted.HasValue && p.PageQuestionItem.IsDeleted.Value) &&
                !(p.PageQuestionItem.PageQuestion.IsDeleted.HasValue && p.PageQuestionItem.IsDeleted.Value) &&
                !(p.PageQuestionItem.PageQuestion.Page.IsDeleted.HasValue && p.PageQuestionItem.PageQuestion.Page.IsDeleted.Value) &&
                !(p.PageQuestionItem.PageQuestion.Page.PageSequence.IsDeleted.HasValue && p.PageQuestionItem.PageQuestion.Page.PageSequence.IsDeleted.Value));
        }

        public PageQuestionItemContent GetPageQuestionItemContentByPageQuestionItemGuid(Guid pageQuestionItemGuid)
        {
            return GetEntities<PageQuestionItemContent>(pq => pq.PageQuestionItemGUID == pageQuestionItemGuid).FirstOrDefault();
        }

        public IQueryable<PageQuestionItemContent> GetPageQuestionItemContent(Guid pageQuestionItemGuid)
        {
            return GetEntities<PageQuestionItemContent>(pq => pq.PageQuestionItemGUID == pageQuestionItemGuid && 
                (pq.IsDeleted.HasValue? pq.IsDeleted.Value == false: true));
        }

        public void InsertPageQuestionItemContent(PageQuestionItemContent pageQuestionItemContent)
        {
            InsertEntity(pageQuestionItemContent);
        }

        public void InsertPageQuestionItemContent(List<PageQuestionItemContent> list)
        {
            InsertEntities<PageQuestionItemContent>(list, new Guid());
        }

        public void UpdatePageQuestionItemContent(PageQuestionItemContent pageQuestionItemContent)
        {
            UpdateEntity(pageQuestionItemContent);
        }

        public void DeletePageQuestionItemContent(Guid pageQuestionItemGuid)
        {
            DeleteEntity<PageQuestionItemContent>(pq => pq.PageQuestionItemGUID == pageQuestionItemGuid, Guid.Empty);
        }

        //public void DeletePageQuestionItemContents(EntityCollection<PageQuestionItemContent> pageQuestionItemContents)
        //{
        //    DeleteEntities<PageQuestionItemContent>(pageQuestionItemContents, Guid.Empty);
        //}

        public void DeletePageQuestionItemContentByQuestionGUID(Guid pageQuestionGUID)
        {
            DeleteEntities<PageQuestionItemContent>(pq => pq.PageQuestionItem.PageQuestion.PageQuestionGUID == pageQuestionGUID, Guid.Empty);
        }
        #endregion
    }
}
