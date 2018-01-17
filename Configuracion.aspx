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
            <table frame="border"  rules="none" bordercolor="#4c206c" border="1" cellspacing="0" bgcolor="#FDFDFE" style="border-color: #C0C0C0">
                <tr>
                    <td align="right" style="width:50%"><asp:Label ID="lblLongOrden" runat="server" CssClass="label" >Longitud orden compra:</asp:Label></td>
                    <td align="left"><asp:TextBox ID="txtLongOrden" runat="server" CssClass="dropdownlist" MaxLength="2" 
                           onKeyUp="validateNumeros(this.id);" ></asp:TextBox></td>
                </tr>
                <tr>
                    <td align="right"><asp:Label ID="lblMesesAtras" runat="server" CssClass="label" >Meses de retraso en registro:</asp:Label></td>
                    <td align="left"><asp:TextBox ID="txtMesesAtras" runat="server" CssClass="dropdownlist" MaxLength="2" 
                            onKeyUp="validateNumeros(this.id);" ></asp:TextBox></td>
                </tr>
                <tr>
                    <td align="right"><asp:Label ID="lblMesesAdelante" runat="server" CssClass="label" >Meses de facturas posfechadas:</asp:Label></td>
                    <td align="left"><asp:TextBox ID="txtMesesAdelante" runat="server" CssClass="dropdownlist" MaxLength="2" 
                            onKeyUp="validateNumeros(this.id);" ></asp:TextBox></td>
                </tr>
                <tr>
                    <td align="right"><asp:Label ID="lblPDFObligatorio" runat="server" CssClass="label" >PDF obligarorio:</asp:Label></td>
                    <td align="left"><asp:RadioButtonList ID="rbtPDFObligatorio" runat="server" CssClass="checklist" RepeatColumns="2" RepeatDirection="Horizontal" BorderStyle="None"  >
                                        </asp:RadioButtonList></td>
                </tr>
                <tr>
                    <td align="right"><asp:Label ID="lblGuardArchBD" runat="server" CssClass="label" >¿Guardar los archivos en la base de datos?:</asp:Label></td>
                    <td align="left"><asp:RadioButtonList ID="rbtGuardarArch" runat="server" CssClass="checklist" RepeatColumns="2" RepeatDirection="Horizontal" BorderStyle="None"  >
                                        </asp:RadioButtonList></td>
                </tr>
                <tr>
                    <td align="right"><asp:Label ID="lblPerfilProv" runat="server" CssClass="label" >Perfil de proveedores:</asp:Label></td>
                    <td align="left"><asp:DropDownList ID="cboPerfiles" runat="server" CssClass="dropdownlist"  ></asp:DropDownList></td>
                </tr>
                <tr>
                    <td align="right"><asp:Label ID="lblMailContacto" runat="server" CssClass="label" >Correo para contacto:</asp:Label></td>
                    <td align="left"><asp:TextBox ID="txtMailContacto" runat="server" CssClass="dropdownlist"  MaxLength="45" ></asp:TextBox></td>
                </tr>
                <tr>
                    <td align="right"><asp:Label ID="lblIdioma" runat="server" class="label" >Idioma:</asp:Label></td>
                    <td align="left" ><asp:DropDownList ID="cboIdioma" runat="server" 
                            CssClass="dropdownlist" ></asp:DropDownList></td>
                </tr>
                <tr>
                    <td align="right"><asp:Label ID="lblMensaje" runat="server" class="label" >Mensaje:</asp:Label></td>
                    <td><asp:TextBox ID="txtMensaje" runat="server" CssClass="dropdownlist" MaxLength="2" 
                            TextMode="MultiLine"></asp:TextBox></td>
                </tr>
                <tr>
                    <td align="right"><asp:Label ID="lblValSAT" runat="server" CssClass="label" >Validar facturas en el SAT:</asp:Label></td>
                    <td align="left"><asp:RadioButtonList ID="rbtValidaSAT" runat="server" CssClass="checklist" RepeatColumns="2" RepeatDirection="Horizontal" BorderStyle="None" >
                                        </asp:RadioButtonList></td>
                </tr>
                <tr>
                    <td align="right"><asp:Label ID="lblConex" runat="server" CssClass="label" >Conexión con ERP:</asp:Label></td>
                    <td align="left"><asp:RadioButtonList ID="rbtConexERP" runat="server" CssClass="checklist" RepeatColumns="2" RepeatDirection="Horizontal" BorderStyle="None" >
                                        </asp:RadioButtonList></td>
                </tr>
                <tr>
                    <td align="center" colspan="2" >
                        
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