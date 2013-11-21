using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IExpressionRepository
    {
        IQueryable<Expression> GetExpressionOfGroup(Guid expressionGroupGUID);
        IQueryable<Expression> GetExpressionOfGroup(string expressionGroupName);
        IQueryable<Expression> GetExpressionOfProgram(Guid programGuid);
        Expression GetExpression(Guid expressionGUID);
        void AddExpression(Expression expression);
        void UpdateExpression(Expression expression);
        void DeleteExpression(Guid expressionGUID);
        List<string> GetExpressionForSession(Guid sessionGuid);
        List<string> GetExpressionPageVariableUsedInExpressionByProgram(Guid programGUID);
        List<string> GetPagesequenceForSessionWithExpression(Guid sessionGuid);
        List<string> GetExpressionForRelapseFromSession(Guid sessionGuid); 
        List<string> GetRelapsePagesequenceFromProgram(Guid programGuid);
    }
}
