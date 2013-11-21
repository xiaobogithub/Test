//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.view.templates {
    import mx.core.*;
    import mx.events.*;
    import mx.containers.*;
    import com.redbox.changetech.model.*;
    import com.redbox.changetech.vo.*;
    import com.redbox.changetech.view.modules.*;

    public class ModuleViewTemplate extends Canvas implements IBalanceModuleTemplate {

        private var _module:IBalanceModule
        protected var _104069929model:BalanceModelLocator
        protected var _985212102_content:Content
        private var _documentDescriptor_:UIComponentDescriptor

        public function ModuleViewTemplate(){
            _documentDescriptor_ = new UIComponentDescriptor({type:Canvas});
            _104069929model = BalanceModelLocator.getInstance();
            super();
            mx_internal::_document = this;
            this.verticalScrollPolicy = "off";
        }
        public function get content():Content{
            return (_content);
        }
        private function set _951530617content(_arg1:Content):void{
            _content = _arg1;
            reset();
        }
        protected function get _content():Content{
            return (this._985212102_content);
        }
        private function set _1068784020module(_arg1:IBalanceModule):void{
            _module = _arg1;
        }
        protected function reset():void{
        }
        override public function initialize():void{
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            super.initialize();
        }
        protected function set model(_arg1:BalanceModelLocator):void{
            var _local2:Object = this._104069929model;
            if (_local2 !== _arg1){
                this._104069929model = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "model", _local2, _arg1));
            };
        }
        public function set module(_arg1:IBalanceModule):void{
            var _local2:Object = this.module;
            if (_local2 !== _arg1){
                this._1068784020module = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "module", _local2, _arg1));
            };
        }
        protected function get model():BalanceModelLocator{
            return (this._104069929model);
        }
        public function set content(_arg1:Content):void{
            var _local2:Object = this.content;
            if (_local2 !== _arg1){
                this._951530617content = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "content", _local2, _arg1));
            };
        }
        public function get module():IBalanceModule{
            return (_module);
        }
        protected function set _content(_arg1:Content):void{
            var _local2:Object = this._985212102_content;
            if (_local2 !== _arg1){
                this._985212102_content = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "_content", _local2, _arg1));
            };
        }

    }
}//package com.redbox.changetech.view.templates 
