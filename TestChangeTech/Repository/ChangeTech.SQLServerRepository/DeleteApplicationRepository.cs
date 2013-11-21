using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class DeleteApplicationRepository : RepositoryBase, IDeleteApplicationRepository
    {
        #region IDeleteApplicationRepository Members

        public IQueryable<DeleteApplication> GetDeleteApplicationByAssigneeAndStatus(Guid assigneeGuid, int status)
        {
            return GetEntities<DeleteApplication>(a => a.User1.UserGUID == assigneeGuid && a.Status == status);
        }

        public IQueryable<DeleteApplication> GetDeleteApplicationByApplicant(Guid applicantGuid)
        {
            return GetEntities<DeleteApplication>(a => a.User.UserGUID == applicantGuid).OrderBy(a => a.Status);
        }

        public DeleteApplication GetDeleteApplicationByGuid(Guid applicationGuid)
        {
            return GetEntities<DeleteApplication>(a => a.DeleteApplicationGUID == applicationGuid).FirstOrDefault();
        }

        public DeleteApplication GetDeleteApplicationByProgramGuidAndStatus(Guid programGuid, int status)
        {
            return GetEntities<DeleteApplication>(a => a.Program.ProgramGUID == programGuid && a.Status == status).FirstOrDefault();
        }

        public void UpdateDeleteApplication(DeleteApplication application)
        {
            UpdateEntity(application);
        }

        public void InsertDeleteApplication(DeleteApplication application)
        {
            InsertEntity(application);
        }

        #endregion
    }
}
