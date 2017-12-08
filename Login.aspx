<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PortalFacturas.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>

   <script src="Scripts\ChangeButtonLang.js"></script>
    <script>
    
        function CallChangeButtonLang() {
            var CurrentCulture = "";
            CurrentCulture = '<%= this.GetCurrentCulture %>';

            ChangeButtonLang(CurrentCulture.substring(0,2));
}
    </script>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <script type='text/JavaScript' src='scripts\FuncValidaciones.js'></script>
    <link href="App_Themes/LoginCSS.css" type="text/css" rel="stylesheet"/>
    <title>Login</title>
   
    <style type="text/css">
        .auto-style1 {
            color: #656565;
            vertical-align: bottom;
            display: inline-block;
            outline: none;
            margin-right: 7px;
            margin-left: 0px;
        }
        .auto-style2 {
            color: #656565;
            vertical-align: bottom;
            display: inline-block;
            outline: none;
            margin-right: 0px;
        }
        .auto-style3 {
            width: 58%;
        }
        .auto-style4 {
            color: #656565;
            border-bottom: 1px solid #a9a9a9;
            display: inline-block;
            outline: none;
            width: 90%;
            min-width: 225px;
            height: 30px;
            margin-top: 4px;
            margin-bottom: 8px;
            vertical-align: top;
            background-color: #fff;
            border-left-style: none;
            border-left-color: inherit;
            border-left-width: medium;
            border-right-style: none;
            border-right-color: inherit;
            border-right-width: medium;
            border-top-style: none;
            border-top-color: inherit;
            border-top-width: medium;
            margin-left: 0px;
        }
        .auto-style5 {
            height: 49px;
        }
        .auto-style6 {
            width: 100%;
        }
        .auto-style7 {
            width: 57%;
        }
        .auto-style8 {
            color: #656565;
            vertical-align: bottom;
            display: inline-block;
            outline: none;
            margin-right: 7px;
        }
    </style>
   
</head>
<body onload="CallChangeButtonLang()">


<%--<div class="header">
    <div class="logoPF"><img src="images/LogoPF.png" width="170" height="50"/></div>
    <div class="logo"><img src="images/LogoTMM.png" width="102" height="68"/></div>
</div>--%>

