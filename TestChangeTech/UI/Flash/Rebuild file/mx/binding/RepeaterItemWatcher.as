//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.binding {
    import mx.events.*;
    import mx.collections.*;

    public class RepeaterItemWatcher extends Watcher {

        private var original:Boolean = true
        private var clones:Array
        private var dataProviderWatcher:PropertyWatcher

        mx_internal static const VERSION:String = "3.2.0.3958";

        public function RepeaterItemWatcher(_arg1:PropertyWatcher){
            this.dataProviderWatcher = _arg1;
        }
        override protected function shallowClone():Watcher{
            return (new RepeaterItemWatcher(dataProviderWatcher));
        }
        override public function updateParent(_arg1:Object):void{
            var dataProvider:* = null;
            var parent:* = _arg1;
            if (dataProviderWatcher){
                dataProvider = ICollectionView(dataProviderWatcher.value);
                if (dataProvider != null){
                    dataProvider.removeEventListener(CollectionEvent.COLLECTION_CHANGE, changedHandler, false);
                };
            };
            dataProviderWatcher = PropertyWatcher(parent);
            dataProvider = ICollectionView(dataProviderWatcher.value);
            if (dataProvider){
                if (original){
                    dataProvider.addEventListener(CollectionEvent.COLLECTION_CHANGE, changedHandler, false, 0, true);
                    updateClones(dataProvider);
                } else {
                    wrapUpdate(function ():void{
                        var _local1:IViewCursor = dataProvider.createCursor();
                        _local1.seek(CursorBookmark.FIRST, cloneIndex);
                        value = _local1.current;
                        updateChildren();
                    });
                };
            };
        }
        private function changedHandler(_arg1:CollectionEvent):void{
            var _local2:ICollectionView = ICollectionView(dataProviderWatcher.value);
            if (_local2){
                updateClones(_local2);
            };
        }
        private function updateClones(_arg1:ICollectionView):void{
            var _local3:RepeaterItemWatcher;
            if (clones){
                clones = clones.splice(0, _arg1.length);
            } else {
                clones = [];
            };
            var _local2:int;
            while (_local2 < _arg1.length) {
                _local3 = RepeaterItemWatcher(clones[_local2]);
                if (!_local3){
                    _local3 = RepeaterItemWatcher(deepClone(_local2));
                    _local3.original = false;
                    clones[_local2] = _local3;
                };
                _local3.updateParent(dataProviderWatcher);
                _local2++;
            };
        }

    }
}//package mx.binding 
