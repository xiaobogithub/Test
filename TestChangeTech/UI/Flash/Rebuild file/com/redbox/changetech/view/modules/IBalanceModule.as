//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.view.modules {
    import mx.core.*;
    import flash.events.*;
    import com.redbox.changetech.view.components.*;
    import com.redbox.changetech.vo.*;

    public interface IBalanceModule {

        function get content():Content;
        function set contentIndex(_arg1:Number):void;
        function get contentImage():BalanceImageReflectionCanvas;
        function set contentImage(_arg1:BalanceImageReflectionCanvas):void;
        function get transitionContainer1():IContainer;
        function get transitionContainer2():IContainer;
        function set mandatoryQuestionsComplete(_arg1:Boolean):void;
        function set transitionContainer1(_arg1:IContainer):void;
        function set transitionContainer2(_arg1:IContainer):void;
        function getAnswerById(_arg1:int):Answer;
        function set contentCollection(_arg1:ContentCollection):void;
        function collectionComplete(_arg1:Event=null):void;
        function moduleReady(_arg1:Event=null):void;
        function set content(_arg1:Content):void;
        function get ctaBtn():BalanceButtonReflectionCanvas;
        function get mandatoryQuestionsComplete():Boolean;
        function get contentCollection():ContentCollection;
        function set ctaBtn(_arg1:BalanceButtonReflectionCanvas):void;
        function get contentIndex():Number;
        function next(_arg1:Event=null):Boolean;
        function previous(_arg1:Event=null):Boolean;

    }
}//package com.redbox.changetech.view.modules 
