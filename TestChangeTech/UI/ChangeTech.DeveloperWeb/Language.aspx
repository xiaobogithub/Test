<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Language.aspx.cs" Inherits="ChangeTech.DeveloperWeb.Language" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <%-- <h4>
        <asp:Localize runat="server" Text="<%$Resources:Prompt %>"></asp:Localize></h4>--%>
    <asp:MultiView runat="server" ActiveViewIndex="0" ID="languageMultiView">
        <asp:View ID="ViewMode" runat="server">
            <table>
                <tr>
                    <th>
                        <asp:Localize runat="server" Text="<%$Resources: LanguageName %>"></asp:Localize>
                    </th>
                    <td>
                        <asp:TextBox ID="languageNameTextBox" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                            ControlToValidate="languageNameTextBox" ID="languageNameRequiredValidator" runat="server"
                            ErrorMessage="<%$Resources:LanguageNameRequiredMessage %>" Text="*" Display="Dynamic">
                        </asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="uniqueLanguageValidator" runat="server" ErrorMessage="<%$ Resources:DuplicateLanguageName %>"
                            OnServerValidate="IsLanguageUnique" ControlToValidate="languageNameTextBox" Display="Dynamic"
                            Width="400px"></asp:CustomValidator>
                    </td>
                    <td>
                        <asp:Button ID="addLanguageButton" runat="server" Text="<%$Resources:AddLanguageButtonText %>"
                            OnClick="addLanguageButton_Click" Width="120px" />
                    </td>
                </tr>
            </table>
            <asp:Repeater runat="server" ID="languagesRepeater">
                <HeaderTemplate>
                    <table>
                        <tr>
                            <%--<th>
                                <asp:Localize runat="server" Text="<%$Resources: LanguageName %>" />
                            </th>--%>
                            <%# HeaderString %>
                            <th>
                            </th>
                            <th>
                            </th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%#Eval("Name")%>
                        </td>
                        <td>
                            <asp:Button ID="editButton" runat="server" Text="<%$Resources:EditButtonText %>"
                                CausesValidation="false" OnClick="editButton_Click" CommandArgument='<%#Eval("LanguageGUID")%>' />
                        </td>
                        <td>
                            <asp:Button ID="deleteButton" runat="server" Text="<%$Resources:DeleteButtonText %>"
                                CausesValidation="false" OnClick="deleteButton_Click" CommandArgument='<%#Eval("LanguageGUID")%>'
                                OnClientClick="return confirm('Are you sure to delete?')" />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                    <%# PagingString %>
                </FooterTemplate>
            </asp:Repeater>
        </asp:View>
        <asp:View ID="EditMode" runat="server">
          <%--  <h4>
                <asp:Localize runat="server" Text="<%$Resources:EditLanguageTitle %>"></asp:Localize></h4>--%>
            <table>
                <tr>
                    <th>
                        <asp:Localize runat="server" Text="<%$Resources: LanguageName %>"></asp:Localize>
                    </th>
                    <td>
                        <asp:TextBox ID="newLanguageNameTextBox" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                            ControlToValidate="languageNameTextBox" ID="RequiredFieldValidator1" runat="server"
                            ErrorMessage="<%$Resources:LanguageNameRequiredMessage %>" Text="*" Display="Dynamic">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Button ID="saveButton" runat="server" Text="<%$Resources:SaveButtonText %>"
                            OnClick="saveButton_Click" />
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</asp:Content>
