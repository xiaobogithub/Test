using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;
using ChangeTech.Models;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;
using System.Data;
using Ethos.Utility;

namespace ChangeTech.Services
{
    public class CTPPService : ServiceBase, ICTPPService
    {
        public const string LINK_A_START = "<A";
        public const string LINK_A_END = "</A>";
        public const string ORIGINAL_IMAGE_DIRECTORY = "originalimagecontainer/";
        public const string LI_START = "<LI";
        public const string LI_END = "</LI>";
        public const string CLASS_VIDEO = "video";
        public const string CLASS_AUDIO = "audio";
        public const string CLASS_DOCUMENT = "document";
        public const string CLASS_IMAGE = "image";
        public const string REQUEST_RESOURCE = "RequestResource.aspx?target=";
        public const string MEDIA = "&media=";
        public Guid AddCTPPTemplate(CTPPModel Model)
        {
            CTPP CTPPEntity = new CTPP();
            CTPPEntity.Brand = Resolve<IBrandRepository>().GetBrandByGuid(Model.BrandGUID);
            CTPPEntity.CTPPGUID = Guid.NewGuid();
            CTPPEntity.IsFacebook = Model.IsFacebook;
            CTPPEntity.IsTwitter = Model.IsTwitter;
            CTPPEntity.IsNotShowOtherPrograms = Model.IsNotShowOtherPrograms;
            CTPPEntity.IsNotShowStartButton = Model.IsNotShowStartButton;
            CTPPEntity.LinkTechnology = Model.LinkTechnology;
            CTPPEntity.ProgramDescription = Model.ProgramDescription;
            CTPPEntity.Program = Resolve<IProgramRepository>().GetProgramByGuid(Model.ProgramGUID);
            CTPPEntity.ProgramName = Model.ProgramName;
            //CTPPEntity Model.ProgramPresenter;
            CTPPEntity.ProgramSubheading = Model.ProgramSubheading;
            CTPPEntity.ProgramURL = Model.ProgramURL;
            //Model.PromotionField1;
            //Model.PromotionField2;
            CTPPEntity.PromotionLink1 = Model.PromotionLink1;
            CTPPEntity.PromotionLink2 = Model.PromotionLink2;

            CTPPEntity.Fact1Header = Model.FactHeader1;
            CTPPEntity.Fact2Header = Model.FactHeader2;
            CTPPEntity.Fact3Header = Model.FactHeader3;
            CTPPEntity.Fact4Header = Model.FactHeader4;
            CTPPEntity.Fact1Content = Model.FactContent1;
            CTPPEntity.Fact2Content = Model.FactContent2;
            CTPPEntity.Fact3Content = Model.FactContent3;
            CTPPEntity.Fact4Content = Model.FactContent4;

            if (Model.PromotionField1 != null)
            {
                CTPPEntity.Resource = new Resource
                {
                    ResourceGUID = Model.PromotionField1.ID,
                    Name = Model.PromotionField1.Name,
                    Type = Model.PromotionField1.Type,
                    FileExtension = Model.PromotionField1.Extension,
                    NameOnServer = Model.PromotionField1.NameOnServer,
                    ResourceCategory = Resolve<IResourceCategoryRepository>().GetResourceCategory("Default"),
                };
            }
            if (Model.PromotionField2 != null)
            {
                CTPPEntity.Resource1 = new Resource
                {
                    ResourceGUID = Model.PromotionField2.ID,
                    Name = Model.PromotionField2.Name,
                    Type = Model.PromotionField2.Type,
                    FileExtension = Model.PromotionField2.Extension,
                    NameOnServer = Model.PromotionField2.NameOnServer,
                    ResourceCategory = Resolve<IResourceCategoryRepository>().GetResourceCategory("Default"),
                };

            }
            CTPPEntity.IsGooglePlus = Model.IsGooglePlus;
            CTPPEntity.IsInactive = Model.InActive;
            CTPPEntity.TopBackDarkColor = Model.BackDarkColor;
            CTPPEntity.TopBackLightColor = Model.BackLightColor;
            CTPPEntity.TopSubheadColor = Model.ProgramSubheadColor;
            CTPPEntity.ProgramDescriptionTitle = Model.ProgramDescriptionTitle;
            CTPPEntity.ProgramDescriptionForMobile = Model.ProgramDescriptionForMobile;
            CTPPEntity.ProgramDescriptionTitleForMobile = Model.ProgramDescriptionTitleForMobile;
            CTPPEntity.IsRetakeEnable = Model.RetakeEnable;
            if (Model.CTPPLogo != null)
            {
                CTPPEntity.Resource3 = new Resource
                {
                    ResourceGUID = Model.CTPPLogo.ID,
                    Name = Model.CTPPLogo.Name,
                    Type = Model.CTPPLogo.Type,
                    FileExtension = Model.CTPPLogo.Extension,
                    NameOnServer = Model.CTPPLogo.NameOnServer,
                    ResourceCategory = Resolve<IResourceCategoryRepository>().GetResourceCategory("Default"),
                };
            }
            if (Model.ImageForVideoLink != null)
            {
                CTPPEntity.Resource_1 = new Resource
                {
                    ResourceGUID = Model.ImageForVideoLink.ID,
                    Name = Model.ImageForVideoLink.Name,
                    Type = Model.ImageForVideoLink.Type,
                    FileExtension = Model.ImageForVideoLink.Extension,
                    NameOnServer = Model.ImageForVideoLink.NameOnServer,
                    ResourceCategory = Resolve<IResourceCategoryRepository>().GetResourceCategory("Default"),
                };
            }

            if (Model.MobileBookmarkLink != null)
            {
                CTPPEntity.Resource_MobileBookmark = new Resource
                {
                    ResourceGUID = Model.MobileBookmarkLink.ID,
                    Name = Model.MobileBookmarkLink.Name,
                    Type = Model.MobileBookmarkLink.Type,
                    FileExtension = Model.MobileBookmarkLink.Extension,
                    NameOnServer = Model.MobileBookmarkLink.NameOnServer,
                    ResourceCategory = Resolve<IResourceCategoryRepository>().GetResourceCategory("Default"),
                };
            }

            if (Model.HelpButtonRelapseGuid != null && Model.HelpButtonRelapseGuid != Guid.Empty)
            {
                Relapse relapseForHelpButtonEntiy = Resolve<IRelapseRepository>().Get(Model.HelpButtonRelapseGuid);
                CTPPEntity.Relapse = relapseForHelpButtonEntiy;
            }

            if (Model.ReportButtonRelapseGuid != null && Model.ReportButtonRelapseGuid != Guid.Empty)
            {
                Relapse relapseForHelpButtonEntiy = Resolve<IRelapseRepository>().Get(Model.ReportButtonRelapseGuid);
                CTPPEntity.Relapse1 = relapseForHelpButtonEntiy;
            }
            CTPPEntity.ReportButtonAvailableTime = Model.ReportButtonAvailableTime;

            CTPPEntity.ReportRemindSMS1Text = Model.RemindSMS1Text;
            CTPPEntity.ReportRemindSMS1Time = Model.RemindSMS1TimeMinute;
            CTPPEntity.ReportRemindSMS2Text = Model.RemindSMS2Text;
            CTPPEntity.ReportRemindSMS2Time = Model.RemindSMS2TimeMinute;

            Resolve<ICTPPRepository>().Insert(CTPPEntity);
            return CTPPEntity.CTPPGUID;
        }

