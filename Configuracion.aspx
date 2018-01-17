<%@ Page Title="" Language="C#" MasterPageFile="~/PortalFacturas.Master" AutoEventWireup="true" CodeBehind="Configuracion.aspx.cs" Inherits="PortalFacturas.Configuracion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <table>
    <tr>
        <td>
            <asp:label ID="lblLeyConfig" runat="server" CssClass="h2">Configuración</asp:label>
        </td>
    </tr>
      <tr>
        <td valign="top">
            <table frame="border" rules="none" bordercolor="#4c206c" border="1" cellspacing="0" bgcolor="#FDFDFE" style="border-color: #C0C0C0">
                <tr>
                    <td align="right" style="width: 573px; height: 36px"><asp:Label ID="lblLongOrden" runat="server" CssClass="label" Width="300">Longitud orden compra:</asp:Label></td>
                    <td style="height: 36px"><asp:TextBox ID="txtLongOrden" runat="server" CssClass="dropdownlist" MaxLength="2" 
                            Width="150px" onKeyUp="validateNumeros(this.id);" Height="16px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td align="right" style="width: 573px; height: 36px"><asp:Label ID="lblMesesAtras" runat="server" CssClass="label" Width="300">Meses de retraso en registro:</asp:Label></td>
                    <td style="height: 36px"><asp:TextBox ID="txtMesesAtras" runat="server" CssClass="dropdownlist" MaxLength="2" 
                            Width="150px" onKeyUp="validateNumeros(this.id);" Height="16px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td align="right" style="width: 573px; height: 36px"><asp:Label ID="lblMesesAdelante" runat="server" CssClass="label" Width="300">Meses de facturas posfechadas:</asp:Label></td>
                    <td style="height: 36px"><asp:TextBox ID="txtMesesAdelante" runat="server" CssClass="dropdownlist" MaxLength="2" 
                            Width="150px" onKeyUp="validateNumeros(this.id);" Height="16px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td align="right" style="width: 573px; height: 36px"><asp:Label ID="lblPDFObligatorio" runat="server" CssClass="label" Width="300">PDF obligarorio:</asp:Label></td>
                    <td style="height: 36px"><asp:RadioButtonList ID="rbtPDFObligatorio" runat="server" CssClass="checklist" RepeatColumns="2" RepeatDirection="Horizontal" BorderStyle="None" Height="16px" Width="230px">
                                        </asp:RadioButtonList></td>
                </tr>
                <tr>
                    <td align="right" style="width: 573px; height: 36px"><asp:Label ID="lblGuardArchBD" runat="server" CssClass="label" Width="300">¿Guardar los archivos en la base de datos?:</asp:Label></td>
                    <td style="height: 36px"><asp:RadioButtonList ID="rbtGuardarArch" runat="server" CssClass="checklist" RepeatColumns="2" RepeatDirection="Horizontal" BorderStyle="None" Height="16px" Width="228px">
                                        </asp:RadioButtonList></td>
                </tr>
                <tr>
                    <td align="right" style="width: 573px; height: 36px"><asp:Label ID="lblPerfilProv" runat="server" CssClass="label" Width="300">Perfil de proveedores:</asp:Label></td>
                    <td style="height: 36px"><asp:DropDownList ID="cboPerfiles" runat="server" CssClass="dropdownlist" Width ="150px" Height="16px"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td align="right" style="width: 573px; height: 36px"><asp:Label ID="lblMailContacto" runat="server" CssClass="label" Width="300">Correo para contacto:</asp:Label></td>
                    <td style="height: 36px"><asp:TextBox ID="txtMailContacto" runat="server" CssClass="dropdownlist" Width="150px" MaxLength="45" Height="16px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td align="right" style="width: 573px; height: 36px"><asp:Label ID="lblIdioma" runat="server" class="label" Width="300">Idioma:</asp:Label></td>
                    <td align="left" style="height: 36px"><asp:DropDownList ID="cboIdioma" runat="server" 
                            CssClass="dropdownlist" Width ="150px" Height="16px"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td valign="top" align="right" style="width: 573px; height: 70px"><asp:Label ID="lblMensaje" runat="server" class="label" Width="300">Mensaje:</asp:Label></td>
                    <td style="height: 70px"><asp:TextBox ID="txtMensaje" runat="server" CssClass="dropdownlist" MaxLength="2" 
                            Width="221px" Height="47px" TextMode="MultiLine"></asp:TextBox></td>
                </tr>
                <tr>
                    <td align="left" style="width: 573px; height: 36px"><asp:Label ID="lblValSAT" runat="server" CssClass="label" Width="300">Validar facturas en el SAT:</asp:Label></td>
                    <td style="height: 36px"><asp:RadioButtonList ID="rbtValidaSAT" runat="server" CssClass="checklist" RepeatColumns="2" RepeatDirection="Horizontal" BorderStyle="None" Height="16px" Width="200px">
                                        </asp:RadioButtonList></td>
                </tr>
                <tr>
                    <td align="right" style="width: 573px; height: 36px"><asp:Label ID="lblConex" runat="server" CssClass="label" Width="300">Conexión con ERP:</asp:Label></td>
                    <td style="height: 36px"><asp:RadioButtonList ID="rbtConexERP" runat="server" CssClass="checklist" RepeatColumns="2" RepeatDirection="Horizontal" BorderStyle="None" Height="16px" Width="200px">
                                        </asp:RadioButtonList></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 54px">
                        
                        <asp:Button ID="btnGuardar" runat="server" 
                            IValue="Guardar" 
                            OnClientClick="return ValidaDatos();"  onclick="btnGuardar_Click" /> 
                        <asp:Button ID="btnCancelar" runat="server"  
                            Value="Cancelar" 
                            onclick="btnCancelar_Click"/>
                    </td>    
                </tr>
                
            </table>
        </td>
      </tr>
    </table>

</asp:Content>
