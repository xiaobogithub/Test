using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ChangeTech.Entities;
using ChangeTech.IDataRepository;
namespace ChangeTech.SQLServerRepository
{
    public class ExpressionGroupRepository: RepositoryBase, IExpressionGroupRepository
    {
        public IQueryable<ExpressionGroup> GetExpressionGroupOfProgram(Guid programGUID)
        {
            return GetEntities<ExpressionGroup>(e => e.Program.ProgramGUID == programGUID);
        }

        public IQueryable<ExpressionGroup> GetExpressionGroupOfProgram(string programName)
        {
            return GetEntities<ExpressionGroup>(e => e.Program.Name.Equals(programName));
        }

        public ExpressionGroup GetExpressionGroup(Guid expressionGroupGUID)
        {
            return GetEntities<ExpressionGroup>(e => e.ExpressionGroupGUID == expressionGroupGUID).FirstOrDefault();
        }

        public void AddExpressionGroup(ExpressionGroup expressionGroup)
        {
            InsertEntity(expressionGroup);
        }

        public void UpdateExpressionGroup(ExpressionGroup expressionGroup)
        {
            UpdateEntity(expressionGroup);
        }

        public void DeleteExpressionGroup(Guid expressionGroupGUID)
        {
            DeleteEntity<ExpressionGroup>(e=>e.ExpressionGroupGUID == expressionGroupGUID,Guid.Empty);
        }
    }
}
