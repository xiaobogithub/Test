//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.view.components {
    import flash.display.*;
    import flash.geom.*;
    import flash.media.*;
    import flash.text.*;
    import mx.core.*;
    import flash.events.*;
    import mx.styles.*;
    import mx.binding.*;
    import mx.containers.*;
    import flash.utils.*;
    import flash.system.*;
    import flash.accessibility.*;
    import flash.xml.*;
    import flash.net.*;
    import flash.filters.*;
    import flash.ui.*;
    import flash.external.*;
    import flash.debugger.*;
    import flash.errors.*;
    import flash.printing.*;
    import flash.profiler.*;

    public class Header extends HBox {

        private var _documentDescriptor_:UIComponentDescriptor

        public function Header(){
            _documentDescriptor_ = new UIComponentDescriptor({type:HBox, propertiesFactory:function ():Object{
                return ({childDescriptors:[new UIComponentDescriptor({type:InfoPanel, stylesFactory:function ():void{
                    this.horizontalAlign = "right";
                }, propertiesFactory:function ():Object{
                    return ({percentWidth:100});
                }})]});
            }});
            super();
            mx_internal::_document = this;
            if (!this.styleDeclaration){
                this.styleDeclaration = new CSSStyleDeclaration();
            };
            this.styleDeclaration.defaultFactory = function ():void{
                this.paddingTop = 30;
                this.paddingLeft = 30;
                this.paddingRight = 30;
            };
            this.percentWidth = 100;
        }
        override public function initialize():void{
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            super.initialize();
        }

    }
}//package com.redbox.changetech.view.components 
