using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.IDataRepository;
using ChangeTech.Entities;
using System.Data.Objects.DataClasses;

namespace ChangeTech.SQLServerRepository
{
    public class UserPageVariablePerDayRepository : RepositoryBase, IUserPageVariablePerDayRepository
    {
        public UserPageVariablePerDay GetUserPageVariable(Guid userGUID, Guid pageVariableGUID, Guid sessionGuid)
        {
            return GetEntities<UserPageVariablePerDay>(p => p.User.UserGUID == userGUID && p.PageVariable.PageVariableGUID == pageVariableGUID && p.SessionGUID == sessionGuid).FirstOrDefault();
        }

        public void InsertUserPageVariable(ChangeTech.Entities.UserPageVariablePerDay userPageVariablePerDayEntity)
        {
            InsertEntity(userPageVariablePerDayEntity);
        }

        public void UpdateUserPageVariable(ChangeTech.Entities.UserPageVariablePerDay userPageVariablePerDayEntity)
        {
            UpdateEntity(userPageVariablePerDayEntity);
        }

        public void Delete(EntityCollection<UserPageVariablePerDay> userpagevariables)
        {
            DeleteEntities<UserPageVariablePerDay>(userpagevariables, Guid.Empty);
        }

        public void DeleteByQuestionAnswerGuid(Guid qaGuid)
        {
            DeleteEntities<UserPageVariablePerDay>(u => u.QuestionAnswer.QuestionAnswerGUID == qaGuid, Guid.Empty);
        }
    }
}
