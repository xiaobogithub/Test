using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class QuestionAnswerValueRepository : RepositoryBase, IQuestionAnswerValueRepository
    {
        public void Delete(System.Data.Objects.DataClasses.EntityCollection<QuestionAnswerValue> answerValues)
        {
            DeleteEntities<QuestionAnswerValue>(answerValues, Guid.Empty);
        }

        public void DeleteByQuestionGuid(Guid questionGuid)
        {
            DeleteEntities<QuestionAnswerValue>(qa => qa.QuestionAnswer.PageQuestion.PageQuestionGUID == questionGuid, Guid.Empty);
        }

        public void Update(QuestionAnswerValue qustionAnswerValue)
        {
            UpdateEntity(qustionAnswerValue);
        }
    }
}