        /// <remarks>This function does not include the update HelpButton and ReportButton Relapse. They have their own functions.</remarks>
        public void UpdateCTPP(CTPPModel Model, bool isSetPresenter)
        {
            CTPP entity = Resolve<ICTPPRepository>().GetCTPPByProgramGuid(Model.ProgramGUID);

            if (!isSetPresenter)
            {
                string PromotionPic1Extension = Model.PromotionField1.Extension;
                if (PromotionPic1Extension != "")
                {
                    entity.Resource = new Resource
                    {
                        ResourceGUID = Model.PromotionField1.ID,
                        Name = Model.PromotionField1.Name,
                        Type = Model.PromotionField1.Type,
                        FileExtension = Model.PromotionField1.Extension,
                        NameOnServer = Model.PromotionField1.NameOnServer,
                        ResourceCategory = Resolve<IResourceCategoryRepository>().GetResourceCategory("Default"),
                    };
                }
                string PromotionPic2Extension = Model.PromotionField2.Extension;
                if (PromotionPic2Extension != "")
                {
                    entity.Resource1 = new Resource
                    {
                        ResourceGUID = Model.PromotionField2.ID,
                        Name = Model.PromotionField2.Name,
                        Type = Model.PromotionField2.Type,
                        FileExtension = Model.PromotionField2.Extension,
                        NameOnServer = Model.PromotionField2.NameOnServer,
                        ResourceCategory = Resolve<IResourceCategoryRepository>().GetResourceCategory("Default"),
                    };
                }

                entity.Brand = Resolve<IBrandRepository>().GetBrandByGuid(Model.BrandGUID);
                if (entity.CTPPGUID != Model.CTPPGUID)
                {
                    entity.CTPPGUID = Model.CTPPGUID;
                }
                entity.IsFacebook = Model.IsFacebook;
                entity.IsTwitter = Model.IsTwitter;
                entity.LinkTechnology = Model.LinkTechnology;
                entity.ProgramDescription = Model.ProgramDescription;
                entity.ProgramDescriptionTitle = Model.ProgramDescriptionTitle;
                entity.ProgramDescriptionForMobile = Model.ProgramDescriptionForMobile;
                entity.ProgramDescriptionTitleForMobile = Model.ProgramDescriptionTitleForMobile;
                entity.Program = Resolve<IProgramRepository>().GetProgramByGuid(Model.ProgramGUID);
                entity.ProgramName = Model.ProgramName;
                entity.ProgramSubheading = Model.ProgramSubheading;
                entity.ProgramURL = Model.ProgramURL;
                entity.PromotionLink1 = Model.PromotionLink1;
                entity.PromotionLink2 = Model.PromotionLink2;
                entity.FacebookLink = Model.FacebookLink;
                entity.ForSideLink = Model.ForSideLink;
                entity.SubPriceLink = Model.SubPriceLink;
                entity.VideoLink = Model.VideoLink;

                entity.IsGooglePlus = Model.IsGooglePlus;
                entity.IsInactive = Model.InActive;
                entity.IsRetakeEnable = Model.RetakeEnable;
                entity.IsNotShowOtherPrograms = Model.IsNotShowOtherPrograms.HasValue?Model.IsNotShowOtherPrograms.Value:false;
                entity.IsNotShowStartButton = Model.IsNotShowStartButton.HasValue ? Model.IsNotShowStartButton.Value : false;
                entity.TopBackDarkColor = Model.BackDarkColor;
                entity.TopBackLightColor = Model.BackLightColor;
                entity.TopSubheadColor = Model.ProgramSubheadColor;

                entity.Fact1Header = Model.FactHeader1;
                entity.Fact2Header = Model.FactHeader2;
                entity.Fact3Header = Model.FactHeader3;
                entity.Fact4Header = Model.FactHeader4;
                entity.Fact1Content = Model.FactContent1;
                entity.Fact2Content = Model.FactContent2;
                entity.Fact3Content = Model.FactContent3;
                entity.Fact4Content = Model.FactContent4;

                //IsEnableSpecificReportAndHelpButtons
                entity.IsEnableSpecificReportAndHelpButtons = Model.IsEnableSpecificReportAndHelpButtons.HasValue ? Model.IsEnableSpecificReportAndHelpButtons.Value : false;
                entity.ReportButtonHeading = Model.ReportButtonHeading;
                entity.ReportButtonActual = Model.ReportButtonActual;
                entity.ReportButtonComplete = Model.ReportButtonComplete;
                entity.ReportButtonUntaken = Model.ReportButtonUntaken;
                entity.HelpButtonHeading = Model.HelpButtonHeading;
                entity.HelpButtonActual = Model.HelpButtonActual;
                entity.ReportButtonAvailableTime = Model.ReportButtonAvailableTime;

                entity.ReportRemindSMS1Text = Model.RemindSMS1Text;
                entity.ReportRemindSMS1Time = Model.RemindSMS1TimeMinute;
                entity.ReportRemindSMS2Text = Model.RemindSMS2Text;
                entity.ReportRemindSMS2Time = Model.RemindSMS2TimeMinute;

                string CTPPLogoExtension = Model.CTPPLogo.Extension;
                if (CTPPLogoExtension != "")
                {
                    entity.Resource3 = new Resource
                    {
                        ResourceGUID = Model.CTPPLogo.ID,
                        Name = Model.CTPPLogo.Name,
                        Type = Model.CTPPLogo.Type,
                        FileExtension = Model.CTPPLogo.Extension,
                        NameOnServer = Model.CTPPLogo.NameOnServer,
                        ResourceCategory = Resolve<IResourceCategoryRepository>().GetResourceCategory("Default"),
                    };
                }

                string ImageForVideoExtension = Model.ImageForVideoLink.Extension;
                if (ImageForVideoExtension != "")
                {
                    entity.Resource_1 = new Resource
                    {
                        ResourceGUID = Model.ImageForVideoLink.ID,
                        Name = Model.ImageForVideoLink.Name,
                        Type = Model.ImageForVideoLink.Type,
                        FileExtension = Model.ImageForVideoLink.Extension,
                        NameOnServer = Model.ImageForVideoLink.NameOnServer,
                        ResourceCategory = Resolve<IResourceCategoryRepository>().GetResourceCategory("Default"),
                    };
                }

                string MobileBookmarkExtension = Model.MobileBookmarkLink.Extension;
                if (MobileBookmarkExtension != "")
                {
                    entity.Resource_MobileBookmark = new Resource
                    {
                        ResourceGUID = Model.MobileBookmarkLink.ID,
                        Name = Model.MobileBookmarkLink.Name,
                        Type = Model.MobileBookmarkLink.Type,
                        FileExtension = Model.MobileBookmarkLink.Extension,
                        NameOnServer = Model.MobileBookmarkLink.NameOnServer,
                        ResourceCategory = Resolve<IResourceCategoryRepository>().GetResourceCategory("Default"),
                    };
                }

            }
            else//set presenter
            {
                string programPresenterExtension = Model.ProgramPresenter.Extension;
                {
                    if (programPresenterExtension != null)
                    {
                        entity.Resource2 = Resolve<IResourceRepository>().GetResource(Model.ProgramPresenter.ID);
                    }
                }
            }


            Resolve<ICTPPRepository>().Update(entity);
        }

