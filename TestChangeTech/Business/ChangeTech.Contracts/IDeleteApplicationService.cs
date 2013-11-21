using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;

namespace ChangeTech.Contracts
{
    public interface IDeleteApplicationService
    {
        List<DeleteApplicationModel> GetApplicationListByAssigneeAndStatus(Guid assigneeGuid, int status);
        List<DeleteApplicationModel> GetApplicationListByApplicant(Guid applicantGuid);
        void Approve(Guid applicationGuid);
        void Decline(Guid applicationGuid);
        void CreateApplication(DeleteApplicationModel applicationModel);
        bool IsProgramDeleteApplicationExist(Guid programgGuid);
    }
}
