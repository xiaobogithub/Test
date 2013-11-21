using System;
using System.Collections.Generic;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;
using System.Linq;

namespace ChangeTech.SQLServerRepository
{
    public class SessionContentRepository : RepositoryBase, ISessionContentRepository
    { 
        #region ISessionContentRepository Members 

        public IQueryable<SessionContent> GetSessionContentBySessionGuid(Guid sessionGuid)
        {
            return GetEntities<SessionContent>(s => s.Session.SessionGUID == sessionGuid &&
                (s.IsDeleted.HasValue ? s.IsDeleted.Value == false : true)).OrderBy(s => s.PageSequenceOrderNo);
        }

        public IQueryable<SessionContent> GetSessionContentByPageSeqGuid(Guid PageSeqGuid)
        {
            return GetEntities<SessionContent>(s => s.PageSequence.PageSequenceGUID == PageSeqGuid &&
                (s.IsDeleted.HasValue ? s.IsDeleted.Value == false : true)).OrderBy(s => s.PageSequenceOrderNo);
        }

        public IQueryable<SessionContent> GetSessionContentByRoomGuid(Guid RoomGuid)
        {
            return GetEntities<SessionContent>(s => s.ProgramRoom.ProgramRoomGUID == RoomGuid &&
                (s.IsDeleted.HasValue ? s.IsDeleted.Value == false : true)).OrderBy(s => s.PageSequenceOrderNo);
        }

        //public IQueryable<SessionContent> GetSessionContentByRoomGuidInterventGuid(Guid RoomGuid, Guid InterventGuid)
        //{
        //    return GetEntities<SessionContent>(s => s.PageSequence.Intervent.InterventGUID == InterventGuid && s.ProgramRoom.ProgramRoomGUID == RoomGuid);
        //}

        public void DeleteSessionContent(System.Data.Objects.DataClasses.EntityCollection<SessionContent> sessionContent)
        {
            DeleteEntities<SessionContent>(sessionContent, new Guid());
        }

        public void DeleteSessionContent(Guid guid)
        {
            DeleteEntity<SessionContent>(s => s.SessionContentGUID == guid, new Guid());
        }

        public SessionContent GetSessionContentBySessionContentGuid(Guid sessionContentGuid)
        {
            return GetEntities<SessionContent>(s => s.SessionContentGUID == sessionContentGuid && 
                (s.IsDeleted.HasValue ? s.IsDeleted.Value == false : true)).FirstOrDefault();
        }

        public SessionContent GetSessionContentBySessionGuidPageSequenceGuid(Guid sessionGuid, Guid PageSequenceGuid)
        {
            return GetEntities<SessionContent>(s => s.Session.SessionGUID == sessionGuid && s.PageSequence.PageSequenceGUID == PageSequenceGuid &&
                (s.IsDeleted.HasValue ? s.IsDeleted.Value == false : true)).FirstOrDefault();
        }

        public SessionContent GetSessionContentBySessionGuidAndOrderNO(Guid sessionGuid, int orderNO)
        {
            return GetEntities<SessionContent>(s => s.Session.SessionGUID == sessionGuid && s.PageSequenceOrderNo == orderNO &&
                (s.IsDeleted.HasValue ? s.IsDeleted.Value == false : true)).FirstOrDefault();
        }

        public void UpdateSessionContent(SessionContent sessionContent)
        {
            UpdateEntity(sessionContent);
        }

        public SessionContent GetLastSessionContent(Guid sessionGuid)
        {
            return GetEntities<SessionContent>(s => s.Session.SessionGUID == sessionGuid &&
                (s.IsDeleted.HasValue ? s.IsDeleted.Value == false : true)).OrderByDescending(s => s.PageSequenceOrderNo).FirstOrDefault();
        }

        public void Insert(SessionContent sessionContent)
        {
            InsertEntity(sessionContent);
        }

        public IQueryable<SessionContent> GetSessionContentOfProgram(Guid programGUID)
        {
            return GetEntities<SessionContent>().Where(s => s.Session.Program.ProgramGUID == programGUID && !(s.IsDeleted.HasValue && s.IsDeleted.Value)).OrderBy(s=>s.Session.Day);
        }

        public int GetSessionContentCoutButPageSequenceGUID(Guid pageSequenceGUID)
        {
            return GetEntities<SessionContent>(s => s.PageSequence.PageSequenceGUID == pageSequenceGUID && (s.IsDeleted.HasValue ? s.IsDeleted.Value == false : true)).Count();
        }

        #endregion
    }
}
