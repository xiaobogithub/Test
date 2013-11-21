<%@ Page Title="" Language="C#" MasterPageFile="~/3rdParty.Master" AutoEventWireup="true"
    CodeBehind="EditNullCompany3rdParty.aspx.cs" Inherits="ChangeTech.DeveloperWeb.EditNullCompany3rdParty" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>Edit Nullcompany 3rdParty overview</h1>
  <div class="headermenu">
  </div>
  <div class="clear"></div>
</div>
 <div class="box-main">
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
               <p class="name"> Version:</p>
            </td>
            <td colspan="3">
                <asp:Label ID="versionLabel" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="1">
                <p class="name">Users not register on company</p>
            </td>
            <td colspan="3" align="left">
                <asp:Button ID="ShowUserButton" runat="server" Text="Show Users" CssClass="button-update" OnClick="ShowUserButton_Click" />
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
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
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
                    <p class="name">For Male</p>
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
                    <p class="name">For Female</p>
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
               <p class="name"> ActiveUser:</p>
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
