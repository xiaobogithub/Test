<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ManageDeleteProgram.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ManageDeleteProgram" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>Manage delete program overview</h1>
  <div class="headermenu"></div>
  <div class="clear"></div>
</div>
<div>
<div class="box-main">
        <table>
            <tr>
                <td style="width:47%">&nbsp;</td>
                <td style="width:6%">&nbsp;</td>
                <td style="width:47%">&nbsp;</td>
            </tr>
             <tr>
                <td><p class="name">Applied by me</p></td>
                <td>&nbsp;</td>
                <td><p class="name">Applied to me</p></td>
            </tr>
            <tr>
                <td>
                        <asp:GridView ID="myApplicationGridView" runat="server" AutoGenerateColumns="false"
                            CssClass="list" AllowPaging="true" PageSize="10" OnSelectedIndexChanging="myApplicationGridView_SelectedIndexChanging">
                            <Columns>
                                <asp:BoundField HeaderText="Program" DataField="ProgramName" />
                                <asp:BoundField HeaderText="Assignee" DataField="AssigneeEmail" />
                                <asp:BoundField HeaderText="Status" DataField="Status" />
                            </Columns>
                        </asp:GridView>
                </td>
                <td>&nbsp;</td>
                <td>
                        <asp:GridView ID="assignToMeGridView" runat="server" AutoGenerateColumns="false"
                            CssClass="list" AllowPaging="true" PageSize="10"  OnSelectedIndexChanging="assignToMeGridView_SelectedIndexChanging">
                            <Columns>
                                <asp:BoundField HeaderText="Program" DataField="ProgramName" />
                                <asp:BoundField HeaderText="Assignee" DataField="AssigneeEmail" />
                                <asp:BoundField HeaderText="Status" DataField="Status" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="approveButton" runat="server" Text="Approve" CommandArgument='<%#Eval("ApplicationGUID") %>'
                                        OnClientClick="confirm('Are you sure you want to approve the application?');"
                                        OnClick="approveButton_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="declineButton" runat="server" Text="Decline" CommandArgument='<%#Eval("ApplicationGUID") %>'
                                    OnClick="declineButton_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                </td>
            </tr>
            <tr>
                <td><p class="divider">&nbsp;</p></td>
                <td><p class="divider">&nbsp;</p></td>
                <td><p class="divider">&nbsp;</p></td>
            </tr>
             <tr>
                <td colspan="3">
                <p class="name">Submit application : </p>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table>
                        <tr>
                            <td style="width:20%;">&nbsp;</td>
                            <td style="width:45%;">&nbsp;</td>
                            <td style="width:35%;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td><p class="name">Program:</p></td>
                            <td>
                               <asp:DropDownList ID="programDropDownList" CssClass="listmenu-large" runat="server"  >
                            </asp:DropDownList>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                         <tr>
                            <td><p class="name">Assign to:</p></td>
                            <td>
                                <asp:DropDownList ID="AssigneeDropDownList" runat="server" CssClass="listmenu-large">
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
                            <td align="right">
                               <asp:Button ID="submitButton" runat="server" Text="Submit" CssClass="button-update" OnClick="submitButton_Click"/>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
    </table>
    </div>
    </div>
</asp:Content>
