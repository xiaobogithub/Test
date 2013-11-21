using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;
using ChangeTech.Models;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.Services
{
    public class DeleteApplicationService : ServiceBase, IDeleteApplicationService
    {
        #region IDeleteApplicationService Members

        public List<DeleteApplicationModel> GetApplicationListByAssigneeAndStatus(Guid assigneeGuid, int status)
        {
            List<DeleteApplicationModel> applicationModelList = new List<DeleteApplicationModel>();
            List<DeleteApplication> applicationList = Resolve<IDeleteApplicationRepository>().GetDeleteApplicationByAssigneeAndStatus(assigneeGuid, status).ToList<DeleteApplication>();
            foreach (DeleteApplication application in applicationList)
            {
                if (!application.UserReference.IsLoaded)
                {
                    application.UserReference.Load();
                }
                if(!application.ProgramReference.IsLoaded)
                {
                    application.ProgramReference.Load();
                }
                DeleteApplicationModel applicationModel = new DeleteApplicationModel()
                {
                    ApplicationGUID=application.DeleteApplicationGUID,
                    ProgramGUID = application.Program.ProgramGUID,
                    ProgramName = application.Program.Name,
                    Status = Enum.GetName(typeof(ApplicationStatusEnum), application.Status),
                    ApplicantEmail = application.User.Email
                };
                applicationModelList.Add(applicationModel);                
            }

            return applicationModelList;
        }

        public List<DeleteApplicationModel> GetApplicationListByApplicant(Guid applicantGuid)
        {
            List<DeleteApplicationModel> applicationModelList = new List<DeleteApplicationModel>();
            List<DeleteApplication> applicationList = Resolve<IDeleteApplicationRepository>().GetDeleteApplicationByApplicant(applicantGuid).ToList<DeleteApplication>();
            foreach (DeleteApplication application in applicationList)
            {
                if (!application.User1Reference.IsLoaded)
                {
                    application.User1Reference.Load();
                }
                if (!application.ProgramReference.IsLoaded)
                {
                    application.ProgramReference.Load();
                }
                DeleteApplicationModel applicationModel = new DeleteApplicationModel()
                {
                    ApplicationGUID = application.DeleteApplicationGUID,
                    ProgramGUID = application.Program.ProgramGUID,
                    ProgramName = application.Program.Name,
                    Status = Enum.GetName(typeof(ApplicationStatusEnum),application.Status),
                    AssigneeEmail = application.User1.Email
                };
                applicationModelList.Add(applicationModel);
            }

            return applicationModelList;
        }

        public void Approve(Guid applicationGuid)
        {
            DeleteApplication application = Resolve<IDeleteApplicationRepository>().GetDeleteApplicationByGuid(applicationGuid);
            if(!application.ProgramReference.IsLoaded)
            {
                application.ProgramReference.Load();
            }
            //Delete program
            Resolve<ProgramService>().DeleteProgram(application.Program.ProgramGUID);
            //Update application status
            application.Status = (int)ApplicationStatusEnum.Approved;
            Resolve<IDeleteApplicationRepository>().UpdateDeleteApplication(application);
        }

        public void Decline(Guid applicationGuid)
        {
            DeleteApplication application = Resolve<IDeleteApplicationRepository>().GetDeleteApplicationByGuid(applicationGuid);
            application.Status = (int)ApplicationStatusEnum.Declined;
            Resolve<IDeleteApplicationRepository>().UpdateDeleteApplication(application);
        }

        public void CreateApplication(DeleteApplicationModel applicationModel)
        {
            DeleteApplication application = new DeleteApplication
            {
                Status = (int)ApplicationStatusEnum.OnProcess,
                DeleteApplicationGUID = Guid.NewGuid(),
                Program = Resolve<IProgramRepository>().GetProgramByGuid(applicationModel.ProgramGUID),
                User = Resolve<IUserRepository>().GetUserByGuid(applicationModel.ApplicantGUID),
                User1 = Resolve<IUserRepository>().GetUserByGuid(applicationModel.AssigneeGUID)
            };
            Resolve<IDeleteApplicationRepository>().InsertDeleteApplication(application);
        }

        public bool IsProgramDeleteApplicationExist(Guid programgGuid)
        {
            bool flug = false;
            DeleteApplication application = Resolve<IDeleteApplicationRepository>().GetDeleteApplicationByProgramGuidAndStatus(programgGuid, (int)ApplicationStatusEnum.OnProcess);
            if (application != null)
            {
                flug = true;
            }

            return flug;
        }

        #endregion
    }
}
