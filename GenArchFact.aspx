<%@ Page Title="" Language="C#" MasterPageFile="~/PortalFacturas.Master" AutoEventWireup="true" CodeBehind="GenArchFact.aspx.cs" Inherits="PortalFacturas.GenArchFact" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainPlaceHolder" runat="server">
        <table frame="border" style="border-style: solid 1px #c0c0c0; background-color: #FDFDFE;">
      <tr>
         <td>
             <asp:label ID="lblTitulo" runat="server" CssClass="h2">Generar archivos de facturas</asp:label>
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
          <td>
              <table>
                  <tr>
                      <td><asp:label ID="lblLogFechIni" runat="server" CssClass="h3">Fecha del:</asp:label></td>
                      <td><asp:TextBox ID="txtLogFecIni" runat="server" CssClass="text" MaxLength="10" Width="80px" ></asp:TextBox></td>
                      <td><asp:label ID="lblLogFechFin" runat="server" CssClass="h3">al:</asp:label></td>
                      <td><asp:TextBox ID="txtLogFecFin" runat="server" CssClass="text" MaxLength="10" Width="80px" ></asp:TextBox></td>
                      <td><asp:ImageButton ID="btnGenerar" runat="server"  ImageUrl="~/Images/btnGenerar.png" CommandName="Generar" 
                           OnClientClick="return ValidaDatosLog();" onclick="btnGenerarLog_Click"/></td>
                  </tr>
              </table>
          </td>
      </tr>
      <tr><td><br /></td></tr>
      <tr><td><br /></td></tr>
      <tr><td><br /></td></tr>
     <tr>
         <td>
             <asp:label ID="lblFactFin" runat="server" CssClass="h3">Facturas financieras</asp:label>
         </td>
      </tr>
      <tr><td><br /></td></tr>
      <tr>
          <td>
              <table>
                  <tr>
                      <td><asp:label ID="lblFinFechIni" runat="server" CssClass="h3">Fecha del:</asp:label></td>
                      <td><asp:TextBox ID="txtFinFecIni" runat="server" CssClass="text" MaxLength="10" Width="80px" ></asp:TextBox></td>
                      <td><asp:label ID="lblFinFechFin" runat="server" CssClass="h3">al:</asp:label></td>
                      <td><asp:TextBox ID="txtFinFecFin" runat="server" CssClass="text" MaxLength="10" Width="80px" ></asp:TextBox></td>
                      <td>
                          <asp:ImageButton ID="ImageButton1" runat="server"  ImageUrl="~/Images/btnGenerar.png" CommandName="Generar" 
                                                            OnClientClick="return ValidaDatosFin();" onclick="btnGenerarFin_Click"/></td>
                  </tr>
                  </table>
          </td>
      </tr>

    </table>

</asp:Content>
