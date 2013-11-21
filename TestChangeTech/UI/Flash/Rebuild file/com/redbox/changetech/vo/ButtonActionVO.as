//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.vo {
    import com.adobe.cairngorm.vo.*;

    public class ButtonActionVO implements IValueObject {

        public var Type:String
        public var Link:String
        public var NextType:String

        public static var NEXT:String = "Next";
        public static var LINK:String = "Link";
        public static var JUMP_TO:String = "JumpTo";
        public static var PRINT:String = "Print";
        public static var EXIT:String = "Exit";

    }
}//package com.redbox.changetech.vo 
