<%@ Page Title="" Language="C#" MasterPageFile="~/TempSite.Master" AutoEventWireup="true"
    CodeBehind="EditSession.aspx.cs" Inherits="ChangeTech.DeveloperWeb.EditSession" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script type="text/javascript">
         function ShowConfirm(obj) {
             if (obj.selectedIndex == 1 || obj.selectedIndex == 2) {
                 if (confirm('Are you sure you want to do this?')) {
                     __doPostBack("moreOptionsDDL", "");
                 }
             }
             else
                 if (obj.selectedIndex == 0) {
                 }
         }
        </script>
</asp:Content>
<asp:Content ID="ContentSiteMapPath" ContentPlaceHolderID="SiteMapPath" runat="server">
    <li>
    <asp:HyperLink ID="hpProgramLink" runat="server" Text="<%$ Resources: Share, Programs%>"
        ToolTip="<%$ Resources: Share, GoPrograms%>"></asp:HyperLink>
    </li>
    <li>
    <asp:HyperLink ID="hpEditProgramLink" runat="server" Text="<%$ Resources: Share, EditProgram%>"
        ToolTip="<%$ Resources: Share, GoEditProgram%>"></asp:HyperLink>
    </li> 
    <li>   
    <span  class="lastlinode">
        <asp:Literal ID="ltlEditSession" Text="<%$ Resources:Share, EditSession%>" runat="server"></asp:Literal>
    </span>
    </li> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br /><br />
    <div class="alertmessage hidden">There is an error somewhere</div>
    <div class="confirmationmessage hidden">The program is updated!</div>
    <div class="header">
        <h1>Day overview</h1>
        <div class="clear"></div>
    </div>
    <div class="box-main">
    <table>
    <tr>
      <td style="width:35%;">&nbsp;</td>
      <td style="width:30%;">&nbsp;</td>
      <td style="width:35%;">&nbsp;</td>
    </tr>
    <tr>
        <td><p class="name"><asp:Literal ID="Literal10" Text="<%$ Resources:Share, Day%>" runat="server"></asp:Literal></p></td>
        <td>
            <asp:DropDownList ID="ddlDay" runat="server" CssClass="listmenu-default">
                </asp:DropDownList>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name"><asp:Literal ID="Literal11" Text="<%$ Resources:Share, Name%>" runat="server"></asp:Literal></p></td>
      <td><asp:TextBox ID="txtSessionName" CssClass="textfield-largetext" runat="server"></asp:TextBox></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name"><asp:Literal ID="Literal12" Text="<%$ Resources:Share, Description%>" runat="server"></asp:Literal></p></td>
      <td>
            <asp:TextBox ID="txtSessionDescription" runat="server" CssClass="textfield-largetext" Rows="3" TextMode="MultiLine"></asp:TextBox>
      </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="report">Report button</p></td>
      <td>
            <asp:CheckBox ID="chkIsNeedReport" Text="<%$ Resources:IsNeedReport %>" CssClass="chk"   runat="server" />
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="help">Help button</p></td>
      <td>
            <asp:CheckBox ID="chkIsNeedHelp" Text="<%$ Resources:IsNeedHelp %>"  CssClass="chk" runat="server" />
       </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td></td>
      <td>
            <span style="padding-left:470px;">
            <asp:Button ID="btnUpdateSession" Text="Update" runat="server"
                    OnClick="btnUpdateSession_Click" CssClass="button-update" Enabled="false" />
            </span>
        </td>
      <td colspan="3">
            <asp:Label ID="warnLbl" runat="server" Text="<%$ Resources:Share, NotChangeableBecauseOfPublished%>"
                    Visible="false" ForeColor="Red"></asp:Label>
        </td>
    </tr>
  </table>
</div>
<p>&nbsp;</p>
<div class="header">
  <h1>Day sequences</h1>
  <div class="headermenu">
  <asp:Button ID="btnAddSeq" runat="server" CssClass="button-add" Text="<%$ resources:AddNewPageSeq %>" OnClick="btnAddSeq_Click" Enabled="false" />
    <asp:Button ID="addFromDayButton" runat="server" CssClass="button-add"  Text="Copy from another day" OnClick="addFromDayButton_Click" />
  </div>
  <div class="clear"></div>
