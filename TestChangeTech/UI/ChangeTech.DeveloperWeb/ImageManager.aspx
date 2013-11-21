<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImageManager.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ImageManager" %>

<%@ Register Assembly="System.Web.Silverlight" Namespace="System.Web.UI.SilverlightControls"
    TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <Ajax:ToolkitScriptManager runat="server" ID="toolkitScriptManage1">
    </Ajax:ToolkitScriptManager>
    <div>
        <asp:Silverlight ID="Silverlight1" Visible="true" 
            AutoUpgrade="true" ClientIDMode="Static" Windowless="true" runat="server" Height="690"
            Width="800" />
    </div>
    </form>
</body>
</html>
