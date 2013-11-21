using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;

namespace ChangeTech.Contracts
{
    public interface IShortMessageService
    {
        void AddShortMessage(ShortMessageModel model);
        void AddMessage(MessageModel model);
        MessageModel GetMessageByProgramAndType(Guid programGuid, string type);
        void UpdateMessage(MessageModel model);
        string GetMessageTextByProgramAndType(Guid programGuid, string type);
        bool SendSM(GetMessageModel model);
        void SendShortMessageQueue();
        List<ShortMessageQueueModel> GetSMQList(Guid programGuid, int currentPage, int pageSize, string email);
        int GetSMQListCount(Guid programGuid, string email);
        void AddSMSRecord(SMSRecordModel model);
        List<DailySMSContentModel> GetDailySMSContentList(Guid programGuid);
        void UpdateProgramDailySMSContent(Guid programDailySMSGuid, string newContent);
        void UpdateProgramDailySMSContentBySessionGuid(Guid sessionGuid, string newContent);
    }
}
