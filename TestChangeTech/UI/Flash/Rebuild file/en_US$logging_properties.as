﻿//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import mx.resources.*;

    public class en_US$logging_properties extends ResourceBundle {

        public function en_US$logging_properties(){
            super("en_US", "logging");
        }
        override protected function getContent():Object{
            var _local1:Object = {invalidTarget:"Invalid target specified.", charsInvalid:"Error for filter '{0}': The following characters are not valid: []~$^&/(){}<>+=_-`!@#%?,:;'\".", charPlacement:"Error for filter '{0}': '*' must be the right most character.", levelLimit:"Level must be less than LogEventLevel.ALL.", invalidChars:"Categories can not contain any of the following characters: []`~,!@#$%*^&()]{}+=|';?><./\".", invalidLen:"Categories must be at least one character in length."};
            return (_local1);
        }

    }
}//package 
