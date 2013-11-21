﻿//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.messaging.messages {

    public class HTTPRequestMessage extends AbstractMessage {

        private var _method:String
        public var httpHeaders:Object
        public var url:String
        public var contentType:String
        public var recordHeaders:Boolean

        public static const POST_METHOD:String = "POST";
        public static const CONTENT_TYPE_SOAP_XML:String = "text/xml; charset=utf-8";
        public static const CONTENT_TYPE_FORM:String = "application/x-www-form-urlencoded";
        public static const GET_METHOD:String = "GET";
        public static const HEAD_METHOD:String = "HEAD";
        public static const PUT_METHOD:String = "PUT";
        public static const TRACE_METHOD:String = "TRACE";
        public static const DELETE_METHOD:String = "DELETE";
        public static const CONTENT_TYPE_XML:String = "application/xml";
        public static const OPTIONS_METHOD:String = "OPTIONS";
        private static const VALID_METHODS:String = "POST,PUT,GET,HEAD,DELETE,OPTIONS,TRACE";

        public function HTTPRequestMessage(){
            _method = GET_METHOD;
        }
        public function get method():String{
            return (_method);
        }
        public function set method(_arg1:String):void{
            _method = _arg1;
        }

    }
}//package mx.messaging.messages 
