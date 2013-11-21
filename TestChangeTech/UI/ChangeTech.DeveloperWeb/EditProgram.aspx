<%@ Page Title="Edit Program" Language="C#" MasterPageFile="~/TempSite.Master" AutoEventWireup="true"
    CodeBehind="EditProgram.aspx.cs" Inherits="ChangeTech.DeveloperWeb.EditProgram" %>

<%@ Import Namespace="ChangeTech.Models" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script type="text/javascript">
            function ShowConfirm(obj) {
                var isSubmit = 1;
                if (obj.selectedIndex == 3) {
                    if (!confirm('You are deleting a session day ,Are you sure you delete it?')) {
                        isSubmit = 0;
                    }
                }
                else if (obj.selectedIndex == 2) {
                    if (!confirm('You are coping a session day ,Are you sure you copy it?')) {
                        isSubmit = 0;
                    }
                }
                if (isSubmit == 1) {
                    __doPostBack("moreOptionsDDL", "");
                }
            }
            $(function () {
                $("#ctl00_ContentPlaceHolder1_ProgramManageDDL").get(0).selectedIndex = 0;
            });
        </script>
</asp:Content>
<asp:Content ID="ContentSiteMapPath" ContentPlaceHolderID="SiteMapPath" runat="server">
    <li>
    <asp:HyperLink ID="hpProgramLink" runat="server" Text="<%$ Resources: Share, Programs%>"
        ToolTip="<%$ Resources: Share, GoPrograms%>"></asp:HyperLink>
    </li>
    <li>
    <span class="lastlinode">
    <asp:Literal ID="ltlEditProgram" Text="<%$ Resources:Share, EditProgram%>" runat="server"></asp:Literal>
    </span>
    </li>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="alertmessage hidden">There is an error somewhere</div>
<div class="confirmationmessage hidden">The program is updated!</div>
<div class="header">
  <h1>Program overview</h1>
  <div class="clear"></div>
