using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Browser;
using ChangeTech.Silverlight.Common;

namespace ChangeTech.Silverlight.DesignPage
{
    public partial class App : Application
    {

        public App()
        {
            this.Startup += this.Application_Startup;
            this.Exit += this.Application_Exit;
            this.UnhandledException += this.Application_UnhandledException;

            InitializeComponent();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {            
            if (e.InitParams.ContainsKey("Mode") &&
                e.InitParams.ContainsKey("Azure"))
            {
                if (e.InitParams["Mode"].Equals("New"))
                {
                    NewPage newPage = new NewPage();
                    this.RootVisual = newPage;
                }
                else if (e.InitParams["Mode"].Equals("Edit"))
                {
                    EditPage editPage = new EditPage();
                    this.RootVisual = editPage;
                }
                else if (e.InitParams["Mode"].Equals("Language"))
                {
                    ManageProgramLanguage manangeLanguagePage = new ManageProgramLanguage();
                    manangeLanguagePage.ProgramName = e.InitParams["Program"];
                    this.RootVisual = manangeLanguagePage;
                }
                else if (e.InitParams["Mode"].Equals("CTPPPresenterImage"))
                {
                    #region This function: it inits when pageload, it will be replaced by a new function that will popup a page for silverlight when click a button.
                    //CTPPPresenterImageSelection ctppPresenterImage = new CTPPPresenterImageSelection();
                    //this.RootVisual = ctppPresenterImage;
                    #endregion

                    ImageManager imageManager = new ImageManager("CTPPPresenter");
                    this.RootVisual = imageManager;


                }
                else if (e.InitParams["Mode"].Equals("PagePresenterImage"))
                {
                    ImageManager imageManager = new ImageManager("PagePresenter");
                    this.RootVisual = imageManager;
                }
                else
                {
                    HtmlPage.Window.Alert("Unregonized mode.");
                }

                StringUtility.AzureStroageAccountName = e.InitParams["Azure"];
            }
            else
            {
                HtmlPage.Window.Alert("Init parameters is wrong.");
            }
        }

        private void Application_Exit(object sender, EventArgs e)
        {

        }
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            // If the app is running outside of the debugger then report the exception using
            // the browser's exception mechanism. On IE this will display it a yellow alert 
            // icon in the status bar and Firefox will display a script error.
            if (!System.Diagnostics.Debugger.IsAttached)
            {

                // NOTE: This will allow the application to continue running after an exception has been thrown
                // but not handled. 
                // For production applications this error handling should be replaced with something that will 
                // report the error to the website and stop the application.
                e.Handled = true;
                Deployment.Current.Dispatcher.BeginInvoke(delegate { ReportErrorToDOM(e); });
            }
        }
        private void ReportErrorToDOM(ApplicationUnhandledExceptionEventArgs e)
        {
            try
            {
                string errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
                errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

                System.Windows.Browser.HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
            }
            catch (Exception)
            {
            }
        }
    }
}
