<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="halsoprofil-setup-independent.aspx.cs" Inherits="ChangeTech.DeveloperWeb.HealthProfileSystem.halsoprofil_setup_independent" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head  runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
<style type="text/css">
@charset "UTF-8";
/* ----------------------------------------------------*/

/* GENERAL SETTINGS  */





body {
	font-family: Arial, Helvetica, sans-serif;
	color: #000;
	font-size: 1.0em;
}
p, img, table, td, body, h1, h2, h3, h4 {
	border: 0;
	margin: 0;
	padding: 0;
}
h1, h2 {
	font-family: Georgia, "Times New Roman", Times, serif;
	font-weight: normal;
	font-size: 1.5em;
	color: #000;
	padding-bottom: 5px;
}
h1.large {
	font-size: 2.5em;
}
h3 {
	font-size: 1.0em;
	color: #000;
	padding-bottom: 5px;
}
h4 {
	font-size: 1.2em;
	font-weight: normal;
	padding-bottom: 5px;
	color: #000;
}
td {
	vertical-align: top;
}
ul {
	list-style-type: none;
	padding: 0;
	margin: 0;
}
ol {
	list-style-type: disc;
}
ol li {
	padding-bottom: 20px;
	padding-left: 5px;
	margin-left: -15px;
}
hr {
	clear: both;
	border: none;
	border-top: 1px dashed #b2b2b2;
	margin-top: 10px;
	margin-bottom: 10px;
}
a {
	color: #000;
	text-decoration: none;
}
a:hover {
	color: #b5111d;
}
.emptyline {
	line-height: 50%;
}
.clear {
	clear: both;
}
object, embed {
	outline: 0;
}
 input::-moz-focus-inner {
 border: 0;
}
.hidden {
	display: none;
}
.infotext {
	font-size: 0.8em;
}
.justifiedtext {
	text-align: justify;
}
.narrowtext {
	letter-spacing: -5px;
}
.narrowtext2 {
	letter-spacing: -3px;
}
.smallertextonfrontpage {
	font-size: 13px;
	line-height: 150%;
}
/* End */













/* The Site nav */

#wrapper-sitenav-large {
	margin: 0 auto;
	width: 100%;
	margin-bottom: -20px;
	border-bottom: 2px solid #b5111c;
	background: #eee;
	position: fixed;
	z-index: 99999;
}
.sitenav {
	margin: 0 auto;
	width: 890px;
}
.logo-halsoportalen {
	float: left;
	height: 69px;
}
.logo-halsoportalen h1 {
	font-family: Georgia, "Times New Roman", Times, serif;
	font-size: 2.5em;
	color: #000;
	padding: 10px 10px 0 0;
}
.logosalad {
	float: right;
	width: 50px;
	height: 69px;
}
.logosalad img {
	margin: 10px 0 0 7px;
}
.payoff {
	float: left;
	display: block;
	font-size: 0.8em;
	color: #666;
	padding-top: 25px;
	line-height: 95%;
}
.payoff-for-print {
	display: none;
}
/* The globalmenu within the sitenav */

ul.dropdown, ul.dropdown2 {
	margin: 0 auto;
	display: block;
}
ul.dropdown li {
	display: block;
	float: left;
}
ul.dropdown2 li {
	display: block;
	float: right;
}
ul.dropdown a:hover, ul.dropdown2 a:hover {
	color: #FFF;
}
ul.dropdown li a, ul.dropdown2 li a {
	display: block;
	margin: 0px 2px;
	padding: 4px 12px 0 12px;
	height: 20px;
	text-decoration: none;
	font-size: 0.85em;
	font-weight: normal;
	color: #eee;
	background: #999;
	-moz-border-radius: 3px;
	-webkit-border-radius: 3px;
	border-radius: 3px;
}
ul.dropdown li a.active, ul.dropdown2 li a.active {
	display: block;
	padding: 4px 12px 0 12px;
	height: 24px;
	text-decoration: none;
	font-size: 1.0em;
	font-weight: normal;
	color: #FFF;
	-moz-border-radius: 0px;
	-webkit-border-radius: 0px;
	border-radius: 0px;
	-moz-border-radius-topleft: 3px;
	-webkit-border-top-left-radius: 3px;
	border-top-left-radius: 3px;
	-moz-border-radius-topright: 3px;
	-webkit-border-top-right-radius: 3px;
	border-top-right-radius: 3px;
	background: #b20000;
}
ul.dropdown li.hover, ul.dropdown li:hover, ul.dropdown2 li.hover, ul.dropdown2 li:hover {
	position: relative;
}
ul.dropdown li a:hover, ul.dropdown2 li a:hover {
	background: #aaa;
}
ul.dropdown li.hover .dropdown-program.active {
	background: #b81800;
}
ul.dropdown2 li a.dropdown-minhalsoprofil {
	float: right;
}
ul.dropdown li a:hover.active, ul.dropdown2 li a:hover.active {
	background: #b81800;
}
/* 

	LEVEL TWO

*/

.backgroundshader {
	width: 910px;
	margin: 6px -10px 0 -10px;
	background: url(../gfx/bg-dropdownmenu.png) repeat-y;
}
.backgroundshader-end {
	width: 910px;
	height: 125px;
	margin: 0 -10px 0 -10px;
	background: url(../gfx/bg-dropdownmenu-end.png) no-repeat;
}
ul.dropdown ul.pop-submenu {
	width: 890px;
	visibility: hidden;
	position: absolute;
	top: 100%;
	left: 0;
}
ul.dropdown ul li {
	display: block;
}
ul.dropdown ul li a {
	background: none;
}
.pop-submenu {
	width: 890px;
	margin-left: -155px;
}
.pop-submenu ul {
	display: block;
}
.pop-submenu li a {
	display: block;
	margin: 0 auto;
	width: 865px;
	height: 60px;
}
.pop-submenu li a.program-stressamindre, .pop-submenu li a.program-mabattre, .pop-submenu li a.program-slutaroka, .pop-submenu li a.program-litesundare, .pop-submenu li a.program-minskaalkohol {
	height: 60px;
	margin: 5px 0 0 10px;
	border-radius: 0;
	background: url(../gfx/arrow-dropdownmenu-idle.png) 830px 15px no-repeat #FFF;
}
.pop-submenu li a:hover.program-stressamindre {
	background: url(../gfx/arrow-dropdownmenu-stressamindre-hover.png) 830px 15px no-repeat #8f9ab6;
}
.pop-submenu li a:hover.program-mabattre {
	background: url(../gfx/arrow-dropdownmenu-mabattre-hover.png) 830px 15px no-repeat #c9644f;
}
.pop-submenu li a:hover.program-slutaroka {
	background: url(../gfx/arrow-dropdownmenu-slutaroka-hover.png) 830px 15px no-repeat #79afb1;
}
.pop-submenu li a:hover.program-litesundare {
	background: url(../gfx/arrow-dropdownmenu-litesundare-hover.png) 830px 15px no-repeat #3facd8;
}
.pop-submenu li a:hover.program-minskaalkohol {
	background: url(../gfx/arrow-dropdownmenu-minskaalkohol-hover.png) 830px 15px no-repeat #ce9767;
}
/* End */



































