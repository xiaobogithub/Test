using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ChangeTech.Models
{
    [DataContract]
    public class CTPPModel
    {
        [DataMember]
        public Guid CTPPGUID { get; set; }
        [DataMember]
        public Guid ProgramGUID { get; set; }
        [DataMember]
        public Guid LanguageGUID { get; set; }
        [DataMember]
        public string ProgramName { get; set; }
        [DataMember]
        public string ProgramURL { get; set; }
        [DataMember]
        public string ProgramSubheading { get; set; }
        [DataMember]
        public string ProgramDescription { get; set; }
        [DataMember]
        public string ProgramDescriptionTitle { get; set; }
        [DataMember]
        public string ProgramDescriptionForMobile { get; set; }
        [DataMember]
        public string ProgramDescriptionTitleForMobile { get; set; }
        [DataMember]
        public int IsFacebook { get; set; }
        [DataMember]
        public int IsTwitter { get; set; }
        [DataMember]
        public string LinkTechnology { get; set; }
        [DataMember]
        public ResourceModel PromotionField1 { get; set; }
        [DataMember]
        public string PromotionLink1 { get; set; }
        [DataMember]
        public ResourceModel PromotionField2 { get; set; }
        [DataMember]
        public string PromotionLink2 { get; set; }
        [DataMember]
        public Guid BrandGUID { get; set; }
        [DataMember]
        public ResourceModel ProgramPresenter { get; set; }
        [DataMember]
        public bool NeedPay { get; set; }
        [DataMember]
        public string Price { get; set; }

        [DataMember]
        public string BackDarkColor { get; set; }
        [DataMember]
        public string BackLightColor { get; set; }
        [DataMember]
        public string ProgramSubheadColor { get; set; }
        [DataMember]
        public int IsGooglePlus { get; set; }
        [DataMember]
        public ResourceModel CTPPLogo { get; set; }
        [DataMember]
        public int InActive { get; set; }

        [DataMember]
        public int RetakeEnable { get; set; }

        [DataMember]
        public string SubPriceLink { get; set; }
        [DataMember]
        public string ForSideLink { get; set; }
        [DataMember]
        public string VideoLink { get; set; }
        [DataMember]
        public ResourceModel ImageForVideoLink { get; set; }
        [DataMember]
        public string FacebookLink { get; set; }

        [DataMember]
        public string FactHeader1 { get; set; }
        [DataMember]
        public string FactContent1 { get; set; }
        [DataMember]
        public string FactHeader2 { get; set; }
        [DataMember]
        public string FactContent2 { get; set; }
        [DataMember]
        public string FactHeader3 { get; set; }
        [DataMember]
        public string FactContent3 { get; set; }
        [DataMember]
        public string FactHeader4 { get; set; }
        [DataMember]
        public string FactContent4 { get; set; }

        [DataMember]
        public bool? IsEnableSpecificReportAndHelpButtons { get; set; }
        [DataMember]
        public string ReportButtonHeading { get; set; }
        [DataMember]
        public string ReportButtonActual { get; set; }
        [DataMember]
        public string ReportButtonComplete { get; set; }
        [DataMember]
        public string ReportButtonUntaken { get; set; }
        [DataMember]
        public string HelpButtonHeading { get; set; }
        [DataMember]
        public string HelpButtonActual { get; set; }

        [DataMember]
        public Guid HelpButtonRelapseGuid { get; set; }
        [DataMember]
        public Guid HelpButtonRelapsePageSequenceGuid { get; set; }
        [DataMember]
        public string HelpButtonRelapsePageSequenceName { get; set; }

        [DataMember]
        public Guid ReportButtonRelapseGuid { get; set; }
        [DataMember]
        public Guid ReportButtonRelapsePageSequenceGuid { get; set; }
        [DataMember]
        public string ReportButtonRelapsePageSequenceName { get; set; }

        [DataMember]
        public int? ReportButtonAvailableTime { get; set; }

        [DataMember]
        public string RemindSMS1Text { get; set; }
        [DataMember]
        public int? RemindSMS1TimeMinute { get; set; } //transfer from datetime hh:mm to mm by (hh*60+mm)
        [DataMember]
        public string RemindSMS2Text { get; set; }
        [DataMember]
        public int? RemindSMS2TimeMinute { get; set; }  //transfer from datetime hh:mm to mm by (hh*60+mm)

        [DataMember]
        public ResourceModel MobileBookmarkLink { get; set; }
        [DataMember]
        public bool? IsNotShowOtherPrograms { get; set; }
        [DataMember]
        public bool? IsNotShowStartButton { get; set; }
        
    }
}
