using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IExpressionGroupRepository
    {
        IQueryable<ExpressionGroup> GetExpressionGroupOfProgram(Guid programGUID);
        IQueryable<ExpressionGroup> GetExpressionGroupOfProgram(string programName);
        ExpressionGroup GetExpressionGroup(Guid expressionGroupGUID);
        void AddExpressionGroup(ExpressionGroup expressionGroup);
        void UpdateExpressionGroup(ExpressionGroup expressionGroup);
        void DeleteExpressionGroup(Guid expressionGroupGUID);
    }
}