/* The main area */

.wrapper-main {
	margin: 0 auto;
	width: 890px;
	padding-top: 20px;
}
#wrapper-facts {
	width: 100%;
	padding: 0px 0;
}
#wrapper-facts h3 {
	float: left;
	width: 280px;
	padding: 10px 0 20px 0;
	text-transform: uppercase;
	font-size: 1.2em;
	font-weight: normal;
}
#supportform h3 {
	float: left;
	width: 280px;
	padding: 0px 0 0px 0;
	text-transform: uppercase;
	font-size: 1.2em;
	font-weight: normal;
}
#wrapper-facts h4 {
	float: left;
	width: 585px;
	padding: 12px 0 20px 0;
	font-size: 1.0em;
	font-weight: normal;
	margin-left: 25px;
}
.infolist li {
	border-bottom: 1px dashed #999999;
	padding: 10px 0 10px 10px;
}
.infolist li.decor {
	padding: 10px 0;
	margin-left: 30px;
	list-style-type: disc;
	border-bottom: none;
}
.infolist li.header {
	border-bottom: 1px solid #999;
	margin-top: -2px;
	margin-bottom: 10px;
}
.infolist li .level2 li {
	border-bottom: none;
	padding: 10px 0 0 0;
	margin-left: 20px;
	list-style-type: square;
}
.infolist li:last-child {
	border-bottom: none;
}
.col150 {
	float: left;
	width: 150px;
	padding: 0px 0;
}
.col160 {
	float: left;
	width: 160px;
	padding: 0px 0;
}
.col200 {
	float: left;
	width: 200px;
	padding: 0px 0;
}
.col210 {
	float: left;
	width: 210px;
	padding: 0px 0;
}
.col250 {
	float: left;
	width: 250px;
	padding: 0px 0;
}
.col280 {
	float: left;
	width: 280px;
	padding: 0px 0;
}
.box-halsoprofil .col280 {
	float: left;
	width: 280px;
	padding: 20px 0 0 0;
}
#supportform .col280 {
	padding: 20px 0;
}
.col350 {
	float: left;
	width: 300px;
	padding: 20px 0;
}
.col430 {
	float: left;
	width: 430px;
	padding: 20px 0;
}
.col430-print {
	float: left;
	width: 430px;
	padding: 20px 0;
}
.col430-print-right {
	float: left;
	width: 430px;
	padding: 20px 0;
	margin-left: 30px;
}
.col460 {
	float: left;
	width: 460px;
	padding: 20px 0;
}
.col585 {
	float: left;
	width: 585px;
	padding: 0px 0;
}
#supportform .col585 {
	padding: 20px 0;
}
.col890 {
	float: left;
	width: 890px;
	padding: 20px 0;
}
.col890-print {
	clear: both;
	float: left;
	width: 890px;
	padding: 20px 0;
	margin-bottom: -10px;
}
.col890-midjastuss-midjastuss {
	clear: both;
	float: left;
	width: 470px;
	padding: 20px 400px 20px 20px;
	min-height: 250px;
	background: url(../gfx/ill-midjastuss-midjastuss.png) right top no-repeat #eee;
	border: 1px solid #ddd;
	border-radius: 8px;
}
.col890-midjastuss-bukhojd {
	clear: both;
	float: left;
	width: 470px;
	padding: 20px 400px 20px 20px;
	min-height: 250px;
	background: url(../gfx/ill-midjastuss-bukhojd.png) right top no-repeat #eee;
	border: 1px solid #ddd;
	border-radius: 8px;
}
#breadcrumbs {
	float: left;
	width: 890px;
	padding: 20px 0 0 0;
	font-size: 0.9em;
}
.col890 h1, .col890-print h1 {
	font-family: Georgia, "Times New Roman", Times, serif;
	font-size: 2.2em;
	font-weight: 100;
}
.col890pcp {
	float: left;
	width: 430px;
	padding: 20px 0 0 460px;
	background: url(../gfx/motive-aboutpcp.jpg) bottom no-repeat;
}
h3.statement {
	font-family: Georgia, "Times New Roman", Times, serif;
	font-weight: normal;
	font-size: 1.1em;
}
.spacing-left-50px {
	margin-left: 50px;
}
.spacing-left-30px {
	margin-left: 30px;
}
.spacing-left-25px {
	margin-left: 25px;
}
.spacing-left-20px {
	margin-left: 20px;
}
.line-spacer {
	line-height: 150%;
}
.site-payoff {
	width: 100%;
	padding-bottom: 10px;
	text-align: center;
}
.site-payoff-2 {
	width: 100%;
	padding-bottom: 0px;
	text-align: left;
}
.site-payoff h1, .site-payoff-2 h1 {
	font-family: Georgia, "Times New Roman", Times, serif;
	font-weight: normal;
	font-size: 1.4em;
	color: #60554e;
}
.box-halsoprofil {
	margin: 0 auto;
	width: 890px;
	min-height: 250px;
	padding: 0px;
	background: url(../gfx/halsoprofilen-motive-frontpage.jpg) top right no-repeat #f0f0ef;
	-webkit-border-radius: 8px;
	-moz-border-radius: 8px;
	border-radius: 8px;
}
.box-halsoprofil h2 {
	margin-top: 20px;
	border-bottom: 1px dashed #b2b2b2;
}
.box-halsolinjen {
	float: left;
	width: 240px;
	min-height: 450px;
	margin-left: 25px;
	margin-top: 0px;
	padding: 20px 20px 20px 20px;
	/*border: 1px solid #CCC;*/

	background: url(../gfx/samtalsstod-motive-frontpage.jpg) 0 bottom no-repeat #FFF;
	-webkit-border-radius: 8px;
	-moz-border-radius: 8px;
	border-radius: 8px;
}
.box-halsolinjen h2 {
	border-bottom: 1px dashed #b2b2b2;
	margin: 0 -20px 20px -20px;
	padding-left: 20px;
}
.box-halsolinjen .actionplate, #wrapper-mainmotive-halsolinjen .actionplate {
	width: 430px;
	margin-left: 0px;
	margin-top: 20px;
	text-align: center;
}
.box-halsolinjen .actionplate h1, #wrapper-mainmotive-halsolinjen .actionplate h1 {
	font-size: 2.0em;
}
.box-programmer {
	float: left;
	width: 585px;
	padding: 20px 0px 0px 0px;
}
.box-programmer h2 {
	border-bottom: 1px dashed #b2b2b2;
	margin-bottom: 20px;
}
.tablespacing {
	width: 25px;
	padding: 0;
	margin: 0;
}
a.prioritymail {
	display: block;
	min-height: 25px;
	font-size: 0.9em;
	font-weight: bold;
	text-decoration: none;
	color: #000;
	padding: 2px 5px 2px 25px;
	background: url(../gfx/arrow-priority-email-idle.png) 0 1px no-repeat;
}
a:hover.prioritymail {
	color: #7c1008;
	background: url(../gfx/arrow-priority-email-hover.png) 0 1px no-repeat;
}
.retake, .retake a:hover {
	-webkit-border-radius: 8px;
	-moz-border-radius: 8px;
	border-radius: 8px;
}
#wrapper-mainmotive .actionplate {
	width: 430px;
	margin: 20px 0;
}
.col430 .actionplate {
	width: 430px;
}
.box-halsoprofil .actionplate {
	width: 280px;
	margin: 23px 0 25px 25px;
}
.box-halsolinjen .actionplate {
	width: 240px;
	margin-top: 70px;
}
.box-halsolinjen .upper {
	padding: 10px 10px 10px 10px;
}
.box-halsolinjen .upper h3 {
	font-size: 1.1em;
}
.box-halsolinjen .lower {
	padding: 10px 10px 15px 10px;
}
.actionplate .upper {
	background-color: #b20000;
	/*background: url(../gfx/bg-actionplate-red-idle.png) bottom repeat-x #aa1315;*/

	-moz-border-radius-topleft: 8px;
	-webkit-border-top-left-radius: 8px;
	border-top-left-radius: 8px;
	-moz-border-radius-topright: 8px;
	-webkit-border-top-right-radius: 8px;
	border-top-right-radius: 8px;
}
.actionplate .andlower {
	background-color: #b20000;
	/*background: url(../gfx/bg-actionplate-red-idle.png) bottom repeat-x #aa1315;*/

	-moz-border-radius-bottomright: 8px;
	-webkit-border-bottom-right-radius: 8px;
	border-bottom-right-radius: 8px;
	-moz-border-radius-bottomleft: 8px;
	-webkit-border-bottom-left-radius: 8px;
	border-bottom-left-radius: 8px;
}
#wrapper-textbubble .actionplate {
	margin: 25px 0 25px 80px;
	width: 270px;
}
#wrapper-textbubble .actionplate .upper {
	-webkit-border-radius: 8px;
	-moz-border-radius: 8px;
	border-radius: 8px;
	padding: 0 0px 5px 0;
}
.actionplate .upper .actionlink a, .actionplate .lower .actionlink a {
	display: block;
	padding: 10px 10px 10px 10px;
}
li.upper a:hover {
	background-color: #810200;
	-moz-border-radius-topleft: 8px;
	-webkit-border-top-left-radius: 8px;
	border-top-left-radius: 8px;
	-moz-border-radius-topright: 8px;
	-webkit-border-top-right-radius: 8px;
	border-top-right-radius: 8px;
}
#wrapper-textbubble .actionplate .upper .actionlink a {
	display: block;
	padding: 5px;
}
.upper h1 {
	font-family: Arial, Helvetica, sans-serif;
	font-size: 0.9em;
	font-weight: bold;
	text-transform: uppercase;
	color: #FFF;
	margin: 0 0 0 0;
	padding: 0 0 0 0;
}
#wrapper-textbubble .upper h1 {
	padding: 5px 0 0 0;
}
.col430 .actionplate .upper h1 {
	font-family: Arial, Helvetica, sans-serif;
	font-size: 1.2em;
	font-weight: bold;
	text-transform: uppercase;
	color: #FFF;
}
.upper h3 {
	font-size: 0.8em;
	font-weight: normal;
	color: #f2d0d0;
	margin: 0 0 0 0;
	padding: 0 0 0 0;
}
.lower h1 {
	font-family: Arial, Helvetica, sans-serif;
	font-size: 0.9em;
	font-weight: bold;
	text-transform: uppercase;
	color: #783f3a;
	margin: 0 0 0 0;
	padding: 0 0 0 0;
}
.col430 .actionplate .lower h1 {
	font-family: Arial, Helvetica, sans-serif;
	font-size: 1.2em;
	font-weight: bold;
	text-transform: uppercase;
	color: #000;
}
.lower h3 {
	font-size: 0.8em;
	font-weight: normal;
	margin: 0 0 0 0;
	padding: 0 0 0 0;
}
.actionplate .lower {
	background-color: #ccc;
	-moz-border-radius-bottomright: 8px;
	-webkit-border-bottom-right-radius: 8px;
	border-bottom-right-radius: 8px;
	-moz-border-radius-bottomleft: 8px;
	-webkit-border-bottom-left-radius: 8px;
	border-bottom-left-radius: 8px;
}
li.lower a:hover {
	background-color: #aaa;
	-moz-border-radius-bottomright: 8px;
	-webkit-border-bottom-right-radius: 8px;
	border-bottom-right-radius: 8px;
	-moz-border-radius-bottomleft: 8px;
	-webkit-border-bottom-left-radius: 8px;
	border-bottom-left-radius: 8px;
}
li.andlower a:hover {
	-moz-border-radius-bottomright: 8px;
	-webkit-border-bottom-right-radius: 8px;
	border-bottom-right-radius: 8px;
	-moz-border-radius-bottomleft: 8px;
	-webkit-border-bottom-left-radius: 8px;
	border-bottom-left-radius: 8px;
}
.time-20min {
	float: left;
	width: 50px;
	height: 40px;
	background: url(../gfx/time-20min.png) no-repeat;
	margin-top: 0px;
	margin-left: 5px;
}
.box-halsoprofil .time-20min {
	width: 45px;
	margin-top: -2px;
}
.time-1min {
	float: left;
	width: 50px;
	height: 40px;
	background: url(../gfx/time-1min.png) 3px 0 no-repeat;
	margin-top: -1px;
	margin-left: 5px;
}
.box-halsoprofil .time-1min {
	width: 45px;
	margin-top: -2px;
}
.print-profile {
	float: left;
	width: 40px;
	height: 60px;
	background: url(../gfx/icon-print.png) 0 -2px no-repeat;
}
.actionplate .upper .actionlink .arrow {
	float: right;
	width: 32px;
	height: 36px;
	background: url(../gfx/arrow-actionplate-red-idle.png) 0 0px no-repeat;
}
.dialoguebox-main input.buttonclass {
	background: url("../gfx/arrow-actionbutton-program-idle.png") no-repeat scroll right center #7C1008;
	border: medium none;
	border-radius: 6px 6px 6px 6px;
	color: #FFFFFF;
	display: block;
	float: right;
	font-size: 1em;
	margin: 2px 0 0;
	padding: 11px 11px 11px 20px;
	text-align: left;
	text-transform: uppercase;
	width: 230px;
}
input:hover.buttonclass {
	background: url(../gfx/arrow-actionbutton-program-idle.png) right no-repeat #9D180E;
	-webkit-border-radius: 6px;
	-moz-border-radius: 6px;
	border-radius: 6px;
	cursor: pointer;
}
.actionplate .upper.retake .actionlink .arrow {
/*background-position: 0 3px;*/

}
.actionplate .upper.contact {
	padding: 10px 0px 10px 25px;
	text-align: left;
}
.actionplate .upper.contact h1, .actionplate .lower.contact h1 {
	font-size: 2.0em;
	margin-top: 0px;
	padding-right: 30px;
	float: right;
}
.actionplate .roundedcornersextra {
	border-radius: 8px;
}
.actionplate .upper.contact h3 {
	margin-top: 6px;
	font-size: 1.0em;
	line-height: 90%;
	float: left;
}
.actionplate .lower.contact2 h3 {
	margin-top: 6px;
	font-size: 1.0em;
	line-height: 90%;
	float: left;
}
.actionplate .lower.contact h3 {
	font-size: 1.0em;
	text-align: center;
}
.actionplate .upper.contact h4 {
	font-size: 0.8em;
	font-weight: normal;
	color: #b7736d;
}
.actionplate .lower.contact h4 {
	font-size: 0.8em;
	font-weight: normal;
}
.actionplate .lower.contact {
	padding: 10px 0px 10px 25px;
	text-align: center;
}
.actionplate .lower .actionlink .arrow {
	float: right;
	width: 32px;
	height: 36px;
	background: url(../gfx/arrow-actionplate-beige-idle.png) 0 0px no-repeat;
}
.programlabel {
	clear: both;
	float: left;
	width: 280px;
	margin-bottom: 20px;
	padding: 90px 0 0 0;
	background: #FC0;
	-webkit-border-radius: 5px;
	-moz-border-radius: 5px;
	border-radius: 5px;
}
.divider {
	float: left;
	width: 25px;
	height: 25px;
}
.programlabel h3 {
	padding: 6px 25px 3px 0;
	font-size: 1.0em;
	text-transform: uppercase;
	color: #FFF;
}
.programlabel.program-stressamindre {
	background: url(../gfx/stressamindre-motive-small.jpg) no-repeat;
}
.programlabel.program-mabattre {
	background: url(../gfx/mabattre-motive-small.jpg) no-repeat;
}
.programlabel.program-slutaroka {
	background: url(../gfx/roykeslutt-motive-small.jpg) no-repeat;
}
.programlabel.program-litesundare {
	background: url(../gfx/litesundare-motive-small.jpg) no-repeat;
}
.programlabel.program-minskaalkohol {
	background: url(../gfx/minskaalkohol-motive-small.jpg) no-repeat;
}
.programlabel .motive {
	display: none;
	position: relative;
	float: left;
	left: 0px;
	top: 0;
	z-index: 10;
}
.programlabel .host {
	float: left;
	position: relative;
	width: 48px;
	height: 48px;
	margin: -26px 10px 12px 15px;
	z-index: 40000;
}
.programlabel h3 {
	float: left;
	position: relative;
	margin: 0px 0 0 0px;
	z-index: 40000;
}
.programlabel .programribbon {
	min-height: 36px;
	margin-bottom: -36px;
	-moz-border-radius-bottomright: 5px;
	-webkit-border-bottom-right-radius: 5px;
	border-bottom-right-radius: 5px;
	-moz-border-radius-bottomleft: 5px;
	-webkit-border-bottom-left-radius: 5px;
	border-bottom-left-radius: 5px;
}
.backgroundcolors {
	display: none;
	position: relative;
	left: 0;
	top: 0;
	z-index: 30000;
}
.programlabel.program-stressamindre .programribbon {
	background: url(../gfx/arrow-programlabel-stressamindre-idle.png) 250px 6px no-repeat #8f9ab6;
}
a:hover .programlabel.program-stressamindre .programribbon {
	background-color: #7783a1;
}
.programlabel.program-mabattre .programribbon {
	background: url(../gfx/arrow-programlabel-mabattre-idle.png) 250px 6px no-repeat #c9644f;
}
a:hover .programlabel.program-mabattre .programribbon {
	background-color: #b55440;
}
.programlabel.program-slutaroka .programribbon {
	background: url(../gfx/arrow-programlabel-slutaroka-idle.png) 250px 6px no-repeat #79afb1;
}
a:hover .programlabel.program-slutaroka .programribbon {
	background-color: #629799;
}
.programlabel.program-litesundare .programribbon {
	background: url(../gfx/arrow-programlabel-litesundare-idle.png) 250px 6px no-repeat #3facd8;
}
a:hover .programlabel.program-litesundare .programribbon {
	background-color: #3097c1;
}
.programlabel.program-minskaalkohol .programribbon {
	background: url(../gfx/arrow-programlabel-minskaalkohol-idle.png) 250px 6px no-repeat #ce9767;
}
a:hover .programlabel.program-minskaalkohol .programribbon {
	background-color: #b58154;
}
#programribbon a.actionlink {
	float: right;
	display: block;
	width: 240px;
	padding: 6px 10px;
	margin: 20px 20px 0 0;
	text-transform: uppercase;
	font-size: 1.0em;
	color: #FFF;
	background: url(../gfx/arrow-actionbutton-program-idle.png) right no-repeat #265e5f;
	-webkit-border-radius: 6px;
	-moz-border-radius: 6px;
	border-radius: 6px;
}
.program-stressamindre #programribbon a.actionlink {
	background: url(../gfx/arrow-actionbutton-program-idle.png) right no-repeat #485278;
}
.program-mabattre #programribbon a.actionlink {
	background: url(../gfx/arrow-actionbutton-program-idle.png) right no-repeat #9a3d2a;
}
.program-slutaroka #programribbon a.actionlink {
	background: url(../gfx/arrow-actionbutton-program-idle.png) right no-repeat #265e5f;
}
.program-litesundare #programribbon a.actionlink {
	background: url(../gfx/arrow-actionbutton-program-idle.png) right no-repeat #2e516a;
}
.program-minskaalkohol #programribbon a.actionlink {
	background: url(../gfx/arrow-actionbutton-program-idle.png) right no-repeat #854f3f;
}
#programribbon a:hover.actionlink {
	background: url(../gfx/arrow-actionbutton-program-idle.png) right no-repeat #1c4546;
	-webkit-border-radius: 6px;
	-moz-border-radius: 6px;
	border-radius: 6px;
}
.program-stressamindre #programribbon a:hover.actionlink {
	background: url(../gfx/arrow-actionbutton-program-idle.png) right no-repeat #3a4364;
}
.program-mabattre #programribbon a:hover.actionlink {
	background: url(../gfx/arrow-actionbutton-program-idle.png) right no-repeat #873423;
}
.program-slutaroka #programribbon a:hover.actionlink {
	background: url(../gfx/arrow-actionbutton-program-idle.png) right no-repeat #1b4546;
}
.program-litesundare #programribbon a:hover.actionlink {
	background: url(../gfx/arrow-actionbutton-program-idle.png) right no-repeat #223c4f;
}
.program-minskaalkohol #programribbon a:hover.actionlink {
	background: url(../gfx/arrow-actionbutton-program-idle.png) right no-repeat #6c4033;
}
/* End */







