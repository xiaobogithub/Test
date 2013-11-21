using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;
using ChangeTech.Entities;

namespace ChangeTech.Contracts
{
    public interface ITipMessageService
    {
        TipMessageListModel GetTipMeassageByProgram(Guid programGuid);
        TipMessageModel GetTipMessageModel(Guid tipMessageGuid);
        void UpdateTipMessageModel(TipMessageModel tipmessage);
        List<TipMessageTypeModel> GetAllTipMessageType();
        TipMessageModel GetTipMessageModel(Guid tipMessageTypeGuid, Guid languageGuid);
        void InsertTipMessage(TipMessageModel tipmessage);
        string GetTipMessageText(Guid programGuid, string tipMessageTypeName);
        void CopyTipMessageForProgram(Guid programGUID);
        bool CopyTipMessageFromProgram(Guid programGuid, Guid originalProgramGuid);
        TipMessage CloneTipMessage(TipMessage message);
        TipMessage SetDefaultGuidForTipMessage(TipMessage needSetDefaultEntity, CloneProgramParameterModel cloneParameterModel);
    }
}
