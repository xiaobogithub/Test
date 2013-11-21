using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ChangeTech.Contracts;
using Ethos.DependencyInjection;

namespace AddUserMenuForProgram
{
    public class ServerProgram
    {
        static void Main(string[] args)
        {
            CreateUserMenuForProgram();
        }

        private static void CreateUserMenuForProgram()
        {
            IContainerContext context = ContainerManager.GetContainer("container");
            try
            {
                context.Resolve<IUserMenuService>().CreateUserMenuForProgramWhoDonotHave();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("Successful");
        }        
    }
}
