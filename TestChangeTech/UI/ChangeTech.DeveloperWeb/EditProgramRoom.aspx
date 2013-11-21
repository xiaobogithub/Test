<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="EditProgramRoom.aspx.cs" Inherits="ChangeTech.DeveloperWeb.EditProgramRoom" %>

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
    <div class="header">
  <h1>Edit program room overview</h1>
  <div class="headermenu"></div>
  <div class="clear"></div>
</div>
<div class="box-main">
    <table>
    <tr>
      <td style="width:35%;">&nbsp;</td>
      <td style="width:30%;">&nbsp;</td>
      <td style="width:35%;">&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name"><asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:Program %>"></asp:Literal></p></td>
      <td><asp:Literal ID="ltProgram" runat="server" Text=""></asp:Literal></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
        <td><p class="name"><asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:Share,Name %>"></asp:Literal></p></td>
        <td>
            <asp:TextBox ID="txtName" runat="server" CssClass="textfield-largetext"></asp:TextBox><span style="color: Red">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName" ErrorMessage="Required"></asp:RequiredFieldValidator>
        </td>
      <td>&nbsp;</td>
    </tr>
      <tr>
        <td><p class="name"><asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Share,Description %>"></asp:Literal></p></td>
        <td>
            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" CssClass="textfield-largetext" Rows="5"></asp:TextBox>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td>&nbsp;</td>
    </tr>
      <tr>
        <td><p class="name"><asp:Literal ID="Literal5" Text="Is cover shadow visible" runat="server"></asp:Literal></p></td>
        <td>
            <asp:CheckBox ID="coverShadowVisibleCheckBox" runat="server" />
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
        <td><p class="name"><span style="font-weight:bold;">Primary button color</span></p></td>
        <td>
            &nbsp;
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
        <td><p class="name"> Top line and body color</p></td>
        <td>
             <div style="float: left; width: 65px; display: block">
                    <asp:TextBox ID="roomColorText" runat="server" Width="60px"></asp:TextBox></div>
                <div style="float: left">
                    <a href="javascript:void(0);" rel="colorpicker&objcode=ctl00_ContentPlaceHolder1_roomColorText&objshow=myshowcolor&showrgb=1"
                        style="text-decoration: none;">
                        <div id="myshowcolor" style="width: 15px; height: 15px; border: 1px solid black;">
                            &nbsp;</div>
                    </a>
                </div>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
        <td><p class="name"><asp:Literal ID="Literal4" Text="Cover shadow color" runat="server"></asp:Literal></p></td>
        <td>
            <div style="float: left; width: 65px; display: block">
                    <asp:TextBox ID="shadowTextBox" runat="server" Width="60px"></asp:TextBox></div>
                <div style="float: left">
                    <a href="javascript:void(0);" rel="colorpicker&objcode=ctl00_ContentPlaceHolder1_shadowTextBox&objshow=Div9&showrgb=1"
                        style="text-decoration: none;">
                        <div id="Div9" style="width: 15px; height: 15px; border: 1px solid black;">
                            &nbsp;</div>
                    </a>
                </div>
        </td>
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
        <td colspan="2" align="right">
            <div class="buttons">
                <asp:Button ID="btnSave" runat="server" CssClass="button-update" Text="<%$ Resources:Share,Update %>" OnClick="btnSave_Click" />
                &nbsp;&nbsp;&nbsp; 
                <asp:Button ID="btnCancel" runat="server" CssClass="button-delete" Text="<%$ resources:Share,Cancel %>" CausesValidation="false" OnClick="btnCancel_Click"/>
            </div>
        </td>
    </tr>
    </table>
    </div>

    <script type="text/javascript">
        $(document).ready(
				function() {
				    $.ColorPicker.init();
				}
			);
    </script>

</asp:Content>
