<%@ Page Title="" Language="C#" MasterPageFile="~/PortalFacturas.Master" AutoEventWireup="true" CodeBehind="Entregas.aspx.cs" Inherits="PortalFacturas.Entregas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainPlaceHolder" runat="server">

  <table>
    <tr>
        <td>
            <asp:label ID="lblTitulo" runat="server" CssClass="h2">Entrada de mercancía</asp:label>
        </td>
    </tr>
    <tr>
        <td >
          <div id="divFiltros" runat="server">
            <table class="filtersTable">
                <tr>
                    <td><asp:Label ID="lblFilProv" runat="server" CssClass="h3" >Proveedor:</asp:Label></td>
                    <td><asp:Label ID="lblFilPedido" runat="server" CssClass="h3" >Pedido:</asp:Label></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="txtFilProv" runat="server" CssClass="text"  MaxLength="10" onKeyUp="validateNumeros(this.id);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtFilPedido" runat="server" CssClass="text" W MaxLength="10" onKeyUp="validateNumeros(this.id);"></asp:TextBox></td>
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
                        <td><INPUT type=file id=File1 name=File1 runat="server" size="50"/></td>
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
                <asp:GridView ID="grdEntregas" runat="server" 
                    CssClass="mGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                    AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center" DataKeyNames="id_sociedad, ejercicio, id_pedido"
                    AllowPaging="true" PageSize="25" onpageindexchanging="grdEntregas_PageIndexChanging1">
                    <Columns>
                        <asp:BoundField DataField="ejercicio" HeaderText="Ejercicio">
                            <ItemStyle  HorizontalAlign="Center"/>
                        </asp:BoundField>                            
                        <asp:BoundField DataField="id_pedido" HeaderText="Pedido">
                            <ItemStyle  HorizontalAlign="Center"/>
                        </asp:BoundField>                            
                        <asp:BoundField DataField="id_sociedad" HeaderText="Sociedad">
                            <ItemStyle  HorizontalAlign="Center"/>
                        </asp:BoundField>    
                        <asp:BoundField DataField="id_orgcomp" HeaderText="OrgCompras">
                            <ItemStyle  HorizontalAlign="Center"/>
                        </asp:BoundField>    
                        <asp:BoundField DataField="id_gpocomp" HeaderText="GpoCompras">
                            <ItemStyle  HorizontalAlign="Center"/>
                        </asp:BoundField>    
                        <asp:BoundField DataField="id_clasedoc" HeaderText="ClaseDoc">
                            <ItemStyle  HorizontalAlign="Center"/>
                        </asp:BoundField>    
                        <asp:BoundField DataField="id_proveedor" HeaderText="Proveedor">
                            <ItemStyle  HorizontalAlign="Center"/>
                        </asp:BoundField>                            
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre">
                            <ItemStyle  />
                        </asp:BoundField>
                        <asp:BoundField DataField="id_entrega" HeaderText="Entrega" >
                            <ItemStyle  HorizontalAlign="Center"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="id_posicion" HeaderText="Posicion" >
                            <ItemStyle  HorizontalAlign="Center"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="id_material" HeaderText="Material" >
                            <ItemStyle  HorizontalAlign="Left"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion" HeaderText="Descripcion" >
                            <ItemStyle  HorizontalAlign="Left"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="cantidad" HeaderText="Cantidad" >
                            <ItemStyle  HorizontalAlign="Right"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="ImporteML" HeaderText="ImporteML" >
                            <ItemStyle  HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Importe" HeaderText="Importe" >
                            <ItemStyle  HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Moneda" HeaderText="Moneda" >
                            <ItemStyle  HorizontalAlign="Right" />
                        </asp:BoundField>

<%--                        <asp:TemplateField   ItemStyle-CssClass="tableOptions"  ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnVerDetalles" runat="server" CausesValidation="false" CommandName="Consultar" CommandArgument="<%# Container.DataItemIndex %>" 
                                    ImageUrl="~/Images/btnGridVerDetalles.png" OnCommand="btnEditar_Command" Text="Detalles"  />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                    </Columns>
                </asp:GridView>
            </td>
         </tr>
    </table>

</asp:Content>