</div>
    <asp:Repeater ID="rpSession" runat="server" OnItemDataBound="rpSession_ItemDataBound">
        <HeaderTemplate>
        <div class="list">
            <table>
                    <tr>
                        <th style="width: 5%; text-align:left; padding-left:10px;">
                            <asp:Literal ID="Literal1" runat="server" Text="<%$ resources:PageSeq %>"></asp:Literal>
                        </th>
                        <th style="width: 5%; text-align:left; padding-left:10px;">
                            <asp:Literal ID="Literal7" runat="server" Text="<%$ resources:CountOfPages %>"></asp:Literal>
                        </th>
                        <th style="width: 40%; text-align:left; padding-left:10px;">
                            <asp:Literal ID="Literal6" runat="server" Text="Name & Description"></asp:Literal>
                        </th>
                        <th style="width: 10%; text-align:left; padding-left:10px;">
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ resources:Predictor %>"></asp:Literal>
                        </th>
                        <th style="width: 10%; text-align:left; padding-left:10px;">
                            <asp:Literal ID="Literal5" runat="server" Text="<%$ resources:Intervent %>"></asp:Literal>
                        </th>
                        <th style="width: 10%; text-align:left; padding-left:10px;">
                            <asp:Literal ID="Literal8" runat="server" Text="<%$ resources:ProgramRoom %>"></asp:Literal>
                        </th>
                        <th style="width: 10%; text-align:left; padding-left:10px;">
                            <asp:Literal ID="Literal3" Text="<%$ Resources:Share, LastUpdateBy %>" runat="server"></asp:Literal>
                        </th>
                        <th style="width: 10%">
                            &nbsp;
                        </th>
                    </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <asp:Button  ID="ButtonUp" runat="server" CssClass="button-up" CommandArgument='<%#Eval ("SessionContentID") %>' OnClick="btnUp_click"/>
                        <p class="counter"><%#Eval("Order")%></p>
                    <asp:Button ID="ButtonDown" runat="server" CssClass="button-down" CommandArgument='<%#Eval ("SessionContentID") %>' OnClick="btnDown_click"/>
                </td>
                <td><p class="counter-seq"><%#Eval("CountOfPages")%></p></td>
                <td><p class="name"> <%#Eval("Name")%></p>
                <p class="description"><%# Eval("Description") != null ? ((string)Eval("Description")).Replace("\r\n", "<br/>").Replace("\r", "<br/>").Replace("\n", "<br/>") : Eval("Description")%></p></td>
                <td><%#Eval("Predictor.Value")%></td>
                <td><%#Eval("InterventCategory.Value")%></td>
                <td><%#Eval("ProgramRoom")%></td>
                <td><p class="user"><%# Eval("LastUpdateBy.UserName") %></p></td>
                <td><div class="buttons">
                <asp:Button ID="btnEdit" runat="server" CssClass="button-open" Text="<%$ Resources:Share, Edit %>" CommandArgument='<%#Eval ("PageSequenceID") %>'
                            OnClick="btnEdit_Click" Enabled="false" />
                 <asp:Button ID="btnPreview" runat="server" CssClass="button-open"  Enabled="false" Text="<%$ Resources:Preview %>" CommandArgument='<%#Eval ("PageSequenceID") %>' />
                <asp:DropDownList ID="moreOptionsDDL" runat="server"  onchange="return ShowConfirm(this);" DataValueField='<%#Eval ("PageSequenceID") %>' AutoPostBack="true" CssClass="listmenu-small" OnSelectedIndexChanged="moreOptionsDDL_SelectedIndexChanged" Enabled="false">
                                <asp:ListItem>More options</asp:ListItem>
                                <%--<asp:ListItem Value="Preview">Preview</asp:ListItem>--%>
                                <asp:ListItem Value="Make copy">Make copy</asp:ListItem>
                                <asp:ListItem Value="Delete">Delete</asp:ListItem>
                </asp:DropDownList>
         <%--       <asp:Button ID="btnDelete" runat="server" CssClass="button-delete" Text="<%$ Resources:Share, Delete %>" CommandArgument='<%#Eval ("SessionContentID") %>'
                            OnClientClick="return confirm('Are you sure you want to remove this page sequence from this session?')"
                            OnClick="btnDelete_Click" Enabled="false" />--%>
              <%--  <asp:Button ID="btnMakeCopy" runat="server" Text="<%$ Resources:Share, MakeCopy %>"
                            CommandArgument='<%#Eval ("SessionContentID") %>' OnClick="btnMakeCopy_Click"
                            Enabled="false" />--%>
            </div>
            </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
            </div>
            <div class="pagenav">
            <%# PagingString %>
            <div class="clear"></div>
            </div>
        </FooterTemplate>
    </asp:Repeater>
    <asp:HiddenField ID="hfProgramGuid" runat="server" />
    <script type="text/javascript">
        function openPageD(url, url1) {
            window.open(url,target='_blank');
            window.open(url1,target= '_blank');
            return false;
        }

        function openPage(url) {
            window.open(url,target='_blank');
            return false;
        }
    </script>
</asp:Content>
