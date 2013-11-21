using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;
using ChangeTech.Models;

namespace ChangeTech.Services
{
    public class CompanyService : ServiceBase, ICompanyService
    {
        #region ICompanyService Members

        public List<CompanyRightModel> GetCompanyRightListByProgram(Guid programGuid)
        {
            List<CompanyRightModel> models = new List<CompanyRightModel>();
            List<CompanyRight> companyrightentities = Resolve<ICompanyRightRepository>().GetCompanyRightByProgram(programGuid).ToList();
            foreach(CompanyRight right in companyrightentities)
            {
                if(!right.CompanyReference.IsLoaded)
                {
                    right.CompanyReference.Load();
                }
                CompanyRightModel model = new CompanyRightModel
                {
                    CompanyGUID = right.Company.CompanyGUID,
                    CompanyName = right.Company.Name,
                    OverDueTime = right.OverdueTime.Value,
                    CompanyRightGUID = right.CompanyRightGUID,
                    StartTime = right.StartTime.HasValue ? right.StartTime.Value : DateTime.MinValue
                };
                models.Add(model);
            }

            return models;
        }

        public List<CompanyRightModel> GetCompanyRightListByProgram(Guid programGuid, int currentPage, int pageSize)
        {
            List<CompanyRightModel> models = new List<CompanyRightModel>();
            List<CompanyRight> companyrightentities = Resolve<ICompanyRightRepository>().GetCompanyRightByProgram(programGuid).OrderBy(p => p.Company.Name).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            foreach(CompanyRight right in companyrightentities)
            {
                if(!right.CompanyReference.IsLoaded)
                {
                    right.CompanyReference.Load();
                }
                CompanyRightModel model = new CompanyRightModel
                {
                    CompanyGUID = right.Company.CompanyGUID,
                    CompanyName = right.Company.Name,
                    OverDueTime = right.OverdueTime.Value,
                    CompanyRightGUID = right.CompanyRightGUID,
                    StartTime = right.StartTime.HasValue ? right.StartTime.Value : DateTime.MinValue
                };
                models.Add(model);
            }

            return models;
        }

        public List<CompanyModel> GetCompanyListNotJoinProgram(Guid programGuid)
        {
            List<CompanyModel> models = new List<CompanyModel>();
            List<Company> companyentities = Resolve<ICompanyRepository>().GetCompanyNotJoinProgram(programGuid).ToList();
            foreach(Company entity in companyentities)
            {
                CompanyModel model = new CompanyModel
                {
                    CompanyGUID = entity.CompanyGUID,
                    Description = entity.Description,
                    Name = entity.Name
                };

                models.Add(model);
            }

            return models;
        }

        public void AddCompany(CompanyRightModel model)
        {
            CompanyModel companymodel = new CompanyModel();
            companymodel.CompanyGUID = model.CompanyGUID;
            companymodel.Name = model.CompanyName;
            companymodel.Description = model.ComayDescription;
            companymodel.ContactPerson = model.ContactPerson;
            companymodel.InternalContact = model.InternalContact;
            companymodel.Mobile = model.Mobile;
            companymodel.Email = model.Email;
            companymodel.StreetAddress = model.StreetAddress;
            companymodel.PostalAddress = model.PostalAddress;

            AddCompany(companymodel);
            AddCompanyRight(model);
        }

        public void AddCompany(CompanyModel model)
        {
            Company entity = new Company();
            entity.Name = model.Name;
            entity.Description = model.Description;
            entity.CompanyGUID = model.CompanyGUID;
            entity.ContactPerson = model.ContactPerson;
            entity.InternalContact = model.InternalContact;
            entity.Mobile = model.Mobile;
            entity.Email = model.Email;
            entity.StreetAddress = model.StreetAddress;
            entity.PostalAddress = model.PostalAddress;
            entity.Code = GetCompanyCode();

            Resolve<ICompanyRepository>().AddCompany(entity);
        }

        public void AddCompanyRight(CompanyRightModel rightmodel)
        {
            CompanyRight entity = new CompanyRight
            {
                Company = Resolve<ICompanyRepository>().GetCompanyByGuid(rightmodel.CompanyGUID),
                Program = Resolve<IProgramRepository>().GetProgramByGuid(rightmodel.ProgramGUID),
                OverdueTime = rightmodel.OverDueTime,
                CompanyRightGUID = rightmodel.CompanyRightGUID,
                StartTime = rightmodel.StartTime
            };

            Resolve<ICompanyRightRepository>().Add(entity);
        }

