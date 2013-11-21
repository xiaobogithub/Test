using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class LFAuthenticationLogRepository : RepositoryBase, ILFAuthenticationLogRepository
    {
        public void Insert(LFAuthenticationLog log)
        {
            InsertEntity(log);
        }

        public void Update(LFAuthenticationLog log)
        {
            UpdateEntity(log);
        }

        public LFAuthenticationLog GetLFAuthenticationLog(string ip, string browser)
        {
            return GetEntities<LFAuthenticationLog>().Where(lf => lf.Browser == browser && lf.IP == ip).FirstOrDefault();
        }
    }
}
