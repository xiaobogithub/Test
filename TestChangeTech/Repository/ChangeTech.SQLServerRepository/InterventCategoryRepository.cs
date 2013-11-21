using System;
using System.Collections.Generic;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;
using System.Linq;

namespace ChangeTech.SQLServerRepository
{
    public class InterventCategoryRepository : RepositoryBase, IInterventCategoryRepository
    {
        #region InterventCategoryRepository Members

        public List<InterventCategory> GetInterventCategoryByPredictorGuid(Guid guid)
        {
            return GetEntities<InterventCategory>(i => i.Predictor.PredictorGUID == guid && 
                (i.IsDeleted.HasValue? i.IsDeleted.Value == false: true)).ToList<InterventCategory>();
        }

        public List<InterventCategory> GetAllInterventCategory()
        {
            return GetEntities<InterventCategory>().OrderBy(i =>i.Name).ToList<InterventCategory>();
        }

        public InterventCategory GetInterventCategory(Guid interventCategoryGuid)
        {
            return GetEntities<InterventCategory>(i => i.InterventCategoryGUID == interventCategoryGuid && 
                (i.IsDeleted.HasValue? i.IsDeleted.Value == false: true)).FirstOrDefault();
        }

        public void UpdateInterventCategory(InterventCategory interventCategory)
        {
            UpdateEntity(interventCategory);
        }

        public void DeleteInterventCategory(Guid categoryGuid)
        {
            DeleteEntity<InterventCategory>(i => i.InterventCategoryGUID == categoryGuid, new Guid());
        }

        public void InsertInterventCategory(InterventCategory interventCategory)
        {
            InsertEntity(interventCategory);
        }

        #endregion
    }
}
