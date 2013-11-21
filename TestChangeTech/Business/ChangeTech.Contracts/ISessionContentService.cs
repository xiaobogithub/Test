using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;
using ChangeTech.Entities;

namespace ChangeTech.Contracts
{
    public interface ISessionContentService
    {
        void UpOrderNO(Guid sessionContentGuid);
        void DownOrderNO(Guid sessionContentGuid);
        SessionContent GetSessionContentByPageSeqGuidAndSessionGuid(Guid pageSeqGuid,Guid sessionGuid);
        void AddNewSessionContent(Guid sessionGuid, Guid pageSeqGuid, Guid RoomGuid, int pageSeqOrder);
        void AddNewSessionContentBaseOnExistPageSequence(Guid sessionGuid, Guid pageSeqGuid);
        void AddNewSessionContent(Guid sessionGuid, PageSequenceModel pageSeqModel);
        void DeleteSessionContent(Guid sessionContentGuid);
        void MakeCopySessionContent(Guid sessionContentGuid);
        void CopySessionContentWithCopyPageSequence(Guid sessionContentGuid);
        int HowManySessionContentCiteThePageSequence(Guid pageSeqGuid);
        SessionContent CloneSessionContent(SessionContent sessionContent, List<KeyValuePair<string, string>> cloneRelapseGUIDList, Dictionary<Guid, Guid> pageDictionary, CloneProgramParameterModel cloneParameterModel);
        SessionContent SetDefaultGuidForSessionContent(SessionContent needSetDefaultEntity, CloneProgramParameterModel cloneParameterModel);
    }
}