#wrapper-mainmotive {
	margin: 0 auto;
	width: 890px;
}
#wrapper-mainmotive-halsolinjen {
	margin: 0 auto;
	width: 890px;
}
/* The program page */

.mainmotive {
	margin: 0 auto;
	width: 100%;
	padding-top: 100px;
	margin-bottom: -120px;
}
.mainmotive.halsoprofil {
	background: url(../gfx/halsoprofilen-motive-large.jpg) center 75px no-repeat #e6e8e8;
	padding-bottom: 0px;
}
.mainmotive.samtalstod {
	background: url(../gfx/samtalsstod-motive-large.jpg) center 97px no-repeat #e6e8e8;
}
.mainmotive.program-stressamindre {
	background: url(../gfx/stressamindre-motive-large.jpg) bottom center no-repeat #e2e7eb;
}
.mainmotive.program-mabattre {
	background: url(../gfx/mabattre-motive-large.jpg) bottom center no-repeat #fef6e4;
}
.mainmotive.program-slutaroka {
	background: url(../gfx/roykeslutt-motive-large.jpg) bottom center no-repeat #ddebfb;
}
.mainmotive.program-litesundare {
	background: url(../gfx/litesundare-motive-large.jpg) bottom center no-repeat #d8f3fe;
}
.mainmotive.program-minskaalkohol {
	background: url(../gfx/minskaalkohol-motive-large.jpg) bottom center no-repeat #e9e2dc;
}
.mainmotive.minhalsoprofil {
	background: url(../gfx/bg-hostmotives.png) bottom repeat-x #FFF;
	border-bottom: 1px solid #bdccd6;
	padding-top: 98px;
}
#programribbon {
	margin: 0 auto;
	width: 890px;
	height: 90px;
	margin-top: 280px;
	background: #999;
	-moz-border-radius-topleft: 10px;
	-webkit-border-top-left-radius: 10px;
	border-top-left-radius: 10px;
	-moz-border-radius-topright: 10px;
	-webkit-border-top-right-radius: 10px;
	border-top-right-radius: 10px;
}
.program-stressamindre #programribbon {
	background: #8f9ab6;
}
.program-mabattre #programribbon {
	background: #c9644f;
}
.program-slutaroka #programribbon {
	background: #79afb1;
}
.program-litesundare #programribbon {
	background: #3facd8;
}
.program-minskaalkohol #programribbon {
	background: #ce9767;
}
#programribbon h1 {
	font-family: Arial, Helvetica, sans-serif;
	font-size: 3.0em;
	font-weight: bold;
	text-transform: uppercase;
	color: #FFF;
	padding: 4px 0 0 0;
}
#programribbon h3 {
	font-family: Arial, Helvetica, sans-serif;
	font-size: 1.2em;
	font-weight: normal;
	color: #FFF;
	padding: 0;
	margin-top: -8px;
}
.program-minskaalkohol #programribbon h3 {
	padding-top: 3px;
	font-size: 1.0em;
}
.host {
	width: 100px;
	height: 100px;
	border: 2px solid #FFF;
	background: #CCC;
	float: left;
	margin: -52px 20px 30px 35px;
}
.pop-submenu .host {
	float: left;
	width: 48px;
	height: 48px;
	margin: 2px 20px 4px 10px;
}
.pop-submenu a h1 {
	font-family: Arial, Helvetica, sans-serif;
	font-size: 1.4em;
	font-weight: bold;
	text-transform: uppercase;
	color: #265e5f;
	padding: 7px 0 0 0;
}
.pop-submenu a h3 {
	font-family: Arial, Helvetica, sans-serif;
	font-size: 1.0em;
	font-weight: normal;
	color: #79afb1;
	padding: 0;
	margin-top: -4px;
}
.pop-submenu a.program-stressamindre h1 {
	color: #485278;
}
.pop-submenu a.program-stressamindre h3 {
	color: #8f9ab6;
}
.pop-submenu a.program-mabattre h1 {
	color: #9a3d2a;
}
.pop-submenu a.program-mabattre h3 {
	color: #c9644f;
}
.pop-submenu a.program-slutaroka h1 {
	color: #265e5f;
}
.pop-submenu a.program-slutaroka h3 {
	color: #79afb1;
}
.pop-submenu a.program-litesundare h1 {
	color: #2e516a;
}
.pop-submenu a.program-litesundare h3 {
	color: #3facd8;
}
.pop-submenu a.program-minskaalkohol h1 {
	color: #854f3f;
}
.pop-submenu a.program-minskaalkohol h3 {
	color: #ce9767;
}
.pop-submenu a:hover h1, .pop-submenu a:hover h3 {
	color: #FFF;
}
/* End */





















