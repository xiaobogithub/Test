<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="InsertProgramToStudy.aspx.cs" Inherits="ChangeTech.DeveloperWeb.InsertProgramToStudy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td>
                Program name:
            </td>
            <td>
                <asp:TextBox ID="programNameTextBox" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="searchButton" runat="server" Text="Search" 
                    onclick="searchButton_Click" />
            </td>
        </tr>
    </table>
    <p>
    </p>
    Please select a program:
    <asp:Repeater ID="programRepeater" runat="server">
        <HeaderTemplate>
            <table>
                <tr>
                    <th>
                        Program name
                    </th>
                    <th>
                        Description
                    </th>
                    <th>
                    </th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <%#Eval("ProgramName")%>
                </td>
                <td>
                    <%#Eval("Description")%>
                </td>
                <td>
                    <asp:Button ID="selectButton" runat="server" Text="Select" OnClick="selectButton_Click"
                        CommandArgument='<%#Eval("ProgramGuid") %>' />
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
            <%=PagingString %>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
