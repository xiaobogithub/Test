//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.messaging.messages {

    public class ErrorMessage extends AcknowledgeMessage {

        public var faultString:String
        public var extendedData:Object
        public var rootCause:Object
        public var faultCode:String
        public var faultDetail:String

        public static const RETRYABLE_HINT_HEADER:String = "DSRetryableErrorHint";
        public static const MESSAGE_DELIVERY_IN_DOUBT:String = "Client.Error.DeliveryInDoubt";

        override public function getSmallMessage():IMessage{
            return (null);
        }

    }
}//package mx.messaging.messages 
