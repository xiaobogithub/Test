using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class QuestionAnswerRepository: RepositoryBase, IQuestionAnswerRepository
    {
        #region IQuestionAnswerRepository Members

        public IQueryable<QuestionAnswer> GetQuestionAnswer()
        {
            return GetEntities<QuestionAnswer>();
        }

        public IQueryable<QuestionAnswer> GetQuestionAnswersByQuestion(Guid pageQuestionGUID)
        {
            return GetEntities<QuestionAnswer>(q => q.PageQuestion.PageQuestionGUID == pageQuestionGUID);
        }

        public void AddQuestionAnswer(QuestionAnswer questionAnswer2Add)
        {
            InsertEntity(questionAnswer2Add);
        }

        public void UpdateQuestionAnswer(QuestionAnswer questionAnswer2Update)
        {
            UpdateEntity(questionAnswer2Update);
        }

        public QuestionAnswer GetQuestionAnswerByUserQuePmSCNotForRelapse(Guid userGuid, Guid pageQuestionGUID, Guid programGuid, Guid sessionContentGuid)
        {
            return GetEntities<QuestionAnswer>(q => q.User.UserGUID == userGuid &&
                q.PageQuestion.PageQuestionGUID == pageQuestionGUID &&
                q.Program.ProgramGUID == programGuid &&
                q.SessionContent.SessionContentGUID == sessionContentGuid &&
                q.RelapsePageSequenceGUID == null &&
                q.RelapsePageGUID == null).FirstOrDefault();
        }

        public QuestionAnswer GetQuestionAnswerByUserQuePmSCForRelapse(Guid userGuid, Guid pageQuestionGUID, Guid programGuid, Guid sessionContentGuid, Guid RelapsePageSequenceGUID, Guid RelapsePageGuid)
        {
            return GetEntities<QuestionAnswer>(q => q.User.UserGUID == userGuid &&
                q.PageQuestion.PageQuestionGUID == pageQuestionGUID &&
                q.Program.ProgramGUID == programGuid &&
                q.SessionContent.SessionContentGUID == sessionContentGuid &&
                q.RelapsePageSequenceGUID == RelapsePageSequenceGUID &&
                q.RelapsePageGUID == RelapsePageGuid).FirstOrDefault();
        }

        public QuestionAnswer GetQuestionAnswerByGuid(Guid answerGuid)
        {
            return GetEntities<QuestionAnswer>(q => q.QuestionAnswerGUID == answerGuid).FirstOrDefault();
        }

        public QuestionAnswer GetQuestionAnswerByUserPgPmSCNotForRelapse(Guid userGuid, Guid pageGuid, Guid programGuid, Guid sessionContentGuid)
        {
            return GetEntities<QuestionAnswer>(q => q.User.UserGUID == userGuid &&
                q.Page.PageGUID == pageGuid &&
                q.Program.ProgramGUID == programGuid &&
                q.SessionContent.SessionContentGUID == sessionContentGuid &&
                q.RelapsePageSequenceGUID == null &&
                q.RelapsePageGUID == null).FirstOrDefault();
        }

        public QuestionAnswer GetQuestionAnswerByUserPgPmSCForRelapse(Guid userGuid, Guid pageGuid, Guid programGuid, Guid sessionContentGuid, Guid RelapsePageSequenceGUID, Guid RelapsePageGuid)
        {
            return GetEntities<QuestionAnswer>(q => q.User.UserGUID == userGuid &&
                q.Page.PageGUID == pageGuid &&
                q.Program.ProgramGUID == programGuid &&
                q.SessionContent.SessionContentGUID == sessionContentGuid &&
                q.RelapsePageSequenceGUID == RelapsePageSequenceGUID &&
                q.RelapsePageGUID == RelapsePageGuid).FirstOrDefault();
        }

        public void DeleteByQuestion(Guid QuestionGuid)
        {
            DeleteEntities<QuestionAnswer>(qa => qa.PageQuestion.PageQuestionGUID == QuestionGuid, Guid.Empty);
        }

        public void Delete(Guid qaGuid)
        {
            DeleteEntity<QuestionAnswer>(qa => qa.QuestionAnswerGUID == qaGuid, Guid.Empty);
        }

        #endregion
    }
}
