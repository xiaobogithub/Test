function intializeTr() {
    $("tr:odd").addClass("tr_odd");
    $("tr:even").addClass("tr_even");
}
$(function(){
	intializeTr();
});