//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.vo {
    import com.adobe.cairngorm.vo.*;

    public class Media implements IValueObject {

        public var Title:String
        public var Type:String
        public var Id:Number
        public var Url:String

        public static var VIDEO:String = "Video";
        public static var AUDIO:String = "Audio";

    }
}//package com.redbox.changetech.vo 
