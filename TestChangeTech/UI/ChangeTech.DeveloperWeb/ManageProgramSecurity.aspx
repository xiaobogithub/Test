<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ManageProgramSecurity.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ManageProgramSecurity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="header">
  <h1>Manage Program Security </h1>
  <div class="headermenu">
        <asp:Button ID="RegisterLinkButton" runat="server" OnClick="RegisterLinkButton_Click" Text="<%$Resources: NewUser %>" CssClass="button-add" />
  </div>
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
            <td>
                <p class="name">Program:</p>
            </td>
            <td>
                <asp:DropDownList CssClass="listmenu-large" ID="programDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="programDropDownList_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            </tr>
            <tr>
            <td>
                <p class="name">Language:</p>
            </td>
            <td>
                <asp:DropDownList CssClass="listmenu-default" ID="languageDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="languageDropDownList_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <p class="name">E-mail</p>
            </td>
            <td colspan="2">
                <asp:TextBox ID="emailTextBox" runat="server" CssClass="textfield-largetext"></asp:TextBox>
            </td>
        </tr>
        <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td>&nbsp;</td>
    </tr>
        <tr>
        <td></td>
            <td>
                <asp:Button ID="searchButton" runat="server" Text="Search" OnClick="searchButton_Click" CssClass="button-update floatRight" />
            </td>
        </tr>
    </table>
