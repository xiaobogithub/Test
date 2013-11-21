<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="MovePageSequence.aspx.cs" Inherits="ChangeTech.DeveloperWeb.MovePageSequence" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>Move PageSequence </h1>
  <div class="headermenu">
  </div>
  <div class="clear"></div>
</div>
<div class="box-main">
    <table>
    <tr>
      <td style="width:25%;">&nbsp;</td>
      <td style="width:40%;">&nbsp;</td>
      <td style="width:35%;">&nbsp;</td>
    </tr>
        <tr>
            <td>
                <p class="name"><asp:Literal ID="Literal4" runat="server" Text="<%$ resources:Share,Predictor %>"></asp:Literal></p>
            </td>
            <td>
                <asp:DropDownList ID="predictorDropdownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="predictorDropDownListChanged" CssClass="listmenu-large">
                </asp:DropDownList>
            </td>
            </tr>
            <tr>
            <td>
                <p class="name"><asp:Literal ID="Literal3" runat="server" Text="<%$ resources:Share,InterventCategory %>"></asp:Literal></p>
            </td>
            <td>
                <asp:DropDownList ID="interventCategoryDropdownList" runat="server" AutoPostBack="true"
                    OnSelectedIndexChanged="interventCategoryDropdownList_SelectedIndexChanged" CssClass="listmenu-large">
                </asp:DropDownList>
            </td>
            </tr>
        <tr>
            <td>
                <p class="name"><asp:Literal ID="Literal2" runat="server" Text="<%$ resources:Share,Intervent %>"></asp:Literal></p>
            </td>
            
            <td>
                <asp:DropDownList ID="interventDropdownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="intervnetDropdownList_SelectedIndexChanged" CssClass="listmenu-large">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
      <td style="width:25%;">&nbsp;</td>
      <td style="width:40%;">&nbsp;</td>
      <td style="width:35%;">&nbsp;</td>
    </tr>
    </table>
    </div>
    <h1>&nbsp;</h1>
    <asp:HiddenField ID="pageSequenceList" runat="server" />

    <div class="list">
    <asp:GridView ID="pageSequenceGridView" runat="server" AutoGenerateColumns="false"
        AllowSorting="true" DataKeyNames="PageSequenceID">
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                   <input type="checkbox" name="ifAll" id="ifAll" onclick="checkAll()" class="checkBoxInList">All
                </HeaderTemplate>
                <ItemTemplate>
                    <input type='checkbox' name='pagesequence' value='<%# Eval("PageSequenceID") %>' class="checkBoxInList" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Name" DataField="Name"  ControlStyle-CssClass="name" />
            <asp:BoundField HeaderText="Count of pages" DataField="CountOfPages" HeaderStyle-Width="7%" />
            <asp:BoundField HeaderText="Used in program" DataField="UsedInProgram" HeaderStyle-Width="45%" HeaderStyle-CssClass="descriptionLastTh" />
        </Columns>
    </asp:GridView>
    </div>
    <h1>&nbsp;</h1>

    <div class="box-main">
    <table>
    <tr>
      <td style="width:25%;">&nbsp;</td>
      <td style="width:40%;">&nbsp;</td>
      <td style="width:35%;">&nbsp;</td>
    </tr>
    <tr>
    <td><p class="name">Move them to intervert</p></td>
    <td> 
            <asp:DropDownList ID="targetInterventDropDownList" runat="server" CssClass="listmenu-large">
            </asp:DropDownList>
    </td>
    <td align="center"> 
        <asp:Button ID="moveButton" runat="server"  OnClientClick="GetSelectedCompany();" Text="move" onclick="moveButton_Click" CssClass="button-update" />
    </td>
    </tr>
    <tr>
      <td style="width:25%;">&nbsp;</td>
      <td style="width:40%;">&nbsp;</td>
      <td style="width:35%;">&nbsp;</td>
    </tr>
    </table>
    </div>
    <script language="javascript" type="text/javascript">
        function checkAll() {

            for (var i = 0; i < document.getElementsByName("pagesequence").length; i++) {
                document.getElementsByName("pagesequence")[i].checked = document.getElementById("ifAll").checked;
            }
        }

        function GetSelectedCompany() {
            var strValues = "";
            var el = document.getElementsByName("pagesequence");
            var len = el.length;
            for (var i = 0; i < len; i++) {
                if ((el[i].type == "checkbox") && (el[i].checked == true)) {
                    strValues += el[i].value + ",";
                }
            }

            strValues = strValues.substring(0, strValues.length - 1);
            document.getElementById('<%=pageSequenceList.ClientID %>').value = strValues;
        }
    </script>

</asp:Content>
