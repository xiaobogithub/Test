using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class ProgramRoomRepository : RepositoryBase, IProgramRoomRepository
    {
        public List<ProgramRoom> GetRoomByProgram(Guid ProgramGuid)
        {
            return GetEntities<ProgramRoom>(l => l.Program.ProgramGUID == ProgramGuid ).ToList<ProgramRoom>();
        }

        public List<ProgramRoom> GetItems(string strName)
        {
            return GetEntities<ProgramRoom>(l => l.Name == strName).ToList<ProgramRoom>();
        }

        public bool IsExist(string name, Guid programguid)
        {
            bool flug = false;
            if (GetEntities<ProgramRoom>(l => l.Name == name && l.Program.ProgramGUID == programguid).Count() > 0)
            {
                flug = true;
            }

            return flug;
        }

        public IQueryable<ProgramRoom> GetRoomsByProgram(Guid programguid)
        {
            return GetEntities<ProgramRoom>(p => p.Program.ProgramGUID == programguid);
        }

        public ProgramRoom GetRoom(Guid RoomGuid)
        {
            return GetEntities<ProgramRoom>(l => l.ProgramRoomGUID == RoomGuid).FirstOrDefault<ProgramRoom>();
        }

        public void Insert(ProgramRoom room)
        {
            InsertEntity(room);
        }

        public void Update(ProgramRoom room)
        {
            UpdateEntity(room);
        }

        public void Delete(Guid RoomGuid)
        {
            DeleteEntity<ProgramRoom>(l => l.ProgramRoomGUID == RoomGuid, Guid.Empty);
        }

        public ProgramRoom GetRoomByProgramAndParent(Guid programGuid, Guid parentGuid)
        {
            return GetEntities<ProgramRoom>(pr => pr.Program.ProgramGUID == programGuid && pr.ParentProgramRoomGUID == parentGuid).FirstOrDefault();
        }

        public void DeleteRoomOfProgram(Guid programGuid)
        {
            DeleteEntities<ProgramRoom>(pr=>pr.Program.ProgramGUID == programGuid, Guid.Empty);
        }
    }
}
