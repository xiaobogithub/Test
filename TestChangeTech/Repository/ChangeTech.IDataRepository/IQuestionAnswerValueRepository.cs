using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IQuestionAnswerValueRepository
    {
        void Delete(System.Data.Objects.DataClasses.EntityCollection<QuestionAnswerValue> answerValues);
        void DeleteByQuestionGuid(Guid QuestionGuid);
        void Update(QuestionAnswerValue qustionAnswerValue);
    }
}
