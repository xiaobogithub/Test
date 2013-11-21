using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IShortMessageRepository
    {
        void AddShortMessageRepository(ShortMessageQueue message);
        IQueryable<ShortMessageQueue> GetShortMessageShouldSend();
        List<ShortMessageQueue> GetShortMessageListShouldSend(DateTime time);
        List<ShortMessageQueue> GetEmailListShouldSend(DateTime time);
        void AddMessage(ShortMessage message);
        ShortMessage GetMessageByProgramAndType(Guid programGuid, string type);
        void Update(ShortMessage message);
        void UpdateMessageQueue(ShortMessageQueue queue);
        IQueryable<ShortMessageQueue> GetShortMessageQueueByProgram(Guid programGuid);
        void AddSMSRecord(SMSRecord record);
        List<ProgramDailySMS> GetProgramDailySMSList(Guid programGuid);
        void UpdateProgramDailySMS(ProgramDailySMS dailySMSEntity);
        ProgramDailySMS GetProgramDailySMS(Guid programDailySMSGuid);
        ProgramDailySMS GetProgramDailySMSBySessionGuid(Guid sessionGuid);
        void AddProgramDailySMS(ProgramDailySMS dailySMS);
    }
}
