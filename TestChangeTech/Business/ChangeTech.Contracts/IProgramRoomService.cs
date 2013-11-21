using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ChangeTech.Models;
using ChangeTech.Entities;

namespace ChangeTech.Contracts
{
    public interface IProgramRoomService
    {
        ProgramRoomsModel GetRoomByProgram(Guid ProgramGuid);
        ProgramRoomModel GetRoom(Guid RoomGuid);
        bool Insert(ProgramRoomModel room);
        void Update(ProgramRoomModel room);
        void Delete(Guid RoomGuid);
        bool CanDelete(Guid RoomGuid);
        ProgramRoom CloneProgramRoom(ProgramRoom room);
        ProgramRoom SetDefaultGuidForProgramRoom(ProgramRoom needSetDefaultEntity, CloneProgramParameterModel cloneParameterModel);
    }
}