        public CTPPModel GetCTPP(Guid programGUID)
        {
            CTPPModel cm = new CTPPModel();
            CTPP ctppEntity = Resolve<ICTPPRepository>().GetCTPPByProgramGuid(programGUID);

            cm.ProgramGUID = programGUID;
            if (ctppEntity != null)
            {
                if (!ctppEntity.BrandReference.IsLoaded)
                {
                    ctppEntity.BrandReference.Load();
                }
                if (ctppEntity.Brand != null)
                {
                    cm.BrandGUID = ctppEntity.Brand.BrandGUID;
                }
                if (!ctppEntity.ProgramReference.IsLoaded)
                {
                    ctppEntity.ProgramReference.Load();
                } 
                if (!ctppEntity.Program.LanguageReference.IsLoaded)
                {
                    ctppEntity.Program.LanguageReference.Load();
                }

                cm.CTPPGUID = ctppEntity.CTPPGUID;
                cm.LanguageGUID = ctppEntity.Program.Language.LanguageGUID;
                cm.NeedPay = ctppEntity.Program.IsWithPay.HasValue ? ctppEntity.Program.IsWithPay.Value : false;
                cm.Price = ctppEntity.Program.Price.HasValue ? ctppEntity.Program.Price.Value.ToString() : "";
                cm.IsFacebook = ctppEntity.IsFacebook;
                cm.IsTwitter = ctppEntity.IsTwitter;
                cm.IsNotShowOtherPrograms = ctppEntity.IsNotShowOtherPrograms.HasValue ? ctppEntity.IsNotShowOtherPrograms.Value : false;
                cm.IsNotShowStartButton = ctppEntity.IsNotShowStartButton.HasValue ? ctppEntity.IsNotShowStartButton.Value : false;
                cm.LinkTechnology = ctppEntity.LinkTechnology;
                cm.ProgramDescription = ctppEntity.ProgramDescription;
                cm.ProgramDescriptionTitle = ctppEntity.ProgramDescriptionTitle;
                cm.ProgramDescriptionForMobile = ctppEntity.ProgramDescriptionForMobile;
                cm.ProgramDescriptionTitleForMobile = ctppEntity.ProgramDescriptionTitleForMobile;
                cm.ProgramName = ctppEntity.ProgramName;
                cm.ProgramSubheading = ctppEntity.ProgramSubheading;
                cm.ProgramURL = ctppEntity.ProgramURL;
                cm.PromotionLink1 = ctppEntity.PromotionLink1;
                cm.PromotionLink2 = ctppEntity.PromotionLink2;
                cm.VideoLink = ctppEntity.VideoLink;
                cm.SubPriceLink = ctppEntity.SubPriceLink;
                cm.FacebookLink = ctppEntity.FacebookLink;
                cm.ForSideLink = ctppEntity.ForSideLink;

                cm.FactHeader1 = ctppEntity.Fact1Header;
                cm.FactHeader2 = ctppEntity.Fact2Header;
                cm.FactHeader3 = ctppEntity.Fact3Header;
                cm.FactHeader4 = ctppEntity.Fact4Header;
                cm.FactContent1 = ctppEntity.Fact1Content;
                cm.FactContent2 = ctppEntity.Fact2Content;
                cm.FactContent3 = ctppEntity.Fact3Content;
                cm.FactContent4 = ctppEntity.Fact4Content;

                cm.IsEnableSpecificReportAndHelpButtons = ctppEntity.IsEnableSpecificReportAndHelpButtons.HasValue ? ctppEntity.IsEnableSpecificReportAndHelpButtons.Value : false;
                cm.ReportButtonHeading = ctppEntity.ReportButtonHeading;
                cm.ReportButtonActual = ctppEntity.ReportButtonActual;
                cm.ReportButtonComplete = ctppEntity.ReportButtonComplete;
                cm.ReportButtonUntaken = ctppEntity.ReportButtonUntaken;
                cm.HelpButtonHeading = ctppEntity.HelpButtonHeading;
                cm.HelpButtonActual = ctppEntity.HelpButtonActual;

                if (!ctppEntity.ResourceReference.IsLoaded)
                {
                    ctppEntity.ResourceReference.Load();
                }
                if (ctppEntity.Resource != null)
                {
                    cm.PromotionField1 = new ResourceModel
                    {
                        Extension = ctppEntity.Resource.FileExtension,
                        ID = ctppEntity.Resource.ResourceGUID,
                        Name = ctppEntity.Resource.Name,
                        NameOnServer = ctppEntity.Resource.NameOnServer,
                        Type = ctppEntity.Resource.Type,
                    };
                }
                if (!ctppEntity.Resource1Reference.IsLoaded)
                {
                    ctppEntity.Resource1Reference.Load();
                }
                if (ctppEntity.Resource1 != null)
                {
                    cm.PromotionField2 = new ResourceModel
                    {
                        Extension = ctppEntity.Resource1.FileExtension,
                        ID = ctppEntity.Resource1.ResourceGUID,
                        Name = ctppEntity.Resource1.Name,
                        NameOnServer = ctppEntity.Resource1.NameOnServer,
                        Type = ctppEntity.Resource1.Type,

                    };
                }

                if (!ctppEntity.Resource2Reference.IsLoaded)
                {
                    ctppEntity.Resource2Reference.Load();
                }
                if (ctppEntity.Resource2 != null)
                {
                    cm.ProgramPresenter = new ResourceModel
                    {
                        Extension = ctppEntity.Resource2.FileExtension,
                        ID = ctppEntity.Resource2.ResourceGUID,
                        Name = ctppEntity.Resource2.Name,
                        NameOnServer = ctppEntity.Resource2.NameOnServer,
                        Type = ctppEntity.Resource2.Type,
                    };

                }

                cm.IsGooglePlus = ctppEntity.IsGooglePlus;
                cm.InActive = ctppEntity.IsInactive;
                cm.RetakeEnable = ctppEntity.IsRetakeEnable;
                cm.BackDarkColor = ctppEntity.TopBackDarkColor;
                cm.BackLightColor = ctppEntity.TopBackLightColor;
                cm.ProgramSubheadColor = ctppEntity.TopSubheadColor;
                if (!ctppEntity.Resource3Reference.IsLoaded)
                {
                    ctppEntity.Resource3Reference.Load();
                }
                if (ctppEntity.Resource3 != null)
                {
                    cm.CTPPLogo = new ResourceModel
                    {
                        Extension = ctppEntity.Resource3.FileExtension,
                        ID = ctppEntity.Resource3.ResourceGUID,
                        Name = ctppEntity.Resource3.Name,
                        NameOnServer = ctppEntity.Resource3.NameOnServer,
                        Type = ctppEntity.Resource3.Type,
                    };
                }
                if (!ctppEntity.Resource_1Reference.IsLoaded)
                {
                    ctppEntity.Resource_1Reference.Load();
                }
                if (ctppEntity.Resource_1 != null)
                {
                    cm.ImageForVideoLink = new ResourceModel
                    {
                        Extension = ctppEntity.Resource_1.FileExtension,
                        ID = ctppEntity.Resource_1.ResourceGUID,
                        Name = ctppEntity.Resource_1.Name,
                        NameOnServer = ctppEntity.Resource_1.NameOnServer,
                        Type = ctppEntity.Resource_1.Type,
                    };
                }
                else
                {
                    cm.ImageForVideoLink = null;
                }

                if (!ctppEntity.Resource_MobileBookmarkReference.IsLoaded)
                {
                    ctppEntity.Resource_MobileBookmarkReference.Load();
                }
                if (ctppEntity.Resource_MobileBookmark != null)
                {
                    cm.MobileBookmarkLink = new ResourceModel
                    {
                        Extension = ctppEntity.Resource_MobileBookmark.FileExtension,
                        ID = ctppEntity.Resource_MobileBookmark.ResourceGUID,
                        Name = ctppEntity.Resource_MobileBookmark.Name,
                        NameOnServer = ctppEntity.Resource_MobileBookmark.NameOnServer,
                        Type = ctppEntity.Resource_MobileBookmark.Type,
                    };
                }
                else
                {
                    cm.MobileBookmarkLink = null;
                }
                
                //For CTPP help button Relapse
                if (!ctppEntity.RelapseReference.IsLoaded) ctppEntity.RelapseReference.Load();
                if (ctppEntity.Relapse != null)
                {
                    if (!ctppEntity.Relapse.PageSequenceReference.IsLoaded) ctppEntity.Relapse.PageSequenceReference.Load();

                    cm.HelpButtonRelapseGuid = ctppEntity.Relapse.RelapseGUID;
                    if (ctppEntity.Relapse.PageSequence != null)
                    {
                        cm.HelpButtonRelapsePageSequenceGuid = ctppEntity.Relapse.PageSequence.PageSequenceGUID;
                        cm.HelpButtonRelapsePageSequenceName = ctppEntity.Relapse.PageSequence.Name;
                    }
                }

                //For CTPP report button Relapse
                if (!ctppEntity.Relapse1Reference.IsLoaded) ctppEntity.Relapse1Reference.Load();
                if (ctppEntity.Relapse1 != null)
                {
                    if (!ctppEntity.Relapse1.PageSequenceReference.IsLoaded) ctppEntity.Relapse1.PageSequenceReference.Load();

                    cm.ReportButtonRelapseGuid = ctppEntity.Relapse1.RelapseGUID;
                    if (ctppEntity.Relapse1.PageSequence != null)
                    {
                        cm.ReportButtonRelapsePageSequenceGuid = ctppEntity.Relapse1.PageSequence.PageSequenceGUID;
                        cm.ReportButtonRelapsePageSequenceName = ctppEntity.Relapse1.PageSequence.Name;
                    }
                }
                cm.ReportButtonAvailableTime = ctppEntity.ReportButtonAvailableTime;

                cm.RemindSMS1Text = ctppEntity.ReportRemindSMS1Text;
                cm.RemindSMS1TimeMinute = ctppEntity.ReportRemindSMS1Time;
                cm.RemindSMS2Text = ctppEntity.ReportRemindSMS2Text;
                cm.RemindSMS2TimeMinute = ctppEntity.ReportRemindSMS2Time;
            }
            else
            {
                cm = null;
            }
            return cm;
        }

