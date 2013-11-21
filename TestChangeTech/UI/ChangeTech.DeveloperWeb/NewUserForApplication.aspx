<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NewUserForApplication.aspx.cs" Inherits="ChangeTech.DeveloperWeb.NewUserForApplication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>New user for application overview</h1>
  <div class="headermenu"></div>
  <div class="clear"></div>
</div>
<div style="height:580px;">
    <div class="box-main">
        <table>
            <tr>
                <td style="width:30%;">&nbsp;</td>
                <td style="width:45%;">&nbsp;</td>
                <td style="width:25%;">&nbsp;</td>
            </tr>
            <tr>
        <td><p class="name"><asp:Label ID="Label1" runat="server" Text="<%$ Resources: Email%>"></asp:Label>:</p></td>
        <td>
             <asp:TextBox runat="server" ID="emailTextBox" CssClass="textfield-largetext"></asp:TextBox>
            <asp:RequiredFieldValidator ID="emailRequireValidator" runat="server" ErrorMessage="<%$ Resources:EmailRequired%>"
                ControlToValidate="emailTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="emailFormatValidator" runat="server" ErrorMessage="<%$ Resources: InvalidEmailFormat %>"
                ControlToValidate="emailTextBox" ValidationExpression="^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"
                Display="Dynamic"></asp:RegularExpressionValidator>
            <%--<asp:CustomValidator ID="uniqueEmailValidator" runat="server" ErrorMessage="<%$ Resources:DuplicateAccountName %>"
                OnServerValidate="IsUserNameUnique" ControlToValidate="emailTextBox" Display="Dynamic"
                Width="400px"></asp:CustomValidator>--%>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
        <td><p class="name"> <asp:Label ID="Label3" runat="server" Text="<%$ Resources: FirstName%>"></asp:Label>:</p></td>
        <td>
             <asp:TextBox ID="firstNameTextBox" runat="server" CssClass="textfield-largetext"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please input first name"
                ControlToValidate="firstNameTextBox"></asp:RequiredFieldValidator>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
        <td><p class="name"><asp:Label ID="Label4" runat="server" Text="<%$ Resources: LastName%>"></asp:Label>:</p></td>
        <td>
             <asp:TextBox ID="lastNameTextBox" runat="server" CssClass="textfield-largetext"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please input last name"
                ControlToValidate="lastNameTextBox"></asp:RequiredFieldValidator>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
        <td><p class="name"><asp:Label ID="Label5" runat="server" Text="<%$ Resources: Password%>"></asp:Label>:</p></td>
        <td>
             <asp:TextBox runat="server" ID="passwordTextBox" TextMode="Password" CssClass="textfield-largetext"></asp:TextBox>
            <asp:RequiredFieldValidator ID="passwordRequireValidator" runat="server" ErrorMessage="<%$ Resources:PasswordRequired%>"
                ControlToValidate="passwordTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
        <td><p class="name"><asp:Label ID="Label6" runat="server" Text="<%$ Resources: ConfirmPassword%>"></asp:Label>:</p></td>
        <td>
              <asp:TextBox runat="server" ID="confirmPasswordTextBox" TextMode="Password" CssClass="textfield-largetext"></asp:TextBox>
            <asp:RequiredFieldValidator ID="confirmPasswordRequireValidator" runat="server" ErrorMessage="<%$ Resources:ConfirmPasswordRequired%>"
                ControlToValidate="confirmPasswordTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="passwordCompareValidator" runat="server" ErrorMessage="<%$ Resources: PasswordNotEqual %>"
                ControlToValidate="confirmPasswordTextBox" ControlToCompare="passwordTextBox"
                Operator="Equal" Display="Dynamic">
            </asp:CompareValidator>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
        <td><p class="name"><asp:Label ID="Label7" runat="server" Text="<%$ Resources: MobilePhone%>"></asp:Label>:</p></td>
        <td>
             <asp:TextBox runat="server" ID="mobilePhoneTextBox" CssClass="textfield-largetext"></asp:TextBox>
            <asp:RequiredFieldValidator ID="mobilePhoneRequireValidator" runat="server" ErrorMessage="<%$ Resources:MobilePhoneRequired%>"
                ControlToValidate="mobilePhoneTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
        <td><p class="name"><asp:Label ID="Label8" runat="server" Text="<%$ Resources: Gender%>"></asp:Label>:</p></td>
        <td>
             <asp:DropDownList runat="server" ID="genderDropdownList" CssClass="listmenu-default">
            </asp:DropDownList>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
        <td><p class="name"><asp:Label ID="Label9" runat="server" Text="<%$ Resources: UserType%>"></asp:Label>:</p></td>
        <td>
            <asp:DropDownList ID="ddlUserType" runat="server" CssClass="listmenu-default" DataTextField="DisplayText" DataValueField="UserTypeID">
            </asp:DropDownList>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
        <td><p class="name"><asp:Label ID="Label2" runat="server" Text="<%$ Resources: SendEmail%>"></asp:Label>:</p></td>
        <td>
             <asp:CheckBox ID="IsSendMailCheckBox" runat="server" />
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
        <td><p class="name"><asp:Label ID="Label10" runat="server" Text="<%$ Resources: SMSLogin%>"></asp:Label>:</p></td>
        <td>
             <asp:CheckBox ID="IsSMSLoginCheckBox" Checked="true" runat="server" />
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
        <td align="right">
             <asp:Button ID="submitButton" runat="server" Text="<%$ Resources: Submit%>" CssClass="button-update" OnClick="submitButton_Click" />
             <asp:Button ID="cancelButton" runat="server" Text="Cancel" CausesValidation="False" OnClick="cancelButton_Click"  CssClass="button-delete"/>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
        <td colspan="3">
             <asp:Label ID="msgLbl" runat="server" ForeColor="Red" Width="100%"></asp:Label>
        </td>
    </tr>
    </table>
    </div>
    </div>
</asp:Content>
