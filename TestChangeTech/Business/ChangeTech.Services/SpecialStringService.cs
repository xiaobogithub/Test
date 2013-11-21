using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;
using ChangeTech.IDataRepository;
using ChangeTech.Models;
using ChangeTech.Entities;

namespace ChangeTech.Services
{
    public class SpecialStringService : ServiceBase, ISpecialStringService
    {
        public List<ChangeTech.Models.SpecialStringModel> GetSpecialStringByLanguage(Guid languageGuid)
        {
            List<SpecialString> stringlist = Resolve<ISpecialStringRepository>().GetSpecialStringListOfLanguage(languageGuid).ToList();
            List<string> allSpecialStringKeys = Resolve<ISpecialStringRepository>().GetAllSpecialStringKeys().ToList();
            List<ChangeTech.Models.SpecialStringModel> models = new List<ChangeTech.Models.SpecialStringModel>();
            foreach (string key in allSpecialStringKeys)
            {
                SpecialString specialStringEntity = stringlist.Where(s => s.Name == key).FirstOrDefault();
                SpecialStringModel model = null;
                if (specialStringEntity != null)
                {
                    model = new ChangeTech.Models.SpecialStringModel()
                    {
                        LanguageGuid = languageGuid,
                        Name = specialStringEntity.Name,
                        Value = specialStringEntity.Value
                    };
                }
                else
                {
                    specialStringEntity = new SpecialString();
                    specialStringEntity.Name = key;
                    specialStringEntity.Value = "";
                    specialStringEntity.Language = Resolve<ILanguageRepository>().GetLanguage(languageGuid);
                    Resolve<ISpecialStringRepository>().AddSpecialString(specialStringEntity);

                    model = new ChangeTech.Models.SpecialStringModel()
                    {
                        LanguageGuid = languageGuid,
                        Name = key,
                        Value = ""
                    };
                }
                models.Add(model);
            }

            return models;
        }

        public void UpdateSpecialString(SpecialStringModel model)
        {
            ChangeTech.Entities.SpecialString spstring = Resolve<ISpecialStringRepository>().GetSpecialString(model.LanguageGuid, model.Name);
            spstring.Value = model.Value;
            Resolve<ISpecialStringRepository>().Update(spstring);
            Resolve<ITranslationJobService>().UpdateElementWhenFromUpdated("SpecialString", spstring.Name, spstring.LanguageGUID);
        }

        public SpecialStringModel GetSpecialStringBy(string name, Guid languageguid)
        {
            ChangeTech.Entities.SpecialString spstring = Resolve<ISpecialStringRepository>().GetSpecialString(languageguid, name);
            SpecialStringModel model = null;
            if (spstring != null)
            {
                if (!spstring.LanguageReference.IsLoaded)
                {
                    spstring.LanguageReference.Load();
                }

                model = new SpecialStringModel
                {
                    Value = spstring.Value,
                    LanguageGuid = languageguid,
                    Name = name,
                    LanguageName = spstring.Language.Name
                };
            }
            else
            {
                model = new SpecialStringModel
                {
                    Value = "",
                    LanguageGuid = languageguid,
                    Name = name,
                    LanguageName = spstring.Language.Name
                };
            }

            return model;
        }

        public void AddSpecialString(string name)
        {
            List<Language> languageEntities = Resolve<ILanguageRepository>().GetAllLanguages().ToList();
            foreach (Language lanaguageEntity in languageEntities)
            {
                SpecialString specialStringEntity = new SpecialString();
                specialStringEntity.Name = name;
                specialStringEntity.Value = "";
                specialStringEntity.Language = lanaguageEntity;
                Resolve<ISpecialStringRepository>().AddSpecialString(specialStringEntity);
            }
        }

        public bool ExistSpecialString(string name)
        {
            return Resolve<ISpecialStringRepository>().ExistSpecialString(name);
        }
    }
}
