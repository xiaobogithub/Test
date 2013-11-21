
//fl.enableImmediateUpdates();
fl.outputPanel.clear();
fl.openDocument("file:///E:/work/09-36 ChangeTech/trunck/UI/Flash/Rebuild file/Main_rebuild.fla");
var doc=fl.getDocumentDOM();
var tl=doc.getTimeline();
var lib=doc.library;
var newSel=new Array();
var si,li,ci,pi,tx,r0,nr,cx,cy;

//movie properties
doc.width=500;
doc.height=375;
doc.frameRate=24;
doc.backgroundColor="#869CA7";