/* PROGRAMLIST TEXT and LINKS */

li.programlist a {
	float: left;
	width: 208px;
	min-height: 40px;
	display: block;
	text-decoration: none;
	padding: 10px 50px 10px 20px;
	margin: -1px -21px 0 -21px;
	border-top: 1px dotted #c8d3da;
	background: url(../gfx/actionarrow.png) 240px 20px no-repeat;
}
li.programlist a:hover {
	background: url(../gfx/actionarrow-hover.png) 240px 20px no-repeat;
}
.programlist h3 {
	text-transform: uppercase;
	font-weight: bold;
	padding-bottom: 2px;
	line-height: normal;
}
.programlist a:hover h3, .programlist a:hover h4 {
	color: #7c1008;
}
.programlist h4 {
	font-size: 0.9em;
	font-weight: normal;
	line-height: 100%;
	color: #646b7a;
}
.program-bedrehverdag h3 {
	color: #656c42;
}
.program-hadetbedre h3 {
	color: #9a3d2a;
}
.program-littsunnere h3 {
	color: #3a77b5;
}
.program-stressemindre h3 {
	color: #485278;
}
.program-roykeslutt h3 {
	color: #265e5f;
}
.program-balance h3 {
	color: #854f3f;
}
.program-oppdrabarn h3 {
	color: #77800e;
}
/* End */









