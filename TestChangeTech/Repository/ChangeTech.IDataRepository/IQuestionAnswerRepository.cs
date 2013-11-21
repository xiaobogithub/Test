using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;


namespace ChangeTech.IDataRepository
{
    public interface IQuestionAnswerRepository
    {
        IQueryable<QuestionAnswer> GetQuestionAnswer();
        IQueryable<QuestionAnswer> GetQuestionAnswersByQuestion(Guid pageQuestionGUID);
        void AddQuestionAnswer(QuestionAnswer questionAnswer2Add);
        void UpdateQuestionAnswer(QuestionAnswer questionAnswer2Update);
        QuestionAnswer GetQuestionAnswerByUserQuePmSCNotForRelapse(Guid userGuid, Guid pageQuestionGUID, Guid programGuid, Guid sessionContentGuid);
        QuestionAnswer GetQuestionAnswerByUserQuePmSCForRelapse(Guid userGuid, Guid pageQuestionGUID, Guid programGuid, Guid sessionContentGuid, Guid RelapseGuid, Guid RelapsePageGuid);
        QuestionAnswer GetQuestionAnswerByGuid(Guid answerGuid);
        QuestionAnswer GetQuestionAnswerByUserPgPmSCNotForRelapse(Guid userGuid, Guid pageGuid, Guid programGuid, Guid sessionContentGuid);
        QuestionAnswer GetQuestionAnswerByUserPgPmSCForRelapse(Guid userGuid, Guid pageGuid, Guid programGuid, Guid sessionContentGuid, Guid RelapseGuid, Guid RelapsePageGuid);
        void DeleteByQuestion(Guid QuestionGuid);
        void Delete(Guid qaGuid);
    }
}