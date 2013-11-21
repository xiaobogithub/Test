<%@ Page Title="" Language="C#" MasterPageFile="~/HealthProfileSystem/HealthProfileSystem.Master" AutoEventWireup="true" CodeBehind="EditOrder.aspx.cs" Inherits="ChangeTech.DeveloperWeb.HealthProfileSystem.EditOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>Edit order overview</h1>
  <div class="headermenu">
  </div>
  <div class="clear"></div>
</div>   
    <act:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </act:ToolkitScriptManager>
    <div class="box-main">
<table>
<tr>
      <td style="width:30%">&nbsp;</td>
      <td style="width:40%">&nbsp;</td>
      <td style="width:30%">&nbsp;</td>
    </tr>
        <tr>
            <td>
                <p class="name">Language :</p>
            </td>
            <td>
                <asp:DropDownList ID="ddlLanguage" runat="server" CssClass="listmenu-common" AutoPostBack="true" 
                    onselectedindexchanged="ddlLanguage_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <p class="name">Health profile program :</p>
            </td>
            <td>
                <asp:DropDownList ID="ddlHPProgram" runat="server"  CssClass="listmenu-large" >
                </asp:DropDownList>
            </td>
            <td>&nbsp;</td>
        </tr>
         <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td>&nbsp;</td>
    </tr>
        <tr>
            <td>
                <p class="name">Customer name :</p>
            </td>
            <td>
                <asp:TextBox ID="txtCustomerName" runat="server" CssClass="textfield-small" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCustomerName"
                    ErrorMessage="Please enter correctly format."></asp:RequiredFieldValidator>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <p class="name">Contact person name :</p>
            </td>
            <td>
                <asp:TextBox ID="txtContactPersonName" runat="server" CssClass="textfield-small" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtContactPersonName"
                    ErrorMessage="Please enter correctly format."></asp:RequiredFieldValidator>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <p class="name">Contact person number :</p>
            </td>
            <td>
                <asp:TextBox ID="txtContactPersonNum" runat="server" CssClass="textfield-small"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtContactPersonNum"
                    ErrorMessage="Please enter correctly person number."></asp:RequiredFieldValidator>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <p class="name">Contact person phone :</p>
            </td>
            <td>
                <asp:TextBox ID="txtContactPersonPhone" runat="server" CssClass="textfield-small"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtContactPersonPhone"
                    ErrorMessage="Please enter correctly telphone num."></asp:RequiredFieldValidator>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <p class="name">Contact person email :</p>
            </td>
            <td>
                <asp:TextBox ID="txtContactPersonEmail" runat="server" CssClass="textfield-small" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="emailRequireValidator" runat="server" ErrorMessage="EmailRequired"
                    ControlToValidate="txtContactPersonEmail" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="emailFormatValidator" runat="server" ErrorMessage=" InvalidEmailFormat"
                    ControlToValidate="txtContactPersonEmail" ValidationExpression="^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"
                    Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
               <p class="name"> SOSI contact email :</p>
            </td>
            <td>
                <asp:TextBox ID="txtSOSIContactEmail" runat="server"  CssClass="textfield-small"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="EmailRequired"
                    ControlToValidate="txtSOSIContactEmail" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage=" InvalidEmailFormat"
                    ControlToValidate="txtSOSIContactEmail" ValidationExpression="^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"
                    Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
        </tr>
         <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td>&nbsp;</td>
    </tr>
        <tr>
            <td>
               <p class="name"> # Of users :</p>
            </td>
            <td>
                <asp:TextBox ID="txtUserCount" runat="server"  CssClass="textfield-small"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression= "^(\d{1,4}|10000)$"
                    ControlToValidate="txtUserCount" ErrorMessage="Please enter correctly number!"></asp:RegularExpressionValidator>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
               <p class="name"> Start date :</p>
            </td>
            <td>
                <asp:TextBox ID="txtStartDate" runat="server" CssClass="textfield-small"></asp:TextBox>
                <act:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MM-yyyy" TargetControlID="txtStartDate">
                </act:CalendarExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtStartDate"
                    ErrorMessage="Please enter correctly date."></asp:RequiredFieldValidator>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
               <p class="name"> Stop date :</p>
            </td>
            <td>
                <asp:TextBox ID="txtStopDate" runat="server" CssClass="textfield-small" ></asp:TextBox>
                <act:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MM-yyyy" TargetControlID="txtStopDate">
                </act:CalendarExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtStopDate"
                    ErrorMessage="Please enter correctly date."></asp:RequiredFieldValidator>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <p class="name">Location :</p>
            </td>
            <td>
                <asp:DropDownList ID="ddlLocation" runat="server"  CssClass="listmenu-common" >
                </asp:DropDownList>
            </td>
            <td>&nbsp;</td>
        </tr>
         <tr>
            <td>
               <p class="name"> Industry :</p>
            </td>
            <td>
                <asp:DropDownList ID="ddlIndustry" runat="server"  CssClass="listmenu-large" >
                </asp:DropDownList>
            </td>
            <td>&nbsp;</td>
        </tr>
         <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td>&nbsp;</td>
    </tr>
        <tr>
            <td>&nbsp;</td>
            <td  align="right">
                <asp:Button ID="cancelButton" runat="server" Text="Cancel" 
                    CausesValidation="False" OnClick="cancelButton_Click"  CssClass="button-delete" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="editButton" runat="server" Text="Save" 
                    OnClick="editButton_Click"  CssClass="button-update"/>
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
    </div>
</asp:Content>
