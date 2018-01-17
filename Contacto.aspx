<%@ Page Title="" Language="C#" MasterPageFile="~/PortalFacturas.Master" AutoEventWireup="true" CodeBehind="Contacto.aspx.cs" Inherits="PortalFacturas.Contacto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainPlaceHolder" runat="server">
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
                        <td><asp:Label ID="lblNombre" runat="server" CssClass="label" >Nombre:</asp:Label></td>
                        <td><asp:TextBox ID="txtNombre" runat="server" CssClass="text" MaxLength="45" ></asp:TextBox></td>
                    </tr>
                     <tr>
                        <td><asp:Label ID="lblMail" runat="server" CssClass="label" >eMail:</asp:Label></td>
                        <td><asp:TextBox ID="txtMail" runat="server" CssClass="text" MaxLength="45" ></asp:TextBox></td>
                    </tr>
                     <tr>
                        <td valign="top"><asp:Label ID="lblMensaje" runat="server" CssClass="label" >Mensaje</asp:Label></td>
                        <td><asp:TextBox ID="txtMensaje" runat="server" CssClass="text" TextMode="MultiLine" 
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
                        <asp:Button ID="btnCancelar" runat="server" Value="Cancelar" 
                                         onclick="btnCancelar_Click"/>

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
<%--<form id="contact_form" action="#" method="POST" enctype="multipart/form-data">
	<div class="row">
		<label for="name">Your name:</label><br />
		<input id="name" class="input" name="name" type="text" value="" size="30" /><br />
	</div>
	<div class="row">
		<label for="email">Your email:</label><br />
		<input id="email" class="input" name="email" type="text" value="" size="30" /><br />
	</div>
	<div class="row">
		<label for="message">Your message:</label><br />
		<textarea id="message" class="input" name="message" rows="7" cols="30"></textarea><br />
	</div>
	<input id="submit_button" type="submit" value="Send email" />
</form>						--%>
</asp:Content>
 