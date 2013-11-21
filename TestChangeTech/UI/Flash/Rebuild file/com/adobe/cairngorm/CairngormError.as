//Created by Action Script Viewer - http://www.buraks.com/asv
package com.adobe.cairngorm {
    import mx.resources.*;
    import flash.system.*;
    import mx.utils.*;

    public class CairngormError extends Error {

        private static var rb:ResourceBundle = ResourceBundle.getResourceBundle("CairngormMessages", ApplicationDomain.currentDomain);

        public function CairngormError(_arg1:String, ... _args){
            super(formatMessage(_arg1, _args.toString()));
        }
        private function formatMessage(_arg1:String, ... _args):String{
            var _local3:String;
            _local3 = StringUtil.substitute(resourceBundle.getString(_arg1), _args);
            return (StringUtil.substitute("{0}: {1}", _arg1, _local3));
        }
        protected function get resourceBundle():ResourceBundle{
            return (rb);
        }

    }
}//package com.adobe.cairngorm 
