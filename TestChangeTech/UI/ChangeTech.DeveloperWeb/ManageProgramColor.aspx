<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ManageProgramColor.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ManageProgramColor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="Scripts/jquery/jquery.js" type="text/javascript"></script>

    <script src="Scripts/jquery/ifx.js" type="text/javascript"></script>

    <script src="Scripts/jquery/idrop.js" type="text/javascript"></script>

    <script src="Scripts/jquery/idrag.js" type="text/javascript"></script>

    <script src="Scripts/jquery/iutil.js" type="text/javascript"></script>

    <script src="Scripts/jquery/islider.js" type="text/javascript"></script>

    <script src="Scripts/jquery/color_picker/color_picker.js" type="text/javascript"></script>

    <link href="Scripts/jquery/color_picker/color_picker.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="header" style="padding:10px;">
    <h1>Manage program color overview</h1>
        <div class="headermenu"></div>
        <div class="clear"></div>
    </div>
    <div style="height:580px;" >
    <div class="box-main">
    <table>
    <tr>
      <td style="width:35%;">&nbsp;</td>
      <td style="width:30%;">&nbsp;</td>
      <td style="width:35%;">&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name"><asp:Literal ID="Literal2" Text="Is cover shadow visible" runat="server"></asp:Literal></p></td>
      <td><asp:CheckBox ID="coverShadowVisibleCheckBox" runat="server" /></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name"><span style="font-weight:bold;">Primary button color</span></p></td>
      <td>&nbsp;</td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name"><asp:Literal ID="Literal9" Text="Body and top line color" runat="server"></asp:Literal></p></td>
      <td><div style="float: left; width: 65px; display: block">
                    <asp:TextBox ID="lineColorText" runat="server" Width="60px"></asp:TextBox></div>
                <div style="float: left">
                    <a href="javascript:void(0);" rel="colorpicker&objcode=ctl00_ContentPlaceHolder1_lineColorText&objshow=myshowcolor&showrgb=1"
                        style="text-decoration: none;">
                        <div id="myshowcolor" style="width: 15px; height: 15px; border: 1px solid black;">
                            &nbsp;</div>
                    </a>
                </div>
                <%--<input id="bodyAndTopLineColor" type="text" runat="server" />
                <a href="javascript:void(0);" rel="colorpicker&objcode=bodyAndTopLineColor&objshow=myshowcolor&showrgb=1" style="text-decoration:none;" ><div id="myshowcolor" style="width:15px;height:15px;border:1px solid black;">&nbsp;</div></a>--%></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name"><asp:Literal ID="Literal1" Text="Cover shadow color" runat="server"></asp:Literal></p></td>
      <td>
                <div style="float: left; width: 65px; display: block">
                    <asp:TextBox ID="shadowTextBox" runat="server" Width="60px"></asp:TextBox></div>
                <div style="float: left">
                    <a href="javascript:void(0);" rel="colorpicker&objcode=ctl00_ContentPlaceHolder1_shadowTextBox&objshow=Div9&showrgb=1"
                        style="text-decoration: none;">
                        <div id="Div9" style="width: 15px; height: 15px; border: 1px solid black;">
                            &nbsp;</div>
                    </a>
                </div></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name">Normal color</p></td>
      <td>
            <div style="float: left; width: 65px; display: block">
                    <asp:TextBox ID="normalFromTextBox" runat="server" Width="60px"></asp:TextBox></div>
                <div style="float: left">
                    <a href="javascript:void(0);" rel="colorpicker&objcode=ctl00_ContentPlaceHolder1_normalFromTextBox&objshow=Div1&showrgb=1"
                        style="text-decoration: none;">
                        <div id="Div1" style="width: 15px; height: 15px; border: 1px solid black;">
                            &nbsp;</div>
                    </a>
                </div>
                <div style="float: left; width: 65px; display: block">
                    <asp:TextBox ID="normalToTextBox" runat="server" Width="60px"></asp:TextBox></div>
                <div style="float: left">
                    <a href="javascript:void(0);" rel="colorpicker&objcode=ctl00_ContentPlaceHolder1_normalToTextBox&objshow=Div2&showrgb=1"
                        style="text-decoration: none;">
                        <div id="Div2" style="width: 15px; height: 15px; border: 1px solid black;">
                            &nbsp;</div>
                    </a>
                </div>
      </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name">Mouse over color</p></td>
      <td>
             <div style="float: left; width: 65px; display: block">
                    <asp:TextBox ID="overFromTextBox" runat="server" Width="60px"></asp:TextBox></div>
                <div style="float: left">
                    <a href="javascript:void(0);" rel="colorpicker&objcode=ctl00_ContentPlaceHolder1_overFromTextBox&objshow=Div3&showrgb=1"
                        style="text-decoration: none;">
                        <div id="Div3" style="width: 15px; height: 15px; border: 1px solid black;">
                            &nbsp;</div>
                    </a>
                </div>
                <div style="float: left; width: 65px; display: block">
                    <asp:TextBox ID="overToTextBox" runat="server" Width="60px"></asp:TextBox></div>
                <div style="float: left">
                    <a href="javascript:void(0);" rel="colorpicker&objcode=ctl00_ContentPlaceHolder1_overToTextBox&objshow=Div4&showrgb=1"
                        style="text-decoration: none;">
                        <div id="Div4" style="width: 15px; height: 15px; border: 1px solid black;">
                            &nbsp;</div>
                    </a>
                </div>
      </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name">Click down color</p></td>
      <td>
            <div style="float: left; width: 65px; display: block">
                    <asp:TextBox ID="DownFromTextBox" runat="server" Width="60px"></asp:TextBox></div>
                <div style="float: left">
                    <a href="javascript:void(0);" rel="colorpicker&objcode=ctl00_ContentPlaceHolder1_DownFromTextBox&objshow=Div5&showrgb=1"
                        style="text-decoration: none;">
                        <div id="Div5" style="width: 15px; height: 15px; border: 1px solid black;">
                            &nbsp;</div>
                    </a>
                </div>
                <div style="float: left; width: 65px; display: block">
                    <asp:TextBox ID="DownToTextBox" runat="server" Width="60px"></asp:TextBox></div>
                <div style="float: left">
                    <a href="javascript:void(0);" rel="colorpicker&objcode=ctl00_ContentPlaceHolder1_DownToTextBox&objshow=Div6&showrgb=1"
                        style="text-decoration: none;">
                        <div id="Div6" style="width: 15px; height: 15px; border: 1px solid black;">
                            &nbsp;</div>
                    </a>
                </div>
      </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name">Disabled color</p></td>
      <td>
            <div style="float: left; width: 65px; display: block">
                    <asp:TextBox ID="disableFromTextBox" runat="server" Width="60px"></asp:TextBox></div>
                <div style="float: left">
                    <a href="javascript:void(0);" rel="colorpicker&objcode=ctl00_ContentPlaceHolder1_disableFromTextBox&objshow=Div7&showrgb=1"
                        style="text-decoration: none;">
                        <div id="Div7" style="width: 15px; height: 15px; border: 1px solid black;">
                            &nbsp;</div>
                    </a>
                </div>
                <div style="float: left; width: 65px; display: block">
                    <asp:TextBox ID="disableToTextBox" runat="server" Width="60px"></asp:TextBox></div>
                <div style="float: left">
                    <a href="javascript:void(0);" rel="colorpicker&objcode=ctl00_ContentPlaceHolder1_disableToTextBox&objshow=Div8&showrgb=1"
                        style="text-decoration: none;">
                        <div id="Div8" style="width: 15px; height: 15px; border: 1px solid black;">
                            &nbsp;</div>
                    </a>
                </div>
      </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
    <td></td>
      <td>
            <span style="padding-left:275px;">
                <asp:Button ID="Button1" runat="server" Text="Update" CssClass="button-update" OnClick="Button1_Click" />
            </span>
        </td>
      <td>&nbsp;</td>
    </tr>
    </table>
    </div>
    </div>
    <script type="text/javascript">
        $(document).ready(
				function() {
				    $.ColorPicker.init();
				}
			);
    </script>
</asp:Content>
