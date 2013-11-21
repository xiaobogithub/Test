using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Ethos.DependencyInjection;

namespace ChangeTech.ImageUploader
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ClientInstance.ContaionerContext = ContainerManager.GetContainer("container");

            if (ClientInstance.ContaionerContext != null)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            else
            {
                MessageBox.Show("Cannot initialize container, please contact ChangeTech Develop team.");
            }
        }
    }
}
