using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using log4net;
using log4net.Config;
using System.IO;

namespace Ethos.Utility
{
    public class LogUtility
    {
        private static LogUtility _logUtiltiyInstance = null;
        public static LogUtility LogUtilityIntance
        {
            get {
                if (_logUtiltiyInstance == null)
                {
                    _logUtiltiyInstance = new LogUtility();
                }
                return _logUtiltiyInstance;
            }
        }

        private ILog _loger = null;

        private LogUtility()
        {
            //log4net.Config.DOMConfigurator.Configure();
            XmlConfigurator.Configure();
            _loger = LogManager.GetLogger("ChangeTechAs"); 
        }

        public void LogException(Exception ex, string methodAndParameters)
        {
            if (ex is System.Threading.ThreadAbortException)
            {

            }
            else
            {
                _loger.Fatal(methodAndParameters, ex);
            }
        }

        public void LogMessage(string messagex)
        {
            _loger.Info(messagex);
        }
    }
}
