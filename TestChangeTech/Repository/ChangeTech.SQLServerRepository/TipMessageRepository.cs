using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class TipMessageRepository : RepositoryBase, ITipMessageRepository
    {
        public IQueryable<TipMessage> GetTipMessageByProgram(Guid programguid)
        {
            return GetEntities<TipMessage>(t => t.Program.ProgramGUID == programguid);
        }

        public TipMessage GetTipMessage(Guid tipMessageGuid)
        {
            return GetEntities<TipMessage>(t => t.TipMessageGUID == tipMessageGuid).FirstOrDefault();
        }

        public void UpdateTipMessage(TipMessage tipMessage)
        {
            UpdateEntity(tipMessage);
        }

        public IQueryable<TipMessageType> GetAllTipMessageType()
        {
            return GetEntities<TipMessageType>().OrderBy(t => t.TipMessageTypeName);
        }

        public TipMessage GetTipMessage(Guid tipMessageTypeGuid, Guid programGuid)
        {
            return GetEntities<TipMessage>(t => t.Program.ProgramGUID == programGuid && t.TipMessageType.TipMessageTypeGUID == tipMessageTypeGuid).FirstOrDefault();
        }

        public TipMessageType GetTipMessageType(Guid tipMessageTypeGuid)
        {
            return GetEntities<TipMessageType>(t => t.TipMessageTypeGUID == tipMessageTypeGuid).FirstOrDefault();
        }

        public void Insert(TipMessage tipmessage)
        {
            InsertEntity(tipmessage);
        }

        public void DeleteTipMessagesByProgramGuid(Guid programGuid)
        {
            DeleteEntities<TipMessage>(tm => tm.Program.ProgramGUID == programGuid, Guid.Empty);
        }

        public TipMessageType GetTipMessageType(string name)
        {
            return GetEntities<TipMessageType>(t => t.TipMessageTypeName == name).FirstOrDefault();
        }

        public TipMessage GetTipMessage(Guid programGuid, string tipMesageTypeName)
        {
            return GetEntities<TipMessage>(t => t.TipMessageType.TipMessageTypeName == tipMesageTypeName && t.Program.ProgramGUID == programGuid).FirstOrDefault();
        }
    }
}
