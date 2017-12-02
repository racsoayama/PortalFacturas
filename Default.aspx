<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PortalFacturas.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <script type='text/JavaScript' src='scripts\FuncValidaciones.js'></script>
    <title>Login</title>
    <link href="App_Themes/PortalCSS.css" type="text/css" rel="stylesheet"/>
    <style type="text/css">
        .style6
        {
            width: 330px;
        }
        .style7
        {
            height: 49px;
            width: 100%;
        }
        .style8
        {
            width: 330px;
            height: 10px;
        }
        .style9
        {
            width: 330px;
            height: 3px;
        }
        .style10
        {
            height: 23px;
        }
        .style11
        {
            height: 18px;
        }
        </style>

</head>
<body>
    <div class="header">
        <%--<div class="logo"> <img src="images/logo.png" width="104" height="33" alt="logo" /></div>--%>
    </div>
    <div>
        <div class="log-frame">
            <form id="form1" runat="server" defaultfocus="btnAceptar">
                <table align="center" style="width: 90%">
                    <tr><td class="style8"></td></tr>
                    <tr><td class="style6" align="center"><asp:Label ID="lblTitulo" runat="server" Class="h1" Width="200">Bienvenido</asp:Label></td></tr>
                    <tr>
                        <td align ="center" class="style6">
                            <div id="divLogin" runat="server">
                                <table style="width: 90%">
                                    <tr><td class="style9" colspan="2">&nbsp;</td></tr>
                                    <tr>
                                        <td align="right"><asp:Label ID="lblUsuario" runat="server" Class="label2" Width="80">Usuario:</asp:Label></td>
                                        <td align="left"><asp:TextBox ID="txtUsuario" runat="server" Width="70" Class="text" MaxLength="10"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right"><asp:Label ID="lblPassword" runat="server" class="label2" Width="80">Contraseña:</asp:Label></td>
                                        <td align="left"><asp:TextBox ID="txtPassword" runat="server" password="true" 
                                                class="text" Width="70" MaxLength="15" TextMode="Password"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right"><asp:Label ID="lblIdioma" runat="server" class="label2" Width="80">Idioma:</asp:Label></td>
                                        <td align="left"><asp:DropDownList ID="cboIdioma" runat="server" CssClass="dropdownlist" Width ="70"></asp:DropDownList></td>
                                    </tr>
                                    <tr align="center">
                                        <td class="style7" align="center" colspan="2">
                                            <asp:ImageButton ID="btnCambioPassword" runat="server" ImageUrl="~/Images/btnSeguridad.png"  OnClick="btnCambioPassword_Click" ToolTip="Cambio de password" TabIndex="1"></asp:ImageButton>
                                            <asp:ImageButton ID="btnAceptar" runat="server" ImageUrl="~/Images/btnAceptar.png" OnClientClick="return ValidaLogin();" OnClick="LoginButton_Click"  TabIndex="2"></asp:ImageButton>
                                            <asp:ImageButton ID="btnCancelar" runat="server" ImageUrl="~/Images/btnCerrar.png"  OnClientClick="window.close();"  TabIndex="3"></asp:ImageButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <%--<asp:ImageButton ID="btnCambioPassword" runat="server" ImageUrl="~/Images/btnCambiarPassword.png"  OnClick="btnCambioPassword_Click" ToolTip="Cambio de password" TabIndex="1"></asp:ImageButton>--%>
                                            <asp:ImageButton ID="btnRecuperaCambio" runat="server" ImageUrl="~/Images/btnRecuperarPwd.png"  OnClick="btnRecuperaPassword_Click" TabIndex="1"></asp:ImageButton>
                                            <asp:ImageButton ID="btnCrearCta" runat="server" 
                                                ImageUrl="~/Images/btnCrearCuenta.png"  TabIndex="1" onclick="btnCrearCta_Click"></asp:ImageButton>
                                            <asp:ImageButton ID="btnVerManual" runat="server" 
                                                ImageUrl="~/Images/btnVerManual.png"  TabIndex="1" onclick="btnVerManual_Click"></asp:ImageButton>
                                    </td></tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="style6">
                            <div id="divCambioPasw" runat="server">
                                <table style="width: 90%">
                                    <tr>
                                        <td style="width: 60%" align="right"><asp:Label ID="lblUsuario2" runat="server" Class="label2" Width="130">Usuario:</asp:Label></td>
                                        <td align="left"><asp:TextBox ID="txtUsuario2" runat="server" Width="70" Class="text" MaxLength="8"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 60%" align="right"><asp:Label ID="lblPassActual" runat="server" class="label2" Width="130">Contraseña actual:</asp:Label></td>
                                        <td align="left"><asp:TextBox ID="txtPassActual" runat="server" password="true" 
                                                class="text" Width="70" MaxLength="15" TextMode="Password"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 60%" align="right"><asp:Label ID="lblPassNuevo" runat="server" class="label2" Width="130">Nueva contraseña:</asp:Label></td>
                                        <td align="left"><asp:TextBox ID="txtNuevoPassword" runat="server" password="true" class="text" Width="70" MaxLength="15" TextMode="Password"></asp:TextBox></td>
                                    </tr>           
                                    <tr>
                                        <td style="width: 60%" align="right"><asp:Label ID="lblConfPass" runat="server" class="label2" Width="130">Confirme contraseña:</asp:Label></td>
                                        <td align="left"><asp:TextBox ID="txtConfPassword" runat="server" password="true" class="text" Width="70" MaxLength="15" TextMode="Password"></asp:TextBox></td>
                                    </tr>           
                                    <tr>
                                        <td class="style7" align="center" colspan="2">
                                            <asp:ImageButton ID="btnAceptarCambio" runat="server" ImageUrl="~/Images/btnAceptar.png" OnClientClick="return ValidaCambio();" OnClick="btnAceptarCambio_Click"  TabIndex="2"></asp:ImageButton>
                                            <asp:ImageButton ID="btnCancelarCambio" runat="server" ImageUrl="~/Images/btnCancelar.png" OnClick="btnCancelarCambio_Click"  TabIndex="3"></asp:ImageButton>
                                        </td>
                                    </tr>
                               
                                </table>
                            </div>
                        </td>
                    </tr>

                    <tr>
                        <td align="center" class="style6">
                            <div id="divNuevaCta" runat="server">
                                <table style="width: 90%">
                                    <tr>
                                        <td align="left" colspan="2" class="style11"></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50%" align="right"><asp:Label ID="lblProv" runat="server" 
                                                  class="label2" Width="80">Proveedor:</asp:Label></td>
                                        <td align="left"><asp:TextBox ID="txtProveedor" runat="server" 
                                                class="text" Width="100" MaxLength="10" onKeyUp="validateNumeros(this.id);"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50%" align="right"><asp:Label ID="lblRFC" runat="server" 
                                                  class="label2" Width="80">RFC Prov:</asp:Label></td>
                                        <td align="left"><asp:TextBox ID="txtRFCProv" runat="server" 
                                                class="text" Width="100" MaxLength="14" onKeyUp="changeToUpperCase(this.id);"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="style7" align="center" colspan="2">
                                            <asp:ImageButton ID="btnRegistrar" runat="server" ImageUrl="~/Images/btnAceptar.png" OnClientClick="return ValidaDatosRegistro();" OnClick="btnAceptarRegistro_Click"  TabIndex="2"></asp:ImageButton>
                                            <asp:ImageButton ID="btnCancNuevaCta" runat="server" ImageUrl="~/Images/btnCancelar.png" OnClick="btnCancelarRegistro_Click"  TabIndex="3"></asp:ImageButton>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>                
                </table>
            </form>
        </div>
   </div> 
</body>
</html>
