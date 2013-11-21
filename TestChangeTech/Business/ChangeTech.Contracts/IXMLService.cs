using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeTech.Contracts
{
    public interface IXMLService
    {
        string PraseGraphPage(string originalXMLStr, Guid userGUID, Guid sessionGUID, Guid languageGuid, bool isRetake);
        string PraseGraphForOnePage(string originalXMLStr, Guid userGUID, Guid sessionGUID, Guid languageGuid);
        string ParseBeforeAfterShowExpression(string originalXMLStr, Guid sessionGUID, string objects, string programGUID, string userGUID);
        string ParseTimePickerQuestion(string originalXMLStr, Guid languageGuid);
        string GetTimeZoneOpts(Guid programGuid);
    }
}
