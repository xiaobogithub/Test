using ChangeTech.Models;
using System.Collections.Generic;
using System;
using ChangeTech.Entities;

namespace ChangeTech.Contracts
{
    public interface IPageSequenceService
    {
        PageSequenceModel GetPageSequenceBySessionGuidAndPageSeqGuid(Guid sessionGuid, Guid pageSeqGuid);
        List<PageSequenceModel> GetPageSequenceBySessionGuid(Guid sessionGuid);
        List<PageSequenceModel> GetPageSequenceByInterventCategory(Guid interventCategoryGuid);
        List<PageSequenceModel> GetPageSequenceByInterventGuid(Guid interventGuid);
        EditPageSequenceModel GetPageSequenceBySessionContetnGuid(Guid sessionContentGuid);
        EditPageSequenceModel GetPageSequenceBySessionGuidPageSequenceGuid(Guid sessionGuid, Guid PageSequenceGuid);
        EditPageSequenceModel GetPageSequenceByProgramGuidPageSequenceGuid(Guid programGuid, Guid pageSequenceGuid);
        EditPageSequenceModel GetPageSequenceBySequenceGuid(Guid sequenceGuid);
        PageSequenceModel GetRelapsePageSequenceModelBySequenceGuid(Guid sequenceGuid);
        List<PageSequenceUsedInfo> GetPageSequenceUsedInfo(Guid sessionContentGuid);
        void UpdateRelapsePageSequence(Guid programGuid, Guid pageSequenceGuid, string name, string description, Guid roomGuid);
        void UpdatePageSequence(Guid pageSequenceGuid, Guid sessionGuid, Guid roomGuid, string name, string description);
        void AddPageSequence(string name, string description, Guid specificInterventGuid);
        string GetPreviewPageSequenceModelAsXML(Guid languageGuid, Guid sessionGuid, Guid pageSequenceGuid, Guid userGuid);
        string GetRelapseModelAsXML(Guid languageGuid, Guid programGuid, Guid pageSeuqneceGuid, Guid userGuid);
        string GetRelapsePreviewModelAsXML(Guid languageGuid, Guid programGuid, Guid pageSeuqneceGuid, Guid userGuid);
        string GetTempPreviewPageSequenceModelAsXML(Guid languageGuid, Guid sessionGuid, Guid pageSequenceGuid, Guid userGuid);
        bool PageSequenceReferenced(Guid pageSeqGuid);
        bool PageSequenceInMoreSession(Guid currentSession,Guid pageSeqGuid);
        Guid BeforeEditPageSequenceAfterRefactoring(Guid sessionGuid, Guid pageSeqGuid, bool affectFlag);
        //void BeforeEditPageSequence(Guid sessionGuid, Guid pageSeqGuid, bool affectFlag);
        void BeforeEditPageSequenceOnly(Guid pageSeqGuid, bool affectFlag);
        bool DeletePageSequence(Guid pageSequenceGuid);
        int GetCountOfPagesInPageSequence(Guid sequenceGuid);
        //int GetBiggestPageNOInPageSequence(Guid sequenceGuid);
        void UseExistedPageSequence(Guid sessionGuid, Guid sequenceGuid, Guid roomGuid, int order);
        void MovePageSequenceToAnotherIntervent(Guid sequenceGuid, Guid interventGuid);
        void MovePageSequenceToAnotherIntervent(string[] sequences, Guid interventGuid);
        bool IsReferencedByProgram(Guid sequenceGuid);
        PageSequence ClonePageSequence(PageSequence pageSeq, List<KeyValuePair<string, string>> cloneRelapseGUIDList, Dictionary<Guid, Guid> pageDictionary, CloneProgramParameterModel cloneParameterModel);
        PageSequence ClonePageSequence(PageSequence pageSeq, List<KeyValuePair<string, string>> cloneRelapseGUIDList, CloneProgramParameterModel cloneParameterModel);
        PageSequence SetDefaultGuidForPageSequence(PageSequence needSetDefaultEntity, CloneProgramParameterModel cloneParameterModel);
    }
}
