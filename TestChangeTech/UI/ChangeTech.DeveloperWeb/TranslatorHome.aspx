<%@ Page Title="" Language="C#" MasterPageFile="~/Translator.Master" AutoEventWireup="true" CodeBehind="TranslatorHome.aspx.cs" Inherits="ChangeTech.DeveloperWeb.TranslatorHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Repeater ID="translationJobRepeater" runat="server">
        <HeaderTemplate>
            <table width="80%">
                <tr>
                    <th>
                        Program
                    </th>
                    <th>
                        From language
                    </th>
                    <th>
                        To language
                    </th>
                    <th>
                        Translators
                    </th>
                    <th>
                        Elements
                    </th>
                    <th>
                        Words
                    </th>
                    <th>
                        Completed
                    </th>
                    <th>
                        Default content in Translated field
                    </th>
                    <th>
                        Action
                    </th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <%#Eval("Program.Name")%>
                </td>
                <td>
                    <%#Eval("FromLanguage.Name")%>
                </td>
                <td>
                    <%#Eval("ToLanguage.Name")%>
                </td>
                <td>
                    <%#Eval("Translators")%>
                </td>
                <td>
                    <%#Eval("Elements")%>
                </td>
                <td>
                    <%#Eval("Words")%>
                </td>
                <td>
                    <%#Eval("Completed")%>
                </td>
                <td>
                    <%#Eval("TextOfDefaultTranslatedContent")%>
                </td>
                <td>
                    <asp:Button ID="openButton" runat="server" Text="Open" CommandArgument='<%#Eval("TranslationJobGUID") %>'
                        OnClick="openButton_Click" />
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
