using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class QuestionRepository : RepositoryBase, IQuestionRepository
    {
        #region IQuestionRepository Members

        public IQueryable<Question> GetQuestions()
        {
            return GetEntities<Question>();
        }

        public Question GetQuestion(Guid questionGUID)
        {
            return GetEntities<Question>(q => q.QuestionGUID == questionGUID).FirstOrDefault();
        }

        public void UpdateQuestion(Question question2Update)
        {
            UpdateEntity(question2Update);
        }

        public void DeleteQuestion(Guid question2Delete)
        {
            DeleteEntity<Question>(q => q.QuestionGUID == question2Delete, Guid.Empty);
        }

        public void AddQuestion(Question question2Add)
        {
            InsertEntity(question2Add);
        }

        #endregion
    }
}