/* The dialogue box */

#wrapper-dialoguebox {
	position: absolute;
	margin: 0 auto;
	width: 100%;
	/*z-index: 90;*/

	z-index: 90000;
	padding-top: 110px;
}
.dialoguebox-top {
	margin: 0 auto;
	width: 910px;
	height: 27px;
	background: url(../gfx/bg-dialoguebox-top.png) no-repeat;
}
.dialoguebox-main {
	clear: both;
	margin: 0 auto;
	width: 650px;
	padding: 0 130px;
	background: url(../gfx/bg-dialoguebox.png) repeat-y;
}
.dialoguebox-end {
	clear: both;
	margin: 0 auto;
	width: 910px;
	height: 320px;
	background: url(../gfx/bg-dialoguebox-end.png) bottom no-repeat;
}
.dialoguebox-main a.prioritylink {
	float: right;
	margin-right: -100px;
	display: block;
	min-height: 25px;
	font-size: 0.9em;
	font-weight: bold;
	text-decoration: none;
	color: #000;
	padding: 2px 27px 2px 5px;
	background: url(../gfx/b-close.png) right 2px no-repeat;
}
.dialoguebox-main a:hover.prioritylink {
	color: #7c1008;
}
.dialoguebox-main h4 {
	font-size: 0.9em;
}
.textfield01 {
	width: 360px;
	padding: 5px 5px 5px 10px;
	border: 1px solid #ccc;
	-webkit-border-radius: 6px;
	-moz-border-radius: 6px;
	border-radius: 6px;
	font-size: 1.5em;
}
.textfield02 {
	width: 568px;
	padding: 5px 5px 5px 10px;
	border: 1px solid #ccc;
	-webkit-border-radius: 6px;
	-moz-border-radius: 6px;
	border-radius: 6px;
	font-size: 1.5em;
	margin-top: -8px;
}
.textfield03 {
	width: 568px;
	padding: 5px 5px 5px 10px;
	margin-top: -5px;
	border: 1px solid #ccc;
	-webkit-border-radius: 6px;
	-moz-border-radius: 6px;
	border-radius: 6px;
	font-size: 1.2em;
}
#supportform a.actionlink {
	float: right;
	display: block;
	width: 200px;
	padding: 11px 11px 11px 20px;
	margin: 2px 0px 0 0;
	text-transform: uppercase;
	text-align: left;
	font-size: 1.0em;
	color: #FFF;
	background: url(../gfx/arrow-actionbutton-program-idle.png) right no-repeat #7c1008;
	border: none;
	-webkit-border-radius: 6px;
	-moz-border-radius: 6px;
	border-radius: 6px;
}
#supportform a:hover.actionlink {
	background: url(../gfx/arrow-actionbutton-program-idle.png) right no-repeat #9d180e;
}
.dialoguebox-main a.actionlink {
	float: right;
	display: block;
	width: 200px;
	padding: 11px 11px 11px 20px;
	margin: 2px 0px 0 0;
	text-transform: uppercase;
	text-align: left;
	font-size: 1.0em;
	color: #FFF;
	background: url(../gfx/arrow-actionbutton-program-idle.png) right no-repeat #7c1008;
	border: none;
	-webkit-border-radius: 6px;
	-moz-border-radius: 6px;
	border-radius: 6px;
}
.dialoguebox-main a:hover.actionlink {
	background: url(../gfx/arrow-actionbutton-program-idle.png) right no-repeat #9d180e;
}
.alertmessage {
	padding: 12px 15px;
	border: 1px solid #900;
	background: #f8dbdb;
	font-size: 1.1em;
	color: #794642;
	margin-bottom: 20px;
	text-align: center;
	-webkit-border-radius: 6px;
	-moz-border-radius: 6px;
	border-radius: 6px;
}
.confirmationmessage {
	margin: 2px 0px 0 0;
	float: left;
	width: 270px;
	padding: 10px 35px;
	border: 1px solid #5b842d;
	background: #e1f0c9;
	font-size: 1.0em;
	color: #085d23;
	margin-bottom: 20px;
	text-align: center;
	-webkit-border-radius: 6px;
	-moz-border-radius: 6px;
	border-radius: 6px;
}
#supportform .alertmessage {
	margin: 2px 0px 0 0;
	float: left;
	width: 270px;
	padding: 10px 35px;
	border: 1px solid #900;
	background: #f8dbdb;
	font-size: 1.0em;
	color: #794642;
	margin-bottom: 20px;
	text-align: center;
	-webkit-border-radius: 6px;
	-moz-border-radius: 6px;
	border-radius: 6px;
}
/* End */





