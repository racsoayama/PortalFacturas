<%@ Page Title="" Language="C#" MasterPageFile="~/PortalFacturas.Master" AutoEventWireup="true" CodeBehind="BitEstFact.aspx.cs" Inherits="PortalFacturas.BitEstFact" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainPlaceHolder" runat="server">
           <table>
        <tr>
            <td>
                <asp:label ID="lblTitulo" runat="server" CssClass="h2">Consultar bitácora carga de status de facturas</asp:label>
            </td>
        </tr>
        <tr>
            <td>
                <div id="divFiltros" runat="server">
                    <table>
                        <tr>
                            <td><asp:label ID="lblFecha" runat="server" CssClass="label" Width="80">Fecha:</asp:label></td>
                            <td><asp:textBox ID="txtFecha" runat="server" CssClass="text" Width="100" MaxLength="10"></asp:textBox></td>
                            <td><asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/Images/btnBuscar.png" CommandName="Buscar" 
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
                            <ItemStyle Width="130px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="id_usuario" HeaderText="Usuario">
                            <ItemStyle Width="80px" HorizontalAlign="Center"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="accion" HeaderText="Archivo">
                            <ItemStyle Width="150px" HorizontalAlign="Left"/>  
                        </asp:BoundField>                            

                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
