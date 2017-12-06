<%@ Page Title="" Language="C#" MasterPageFile="~/PortalFacturas.Master" AutoEventWireup="true" CodeBehind="PosteoFacturas.aspx.cs" Inherits="PortalFacturas.PosteoFacturas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <table style="border-style: 1; border-color: #C0C0C0; background-color: #FDFDFE">
      <tr>
         <td>
             <asp:label ID="lblTitulo" runat="server" CssClass="h2">Generar archivo para postear las facturas</asp:label>
         </td>
      </tr>

      <tr>
        <td colspan="2">
            <div id="divImportar" runat="server">
                <table>
                    <tr>
                        <td><asp:label ID="lblTipoFacturas" runat="server" CssClass="h3">Selecciona el tipo de facturas:</asp:label></td>
                    </tr>
                    <tr>
                        <td><asp:DropDownList ID="cboTipoFact" runat="server" CssClass="dropdownlist" Width="250"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:ImageButton ID="btnAceptar" runat="server"  ImageUrl="~/Images/btnAceptar.png" CommandName="Cancelar" OnClientClick="return validaDirectorio();" onclick="btnAceptar_Click"/>&nbsp;
                            <asp:ImageButton ID="btnCancelar" runat="server"  ImageUrl="~/Images/btnCancelar.png" CommandName="Cancelar" onclick="btnCancelar_Click"/>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
      </tr>
    </table>

</asp:Content>
