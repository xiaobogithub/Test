<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="EditProfile.aspx.cs" Inherits="ChangeTech.DeveloperWeb.EditProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="header">
        <h1>
            Edit Profile
        </h1>
        <div class="headermenu">
        </div>
        <div class="clear">
        </div>
    </div>
    <div style="height:580px;">
    <div class="box-main">
    <table>
    <tr>
                <td width="35%">
                    &nbsp;
                </td>
                <td width="40%">
                    &nbsp;
                </td>
                <td width="25%">
                    &nbsp;
                </td>
            </tr>
        <tr>
            <td>
               <p class="name"> <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources: UserName %>"></asp:Literal></p>
            </td>
            <td>
                <asp:TextBox ID="txtUserName" runat="server" CssClass="textfield-largetext"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ControlToValidate="txtUserName" ErrorMessage="Invalid email address" 
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>
                <p class="name"><asp:Literal ID="Literal2" runat="server" Text="<%$ Resources: Gender %>"></asp:Literal></p>
            </td>
            <td>
                <asp:DropDownList ID="ddlGender" runat="server" CssClass="listmenu-default">
                <asp:ListItem Text = "Male" Value="Male"></asp:ListItem>
                <asp:ListItem Text = "Female" Value="Female"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <p class="name"><asp:Literal ID="Literal3" runat="server" Text="<%$ Resources: MobilePhone %>"></asp:Literal></p>
            </td>
            <td>
                <asp:TextBox ID="txtMobilePhone" runat="server" CssClass="textfield-largetext"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
        <td>
        <p class="divider">&nbsp;</p>
        </td>
        <td>
        <p class="divider">&nbsp;</p>
        </td>
        <td>&nbsp;</td>
        </tr>
        <tr>
        <td></td>
          <td>
             <asp:Button ID="btnCancle" runat="server" Text="<%$ Resources: Share,Cancel %>"  onclick="btnCancle_Click"  CssClass="button-delete"/>
            <asp:Button ID="btnUpdate" runat="server" Text="<%$ Resources: Share,Update %>" 
                onclick="btnUpdate_Click"  CssClass="button-update floatRight"/>
           </td>
          </tr>
    </table>    
    </div>
</div>
</asp:Content>
