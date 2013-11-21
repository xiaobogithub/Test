using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;
using ChangeTech.Entities;
using System.Xml;

namespace ChangeTech.Contracts
{
    /// <summary>
    /// TODO: Need to refactor
    /// </summary>
    public interface IProgramService
    {
        ProgramsModel GetProgramsModel();
        ProgramsModel GetProgramsModel(int pageNumber, int pageSize);
        List<ProgramModel> GetSortProgramsModel(int pageNumber, int pageSize, string sortByContent);
        List<ProgramModel> GetAllProgramsModel();
        ProgramModel GetProgramByGUID(Guid programGUID);
        ProgramModel GetProgramByProgramGUIDAndLanguageGUID(Guid programGUID, Guid languageGUID);
        SimpleProgramModel GetSimpleProgram(Guid programGUID);
        Guid GetProgramGuidBySessionContentGuid(Guid sessionContentGuid);

        int GetNumberOfProgram();
        int GetNumberOfUser(Guid programGuid, string email);
        int GetAllNumberOfProgram(string keyword);

        List<SimpleProgramModel> GetSimplePrograms(int pageNumber, int pageSize, string keyword);
        List<SimpleProgramModel> GetAllSimpleProgramModel();
        List<SimpleProgramModel> GetSimpleProgramsModel();
        EditProgramModel GetEditProgramModelByGuid(Guid programGuid, int currentPageNumber, int pagesize);

        ProgramSecurityModel GetProgramSecuirtyModel(Guid programGuid);
        ProgramSecurityModel GetProgramSecuirtyModel(Guid programGuid, string email, int pageNumber, int pageSize);
        UserSecurityModel GetProgramUserSecureityModel(Guid programUserGuid);
        UserSecurityModel GetProgramUserSecurityModel(Guid programGuid, Guid userGuid);
        //void UpdateProgramSecurity(Guid prorgamGuid, Guid userGuid, Permission security, int mailTime, string status);
        //void UpdateProgramSecurity(Guid programuserguid, Permission userSecurity, int mailTime, string status, string email, string firstName, string lastName, string mobile, string gender);
        void DeleteProgramUser(Guid programGuid, Guid userGuid);
        void DeleteProgramUser(Guid programuserguid);
        SortedList<Guid, int> GetProgramPermissionByUserGuid(Guid userGuid);

        LanguageModel GetProgramLanguage(Guid programGuid);

        string GetProgramLogo(Guid ProgramGuid);
        string GetProgramSetting(Guid programGuid);
        string GetProgramSettingBySessinGUID(Guid sessionGuid);
        ProgramReportModel GetProgramReportModel(Guid programGuid);

        PrimaryButtonColorModel GetPrimaryButtonColor(Guid programGuid);
        ProgramColorModel GetProgramColor(Guid programGuid);
        void UpdateProgarmPrimaryButtonAndTopLineColor(Guid programGuid, string normalFrom, string normalTo, string overFrom, string overTo, string downFrom, string downTo, string disableFrom, string disableTo, string topLineColor);
        string GetProgramMainColor(Guid programGuid);
        void UpdateProgramMainColor(Guid programGuid, string toplinecolor, string shadowcolor, bool isvisible);

        //void EditProgram(Guid guid, Guid statusGuid, string programName, string description, Guid defauleLanguage, Guid ProgramLogoGuid
        //    , string LogoName, string LogoType, string LogoFileExtension, string shortName);
        void EditProgram(ProgramModel program);
        void CopyProgram(Guid programGuid, Guid userGuid);

        bool CanDeleteProgram(Guid programGuid);
        void DeleteProgram(Guid programGuid);

        void AddProgram(ProgramModel program, Guid userGuid);
        void JoinProgram(Guid programGuid, Guid userGuid, Guid companyguid, int mailTime, string clientIP, ProgramUserStatusEnum userStatus, string studyContent);
        void AddLanguageForProgram(Guid programGuid, Guid languageGuid, Guid userGuid);

        Program CloneProgramIncludeDefaultGuid(Program program, string programCode, CloneProgramParameterModel cloneParameterModel);
        Program SetDefaultGuidForProgram(Program needSetDefaultEntity, CloneProgramParameterModel cloneParameterModel);

        void AssignManagerToProgram(Guid programGuid, Guid userGuid);

        List<string> CheckExpressionForProgram(Guid programGuid);
        List<string> CheckProgramSetting(Guid programGuid);

        void DeleteProgramScheduleByProgramAndWeek(Guid programGuid, int week);

        List<TranslationModel> GetTranslationData(Guid programGuid, int startDay, int endDay, bool includeProgramSettings);

        void ClearProgramByDefaultGUID(Guid programGuid);
        int GetProgramCountByLanguageGuid(Guid languageGuid);

        void CopyProgramPrimaryButtonColorFromLayoutSetting(Guid programGuid);
        bool IsProgramNeedPinCode(Guid programGuid);
        ProgramPropertyModel GetProgramProperty(Guid programGuid);
        void UpdateProgramProperty(ProgramPropertyModel model);

        List<SimpleProgramModel> GetSimpleProgramUserHasPermission(Guid userGuid);

        bool IsValidShortName(Guid programGuid, string shortName);
        Guid GetProgramGUIDByProgramCode(string code);
        bool IsProgramSeparateGender(Guid programGuid);
        bool IsSupportHttps(Guid programGuid);
        bool IsProgramNeedPay(Guid programGuid);
        bool IsProgramCTPPEnable(Guid programGuid);
        // for set code for all program, temp function
        void SetProgramCodeForProgram(Guid programGuid);
        string GenerateProgramCode();
        int? GetProgramDailySMSTime(Guid programGuid);
        void UpdateProgramDailySMSTime(Guid programGuid, int? dailySMSTime);
        List<SimpleProgramModel> GetProgramsByLanguageGuid(Guid languageGuid);
        List<SimpleProgramModel> GetSimpleProgramsByLanguageGuid(Guid languageGuid);
        List<SimpleProgramModel> GetOrderProgramsByLanguageGuid(Guid languageGuid);
        List<SimpleProgramModel> GetOrderProgramsByLanguageGuidAndProgramPublishStatusGuid(Guid languageGuid);
        List<SimpleProgramModel> GetHPOrderProgramsByLanguageGuid(Guid languageGuid);

        //the services provider for Win8Service.
        string GetReportButtonLinkAddress(ProgramUser pu);
        string GetHelpButtonLinkAddress(ProgramUser pu);
        ProgramContentModel GetProgramInfoModelByProgramGuid(string windowsLiveId, string applicationId, Guid programGuid);

        //the services provider for ctpp
        List<CTPPSessionPageBodyModel> GetAllPageBodyList(Guid ProgramGUID);
        List<CTPPSessionPageMediaResourceModel> GetAllPageMediaResourceList(Guid ProgramGUID);
    }
}
