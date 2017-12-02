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
            <table frame="border" rules="none" bordercolor="#4c206c" border="3" cellspacing="0">
                <tr>
                    <td><asp:Label ID="lblLongOrden" runat="server" CssClass="label" Width="300">Longitud orden compra:</asp:Label></td>
                    <td><asp:TextBox ID="txtLongOrden" runat="server" CssClass="text" MaxLength="2" 
                            Width="46px" onKeyUp="validateNumeros(this.id);"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:Label ID="lblMesesAtras" runat="server" CssClass="label" Width="300">Meses de retraso en registro:</asp:Label></td>
                    <td><asp:TextBox ID="txtMesesAtras" runat="server" CssClass="text" MaxLength="2" 
                            Width="45px" onKeyUp="validateNumeros(this.id);"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:Label ID="lblMesesAdelante" runat="server" CssClass="label" Width="300">Meses de facturas posfechadas:</asp:Label></td>
                    <td><asp:TextBox ID="txtMesesAdelante" runat="server" CssClass="text" MaxLength="2" 
                            Width="44px" onKeyUp="validateNumeros(this.id);"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:Label ID="lblPDFObligatorio" runat="server" CssClass="label" Width="300">PDF obligarorio:</asp:Label></td>
                    <td><asp:RadioButtonList ID="rbtPDFObligatorio" runat="server" CssClass="checklist" RepeatColumns="2" RepeatDirection="Horizontal">
                                        </asp:RadioButtonList></td>
                </tr>
                <tr>
                    <td><asp:Label ID="lblGuardArchBD" runat="server" CssClass="label" Width="300">¿Guardar los archivos en la base de datos?:</asp:Label></td>
                    <td><asp:RadioButtonList ID="rbtGuardarArch" runat="server" CssClass="checklist" RepeatColumns="2" RepeatDirection="Horizontal">
                                        </asp:RadioButtonList></td>
                </tr>
                <tr>
                    <td><asp:Label ID="lblPerfilProv" runat="server" CssClass="label" Width="300">Perfil de proveedores:</asp:Label></td>
                    <td><asp:DropDownList ID="cboPerfiles" runat="server" CssClass="dropdownlist" Width ="200"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td><asp:Label ID="lblMailContacto" runat="server" CssClass="label" Width="300">Correo para contacto:</asp:Label></td>
                    <td><asp:TextBox ID="txtMailContacto" runat="server" CssClass="text" Width="300" MaxLength="45"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:Label ID="lblIdioma" runat="server" class="label" Width="300">Idioma:</asp:Label></td>
                    <td align="left"><asp:DropDownList ID="cboIdioma" runat="server" 
                            CssClass="dropdownlist" Width ="88px"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td valign="top"><asp:Label ID="lblMensaje" runat="server" class="label" Width="300">Mensaje:</asp:Label></td>
                    <td><asp:TextBox ID="txtMensaje" runat="server" CssClass="text" MaxLength="2" 
                            Width="310px" Height="48px" TextMode="MultiLine"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:Label ID="lblValSAT" runat="server" CssClass="label" Width="300">Validar facturas en el SAT:</asp:Label></td>
                    <td><asp:RadioButtonList ID="rbtValidaSAT" runat="server" CssClass="checklist" RepeatColumns="2" RepeatDirection="Horizontal">
                                        </asp:RadioButtonList></td>
                </tr>
                <tr>
                    <td><asp:Label ID="lblConex" runat="server" CssClass="label" Width="300">Conexión con ERP:</asp:Label></td>
                    <td><asp:RadioButtonList ID="rbtConexERP" runat="server" CssClass="checklist" RepeatColumns="2" RepeatDirection="Horizontal">
                                        </asp:RadioButtonList></td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        
                        <asp:ImageButton ID="btnGuardar" runat="server" 
                            ImageUrl="~/Images/btnGuardar.png" CommandName="Guardar" 
                            OnClientClick="return ValidaDatos();"  onclick="btnGuardar_Click" /> 
                        <asp:ImageButton ID="btnCancelar" runat="server"  
                            ImageUrl="~/Images/btnCancelar.png" CommandName="Cancelar" 
                            onclick="btnCancelar_Click"/>
                    </td>    
                </tr>
                
            </table>
        </td>
      </tr>
    </table>

</asp:Content>