<div>
     <div class="logo">  <img src="images/logoLands.png" /></div>
    <div class="log-frame">
       
        <form id="Form2" runat="server" defaultfocus="btnAceptar">
            <table align="center" >
                <tr><td class="style6" align="center" valign="middle"><br />
                    <asp:Label ID="lblTitulo" runat="server" CssClass="h2" Width="368px" Height="30px">Bienvenido</asp:Label></td></tr>
                <tr>
                    <td align ="center" class="style6">
                        <div id="divLogin" runat="server">
                            <table style="width: 90%">
                               <%-- <tr><td class="style9" colspan="2">&nbsp;</td></tr>--%>
                                <tr>
                                    <td align="right" class="auto-style7"><asp:Label ID="lblUsuario" runat="server" CssClass="label2" >Usuario:</asp:Label></td>
                                    <td align="left"><asp:TextBox ID="txtUsuario" runat="server"  CssClass="text" MaxLength="10"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="right" class="auto-style7"><asp:Label ID="lblPassword" runat="server" CssClass="label2" >Contraseña:</asp:Label></td>
                                    <td align="left"><asp:TextBox ID="txtPassword" runat="server" password="true" 
                                            CssClass="text"  MaxLength="15" TextMode="Password"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="right" class="auto-style7"><asp:Label ID="lblIdioma" runat="server" CssClass="label2" >Idioma:</asp:Label></td>
                                    <td align="left"><asp:DropDownList ID="cboIdioma" runat="server" CssClass="dropdownlist"  AutoPostBack="true" 
                                                        onselectedindexchanged="cboIdioma_SelectedIndexChanged"></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td align="right" class="auto-style7"><img  id="imgcatcha" src="Captcha.aspx" ></td>
                                    <td align="left"><asp:TextBox ID="txtCaptcha" runat="server" password="true" placeholder="Captcha"
                                            CssClass="text"  MaxLength="8"></asp:TextBox></td>
                                </tr>

                                <tr align="center">
                                    <td class="style7" align="center" colspan="2">
                                        <asp:Button ID="btnCambioPassword" runat="server" value="Contraseña" OnClick="btnCambioPassword_Click"  ></asp:Button>
                                        <asp:Button ID="btnAceptar" runat="server" value="Aceptar" OnClientClick="return ValidaLogin();" OnClick="LoginButton_Click"  ></asp:Button>
                                        <%--<asp:ImageButton ID="btnCancelar" runat="server" ImageUrl="~/Images/btnCerrar.png"  OnClientClick="window.close();"  TabIndex="3"></asp:ImageButton>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <%--<asp:ImageButton ID="btnCambioPassword" runat="server" ImageUrl="~/Images/btnCambiarPassword.png"  OnClick="btnCambioPassword_Click" ToolTip="Cambio de password" TabIndex="1"></asp:ImageButton>--%>
                                        &nbsp;
                                        <asp:Button ID="btnRecuperaCambio" runat="server" Value="Recuperar Contraseña" OnClick="btnRecuperaPassword_Click" TabIndex="1"></asp:Button>
                                        <asp:Button ID="btnCrearCta" runat="server" 
                                            Value="Crear Cuenta"  TabIndex="1" onclick="btnCrearCta_Click"></asp:Button>
                                        <%--<asp:ImageButton ID="btnVerManual" runat="server" 
                                            ImageUrl="~/Images/btnVerManual.png"  TabIndex="1" onclick="btnVerManual_Click"></asp:ImageButton>--%>
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
                                    <td  align="right" class="auto-style3"><asp:Label ID="lblUsuario2" runat="server" CssClass="label2" >Usuario:</asp:Label></td>
                                    <td align="left"><asp:TextBox ID="txtUsuario2" runat="server"  CssClass="text" MaxLength="8"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td  align="right" class="auto-style3"><asp:Label ID="lblPassActual" runat="server" CssClass="auto-style2" Width="160px" >Contraseña actual:</asp:Label></td>
                                    <td align="left"><asp:TextBox ID="txtPassActual" runat="server" password="true" 
                                            CssClass="text"  MaxLength="15" TextMode="Password"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td  align="right" class="auto-style3"><asp:Label ID="lblPassNuevo" runat="server" CssClass="auto-style1" Width="162px" >Nueva contraseña:</asp:Label></td>
                                    <td align="left"><asp:TextBox ID="txtNuevoPassword" runat="server" password="true" CssClass="text"  MaxLength="15" TextMode="Password"></asp:TextBox></td>
                                </tr>           
                                <tr>
                                    <td  align="right" class="auto-style3"><asp:Label ID="lblConfPass" runat="server" CssClass="auto-style8" Width="173px" >Confirme contraseña:</asp:Label></td>
                                    <td align="left"><asp:TextBox ID="txtConfPassword" runat="server" password="true" CssClass="text"  MaxLength="15" TextMode="Password"></asp:TextBox></td>
                                </tr>           
                                <tr>
                                    <td class="style7" align="center" colspan="2">
                                        <asp:Button ID="btnAceptarCambio" runat="server" value="Aceptar" OnClientClick="return ValidaCambio();" OnClick="btnAceptarCambio_Click"  TabIndex="2"></asp:Button>
                                        <asp:Button ID="btnCancelarCambio" runat="server" value="Cancelar" OnClick="btnCancelarCambio_Click"  TabIndex="3"></asp:Button>
                                    </td>
                                </tr>
                               
                            </table>
                        </div>
                    </td>
                </tr>

                <tr>
                    <td align="center" class="style6">
                        <div id="divNuevaCta" runat="server">
                            <table class="auto-style6">
                                <tr>
                                    <td align="left" colspan="2" class="style11"></td>
                                </tr>
                                <tr>
                                    <td align="right" class="auto-style3"><asp:Label ID="lblProv" runat="server" 
                                              CssClass="label2" >Proveedor:</asp:Label></td>
                                    <td align="left"><asp:TextBox ID="txtProveedor" runat="server" 
                                            CssClass="auto-style4"  MaxLength="10" onKeyUp="validateNumeros(this.id);"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="right" class="auto-style3"><asp:Label ID="lblRFC" runat="server" 
                                              CssClass="label2" >RFC Prov:</asp:Label></td>
                                    <td align="left"><asp:TextBox ID="txtRFCProv" runat="server" 
                                            CssClass="text"  MaxLength="14" onKeyUp="changeToUpperCase(this.id);"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="auto-style5" align="center" colspan="2">
                                        <asp:Button ID="btnRegistrar" runat="server" value="Aceptar" OnClientClick="return ValidaDatosRegistro();" OnClick="btnAceptarRegistro_Click"  TabIndex="2"></asp:Button>
                                        <asp:Button ID="btnCancNuevaCta" runat="server" value="Cancelar" OnClick="btnCancelarRegistro_Click"  TabIndex="3"></asp:Button>
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

