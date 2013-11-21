using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IProgramUserSessionRepository
    {
        void Update(ProgramUserSession programUserSession);
        void Insert(ProgramUserSession programUserSession);
        void Delete(Guid programUserSessionGuid);
        void DeleteEntities(Guid programUserGuid);
        IQueryable<ProgramUserSession> GetProgramUserSessionListByProgramUserGuid(Guid programUserGuid);
        ProgramUserSession GetProgramUserSessionByProgramUserGuidAndSessionGuid(Guid programUserGuid, Guid sessionGuid);
    }
}
