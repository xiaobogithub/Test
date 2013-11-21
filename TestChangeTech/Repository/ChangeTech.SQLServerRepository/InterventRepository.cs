using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.IDataRepository;
using ChangeTech.Entities;

namespace ChangeTech.SQLServerRepository
{
    public class InterventRepository : RepositoryBase, IInterventRepository
    {
        #region ISpecificInterventRepository Members

        public List<Intervent> GetAllIntervents()
        {
            return GetEntities<Intervent>().OrderBy(s => s.Name).ToList<Intervent>();
        }

        public Intervent GetIntervent(Guid interventGuid)
        {
            return GetEntities<Intervent>(s => s.InterventGUID == interventGuid && 
                (s.IsDeleted.HasValue? s.IsDeleted.Value == false : true)).FirstOrDefault();
        }

        public List<Intervent> GetInterventsOfCategory(Guid categoryGuid)
        {
            return GetEntities<Intervent>(s => s.InterventCategory.InterventCategoryGUID == categoryGuid && 
                (s.IsDeleted.HasValue? s.IsDeleted.Value == false : true)).ToList<Intervent>();
        }

        public void DeteteIntervent(Guid interventGuid)
        {
            DeleteEntity<Intervent>(s => s.InterventGUID == interventGuid, new Guid());
        }

        public void UpdateIntervent(Intervent intervent)
        {
            UpdateEntity(intervent);
        }

        public void InsertIntervent(Intervent intervent)
        {
            InsertEntity(intervent);
        }

        #endregion
    }
}
