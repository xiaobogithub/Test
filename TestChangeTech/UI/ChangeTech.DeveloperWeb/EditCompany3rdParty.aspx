<%@ Page Title="" Language="C#" MasterPageFile="~/3rdParty.Master" AutoEventWireup="true"
    CodeBehind="EditCompany3rdParty.aspx.cs" Inherits="ChangeTech.DeveloperWeb.EditCompany3rdParty" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>Edit Company3rdParty overview</h1>
  <div class="headermenu">
  </div>
  <div class="clear"></div>
</div>
<div class="box-main">
    <act:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </act:ToolkitScriptManager>
    <table>
        <tr>
            <td style="width:30%;">&nbsp;</td>
            <td style="width:30%;">&nbsp;</td>
            <td style="width:30%">&nbsp;</td>
            <td style="width:10%">&nbsp;</td>
        </tr>
        <tr>
             <td colspan="4" style="font-weight:bold;">
                <p class="name">Program information</p>
            </td>
        </tr>
        <tr>
            <td>
                <p class="name">Name:</p>
            </td>
            <td colspan="3">
                <asp:Label ID="programNameLabel" runat="server" Text=""></asp:Label>
            </td>
            </tr>
            <tr>
            <td>
                <p class="name">Version:</p>
            </td>
            <td colspan="3">
                <asp:Label ID="versionLabel" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <p class="name"><span style="font-weight:bold;">Company information</span></p>
            </td>
            <td  >
                <asp:Button ID="ShowUserButton" runat="server" Text="Show Users" CssClass="button-update" OnClick="ShowUserButton_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <p class="name">Compay name</p>
            </td>
            <td>
                <asp:TextBox ID="companyTextBox" runat="server" CssClass="textfield-small"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="companyTextBox"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td colspan="2">
            </td>
        </tr>
        <tr>
            <td>
                <p class="name">Start date</p>
            </td>
            <td>
                <asp:TextBox ID="startTextBox" runat="server"  CssClass="textfield-small"></asp:TextBox>
                <act:CalendarExtender ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd" TargetControlID="startTextBox">
                </act:CalendarExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="startTextBox"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                <p class="name">Overdue date</p>
            </td>
            <td>
                <asp:TextBox ID="overdueTextBox" runat="server"  CssClass="textfield-small"></asp:TextBox>
                <act:CalendarExtender ID="CalendarExtender2" runat="server" Format="yyyy-MM-dd" TargetControlID="overdueTextBox">
                </act:CalendarExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="overdueTextBox"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <p class="name">Contact person</p>
            </td>
            <td>
                <asp:TextBox ID="contactPersonTextBox" runat="server"  CssClass="textfield-small"></asp:TextBox>
            </td>
            <td>
               <p class="name"> Internal contact</p>
            </td>
            <td>
                <asp:TextBox ID="internalContactTextBox" runat="server"  CssClass="textfield-small"></asp:TextBox>
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
               <p class="name"> Email</p>
            </td>
            <td>
                <asp:TextBox ID="emailTextBox" runat="server"  CssClass="textfield-small"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <p class="name">Street address</p>
            </td>
            <td>
                <asp:TextBox ID="streetAddressTextBox" runat="server"  CssClass="textfield-small"></asp:TextBox>
            </td>
            <td>
                <p class="name">Postal address</p>
            </td>
            <td>
                <asp:TextBox ID="postalAddressTextBox" runat="server"  CssClass="textfield-small"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <p class="name">Notes</p>
            </td>
            <td colspan="3">
                <asp:TextBox ID="notesTextBox" runat="server" TextMode="MultiLine"  CssClass="textfield-largetext" Rows="7"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <p class="name">Screen URL</p>
            </td>
            <td colspan="3">
                <asp:HyperLink ID="screenurlHyperLink" runat="server"></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td>
                <p class="name">Login URL</p>
            </td>
            <td colspan="3">
                <asp:HyperLink ID="loginHyperLink" runat="server"></asp:HyperLink>
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
                <asp:Button ID="editButton" runat="server" Text="Update" CssClass="button-update" OnClick="editButton_Click" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="cancelButton" runat="server" Text="Cancel" OnClick="cancelButton_Click"  CssClass="button-delete" CausesValidation="False" />
            </td>
        </tr>
    </table>
    </div>
    <br />
    <div class="box-main">
    <table>
        <tr>
            <td colspan="2">
                <p class="name"><span style="font-weight:bold;">Statistics</span></p>
            </td>
        </tr>
        <tr>
            <td>
               <p class="name"><span style="font-weight:bold;">For all user</span></p>
               <br />
               <div class="list">
                <asp:GridView ID="ReportGridView" runat="server" AutoGenerateColumns="false" BorderColor="White">
                    <Columns>
                        <asp:BoundField HeaderText="Case" DataField="Name" />
                        <asp:BoundField HeaderText="Number" DataField="Value" />
                    </Columns>
                </asp:GridView>
                </div>
            </td>
            <asp:Panel ID="separatePanel" runat="server" Visible="false">
                <td style="padding-left:5px;">
                    <p class="name"><span style="font-weight:bold;">For Male</span></p>
                    <br />
                    <div class="list">
                    <asp:GridView ID="maleGridView" runat="server" AutoGenerateColumns="false" BorderColor="White">
                        <Columns>
                            <asp:BoundField HeaderText="Case" DataField="Name" />
                            <asp:BoundField HeaderText="Number" DataField="Value" />
                        </Columns>
                    </asp:GridView>
                    </div>
                </td>
                <td style="padding-left:5px;">
                    <p class="name"><span style="font-weight:bold;">For Female</span></p>
                    <br />
                    <div class="list">
                    <asp:GridView ID="femaleGridView" runat="server" AutoGenerateColumns="false" BorderColor="White">
                        <Columns>
                            <asp:BoundField HeaderText="Case" DataField="Name" />
                            <asp:BoundField HeaderText="Number" DataField="Value" />
                        </Columns>
                    </asp:GridView>
                    </div>
                </td>
            </asp:Panel>
            <td style="padding-left:5px;">
            <div class="list">
                <p class="name">AllUser:</p>
                <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources: AllUser %>"></asp:Literal><br />
                <p class="name">NotCompleteScreening:</p>
                <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources: NotCompleteScreening %>"></asp:Literal><br />
                <p class="name">CompleteScreening:</p>
                <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources: CompleteScreening %>"></asp:Literal><br />
                <p class="name">RegisteredUser:</p>
                <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources: RegisteredUser %>"></asp:Literal><br />
                <p class="name">UsersInProgramme:</p>
                <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources: UsersInProgramme %>"></asp:Literal><br />
                <p class="name">ActiveUser:</p>
                <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources: ActiveUser %>"></asp:Literal><br />
                <p class="name">InActiveUser:</p>
                <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources: InActiveUser %>"></asp:Literal><br />
                <p class="name">CompleteUser:</p>
                <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources: CompleteUser %>"></asp:Literal><br />
                <p class="name">TerminateUser:</p>
                <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources: TerminateUser %>"></asp:Literal><br />
                <p class="name">PauseUser:</p>
                <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources: PauseUser %>"></asp:Literal>
                </div>
            </td>
        </tr>
    </table>
    </div>
</asp:Content>
