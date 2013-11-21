using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class CTPPRepository : RepositoryBase, ICTPPRepository
    {
        public CTPP Get(Guid CTPPGuid)
        {
            return GetEntities<CTPP>(s => s.CTPPGUID == CTPPGuid).FirstOrDefault();
        }

        public void Insert(CTPP entity)
        {
            InsertEntity(entity);
        }

        public CTPP GetCTPPByProgramGuid(Guid programGuid)
        {
            return GetEntities<CTPP>(s => s.Program.ProgramGUID == programGuid).FirstOrDefault();
        }

        public CTPP GetCTPPByBrandAndProgram(string brandUrl, string programUrl)
        {
            return GetEntities<CTPP>(s => s.Brand.BrandURL.Equals(brandUrl, StringComparison.OrdinalIgnoreCase) && s.ProgramURL.Equals(programUrl, StringComparison.OrdinalIgnoreCase) && (s.Program.IsDeleted.HasValue ? s.Program.IsDeleted == false : true) && (s.Program.IsCTPPEnable.HasValue ? s.Program.IsCTPPEnable == true : false)).FirstOrDefault();
        }
        public void Update(CTPP entity)
        {
            UpdateEntity(entity);
        }


        public List<CTPP> GetCTPPInBrandNotThisProgram(string brandName, string programName)//.OrderBy(p => p.PageOrderNo).ToList<Page>()
        {
            return GetEntities<CTPP>(s => s.Brand.BrandURL.Equals(brandName, StringComparison.OrdinalIgnoreCase) && s.ProgramURL.Equals(programName, StringComparison.OrdinalIgnoreCase) == false && (s.Program.IsDeleted.HasValue ? s.Program.IsDeleted == false : true) && s.CTPPGUID != Guid.Empty && s.Brand != null && s.Brand.BrandGUID != Guid.Empty && (s.Program.IsCTPPEnable.HasValue ? s.Program.IsCTPPEnable == true : false)).ToList<CTPP>();
        }
    }
}