/* The host presentation on the profile test results page */

#wrapper-hostpresentation {
	margin: 0 auto;
	width: 890px;
	min-height: 340px;
	background: url(../gfx/host-motive1.png) 30px 10px no-repeat;
}
#wrapper-hostpresentation-void {
	margin: 0 auto;
	width: 890px;
	min-height: 340px;
	background: url(../gfx/host-motive-void.png) 30px 10px no-repeat;
}
.hostpresentation-for-print {
	display: none;
	width: 400px;
	float: left;
}
#wrapper-textbubble {
	width: 430px;
	margin-left: 460px;
	padding-top: 30px;
}
.textbubble-top {
	width: 390px;
	background: url(../gfx/textbubble-top.png) 0 0 no-repeat;
	padding: 30px 30px 0 30px;
	font-family: Georgia, "Times New Roman", Times, serif;
	font-size: 1.0em;
	color: #435b6c;
	margin-left: -9px;
}
.textbubble-top h2 {
	font-size: 1.3em;
	margin-bottom: 0px;
	padding-bottom: 0px;
	color: #000;
}
.textbubble-bottom {
	width: 450px;
	height: 36px;
	background: url(../gfx/textbubble-bottom.png) 0 bottom no-repeat;
	margin-left: -9px;
}
/* End */





/* The health profile results list */

