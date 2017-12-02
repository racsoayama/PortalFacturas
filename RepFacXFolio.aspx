<%@ Page Title="" Language="C#" MasterPageFile="~/PortalFacturas.Master" AutoEventWireup="true" CodeBehind="RepFacXFolio.aspx.cs" Inherits="PortalFacturas.RepFactXFolio" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %><%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <table>
        <tr>
            <td><asp:label ID="lblTitulo" runat="server" CssClass="h2">Reporte de facturas registradas por folio</asp:label></td>
        </tr>
        <tr>
            <td>
                <div id="divFiltros" runat="server">
                    <table>
                        <tr>
                            <td><asp:label ID="lblFolInicial" runat="server" CssClass="label">Folio inicial:</asp:label></td>
                            <td><asp:TextBox ID="txtFolInicial" runat="server" CssClass="text" MaxLength="10" 
                                                        Width="60px" onKeyUp="validateNumeros(this.id);"></asp:TextBox></td>
                            <td><asp:label ID="lblFolioFinal" runat="server" CssClass="label">Folio final:</asp:label></td>
                            <td><asp:TextBox ID="txtFolFinal" runat="server" CssClass="text" MaxLength="10" 
                                                        Width="60px" onKeyUp="validateNumeros(this.id);"></asp:TextBox></td>
                            <td><asp:ImageButton ID="btnGenear" runat="server"  ImageUrl="~/Images/btnVisualizar.png" CommandName="Generar" onclick="btnGenerar_Click"/></td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td>
              <div id="divReporte" style="width:100%; height:100%" runat="server">   
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
                    Font-Size="8pt" AsyncRendering="False" SizeToReportContent="true">
                        <LocalReport ReportPath="Reports\RptFacXFolio.rdlc">
                        </LocalReport>
                    </rsweb:ReportViewer>
              </div>
            </td>
        </tr>
    </table>
</asp:Content>
