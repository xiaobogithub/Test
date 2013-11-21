using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;
using ChangeTech.Entities;

namespace ChangeTech.Contracts
{
    public interface IRelapseService
    {
        void AddRelapseForProgram(Guid programGuid, Guid sequenceGuid, Guid programRoomGuid);
        void DeleteRelapse(Guid relapseGUID);
        List<RelapseModel> GetRelapseModelList(Guid programGUID);
        List<RelapseModel> GetRelapseModelListBySessionGUID(Guid sessionGuid);
        RelapseModel GetRelapseModel(Guid programGUID, Guid pageSequnceGUID);
        Relapse CloneRelapse(Relapse rel, CloneProgramParameterModel cloneParameterModel);
        Relapse SetDefaultGuidForRelapse(Relapse needSetDefaultEntity, CloneProgramParameterModel cloneParameterModel);
    }
}
