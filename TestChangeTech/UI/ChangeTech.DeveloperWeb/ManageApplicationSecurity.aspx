<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ManageApplicationSecurity.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ManageApplicationSecurity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="header">
  <h1>Manage Application Security </h1>
  <div class="headermenu">
        <asp:Button ID="addAdminLinkButton" runat="server" Text="<%$Resources: AddAdmin%>" OnClick="addAdminLinkButton_Click" CssClass="button-add" />
  </div>
  <div class="clear"></div>
</div> 
<br />
    <p class="guide">
        <asp:Label ID="programLabel" Font-Bold="true" Font-Size="Medium" runat="server" Text="<%$ Resources:AdminDescription%>"></asp:Label>
        <%--<asp:Localize ID="Localize2" runat="server" Text="<%$ Resources:AdminDescription%>"></asp:Localize>--%>
    </p>
    <br />
    <asp:MultiView runat="server" ID="applicationSecurityView" ActiveViewIndex="0">
        <asp:View runat="server" ID="securityListView">
            <asp:Repeater ID="applicationSecurityRepeater" runat="server" OnItemDataBound="applicationSecurityRepeater_ItemDataBound">
                <HeaderTemplate>
                <div class="list">
                    <table>
                        <tr>
                            <%# HeaderString %>
                            <th>
                                <asp:Localize ID="Localize8" runat="server" Text="<%$Resources: FirstName%>"></asp:Localize>
                            </th>
                            <th>
                                <asp:Localize ID="Localize9" runat="server" Text="<%$Resources: LastName%>"></asp:Localize>
                            </th>
                            <th>
                                <asp:Localize ID="Localize10" runat="server" Text="<%$Resources: MobilePhone%>"></asp:Localize>
                            </th>
                            <th>
                                <asp:Localize ID="Localize11" runat="server" Text="<%$Resources: Gender%>"></asp:Localize>
                            </th>
                            <th>
                                <asp:Localize ID="Localize6" runat="server" Text="<%$Resources: UserType%>"></asp:Localize>
                            </th>
                            <th>
                                <asp:Localize ID="Localize1" runat="server" Text="<%$Resources: SMSLogin%>"></asp:Localize>
                            </th>
                            <th style="width:50px">
                            </th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <p class="name"><asp:Label ID="lbAccount" runat="server" Text='<%#Eval("UserName")%>'></asp:Label></p>
                        </td>
                        <td>
                            <p class="user"><asp:Label runat="server" ID="superAdminCheckBox" Text='<%#Eval("FirstName")%>'/></p>
                        </td>
                        <td>
                            <p class="user"><asp:Label runat="server" ID="adminCheckBox" Text='<%#Eval("LastName")%>' /></p>
                        </td>
                        <td>
                            <p class="user"><asp:Label runat="server" ID="createCheckBox" Text='<%#Eval("PhoneNumber")%>' /></p>
                        </td>
                        <td>
                            <p class="user"><asp:Label runat="server" ID="genderCheckBox" Text='<%#Eval("Gender")%>' Width="80px" /></p>
                            <%--<asp:Label runat="server" ID="genderCheckBox" Text='<%#Enum.GetName(typeof(ChangeTech.Models.GenderEnum), Eval("Gender"))%>' Width="80px" />--%>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlUserType" runat="server"   Enabled="false" DataTextField="DisplayText"
                                DataValueField="UserTypeID" CssClass="listmenu-sortby noOpacity">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:CheckBox ID="IsSMSLoginCheckBox" Checked='<%#Eval("IsSMSLogin")%>' Enabled="false"
                                runat="server" CssClass="noOpacity" />
                        </td>
                        <td>
                            <asp:Button runat="server" ID="editBtn" Text="<%$Resources:EditButtonText %>" OnClick="editBtn_Click"
                                CommandArgument='<%#Eval("UserGuid")%>'  CssClass="button-open"/>
                        <asp:Button runat="server" ID="deleteBtn" Text="<%$Resources:DeleteButtonText %>"
                                OnClick="deleteBtn_Click" CommandArgument='<%#Eval("UserGuid")%>'  CssClass="button-delete" />
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
        <asp:View runat="server" ID="editSecurityView">
        <div class="box-main">
         <table>
         <tr>
     <td style="width:35%">&nbsp;</td>
      <td style="width:30%">&nbsp;</td>
      <td style="width:35%">&nbsp;</td>
      </tr>
         <tr>
         <td><p class="name"><asp:Localize ID="Localize7" runat="server" Text="<%$Resources: Email%>"></asp:Localize></p></td>
         <td><asp:TextBox ID="emailTxtBox" runat="server" CssClass="textfield-largetext"></asp:TextBox></td>
         </tr>
         <tr>
         <td><p class="name"> <asp:Localize ID="Localize8" runat="server" Text="<%$Resources: FirstName%>"></asp:Localize></p></td>
         <td><asp:TextBox ID="firstNameTxtBox" runat="server" CssClass="textfield-largetext"></asp:TextBox></td>
         </tr>
         <tr>
         <td><p class="name"><asp:Localize ID="Localize9" runat="server" Text="<%$Resources: LastName%>"></asp:Localize></p></td>
         <td><asp:TextBox ID="lastNameTxtBox" runat="server" CssClass="textfield-largetext"></asp:TextBox></td>
         </tr>
         <tr>
         <td><p class="name"><asp:Localize ID="Localize10" runat="server" Text="<%$Resources: MobilePhone%>"></asp:Localize></p></td>
         <td><asp:TextBox ID="mobilePhoneTxtBox" runat="server" CssClass="textfield-largetext"></asp:TextBox></td>
         </tr>
         <tr>
         <td><p class="name"> <asp:Localize ID="Localize11" runat="server" Text="<%$Resources: Gender%>"></asp:Localize></p></td>
         <td> <asp:DropDownList ID="genderDDL" runat="server" CssClass="listmenu-default">
                            <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                            <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                        </asp:DropDownList></td>
         </tr>
         <tr>
         <td><p class="name"><asp:Localize ID="Localize6" runat="server" Text="<%$Resources: UserType%>"></asp:Localize></p></td>
         <td> <asp:DropDownList ID="ddlUserType" runat="server"  CssClass="listmenu-default" DataTextField="DisplayText"
                            DataValueField="UserTypeID">
                        </asp:DropDownList></td>
         </tr>
         <tr>
         <td><p class="name"><asp:Localize ID="Localize1" runat="server" Text="<%$Resources: SMSLogin%>"></asp:Localize></p></td>
         <td><asp:CheckBox ID="cboIsSMSLogin" Checked='<%#Eval("IsSMSLogin")%>' CssClass="checkbox-default" runat="server" /></td>
         </tr>
          <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td>&nbsp;</td>
    </tr>
         <tr>
         <td></td>
         <td><asp:Button runat="server" ID="saveBtn" Text="<%$ Resources: SaveButtonText %>" OnClick="saveBtn_Click"
                             CssClass="button-update floatRight" /></td>
         </tr>
         </table>
        </div>
        <h1>&nbsp;</h1>
        <h1>Programs user has permission:</h1>
        <div class="list">
        <asp:GridView ID="userHasPermissionGridView" runat="server" AutoGenerateColumns="False"
                            AllowPaging="True" OnPageIndexChanging="userHasPermissionGridView_PageIndexChanging"
                            OnRowCommand="userHasPermissionGridView_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="<%$Resources: Program%>">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <p class="name"><asp:Label ID="Label2" runat="server" Text='<%#Eval("ProgramName")%>'></asp:Label></p>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="<%$Resources: Language%>">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("LanguageName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:Button runat="server" ID="deleteProgramBtn" Text="<%$Resources:DeleteButtonText %>"
                                            CommandArgument='<%#Eval("ProgramGUID")%>'  CssClass="button-delete"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="pagenav" />
                        </asp:GridView>
        </div>
        <h1>&nbsp;</h1>
        <h1> Programs user has not permission:</h1>
        <div class="list">
        <asp:GridView ID="userHasNotPermissionGridView" runat="server" AutoGenerateColumns="False"
                            AllowPaging="True" OnPageIndexChanging="userHasNotPermissionGridView_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="<%$Resources: Program%>">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <p class="name"><asp:Label ID="Label2" runat="server" Text='<%#Eval("ProgramName")%>'></asp:Label></p>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="<%$Resources: Language%>">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("LanguageName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:Button runat="server" ID="addProgramBtn" CommandName="addProgram" Text="<%$Resources:AddPermissionButtonText %>"
                                            CommandArgument='<%#Eval("ProgramGUID")%>' CssClass="button-add floatRight" OnClick="addProgramBtn_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="pagenav" />
                        </asp:GridView>
        </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>
