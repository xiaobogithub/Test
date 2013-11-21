using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IProgramRoomRepository
    {
        List<ProgramRoom> GetRoomByProgram(Guid ProgramGuid);
        List<ProgramRoom> GetItems(string strName);
        ProgramRoom GetRoom(Guid RoomGuid);
        ProgramRoom GetRoomByProgramAndParent(Guid programGuid, Guid parentGuid);
        void Insert(ProgramRoom room);
        void Update(ProgramRoom room);
        void Delete(Guid RoomGuid);
        void DeleteRoomOfProgram(Guid programGuid);
        IQueryable<ProgramRoom> GetRoomsByProgram(Guid programguid);
        bool IsExist(string name, Guid programguid);
    }
}
