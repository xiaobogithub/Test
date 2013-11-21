using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface ICTPPRepository
    {
        CTPP Get(Guid CTPPGuid);   
        void Insert(CTPP ctpp);
        CTPP GetCTPPByProgramGuid(Guid programGuid);
        void Update(CTPP ctpp);

        CTPP GetCTPPByBrandAndProgram(string brandUrl, string programUrl);

        //IQueryable<Brand> GetAllBrand();

        //void Delete(Guid brandGuid);

        List<CTPP> GetCTPPInBrandNotThisProgram(string brandName, string programName);
    }
}
