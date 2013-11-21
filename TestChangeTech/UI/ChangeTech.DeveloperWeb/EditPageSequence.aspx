<%@ Page Title="" Language="C#" MasterPageFile="~/TempSite.Master" AutoEventWireup="true"
    CodeBehind="EditPageSequence.aspx.cs" Inherits="ChangeTech.DeveloperWeb.EditPageSequence" %>

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
    <asp:HyperLink ID="hpEditSessionLink" runat="server" Text="<%$ Resources: Share, EditSession%>"
        ToolTip="<%$ Resources: Share, GoEditSession%>"></asp:HyperLink>
    </li>
    <li>
        <span  class="lastlinode">
            <asp:Literal ID="ltlEditPageSequence" Text="<%$ Resources:Share, EditPageSequence%>"
            runat="server"></asp:Literal>
        </span>
    </li>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<br /><br />
<div class="alertmessage hidden">There is an error somewhere</div>
<div class="confirmationmessage hidden">The program is updated!</div>
<div class="header">
  <h1>Sequence overview</h1>
  <div class="clear"></div>
</div>
<div class="box-main">
  <table>
  <tr> 
      <td style="width:35%">&nbsp;</td>
      <td style="width:30%">&nbsp;</td>
      <td style="width:35%">&nbsp;</td>
    </tr>
    <tr>
        <td><p class="name"><asp:Literal ID="Literal9" Text="<%$ Resources:Share, ProgramRoom%>" runat="server"></asp:Literal></p></td>
        <td>
            <asp:DropDownList ID="ddlProgramRoom" runat="server" CssClass="listmenu-default" DataTextField="Name" DataValueField="ProgramRoomGuid">
                </asp:DropDownList>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name"> <asp:Literal ID="Literal10" Text="<%$ Resources:Share, Name%>" runat="server"></asp:Literal></p></td>
      <td><asp:TextBox ID="txtPageSeqName" runat="server" CssClass="textfield-largetext"></asp:TextBox></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
        <td><p class="name"><asp:Literal ID="Literal4" Text="<%$ Resources:Share, Description%>" runat="server"></asp:Literal></p></td>
        <td><asp:TextBox ID="txtPageSeqDescription" runat="server" CssClass="textfield-largetext" Rows="3" TextMode="MultiLine"></asp:TextBox>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            <span style="padding-left:470px;">
                <asp:Button ID="btnUpdatePageSeq" Text="Update" OnClientClick="return OnConfirm();" runat="server" OnClick="btnUpdatePageSeq_Click" CssClass="button-update" />
            </span>
        </td>
      <td>
            <asp:Label ID="warnLbl" runat="server" Text="<%$ Resources:Share, NotChangeableBecauseOfPublished%>" Visible="false" ForeColor="Red"></asp:Label>
      </td>
    </tr>
  </table>
</div>
<p>&nbsp;</p>
<%--Repeater--%>
<div class="header">
  <h1>Sequence pages </h1>
  <div class="headermenu">
    <asp:Button ID="btnAddPage" CssClass="button-add" runat="server" Text="<%$ resources:AddNewPage %>" Enabled="false" OnClick="btnAddPage_Click" />
    <%--<input type="submit" class="button-open" value="Quick edit" />
    <input type="submit" class="button-add" value="Add page" />--%>
  </div>
  <div class="clear"></div>
