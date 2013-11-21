<%@ Page Title="" Language="C#" MasterPageFile="~/Translator/Translator.Master" 
AutoEventWireup="true" CodeBehind="TranslatorHome.aspx.cs" Inherits="ChangeTech.DeveloperWeb.TranslatorHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>Translator home overview</h1>
  <div class="headermenu">
  </div>
  <div class="clear"></div>
</div>   
    <asp:Repeater ID="translationJobRepeater" runat="server">
        <HeaderTemplate>
        <div class="list">
            <table >
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
                    <th style="  text-align:center;">
                        Action
                    </th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                   <p class="name"> <%#Eval("Program.Name")%></p>
                </td>
                <td>
                    <p class="description"><%#Eval("FromLanguage.Name")%></p>
                </td>
                <td>
                    <p class="description"><%#Eval("ToLanguage.Name")%></p>
                </td>
                <td>
                    <p class="description"><%#Eval("Translators")%></p>
                </td>
                <td>
                    <p class="useramount"><%#Eval("Elements")%></p>
                </td>
                <td>
                    <p class="useramount"><%#Eval("Words")%></p>
                </td>
                <td>
                    <p class="useramount"><%#Eval("Completed")%></p>
                </td>
                <td>
                    <p class="description"><%#Eval("TextOfDefaultTranslatedContent")%></p>
                </td>
                <td>
                 <div class="buttons">
                    <asp:Button ID="openButton" runat="server" CssClass="button-open" Text="Open" CommandArgument='<%#Eval("TranslationJobGUID") %>'
                        OnClick="openButton_Click" />
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
