using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;

namespace ChangeTech.Contracts
{
    public interface IOrderContentService
    {
        void InsertOrderContentList(List<OrderContentModel> orderContents);
        int GetLicenceCountByOrderGuid(Guid orderGuid);
        //int GetUsedLicenceCountByOrderGuid(Guid orderGuid);
        List<OrderContentModel> GetOrderContentsByOrderGuid(Guid orderGuid);
        OrderContentModel GetOrderContentByOrderGuidAndProgramGuid(Guid orderGuid, Guid programGuid);
        OrderContentModel GetOrderContentByOrderContentGuid(Guid orderContentGuid);
    }
}
