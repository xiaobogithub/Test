using System.Linq;
using System.Collections.Generic;
using ChangeTech.Contracts;
using ChangeTech.IDataRepository;
using Ethos.DependencyInjection;
using ChangeTech.Entities;
using ChangeTech.Models;
using System;

namespace ChangeTech.Services
{
    public class InterventService : ServiceBase, IInterventService
    {
        #region ISpecificInterventService Members

        public List<InterventModel> GetAllIntervent()
        {
            List<InterventModel> allIntervent = new List<InterventModel>();
            List<Intervent> allInterventEntity = Resolve<IInterventRepository>().GetAllIntervents();
            foreach (Intervent interventEntity in allInterventEntity)
            {
                if (!interventEntity.InterventCategoryReference.IsLoaded)
                {
                    interventEntity.InterventCategoryReference.Load(); 
                }
                InterventModel interventModel = new InterventModel();
                interventModel.InterventGUID = interventEntity.InterventGUID;
                interventModel.InterventName = interventEntity.Name;
                interventModel.Description = interventEntity.Description;
                interventModel.InterventCategoryGUID = interventEntity.InterventCategory.InterventCategoryGUID;
                interventModel.InterventCategoryName = interventEntity.InterventCategory.Name;

                allIntervent.Add(interventModel);
            }

            return allIntervent;
        }

        public List<InterventModel> GetInterventsOfCategory(Guid categoryGuid)
        {
            List<InterventModel> intervents = new List<InterventModel>();
            List<Intervent> interventEntities = Resolve<IInterventRepository>().GetInterventsOfCategory(categoryGuid);
            foreach (Intervent intervent in interventEntities)
            {
                if (!intervent.InterventCategoryReference.IsLoaded)
                {
                    intervent.InterventCategoryReference.Load();
                }
                InterventModel specificIntervent = new InterventModel();
                specificIntervent.InterventGUID = intervent.InterventGUID;
                specificIntervent.InterventName = intervent.Name;
                specificIntervent.Description = intervent.Description;
                specificIntervent.InterventCategoryGUID = intervent.InterventCategory.InterventCategoryGUID;
                specificIntervent.InterventCategoryName = intervent.InterventCategory.Name;

                intervents.Add(specificIntervent);
            }

            return intervents; 
        }

        public void InsertIntervent(InterventModel interventModel)
        {
            Intervent intervent = new Intervent();
            intervent.InterventCategory = Resolve<IInterventCategoryRepository>().GetInterventCategory(interventModel.InterventCategoryGUID);
            intervent.Name = interventModel.InterventName;
            intervent.Description = interventModel.Description;
            intervent.InterventGUID = Guid.NewGuid();
            intervent.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
            Resolve<IInterventRepository>().InsertIntervent(intervent);
        }

        public void DeleteIntervent(Guid interventGuid)
        {
            Resolve<IInterventRepository>().DeteteIntervent(interventGuid);
        }

        public bool CanDeleteIntervent(Guid interventGuid)
        {
            bool flug = true;
            if (Resolve<IPageSequenceRepository>().GetPageSequenceByInterventGuid(interventGuid).Count > 0)
            {
                flug = false;
            }

            return flug;
        }

        public InterventModel GetIntervent(Guid interventGuid)
        {
            Intervent intervent = Resolve<IInterventRepository>().GetIntervent(interventGuid);
            if (!intervent.InterventCategoryReference.IsLoaded)
            {
                intervent.InterventCategoryReference.Load();
            }

            InterventModel specificInterventModel = new InterventModel();
            specificInterventModel.InterventName = intervent.Name;
            specificInterventModel.Description = intervent.Description;
            specificInterventModel.InterventGUID = intervent.InterventGUID;
            specificInterventModel.InterventCategoryGUID = intervent.InterventCategory.InterventCategoryGUID;
            specificInterventModel.InterventCategoryName = intervent.InterventCategory.Name;

            return specificInterventModel;
        }

        public string GetInterventName(Guid interventGuid)
        {
            return Resolve<IInterventRepository>().GetIntervent(interventGuid).Name;
        }

        public void UpdateIntervent(InterventModel interventModel)
        {
            Intervent interventEntity = Resolve<IInterventRepository>().GetIntervent(interventModel.InterventGUID);
            interventEntity.InterventCategory = Resolve<IInterventCategoryRepository>().GetInterventCategory(interventModel.InterventCategoryGUID);
            interventEntity.Name = interventModel.InterventName;
            interventEntity.Description = interventModel.Description;
            interventEntity.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
            Resolve<IInterventRepository>().UpdateIntervent(interventEntity);
        }

        public void MergeIntervent(Guid toInterventGUID, Guid fromInterventGUID)
        {
            List<PageSequence> pagesequencelist = Resolve<IPageSequenceRepository>().GetPageSequenceByInterventGuid(fromInterventGUID);
            Intervent toIntervent = Resolve<IInterventRepository>().GetIntervent(toInterventGUID);
            foreach (PageSequence sequence in pagesequencelist)
            {
                sequence.Intervent = toIntervent;
                Resolve<IPageSequenceRepository>().UpdatePageSequence(sequence);
            }
        }

        #endregion
    }
}
