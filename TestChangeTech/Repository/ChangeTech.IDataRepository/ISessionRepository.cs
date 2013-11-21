using System;
using System.Linq;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface ISessionRepository
    {
        IQueryable<Session> GetSessionByProgramGuid(Guid programGuid);
        void DeleteSession(System.Data.Objects.DataClasses.EntityCollection<Session> session);
        void DeleteSession(Guid sessionGuid);
        void InsertSession(Session session);
        void UpdateSession(Session session);
        Session GetSessionBySessionGuid(Guid sessionGuid);
        Session GetLastSessionOfProgram(Guid programGuid);
        Session GetSessionByProgramGuidAndDay(Guid programGuid, int day);
    }
}
