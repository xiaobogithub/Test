using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ChangeTech.IDataRepository;
using ChangeTech.Entities;

namespace ChangeTech.SQLServerRepository
{
    public class RelapseRepository : RepositoryBase, IRelapseRepository
    {
        #region IRelapseRepository Members

        public void Delete(Guid guid)
        {
            DeleteEntity<Relapse>(r => r.RelapseGUID == guid, Guid.Empty);
        }

        public void Add(ChangeTech.Entities.Relapse relapse)
        {
            InsertEntity(relapse);
        }

        public List<ChangeTech.Entities.Relapse> GetRelapseList(Guid programGuid)
        {
            return GetEntities<Relapse>(r => r.Program.ProgramGUID == programGuid).ToList();
        }

        public int GetRelapseCountByPageSequenceGUID(Guid sequenceGuid)
        {
            return GetEntities<Relapse>(r => r.PageSequence.PageSequenceGUID == sequenceGuid).Count();
        }

        public Relapse GetRelapseByProgramGUIDAndPageSequenceGUID(Guid programGUID, Guid pageSequnceGUID)
        {
            return GetEntities<Relapse>(r => r.Program.ProgramGUID == programGUID && r.PageSequence.PageSequenceGUID == pageSequnceGUID).FirstOrDefault();
        }

        public Relapse GetRelapseByPageSequenceGUID(Guid pageSequnceGUID)
        {
            return GetEntities<Relapse>(r => r.PageSequence.PageSequenceGUID == pageSequnceGUID).FirstOrDefault();
        }

        public Relapse Get(Guid RelapseGUID)
        {
            return GetEntities<Relapse>(r => r.RelapseGUID == RelapseGUID).FirstOrDefault();
        }

        public void Update(Relapse relapse)
        {
            UpdateEntity(relapse);
        }

        public void DeleteRelapseOfProgram(Guid programGuid)
        {
            DeleteEntities<Relapse>(r => r.Program.ProgramGUID == programGuid, Guid.Empty);
        }
        #endregion
    }
}
