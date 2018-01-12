<%@ Page Title="" Language="C#" MasterPageFile="~/PortalFacturas.Master" AutoEventWireup="true" CodeBehind="Pedidos.aspx.cs" Inherits="PortalFacturas.Pedidos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <table>
    <tr>
        <td>
            <asp:label ID="lblTitPedidos" runat="server" CssClass="h2">Pedidos</asp:label>
        </td>
    </tr>
    <tr>
        <td >
          <div id="divFiltros" runat="server">
            <table class="filtersTable">
                <tr>
                    <td><asp:Label ID="lblFilProv" runat="server" CssClass="h3" Width="110">Proveedor:</asp:Label></td>
                    <td class="style2" style="width: 172px"><asp:Label ID="lblFilPedido" runat="server" CssClass="h3" Width="110">Pedido:</asp:Label></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="txtFilProv" runat="server" CssClass="text" Width="110" MaxLength="10" onKeyUp="validateNumeros(this.id);"></asp:TextBox></td>
                    <td class="style2" style="width: 172px"><asp:TextBox ID="txtFilPedido" runat="server" CssClass="dropdownlist" Width="150px" MaxLength="10" onKeyUp="validateNumeros(this.id);" Height="16px"></asp:TextBox></td>
                    <td><asp:Button ID="btnBuscar" runat="server" Value="Buscar" 
                         onclick="btnBuscar_Click"/></td>
                    <td><asp:Button ID="btnMostrarTodos" runat="server" Value="Mostrar" 
                         onclick="btnMostrarTodos_Click"/>
                    </td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    <td >
                         <asp:Button ID="btnImportar" runat="server" Value="Importar" 
                                     onclick="btnImportar_Click"/>
                    </td>
                </tr>
            </table>
          </div>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <div id="divImportar" runat="server">
                <table>
                    <tr>
                        <td><asp:label ID="lblLeyArchivo" runat="server" CssClass="h3">Seleccione los archivos con al información a cargar:</asp:label></td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="lblEncabezados" runat="server" CssClass="label" Width="300">Encabezados:</asp:Label></td>
                    </tr>
                    <tr>
                        <td><INPUT type=file id=File1 name=File1 runat="server" size="50"/></td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="lblDetalles" runat="server" CssClass="label"  Width="300">Detalles:</asp:Label></td>
                    </tr>
                    <tr>
                        <td><INPUT type=file id=File2 name=File2 runat="server" size="50"/></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Button ID="btnAceptarImportar" runat="server"  Value="Aceptar" OnClientClick="return validaNombreArchivo();" onclick="btnAceptarImportar_Click"/>&nbsp;
                            <asp:Button ID="btnCancelarImportar" runat="server"  Value="Cancelar" onclick="btnCancelarImportar_Click"/>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
        <tr>
            <td valign="top"  colspan="2">
                <asp:GridView ID="grdPedidos" runat="server" 
                    CssClass="mGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                    AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center" DataKeyNames="id_sociedad, id_pedido"
                    AllowPaging="true" PageSize="25" onpageindexchanging="grdPedidos_PageIndexChanging1"
                    OnRowDataBound="grdPedidos_RowDataBound">
                    <Columns>
                        <asp:TemplateField ItemStyle-CssClass="tableOptions">
                            <ItemTemplate>
                                <img alt = "" style="cursor: pointer" class="botonGrid" src="images/plus.png" />
                                <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                                    <asp:GridView ID="grdDetalle" runat="server" AutoGenerateColumns="false" CssClass = "mGrid2">
                                        <Columns>
                                            <asp:BoundField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" DataField="id_posicion" HeaderText="Posicion" />
                                            <asp:BoundField ItemStyle-Width="130px" ItemStyle-HorizontalAlign="Left" DataField="id_material" HeaderText="Material" />
                                            <asp:BoundField ItemStyle-Width="230px" ItemStyle-HorizontalAlign="Left" DataField="descripcion" HeaderText="Descripcion" />
                                            <asp:BoundField ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Right" DataField="cant_pedida" HeaderText="Cantidad" DataFormatString="{0:N0}" />
                                            <asp:BoundField ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" DataField="Id_unidadMedida" HeaderText="UnidadMedida" />
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="id_pedido" HeaderText="Pedido">
                            <ItemStyle Width="80px" HorizontalAlign="Center"/>
                        </asp:BoundField>                            
                        <asp:BoundField DataField="id_sociedad" HeaderText="Sociedad">
                            <ItemStyle Width="80px" HorizontalAlign="Center"/>
                        </asp:BoundField>    
                        <asp:BoundField DataField="id_orgcomp" HeaderText="OrgCompras">
                            <ItemStyle Width="80px" HorizontalAlign="Center"/>
                        </asp:BoundField>    
                        <asp:BoundField DataField="id_gpocomp" HeaderText="GpoCompras">
                            <ItemStyle Width="80px" HorizontalAlign="Center"/>
                        </asp:BoundField>    
                        <asp:BoundField DataField="id_clasedoc" HeaderText="ClaseDoc">
                            <ItemStyle Width="80px" HorizontalAlign="Center"/>
                        </asp:BoundField>    
                        <asp:BoundField DataField="id_proveedor" HeaderText="Proveedor">
                            <ItemStyle Width="80px" HorizontalAlign="Center"/>
                        </asp:BoundField>                            
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre">
                            <ItemStyle Width="250px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Fecha_pedido" HeaderText="Fecha" DataFormatString="{0:dd-MM-yyyy}" >
                            <ItemStyle Width="80px" HorizontalAlign="Center"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="Id_moneda" HeaderText="Moneda" >
                            <ItemStyle Width="60px" HorizontalAlign="Center"/>
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </td>
         </tr>
    </table>

</asp:Content>
