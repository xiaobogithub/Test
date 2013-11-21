using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.IDataRepository;
using ChangeTech.Entities;

namespace ChangeTech.SQLServerRepository
{
    public class ProgramStatusRepository : RepositoryBase,IProgramStatusRepository
    {
        #region IProgramStatusRepository Members

        public IQueryable<ChangeTech.Entities.ProgramStatus> GetAllProgramStatus()
        {
            return GetEntities<ProgramStatus>().OrderBy(p => p.Name);
        }

        public ProgramStatus GetProgramStatusByStatusGuid(Guid statusGuid)
        {
            return GetEntities<ProgramStatus>(p => p.ProgramStatusGUID == statusGuid && 
                (p.IsDeleted.HasValue?p.IsDeleted.Value == false: true)).FirstOrDefault();
        }

        public void UpdateProgramStatus(ChangeTech.Entities.ProgramStatus status)
        {
            UpdateEntity(status);
        }

        public void DeleteProgramStatus(Guid statusGuid)
        {
            DeleteEntity<ProgramStatus>(p => p.ProgramStatusGUID == statusGuid, new Guid());
        }

        public void InstertProgramStatus(ChangeTech.Entities.ProgramStatus status)
        {
            InsertEntity(status); 
        }

        //public ProgramStatus Get(string name)
        //{
        //    return GetEntities<ProgramStatus>(p=>p.Name.Equals(name)).FirstOrDefault();
        //}
        #endregion
    }
}
