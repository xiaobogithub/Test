using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;
using System.Data.Objects.DataClasses;

namespace ChangeTech.SQLServerRepository
{
    public class PageQuestionContentRepository : RepositoryBase, IPageQuestionContentRepository
    {
        #region IPageQuestionContentRepository Members

        //public PageQuestionContent GetPageQuestionContent(Guid pageQuestionGuid, Guid languageGuid)
        //{
        //    return GetEntities<PageQuestionContent>(q=>q.PageQuestionGUID == pageQuestionGuid && q.LanguageGUID == languageGuid 
        //        && (q.IsDeleted.HasValue? q.IsDeleted.Value == false: true)).FirstOrDefault();
        //}

        public PageQuestionContent GetPageQuestionContentByPageQuestionGuid(Guid pageQuestionGuid)
        {
            return GetEntities<PageQuestionContent>(q => q.PageQuestionGUID == pageQuestionGuid).FirstOrDefault();
        }

        public IQueryable<PageQuestionContent> GetPageQuestionContent(Guid pageQuestionGuid)
        {
            return GetEntities<PageQuestionContent>(q => q.PageQuestionGUID == pageQuestionGuid &&
                (q.IsDeleted.HasValue? q.IsDeleted.Value == false: true));
        }

        public IQueryable<PageQuestionContent> GetPageQuestionContentByProgram(Guid programGuid, int startDay, int endDay)
        {
            return GetEntities<PageQuestionContent>(q =>
                q.PageQuestion.Page.PageSequence.SessionContent.Where(s => 
                    s.Session.Program.ProgramGUID == programGuid &&
                    s.Session.Day.Value >= startDay && s.Session.Day <= endDay &&
                    !(s.IsDeleted.HasValue && s.IsDeleted.Value) &&
                !(s.Session.IsDeleted.HasValue && s.Session.IsDeleted.Value) && 
                !(s.Session.Program.IsDeleted.HasValue && s.Session.Program.IsDeleted.Value)).Count() > 0 &&

                !(q.IsDeleted.HasValue && q.IsDeleted.Value) &&
                !(q.PageQuestion.Page.IsDeleted.HasValue && q.PageQuestion.Page.IsDeleted.Value) &&
                !(q.PageQuestion.Page.PageSequence.IsDeleted.HasValue && q.PageQuestion.Page.PageSequence.IsDeleted.Value)).OrderBy(p=>p.PageQuestion.Order);
        }

        public void InsertPageQuestionContent(PageQuestionContent pageQuestionContent)
        {
            InsertEntity(pageQuestionContent);
        }

        public void InsertPageQuestionContent(List<PageQuestionContent> list)
        {
            InsertEntities<PageQuestionContent>(list, Guid.NewGuid());
        }

        public void DeletePageQuestionContent(Guid pageQuestionGuid)
        {
            DeleteEntities<PageQuestionContent>(q => q.PageQuestionGUID == pageQuestionGuid, Guid.Empty);
        }

        //public void DeletePageQuestionContent(Guid pageQuestionGuid, Guid languageGuid)
        //{
        //    DeleteEntity<PageQuestionContent>(q=>q.PageQuestionGUID == pageQuestionGuid && q.LanguageGUID == languageGuid, Guid.Empty);
        //}

        public void UpdatePageQuestionContent(PageQuestionContent pageQuestionContent)
        {
            UpdateEntity(pageQuestionContent);
        }

        //public void DeletePageQuestionContents(EntityCollection<PageQuestionContent> pageQuestionContents)
        //{
        //    DeleteEntities<PageQuestionContent>(pageQuestionContents, Guid.Empty);
        //}
        #endregion
    }
}
