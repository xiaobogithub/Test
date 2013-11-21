//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.core {
    import flash.display.*;
    import mx.automation.*;
    import flash.events.*;
    import mx.events.*;
    import mx.collections.*;
    import mx.collections.errors.*;

    public class Repeater extends UIComponent implements IRepeater {

        private var _container:Container
        private var _count:int = -1
        private var descriptorIndex:int
        public var childDescriptors:Array
        mx_internal var createdComponents:Array
        private var collection:ICollectionView
        private var _currentIndex:int
        private var created:Boolean = false
        private var iterator:IViewCursor
        private var _startingIndex:int = 0
        private var _recycleChildren:Boolean = false

        mx_internal static const VERSION:String = "3.2.0.3958";

        public function get container():IContainer{
            return ((_container as IContainer));
        }
        private function createComponentFromDescriptor(_arg1:int, _arg2:int, _arg3:Boolean):IFlexDisplayObject{
            var _local4:UIComponentDescriptor = childDescriptors[_arg2];
            if (!_local4.document){
                _local4.document = document;
            };
            _local4.instanceIndices = (instanceIndices) ? instanceIndices : [];
            _local4.instanceIndices.push(_arg1);
            _local4.repeaters = repeaters;
            _local4.repeaters.push(this);
            _local4.repeaterIndices = repeaterIndices;
            _local4.repeaterIndices.push((startingIndex + _arg1));
            _local4.invalidateProperties();
            var _local5:IFlexDisplayObject = Container(container).createComponentFromDescriptor(_local4, _arg3);
            _local4.instanceIndices = null;
            _local4.repeaters = null;
            _local4.repeaterIndices = null;
            dispatchEvent(new Event("nextRepeaterItem"));
            return (_local5);
        }
        private function responderResultHandler(_arg1:Object, _arg2:Object):void{
            execute();
        }
        private function removeAllChildRepeaters(_arg1:Container):void{
            var _local2:int;
            var _local3:int;
            var _local4:Repeater;
            if (_arg1.childRepeaters){
                _local2 = _arg1.childRepeaters.length;
                _local3 = (_local2 - 1);
                while (_local3 >= 0) {
                    _local4 = _arg1.childRepeaters[_local3];
                    if (hasDescendant(_local4)){
                        removeRepeater(_local4);
                    };
                    _local3--;
                };
            };
        }
        private function recycle():void{
            var _local2:int;
            var _local3:int;
            var _local4:int;
            var _local6:int;
            var _local7:Repeater;
            var _local8:IRepeaterClient;
            var _local9:int;
            var _local10:int;
            var _local11:IRepeaterClient;
            var _local12:IRepeaterClient;
            dispatchEvent(new FlexEvent(FlexEvent.REPEAT_START));
            var _local1:int;
            var _local5:int;
            if (((((collection) && ((collection.length > 0)))) && (((collection.length - startingIndex) > 0)))){
                _local1 = positiveMin((collection.length - startingIndex), count);
                _local6 = 0;
                _local2 = 0;
                while (_local2 < _local1) {
                    _currentIndex = (startingIndex + _local2);
                    dispatchEvent(new FlexEvent(FlexEvent.REPEAT));
                    if (childDescriptors){
                        _local3 = childDescriptors.length;
                        if (createdComponents.length >= ((_local2 + 1) * _local3)){
                            _local4 = 0;
                            while (_local4 < _local3) {
                                _local8 = createdComponents[((_local2 * _local3) + _local4)];
                                if ((_local8 is Repeater)){
                                    _local7 = Repeater(_local8);
                                    resetRepeaterIndices(_local7, _currentIndex);
                                    _local7.owner = this;
                                    _local7.execute();
                                } else {
                                    resetRepeaterIndices(_local8, _currentIndex);
                                    if ((_local8 is IDeferredInstantiationUIComponent)){
                                        IDeferredInstantiationUIComponent(_local8).executeBindings(true);
                                    };
                                };
                                _local5++;
                                _local4++;
                            };
                        } else {
                            _local4 = 0;
                            while (_local4 < _local3) {
                                _local9 = container.numChildren;
                                _local10 = (getIndexForFirstChild() + numCreatedChildren);
                                _local11 = IRepeaterClient(createComponentFromDescriptor(_local2, _local4, true));
                                createdComponents.push(_local11);
                                if ((_local11 is IUIComponent)){
                                    IUIComponent(_local11).owner = this;
                                };
                                if ((_local11 is IAutomationObject)){
                                    IAutomationObject(_local11).showInAutomationHierarchy = true;
                                };
                                if ((_local11 is Repeater)){
                                    _local7 = Repeater(_local11);
                                    _local7.reindexDescendants(_local9, _local10);
                                } else {
                                    container.setChildIndex(DisplayObject(_local11), _local10);
                                };
                                _local5++;
                                _local4++;
                            };
                        };
                    };
                    _local2++;
                };
            };
            _currentIndex = -1;
            _local2 = (createdComponents.length - 1);
            while (_local2 >= _local5) {
                _local12 = createdComponents.pop();
                if ((_local12 is Repeater)){
                    removeRepeater(Repeater(_local12));
                } else {
                    if (_local12){
                        if ((_local12 is Container)){
                            removeAllChildren(Container(_local12));
                            removeAllChildRepeaters(Container(_local12));
                        };
                        if (container.contains(DisplayObject(_local12))){
                            container.removeChild(DisplayObject(_local12));
                        };
                        if ((_local12 is IDeferredInstantiationUIComponent)){
                            IDeferredInstantiationUIComponent(_local12).deleteReferenceOnParentDocument(IFlexDisplayObject(parentDocument));
                        };
                    };
                };
                _local2--;
            };
            dispatchEvent(new FlexEvent(FlexEvent.REPEAT_END));
        }
        public function get count():int{
            return (_count);
        }
        private function reindexDescendants(_arg1:int, _arg2:int):void{
            var _local5:IRepeaterClient;
            var _local3:int = (container.numChildren - _arg1);
            var _local4:int;
            while (_local4 < _local3) {
                _local5 = IRepeaterClient(container.getChildAt((_arg1 + _local4)));
                container.setChildIndex(DisplayObject(_local5), (_arg2 + _local4));
                _local4++;
            };
        }
        private function adjustIndices(_arg1:IRepeaterClient, _arg2:int):void{
            var _local3:Array = _arg1.repeaters;
            if (_local3 == null){
                return;
            };
            var _local4:int = _local3.length;
            var _local5:int;
            while (_local5 < _local4) {
                if (_local3[_local5] == this){
                    _arg1.repeaterIndices[_local5] = (_arg1.repeaterIndices[_local5] + _arg2);
                    _arg1.instanceIndices[_local5] = (_arg1.instanceIndices[_local5] + _arg2);
                    break;
                };
                _local5++;
            };
        }
        private function positiveMin(_arg1:int, _arg2:int):int{
            var _local3:int;
            if (_arg1 >= 0){
                if (_arg2 >= 0){
                    if (_arg1 < _arg2){
                        _local3 = _arg1;
                    } else {
                        _local3 = _arg2;
                    };
                } else {
                    _local3 = _arg1;
                };
            } else {
                _local3 = _arg2;
            };
            return (_local3);
        }
        mx_internal function getItemAt(_arg1:int):Object{
            var result:* = null;
            var index:* = _arg1;
            if (iterator){
                try {
                    iterator.seek(CursorBookmark.FIRST, index);
                    result = iterator.current;
                } catch(itemPendingError:ItemPendingError) {
                    itemPendingError.addResponder(new ItemResponder(responderResultHandler, responderFaultHandler));
                };
            };
            return (result);
        }
        public function set count(_arg1:int):void{
            _count = _arg1;
            execute();
            dispatchEvent(new Event("countChanged"));
        }
        private function addItems(_arg1:int, _arg2:int):void{
            var _local3:int;
            var _local6:IRepeaterClient;
            var _local7:int;
            var _local8:int;
            var _local9:int;
            var _local10:int;
            var _local11:int;
            var _local12:int;
            var _local13:IFlexDisplayObject;
            var _local14:Repeater;
            if (startingIndex > _arg2){
                return;
            };
            var _local4 = -1;
            var _local5:int = container.numChildren;
            if (_arg2 == (collection.length - 1)){
                _local3 = (_local5 - 1);
                while (_local3 >= 0) {
                    _local6 = IRepeaterClient(container.getChildAt(_local3));
                    _local7 = getRepeaterIndex(_local6);
                    if (_local7 != -1){
                        _local4 = (_local3 + 1);
                        break;
                    };
                    _local3--;
                };
            } else {
                _local8 = ((_arg2 - _arg1) + 1);
                _local3 = 0;
                while (_local3 < _local5) {
                    _local6 = IRepeaterClient(container.getChildAt(_local3));
                    _local7 = getRepeaterIndex(_local6);
                    if (_local7 != -1){
                        if ((((((_arg1 <= _local7)) && ((_local7 <= _arg2)))) && ((_local4 == -1)))){
                            _local4 = _local3;
                        };
                        if (_local7 >= _arg1){
                            adjustIndices(_local6, _local8);
                        };
                    };
                    _local3++;
                };
            };
            if (count == -1){
                _local5 = _arg2;
            } else {
                _local5 = positiveMin(((startingIndex + count) - 1), _arg2);
            };
            _local3 = Math.max(startingIndex, _arg1);
            while (_local3 <= _local5) {
                _local9 = childDescriptors.length;
                _currentIndex = _local3;
                dispatchEvent(new FlexEvent(FlexEvent.REPEAT));
                _local10 = 0;
                while (_local10 < _local9) {
                    _local11 = container.numChildren;
                    _local12 = (getIndexForFirstChild() + numCreatedChildren);
                    _local13 = createComponentFromDescriptor((_local3 - startingIndex), _local10, true);
                    createdComponents.push(_local13);
                    if ((_local13 is IUIComponent)){
                        IUIComponent(_local13).owner = this;
                    };
                    if ((_local13 is IAutomationObject)){
                        IAutomationObject(_local13).showInAutomationHierarchy = true;
                    };
                    if ((_local13 is Repeater)){
                        _local14 = Repeater(_local13);
                        _local14.owner = this;
                        _local14.reindexDescendants(_local11, _local12);
                    } else {
                        container.setChildIndex(DisplayObject(_local13), _local12);
                    };
                    _local10++;
                };
                _local3++;
            };
            _currentIndex = -1;
        }
        private function get numCreatedChildren():int{
            var _local3:IFlexDisplayObject;
            var _local4:Repeater;
            var _local1:int;
            var _local2:int;
            while (_local2 < createdComponents.length) {
                _local3 = createdComponents[_local2];
                if ((_local3 is Repeater)){
                    _local4 = Repeater(_local3);
                    _local1 = (_local1 + _local4.numCreatedChildren);
                } else {
                    _local1 = (_local1 + 1);
                };
                _local2++;
            };
            return (_local1);
        }
        private function removeChildRepeater(_arg1:Container, _arg2:Repeater):void{
            var _local3:int;
            var _local4:int = _arg1.childRepeaters.length;
            while (_local3 < _local4) {
                if (_arg1.repeaters[_local3] == _arg2){
                    _arg1.repeaters.splice(_local3, 1);
                    break;
                };
                _local3++;
            };
        }
        private function removeAllChildren(_arg1:IContainer):void{
            var _local4:IRepeaterClient;
            var _local2:int = _arg1.numChildren;
            var _local3:int = (_local2 - 1);
            while (_local3 >= 0) {
                _local4 = IRepeaterClient(_arg1.getChildAt(_local3));
                if (hasDescendant(_local4)){
                    if ((_local4 is Container)){
                        removeAllChildren(Container(_local4));
                        removeAllChildRepeaters(Container(_local4));
                    };
                    _arg1.removeChildAt(_local3);
                    if ((_local4 is IDeferredInstantiationUIComponent)){
                        IDeferredInstantiationUIComponent(_local4).deleteReferenceOnParentDocument(IFlexDisplayObject(parentDocument));
                    };
                };
                _local3--;
            };
        }
        public function get currentItem():Object{
            var result:* = null;
            var message:* = null;
            if (_currentIndex == -1){
                message = resourceManager.getString("core", "notExecuting");
                throw (new Error(message));
            };
            if (iterator){
                try {
                    iterator.seek(CursorBookmark.FIRST, _currentIndex);
                    result = iterator.current;
                } catch(itemPendingError:ItemPendingError) {
                    itemPendingError.addResponder(new ItemResponder(responderResultHandler, responderFaultHandler));
                };
            };
            return (result);
        }
        private function resetRepeaterIndices(_arg1:IRepeaterClient, _arg2:int):void{
            var _local4:Container;
            var _local5:int;
            var _local6:int;
            var _local7:IRepeaterClient;
            var _local3:Array = _arg1.repeaterIndices;
            _local3[(_local3.length - 1)] = _arg2;
            _arg1.repeaterIndices = _local3;
            if ((_arg1 is Container)){
                _local4 = Container(_arg1);
                _local5 = _local4.numChildren;
                _local6 = 0;
                while (_local6 < _local5) {
                    _local7 = IRepeaterClient(_local4.getChildAt(_local6));
                    resetRepeaterIndices(_local7, _arg2);
                    _local6++;
                };
            };
        }
        public function get recycleChildren():Boolean{
            return (_recycleChildren);
        }
        private function collectionChangedHandler(_arg1:CollectionEvent):void{
            switch (_arg1.kind){
                case CollectionEventKind.UPDATE:
                    break;
                default:
                    execute();
            };
        }
        private function getIndexForRepeater(_arg1:Repeater, _arg2:LocationInfo):void{
            var _local5:IFlexDisplayObject;
            var _local6:Repeater;
            var _local3:int;
            var _local4:int = createdComponents.length;
            while (_local3 < _local4) {
                _local5 = createdComponents[_local3];
                if (_local5 == _arg1){
                    _arg2.found = true;
                    break;
                } else {
                    if ((_local5 is Repeater)){
                        _local6 = Repeater(_local5);
                        _local6.getIndexForRepeater(_arg1, _arg2);
                        if (_arg2.found){
                            break;
                        };
                    } else {
                        _arg2.index = (_arg2.index + 1);
                    };
                };
                _local3++;
            };
        }
        private function hasDescendant(_arg1:Object):Boolean{
            var _local2:Array = _arg1.repeaters;
            if (_local2 == null){
                return (false);
            };
            var _local3:int = _local2.length;
            var _local4:int;
            while (_local4 < _local3) {
                if (_local2[_local4] == this){
                    return (true);
                };
                _local4++;
            };
            return (false);
        }
        public function initializeRepeater(_arg1:IContainer, _arg2:Boolean):void{
            _container = Container(_arg1);
            descriptorIndex = _arg1.numChildren;
            created = true;
            if (collection){
                createComponentsFromDescriptors(_arg2);
            };
            if (owner == null){
                owner = Container(_arg1);
            };
        }
        public function get dataProvider():Object{
            return (collection);
        }
        private function removeItems(_arg1:int, _arg2:int):void{
            var _local3:int;
            var _local4:IRepeaterClient;
            var _local5:int;
            var _local6:Repeater;
            if (((createdComponents) && ((createdComponents.length > 0)))){
                _local3 = (createdComponents.length - 1);
                while (_local3 >= 0) {
                    _local4 = createdComponents[_local3];
                    _local5 = getRepeaterIndex(_local4);
                    if ((((((_arg1 <= _local5)) && ((((_local5 < _arg2)) || ((_arg2 == -1)))))) || ((_local5 >= dataProvider.length)))){
                        if ((_local4 is Repeater)){
                            _local6 = Repeater(_local4);
                            removeRepeater(_local6);
                        } else {
                            if (container.contains(DisplayObject(_local4))){
                                container.removeChild(DisplayObject(_local4));
                            };
                        };
                        if ((_local4 is IDeferredInstantiationUIComponent)){
                            IDeferredInstantiationUIComponent(_local4).deleteReferenceOnParentDocument(IFlexDisplayObject(parentDocument));
                        };
                        createdComponents.splice(_local3, 1);
                    } else {
                        if ((((((_arg1 <= _local5)) && (!((_arg2 == -1))))) && ((_local5 >= _arg2)))){
                            adjustIndices(_local4, ((_arg2 - _arg1) + 1));
                            if ((_local4 is IDeferredInstantiationUIComponent)){
                                IDeferredInstantiationUIComponent(_local4).executeBindings(true);
                            };
                        };
                    };
                    _local3--;
                };
            };
        }
        private function getIndexForFirstChild():int{
            var _local5:IFlexDisplayObject;
            var _local6:Repeater;
            var _local1:LocationInfo = new LocationInfo();
            var _local2:int;
            var _local3:Array = Container(container).createdComponents;
            var _local4:int = (_local3) ? _local3.length : 0;
            while (_local2 < _local4) {
                _local5 = Container(container).createdComponents[_local2];
                if (_local5 == this){
                    _local1.found = true;
                    break;
                } else {
                    if ((_local5 is Repeater)){
                        _local6 = Repeater(_local5);
                        _local6.getIndexForRepeater(this, _local1);
                        if (_local1.found){
                            break;
                        };
                    } else {
                        _local1.index = (_local1.index + 1);
                    };
                };
                _local2++;
            };
            return ((_local1.found) ? _local1.index : container.numChildren);
        }
        private function createComponentsFromDescriptors(_arg1:Boolean):void{
            var _local2:int;
            var _local3:int;
            var _local4:int;
            var _local5:int;
            var _local6:IFlexDisplayObject;
            dispatchEvent(new FlexEvent(FlexEvent.REPEAT_START));
            createdComponents = [];
            if (((((collection) && ((collection.length > 0)))) && (((collection.length - startingIndex) > 0)))){
                _local2 = positiveMin((collection.length - startingIndex), count);
                _local3 = 0;
                while (_local3 < _local2) {
                    _currentIndex = (startingIndex + _local3);
                    dispatchEvent(new FlexEvent(FlexEvent.REPEAT));
                    if (((childDescriptors) && ((childDescriptors.length > 0)))){
                        _local4 = childDescriptors.length;
                        _local5 = 0;
                        while (_local5 < _local4) {
                            _local6 = createComponentFromDescriptor(_local3, _local5, _arg1);
                            createdComponents.push(_local6);
                            if ((_local6 is IUIComponent)){
                                IUIComponent(_local6).owner = this;
                            };
                            if ((_local6 is IAutomationObject)){
                                IAutomationObject(_local6).showInAutomationHierarchy = true;
                            };
                            _local5++;
                        };
                    };
                    _local3++;
                };
                _currentIndex = -1;
            };
            dispatchEvent(new FlexEvent(FlexEvent.REPEAT_END));
        }
        private function sort():void{
            execute();
        }
        private function removeRepeater(_arg1:Repeater):void{
            _arg1.removeAllChildren(_arg1.container);
            _arg1.removeAllChildRepeaters(Container(_arg1.container));
            removeChildRepeater(Container(container), _arg1);
            _arg1.deleteReferenceOnParentDocument(IFlexDisplayObject(parentDocument));
            _arg1.dataProvider = null;
        }
        public function executeChildBindings():void{
            var _local3:IRepeaterClient;
            var _local1:int = container.numChildren;
            var _local2:int;
            while (_local2 < _local1) {
                _local3 = IRepeaterClient(container.getChildAt(_local2));
                if (((hasDescendant(_local3)) && ((_local3 is IDeferredInstantiationUIComponent)))){
                    IDeferredInstantiationUIComponent(_local3).executeBindings();
                };
                _local2++;
            };
        }
        public function get currentIndex():int{
            var _local1:String;
            if (_currentIndex == -1){
                _local1 = resourceManager.getString("core", "notExecuting");
                throw (new Error(_local1));
            };
            return (_currentIndex);
        }
        public function set startingIndex(_arg1:int):void{
            _startingIndex = _arg1;
            execute();
            dispatchEvent(new Event("startingIndexChanged"));
        }
        private function responderFaultHandler(_arg1:Object, _arg2:Object):void{
        }
        override public function toString():String{
            return (((Object(container).toString() + ".") + super.toString()));
        }
        private function recreate():void{
            removeAllChildren(container);
            removeAllChildRepeaters(Container(container));
            var _local1:int = container.numChildren;
            var _local2:int = getIndexForFirstChild();
            createComponentsFromDescriptors(true);
            if (_local1 != _local2){
                reindexDescendants(_local1, _local2);
            };
        }
        public function get startingIndex():int{
            return (_startingIndex);
        }
        private function getRepeaterIndex(_arg1:IRepeaterClient):int{
            var _local2:Array = _arg1.repeaters;
            if (_local2 == null){
                return (-1);
            };
            var _local3:int = _local2.length;
            var _local4:int;
            while (_local4 < _local3) {
                if (_local2[_local4] == this){
                    return (_arg1.repeaterIndices[_local4]);
                };
                _local4++;
            };
            return (-1);
        }
        private function execute():void{
            if (!created){
                return;
            };
            if (((((recycleChildren) && (createdComponents))) && ((createdComponents.length > 0)))){
                recycle();
            } else {
                recreate();
            };
        }
        override public function set showInAutomationHierarchy(_arg1:Boolean):void{
        }
        public function set recycleChildren(_arg1:Boolean):void{
            _recycleChildren = _arg1;
        }
        public function set dataProvider(_arg1:Object):void{
            var _local3:XMLList;
            var _local4:Array;
            var _local2:Boolean;
            if (collection){
                _local2 = true;
                collection.removeEventListener(CollectionEvent.COLLECTION_CHANGE, collectionChangedHandler);
                collection = null;
                iterator = null;
            };
            if ((_arg1 is Array)){
                collection = new ArrayCollection((_arg1 as Array));
            } else {
                if ((_arg1 is ICollectionView)){
                    collection = ICollectionView(_arg1);
                } else {
                    if ((_arg1 is IList)){
                        collection = new ListCollectionView(IList(_arg1));
                    } else {
                        if ((_arg1 is XMLList)){
                            collection = new XMLListCollection((_arg1 as XMLList));
                        } else {
                            if ((_arg1 is XML)){
                                _local3 = new XMLList();
                                _local3 = (_local3 + _arg1);
                                collection = new XMLListCollection(_local3);
                            } else {
                                if (_arg1 != null){
                                    _local4 = [_arg1];
                                    collection = new ArrayCollection(_local4);
                                };
                            };
                        };
                    };
                };
            };
            if (collection){
                collection.addEventListener(CollectionEvent.COLLECTION_CHANGE, collectionChangedHandler, false, 0, true);
                iterator = collection.createCursor();
            };
            dispatchEvent(new Event("collectionChange"));
            if (((collection) || (_local2))){
                execute();
            };
        }
        private function updateItems(_arg1:int, _arg2:int):void{
            var _local3:int;
            var _local4:int;
            var _local5:IRepeaterClient;
            var _local6:int;
            if (recycleChildren){
                _local3 = container.numChildren;
                _local4 = 0;
                while (_local4 < _local3) {
                    _local5 = IRepeaterClient(container.getChildAt(_local4));
                    _local6 = getRepeaterIndex(_local5);
                    if (((((!((_local6 == -1))) && ((_arg1 <= _local6)))) && ((_local6 <= _arg2)))){
                        if ((_local5 is IDeferredInstantiationUIComponent)){
                            IDeferredInstantiationUIComponent(_local5).executeBindings(true);
                        };
                    };
                    _local4++;
                };
            } else {
                removeItems(_arg1, _arg2);
                addItems(_arg1, _arg2);
            };
        }

    }
}//package mx.core 

class LocationInfo {

    public var index:int = 0
    public var found:Boolean = false

    public function LocationInfo(){
    }
}
