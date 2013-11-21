using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Contracts;
using ChangeTech.Models;
using ChangeTech.Services;
using Ethos.DependencyInjection;
using Ethos.Utility;
using ChangeTech.IDataRepository;

namespace TransferTipmessage
{
    class Program
    {
        static void Main(string[] args)
        {
            IContainerContext context = ContainerManager.GetContainer("container");
            Console.WriteLine("Start *********************************************");
            List<ChangeTech.Entities.Company> companys = context.Resolve<ICompanyRepository>().GetAll().ToList();
            int count = 0;
            foreach(ChangeTech.Entities.Company pro in companys)
            {
                //if (pro.ProgramGUID.ToString() == "35e19fbb-5078-497d-b608-2f6650dae7a6")
                //{
                //Console.WriteLine("Copy program color for program: " + pro.ProgramGUID);
                //try
                //{
                //    //context.Resolve<ITipMessageService>().CopyTipMessageForProgram(pro.ProgramGUID);
                //    context.Resolve<IProgramService>().CopyProgramPrimaryButtonColorFromLayoutSetting(pro.ProgramGUID);
                //}
                //catch
                //{
                //    Console.WriteLine("There is an error, program guid:" + pro.ProgramGUID);
                //}
                ////CopyTipMessageForProgram(pro);
                //Console.WriteLine("Finished for program: " + pro.Name);
                //}
                Console.WriteLine("For program:" + pro.CompanyGUID);
                try
                {
                    if (string.IsNullOrEmpty(pro.Code))
                    {
                        context.Resolve<ICompanyService>().SetCompanyCodeForCompany(pro.CompanyGUID);           
                    }
                    count++;
                }
                catch
                {
                    Console.WriteLine("Error happened:" + pro.CompanyGUID);
                }

                Console.WriteLine("End, program count: " + count);

            }
            Console.WriteLine("End **************************************************");
            Console.ReadKey();
        }
    }
}
