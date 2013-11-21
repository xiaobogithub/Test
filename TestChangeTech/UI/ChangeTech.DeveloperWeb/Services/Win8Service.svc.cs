using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.ServiceModel.Activation;
using Ethos.DependencyInjection;
using ChangeTech.Models;
using ChangeTech.Contracts;
using Ethos.Utility;

namespace ChangeTech.DeveloperWeb.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
    public class Win8Service : ServiceBase, IWin8Service
    {
        public List<ProgramCategoryModel> GetPrograms(string windowsLiveId, string applicationId)
        {
            List<ProgramCategoryModel> programInfoModels = null;
            try
            {
                programInfoModels = Resolve<IProgramCategoryService>().GetProgramCategories(windowsLiveId, applicationId);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }

            return programInfoModels;
        }

        public ProgramContentModel GetProgram(string windowsLiveId, string applicationId, string programGuidStr)
        {
            ProgramContentModel programContentModel = null;
            Guid programGuid=Guid.Empty;
            try
            {
                if (Guid.TryParse(programGuidStr, out programGuid))
                {
                    programContentModel = Resolve<IProgramService>().GetProgramInfoModelByProgramGuid(windowsLiveId, applicationId, programGuid);
                }
                else
                {
                    //Program code format : 26Z28B
                    if (programGuidStr.Length==6)
                    {
                        programGuid = Resolve<IProgramService>().GetProgramGUIDByProgramCode(programGuidStr);
                        programContentModel = Resolve<IProgramService>().GetProgramInfoModelByProgramGuid(windowsLiveId, applicationId, programGuid);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }

            return programContentModel;
        }

        public RegisterMessageModel RegisterProgramUsers(RegisterWin8ProgramUsersModel registerProgramUsersModel)
        {
            RegisterMessageModel registerMessageModel = null;
            try
            {
                registerMessageModel = Resolve<IProgramUserService>().Win8EndUserJoinProgram(registerProgramUsersModel);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return registerMessageModel;
        }

        //public List<SessionInfoModel> GetSessions(string windowsLiveId, string applicationId, string programGuidStr)
        //{
        //    List<SessionInfoModel> sessionInfoModels = null;
        //    try
        //    {
        //        //sessionInfoModels = Resolve<ISessionService>().GetSessionInfoModelsByProgramGuid(programGuid);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
        //        throw ex;
        //    }
        //    return sessionInfoModels;
        //}
    }
}