</div>
<div class="box-main">
  <div class="flipnav">
    <ul id="languagePanel">
    <%if (lastSelectedLanguage != "Danish")
      { %>
      <li >
        <asp:LinkButton runat="server" ID="DanishLanguageLinkBtn" Visible="false"  Text="Danish" 
              onclick="DanishLanguageLinkBtn_Click"></asp:LinkButton>
              <asp:Label runat="server" Visible="false"  ID="DanishLanguageLabel">Danish</asp:Label>
      </li>
      <%}
      else
      {%>
      <li class="active">
              <asp:Label runat="server"  ID="Label1">Danish</asp:Label>
      </li>
      <%} %>
      <%if (lastSelectedLanguage != "English")
      { %>
      <li >
            <asp:LinkButton ID="EnglishLanguageLinkBtn" runat="server" Visible="false"  Text="English" 
                onclick="EnglishLanguageLinkBtn_Click"></asp:LinkButton>
                <asp:Label runat="server" Visible="false" ID="EnglishLanguageLabel">English</asp:Label>
      </li>
      <%}
      else
      {%>
      <li class="active">
              <asp:Label runat="server"  ID="Label3">English</asp:Label>
      </li>
      <%} %>

       <%if (lastSelectedLanguage != "Finnish")
      { %>
      <li >
            <asp:LinkButton ID="FinnishLanguageLinkBtn" runat="server"  Visible="false" Text="Finnish" 
                onclick="FinnishLanguageLinkBtn_Click"></asp:LinkButton>
                <asp:Label runat="server" Visible="false"  ID="FinnishLanguageLabel">Finnish</asp:Label>
      </li>
      <%}
      else
      {%>
      <li class="active">
              <asp:Label runat="server"  ID="Label4">Finnish</asp:Label>
      </li>
      <%} %>
       <%if (lastSelectedLanguage != "Norwegian")
      { %>
      <li  >
            <asp:LinkButton ID="NorwegianLanguageLinkBtn" Visible="false" runat="server" 
                Text="Norwegian" onclick="NorwegianLanguageLinkBtn_Click"></asp:LinkButton>
            <asp:Label runat="server" Visible="false"  ID="NorwegianLanguageLabel">Norwegian</asp:Label>
      </li>
      <%}
      else
      {%>
      <li class="active">
              <asp:Label runat="server"  ID="Label5">Norwegian</asp:Label>
      </li>
      <%} %>
       <%if (lastSelectedLanguage != "Norwegian Test")
      { %>
      <li >
            <asp:LinkButton ID="NorwegianTestLanguageLinkBtn" Visible="false"  runat="server" 
                Text="NorwegianTest" onclick="NorwegianTestLanguageLinkBtn_Click"></asp:LinkButton>
                <asp:Label runat="server" Visible="false" ID="NorwegianTestLanguageLabel">NorwegianTest</asp:Label>
      </li>
      <%}
      else
      {%>
      <li class="active">
              <asp:Label runat="server"  ID="Label6">NorwegianTest</asp:Label>
      </li>
      <%} %>
       <%if (lastSelectedLanguage != "Icelandic")
      { %>
      <li >
            <asp:LinkButton ID="IcelandicLanguageLinkBtn" Visible="false"  runat="server" Text="Icelandic" 
                onclick="IcelandicLanguageLinkBtn_Click"></asp:LinkButton>
                <asp:Label runat="server" Visible="false"  ID="IcelandicLanguageLabel">Icelandic</asp:Label>
      </li>
      <%}
      else
      {%>
      <li class="active">
              <asp:Label runat="server"  ID="Label7">Icelandic</asp:Label>
      </li>
      <%} %>
       <%if (lastSelectedLanguage != "Spanish")
      { %>
      <li >
            <asp:LinkButton ID="SpanishLanguageLinkBtn" Visible="false"  runat="server" Text="Spanish" 
                onclick="SpanishLanguageLinkBtn_Click"></asp:LinkButton>
                <asp:Label runat="server" Visible="false"  ID="SpanishLanguageLabel">Spanish</asp:Label>
      </li>
      <%}
      else
      {%>
      <li class="active">
              <asp:Label runat="server"  ID="Label8">Spanish</asp:Label>
      </li>
      <%} %>
      <%if (lastSelectedLanguage != "Swedish")
      { %>
      <li >
            <asp:LinkButton ID="SwedishLanguageLinkBtn" Visible="false"  runat="server" Text="Swedish" 
                onclick="SwedishLanguageLinkBtn_Click"></asp:LinkButton>
                <asp:Label runat="server" Visible="false"  ID="SwedishLanguageLabel">Swedish</asp:Label>
      </li>
      <%}
      else
      {%>
      <li class="active"   >
              <asp:Label runat="server"  ID="Label2">Swedish</asp:Label>
      </li>
      <%} %>
      <div class="clear"></div>
    </ul>
  </div>
   <table>
    <tr>
      <td style="width:35%;">&nbsp;</td>
      <td style="width:30%;">&nbsp;</td>
      <td style="width:35%;">&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name">Program name</p></td>
      <td><asp:TextBox ID="txtProgramNameByCtpp" runat="server" CssClass="textfield-largetext"></asp:TextBox></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name"><asp:Literal ID="Literal1" Text="Name in developer" runat="server"></asp:Literal></p></td>
      <td><asp:TextBox ID="txtProgramNameInDeveloper" runat="server" CssClass="textfield-largetext"></asp:TextBox></td>
      <td>&nbsp;</td>
    </tr>   
    <tr>
      <td><p class="name"> <asp:Literal ID="Literal2" Text="<%$ Resources:Share, Description%>" runat="server"></asp:Literal></p></td>
      <td>  <asp:TextBox ID="txtProgramDescription" runat="server" TextMode="MultiLine" Rows="3" CssClass="textfield-largetext" ></asp:TextBox></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td>&nbsp;</td>
    </tr>    
    <tr>
      <td>&nbsp;</td>
      <td>
            <table>
                <tr style="text-align:center;">
                    <td> <asp:Button ID="btnManageProgram" Text="Manage program" runat="server"  
                            CssClass="button-update" onclick="btnManageProgram_Click" /></td>
                    <td><asp:Button ID="btnProgramPage" Text="Program page" runat="server"  
                            CssClass="button-update" onclick="btnProgramPage_Click" /></td>
                    <td><asp:Button ID="btnSubroutines" Text="Subroutines" runat="server" 
                            CssClass="button-update" onclick="btnSubroutines_Click" /></td>
                    <td></td>
                </tr>
            </table>
           
            
            
      </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name">Program management</p></td>
      <td>
        <asp:DropDownList ID="ProgramManageDDL" runat="server" AutoPostBack="true" CssClass="listmenu-large" OnSelectedIndexChanged="ProgramManageDDL_SelectedIndexChanged">
                <asp:ListItem Text="Choose..."></asp:ListItem>
                <asp:ListItem Text="· Program room settings"></asp:ListItem>
                <asp:ListItem Text="· Language settings"></asp:ListItem>
                <asp:ListItem Text="· Program color settings(flash)"></asp:ListItem>
                <asp:ListItem Text="· Check program"></asp:ListItem>
                <asp:ListItem Text="· Export user data"></asp:ListItem>
                <asp:ListItem Text="· User groups"></asp:ListItem>
                <asp:ListItem Text="· Tip messages settings"></asp:ListItem>
                <asp:ListItem Text="· Statistics"></asp:ListItem>
                <asp:ListItem Text="· Pincode page settings"></asp:ListItem>
                <asp:ListItem Text="· Pincode SMS settings"></asp:ListItem>
                <asp:ListItem Text="· SMSes sent from program"></asp:ListItem>
                <asp:ListItem Text="· Export user data - extended"></asp:ListItem>
                <asp:ListItem Text="· Daily SMS settings"></asp:ListItem>
                <asp:ListItem Text="· Copy tip messages from another program"></asp:ListItem>
                <asp:ListItem Text="· Program schedule settings"></asp:ListItem>
                <asp:ListItem Text="· Email template settings"></asp:ListItem>
                <asp:ListItem Text="· Login page settings"></asp:ListItem>
                <asp:ListItem Text="· Password reminder page settings"></asp:ListItem>
                <asp:ListItem Text="· Session end page settings (not in use anymore)"></asp:ListItem>
                <asp:ListItem Text="· Payment page settings"></asp:ListItem>
                <asp:ListItem Text="· User menu settings"></asp:ListItem>
                <asp:ListItem Text="· FAQ settings"></asp:ListItem>
               <%-- <asp:ListItem Text="· Manage program"></asp:ListItem>
                <asp:ListItem Text="· Relapse"></asp:ListItem>
                <asp:ListItem Text="· Program Page(CTPP)"></asp:ListItem>--%>
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
        <td><p class="name"><asp:Literal ID="Literal11" Text="<%$ Resources:Share, Status%>" runat="server"></asp:Literal></p></td>
        <td>
                <asp:DropDownList ID="dropListStatus" runat="server" DataTextField="Value" DataValueField="Key"
                        AutoPostBack="true" CssClass="listmenu-default" OnSelectedIndexChanged="dropListStatus_SelectedIndexChanged">
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
      <td style="text-align:right;"><asp:Button ID="btnUpdateProgram" Text="Update" runat="server" OnClick="btnUpdateProgram_Click" CssClass="button-update" /></td>
      <td>&nbsp;</td>
    </tr>
     <tr>
            <td colspan="3">
                <asp:Label ID="warnLbl" runat="server" Text="<%$ Resources:Share, NotChangeableBecauseOfPublished%>"
                    Visible="false" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <%--<tr>
            <td>
                <asp:Literal ID="Literal6" Text="<%$ Resources:DefaultLanguage%>" runat="server"></asp:Literal>
            </td>
            <td>
                <asp:DropDownList ID="defaultLanguageDropdownlist" runat="server" DataTextField="Name"
                    DataValueField="LanguageGUID" Width="200px" OnSelectedIndexChanged="defaultLanguageDropdownlist_SelectedIndexChanged"
                    AutoPostBack="true">
                </asp:DropDownList>
            </td>
        </tr>--%>
    </table>
    </div>
    <p>&nbsp;</p>
    <%--Repeater-Days--%>
    <div class="header">
        <h1>Days in program </h1>
            <div class="headermenu">
                 <asp:Button runat="server" ID="btnAddNewDay" Text="<%$ Resources:btnAddNewDay %>"
                                Enabled="false" CssClass="button-add" OnClick="btnAddNewDay_Click" />
            </div>
        <div class="clear"></div>
    </div>
    <asp:Repeater ID="rpProgram" runat="server" OnItemDataBound="rpProgram_ItemDataBound">
        <HeaderTemplate>
        <div class="list">
            <table>
                <tr style=" height:23px; ">
                    <th style="width:5%; text-align:left; padding-left:10px;"><asp:Literal ID="Literal2" Text="<%$ Resources:Share, Day%>" runat="server"></asp:Literal></th>
                    <th style=" width:5%; text-align:left; padding-left:10px;"><asp:Literal ID="Literal3" Text="<%$ Resources:Share, PageSequence%>" runat="server"></asp:Literal></th>
                    <th style=" width:40%; text-align:left; padding-left:10px;"> <asp:Literal ID="Literal4" Text="<%$ Resources:Share, Name%>" runat="server" /></th>
                    <th style=" width:20%; text-align:left; padding-left:10px;"> <asp:Literal ID="Literal5" Text="<%$ Resources:Share, Description%>" runat="server" /></th>
                    <th style=" width:15%; text-align:left; padding-left:10px;"><asp:Literal ID="ltLastUpdateBy" Text="<%$ Resources:Share, LastUpdateBy %>" runat="server"></asp:Literal></th>
                    <th style=" width:15%; text-align:left; padding-left:10px;">&nbsp;</th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
             <tr>
                <td>
                <asp:Button ID="ButtonUp" runat="server" CssClass="button-up" CommandArgument='<%#Eval ("ID") %>' OnClick="btnUp_Click"/>
                            <p class="counter"> <%#Eval ("Day")%></p>
                <asp:Button ID="ButtonDown" runat="server" CssClass="button-down" CommandArgument='<%#Eval ("ID") %>' OnClick="btnDown_Click"/>
                </td>
                <td><p class="counter-seq"><%#Eval("PageSequenceNumber")%></p></td>
                <td><p class="name"> <%#Eval("Name")%></p></td>
                <td><p class="description"><%# Eval("Description") != null ? ((string)Eval("Description")).Replace("\r\n", "<br/>").Replace("\r", "<br/>").Replace("\n", "<br/>") : Eval("Description")%> </p>
                        <%--<p class="description">Påmelding</p>--%></td>
                <td><p class="user"><%# Eval("LastUpdateBy.UserName") %></p></td>
                <td> 
                    <div class="buttons">
                       <asp:Button Text="<%$ Resources:Share, Edit %>" ID="btnEdit" CssClass="button-open" runat="server" CommandArgument='<%#Eval ("ID") %>'
                        Enabled="false" OnClick="btnEdit_Click"></asp:Button>
                        <asp:Button ID="btnPageReview" Text="Day review" CssClass="button-open" runat="server" CommandArgument='<%#Eval ("ID") %>'
                        Enabled="false" OnClick="btnPageReview_Click"></asp:Button>
                        <%--<asp:Button ID="btnDelete" Text="<%$ Resources:Share, Delete %>" CssClass="button-delete" runat="server" CommandArgument='<%#Eval ("ID") %>'
                        Enabled="false" OnClientClick="return confirm('Do you confirm to delete this session?');" OnClick="btnDelete_Click"></asp:Button>--%>
                        <asp:DropDownList ID="moreOptionsDDL"  onchange="return ShowConfirm(this);" runat="server" DataValueField='<%#Eval ("ID") %>'
                                 CssClass="listmenu-small" Enabled="false" OnSelectedIndexChanged="moreOptionsDDL_SelectedIndexChanged">
                                <asp:ListItem>More options</asp:ListItem>
                                <asp:ListItem Value="Preview">Preview</asp:ListItem>
                                <asp:ListItem Value="Make copy">Make copy</asp:ListItem>
                                <asp:ListItem Value="Delete">Delete</asp:ListItem>
                                <%--<asp:ListItem Value="Page review">Page review</asp:ListItem>--%>
                        </asp:DropDownList>
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

    <script type="text/javascript">
        function openPageD(url) {
            window.open(url, '_blank');
            window.focus();
            return false;
        }

        function openPage(url) {
            window.open(url);
            return false;
        }

        function programReprot() {
            var url = document.URL.replace("EditProgram", "ProgramReport");
            window.open(url);
            return false;
        }

        function OpenExportUserVariablePage() {

            var url = document.URL.replace("EditProgram", "ExportUserVariable");
            window.open(url, "ExportUserVariable", "width=400,height=160,toolbar=no,scrollbars=no,menubar=no,top=300,left=400");
        }
        function OpenExportUserVariableExtensionPage() {
            var url = document.URL.replace("EditProgram", "ExportUserVariableExtension");
            window.open(url, "ExportUserVariable", "width=400,height=160,toolbar=no,scrollbars=no,menubar=no,top=300,left=400");
        }

//        $("li[name='language']").click(function () {
//            $("li[class='active']").removeAttr("class");
//            $(this).addClass("active");
//            alert($(this).attr("class"));
//        });
    </script>
</asp:Content>
