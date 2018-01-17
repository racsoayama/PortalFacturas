<%@ Page Title="" Language="C#" MasterPageFile="~/PortalFacturas.Master" AutoEventWireup="true" CodeBehind="ImpResPark.aspx.cs" Inherits="PortalFacturas.ImpResPark" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainPlaceHolder" runat="server">
        
    <table style="border-style: 1; border-color: #C0C0C0; background-color: #FDFDFE">
      <tr>
         <td>
             <asp:label ID="lblTitulo" runat="server" CssClass="h2">Importar resultado del parqueo</asp:label>
         </td>
      </tr>
      <tr><td><br /></td></tr>
     <tr>
         <td>
             <asp:label ID="lblFactLog" runat="server" CssClass="h3">Facturas logísticas</asp:label>
         </td>
      </tr>
      <tr><td><br /></td></tr>
      <tr>
         <td><asp:label ID="lblLeyArchLog" runat="server" CssClass="h3">Seleccione el archivo con los datos a cargar:</asp:label></td>
      </tr>
      <tr>
         <td><INPUT type=file id=File1 name=File1 runat="server" size="50" class="file"/></td>
      </tr>
      <tr>
         <td align="left">
            <asp:Button ID="btnAceptarLog" runat="server"  btnGenerar="Aceptar" 
                OnClientClick="return ValidaDatosLog();" onclick="btnAceptarLog_Click"/>&nbsp;
         </td>
      </tr>
      <tr><td><br /></td></tr>
     <tr>
         <td>
             <asp:label ID="lblFactFin" runat="server" CssClass="h3">Facturas financieras</asp:label>
         </td>
      </tr>
      <tr><td><br /></td></tr>
      <tr>
         <td><asp:label ID="lblLeyArchFin" runat="server" CssClass="h3">Seleccione el archivo con los datos a cargar:</asp:label></td>
      </tr>
      <tr>
         <td><INPUT type=file id=File2 name=File2 runat="server" size="50" class="file"/></td>
      </tr>
      <tr>
        <td align="left">
            <asp:Button ID="btnAceptarFin" runat="server"  btnGenerar="Cancelar" 
                OnClientClick="return ValidaDatosFin();" onclick="btnAceptarFin_Click"/>&nbsp;
         </td>
      </tr>
    </table>

</asp:Content>
