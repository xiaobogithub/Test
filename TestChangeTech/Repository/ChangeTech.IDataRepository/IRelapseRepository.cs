using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IRelapseRepository
    {
        void Delete(Guid guid);
        void Add(Relapse relapse);
        List<Relapse> GetRelapseList(Guid programGuid);
        int GetRelapseCountByPageSequenceGUID(Guid sequenceGuid);
        Relapse GetRelapseByProgramGUIDAndPageSequenceGUID(Guid programGUID, Guid pageSequnceGUID);
        Relapse GetRelapseByPageSequenceGUID(Guid pageSequnceGUID);
        Relapse Get(Guid RelapseGUID);
        void Update(Relapse relapse);
        void DeleteRelapseOfProgram(Guid programGuid);
    }
}
