using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;
using ChangeTech.Entities;

namespace ChangeTech.Contracts
{
    public interface IExpressionService
    {
        void AddExpression(ExpressionModel expressionModel);
        List<ExpressionModel> GetExpressionsOfGroup(Guid expressionGroupGuid);
        List<ExpressionModel> GetExpressionOfProgram(Guid sessionGuid);
        void SaveExpressions(EditExpressionModel editExpressionModel);
        List<string> GetExpressionForSession(Guid sessionGuid);       
        List<string> CheckExpressionForSession(Session sessionEntity, List<RelapsePageExpressionNodeModel> relapsePageExpressionNodeList);
        List<RelapsePageExpressionNodeModel> GetRelapsePages(List<string> relapsePageList);
    }
}
