using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ChangeTech.Contracts;
using Ethos.DependencyInjection;
using System.Threading;

namespace ChangeTech.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(string.Format("Start to send reminder: {0}", DateTime.Now));

            try
            {
                DateTime lastCheckTime = DateTime.Now;

                do
                {
                    IContainerContext context = ContainerManager.GetContainer("container");
                    context.Resolve<IProgramUserService>().SendEmailToUser();

                    Console.WriteLine(string.Format("Finish send reminder: {0}", DateTime.Now));
                    Console.WriteLine(string.Format("Next time will start at {0}", lastCheckTime.AddMinutes(60 - lastCheckTime.Minute)));
                    lastCheckTime = DateTime.Now;
                    Thread.Sleep((60 - lastCheckTime.Minute) * 60000);
                }
                while (true);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Read();
            }
        }
    }
}
