<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="EditTranslationJob.aspx.cs" Inherits="ChangeTech.DeveloperWeb.EditTranslationJob" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="header">
        <h1>
            Edit Translation Job
        </h1>
        <div class="headermenu">
        </div>
        <div class="clear">
        </div>
    </div>
    <div class="box-main">
        <table>
            <tr>
                <td width="35%">
                    &nbsp;
                </td>
                <td width="30%">
                    &nbsp;
                </td>
                <td width="35%">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <p class="name">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ resources:Program %>"></asp:Literal></p>
                </td>
                <td>
                    <asp:DropDownList ID="ddlProgram" runat="server" DataTextField="ProgramName" DataValueField="Guid"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged"
                        CssClass="listmenu-large">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <p class="name">
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ resources:FromLanguage %>"></asp:Literal></p>
                </td>
                <td>
                    <asp:DropDownList ID="ddlFromLanguage" runat="server" DataTextField="Name" DataValueField="LanguageGUID"
                        CssClass="listmenu-default">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <p class="name">
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ resources:ToLanguage %>"></asp:Literal></p>
                </td>
                <td>
                    <asp:DropDownList ID="ddlToLanguage" runat="server" DataTextField="Name" DataValueField="LanguageGUID"
                        CssClass="listmenu-default">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <p class="divider">
                    </p>
                </td>
                <td>
                    <p class="divider">
                    </p>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnUpdate" runat="server" Text="<%$ resources:Share,Update %>" OnClick="btnUpdate_Click"
                        CssClass="button-update floatRight" />
                </td>
            </tr>
        </table>
    </div>
    <h1>&nbsp;</h1>
    <h1>Translator has permission:</h1>
    <div class="list">
    <asp:GridView ID="translatorHasPermissionGridView" runat="server" AutoGenerateColumns="False"
                    AllowPaging="True" OnRowCommand="translatorHasPermissionGridView_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="<%$Resources: Translator%>">
                            <ItemTemplate>
                                <p class="name"><asp:Label ID="lblTranslatorNameOfPermission" runat="server" Text='<%#Eval("TranslatorName")%>'></asp:Label></p>
                                <%--<asp:HiddenField runat="server" Visible="false" ID="hiddenTranslationJobTranslatorGuid" Value='<%#<%#Eval("TranslationJobTranslatorGUID")%>' />--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:Button runat="server" ID="deleteTranslatorBtn" Text="<%$Resources:DeleteButtonText %>"
                                    CommandArgument='<%#Eval("TranslationJobTranslatorGUID")%>' CommandName="deleteCommand" CssClass="button-delete" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
    </div>
    <h1>&nbsp;</h1>
    <h1>Translator has not permission:</h1>
    <div class="list">
    <asp:GridView ID="translatorHasNotPermissionGridView" runat="server" AutoGenerateColumns="False"
                    AllowPaging="True" PagerStyle-CssClass="pagenav" OnPageIndexChanging="translatorHasNotPermissionGridView_PageIndexChanging" OnRowCommand="translatorHasNotPermissionGridView_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="<%$Resources: Translator%>">
                            <ItemTemplate>
                                <p class="name"><asp:Label ID="lblTranslationJobTranslatorNameOfNotPermission" runat="server" Text='<%#Eval("TranslatorName")%>'></asp:Label></p>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:Button runat="server" ID="addTranslatorBtn" CommandName="addCommand" Text="<%$Resources:AddPermissionButtonText %>"
                                    CommandArgument='<%#Eval("TranslatorGUID")%>' CssClass="button-add floatRight"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
    </div>
</asp:Content>
