using System;
using System.Linq;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class SessionRepository: RepositoryBase,ISessionRepository
    {
        #region ISessionRepository Members      
        public IQueryable<Session> GetSessionByProgramGuid(Guid programGuid)
        {
            return GetEntities<Session>(s => s.Program.ProgramGUID == programGuid &&
                !(s.IsDeleted.HasValue && s.IsDeleted.Value)).OrderBy(s => s.Day);
        }

        public void DeleteSession(System.Data.Objects.DataClasses.EntityCollection<Session> session)
        {
            DeleteEntities<Session>(session, new Guid());
        }

        public void DeleteSession(Guid sessionGuid)
        {
            DeleteEntity<Session>(s => s.SessionGUID == sessionGuid, new Guid());
        }

        public void InsertSession(Session session)
        {
            InsertEntity(session);
        }

        public void UpdateSession(Session session) 
        {
            UpdateEntity(session);
        }

        public Session GetSessionBySessionGuid(Guid sessionGuid)
        {
            return GetEntities<Session>(s => s.SessionGUID == sessionGuid &&
                (s.IsDeleted.HasValue ? s.IsDeleted.Value == false : true)).FirstOrDefault();
        }

        public Session GetLastSessionOfProgram(Guid programGuid)
        {
            return GetEntities<Session>(s => s.Program.ProgramGUID == programGuid &&
                (s.IsDeleted.HasValue ? s.IsDeleted.Value == false : true)).OrderByDescending(s => s.Day).FirstOrDefault();
        }

        public Session GetSessionByProgramGuidAndDay(Guid programGuid, int day)
        {
            return GetEntities<Session>(s => s.Program.ProgramGUID == programGuid && s.Day == day &&
                (s.IsDeleted.HasValue ? s.IsDeleted.Value == false : true)).FirstOrDefault();
        }
        #endregion
    }
}
