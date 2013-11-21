using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;
using ChangeTech.IDataRepository;
using ChangeTech.Entities;
using ChangeTech.Models;

namespace ChangeTech.Services
{
    public class LFAuthenticationLogService : ServiceBase, ILFAuthenticationLogService
    {
        public void Insert(Models.LFAuthenticationLogModel logModel)
        {
            LFAuthenticationLog log = new LFAuthenticationLog
            {
                IP = logModel.IP,
                Browser = logModel.Browser,
                LastUpdated = DateTime.UtcNow
            };
            Resolve<ILFAuthenticationLogRepository>().Insert(log);
        }

        public void Update(Models.LFAuthenticationLogModel logModel)
        {

            LFAuthenticationLog log = Resolve<ILFAuthenticationLogRepository>().GetLFAuthenticationLog(logModel.IP, logModel.Browser);
            log.LastUpdated = DateTime.Now;
            Resolve<ILFAuthenticationLogRepository>().Update(log);
        }

        public LFAuthenticationLogModel GetLFAuthenticationLogModel(string ip, string browser)
        {
            LFAuthenticationLog log = Resolve<ILFAuthenticationLogRepository>().GetLFAuthenticationLog(ip, browser);
            LFAuthenticationLogModel logModel = new LFAuthenticationLogModel
            {
                IP = log.IP,
                Browser = log.Browser,
                LastUpdated = log.LastUpdated.Value
            };
            return logModel;
        }
    }
}
