//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.view {
    import org.papervision3d.cameras.*;
    import org.papervision3d.objects.*;
    import org.papervision3d.core.view.*;
    import org.papervision3d.render.*;
    import org.papervision3d.scenes.*;

    public class BasicView extends AbstractView implements IView {

        public function BasicView(_arg1:Number=640, _arg2:Number=480, _arg3:Boolean=true, _arg4:Boolean=false, _arg5:String="Target"){
            scene = new Scene3D();
            viewport = new Viewport3D(_arg1, _arg2, _arg3, _arg4);
            addChild(viewport);
            renderer = new BasicRenderEngine();
            switch (_arg5){
                case CameraType.DEBUG:
                    _camera = new DebugCamera3D(viewport);
                    break;
                case CameraType.TARGET:
                    _camera = new Camera3D(60);
                    _camera.target = DisplayObject3D.ZERO;
                    break;
                case CameraType.FREE:
                default:
                    _camera = new Camera3D(60);
                    break;
            };
            cameraAsCamera3D.update(viewport.sizeRectangle);
        }
        public function get cameraAsDebugCamera3D():DebugCamera3D{
            return ((_camera as DebugCamera3D));
        }
        public function get cameraAsCamera3D():Camera3D{
            return ((_camera as Camera3D));
        }

    }
}//package org.papervision3d.view 
