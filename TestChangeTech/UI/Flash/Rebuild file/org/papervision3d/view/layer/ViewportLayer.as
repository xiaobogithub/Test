//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.view.layer {
    import flash.display.*;
    import flash.utils.*;
    import org.papervision3d.objects.*;
    import org.papervision3d.core.render.command.*;
    import org.papervision3d.view.*;
    import org.papervision3d.core.log.*;
    import org.papervision3d.view.layer.util.*;

    public class ViewportLayer extends Sprite {

        public var layerIndex:Number
        public var layers:Dictionary
        public var childLayers:Array
        public var weight:Number = 0
        public var dynamicLayer:Boolean = false
        public var forceDepth:Boolean = false
        public var displayObject3D:DisplayObject3D
        public var sortMode:String
        protected var viewport:Viewport3D
        public var displayObjects:Dictionary
        public var graphicsChannel:Graphics
        public var screenDepth:Number = 0

        public function ViewportLayer(_arg1:Viewport3D, _arg2:DisplayObject3D, _arg3:Boolean=false){
            layers = new Dictionary(true);
            displayObjects = new Dictionary(true);
            sortMode = ViewportLayerSortMode.Z_SORT;
            super();
            this.viewport = _arg1;
            this.displayObject3D = _arg2;
            this.dynamicLayer = _arg3;
            this.graphicsChannel = this.graphics;
            if (_arg3){
                this.filters = _arg2.filters;
                this.blendMode = _arg2.blendMode;
                this.alpha = _arg2.alpha;
            };
            if (_arg2){
                addDisplayObject3D(_arg2);
                _arg2.container = this;
            };
            init();
        }
        public function removeLayerAt(_arg1:Number):void{
            var _local2:DisplayObject3D;
            for each (_local2 in childLayers[_arg1].displayObjects) {
                unlinkChild(_local2);
            };
            removeChild(childLayers[_arg1]);
            childLayers.splice(_arg1, 1);
        }
        private function onChildAdded(_arg1:ViewportLayerEvent):void{
            if (_arg1.do3d){
                linkChild(_arg1.do3d, _arg1.layer, _arg1);
            };
        }
        public function addLayer(_arg1:ViewportLayer):void{
            var _local2:DisplayObject3D;
            var _local3:ViewportLayer;
            childLayers.push(_arg1);
            addChild(_arg1);
            _arg1.addEventListener(ViewportLayerEvent.CHILD_ADDED, onChildAdded);
            _arg1.addEventListener(ViewportLayerEvent.CHILD_REMOVED, onChildRemoved);
            for each (_local2 in _arg1.displayObjects) {
                linkChild(_local2, _arg1);
            };
            for each (_local3 in _arg1.layers) {
                for each (_local2 in _local3.displayObjects) {
                    linkChild(_local2, _local3);
                };
            };
        }
        public function getChildLayer(_arg1:DisplayObject3D, _arg2:Boolean=true, _arg3:Boolean=false):ViewportLayer{
            _arg1 = (_arg1.parentContainer) ? _arg1.parentContainer : _arg1;
            if (layers[_arg1]){
                return (layers[_arg1]);
            };
            if (_arg2){
                return (getChildLayerFor(_arg1, _arg3));
            };
            return (null);
        }
        protected function getChildLayerFor(_arg1:DisplayObject3D, _arg2:Boolean=false):ViewportLayer{
            var _local3:ViewportLayer;
            if (_arg1){
                _local3 = new ViewportLayer(viewport, _arg1, _arg1.useOwnContainer);
                addLayer(_local3);
                if (_arg2){
                    _arg1.addChildrenToLayer(_arg1, _local3);
                };
                return (_local3);
            } else {
                PaperLogger.warning("Needs to be a do3d");
            };
            return (null);
        }
        public function updateAfterRender():void{
            var _local1:ViewportLayer;
            for each (_local1 in childLayers) {
                _local1.updateAfterRender();
            };
        }
        protected function init():void{
            childLayers = new Array();
        }
        public function clear():void{
            graphicsChannel.clear();
            reset();
        }
        protected function reset():void{
            if (!forceDepth){
                screenDepth = 0;
            };
            this.weight = 0;
        }
        public function updateInfo():void{
            var _local1:ViewportLayer;
            for each (_local1 in childLayers) {
                _local1.updateInfo();
                if (!forceDepth){
                    this.weight = (this.weight + _local1.weight);
                    this.screenDepth = (this.screenDepth + (_local1.screenDepth * _local1.weight));
                };
            };
            if (!forceDepth){
                this.screenDepth = (this.screenDepth / this.weight);
            };
        }
        private function linkChild(_arg1:DisplayObject3D, _arg2:ViewportLayer, _arg3:ViewportLayerEvent=null):void{
            layers[_arg1] = _arg2;
            dispatchEvent(new ViewportLayerEvent(ViewportLayerEvent.CHILD_ADDED, _arg1, _arg2));
        }
        protected function orderLayers():void{
            var _local1:int;
            while (_local1 < childLayers.length) {
                this.setChildIndex(childLayers[_local1], _local1);
                childLayers[_local1].sortChildLayers();
                _local1++;
            };
        }
        public function updateBeforeRender():void{
            var _local1:ViewportLayer;
            clear();
            for each (_local1 in childLayers) {
                _local1.updateBeforeRender();
            };
        }
        public function hasDisplayObject3D(_arg1:DisplayObject3D):Boolean{
            return (!((displayObjects[_arg1] == null)));
        }
        public function sortChildLayers():void{
            if (sortMode == ViewportLayerSortMode.Z_SORT){
                childLayers.sortOn("screenDepth", (Array.DESCENDING | Array.NUMERIC));
            } else {
                childLayers.sortOn("layerIndex", Array.NUMERIC);
            };
            orderLayers();
        }
        private function onChildRemoved(_arg1:ViewportLayerEvent):void{
            if (_arg1.do3d){
                unlinkChild(_arg1.do3d, _arg1);
            };
        }
        public function removeAllLayers():void{
            var _local1:int = (childLayers.length - 1);
            while (_local1 >= 0) {
                removeLayerAt(_local1);
                _local1--;
            };
        }
        public function processRenderItem(_arg1:RenderableListItem):void{
            if (!forceDepth){
                this.screenDepth = (this.screenDepth + _arg1.screenDepth);
                this.weight++;
            };
        }
        public function removeLayer(_arg1:ViewportLayer):void{
            var _local2:int = getChildIndex(_arg1);
            if (_local2 > -1){
                removeLayerAt(_local2);
            } else {
                PaperLogger.error("Layer not found for removal.");
            };
        }
        public function addDisplayObject3D(_arg1:DisplayObject3D, _arg2:Boolean=false):void{
            if (!_arg1){
                return;
            };
            displayObjects[_arg1] = _arg1;
            dispatchEvent(new ViewportLayerEvent(ViewportLayerEvent.CHILD_ADDED, _arg1, this));
            if (_arg2){
                _arg1.addChildrenToLayer(_arg1, this);
            };
        }
        public function removeDisplayObject3D(_arg1:DisplayObject3D):void{
            displayObjects[_arg1] = null;
            dispatchEvent(new ViewportLayerEvent(ViewportLayerEvent.CHILD_REMOVED, _arg1, this));
        }
        private function unlinkChild(_arg1:DisplayObject3D, _arg2:ViewportLayerEvent=null):void{
            layers[_arg1] = null;
            dispatchEvent(new ViewportLayerEvent(ViewportLayerEvent.CHILD_REMOVED, _arg1));
        }
        public function getLayerObjects(_arg1:Array=null):Array{
            var _local2:DisplayObject3D;
            var _local3:ViewportLayer;
            if (!_arg1){
                _arg1 = new Array();
            };
            for each (_local2 in this.displayObjects) {
                if (((_local2) && ((_local2.parent == null)))){
                    _arg1.push(_local2);
                };
            };
            for each (_local3 in childLayers) {
                _local3.getLayerObjects(_arg1);
            };
            return (_arg1);
        }
        public function childLayerIndex(_arg1:DisplayObject3D):Number{
            _arg1 = (_arg1.parentContainer) ? _arg1.parentContainer : _arg1;
            var _local2:int;
            while (_local2 < childLayers.length) {
                if (childLayers[_local2].hasDisplayObject3D(_arg1)){
                    return (_local2);
                };
                _local2++;
            };
            return (-1);
        }

    }
}//package org.papervision3d.view.layer 
