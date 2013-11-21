<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AssignProjectManager.aspx.cs" Inherits="ChangeTech.DeveloperWeb.AssignProjectManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div style="height:580px; margin:0 auto;">
    <table>
        <tr>
            <td>
                <asp:TextBox ID="emailTextBox"  CssClass="textfield-login" runat="server" OnTextChanged="emailTextBox_TextChanged"></asp:TextBox>
            </td>
            <td>
                &nbsp;
                <asp:Button ID="searchButton" runat="server" CssClass="button-update" Text="Search" OnClick="searchButton_Click" />
            </td>
        </tr>
    </table>
    <%--<asp:Button ID="addButton" runat="server" Text="Add project manager" Width="200"
        OnClientClick="addProjectManager();" />--%>
    <asp:GridView ID="adminGridView" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="12">
        <Columns>
            <asp:BoundField HeaderText="E-mail" DataField="UserName" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="selectLinkButton" runat="server" OnClick="selectLinkButton_Click"
                        CommandArgument='<%#Eval("UserGuid") %>'>Select</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:HiddenField ID="programHiddenField" runat="server" />
    </div>
    <script type="text/javascript">
        function addProjectManager() {

            var programGuid = document.getElementById("<%=programHiddenField.ClientID %>").value;
            var txtUser = document.getElementById("<%=emailTextBox.ClientID %>");
            var usermail = showModalDialog('AdminPopUp.aspx?ProgramGUID=' + programGuid, window, 'dialogWidth:400px;dialogHeight:500px;help:no;scroll:no;status:no');
            if (usermail) {
                txtUser.value = usermail;
            }
        }
    </script>

</asp:Content>
