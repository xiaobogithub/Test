using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class ProgramUserSessionRepository : RepositoryBase, IProgramUserSessionRepository
    {
        #region 01.Update  + void Update(ProgramUserSession programUserSession)
        /// <summary>
        /// Update ProgramUserSessionEntity to DB
        /// </summary>
        /// <param name="programUserSession">ProgramUserSession Model</param>
        public void Update(ProgramUserSession programUserSession)
        {
            UpdateEntity(programUserSession);
        } 
        #endregion

        #region 02.Insert + Insert(ProgramUserSession programUserSession)
        /// <summary>
        /// Insert a ProgramUserSession
        /// </summary>
        /// <param name="programUserSession"></param>
        public void Insert(ProgramUserSession programUserSession)
        {
            //ProgramUserSession programUserSessionEntity= GetProgramUserSessionByProgramUserGuidAndSessionGuid(programUserSession.ProgramUserGUID, programUserSession.SessionGUID);
            //if (programUserSessionEntity == null)
            //{
                InsertEntity(programUserSession);
            //}
            //else
            //{
            //    programUserSession.ProgramUserSessionGUID = programUserSessionEntity.ProgramUserSessionGUID;
            //    UpdateEntity(programUserSession);
            //}
        } 
        #endregion

        #region 03.Delete + void Delete(Guid programUserSessionGuid)
        /// <summary>
        /// Delete a ProgramUserSession
        /// </summary>
        /// <param name="programUserSessionGuid"></param>
        public void Delete(Guid programUserSessionGuid)
        {
                DeleteEntity<ProgramUserSession>(u => u.ProgramUserSessionGUID == programUserSessionGuid, new Guid());
        }

        public void DeleteEntities(Guid programUserGuid)
        {
            DeleteEntities<ProgramUserSession>(pus => pus.ProgramUserGUID == programUserGuid, Guid.Empty);
        }
        #endregion


        #region 04.Get ProgramUserSessionList by ProgramUserGuid + IQueryable<ProgramUserSession> GetProgramUserSessionListByProgramUserGuid(Guid programUserGuid)
        /// <summary>
        /// Get ProgramUserSessionList by ProgramUserGuid
        /// </summary>
        /// <param name="programUserGuid"></param>
        /// <returns></returns>
        public IQueryable<ProgramUserSession> GetProgramUserSessionListByProgramUserGuid(Guid programUserGuid)
        {
            return GetEntities<ProgramUserSession>(u => u.ProgramUserGUID == programUserGuid && (!u.IsDeleted.HasValue || u.IsDeleted.Value == false));
        } 
        #endregion

        #region 05.Get a ProgramUserSession log from ProgramUserSession table + ProgramUserSession GetProgramUserSessionByProgramUserGuidAndSessionGuid(Guid programUserGuid, Guid sessionGuid)
        /// <summary>
        /// Get a ProgramUserSession log from ProgramUserSession table
        /// </summary>
        /// <param name="programUserGuid"></param>
        /// <param name="sessionGuid"></param>
        /// <returns></returns>
        public ProgramUserSession GetProgramUserSessionByProgramUserGuidAndSessionGuid(Guid programUserGuid, Guid sessionGuid)
        {
            return GetEntities<ProgramUserSession>(u => u.ProgramUserGUID == programUserGuid && u.SessionGUID == sessionGuid && (!u.IsDeleted.HasValue || u.IsDeleted.Value == false)).FirstOrDefault();
        } 
        #endregion
    }
}
