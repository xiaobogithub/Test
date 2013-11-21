<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AddProgram.aspx.cs" Inherits="ChangeTech.DeveloperWeb.AddProgram" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>Add program overview</h1>
  <div class="headermenu"></div>
  <div class="clear"></div>
</div>

<div style="height:580px;">
<div class="box-main">
  <table>
    <tr>
      <td style="width:35%;">&nbsp;</td>
      <td style="width:30%;">&nbsp;</td>
      <td style="width:35%;">&nbsp;</td>
    </tr>
    <tr>
        <td><p class="name"><asp:Literal ID="Literal1" runat="server" Text="<%$ resources:ProgramName %>"></asp:Literal> : </p></td>
        <td>
            <asp:TextBox ID="txtProgramName" runat="server" CssClass="textfield-largetext"></asp:TextBox><span style="color: Red"></span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"  ErrorMessage="*" ControlToValidate="txtProgramName"></asp:RequiredFieldValidator>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name"><asp:Literal ID="Literal2" runat="server" Text="<%$ resources:ProgramDescription %>"></asp:Literal> : </p></td>
        <td>
                <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="3" CssClass="textfield-largetext"></asp:TextBox><span style="color: Red"></span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"  ErrorMessage="*" ControlToValidate="txtDescription"></asp:RequiredFieldValidator>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name"><asp:Literal ID="Literal7" Text="<%$ Resources:LogoPicture%>" runat="server"></asp:Literal> : </p></td>
      <td><label for="fileUpload"></label>
        <asp:FileUpload ID="fileUpload" runat="server" />
        <div class="flashlogo"></div></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
        <td></td>
        <td align="right">
                <asp:Button ID="btnAdd" runat="server" CssClass="button-update" Text="<%$ resources:Share,Add %>" OnClick="btnAdd_Click" />
                <asp:Button ID="btnCancel" runat="server" CssClass="button-delete" Text="<%$ resources:Share,Cancel %>" CausesValidation="false" OnClick="btnCancel_Click" />
                <%--<asp:HyperLink ID="HyperLink1" runat="server"  CssClass="button-update" Text = "<%$ resources:Share,Cancel %>" NavigateUrl="ListProgram.aspx"></asp:HyperLink>--%>
        </td>
      <td>&nbsp;</td>
    </tr>
  </table>
    </div>
    <p>&nbsp;</p>
        <%--<tr>
            <td>
                <asp:Literal ID="Literal3" runat="server" Text="<%$ resources:DefaultLanguage %>"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="defaultLanguageDropdownlist" runat="server" DataTextField="Name" DataValueField="LanguageGUID"></asp:DropDownList>
            </td>
        </tr>--%>
        <%--<tr>
            <td>
                <asp:Literal ID="Literal4" runat="server" Text="<%$ resources:ProgramLanguage %>"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                <asp:CheckBoxList ID="cblLanguage" runat="server" RepeatColumns="7" DataTextField="Name" DataValueField="LanguageGUID"></asp:CheckBoxList>
            </td>
        </tr>--%>
        </div>
</asp:Content>
