//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.geom {
    import org.papervision3d.core.render.data.*;
    import org.papervision3d.objects.*;
    import org.papervision3d.core.geom.renderables.*;
    import org.papervision3d.core.log.*;
    import org.papervision3d.materials.special.*;
    import org.papervision3d.core.render.draw.*;

    public class Lines3D extends Vertices3D {

        private var _material:ILineDrawer
        public var lines:Array

        public function Lines3D(_arg1:LineMaterial, _arg2:String=null){
            super(null, _arg2);
            this.material = _arg1;
            init();
        }
        private function init():void{
            this.lines = new Array();
        }
        public function removeAllLines():void{
            PaperLogger.warning("Lines3D.removeAllLines not yet implemented");
        }
        public function addLine(_arg1:Line3D):void{
            lines.push(_arg1);
            _arg1.instance = this;
            if (geometry.vertices.indexOf(_arg1.v0) == -1){
                geometry.vertices.push(_arg1.v0);
            };
            if (geometry.vertices.indexOf(_arg1.v1) == -1){
                geometry.vertices.push(_arg1.v1);
            };
            if (_arg1.cV){
                if (geometry.vertices.indexOf(_arg1.cV) == -1){
                    geometry.vertices.push(_arg1.cV);
                };
            };
        }
        public function addNewSegmentedLine(_arg1:Number, _arg2:Number, _arg3:Number, _arg4:Number, _arg5:Number, _arg6:Number, _arg7:Number, _arg8:Number):void{
            var _local12:Line3D;
            var _local14:Vertex3D;
            var _local9:Number = ((_arg6 - _arg3) / _arg2);
            var _local10:Number = ((_arg7 - _arg4) / _arg2);
            var _local11:Number = ((_arg8 - _arg5) / _arg2);
            var _local13:Vertex3D = new Vertex3D(_arg3, _arg4, _arg5);
            var _local15:Number = 0;
            while (_local15 <= _arg2) {
                _local14 = new Vertex3D((_arg3 + (_local9 * _local15)), (_arg4 + (_local10 * _local15)), (_arg5 + (_local11 * _local15)));
                _local12 = new Line3D(this, (material as LineMaterial), _arg1, _local13, _local14);
                addLine(_local12);
                _local13 = _local14;
                _local15++;
            };
        }
        public function removeLine(_arg1:Line3D):void{
            var _local2:int = lines.indexOf(_arg1);
            if (_local2 > -1){
                lines.splice(_local2, 1);
            } else {
                PaperLogger.warning("Papervision3D Lines3D.removeLine : WARNING removal of non-existant line attempted. ");
            };
        }
        override public function project(_arg1:DisplayObject3D, _arg2:RenderSessionData):Number{
            var _local3:Line3D;
            var _local4:Number;
            super.project(_arg1, _arg2);
            for each (_local3 in lines) {
                if (_arg2.viewPort.lineCuller.testLine(_local3)){
                    _local3.renderCommand.renderer = _local3.material;
                    _local4 = (_local4 + (_local3.renderCommand.screenDepth = ((_local3.v0.vertex3DInstance.z + _local3.v1.vertex3DInstance.z) / 2)));
                    _arg2.renderer.addToRenderList(_local3.renderCommand);
                };
            };
            return ((_local4 / (lines.length + 1)));
        }
        public function addNewLine(_arg1:Number, _arg2:Number, _arg3:Number, _arg4:Number, _arg5:Number, _arg6:Number, _arg7:Number):Line3D{
            var _local8:Line3D = new Line3D(this, (material as LineMaterial), _arg1, new Vertex3D(_arg2, _arg3, _arg4), new Vertex3D(_arg5, _arg6, _arg7));
            addLine(_local8);
            return (_local8);
        }

    }
}//package org.papervision3d.core.geom 
