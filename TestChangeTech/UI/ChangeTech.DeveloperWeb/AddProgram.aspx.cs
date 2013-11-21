using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ChangeTech.Models;
using ChangeTech.Contracts;
using Ethos.DependencyInjection;

using System.IO;
using Ethos.Utility;

namespace ChangeTech.DeveloperWeb
{
    public partial class AddProgram : PageBase<ProgramModel>
    {
        private string ProgramPage
        {
            get
            {
                return Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format("ListProgram.aspx?Page={0}", ProgramPage);
                    LanguagesModel languages = Resolve<ILanguageService>().GetLanguagesModel();
                    //defaultLanguageDropdownlist.DataSource = languages;
                    //defaultLanguageDropdownlist.DataBind();
                }
                catch (Exception ex)
                {
                    LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                    throw ex;
                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ProgramModel programModel = new ProgramModel();

            try
            {
                if (fileUpload.HasFile)
                {
                    Guid fileGuid = Guid.NewGuid();
                    string fileType = fileUpload.FileName.Substring(fileUpload.FileName.LastIndexOf("."));
                    string fileName = fileGuid.ToString() + "." + fileType;

                    //fileUpload.SaveAs(Server.MapPath("ClientBin/ProgramImages/Logo/" + fileGuid + fileType));
                    // 2010-03-19: Chen Pu Replace with save to azure method instead of save to file system
                    Resolve<IResourceService>().SaveResourceToAzureBlobStorage(fileUpload.FileContent, fileGuid.ToString(), fileUpload.FileName, ResourceTypeEnum.Logo.ToString(), Guid.Empty);

                    programModel.ProgramLogo = new ResourceModel
                    {
                        Extension = fileType,
                        ID = fileGuid,
                        Name = fileGuid + fileType,
                        NameOnServer = fileGuid + fileType,
                        Type = "Logo",
                    };
                }

                // for support languages
                programModel.languages = new List<ProgramLanguageModel>();
                programModel.languages.Add(new ProgramLanguageModel
                {
                    language = new LanguageModel
                    {
                        LanguageGUID = new Guid("fb5ae1dc-4caf-4613-9739-7397429ddf25")
                    }
                });

                if (!string.IsNullOrEmpty(txtProgramName.Text))
                {
                    programModel.ProgramName = txtProgramName.Text;
                }

                if (!string.IsNullOrEmpty(txtDescription.Text))
                {
                    programModel.Description = txtDescription.Text;
                }

                //if (programModel != null)
                //{
                programModel.DefaultLanguage = new Guid("fb5ae1dc-4caf-4613-9739-7397429ddf25");
                Resolve<IProgramService>().AddProgram(programModel, ContextService.CurrentAccount.UserGuid);
                ContextService.CurrentAccount.ProgramSecuirty = Resolve<IProgramService>().GetProgramPermissionByUserGuid(ContextService.CurrentAccount.UserGuid);
                Response.Redirect(string.Format("ListProgram.aspx?Page={0}", ProgramPage));
                //}
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListProgram.aspx");
        }
        
    }
}
