using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using System.Data.Objects.DataClasses;

namespace ChangeTech.IDataRepository
{
    public interface IUserPageVariableRepository
    {
        UserPageVariable GetUserPageVariableByUserAndVariable(Guid userGuid, Guid variableGuid);
        void UpdateUserPageVariable(UserPageVariable userpagevariable);
        void AddUserPageVariable(UserPageVariable userpagevariable);
        void Delete(EntityCollection<UserPageVariable> userpagevariables);
        void DeleteByQuestionAnswerGuid(Guid qaGuid);
    }
}
