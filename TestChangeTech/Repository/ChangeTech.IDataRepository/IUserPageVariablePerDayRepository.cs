using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using System.Data.Objects.DataClasses;

namespace ChangeTech.IDataRepository
{
    public interface IUserPageVariablePerDayRepository
    {
        UserPageVariablePerDay GetUserPageVariable(Guid userGUID, Guid pageVariableGUID, Guid sessionGuid);
        void InsertUserPageVariable(UserPageVariablePerDay userPageVariablePerDayEntity);
        void UpdateUserPageVariable(UserPageVariablePerDay userPageVariablePerDayEntity);
        void Delete(EntityCollection<UserPageVariablePerDay> userpagevariables);
        void DeleteByQuestionAnswerGuid(Guid qaGuid);
    }
}
