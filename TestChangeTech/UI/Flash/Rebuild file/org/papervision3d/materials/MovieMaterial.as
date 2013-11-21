//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.materials {
    import flash.display.*;
    import flash.geom.*;
    import org.papervision3d.core.render.data.*;
    import org.papervision3d.core.geom.renderables.*;
    import org.papervision3d.core.log.*;
    import org.papervision3d.core.render.material.*;
    import org.papervision3d.core.render.draw.*;

    public class MovieMaterial extends BitmapMaterial implements ITriangleDrawer, IUpdateBeforeMaterial, IUpdateAfterMaterial {

        public var movieTransparent:Boolean
        private var quality:String
        private var materialIsUsed:Boolean = false
        private var autoClipRect:Rectangle
        public var allowAutoResize:Boolean = false
        public var movie:DisplayObject
        private var movieAnimated:Boolean
        protected var recreateBitmapInSuper:Boolean
        private var stage:Stage
        private var userClipRect:Rectangle

        public function MovieMaterial(_arg1:DisplayObject=null, _arg2:Boolean=false, _arg3:Boolean=false, _arg4:Boolean=false, _arg5:Rectangle=null){
            movieTransparent = _arg2;
            this.animated = _arg3;
            this.precise = _arg4;
            userClipRect = _arg5;
            if (_arg1){
                texture = _arg1;
            };
        }
        protected function createBitmapFromSprite(_arg1:DisplayObject):BitmapData{
            movie = _arg1;
            initBitmap(movie);
            drawBitmap();
            bitmap = super.createBitmap(bitmap);
            return (bitmap);
        }
        public function set rect(_arg1:Rectangle):void{
            userClipRect = _arg1;
            createBitmapFromSprite(movie);
        }
        public function updateAfterRender(_arg1:RenderSessionData):void{
            if ((((movieAnimated == true)) && ((materialIsUsed == true)))){
                drawBitmap();
                if (recreateBitmapInSuper){
                    bitmap = super.createBitmap(bitmap);
                    recreateBitmapInSuper = false;
                };
            };
        }
        public function set animated(_arg1:Boolean):void{
            movieAnimated = _arg1;
        }
        public function drawBitmap():void{
            var _local3:String;
            bitmap.fillRect(bitmap.rect, fillColor);
            if (((stage) && (quality))){
                _local3 = stage.quality;
                stage.quality = quality;
            };
            var _local1:Rectangle = rect;
            var _local2:Matrix = new Matrix(1, 0, 0, 1, -(_local1.x), -(_local1.y));
            bitmap.draw(movie, _local2, movie.transform.colorTransform, null);
            if (!userClipRect){
                autoClipRect = movie.getBounds(movie);
            };
            if (((stage) && (quality))){
                stage.quality = _local3;
            };
        }
        override public function get texture():Object{
            return (this._texture);
        }
        public function updateBeforeRender(_arg1:RenderSessionData):void{
            var _local2:int;
            var _local3:int;
            materialIsUsed = false;
            if (movieAnimated){
                if (userClipRect){
                    _local2 = int((userClipRect.width + 0.5));
                    _local3 = int((userClipRect.height + 0.5));
                } else {
                    _local2 = int((movie.width + 0.5));
                    _local3 = int((movie.height + 0.5));
                };
                if (((allowAutoResize) && (((!((_local2 == bitmap.width))) || (!((_local3 == bitmap.height))))))){
                    initBitmap(movie);
                    recreateBitmapInSuper = true;
                };
            };
        }
        protected function initBitmap(_arg1:DisplayObject):void{
            if (bitmap){
                bitmap.dispose();
            };
            if (userClipRect){
                bitmap = new BitmapData(int((userClipRect.width + 0.5)), int((userClipRect.height + 0.5)), movieTransparent, fillColor);
            } else {
                if ((((_arg1.width == 0)) || ((_arg1.height == 0)))){
                    bitmap = new BitmapData(0x0100, 0x0100, movieTransparent, fillColor);
                } else {
                    bitmap = new BitmapData(int((_arg1.width + 0.5)), int((_arg1.height + 0.5)), movieTransparent, fillColor);
                };
            };
        }
        public function get animated():Boolean{
            return (movieAnimated);
        }
        public function get rect():Rectangle{
            var _local1:Rectangle = ((userClipRect) || (autoClipRect));
            if (((!(_local1)) && (movie))){
                _local1 = movie.getBounds(movie);
            };
            return (_local1);
        }
        override public function set texture(_arg1:Object):void{
            if ((_arg1 is DisplayObject) == false){
                PaperLogger.error("MovieMaterial.texture requires a Sprite to be passed as the object");
                return;
            };
            bitmap = createBitmapFromSprite(DisplayObject(_arg1));
            _texture = _arg1;
        }
        override public function drawTriangle(_arg1:Triangle3D, _arg2:Graphics, _arg3:RenderSessionData, _arg4:BitmapData=null, _arg5:Matrix=null):void{
            materialIsUsed = true;
            super.drawTriangle(_arg1, _arg2, _arg3, _arg4, _arg5);
        }
        public function setQuality(_arg1:String, _arg2:Stage, _arg3:Boolean=true):void{
            this.quality = _arg1;
            this.stage = _arg2;
            if (_arg3){
                createBitmapFromSprite(movie);
            };
        }

    }
}//package org.papervision3d.materials 
