<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewUserForProgram.aspx.cs" Inherits="ChangeTech.DeveloperWeb.NewUser" %>

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
    <asp:ScriptManager ID="scriptManager" runat="server">
    </asp:ScriptManager>
        <table>
            <tr>
                <td style="width:30%;">&nbsp;</td>
                <td style="width:45%;">&nbsp;</td>
                <td style="width:25%;">&nbsp;</td>
            </tr>
            <tr>
                <td><p class="name"><asp:Label ID="Label2" runat="server" Text="<%$ Resources: Email%>"></asp:Label>:</p></td>
                <td>
                    <asp:TextBox runat="server" ID="emailTextBox" CssClass="textfield-largetext"></asp:TextBox>
            <asp:RequiredFieldValidator ID="emailRequireValidator" runat="server" ErrorMessage="<%$ Resources:EmailRequired%>"
                ControlToValidate="emailTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="emailFormatValidator" runat="server" ErrorMessage="<%$ Resources: InvalidEmailFormat %>"
                ControlToValidate="emailTextBox" ValidationExpression="^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"
                Display="Dynamic"></asp:RegularExpressionValidator>
            <asp:CustomValidator ID="uniqueEmailValidator" runat="server" ErrorMessage="<%$ Resources:DuplicateAccountName %>"
                OnServerValidate="IsUserNameUnique" ControlToValidate="emailTextBox" Display="Dynamic"
                Width="400px"></asp:CustomValidator>
                </td>
                <td>&nbsp;</td>
            </tr>
             <tr>
                <td><p class="name"><asp:Label ID="Label3" runat="server" Text="<%$ Resources: FirstName%>"></asp:Label>:</p></td>
                <td>
                    <asp:TextBox ID="firstNameTextBox" runat="server" CssClass="textfield-largetext"></asp:TextBox>            
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please input first name" ControlToValidate="firstNameTextBox"></asp:RequiredFieldValidator>
                </td>
                <td>&nbsp;</td>
            </tr>
             <tr>
                <td><p class="name"><asp:Label ID="Label4" runat="server" Text="<%$ Resources: LastName%>"></asp:Label>:</p></td>
                <td>
                <asp:TextBox ID="lastNameTextBox" runat="server" CssClass="textfield-largetext"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ErrorMessage="Please input last name" ControlToValidate="lastNameTextBox"></asp:RequiredFieldValidator>
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
                    <asp:DropDownList ID="ddlUserType" runat="server" CssClass="listmenu-default">
               <%-- <asp:ListItem Text="User" Value="1"></asp:ListItem>
                <asp:ListItem Text="Administrator" Value="2"></asp:ListItem>--%>
                <asp:ListItem Text="Tester" Value="3"></asp:ListItem>
                <%--<asp:ListItem Text="Customer" Value="4"></asp:ListItem>--%>
            </asp:DropDownList>
                </td>
                <td>&nbsp;</td>
            </tr>
             <tr>
                <td><p class="name"><asp:Label ID="Label1" runat="server" Text="<%$ Resources: EmailTime%>"></asp:Label>:</p></td>
                <td>
                <asp:DropDownList ID="mailTimeDropDownList" runat="server" CssClass="listmenu-default">
            </asp:DropDownList>
                </td>
                <td>&nbsp;</td>
            </tr>
             <tr>
                <td><p class="name"><asp:Label ID="Label10" runat="server" Text="<%$ Resources: SendEmail%>"></asp:Label>:</p></td>
                <td>
                <asp:CheckBox ID="IsSendMailCheckBox" runat="server" />
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
                         <asp:Button runat="server" ID="submitButton" CssClass="button-update" Text="<%$ Resources: Submit%>" OnClick="submitButton_Click" />
                          <asp:Button ID="cancelButton" runat="server" Text="Cancel" CausesValidation="False" OnClick="cancelButton_Click"  CssClass="button-delete"/>
                </td>
                <td>&nbsp;</td>
            </tr>
           </table>
           </div>
           </div>
       <%-- <p>
            <asp:Label runat="server" Text="<%$ Resources: Share, Language%>"></asp:Label>
            <asp:DropDownList ID="LanguageDropdownList" runat="server" DataTextField="Name" DataValueField="LanguageGUID"></asp:DropDownList>
        </p>--%>
       <%-- <p>
            <asp:Panel runat="server" ID="feedbackPanel" Visible="false">
                <asp:Localize runat="server" Text="<%$ Resources: RegisterSuccessfulyPart1%>" Mode="PassThrough"></asp:Localize>
                <asp:LinkButton runat="server" ID="loginLink" Text="<%$ Resources: Here%>" PostBackUrl="~/Default.aspx"
                    CausesValidation="false"></asp:LinkButton>
                <asp:Localize ID="Localize1" runat="server" Text="<%$ Resources: RegisterSuccessfulyPart2%>"
                    Mode="PassThrough"></asp:Localize>
            </asp:Panel>
        </p>--%>
</asp:Content>