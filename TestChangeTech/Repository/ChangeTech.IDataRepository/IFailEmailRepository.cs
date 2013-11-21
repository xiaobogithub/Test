using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IFailEmailRepository
    {
        void Insert(FailEmail email);
    }
}
