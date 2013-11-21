<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ManageTranslationJob.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ManageTranslationJob" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
         <div class="header">
  <h1>Manage Translation Job </h1>
  <div class="headermenu">
    <asp:Button ID="addButton" runat="server" OnClick="addButton_Click" Text="Add new" CssClass="button-add"/>
  </div>
  <div class="clear"></div>
</div> 
    <asp:Repeater ID="translationJobRepeater" runat="server">
        <HeaderTemplate>
          <div class="list">
            <table>
                <tr>
                    <td>
                        Program
                    </td>
                    <td>
                        From language
                    </td>
                    <td>
                        To language
                    </td>
                    <td>
                        Translators
                    </td>
                    <td>
                        Elements
                    </td>
                    <td>
                        Words
                    </th>
                    <th>
                        Completed
                    </td>
                    <td>
                        Default content in Translated field
                    </td>
                    <td align="right">
                        Action
                    </td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <p class="name"><%#Eval("Program.Name")%></p>
                </td>
                <td>
                    <p class="description"><%#Eval("FromLanguage.Name")%></p>
                </td>
                <td>
                    <p class="description"><%#Eval("ToLanguage.Name")%></p>
                </td>
                <td>
                    <p class="description""><%#Eval("Translators")%></p>
                </td>
                <td>
                   <p class="counter"><%#Eval("Elements")%></p>
                </td>
                <td>
                    <p class="counter"><%#Eval("Words")%></p>
                </td>
                <td>
                    <p class="counter"><%#Eval("Completed")%></p>
                </td>
                <td>
                    <p class="description"><%#Eval("TextOfDefaultTranslatedContent")%></p>
                </td>
                <td style="width:10%">
                <div class="buttons">
                    <asp:Button ID="editButton" runat="server" Text="Edit" CommandArgument='<%#Eval("TranslationJobGUID") %>'
                        OnClick="editButton_Click" CssClass="button-open" />
                    <asp:Button ID="statsButton" runat="server" Text="Stats" CommandArgument='<%#Eval("TranslationJobGUID") %>'
                        OnClick="statsButton_Click" CssClass="button-open" />
                    <asp:Button ID="updateButton" runat="server" Text="Finish" CommandArgument='<%#Eval("TranslationJobGUID") %>'
                        OnClientClick="return confirm('All uncompleted elements of this translation job will be updated to default content. Do you confirm this translation job?')"
                        OnClick='updateButton_Click' CssClass="button-open" />
                    <asp:Button ID="deleteButton" runat="server" Text="Delete" CommandArgument='<%#Eval("TranslationJobGUID") %>'
                        OnClick='deleteButton_Click' CssClass="button-delete" />
                </div>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
            </div>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
