using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ChangeTech.Models
{
    public class SessionModel
    {
        public Guid ID { get; set; }
        public int Day { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<PageSequenceModel> PageSequences { get; set; }
        public int PageSequenceNumber { get; set; }
        public UserModel LastUpdateBy { get; set; }
        public bool IsNeedReportButton { get; set; }
        public bool IsNeedHelpButton { get; set; }
    }

    public class EditSessionModel
    {
        public Guid ID { get; set; }
        public int Day { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<PageSequenceModel> PageSequences { get; set; }
        public string ProgramName { get; set; }
        public Guid ProgramGuid { get; set; }
        public Guid ProgramStatusGuid { get; set; }
        public bool IsLiveProgram { get; set; }
        public bool IsNeedReportButton { get; set; }
        public bool IsNeedHelpButton { get; set; }
    }

    public class SimpleSessionModel
    {
        public Guid ID { get; set; }
        public String Name { get; set; }
        public string dayNum { get; set; }
        public string NameWithDayNum
        {
            get
            {
                return (Name == null ? "" : Name) + " -- Day" + (dayNum == null ? "Null" : dayNum);
            }
        }
    }

    [DataContract]
    public class SessionReportModel
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Order { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public List<PageSequenceReportModel> PageSequences { get; set; }
    }




    public class CTPPEndUserPageModel
    {
        public string SpeSLogin { get; set; }
        public string SpeSLogout { get; set; }
        public string SpeSDays_in_program { get; set; }
        public string SpeSClick_a_day { get; set; }
        public string SpeSStart_day { get; set; }
        public string SpeSOther_programs_from { get; set; }
        public string SpeSCompleted { get; set; }
        public string SpeSPrice { get; set; }
        public string SpeSPrice_subtext { get; set; }
        public string SpeSBuy { get; set; }
        public string SpeSBuy_subtext { get; set; }
        public string SpeSFree { get; set; }

        public string SpeSUnavailable { get; set; }
        public string SpeSUntaken { get; set; }
        public string SpeSProvided_by { get; set; }
        public string SpeSUseFrom { get; set; }
        public string SpeSAllRightsReserved { get; set; }

        public string SpeSVideoSubtext1 { get; set; }
        public string SpeSVideoSubtext2 { get; set; }

        public string SpeSReadyToGo { get; set; }
        public string SpeSRetake { get; set; }
        public string SpeSMoreBeforeFB { get; set; }

        public string SpeSReportButtonHeading { get; set; }
        public string SpeSReportButtonActual { get; set; }
        public string SpeSReportButtonComplete { get; set; }
        public string SpeSReportButtonUntaken { get; set; }
        public string SpeSHelpButtonHeading { get; set; }
        public string SpeSHelpButtonActual { get; set; }

        public string SpeSContainerHomescreenHeading { get; set; }
        public string SpeSContainerHomescreenText { get; set; }
        public string SpeSContainerBelowHelpbuttonText { get; set; }

        public string orderUrl { get; set; }

        public List<CTPPSessionModel> endUserSessionModel { get; set; }

    }
    public class CTPPSessionModel
    {
        public Guid ID { get; set; }
        public int Day { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int IsHasDone { get; set; }//0:no;1:Done;2:start
        
        public string VideoURL { get; set; }
        public string SoundURL { get; set; }
        public string PDFURL { get; set; }

        public string DIVSTRStart { get; set; }
        public string DIVSTREnd { get; set; }
    }

    //the services provider for ctpp
    public class CTPPSessionPageBodyModel
    {
        public Guid SessionGUID { get; set; }
        public string PageBody { get; set; }
    }

    public class CTPPSessionPageMediaResourceModel
    {
        public Guid SessionGUID { get; set; }
        public Guid MediaGUID { get; set; }
        public string Name { get; set; }
        public string NameOnServer { get; set; }
        public string Type { get; set; }
    }
}
