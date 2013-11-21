<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Report.aspx.cs" Inherits="ChangeTech.DeveloperWeb.Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .content
        {
            font-size:inherit;
            font-weight:bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>Report overview</h1>
  <div class="headermenu"></div>
  <div class="clear"></div>
</div>
    <div class="box-main">
    <table>
        <tr>
            <td style="width:90%;">&nbsp;</td>
        </tr>
        <tr>
            <td><p class="name"><asp:HyperLink ID="activityLogHyperLink" runat="server" Font-Bold="true" Font-Size="Medium" NavigateUrl="~/LogReport.aspx">Go to export user activity page</asp:HyperLink></p></td>
        </tr>
        <tr>
            <td><p class="divider">&nbsp;</p></td>
        </tr>
        <tr>
            <td>
                    <p class="name">User group:</p>
                    <p class="name"><asp:DropDownList ID="UserGroupDropDownList" AutoPostBack="true" runat="server" CssClass="listmenu-default" OnSelectedIndexChanged="UserGroupDropDownList_SelectedIndexChanged">
                    </asp:DropDownList></p>
            </td>
        </tr>
        <tr>
            <td><p class="divider">&nbsp;</p></td>
        </tr>
        <tr>
            <td> 
            <p class="name"><asp:Button ID="ExportButton" runat="server" CssClass="button-update" Text="Export data" OnClick="ExportButton_Click" /></p></td>
        </tr>
         <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
    </div>
    <table  >
            <tr>
                <td style="width:450px;">&nbsp;</td>
                <td style="width:20px;">&nbsp;</td>
                <td style="width:450px;">&nbsp;</td>
                <td style="width:20px;">&nbsp;</td>
                <td style="width:450px;">&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <p style="font-size:large;font-weight:bold;">For all users:</p>
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
                <td>&nbsp;</td>
                    <td>
                        <p style="font-size:large;font-weight:bold;">For Male:</p>
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
                    <td>&nbsp;</td>
                    <td>
                        <p style="font-size:large;font-weight:bold;">For Female:</p>
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
            </tr>
        </table>
        <br />
        <div class="box-main">
        <div style="margin:8px 0px 8px 10px;">
            <span class="content">AllUser:</span>
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources: AllUser %>"></asp:Literal><br />
            <span class="content">NotCompleteScreening:</span>
            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources: NotCompleteScreening %>"></asp:Literal><br />
            <span class="content">CompleteScreening:</span>
            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources: CompleteScreening %>"></asp:Literal><br />
            <span class="content">RegisteredUser:</span>
            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources: RegisteredUser %>"></asp:Literal><br />
            <span class="content">UsersInProgramme:</span>
            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources: UsersInProgramme %>"></asp:Literal><br />
            <span class="content">ActiveUser:</span>
            <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources: ActiveUser %>"></asp:Literal><br />
           <span class="content"> InActiveUser:</span>
            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources: InActiveUser %>"></asp:Literal><br />
            <span class="content">CompleteUser:</span>
            <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources: CompleteUser %>"></asp:Literal><br />
            <span class="content">TerminateUser:</span>
            <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources: TerminateUser %>"></asp:Literal><br />
            <span class="content">PauseUser:</span>
            <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources: PauseUser %>"></asp:Literal><br />
            </div>
            </div>
</asp:Content>
