using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class FailEmailRepository : RepositoryBase, IFailEmailRepository
    {
        public void Insert(FailEmail email)
        {
            InsertEntity(email);
        }
    }
}
