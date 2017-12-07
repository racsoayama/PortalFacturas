<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Contacto1.aspx.cs" Inherits="PortalFacturas.Contacto1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Contacto</title>
    <link href="App_Themes/PortalCSS.css" type="text/css" rel="stylesheet"/> 
    <script type='text/JavaScript' src='scripts\FuncValidaciones.js'></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
          <table>
            <tr>
                <td>
                    <asp:label ID="lblLeyTitulo" runat="server" CssClass="h2">Contacto</asp:label>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td><asp:Label ID="lblNombre" runat="server" CssClass="label" Width="70">Nombre:</asp:Label></td>
                            <td><asp:TextBox ID="txtNombre" runat="server" CssClass="text" MaxLength="45" Width="250"></asp:TextBox></td>
                        </tr>
                         <tr>
                            <td><asp:Label ID="lblMail" runat="server" CssClass="label" Width="70">eMail:</asp:Label></td>
                            <td><asp:TextBox ID="txtMail" runat="server" CssClass="text" MaxLength="45" Width="250"></asp:TextBox></td>
                        </tr>
                         <tr>
                            <td valign="top"><asp:Label ID="lblMensaje" runat="server" CssClass="label" Width="70">Mensaje</asp:Label></td>
                            <td><asp:TextBox ID="txtMensaje" runat="server" CssClass="text" TextMode="MultiLine" Height="168px" Width="297px"
                                onkeyDown="checkTextAreaMaxLength(this,event,'300');"></asp:TextBox>
                                <asp:RegularExpressionValidator runat="server" ID="valMensaje"
                                                    ControlToValidate="txtMensaje"
                                                    ValidationExpression="^[\s\S]{0,300}$"
                                                    Display="Dynamic">*</asp:RegularExpressionValidator>
                            </td>
                        </tr>   
                        <tr>
                            <td colspan="2" align="right">
                            <asp:Button ID="btnEnviar" runat="server" Value="Enviar" 
                                             OnClientClick="return ValidaDatos();" OnClick ="btnEnviar_Click"/>
                            <asp:Button ID="btnCancelar" runat="server" value="Cancelar" 
                                             OnClientClick="CloseFormCancel();"/>

                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        </div>
    </form>
</body>
</html>
