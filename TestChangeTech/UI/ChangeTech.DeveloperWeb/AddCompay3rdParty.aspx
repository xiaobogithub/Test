<%@ Page Title="" Language="C#" MasterPageFile="~/3rdParty.Master" AutoEventWireup="true"
    CodeBehind="AddCompay3rdParty.aspx.cs" Inherits="ChangeTech.DeveloperWeb.AddCompay3rdParty" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>Add compay3rdParty overview</h1>
  <div class="headermenu">
    
  </div>
  <div class="clear"></div>
</div>
    <div class="box-main">
    <act:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </act:ToolkitScriptManager>
    <table>
        <tr>
            <td style="width:20%;">&nbsp;</td>
            <td style="width:30%;">&nbsp;</td>
            <td style="width:20%">&nbsp;</td>
            <td style="width:30%">&nbsp;</td>
        </tr>
        <tr>
            <td>
                <p class="name">Compay name</p>
            </td>
            <td colspan="3">
                <asp:TextBox ID="companyTextBox" runat="server" CssClass="textfield-small"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="companyTextBox" ErrorMessage="Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <p class="name">Start date</p>
            </td>
            <td>
                <asp:TextBox ID="startTextBox" runat="server" CssClass="textfield-small"></asp:TextBox>
                <act:CalendarExtender ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd" TargetControlID="startTextBox">
                </act:CalendarExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="startTextBox"
                    ErrorMessage="Required"></asp:RequiredFieldValidator>
            </td>
            <td>
                <p class="name">Overdue date</p>
            </td>
            <td>
                <asp:TextBox ID="overdueTextBox" runat="server" CssClass="textfield-small"></asp:TextBox>
                <act:CalendarExtender ID="CalendarExtender2" runat="server" Format="yyyy-MM-dd" TargetControlID="overdueTextBox">
                </act:CalendarExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="overdueTextBox"
                    ErrorMessage="Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <p class="name">Contact person</p>
            </td>
            <td>
                <asp:TextBox ID="contactPersonTextBox" runat="server" CssClass="textfield-small"></asp:TextBox>
            </td>
            <td>
                <p class="name">Internal contact</p>
            </td>
            <td>
                <asp:TextBox ID="internalContactTextBox" runat="server" CssClass="textfield-small"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <p class="name">Mobile</p>
            </td>
            <td>
                <asp:TextBox ID="mobileTextBox" runat="server" CssClass="textfield-small"></asp:TextBox>
            </td>
            <td>
                <p class="name">Email</p>
            </td>
            <td>
                <asp:TextBox ID="emailTextBox" runat="server" CssClass="textfield-small"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <p class="name">Street address</p>
            </td>
            <td>
                <asp:TextBox ID="streetAddressTextBox" runat="server" CssClass="textfield-small"></asp:TextBox>
            </td>
            <td>
                <p class="name">Postal address</p>
            </td>
            <td>
                <asp:TextBox ID="postalAddressTextBox" runat="server" CssClass="textfield-small"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <p class="name">Notes</p>
            </td>
            <td colspan="3">
                <asp:TextBox ID="notesTextBox" runat="server" TextMode="MultiLine" CssClass="textfield-largetext" Rows="7" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><p class="divider">&nbsp;</p></td>
            <td><p class="divider">&nbsp;</p></td>
             <td><p class="divider">&nbsp;</p></td>
            <td><p class="divider">&nbsp;</p></td>
        </tr>
        <tr>
            <td colspan="3" align="right">
                <asp:Button ID="addButton" runat="server" CssClass="button-update" Text="Add" OnClick="addButton_Click" />
                <asp:Button ID="cancelButton" runat="server" CssClass="button-delete" Text="Cancel" OnClick="cancelButton_Click" CausesValidation="False" />
            </td>
        </tr>
    </table>
    </div>
</asp:Content>