.totalreport {
	display: none;
}
.results {
	display: block;
	margin-top: 30px;
}
.results a li {
	float: left;
	width: 100%;
	display: block;
	border: 1px solid #bdccd6;
	-webkit-border-radius: 8px;
	-moz-border-radius: 8px;
	border-radius: 8px;
	margin-bottom: 15px;
}
.results a:hover li {
	background-color: #e2ebf1;
}
.results a:hover.green li {
	background-color: #FFF;
	cursor: default;
}
.results img {
	float: left;
	padding-left: 10px;
}
.results h3 {
	float: left;
	padding: 15px 0 15px 20px;
	text-transform: uppercase;
	font-size: 1.2em;
	font-weight: normal;
}
.results h4 {
	float: right;
	padding: 18px 0 10px 0px;
	text-transform: uppercase;
	font-size: 0.9em;
	font-weight: bold;
	color: #435a6d;
	width: 350px;
	text-align: left;
	background: url(../gfx/arrow-results-small.png) 320px 16px no-repeat;
}
.results h5 {
	display: none;
	float: right;
	padding: 18px 20px 0px 0px;
	margin: 0;
	text-transform: uppercase;
	font-size: 0.9em;
	font-weight: bold;
}
.results .resultcolor-red, .results .resultcolor-yellow, .results .resultcolor-green {
	width: 60px;
	height: 30px;
	position: relative;
	float: left;
	left: 450px;
	top: 11px;
	z-index: 10;
}
.results .red .resultcolor-yellow, .results .red .resultcolor-green {
	display: none;
}
.results .yellow .resultcolor-red, .results .yellow .resultcolor-green {
	display: none;
}
.results .green .resultcolor-red, .results .green .resultcolor-yellow {
	display: none;
}
.results .icon {
	float: left;
	width: 60px;
	height: 54px;
	margin-left: -60px;
}
/*.results h3.result-tobacco {

	background: url(../gfx/icons-results.png) 5px -6px no-repeat;

}

.results h3.result-alcohol {

	background: url(../gfx/icons-results.png) 5px -76px no-repeat;

}

.results h3.result-drugs {

	background: url(../gfx/icons-results.png) 5px -146px no-repeat;

}

.results h3.result-diet {

	background: url(../gfx/icons-results.png) 5px -216px no-repeat;

}

.results h3.result-exercise {

	background: url(../gfx/icons-results.png) 5px -286px no-repeat;

}

.results h3.result-life {

	background: url(../gfx/icons-results.png) 5px -357px no-repeat;

}

.results h3.result-stress {

	background: url(../gfx/icons-results.png) 5px -427px no-repeat;

}

.results h3.result-waist {

	background: url(../gfx/icons-results.png) 5px -496px no-repeat;

}*/

a.red li {
	background: url(../gfx/resultcolors.png) 460px -144px no-repeat;
}
a.yellow li {
	background: url(../gfx/resultcolors.png) 460px -67px no-repeat;
}
a.green li {
	background: url(../gfx/resultcolors.png) 460px 11px no-repeat;
}
a.green li h4 {
	display: none;
}
a.green li h5 {
	display: none;
}
/*a.red li h5.yellow, a.red li h5.green  {

	display: none;

}

a.yellow li h5.red, a.yellow li h5.green  {

	display: none;

}

a.green li h5.yellow, a.green li h5.red  {

	display: none;

}*/
.redcolor {
	color: #C00;
}

.wrapper-resultcategory {
	margin: 0 auto;
	min-height: 340px;
}
.wrapper-resultcategory.result-tobacco {
	background: url(../gfx/icon-tobacco.png) top center no-repeat;
}
.wrapper-resultcategory.result-alcohol {
	background: url(../gfx/icon-alcohol.png) top center no-repeat;
}
.wrapper-resultcategory.result-drugs {
	background: url(../gfx/icon-drugs.png) top center no-repeat;
}
.wrapper-resultcategory.result-diet {
	background: url(../gfx/icon-diet.png) top center no-repeat;
}
.wrapper-resultcategory.result-exercise {
	background: url(../gfx/icon-exercise.png) top center no-repeat;
}
.wrapper-resultcategory.result-life {
	background: url(../gfx/icon-life.png) top center no-repeat;
}
.wrapper-resultcategory.result-stress {
	background: url(../gfx/icon-stress.png) top center no-repeat;
}
.wrapper-resultcategory.result-waist {
	background: url(../gfx/icon-waist.png) top center no-repeat;
}
.wrapper-resultcategory-content {
	margin: 0 auto;
	width: 890px;
}
.wrapper-resultcategory-content .icon {
	display: none;
}
.minhalsoprofil .actionplate {
	font-size: 0.9em;
	width: 205px;
	margin-top: 20px;
}
.minhalsoprofil .actionplate .upper, .minhalsoprofil .actionplate .upper a:hover {
	-webkit-border-radius: 6px;
	-moz-border-radius: 6px;
	border-radius: 6px;
}
.minhalsoprofil .actionplate .upper .actionlink .arrow {
	float: left;
	width: 20px;
	height: 20px;
	background: url(../gfx/arrow-back.png) 0 0px no-repeat;
}
.minhalsoprofil h1.large {
	font-size: 3.5em;
}
.minhalsoprofil h4 {
	color: #000;
}
.wrapper-recommendation {
	display: block;
	width: 888px;
	margin: 20px 0;
}
.wrapper-recommendation li {
	display: block;
	height: 126px;
	background: url(../gfx/arrow-results.png) 820px 35px no-repeat #eee;
	border: 1px solid #ccc;
	-webkit-border-radius: 8px;
	-moz-border-radius: 8px;
	border-radius: 8px;
	margin-bottom: 20px;
}
.wrapper-recommendation a:hover li {
	background-color: #dedede;
}
.wrapper-recommendation h2 {
	padding: 15px 100px 0 300px;
}
.wrapper-recommendation h4 {
	font-size: 1.0em;
	color: #000;
	padding: 0px 100px 0 300px;
}
.wrapper-recommendation .programlabel {
	margin-bottom: 0;
	-moz-border-radius-topleft: 7px;
	-webkit-border-top-left-radius: 7px;
	border-top-left-radius: 7px;
	-moz-border-radius-topright: 0px;
	-webkit-border-top-right-radius: 0px;
	border-top-right-radius: 0px;
	-moz-border-radius-bottomright: 0px;
	-webkit-border-bottom-right-radius: 0px;
	border-bottom-right-radius: 0px;
	-moz-border-radius-bottomleft: 7px;
	-webkit-border-bottom-left-radius: 7px;
	border-bottom-left-radius: 7px;
}
.wrapper-recommendation .programlabel .programribbon {
	margin-bottom: 0;
	-moz-border-radius-topleft: 0px;
	-webkit-border-top-left-radius: 0px;
	border-top-left-radius: 0px;
	-moz-border-radius-topright: 0px;
	-webkit-border-top-right-radius: 0px;
	border-top-right-radius: 0px;
	-moz-border-radius-bottomright: 0px;
	-webkit-border-bottom-right-radius: 0px;
	border-bottom-right-radius: 0px;
	-moz-border-radius-bottomleft: 7px;
	-webkit-border-bottom-left-radius: 7px;
	border-bottom-left-radius: 7px;
}
.wrapper-recommendation .actionplate {
	margin: 0;
	width: 280px;
	height: 86px;
	-webkit-box-shadow: none;
	-moz-box-shadow: none;
	box-shadow: none;
	border: none;
}
.wrapper-recommendation .actionplate .upper {
	margin: 0;
	padding: 20px 10px 10px 17px;
	display: block;
	height: 96px;
	border: none;
	-moz-border-radius-topleft: 7px;
	-webkit-border-top-left-radius: 7px;
	border-top-left-radius: 7px;
	-moz-border-radius-topright: 0px;
	-webkit-border-top-right-radius: 0px;
	border-top-right-radius: 0px;
	-moz-border-radius-bottomright: 0px;
	-webkit-border-bottom-right-radius: 0px;
	border-bottom-right-radius: 0px;
	-moz-border-radius-bottomleft: 7px;
	-webkit-border-bottom-left-radius: 7px;
	border-bottom-left-radius: 7px;
}
.wrapper-recommendation a:hover .actionplate .upper {
	background: #810200;
}
.wrapper-recommendation .actionplate .upper h3 {
	font-size: 1.0em;
}
.wrapper-recommendation .actionplate .upper h1 {
	font-size: 2.5em;
}
/* End */













