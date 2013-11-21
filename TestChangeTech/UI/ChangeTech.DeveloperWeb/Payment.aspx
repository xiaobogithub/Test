<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="ChangeTech.DeveloperWeb.Payment"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        body
        {
            color: #4D4F53;
            font-family: Verdana,Arial,Helvetica,sans-serif;
            font-size: 12px;
        }
        a
        {
            color: #4D4F53;
            text-decoration: none;
        }
        .infoPanel
        {
            background-color: #EEEEFF;
            border: 2px solid #658991;
            color: Black;
            font-weight: bold;
            padding: 5px;
        }
        .warningPanel
        {
            background-color: #FFF9D7;
            border: 2px solid #E2C822;
            color: Black;
            font-weight: bold;
            padding: 5px;
        }
        .errorPanel
        {
            background-color: #FFEBE8;
            border: 2px solid #DD3C10;
            color: Black;
            font-weight: bold;
            padding: 5px;
        }
        .okPanel
        {
            background-color: #77C355;
            border: 2px solid #67A54B;
            color: White;
            font-weight: bold;
            padding: 5px;
        }
        .content
        {
            margin: 0 auto;
            position: relative;
            width: 500px;
        }
        .contentPadding
        {
            padding: 10px;
        }
        .box
        {
            background-color: White;
            height: 25px;
            padding-right: 10px;
            text-align: right;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="">
        <div class="content" style="width: 504px; height: 56px;">
            ChangeTech
        </div>
        <div class="content" style="border-left: 2px solid rgb(127, 123, 123); border-right: 2px solid rgb(127, 123, 123);">
            you should pay
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            for this program. Transation ID:
            <asp:Label ID="Label2" runat="server" Text=""></asp:Label><br />
        </div>
        <div class="content">
            <asp:Button ID="paybutton" runat="server" Text="pay it" />
        </div>
    </div>
    </form>
</body>
</html>
