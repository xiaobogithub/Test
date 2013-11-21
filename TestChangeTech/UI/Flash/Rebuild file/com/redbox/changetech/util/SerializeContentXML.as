//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.util {
    import com.adobe.utils.*;

    public class SerializeContentXML {

        public static function convertToHTMLText(_arg1:XML):String{
            var _local4:String;
            XML.prettyPrinting = false;
            XML.ignoreWhitespace = true;
            var _local2:String = _arg1.toString();
            var _local3 = "</p><p><span class='paragraphSpacingMedium'></span></p><p>";
            _local4 = StringUtil.trim(_local2);
            _local4 = StringUtil.replace(_local4, "<content>", "");
            _local4 = StringUtil.replace(_local4, "</content>", "");
            _local4 = StringUtil.replace(_local4, "</p><p>", _local3);
            return (_local4);
        }

    }
}//package com.redbox.changetech.util 
