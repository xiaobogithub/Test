using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeTech.Contracts
{
    public interface IExportService
    {
        string AddExportProgramCommand(string fileName, Guid programGUID, Guid lanagueGUID, string startDay, string endDay, bool includeRelapse, bool includeProgramRoom,
    bool includeAccessoryTemplate, bool includeEmailTemplate, bool includeHelpItem, bool includeUserMenu, bool includeTipMessage, bool includeSpecialString);
        void ExportProgram(string fileName, Guid programGUID, Guid languageGUID, int startDay, int endDay, bool includeRelapse, bool includeProgramRoom,
        bool includeAccessoryTemplate, bool includeEmailTemplate, bool includeHelpItem, bool includeUserMenu, bool includeTipMessage, bool includeSpecialString);
        string AddReportProgramCommand(string fileName, Guid programGUID, Guid lanagueGUID);
        void ReportProgram(string fileName, Guid programGUID, Guid languageGUID);
        string AddReportProgramUserVariableCommand(string fileName, Guid proramGUID);
        string AddReportProgramUserVariableExtensionCommand(string fileName, Guid programGUID);
        void ExportProgramUserVariable(string fileName, Guid programGUID);
        void ExportUserPageVariable(string fileName, Guid programGUID);
        void ExportUserPageVariableExtension(string fileName, Guid programGUID);
    }
}
