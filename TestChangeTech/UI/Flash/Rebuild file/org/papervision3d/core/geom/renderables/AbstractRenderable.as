//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.geom.renderables {
    import org.papervision3d.objects.*;
    import org.papervision3d.core.render.command.*;
    import org.papervision3d.core.data.*;

    public class AbstractRenderable implements IRenderable {

        public var _userData:UserData
        public var instance:DisplayObject3D

        public function set userData(_arg1:UserData):void{
            _userData = _arg1;
        }
        public function get userData():UserData{
            return (_userData);
        }
        public function getRenderListItem():IRenderListItem{
            return (null);
        }

    }
}//package org.papervision3d.core.geom.renderables 
