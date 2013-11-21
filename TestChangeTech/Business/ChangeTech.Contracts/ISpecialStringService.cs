using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;

namespace ChangeTech.Contracts
{
    public interface ISpecialStringService
    {
        List<SpecialStringModel> GetSpecialStringByLanguage(Guid languageGuid);
        void UpdateSpecialString(SpecialStringModel model);
        SpecialStringModel GetSpecialStringBy(string name, Guid languageguid);
        void AddSpecialString(string name);
        bool ExistSpecialString(string name);
    }
}
