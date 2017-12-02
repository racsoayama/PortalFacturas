<%@ Page Language="C#" MasterPageFile="~/PortalFacturas.Master" AutoEventWireup="true" CodeBehind="Prueba.aspx.cs" Inherits="PortalFacturas.Prueba" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %><%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <table>
        <tr>
            <td><asp:label ID="lblTitulo" runat="server" CssClass="h2">Reporte de facturas registradas por proveedor</asp:label></td>
        </tr>
        <tr>    
            <td>
                <div>
                    
                    <asp:UpdatePanel ID="updatepanel1" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            <asp:PopupControlExtender ID="TextBox1_PopupControlExtender" runat="server"
                                Enabled="True" ExtenderControlID="" TargetControlID="TextBox1"
                                                     PopupControlID="Panel1" OffsetY="22">
                            </asp:PopupControlExtender>
                            <asp:Panel ID="Panel1" runat="server" Height="116px" Width="145px"
                                                  BorderStyle="Solid" BorderWidth="2px" Direction="LeftToRight"
                                 ScrollBars="Auto" BackColor="#CCCCCC" Style="display: none">
                               
                                <asp:CheckBoxList ID="CheckBoxList1" runat="server"
                                                           DataSourceID="SqlDataSource1" DataTextField="holiday_name"
                                     DataValueField="holiday_name" AutoPostBack="True"
                                    OnSelectedIndexChanged="CheckBoxList1_SelectedIndexChanged">
                                </asp:CheckBoxList>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            
            </td>
        </tr>
        <tr>
            <td>
              <div id="divReporte" style="width:100%; height:100%" runat="server">   
                  <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
                  </asp:ScriptManagerProxy>
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
