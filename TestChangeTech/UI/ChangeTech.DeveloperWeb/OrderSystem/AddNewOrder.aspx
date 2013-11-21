<%@ Page Title="" Language="C#" MasterPageFile="~/OrderSystem/OrderSystem.Master"
    AutoEventWireup="true" CodeBehind="AddNewOrder.aspx.cs" 
    Inherits="ChangeTech.DeveloperWeb.OrderSystem.AddNewOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .bg
        {
            background-color: lightGray;
        }
    </style>
    <script type="text/javascript" src="../Scripts/jquery-ui-1.8.16.custom.min.js"></script>
    <script src="../Scripts/orderSystem.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts/jquery-1.6.2.ct.ordercontent.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            var languageGuid = null;
            var orderTypeGuid = null;
            var openLicenceGuid = 'a0f8fd76-8da3-4592-83fb-339d4bf5419f';
            var chkPromotion = $("#<%=chkPromotion.ClientID %>");
            var txtNumOfEmployees = $("#<%=txtNumOfEmployees.ClientID %>");
            var ddlLicenceType = $("#<%=ddlLicenceType.ClientID %>");
            var ddlLanguage = $("#<%=ddlLanguage.ClientID %>");

            if (ddlLicenceType.val() == openLicenceGuid) {
                chkPromotion[0].disabled = true;
                txtNumOfEmployees.attr("disabled", false).removeClass("bg");

            }
            else {
                chkPromotion[0].disabled = false;
                chkPromotion.attr("checked", false);
                txtNumOfEmployees.attr("disabled", true).addClass("bg").val(""); 

            }

            //OrderTypeDDL Change()
            ddlLicenceType.change(function () {
                orderTypeGuid = ddlLicenceType.val();
                languageGuid = ddlLanguage.val();
                var orderContents = $("#ordersystem").data("orderContents");
                if (orderContents) {
                    $("#ordersystem").data("ordercontent").options.orderTypeGuid = orderTypeGuid;
                    $("#ordersystem").data("ordercontent").loadOrderContents(orderContents);
                    // only input number and length<=5.
                    $('input[class="licence"]').each(function (e) {
                        $(this).numeral();
                    });
                }
                if (ddlLicenceType.val() == openLicenceGuid) {
                    chkPromotion[0].disabled = true;
                    chkPromotion.attr("checked", false);
                    txtNumOfEmployees.attr("disabled", false).removeClass("bg"); 
                }
                else {
                    chkPromotion[0].disabled = false;
                    txtNumOfEmployees.attr("disabled", true).addClass("bg").val("");
                }
            });

            //LanguageDDL Change()
            ddlLanguage.change(function () {
                orderTypeGuid = ddlLicenceType.val();
                languageGuid = ddlLanguage.val();
                var orderContents = $("#ordersystem").data("orderContents");
                if (!orderContents) {
                    LoadOrderContent(languageGuid, orderTypeGuid, null);
                } else {
                    CT.Program.GetOrderPrograms(null, languageGuid, function (widget, data) {
                        $("#ordersystem").data("ordercontent").loadOrderContents(data);
                    });
                }

            });
            txtNumOfEmployees.numeral();

            $("#<%=addButton.ClientID %>").click(function () {
                if (ddlLicenceType.val() == openLicenceGuid) {
                    if (txtNumOfEmployees.val().length <= 0) {
                        alert("Please enter the correct NumOfEmployee's format!");
                        return;
                    }
                }
                else {
                    if (txtNumOfEmployees.val().length > 0) {
                        alert("NumOfEmployee's value should be Empty !");
                        return;
                    }
                }
            });
        });

        function ValidateNumer() {
            $('input[class="licence"]').each(function (e) {
                $(this).numeral();
            });
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>Add new order overview</h1>
  <div class="headermenu">
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
                <p class="name">Customer name :</p>
            </td>
            <td>
            <asp:TextBox ID="txtCustomerName" runat="server"  CssClass="textfield-largetext"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCustomerName"
                    ErrorMessage="<%$ Resources:Required%>"></asp:RequiredFieldValidator>
            </td>
            <td>&nbsp;</td>
        </tr>
         <tr>
            <td>
                <p class="name">Customer email :</p>
            </td>
            <td>
             <asp:TextBox ID="txtCustomerEmail" runat="server" CssClass="textfield-largetext"></asp:TextBox>
                <asp:RequiredFieldValidator ID="emailRequireValidator" runat="server" ErrorMessage="<%$ Resources:EmailRequired%>"
                    ControlToValidate="txtCustomerEmail" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="emailFormatValidator" runat="server" ErrorMessage="<%$ Resources: InvalidEmailFormat %>"
                    ControlToValidate="txtCustomerEmail" ValidationExpression="^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"
                    Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
            <td>&nbsp;</td>
        </tr>
         <tr>
            <td>
                <p class="name">Licence type :</p>
            </td>
            <td>
            <asp:DropDownList ID="ddlLicenceType" runat="server" CssClass="listmenu-large" onselectedindexchanged="ddlLicenceType_SelectedIndexChanged" >
                </asp:DropDownList>
            </td>
            <td>&nbsp;</td>
        </tr>
         <tr>
            <td>
                <p class="name">Number of employees :</p>
            </td>
            <td>
            <asp:TextBox ID="txtNumOfEmployees" runat="server"  CssClass="textfield-largetext"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <p class="name">Language :</p>
            </td>
            <td>
                <asp:DropDownList ID="ddlLanguage" runat="server"  CssClass="listmenu-large" >
                </asp:DropDownList>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <p class="name">Order :</p>
            </td>
            <td>
                    <div id="ordersystem" class="box-main" style="overflow:auto; height:400px;">
                    <table id="orderContents">
                    <tr id="orderContentsHead">
                            <th>
                                <p class="name"><asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:ProgramLicences %>"></asp:Literal></p>
                            </th>
                            <th>
                                <p class="name"><asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:ProgramName %>"></asp:Literal></p>
                            </th>
                        </tr>
                        
        <tr>
      <td style="padding-bottom:5px"><p class="divider">&nbsp;</p></td>
      <td style="padding-bottom:5px"><p class="divider">&nbsp;</p></td>
    </tr>
                    </table>
                </div>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <p class="name">Promotion :</p>
            </td>
            <td>
                <asp:CheckBox ID="chkPromotion"  runat="server" /><label for="chkPromotion" class="checkbox-default">Enable</label>
            </td>
            <td><p class="guide">If this is checked, the programs are not to be charged for.</p></td>
        </tr>
        <tr>
            <td>
                <p class="name">Valid until :</p>
            </td>
            <td>
                 <asp:TextBox ID="txtExpiredDate" runat="server" CssClass="textfield-largetext"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtExpiredDate" Display="Dynamic"  ErrorMessage="<%$ Resources: ExpiredDate%>" 
                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01]).((0[13578]|1[02]))|((0[1-9]|[12][0-9]|30).(0[469]|11))|(0[1-9]|[1][0-9]|2[0-8]).(02)).([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3}))|(29/02/(([0-9]{2})(0[48]|[2468][048]|[13579][26])|((0[48]|[2468][048]|[3579][26])00)))"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtExpiredDate"  Display="Dynamic" 
                    ErrorMessage="<%$ Resources:ExpiredDateRequired%>"></asp:RequiredFieldValidator>
                <br />
            </td>
            <td><p class="guide">This field should by default be 12 months after current date.eg:(dd.MM.yyyy)</p></td>
        </tr>
        <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td>&nbsp;</td>
    </tr>
        <tr>
            <td>&nbsp;</td>
            <td align="right">
                <asp:Button ID="cancelButton" runat="server" Text="<%$ Resources: Cancel %>"  CausesValidation="False" OnClick="cancelButton_Click" CssClass="button-delete"/>
                <asp:Button ID="addButton" runat="server" Text="<%$Resources: OK %>" OnClick="addButton_Click" CssClass="button-open"/>
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
    </div>
</asp:Content>
