<%@ Page Title="" Language="C#" MasterPageFile="Monitor.Master" AutoEventWireup="true"
    CodeBehind="EditLogType.aspx.cs" Inherits="ChangeTech.DeveloperWeb.EditLogType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>Edit LogType overview </h1>
  <div class="headermenu">
  </div>
  <div class="clear"></div>
</div>   
<div style="height:580px;">
<div class="box-main">
    <table>
    <tr>
      <td style="width:35%">&nbsp;</td>
      <td style="width:30%">&nbsp;</td>
      <td style="width:35%">&nbsp;</td>
    </tr>
     <tr>
            <td>
                <p class="name"> <asp:Literal ID="Literal1" Text="<%$ Resources:Share, Name%>" runat="server"></asp:Literal> : </p>
            </td>
            <td>
                <p class="name"><asp:Literal ID="ltlLogTypeName" runat="server" ></asp:Literal></p>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <p class="name"><asp:Literal ID="Literal2" Text="<%$ Resources:Share, LogPriority%>" runat="server"></asp:Literal> : </p>
            </td>
            <td>
               <asp:DropDownList ID="logPriorityDropDownList" runat="server" CssClass="listmenu-large">
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
            <td align="right">
                <asp:Button ID="saveButton" runat="server" Text="Save" CssClass="button-update" OnClick="saveButton_Click" />
                <asp:Button ID="cancelButton" runat="server" CssClass="button-delete" Text="Cancel" OnClick="cancelButton_Click" />
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
    </div>
    </div>
</asp:Content>
