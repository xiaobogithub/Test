using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IDeleteApplicationRepository
    {
        IQueryable<DeleteApplication> GetDeleteApplicationByAssigneeAndStatus(Guid assigneeGuid, int status);
        IQueryable<DeleteApplication> GetDeleteApplicationByApplicant(Guid applicantGuid);
        DeleteApplication GetDeleteApplicationByGuid(Guid applicationGuid);
        DeleteApplication GetDeleteApplicationByProgramGuidAndStatus(Guid programGuid, int status);
        void UpdateDeleteApplication(DeleteApplication application);
        void InsertDeleteApplication(DeleteApplication application);
    }
}