</div>
    <asp:Repeater ID="rpPages" runat="server" OnItemDataBound="rpPages_ItemDataBound">
        <HeaderTemplate>
        <div class="list">
         <table>
    <tr>
      <td style="width:5%;"><asp:Literal ID="Literal1" runat="server" Text="<%$ resources:PageNo %>"></asp:Literal></td>
      <td style="width:45%;"><asp:Literal ID="Literal2" runat="server" Text="Heading & Body"></asp:Literal></td>
      <td style="width:10%;"><asp:Literal ID="Literal3" Text="<%$ resources:BeforeShowExpression%>" runat="server"></asp:Literal></td>
      <td style="width:10%;"><asp:Literal ID="Literal8" Text="<%$ resources:AfterShowExpression%>" runat="server"></asp:Literal></td>
      <td style="width:10%;"><asp:Literal ID="Literal5" Text="<%$ resources:PageTemplate%>" runat="server"></asp:Literal></td>
      <td style="width:10%;"><asp:Literal ID="ltLastUpdateBy" Text="<%$ Resources:Share, LastUpdateBy %>" runat="server"></asp:Literal></td>
      <td style="width:10%;">&nbsp;</td>
    </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
            <td>
            <asp:Button ID="ButtonUp" runat="server" CssClass="button-up" CommandArgument='<%#Eval ("Order") %>' OnClick="btnUp_Click" OnClientClick="return OnConfirm();" />
            <p class="counter"><%#Eval("Order")%></p>
            <asp:Button ID="ButtonDown" runat="server" CssClass="button-down" CommandArgument='<%#Eval ("Order") %>' OnClientClick="return OnConfirm();" OnClick="btnDown_Click"/>
            <td><p class="txt-header"><%#Eval("Heading")%></p>
            <p class="txt-body"><%# Eval("Body") != null ? ((string)Eval("Body")).Replace("\r\n", "<br/>").Replace("\r", "<br/>").Replace("\n", "<br/>") : Eval("Body")%></p></td>
            <td><%#Eval("BeforeShowExpression")%></td>
            <td><%#Eval("AfterShowExpression")%></td>
            <td> <%#Eval("TemplateName")%></td>
            <td><p class="user"><%# Eval("LastUpdateBy.UserName") %></p></td>
            <td>
                <div class="buttons">
                    <asp:Button ID="btnEdit" runat="server" Text="<%$ Resources:Share, Edit %>" CommandArgument='<%#Eval ("ID") %>' OnClick="btnEdit_Click" Enabled="false" CssClass="button-open" />
                    <asp:Button ID="btnPreview" runat="server" Text="<%$ Resources:Share, Preview %>" CommandArgument='<%#Eval ("ID") %>' Enabled="true" CssClass="button-open"  />
                    <%--<asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Share, Delete %>" CssClass="button-delete"  OnClientClick="return OnDelete();" CommandArgument='<%#Eval ("Order") %>' OnClick="btnDelete_Click" Enabled="false" />--%>
                    <asp:DropDownList ID="moreOptionsDDL" runat="server"   onchange="return ShowConfirm(this);" DataValueField='<%#Eval ("ID") %>' AutoPostBack="true" CssClass="listmenu-small" OnSelectedIndexChanged="moreOptionsDDL_SelectedIndexChanged"  Enabled="false">
                                    <asp:ListItem>More options</asp:ListItem>
                                    <asp:ListItem Value="Make copy">Make copy</asp:ListItem>
                                    <asp:ListItem Value="Delete">Delete</asp:ListItem>
                                   <%-- <asp:ListItem Value="Preview">Preview</asp:ListItem>--%>
                    </asp:DropDownList>
                   <%--<asp:Button ID="btnMakeCopy" runat="server" Text="<%$ Resources:Share, MakeCopy %>" OnClientClick="return OnConfirm();" CommandArgument='<%#Eval ("Order") %>' OnClick="btnMakeCopy_Click" Enabled="false" />--%>
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
    <asp:HiddenField ID="hfPageSeqID" runat="server" />
    <asp:HiddenField ID="hfFlagMoreReference" runat="server" />
    <asp:HiddenField ID="hfUpdatePageSeq" runat="server" />
    <asp:HiddenField ID="hfEditFlag" runat="server" />
    <script language="javascript" type="text/javascript">
        function OnConfirm() {
            var flag = document.getElementById('<%=hfFlagMoreReference.ClientID %>').value;
            if (flag == "True") {
                if (confirm("Do you want to imfact other session which used this pageSquence?\n if 'yes' press 'ok'\n if 'no' press 'cancel'")) {
                    document.getElementById('<%=hfUpdatePageSeq.ClientID %>').value = "true";
                }
                else {
                    document.getElementById('<%=hfUpdatePageSeq.ClientID %>').value = "false";
                }
            }
            else {
                document.getElementById('<%=hfUpdatePageSeq.ClientID %>').value = "false";
            }
            return true;
        }
        function OnDelete() {
            if (confirm("You are deleting a page from current page sequence, are you sure you want to do this action?")) {
                OnConfirm();
            }
            else {
                return false;
            }
        }
        function openPageD(url, url1) {
            window.open(url, target = '_blank');
            window.open(url1, target = '_blank');
            return false;
        }

        function openPage(url) {
            window.open(url, target = '_blank');
            return false;
        }
    </script>
</asp:Content>