        public CompanyRightModel GetCompanyRightModelByGuid(Guid companyrightguid)
        {
            CompanyRight entity = Resolve<ICompanyRightRepository>().GetConpanyRightByGuid(companyrightguid);
            if(!entity.CompanyReference.IsLoaded)
            {
                entity.CompanyReference.Load();
            }
            if(!entity.ProgramReference.IsLoaded)
            {
                entity.ProgramReference.Load();
            }
            if(!entity.Program.LanguageReference.IsLoaded)
            {
                entity.Program.LanguageReference.Load();
            }
            CompanyRightModel model = new CompanyRightModel
            {
                OverDueTime = entity.OverdueTime.Value,
                StartTime = entity.StartTime.HasValue ? entity.StartTime.Value : DateTime.MinValue,
                Language = entity.Program.Language.Name,
                ProgramName = entity.Program.Name,
                ProgramCode = entity.Program.Code,
                CompanyName = entity.Company.Name,
                CompanyCode = entity.Company.Code,
                ProgramGUID = entity.Program.ProgramGUID,
                CompanyGUID = entity.Company.CompanyGUID,
                ContactPerson = entity.Company.ContactPerson,
                InternalContact = entity.Company.InternalContact,
                Mobile = entity.Company.Mobile,
                Email = entity.Company.Email,
                StreetAddress = entity.Company.StreetAddress,
                PostalAddress = entity.Company.PostalAddress,
                ComayDescription = entity.Company.Description,
                IsSupportHttps = entity.Program.IsSupportHttps.HasValue ? entity.Program.IsSupportHttps.Value : false
            };

            return model;
        }

        public void UpdateCompanyRightOverdueTime(DateTime time, Guid companyguid)
        {
            CompanyRight entity = Resolve<ICompanyRightRepository>().GetConpanyRightByGuid(companyguid);
            entity.OverdueTime = time;
            Resolve<ICompanyRightRepository>().Update(entity);
        }

        public void DeleteCompanyRight(Guid rightguid)
        {
            Resolve<ICompanyRightRepository>().Delete(rightguid);
        }

        public bool ValidateCompanyRight(Guid programguid, Guid companyguid)
        {
            bool flug = false;
            CompanyRight right = Resolve<ICompanyRightRepository>().GetCompanyRightByProgramAndCompany(programguid, companyguid);
            if (right != null && right.OverdueTime.Value.Date >= DateTime.UtcNow.Date)
            {
                flug = true;
            }

            return flug;
        }

        public void UpdateCompanyRight(CompanyRightModel model)
        {
            CompanyRight rightentity = Resolve<ICompanyRightRepository>().GetConpanyRightByGuid(model.CompanyRightGUID);
            if(!rightentity.CompanyReference.IsLoaded)
            {
                rightentity.CompanyReference.Load();
            }

            rightentity.StartTime = model.StartTime;
            rightentity.OverdueTime = model.OverDueTime;
            rightentity.Company.Description = model.ComayDescription;
            rightentity.Company.Name = model.CompanyName;
            rightentity.Company.ContactPerson = model.ContactPerson;
            rightentity.Company.InternalContact = model.InternalContact;
            rightentity.Company.Mobile = model.Mobile;
            rightentity.Company.Email = model.Email;
            rightentity.Company.StreetAddress = model.StreetAddress;
            rightentity.Company.PostalAddress = model.PostalAddress;

            Resolve<ICompanyRightRepository>().Update(rightentity);
        }

        public int GetCompanyCountByProgram(Guid programGuid)
        {
            return Resolve<ICompanyRightRepository>().GetCompanyRightByProgram(programGuid).Count();
        }

        public void SetCompanyCodeForCompany(Guid companyGuid)
        {
            Company companyEntity = Resolve<ICompanyRepository>().GetCompanyByGuid(companyGuid);
            if(string.IsNullOrEmpty(companyEntity.Code))
            {
                companyEntity.Code = GetCompanyCode();
            }

            Resolve<ICompanyRepository>().UpdateCompany(companyEntity);
        }

        public Guid GetCompanyGuidByCode(string code)
        {
            Guid companyGuid = Guid.Empty;
            Company entity = Resolve<ICompanyRepository>().GetCompanyByCode(code);
            if(entity != null)
            {
                companyGuid = entity.CompanyGUID;
            }

            return companyGuid;
        }

        #endregion

        #region

        private string GetCompanyCode()
        {
            string code = string.Empty;
            do
            {
                code = Ethos.Utility.StringUtility.GenerateCheckCode(6);
            }
            while(IsCompanyCodeExisted(code));

            return code;
        }

        private bool IsCompanyCodeExisted(string code)
        {
            bool flag = false;
            if(Resolve<ICompanyRepository>().GetCompanyByCode(code) != null)
            {
                flag = true;
            }

            return flag;
        }

        #endregion
    }
}
