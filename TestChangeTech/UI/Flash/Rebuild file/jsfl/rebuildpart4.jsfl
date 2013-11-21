
var doc=fl.getDocumentDOM();
var tl=doc.getTimeline();
var lib=doc.library;
var newSel=new Array();
var si,li,ci,pi,tx,r0,nr,cx,cy;

doc.docClass="_Main_mx_managers_SystemManager"
//create main timeline
tl=doc.getTimeline();
tl.addNewLayer();
tl.layers[0].name="empty";
tl.setSelectedFrames([0,0,0],true);
tl.setSelectedLayers(0);
tl.insertFrames(1);
doc.selectNone();
tl.addNewLayer();
tl.layers[0].name="labels";
tl.setSelectedFrames([0,0,0],true);
tl.layers[0].frames[0].name="_Main_mx_managers_SystemManager";
tl.layers[0].frames[0].labelType="name";
doc.selectNone();
tl.setSelectedFrames([0,1,1],true);
tl.convertToBlankKeyframes(1);
tl.layers[0].frames[1].name="Main";
tl.layers[0].frames[1].labelType="name";
doc.selectNone();
