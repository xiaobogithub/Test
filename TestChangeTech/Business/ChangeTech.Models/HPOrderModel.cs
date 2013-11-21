using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeTech.Models
{
    public class HPOrderModel
    {
        public Guid HPOrderGUID { get; set; }
        public Guid ProgramGUID { get; set; }
        public int OrderStatus { get; set; }
        public string CustomerName { get; set; }
        public string ContactPersonName { get; set; }
        public string ContactPersonNumber { get; set; }
        public string ContactPersonPhone { get; set; }
        public string ContactPersonEmail { get; set; }
        public string SOSIContactEmail { get; set; }
        public int NumberOfEmployees { get; set; }
        public int UsedLicence { get; set; }
        public string LinkToStartPageURL { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StopDate { get; set; }
        public DateTime Created { get; set; }
        public int LocationID { get; set; }
        public int IndustryID { get; set; }
        public string Code { get; set; }
        public string Updated { get; set; }
        public Guid UpdatedBy { get; set; }
    }


    public class HPOrderLicenceModel
    {
        public Guid HPOrderLicenceGUID { get; set; }
        public Guid HPOrderGUID { get; set; }
        public Guid ProgramUserGUID { get; set; }
        public DateTime Created { get; set; }
    }

    public class HPOrderParamModel
    {
        public int HPOrderParamID { get; set; }
        public string HPOrderParamName { get; set; }
        public string HPOrderParamType { get; set; }
    }

    public class HPOrderEmailModel
    {
        public Guid HPOrderEmailGUID { get; set; }
        public Guid HPOrderGUID { get; set; }
        public Guid ProgramGUID { get; set; }
        public int LogType { get; set; }
        public string HPContactEmail { get; set; }
        public string HPEmailSubject { get; set; }
        public string HPEmailBody { get; set; }
        public DateTime HPEmailSendDate { get; set; }
        public bool IsSend { get; set; }
    }
}
