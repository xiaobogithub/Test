<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ManatePayment.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ManatePayment"
    ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div class="header">
        <h1>Manage payment overview </h1>
        <div class="headermenu">
        </div>
        <div class="clear"></div>
    </div>   
    <b><asp:Label ID="programLabel" runat="server" Font-Bold="true" Font-Size="Medium" Text=""></asp:Label></b>
    <%--<table>
        <tr>
            <td>
                Program
            </td>
            <td>
                <asp:DropDownList ID="programDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="programDropDownList_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>
                Language
            </td>
            <td>
                <asp:DropDownList ID="languageDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="languageDropDownList_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
    </table>--%>
    <div class="box-main">
    <table>
    <tr>
      <td style="width:25%">&nbsp;</td>
      <td style="width:40%">&nbsp;</td>
      <td style="width:35%">&nbsp;</td>
    </tr>
     <tr>
            <td>
                <p class="name">Title:</p>
            </td>
            <td>
               <asp:TextBox ID="titleTextBox" runat="server" CssClass="textfield-largetext"></asp:TextBox>
            </td>
            <td >&nbsp;</td>
        </tr>
        <tr>
            <td>
                <p class="name">Text:</p>
            </td>
            <td>
               <asp:TextBox ID="textTextBox" runat="server" TextMode="MultiLine" CssClass="textfield-largetext" Rows="3"></asp:TextBox>
            </td>
            <td >&nbsp;</td>
        </tr>
        <tr>
            <td>
                <p class="name">PrimaryButtonText:</p>
            </td>
            <td>
               <asp:TextBox ID="primarybuttonTextBox" runat="server" CssClass="textfield-largetext"></asp:TextBox>
            </td>
            <td >&nbsp;</td>
        </tr>
        <tr>
            <td>
                <p class="name">Payment description:</p>
            </td>
            <td>
               <asp:TextBox ID="descriptionTextBox" runat="server" CssClass="textfield-largetext" Rows="3" TextMode="MultiLine"></asp:TextBox><br />
                
            </td>
            <td ><p class="guide"> Order description will show on collect card information page(the special page from Netaxept).</p></td>
        </tr>
         <tr>
            <td>
                <p class="name">Successful tip:</p>
            </td>
            <td>
                <asp:TextBox ID="successfulTipTextBox" runat="server" CssClass="textfield-largetext" Rows="3" TextMode="MultiLine"></asp:TextBox><br />
            </td>
            <td ><p class="guide">Paid successfully, will show this message to user.</p></td>
        </tr>
         <tr>
            <td>
                <p class="name">Exception tip:</p>
            </td>
            <td>
               <asp:TextBox ID="exceptionTipTextBox" runat="server" CssClass="textfield-largetext" Rows="3" TextMode="MultiLine"></asp:TextBox><br />
            </td>
            <td ><p class="guide">Paid not successfully, will show this message to user</p></td>
        </tr>
         <tr>
            <td>
                <p class="name">Login link text:</p>
            </td>
            <td>
              <asp:TextBox ID="loginlinkTextBox" runat="server" CssClass="textfield-largetext"></asp:TextBox><br />
               
            </td>
            <td ><p class="guide"> if there is class on paid day, will show user a link and this text is the link text.</p></td>
        </tr>
         <tr>
            <td>
                <p class="name">PrimaryButtonText:</p>
            </td>
            <td>
               <asp:TextBox ID="TextBox4" runat="server" CssClass="textfield-largetext"></asp:TextBox>
            </td>
            <td >&nbsp;</td>
        </tr>
        <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td>&nbsp;</td>
    </tr>
        <tr>
        <td>&nbsp;</td>
            <td align="right">
                <asp:Button ID="cancelButton" runat="server" Text="Cancel" OnClick="cancelButton_Click" CssClass="button-delete"/>
             <asp:Button ID="okButton" runat="server" Text="OK" OnClick="okButton_Click" CssClass="button-update"/>
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
</div>
</asp:Content>
