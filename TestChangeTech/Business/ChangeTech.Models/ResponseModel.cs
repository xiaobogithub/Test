using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeTech.Models
{
    public class ResponseModel
    {
        public ResultEnum Result { get; set; }
        public object Response { get; set; }
    }

    public class ValidateOrderLicenceResponseModel : ResponseModel
    {
        public LoginFailedTypeEnum LoginFailedType { get; set; }
    }
}
