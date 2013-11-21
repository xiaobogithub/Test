using System;
using System.Linq;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface ISessionContentRepository
    {
        IQueryable<SessionContent> GetSessionContentBySessionGuid(Guid sessionGuid);
        IQueryable<SessionContent> GetSessionContentByPageSeqGuid(Guid PageSeqGuid);
        IQueryable<SessionContent> GetSessionContentByRoomGuid(Guid RoomGuid);
        //IQueryable<SessionContent> GetSessionContentByRoomGuidInterventGuid(Guid RoomGuid, Guid InterventGuid);
        void DeleteSessionContent(System.Data.Objects.DataClasses.EntityCollection<SessionContent> sessionContent);
        void DeleteSessionContent(Guid guid);
        SessionContent GetSessionContentBySessionContentGuid(Guid sessionContentGuid);
        SessionContent GetSessionContentBySessionGuidPageSequenceGuid(Guid sessionGuid, Guid PageSequenceGuid);
        SessionContent GetSessionContentBySessionGuidAndOrderNO(Guid sessionGuid, int orderNO);
        void UpdateSessionContent(SessionContent sessionContent);
        SessionContent GetLastSessionContent(Guid sessionGuid);
        void Insert(SessionContent sessionContent);
        IQueryable<SessionContent> GetSessionContentOfProgram(Guid programGUID);
        int GetSessionContentCoutButPageSequenceGUID(Guid pageSequenceGUID);
    }
}