        public CTPPModel GetCTPPByBrandAndProgram(string brandUrl, string programUrl)
        {
            CTPPModel cm = new CTPPModel();
            CTPP ctppEntity = Resolve<ICTPPRepository>().GetCTPPByBrandAndProgram(brandUrl, programUrl);


            if (ctppEntity != null)
            {
                if (!ctppEntity.ProgramReference.IsLoaded)
                {
                    ctppEntity.ProgramReference.Load();
                }
                if (!ctppEntity.Program.LanguageReference.IsLoaded)
                {
                    ctppEntity.Program.LanguageReference.Load();
                }

                cm.ProgramGUID = ctppEntity.Program.ProgramGUID;
                cm.LanguageGUID = ctppEntity.Program.Language.LanguageGUID;
                cm.NeedPay = ctppEntity.Program.IsWithPay.HasValue ? ctppEntity.Program.IsWithPay.Value : false;
                cm.Price = ctppEntity.Program.Price.HasValue ? ctppEntity.Program.Price.Value.ToString() : "";
                if (!ctppEntity.BrandReference.IsLoaded)
                {
                    ctppEntity.BrandReference.Load();
                }
                if (ctppEntity.Brand != null)
                {
                    cm.BrandGUID = ctppEntity.Brand.BrandGUID;
                }
                cm.CTPPGUID = ctppEntity.CTPPGUID;
                cm.IsFacebook = ctppEntity.IsFacebook;
                cm.IsTwitter = ctppEntity.IsTwitter;
                cm.IsNotShowOtherPrograms = ctppEntity.IsNotShowOtherPrograms.HasValue?ctppEntity.IsNotShowOtherPrograms.Value:false;
                cm.IsNotShowStartButton = ctppEntity.IsNotShowStartButton.HasValue ? ctppEntity.IsNotShowStartButton.Value : false;
                cm.LinkTechnology = ctppEntity.LinkTechnology;
                cm.ProgramDescription = ctppEntity.ProgramDescription;
                cm.ProgramDescriptionTitle = ctppEntity.ProgramDescriptionTitle;
                cm.ProgramDescriptionForMobile = ctppEntity.ProgramDescriptionForMobile;
                cm.ProgramDescriptionTitleForMobile = ctppEntity.ProgramDescriptionTitleForMobile;
                cm.ProgramName = ctppEntity.ProgramName;
                cm.ProgramSubheading = ctppEntity.ProgramSubheading;
                cm.ProgramURL = ctppEntity.ProgramURL;
                cm.PromotionLink1 = ctppEntity.PromotionLink1;
                cm.PromotionLink2 = ctppEntity.PromotionLink2;
                cm.VideoLink = ctppEntity.VideoLink;
                cm.SubPriceLink = ctppEntity.SubPriceLink;
                cm.FacebookLink = ctppEntity.FacebookLink;
                cm.ForSideLink = ctppEntity.ForSideLink;

                cm.FactHeader1 = ctppEntity.Fact1Header;
                cm.FactHeader2 = ctppEntity.Fact2Header;
                cm.FactHeader3 = ctppEntity.Fact3Header;
                cm.FactHeader4 = ctppEntity.Fact4Header;
                cm.FactContent1 = ctppEntity.Fact1Content;
                cm.FactContent2 = ctppEntity.Fact2Content;
                cm.FactContent3 = ctppEntity.Fact3Content;
                cm.FactContent4 = ctppEntity.Fact4Content;

                cm.IsEnableSpecificReportAndHelpButtons = ctppEntity.IsEnableSpecificReportAndHelpButtons.HasValue ? ctppEntity.IsEnableSpecificReportAndHelpButtons.Value : false;
                cm.ReportButtonHeading = ctppEntity.ReportButtonHeading;
                cm.ReportButtonActual = ctppEntity.ReportButtonActual;
                cm.ReportButtonComplete = ctppEntity.ReportButtonComplete;
                cm.ReportButtonUntaken = ctppEntity.ReportButtonUntaken;
                cm.HelpButtonHeading = ctppEntity.HelpButtonHeading;
                cm.HelpButtonActual = ctppEntity.HelpButtonActual;

                if (!ctppEntity.ResourceReference.IsLoaded)
                {
                    ctppEntity.ResourceReference.Load();
                }
                if (ctppEntity.Resource != null)
                {
                    cm.PromotionField1 = new ResourceModel
                    {
                        Extension = ctppEntity.Resource.FileExtension,
                        ID = ctppEntity.Resource.ResourceGUID,
                        Name = ctppEntity.Resource.Name,
                        NameOnServer = ctppEntity.Resource.NameOnServer,
                        Type = ctppEntity.Resource.Type,
                    };
                }
                if (!ctppEntity.Resource1Reference.IsLoaded)
                {
                    ctppEntity.Resource1Reference.Load();
                }
                if (ctppEntity.Resource1 != null)
                {
                    cm.PromotionField2 = new ResourceModel
                    {
                        Extension = ctppEntity.Resource1.FileExtension,
                        ID = ctppEntity.Resource1.ResourceGUID,
                        Name = ctppEntity.Resource1.Name,
                        NameOnServer = ctppEntity.Resource1.NameOnServer,
                        Type = ctppEntity.Resource1.Type,
                    };
                }
                if (!ctppEntity.Resource2Reference.IsLoaded)
                {
                    ctppEntity.Resource2Reference.Load();
                }
                if (ctppEntity.Resource2 != null)
                {
                    cm.ProgramPresenter = new ResourceModel
                    {
                        Extension = ctppEntity.Resource2.FileExtension,
                        ID = ctppEntity.Resource2.ResourceGUID,
                        Name = ctppEntity.Resource2.Name,
                        NameOnServer = ctppEntity.Resource2.NameOnServer,
                        Type = ctppEntity.Resource2.Type,
                    };
                }


                cm.IsGooglePlus = ctppEntity.IsGooglePlus;
                cm.InActive = ctppEntity.IsInactive;
                cm.RetakeEnable = ctppEntity.IsRetakeEnable;
                cm.BackDarkColor = ctppEntity.TopBackDarkColor;
                cm.BackLightColor = ctppEntity.TopBackLightColor;
                cm.ProgramSubheadColor = ctppEntity.TopSubheadColor;
                if (!ctppEntity.Resource3Reference.IsLoaded)
                {
                    ctppEntity.Resource3Reference.Load();
                }
                if (ctppEntity.Resource3 != null)
                {
                    cm.CTPPLogo = new ResourceModel
                    {
                        Extension = ctppEntity.Resource3.FileExtension,
                        ID = ctppEntity.Resource3.ResourceGUID,
                        Name = ctppEntity.Resource3.Name,
                        NameOnServer = ctppEntity.Resource3.NameOnServer,
                        Type = ctppEntity.Resource3.Type,
                    };
                }
                if (!ctppEntity.Resource_1Reference.IsLoaded)
                {
                    ctppEntity.Resource_1Reference.Load();
                }
                if (ctppEntity.Resource_1 != null)
                {
                    cm.ImageForVideoLink = new ResourceModel
                    {
                        Extension = ctppEntity.Resource_1.FileExtension,
                        ID = ctppEntity.Resource_1.ResourceGUID,
                        Name = ctppEntity.Resource_1.Name,
                        NameOnServer = ctppEntity.Resource_1.NameOnServer,
                        Type = ctppEntity.Resource_1.Type,
                    };
                }
                else
                {
                    cm.ImageForVideoLink = null;
                }

                if (!ctppEntity.Resource_MobileBookmarkReference.IsLoaded)
                {
                    ctppEntity.Resource_MobileBookmarkReference.Load();
                }
                if (ctppEntity.Resource_MobileBookmark != null)
                {
                    cm.MobileBookmarkLink = new ResourceModel
                    {
                        Extension = ctppEntity.Resource_MobileBookmark.FileExtension,
                        ID = ctppEntity.Resource_MobileBookmark.ResourceGUID,
                        Name = ctppEntity.Resource_MobileBookmark.Name,
                        NameOnServer = ctppEntity.Resource_MobileBookmark.NameOnServer,
                        Type = ctppEntity.Resource_MobileBookmark.Type,
                    };
                }
                else
                {
                    cm.MobileBookmarkLink = null;
                }
                //For CTPP help button Relapse
                if (!ctppEntity.RelapseReference.IsLoaded) ctppEntity.RelapseReference.Load();
                if (ctppEntity.Relapse != null)
                {
                    if (!ctppEntity.Relapse.PageSequenceReference.IsLoaded) ctppEntity.Relapse.PageSequenceReference.Load();
                    cm.HelpButtonRelapseGuid = ctppEntity.Relapse.RelapseGUID;
                    if (ctppEntity.Relapse.PageSequence != null)
                    {
                        cm.HelpButtonRelapsePageSequenceGuid = ctppEntity.Relapse.PageSequence.PageSequenceGUID;
                        cm.HelpButtonRelapsePageSequenceName = ctppEntity.Relapse.PageSequence.Name;
                    }
                }

                //For CTPP report button Relapse
                if (!ctppEntity.Relapse1Reference.IsLoaded) ctppEntity.Relapse1Reference.Load();
                if (ctppEntity.Relapse1 != null)
                {
                    if (!ctppEntity.Relapse1.PageSequenceReference.IsLoaded) ctppEntity.Relapse1.PageSequenceReference.Load();

                    cm.ReportButtonRelapseGuid = ctppEntity.Relapse1.RelapseGUID;
                    if (ctppEntity.Relapse1.PageSequence != null)
                    {
                        cm.ReportButtonRelapsePageSequenceGuid = ctppEntity.Relapse1.PageSequence.PageSequenceGUID;
                        cm.ReportButtonRelapsePageSequenceName = ctppEntity.Relapse1.PageSequence.Name;
                    }
                }
                cm.ReportButtonAvailableTime = ctppEntity.ReportButtonAvailableTime;

                cm.RemindSMS1Text = ctppEntity.ReportRemindSMS1Text;
                cm.RemindSMS1TimeMinute = ctppEntity.ReportRemindSMS1Time;
                cm.RemindSMS2Text = ctppEntity.ReportRemindSMS2Text;
                cm.RemindSMS2TimeMinute = ctppEntity.ReportRemindSMS2Time;
            }
            else
            {
                cm = null;
            }
            return cm;
        }


