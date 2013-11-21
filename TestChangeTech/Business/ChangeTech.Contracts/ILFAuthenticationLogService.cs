using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;

namespace ChangeTech.Contracts
{
    public interface ILFAuthenticationLogService
    {
        void Insert(LFAuthenticationLogModel logModel);
        void Update(LFAuthenticationLogModel logModel);
        LFAuthenticationLogModel GetLFAuthenticationLogModel(string ip, string browser);
    }
}
