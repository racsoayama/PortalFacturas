<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VisorPDF.aspx.cs" Inherits="PortalFacturas.VisorPDF" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Factura</title>
</head>
<body>
    <form id="form1" runat="server" >
    <div>
         <iframe id="oViewer" runat="server" style="height: 680px; width: 650px"></iframe> 
    </div>
    </form>
</body>
</html>
