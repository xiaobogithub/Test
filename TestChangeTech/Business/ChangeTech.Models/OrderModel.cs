using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeTech.Models
{
    public class OrderModel
    {
        public Guid OrderGUID { get; set; }
        //public Guid OrderContentGUID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public Guid LanguageGUID { get; set; }
        public Guid LicenceTypeGUID { get; set; }
        public int? NumberOfEmployees { get; set; }
        public string OrderStatus { get; set; }
        public int? OrderStatusID { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public bool? IsPromotion { get; set; }
        public DateTime Created { get; set; }
        public Guid UpdatedBy { get; set; }
        public string OrderLicences { get; set; }
        public int UsedLicences { get; set; }
        public List<OrderContentModel> orderContents { get; set; }
    }

    public class OrderContentModel
    {
        public Guid OrderContentGUID { get; set; }
        public Guid OrderGUID { get; set; }
        public Guid ProgramGUID { get; set; }
        public string ProgramName { get; set; }
        public int ProgramLicences { get; set; }
        public string Licences { get; set; }
        public int UsedLicences { get; set; }
        public Guid UpdatedBy { get; set; }
        public string LastRegisted { get; set; }
    }

    public class OrderLicenceModel
    {
        public Guid OrderLicenceGUID { get; set; }
        public Guid? OrderContentGUID { get; set; }
        public Guid? ProgramUserGUID { get; set; }
        //public string PromotionCode { get; set; }
        public Guid UpdatedBy { get; set; }
        public string LastRegisted { get; set; }
    }

    public class OrderLicenceTypeModel
    {
        public Guid OrderLicenceTypeGUID { get; set; }
        public string TypeName { get; set; }
        public string TypeDescription { get; set; }
    }
}
