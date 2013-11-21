using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface ITipMessageRepository
    {
        IQueryable<TipMessage> GetTipMessageByProgram(Guid programGuid);
        TipMessage GetTipMessage(Guid tipMessageGuid);
        void UpdateTipMessage(TipMessage tipMessage);
        IQueryable<TipMessageType> GetAllTipMessageType();
        TipMessage GetTipMessage(Guid tipMessageTypeGuid, Guid programGuid);
        TipMessageType GetTipMessageType(Guid tipMessageTypeGuid);
        void Insert(TipMessage tipmessage);
        TipMessageType GetTipMessageType(string name);
        TipMessage GetTipMessage(Guid programGuid, string tipMesageTypeName);
        void DeleteTipMessagesByProgramGuid(Guid programGuid);
    }
}
