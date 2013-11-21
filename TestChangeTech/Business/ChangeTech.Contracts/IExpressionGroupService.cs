using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ChangeTech.Models;

namespace ChangeTech.Contracts
{
    public interface IExpressionGroupService
    {
        List<ExpressionGroupModel> GetExpressionGroupOfProgram(Guid sessionGUID);
        void SaveEditExpressionGroup(EditExpressionGroupModel editExpressionGroup);
    }
}