        /// <remarks>This function is just for the promotion bar link in ctpp page where there are other ctpps in the same brand</remarks>
        public List<CTPPModel> GetCTPPInBrandNotThisProgram(string brandName, string programName)
        {
            List<CTPPModel> cm = new List<CTPPModel>();
            List<CTPP> ctppEntities = Resolve<ICTPPRepository>().GetCTPPInBrandNotThisProgram(brandName, programName);

            foreach (CTPP ctpp in ctppEntities)
            {
                CTPPModel cModel = new CTPPModel();

                if (!ctpp.BrandReference.IsLoaded) ctpp.BrandReference.Load();
                if (string.IsNullOrEmpty(ctpp.ProgramURL) || ctpp.CTPPGUID == Guid.Empty || ctpp.Brand == null || ctpp.Brand.BrandGUID == Guid.Empty)
                {
                    continue;
                }
                if (!ctpp.ProgramReference.IsLoaded)
                {
                    ctpp.ProgramReference.Load();
                }
                if (ctpp.Program != null)
                {
                    cModel.ProgramGUID = ctpp.Program.ProgramGUID;
                    Program thisProgram = Resolve<IProgramRepository>().GetProgramByGuid(ctpp.Program.ProgramGUID);
                    if (thisProgram == null || ((bool)thisProgram.IsDeleted.HasValue && (bool)thisProgram.IsDeleted))
                    {
                        continue;
                    }
                }
                cModel.ProgramDescription = ctpp.ProgramDescription;
                cModel.ProgramDescriptionTitle = ctpp.ProgramDescriptionTitle;
                cModel.ProgramDescriptionForMobile = ctpp.ProgramDescriptionForMobile;
                cModel.ProgramDescriptionTitleForMobile = ctpp.ProgramDescriptionTitleForMobile;
                cModel.ProgramName = ctpp.ProgramName;
                cModel.ProgramSubheading = ctpp.ProgramSubheading;

                cm.Add(cModel);
            }

            return cm;
        }