/* The footer */

#footer {
	margin: 0 auto;
	width: 100%;
	min-height: 100px;
	padding: 50px 0 80px 0;
	border-top: 8px solid #b5111c;
	background: #eee;
}
.footer-content {
	margin: 0 auto;
	width: 890px;
	text-align: center;
}
#footer a {
	font-weight: bold;
}
/* End */







.focusbox {
	border-radius: 8px;
	border: 1px solid #CCC;
	background: url(/HealthProfileSystem/gfx/bg-setup-foretag.jpg) right -60px no-repeat;
}
.focusbox .left {
	float: left;
	padding: 30px;
	border-right: 1px solid #CCC;
	border-radius: 8px 0 0 8px;
	width: 200px;
	background-color: #FFF;
}
.focusbox .right {
	float: left;
	margin: 70px 0 70px 200px;
	text-align: center;
}
a.actionlink {
	display: block;
	width: 170px;
	padding: 11px 11px 11px 20px;
	margin: 2px 0px 0 0;
	text-transform: uppercase;
	text-align: left;
	font-size: 1.0em;
	color: #FFF;
	background: url(/HealthProfileSystem/gfx/helseprofil-startpage-gui.png) 150px -8px no-repeat #7c1008;
	border: none;
	-webkit-border-radius: 6px;
	-moz-border-radius: 6px;
	border-radius: 6px;
}
a:hover.actionlink {
	background-color: #9d180e;
}
a.prioritylink {
	display: block;
	min-height: 25px;
	font-size: 0.9em;
	font-weight: bold;
	text-decoration: none;
	color: #000;
	padding: 2px 5px 2px 25px;
	background: url(/HealthProfileSystem/gfx/helseprofil-startpage-gui.png) no-repeat;
	background-position: -20px -60px;
}
a:hover.prioritylink {
	color: #7c1008;
	background: url(/HealthProfileSystem/gfx/helseprofil-startpage-gui.png) no-repeat;
	background-position: -20px -100px;
}
.sosilogo {
	margin: 0 auto;
	width: 60px;
	height: 90px;
	background: url(/HealthProfileSystem/gfx/helseprofil-startpage-gui.png) no-repeat;
	background-position: -20px -140px;
}
</style>
<meta name="viewport" content="width=950, user-scalable=yes, target-densitydpi=low-dpi;" />
<title>Hälsoprofil företag</title>
</head>
<body>
    <form id="form1" runat="server">
<div class="wrapper-main">
   <div id="offlineDiv" class="col890 hidden" runat="server">
      <h2 align="center">Hälsoprofilen med Företagsrapport er inte tillgjänglig just nu.</h2>
      <p class="line-spacer" align="center"> Beklagar men den tidsperiod som ditt företag har valt för att genomföra hälsoprofilen är inte aktiv. </p>
      <p class="line-spacer" align="center">Om företagets tidsperiod har gått ut, är du välkommen att genomföra en individuell hälsoprofil om du loggar in via <a class="redcolor" href="https://lansforsakringar.soshalsa.eu">portalen</a>!</p>
      <p>&nbsp;</p>
      <p>&nbsp;</p>
      <p>&nbsp;</p>
      <p>&nbsp;</p>
      <hr />
    </div>

    <div id="onlineDiv" runat="server">
      <div class="col890">
        <h2>Hälsoprofil företag</h2>
        <p class="line-spacer">Du kommer nu att få svara på ett antal frågor som handlar om din livsstil och hälsa. Ditt resultat kommer både att redovisas i en personling rapport direkt till dig samtidigt som dina svar ingår i en gemensam sammanställning för företaget. Du behöver avsätta ca 20 minuter för att genomföra hälsoprofilen. När du trycker på start accepterar du också <a class="redcolor" href="http://program.changetech.no/RequestResource.aspx?target=Document&amp;media=644aad37-4ff4-419e-bd61-c74026b393e2.pdf&amp;name=Användarvillkor Hälsoprofil.pdf">villkoren i hälsoprofil</a>.</p>
      </div>
      <div class="col890">
        <div class="focusbox">
          <div class="left">
            <h3>Företag</h3>
            <p><asp:Label ID="lblCustomerName" runat="server" Text=""></asp:Label></p>
            <p>&nbsp;</p>
            <h3>Beställare</h3>
            <p><asp:Label ID="lblCustomerContactPersonName" runat="server" Text="Label"></asp:Label></p>
            <p>&nbsp;</p>
            <h3>Antal medarbetare</h3>
            <p><asp:Label ID="lblNumberOfUsers" runat="server" Text="Label"></asp:Label></p>
            <p>&nbsp;</p>
            <p>&nbsp;</p>
            <p> 
                    <asp:LinkButton ID="actionLink" runat="server" CssClass="actionlink" OnClick="actionLink_Click" Text="Start"/>
                 <%--<a id="actionLink" class="actionlink" href="javascript:__doPostBack(&#39;actionLink&#39;,&#39;&#39;)">Start</a>--%>
             </p>
          </div>
          <div class="right"> </div>
          <div class="clear"></div>
        </div>
      </div>
      <div class="col890">
        <h4>Sekretess</h4>
        <p>De uppgifter du lägger in blir fullständigt avidentifierade och kan inte knytas till dig som en enskild individ. Företagsrapporten måste minst omfatta tio individer för att säkerställa att ingen enskild person ska kunna bli igenkänd.</p>
        <p>&nbsp;</p>
        <p><a class="prioritylink" href="https://lansforsakringar.soshalsa.eu/om.html#pul">Läs mer om sekretess på Länsförsäkringars hälsotjänster</a></p>
        <p>&nbsp;</p>
      </div>
    </div>
    <div class="col890">
      <p align="center">
      <div class="sosilogo"></div>
      </p>
    </div>
    <div class="clear"></div>
  </div>
</form>
</body>
</html>
