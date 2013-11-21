using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public class RepositoryUtiltiy
    {
        public static void RestObjectContext()
        {
            RepositoryBase.ResetContainer();
        }
    }
}