        public List<string> GetSessionResource(Guid sessionGUID)
        {
            List<string> SessionResource = new List<string>();
            try
            {
                Dictionary<string, string> resourceDic = Resolve<IPageService>().GetResourcesBySessionGuid(sessionGUID);

                foreach (KeyValuePair<string, string> resource in resourceDic)
                {
                    string link = resource.Key;

                    string resourceType = resource.Value == string.Empty ? string.Empty : resource.Value.ToLower();

                    string className = "";
                    if (resourceType == ResourceTypeEnum.Image.ToString().ToLower())
                        className = CLASS_IMAGE;
                    else if (resourceType == ResourceTypeEnum.Document.ToString().ToLower())
                        className = CLASS_DOCUMENT;
                    else if (resourceType == ResourceTypeEnum.Video.ToString().ToLower())
                        className = CLASS_VIDEO;
                    else if (resourceType == ResourceTypeEnum.Audio.ToString().ToLower())
                        className = CLASS_AUDIO;

                    int indexOfResourceName = link.IndexOf('>') + 1;
                    int indexOfResourceNameEnd = (link.ToLower()).IndexOf(LINK_A_END.ToLower()) - 1;
                    string resourceName = link.Substring(indexOfResourceName, indexOfResourceNameEnd - indexOfResourceName + 1);
                    int indexOfResourceNameExtension = resourceName.LastIndexOf('.');
                    if (indexOfResourceNameExtension > -1)
                    {
                        resourceName = resourceName.Remove(indexOfResourceNameExtension);
                    }
                    link = link.Remove(indexOfResourceName) + resourceName + LINK_A_END;
                    link = LI_START + " class=\"{0}\" >" + link + LI_END;
                    link = string.Format(link, className);

                    // so far not include image resource
                    if (!SessionResource.Contains(link) && className != CLASS_IMAGE)
                        SessionResource.Add(link);
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return SessionResource;
        }

        #region the services provider for ctpp
        public List<string> GetSessionResource(Guid sessionGUID, string serverPath, List<CTPPSessionPageBodyModel> sPageBodyList, List<CTPPSessionPageMediaResourceModel> sPageMediaResourceList)
        {
            List<string> SessionResource = new List<string>();
            try
            {
                Dictionary<string, string> resourceDic = Resolve<IPageService>().GetResourcesBySessionGuid(sessionGUID, serverPath, sPageBodyList, sPageMediaResourceList);

                foreach (KeyValuePair<string, string> resource in resourceDic)
                {
                    string link = resource.Key;

                    string resourceType = resource.Value == string.Empty ? string.Empty : resource.Value.ToLower();

                    string className = "";
                    if (resourceType == ResourceTypeEnum.Image.ToString().ToLower())
                        className = CLASS_IMAGE;
                    else if (resourceType == ResourceTypeEnum.Document.ToString().ToLower())
                        className = CLASS_DOCUMENT;
                    else if (resourceType == ResourceTypeEnum.Video.ToString().ToLower())
                        className = CLASS_VIDEO;
                    else if (resourceType == ResourceTypeEnum.Audio.ToString().ToLower())
                        className = CLASS_AUDIO;

                    int indexOfResourceName = link.IndexOf('>') + 1;
                    int indexOfResourceNameEnd = (link.ToLower()).IndexOf(LINK_A_END.ToLower()) - 1;
                    string resourceName = link.Substring(indexOfResourceName, indexOfResourceNameEnd - indexOfResourceName + 1);
                    int indexOfResourceNameExtension = resourceName.LastIndexOf('.');
                    if (indexOfResourceNameExtension > -1)
                    {
                        resourceName = resourceName.Remove(indexOfResourceNameExtension);
                    }
                    link = link.Remove(indexOfResourceName) + resourceName + LINK_A_END;
                    link = LI_START + " class=\"{0}\" >" + link + LI_END;
                    link = string.Format(link, className);

                    // so far not include image resource
                    if (!SessionResource.Contains(link) && className != CLASS_IMAGE)
                        SessionResource.Add(link);
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return SessionResource;
        }
        #endregion
        //public List<string> GetSessionResource(Guid sessionGUID)
        //{
        //    List<string> SessionResource = new List<string>();

        //    List<PageResource> downloadResourceList = new List<PageResource>();
        //    downloadResourceList = Resolve<IPageResourceRepository>().GetPageResourcesBySessionGuid(sessionGUID);
        //    for (int i = 0; i < downloadResourceList.Count; i++)
        //    {
        //        string link = downloadResourceList[i].MediaLinkATag;

        //        string resourceType = downloadResourceList[i].MediaType == string.Empty ? string.Empty : downloadResourceList[i].MediaType.ToLower();

        //        string className = "";
        //        if (resourceType == ResourceTypeEnum.Image.ToString().ToLower())
        //            className = CLASS_IMAGE;
        //        else if (resourceType == ResourceTypeEnum.Document.ToString().ToLower())
        //            className = CLASS_DOCUMENT;
        //        else if (resourceType == ResourceTypeEnum.Video.ToString().ToLower())
        //            className = CLASS_VIDEO;
        //        else if (resourceType == ResourceTypeEnum.Audio.ToString().ToLower())
        //            className = CLASS_AUDIO;

        //        int indexOfResourceName = link.IndexOf('>') + 1;
        //        int indexOfResourceNameEnd = (link.ToLower()).IndexOf(LINK_A_END.ToLower()) - 1;
        //        string resourceName = link.Substring(indexOfResourceName, indexOfResourceNameEnd - indexOfResourceName + 1);
        //        int indexOfResourceNameExtension = resourceName.LastIndexOf('.');
        //        if (indexOfResourceNameExtension > -1)
        //        {
        //            resourceName = resourceName.Remove(indexOfResourceNameExtension);
        //        }
        //        link = link.Remove(indexOfResourceName) + resourceName + LINK_A_END;
        //        link = LI_START + " class=\"{0}\" >" + link + LI_END;
        //        link = string.Format(link, className);

        //        // so far not include image resource
        //        if (!SessionResource.Contains(link) && className != CLASS_IMAGE)
        //            SessionResource.Add(link);
        //    }

        //    return SessionResource;
        //}

        //TODO:
        //cm.VideoLink = ctppEntity.VideoLink;
        //cm.SubPriceLink = ctppEntity.SubPriceLink;
        //cm.FacebookLink = ctppEntity.FacebookLink;
        //cm.ForSideLink = ctppEntity.ForSideLink;

        public string GetCTPPModelAsXML(Guid programGuid)
        {
            return Resolve<IStoreProcedure>().GetCTPPModelAsXML(programGuid);
        }

        public string GetCTPPModelAsXMLByProgramGuidAndUserGuid(Guid programGuid, Guid userGuid)
        {
            return Resolve<IStoreProcedure>().GetCTPPModelAsXMLByProgramGuidAndUserGuid(programGuid, userGuid);
        }

        public bool IsExistCTPPWithBrandUrlAndProgramUrl(string brandUrl, string programUrl)
        {
            bool isExist = false;

            if (Resolve<ICTPPRepository>().GetCTPPByBrandAndProgram(brandUrl, programUrl) != null)
            {
                isExist = true;
            }

            return isExist;
        }

        public bool BindCTPPRelapse(Guid CTPPGuid, Guid RelapseGuid, CTPPRelapseEnum relapseType)
        {
            bool flag = false;
            try
            {
                CTPP ctppEntity = Resolve<ICTPPRepository>().Get(CTPPGuid);
                if (ctppEntity != null)
                {
                    Relapse relapseEntity = Resolve<IRelapseRepository>().Get(RelapseGuid);
                    switch (relapseType)
                    {
                        case CTPPRelapseEnum.HelpButtonRelapse:
                            if (!ctppEntity.RelapseReference.IsLoaded) ctppEntity.RelapseReference.Load();
                            ctppEntity.Relapse = relapseEntity;//.Relapse: this foreign key is bind to HelpButtonRelapseGUID 
                            Resolve<ICTPPRepository>().Update(ctppEntity);
                            flag = true;
                            break;
                        case CTPPRelapseEnum.ReportButtonRelapse:
                            if (!ctppEntity.Relapse1Reference.IsLoaded) ctppEntity.Relapse1Reference.Load();
                            ctppEntity.Relapse1 = relapseEntity;//.Relapse: this foreign key is bind to ReportButtonRelapseGUID 
                            Resolve<ICTPPRepository>().Update(ctppEntity);
                            flag = true;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("{0}::{1}", System.Reflection.MethodBase.GetCurrentMethod().Name, DateTime.UtcNow));
            }
            return flag;
        }

        public bool UnBindCTPPRelapse(Guid CTPPGuid, CTPPRelapseEnum relapseType)
        {
            bool flag = false;
            try
            {
                CTPP ctppEntity = Resolve<ICTPPRepository>().Get(CTPPGuid);
                if (ctppEntity != null)
                {
                    switch (relapseType)
                    {
                        case CTPPRelapseEnum.HelpButtonRelapse:
                            if (!ctppEntity.RelapseReference.IsLoaded) ctppEntity.RelapseReference.Load();
                            ctppEntity.Relapse = null;// this foreign key is bind to HelpButtonRelapseGUID
                            Resolve<ICTPPRepository>().Update(ctppEntity);
                            flag = true;
                            break;
                        case CTPPRelapseEnum.ReportButtonRelapse:
                            if (!ctppEntity.Relapse1Reference.IsLoaded) ctppEntity.Relapse1Reference.Load();
                            ctppEntity.Relapse1 = null;// this foreign key is bind to ReportButtonRelapseGUID
                            Resolve<ICTPPRepository>().Update(ctppEntity);
                            flag = true;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("{0}::{1}", System.Reflection.MethodBase.GetCurrentMethod().Name, DateTime.UtcNow));
            }
            return flag;
        }
    }
}
