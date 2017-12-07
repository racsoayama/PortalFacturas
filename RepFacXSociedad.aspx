<%@ Page Language="C#" MasterPageFile="~/PortalFacturas.Master" AutoEventWireup="true" CodeBehind="RepFacXSociedad.aspx.cs" Inherits="PortalFacturas.RepFacXSociedad" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %><%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %> 
<%@ Register TagPrefix="asp" Namespace="Saplin.Controls" Assembly="DropDownCheckBoxes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <table>
        <tr>
            <td><asp:label ID="lblTitulo" runat="server" CssClass="h2">Reporte de facturas registradas por sociedad</asp:label></td>
        </tr>
        <tr>
            <td>
                <div id="divFiltros" runat="server">
                    <table>
                        <tr>
                            <td valign="top"><asp:label ID="lblSociedad" runat="server" CssClass="label">Sociedad:</asp:label></td>
                            <td><asp:DropDownCheckBoxes ID="cboSociedades" runat="server" Width="230px" UseSelectAllNode = "true" CssClass="dropdownlist">
                                    <Style SelectBoxWidth="245" DropDownBoxBoxWidth="210" DropDownBoxBoxHeight="90" />
                                </asp:DropDownCheckBoxes>
                            </td>
                            <td valign="top"><asp:Button ID="btnGenear" runat="server" Value="Visualizar" onclick="btnGenerar_Click"/></td>
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
