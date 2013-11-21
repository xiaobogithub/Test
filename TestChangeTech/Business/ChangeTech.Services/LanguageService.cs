using System;
using System.Linq;
using ChangeTech.Contracts;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;
using ChangeTech.Models;
using Ethos.DependencyInjection;
using System.Collections.Generic;

namespace ChangeTech.Services
{
    public class LanguageService: ServiceBase, ILanguageService
    {
        #region ILanguageService Members

        public LanguagesModel GetLanguagesModel()
        {
            LanguagesModel languagesMode = new LanguagesModel();
            IQueryable<Language> languageEntities = Resolve<ILanguageRepository>().GetAllLanguages().OrderBy(l=>l.Name);
            foreach (Language languageEntity in languageEntities)
            {
                LanguageModel languageModel = new LanguageModel();
                languageModel.Name = languageEntity.Name;
                languageModel.LanguageGUID = languageEntity.LanguageGUID;
                languagesMode.Add(languageModel);
            }
            return languagesMode;
        }

        public void UpdateLanguage(Guid languageGUID, string name)
        {
            Language languageEntity = Resolve<ILanguageRepository>().GetLanguage(languageGUID);
            languageEntity.Name = name;
            languageEntity.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
            Resolve<ILanguageRepository>().UpdateLanguage(languageEntity);
        }

        public void DeleteLanguage(Guid languageGUID)
        {
            Resolve<ILanguageRepository>().DeleteLanguage(languageGUID);
        }

        public void AddLanguage(string name)
        {
            Language language = new Language();
            language.LanguageGUID = Guid.NewGuid();
            language.Name = name;
            language.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
            Resolve<ILanguageRepository>().AddLanguage(language);
        }

        public LanguageModel GetLanguageMode(Guid languageGUID)
        {
            Language languageEntity = Resolve<ILanguageRepository>().GetLanguage(languageGUID);
            LanguageModel languageModel = new LanguageModel();
            languageModel.Name = languageEntity.Name;
            languageModel.LanguageGUID = languageEntity.LanguageGUID;
            return languageModel;
        }

        public bool IsValidLanguageName(string name)
        {
            Language language = Resolve<ILanguageRepository>().GetLanguage(name);
            if (language != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public List<ManageLanguageModel> GetAllProgramLanguageModel()
        {
            List<Language> Languages = Resolve<ILanguageRepository>().GetAllLanguages().ToList();
            List<ManageLanguageModel> LanguageModels = new List<ManageLanguageModel>();
            foreach (Language lan in Languages)
            {
                ManageLanguageModel languagemodel = new ManageLanguageModel();
                languagemodel.Name = lan.Name;
                languagemodel.LanguageGUID = lan.LanguageGUID;
                languagemodel.Count = Resolve<IProgramService>().GetProgramCountByLanguageGuid(lan.LanguageGUID);

                LanguageModels.Add(languagemodel);
            }

            return LanguageModels;
        }
        #endregion
    }
}
