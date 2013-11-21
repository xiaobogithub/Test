using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IQuestionRepository
    {
        IQueryable<Question> GetQuestions();
        Question GetQuestion(Guid questionGUID);
        void UpdateQuestion(Question question2Update);
        void DeleteQuestion(Guid question2Delete);
        void AddQuestion(Question question2Add);
    }
}
