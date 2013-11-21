using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ChangeTech.Contracts;
using ChangeTech.Entities;
using ChangeTech.Models;
using ChangeTech.IDataRepository;
using Ethos.DependencyInjection;
using Ethos.Utility;

namespace ChangeTech.Services
{
    public class ExpressionGroupService: ServiceBase, IExpressionGroupService
    {
        public List<ExpressionGroupModel> GetExpressionGroupOfProgram(Guid sessionGUID)
        {
            List<ExpressionGroupModel> expressionGroupsModel = new List<ExpressionGroupModel>();

            Session sessionEntity = Resolve<ISessionRepository>().GetSessionBySessionGuid(sessionGUID);
            if (!sessionEntity.ProgramReference.IsLoaded)
            {
                sessionEntity.ProgramReference.Load();
            }

            List<ExpressionGroup> expressionGroupEntities = Resolve<IExpressionGroupRepository>().GetExpressionGroupOfProgram(sessionEntity.Program.ProgramGUID).ToList<ExpressionGroup>();
            foreach (ExpressionGroup expressionGroupEntity in expressionGroupEntities)
            {
                ExpressionGroupModel expressionGroupModel = new ExpressionGroupModel();
                expressionGroupModel.Description = expressionGroupEntity.Description;
                expressionGroupModel.ExpressionGroupGUID = expressionGroupEntity.ExpressionGroupGUID;
                expressionGroupModel.Name = expressionGroupEntity.Name;
                if (!expressionGroupEntity.ProgramReference.IsLoaded)
                {
                    expressionGroupEntity.ProgramReference.Load();
                }
                expressionGroupModel.ProgramGUID = expressionGroupEntity.Program.ProgramGUID;
                expressionGroupsModel.Add(expressionGroupModel);
            }
            return expressionGroupsModel;
        }

        public void SaveEditExpressionGroup(EditExpressionGroupModel editExpressionGroup)
        {
            Guid programGUID = Guid.Empty;
            if (editExpressionGroup.ProgramGUID == Guid.Empty)
            {
                Session sessionEntity = Resolve<ISessionRepository>().GetSessionBySessionGuid(editExpressionGroup.SessionGUID);
                if (!sessionEntity.ProgramReference.IsLoaded)
                {
                    sessionEntity.ProgramReference.Load();
                }
                programGUID = sessionEntity.Program.ProgramGUID;
            }
            else
            {
                programGUID = editExpressionGroup.ProgramGUID;
            }

            foreach (ExpressionGroupModel expressionGroupModel in editExpressionGroup.ExpressionGroups)
            {
                switch (editExpressionGroup.ObjectStatus[expressionGroupModel.ExpressionGroupGUID])
                {
                    case ModelStatus.ModelAdd:
                        AddExpressionGroup(expressionGroupModel, programGUID);
                        break;
                    case ModelStatus.ModelEdit:
                        UpdateExpressionGroup(expressionGroupModel);
                        break;
                }
            }

            foreach (Guid expressionGroupGUID in editExpressionGroup.ObjectStatus.Keys)
            {
                if (editExpressionGroup.ObjectStatus[expressionGroupGUID] == ModelStatus.ModelDelete)
                {
                    DeleteExpressionGroup(expressionGroupGUID);
                }
            }
        }

        private void AddExpressionGroup(ExpressionGroupModel expressionGroupModel, Guid programGUID)
        {
            try
            {
                ExpressionGroup expressionGroupEntity = new ExpressionGroup();
                expressionGroupEntity.Description = expressionGroupModel.Description;
                expressionGroupEntity.ExpressionGroupGUID = expressionGroupModel.ExpressionGroupGUID;
                expressionGroupEntity.Name = expressionGroupModel.Name;
                expressionGroupEntity.Program = Resolve<IProgramRepository>().GetProgramByGuid(programGUID);
                expressionGroupEntity.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
                Resolve<IExpressionGroupRepository>().AddExpressionGroup(expressionGroupEntity);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method: AddExpressionGroup; Name: {0}, Description: {1}, ExpressionGroupGUID {2}, ProgramGUID {3}",
                    expressionGroupModel.Name, expressionGroupModel.Description, expressionGroupModel.ExpressionGroupGUID, programGUID));
                throw ex;
            }
        }

        private void UpdateExpressionGroup(ExpressionGroupModel expressionGroupModel)
        {
            try
            {
                ExpressionGroup expressionGroupEntity = Resolve<IExpressionGroupRepository>().GetExpressionGroup(expressionGroupModel.ExpressionGroupGUID);
                expressionGroupEntity.Description = expressionGroupModel.Description;
                expressionGroupEntity.Name = expressionGroupModel.Name;
                expressionGroupEntity.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
                Resolve<IExpressionGroupRepository>().UpdateExpressionGroup(expressionGroupEntity);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method: UpdateExpressionGroup; Name: {0}, Description: {1}, ExpressionGroupGUID {2}, ProgramGUID {3}",
                    expressionGroupModel.Name, expressionGroupModel.Description, expressionGroupModel.ExpressionGroupGUID, expressionGroupModel.ProgramGUID));
                throw ex;
            }
        }

        private void DeleteExpressionGroup(Guid expressionGroupGUID)
        {
            try
            {
                Resolve<IExpressionGroupRepository>().DeleteExpressionGroup(expressionGroupGUID);
            }
            catch(Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method: DeleteExpressionGroup; ExpressionGroupGUID: {0}", expressionGroupGUID));
                throw ex;
            }
        }
    }
}
