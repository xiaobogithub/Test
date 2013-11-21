using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.IDataRepository;
using ChangeTech.Entities;
using System.Data.Objects.DataClasses;

namespace ChangeTech.SQLServerRepository
{
    public class UserPageVariableRepository : RepositoryBase, IUserPageVariableRepository
    {
        public UserPageVariable GetUserPageVariableByUserAndVariable(Guid userGuid, Guid variableGuid)
        {
            return GetEntities<UserPageVariable>(u => u.User.UserGUID == userGuid && 
                u.PageVariable.PageVariableGUID == variableGuid).FirstOrDefault();
        }

        public void UpdateUserPageVariable(UserPageVariable userpagevariable)
        {
            UpdateEntity(userpagevariable);
        }

        public void AddUserPageVariable(UserPageVariable userpagevariable)
        {
            InsertEntity(userpagevariable);
        }

        public void Delete(EntityCollection<UserPageVariable> userpagevariables)
        {
            DeleteEntities<UserPageVariable>(userpagevariables,Guid.Empty);
        }

        public void DeleteByQuestionAnswerGuid(Guid qaGuid)
        {
            DeleteEntities<UserPageVariable>(u => u.QuestionAnswer.QuestionAnswerGUID == qaGuid, Guid.Empty);
        }
    }
}