</div>
<h1>&nbsp;</h1>
    
    <asp:MultiView ID="programSecurityMultiView" runat="server" ActiveViewIndex="0">
        <asp:View runat="server" ID="viewView">
            <asp:Repeater ID="securityListRepeater" runat="server" OnItemDataBound="securityListRepeater_ItemDataBound">
                <HeaderTemplate>
                 <div class="list">
                    <table>
                       <tr>
                                <td style="width:10%">
                                    <asp:Localize ID="Localize6" runat="server" Text="<%$Resources: Account%>"></asp:Localize>
                                </td>
                                <td>
                                    <asp:Localize ID="Localize5" runat="server" Text="Gender"></asp:Localize>
                                </td>
                                <td>
                                    <asp:Localize ID="Localize8" runat="server" Text="First Name"></asp:Localize>
                                </td>
                                <td>
                                    <asp:Localize ID="Localize9" runat="server" Text="Last Name"></asp:Localize>
                                </td>
                                <td>
                                    <asp:Localize ID="Localize18" runat="server" Text="Mobile Phone"></asp:Localize>
                                </td>
                                <td>
                                    <asp:Localize ID="Localize17" runat="server" Text="Pincode"></asp:Localize>
                                </td>
                                <td>
                                    <asp:Localize ID="Localize3" runat="server" Text="Serial number"></asp:Localize>
                                </td>
                                <td>
                                    <asp:Localize ID="Localize15" runat="server" Text="Register Date"></asp:Localize>
                                </td>
                                <td>
                                    <asp:Localize ID="Localize2" runat="server" Text="Current Day"></asp:Localize>
                                </td>
                                <td>
                                    <asp:Localize ID="Localize10" runat="server" Text="Last Logon"></asp:Localize>
                                </td>
                                <td>
                                    <asp:Localize ID="Localize11" runat="server" Text="Last send reminder email"></asp:Localize>
                                </td>
                                <td>
                                    <asp:Localize ID="Localize16" runat="server" Text="Switch Message Date"></asp:Localize>
                                </td>
                                <td>
                                    <asp:Localize ID="Localize14" runat="server" Text="<%$Resources: Status %>"></asp:Localize>
                                </td>
                                <td colspan="">
                                </td>
                            </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <p class="name"><asp:Label ID="lbAccount" runat="server" Text='<%#Eval("Account")%>'></asp:Label></p>
                        </td>
                        <td>
                            <asp:Literal ID="Literal4" runat="server" Text='<%#Eval("Gender")%>'></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="Literal1" runat="server" Text='<%#Eval("FirstName")%>'></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="Literal5" runat="server" Text='<%#Eval("LastName")%>'></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="Literal2" runat="server" Text='<%#Eval("Mobile")%>'></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="Literal6" runat="server" Text='<%#Eval("Pincode")%>'></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="Literal12" runat="server" Text='<%#Eval("SerialNumber")%>'></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="Literal7" runat="server" Text='<%#Eval("RegisterDateStr")%>'></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="Literal8" runat="server" Text='<%#Eval("CurrentDay")%>'></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="Literal9" runat="server" Text='<%#Eval("LastLogonDateStr")%>'></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="Literal10" runat="server" Text='<%#Eval("LastSendEmailDateStr")%>'></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="Literal11" runat="server" Text='<%#Eval("SwithMessageDateStr")%>'></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="Literal3" runat="server" Text='<%#Eval("Status")%>'></asp:Literal>
                        </td>
                        <td>
                            <asp:Button runat="server" ID="btnEdit" Text="<%$Resources:EditButtonText %>" OnClick="editBtn_Click"
                                CommandArgument='<%#Eval("ProgramUserGuid")%>' CssClass="button-open" />
                            <asp:Button ID="deleteButton" runat="server" Text="<%$Resources:Share,Delete %>" CommandArgument='<%#Eval("ProgramUserGuid")%>' OnClientClick='return confirm("Are you sure?");'
                                OnClick="deleteButton_click" CssClass="button-delete" />
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
        </asp:View>

        <asp:View runat="server" ID="editView">
        <div class="box-main">
            <table>
                <tr>
                    <asp:HiddenField ID="programUserGuidHf" runat="server" />
                    <td style="width:35%">&nbsp;</td>
                    <td style="width:30%">&nbsp;</td>
                    <td style="width:35%">&nbsp;</td>
                </tr>
                <tr>
                    <td><p class="name"><asp:Localize ID="Localize4" runat="server" Text="<%$Resources: Account%>"></asp:Localize>:</p></td>
                    <td>
                            <asp:TextBox ID="userEmailTextBox" runat="server" CssClass="textfield-largetext" ></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                    <tr>
                    <td><p class="name"><asp:Localize ID="Localize5" runat="server" Text="Gender"></asp:Localize>:</p></td>
                    <td>
                            <asp:DropDownList ID="genderDropDownList" runat="server" CssClass="listmenu-default">
                                        <asp:ListItem Text="Male"></asp:ListItem>
                                        <asp:ListItem Text="Female"></asp:ListItem>
                                    </asp:DropDownList>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                    <tr>
                    <td><p class="name"> <asp:Localize ID="Localize8" runat="server" Text="First Name"></asp:Localize>:</p></td>
                    <td>
                            <asp:TextBox ID="firstNameTextBox" runat="server" CssClass="textfield-largetext"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td><p class="name"><asp:Localize ID="Localize9" runat="server" Text="Last Name"></asp:Localize>:</p></td>
                    <td>
                            <asp:TextBox ID="lastNameTextBox" runat="server" CssClass="textfield-largetext"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td><p class="name"><asp:Localize ID="Localize18" runat="server" Text="Mobile Phone"></asp:Localize>:</p></td>
                    <td>
                            <asp:TextBox ID="mobileTextBox" runat="server" CssClass="textfield-largetext"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td><p class="name"><asp:Localize ID="Localize17" runat="server" Text="Pincode"></asp:Localize>:</p></td>
                    <td>
                            <asp:TextBox ID="pinCodeTxtBox" runat="server" CssClass="textfield-largetext"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td><p class="name"><asp:Localize ID="Localize1" runat="server" Text="Serial number"></asp:Localize>:</p></td>
                    <td>
                            <asp:TextBox ID="serialNumberTextBox" runat="server" CssClass="textfield-largetext"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td><p class="name"><asp:Localize ID="Localize14" runat="server" Text="<%$Resources: Status %>"></asp:Localize>:</p></td>
                    <td>
                            <asp:DropDownList ID="StatusDropdownList" runat="server" CssClass="listmenu-default">
                                        <asp:ListItem Text="Screening"></asp:ListItem>
                                        <asp:ListItem Text="Registered"></asp:ListItem>
                                        <asp:ListItem Text="Active"></asp:ListItem>
                                        <asp:ListItem Text="Terminated"></asp:ListItem>
                                        <asp:ListItem Text="Paused"></asp:ListItem>
                                        <asp:ListItem Text="Completed"></asp:ListItem>
                                    </asp:DropDownList>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td><p class="divider">&nbsp;</p></td>
                    <td><p class="divider">&nbsp;</p></td>
                    <td><p class="divider">&nbsp;</p></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td align="right">
                        <asp:Button runat="server" ID="saveBtn" Text="<%$ Resources: SaveButtonText %>" OnClick="saveBtn_Click"  CssClass="button-update"/>
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>
