<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ProgramSchedule.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ProgramSchedule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="header">
        <h1> Program schedule overview</h1>
        <div class="headermenu">
        <asp:Button ID="addButton" runat="server" Text="Add a week" OnClick="addButton_Click" CssClass="button-add" />
        </div>
        <div class="clear">
        </div>
    </div>
    <p>&nbsp;</p>
    <b><asp:Label ID="programNameLabel" runat="server" Text="" Font-Bold="true" Font-Size="Medium"></asp:Label></b>
    <p>&nbsp;</p>
    <%--<tr>
            <td>
                Project
            </td>
            <td>
                <asp:DropDownList ID="programDropDownList" AutoPostBack="true" runat="server" OnSelectedIndexChanged="programDropDownList_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Language
            </td>
            <td>
                <asp:DropDownList ID="languageDropDownList" runat="server" AutoPostBack="true" 
                    onselectedindexchanged="languageDropDownList_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>--%>
        <div class="box-main">
        <p class="guide">
        <br />
        <b>Schdeule is the most important thing for a program, so please check this before publish.</b><br />
                    Comment:<br />
                    1. If you want to give a program a schedule, please select the program first.<br />
                    2. As we know, all of programs are base on week, so the schedule make up with weeks
                    and each of week make up with 7 days, if there is class, please checked the day.<br />
                    i.e. If you want to make a schedule for balance, and there are 4 days classes, monday
                    week1, wednesday week1, friday week3, saturday week4.<br />
                    steps:<br />
                    1. select balance program.<br />
                    2. click add a week button, add a week for week1 and check monday and wednesday,
                    click button to add a week for week3 and check friday, do the same the same for
                    week4.<br />
                    After that, oh congratulations you got it.
                    <br />
                    <br />
        </p>
        </div>
        <p>&nbsp;</p> 
       <div class="list">
        <asp:ListView ID="scheduleListView" runat="server" DataKeyNames="week" ItemPlaceholderID="ItemPlaceHolder"
                    OnItemCanceling="scheduleListView_ItemCanceling" OnItemEditing="scheduleListView_ItemEditing"
                    OnItemInserting="scheduleListView_ItemInserting" OnItemUpdating="scheduleListView_ItemUpdating">
                    <LayoutTemplate>
                        <table>
                                <tr>
                                    <td>
                                        Week
                                    </td>
                                    <td>
                                        Monday
                                    </td>
                                    <td>
                                        Tuesday
                                    </td>
                                    <td>
                                        Wednesday
                                    </td>
                                    <td>
                                        Thursday
                                    </td>
                                    <td>
                                        Friday
                                    </td>
                                    <td>
                                        Saturday
                                    </td>
                                    <td>
                                        Sunday
                                    </td>
                                    <td colspan="2">
                                    </td>
                                </tr>
                               <asp:PlaceHolder ID="ItemPlaceHolder" runat="server"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Literal ID="Literal1" runat="server" Text='<%#Eval("week") %>'></asp:Literal>
                            </td>
                            <td>
                                <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%#Eval("monday")%>' Enabled="false" CssClass="noOpacity checkbox-default" />
                            </td>
                            <td>
                                <asp:CheckBox ID="CheckBox2" runat="server" Checked='<%#Eval("tuesday")%>' Enabled="false" CssClass="noOpacity checkbox-default" />
                            </td>
                            <td>
                                <asp:CheckBox ID="CheckBox3" runat="server" Checked='<%#Eval("wednesday")%>' Enabled="false" CssClass="noOpacity checkbox-default" />
                            </td>
                            <td>
                                <asp:CheckBox ID="CheckBox4" runat="server" Checked='<%#Eval("thursday")%>' Enabled="false"  CssClass="noOpacity checkbox-default"/>
                            </td>
                            <td>
                                <asp:CheckBox ID="CheckBox5" runat="server" Checked='<%#Eval("friday")%>' Enabled="false"  CssClass="noOpacity checkbox-default"/>
                            </td>
                            <td>
                                <asp:CheckBox ID="CheckBox6" runat="server" Checked='<%#Eval("saterday")%>' Enabled="false" CssClass="noOpacity checkbox-default" />
                            </td>
                            <td>
                                <asp:CheckBox ID="CheckBox7" runat="server" Checked='<%#Eval("sunday")%>' Enabled="false" CssClass="noOpacity checkbox-default" />
                            </td>
                            <td  style="width:10%">
                                <asp:Button ID="Button1" runat="server" Text="Edit" CommandName="Edit" CssClass="button-open"/>
                                <asp:Button ID="deleteButton" runat="server" Text="Delete" CommandArgument='<%#Eval("week") %>'
                                    OnClientClick="confirm('Are you sure?')" OnClick="deleteButton_Click" CssClass="button-delete" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <InsertItemTemplate>
                        <tr>
                            <td>
                                <asp:TextBox ID="weekTextBox" runat="server" Width="20px" CssClass="textfield-default noOpacity"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="weekTextBox"
                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:CheckBox ID="monCheckBox" runat="server" CssClass="noOpacity checkbox-default" />
                            </td>
                            <td>
                                <asp:CheckBox ID="tueCheckBox" runat="server" CssClass="noOpacity checkbox-default" />
                            </td>
                            <td>
                                <asp:CheckBox ID="wedCheckBox" runat="server" CssClass="noOpacity checkbox-default" />
                            </td>
                            <td>
                                <asp:CheckBox ID="thursCheckBox" runat="server" CssClass="noOpacity checkbox-default" />
                            </td>
                            <td>
                                <asp:CheckBox ID="friCheckBox" runat="server" CssClass="noOpacity checkbox-default" />
                            </td>
                            <td>
                                <asp:CheckBox ID="satCheckBox" runat="server" CssClass="noOpacity checkbox-default" />
                            </td>
                            <td>
                                <asp:CheckBox ID="sunCheckBox" runat="server" CssClass="noOpacity checkbox-default" />
                            </td>
                            <td>
                                <asp:Button ID="saveButton" runat="server" Text="Save" CommandName="Insert" CssClass="button-open" />
                                <asp:Button ID="insertCancelButton" runat="server" Text="Cancel" OnClick="insertCancelButton_Click"
                                    CausesValidation="false" CssClass="button-delete" />
                                
                            </td>
                        </tr>
                    </InsertItemTemplate>
                    <EditItemTemplate>
                        <tr>
                            <td>
                                <asp:TextBox ID="weekTextBox" runat="server" Width="20px" CssClass="noOpacity textfield-default"></asp:TextBox>
                            </td>
                            <td>
                                <asp:CheckBox ID="monCheckBox" runat="server"  CssClass="noOpacity checkbox-default" />
                            </td>
                            <td>
                                <asp:CheckBox ID="tueCheckBox" runat="server"  CssClass="noOpacity checkbox-default" />
                            </td>
                            <td>
                                <asp:CheckBox ID="wedCheckBox" runat="server"   CssClass="noOpacity checkbox-default"/>
                            </td>
                            <td>
                                <asp:CheckBox ID="thursCheckBox" runat="server"  CssClass="noOpacity checkbox-default" />
                            </td>
                            <td>
                                <asp:CheckBox ID="friCheckBox" runat="server"  CssClass="noOpacity checkbox-default" />
                            </td>
                            <td>
                                <asp:CheckBox ID="satCheckBox" runat="server"  CssClass="noOpacity checkbox-default" />
                            </td>
                            <td>
                                <asp:CheckBox ID="sunCheckBox" runat="server"  CssClass="noOpacity checkbox-default" />
                            </td>
                            <td  style="width:10%">
                                <asp:Button ID="Button" runat="server" Text="Update" CommandName="Update" CssClass="button-open" />
                                <asp:Button ID="Button3" runat="server" Text="Cancel" CommandName="Cancel" CssClass="button-delete" />
                            </td>
                        </tr>
                    </EditItemTemplate>
                </asp:ListView>
       </div>
</asp:Content>
