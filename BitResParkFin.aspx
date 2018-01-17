<%@ Page Title="" Language="C#" MasterPageFile="~/PortalFacturas.Master" AutoEventWireup="true" CodeBehind="BitResParkFin.aspx.cs" Inherits="PortalFacturas.BitResParkFin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainPlaceHolder" runat="server">
        <table>
        <tr>
            <td>
                <asp:label ID="lblTitulo" runat="server" CssClass="h2">Consultar bitácora de logs de parqueo</asp:label>
            </td>
        </tr>
        <tr>
            <td>
                <div id="divFiltros" runat="server">
                    <table>
                        <tr>
                            <td><asp:label ID="lblFecha" runat="server" CssClass="label" >Fecha:</asp:label></td>
                            <td><asp:textBox ID="txtFecha" runat="server" CssClass="text"  MaxLength="10"></asp:textBox></td>
                            <td><asp:Button ID="btnBuscar" runat="server" Value="Buscar" 
                                 OnClientClick="return ValidaDatos();" onclick="btnBuscar_Click"/></td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="grdBitacora" runat="server" 
                    CssClass="mGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                    AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center" DataKeyNames="fecha"
                    AllowPaging="true" PageSize="25"
                    onpageindexchanging="grdBitacora_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" dataformatstring="{0:dd-MM-yyyy hh:mm}">
                            <ItemStyle  HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="id_usuario" HeaderText="Usuario">
                            <ItemStyle  HorizontalAlign="Center"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="accion" HeaderText="Archivo">
                            <ItemStyle HorizontalAlign="Left"/>  
                        </asp:BoundField>                            

                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>


</asp:Content>
