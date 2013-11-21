using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface ILFAuthenticationLogRepository
    {
        void Insert(LFAuthenticationLog log);
        void Update(LFAuthenticationLog log);
        LFAuthenticationLog GetLFAuthenticationLog(string ip,string browser);
    }
}
