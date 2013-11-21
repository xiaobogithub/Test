<%@ Page Title="Email Template" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="EditEmailTemplate.aspx.cs" Inherits="ChangeTech.DeveloperWeb.EditEmailTemplate"
    ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="header">
        <h1>
            Edit Email Template
        </h1>
        <div class="headermenu">
        </div>
        <div class="clear">
        </div>
    </div>
    <p>&nbsp;</p>
    <b><asp:Label ID="programLabel" runat="server" Text="" Font-Bold="true" Font-Size="Medium"></asp:Label></b>
    <p>&nbsp;</p>
    <p class="guide">Comment: For Reminder or Resend password, please use '{LinkName}' represent the real login link, like '{UserPassword}' in Resend password template. </p>
    <p>&nbsp;</p>
    <div class="box-main">
    <table>
     <tr>
      <td style="width:25%;">&nbsp;</td>
      <td style="width:40%;">&nbsp;</td>
      <td style="width:35%;">&nbsp;</td>
    </tr>
    <tr>
    <td>
    <p class="name"><asp:Literal ID="Literal3" runat="server" Text="<%$ resources:EmailTemplateType %>"></asp:Literal></p>
    </td>
    <td><asp:DropDownList ID="ddlEmailTemplateType" runat="server" DataTextField="Name" DataValueField="EmailTemplateTypeGuid"
                    OnSelectedIndexChanged="ddlEmailTemplateType_SelectedIndexChanged" AutoPostBack="true" CssClass="listmenu-default">
                </asp:DropDownList></td>
        <td>&nbsp;</td>
    </tr>
    <tr>
            <td>
                <p class="name"><asp:Literal ID="Literal6" runat="server" Text="<%$ resources:Name %>"></asp:Literal></p>
            </td>
            <td>
                <asp:TextBox ID="txtName" runat="server" CssClass="textfield-largetext"></asp:TextBox>
            </td>
        <td>&nbsp;</td>
        </tr>
         <tr>
            <td>
                <p class="name"><asp:Literal ID="Literal4" runat="server" Text="<%$ resources:Subject %>"></asp:Literal></p>
            </td>
            <td>
                <asp:TextBox ID="txtSubject" runat="server" CssClass="textfield-largetext"></asp:TextBox>
            </td>
        <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <p class="name"><asp:Literal ID="Literal5" runat="server" Text="<%$ resources:Body %>"></asp:Literal></p>
            </td>
            <td>
                <asp:TextBox ID="txtBody" runat="server" TextMode="MultiLine" Height="400px" CssClass="textfield-largetext"></asp:TextBox>
            </td>
        <td>&nbsp;</td>
        </tr>
        <asp:Panel ID="linkTextPanel" Visible="false" runat="server">
            <tr>
                <td>
                    <p class="name"><asp:Literal ID="Literal7" runat="server" Text="LinkText"></asp:Literal></p>
                </td>
                <td>
                    <asp:TextBox ID="linkTextTextBox" runat="server"  CssClass="textfield-largetext"></asp:TextBox>
                </td>
        <td>&nbsp;</td>
            </tr>
        </asp:Panel>
        <tr>
        <td><p class="divider">&nbsp</p></td>
        <td><p class="divider">&nbsp;</p></td>
        <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td align="right"> 
                <asp:Button ID="btnAdd" runat="server" Text="<%$ resources:Save %>" OnClick="btnAdd_Click" CssClass="button-update" />
                <asp:Button ID="cancelButton" runat="server" Text="Cancel" CausesValidation="False" OnClick="cancelButton_Click"  CssClass="button-delete"/>
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
    <p>&nbsp;</p>
    </div>
    <table>
        <tr>
            <%--<th><asp:Literal ID="Literal1" runat="server" Text="<%$ resources:Program %>"></asp:Literal></th>
            <th><asp:Literal ID="Literal2" runat="server" Text="<%$ resources:Language %>"></asp:Literal></th>--%>
            <th></th>
        </tr>
        <tr>
          <%--  <td>
                <asp:DropDownList ID="ddlProgram" runat="server" DataTextField="ProgramName" DataValueField="ProgramGuid"
                    OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
            </td>
            <td>
                <asp:DropDownList ID="ddlLanguage" runat="server" DataTextField="Name" DataValueField="LanguageGUID"
                    OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
            </td>--%>
            <td>
                
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Label ID="labEmailTemlateTypeDescription" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <table style="width: 600px;">
        
       
        
        
        <tr>
            <td colspan="2">
               
            </td>
        </tr>
    </table>
    
</asp:Content>
