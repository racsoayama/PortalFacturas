<%@ Page Title="" Language="C#" MasterPageFile="~/PortalFacturas.Master" AutoEventWireup="true" CodeBehind="ConsFactNal.aspx.cs" Inherits="PortalFacturas.ConsFactNal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainPlaceHolder" runat="server">

    <table>
        <tr>
            <td>
                <asp:label ID="lblTitConsPed" runat="server" CssClass="h2">Consultar estatus de pedidos</asp:label>
            </td>
        </tr>
        <tr>
            <td>
                <div id="divFiltros" runat="server">
                    <table>
                        <tr>
                            <td><asp:label ID="lblProveedor" runat="server" CssClass="label" Width="80">Proveedor:</asp:label></td>
                            <td><asp:textbox ID="txtProveedor" runat="server" CssClass="text" Width="80" MaxLength="10" onKeyUp="validateNumeros(this.id);"></asp:TextBox></td>
                            <td><asp:label ID="lblOrden" runat="server" CssClass="label" Width="100">No. Pedido:</asp:label></td>
                            <td><asp:textbox ID="txtOrden" runat="server" CssClass="text" Width="80" MaxLength="10" onKeyUp="validateNumeros(this.id);"></asp:TextBox></td>
                            <td><asp:Button ID="btnBuscar" runat="server" Value="Buscar" 
                                 onclick="btnBuscar_Click"/></td>
                            <td><asp:Button ID="btnExportar" runat="server" Value="Exportar" 
                                 onclick="btnExportar_Click"/></td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>

        <tr>
            <td>
                <asp:Table ID="tabPedidos" runat="server" Width="100%" CellPadding="2" CellSpacing="0" CssClass="Table"></asp:Table>
<%--                <asp:GridView ID="grdFacturas" runat="server" 
                    CssClass="mGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                    AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center" DataKeyNames="folio, UUID"
                    AllowPaging="true" PageSize="27" onpageindexchanging="grdFacturas_PageIndexChanging1"
                    OnRowDataBound="grdFacturas_RowDataBound">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <img alt = "" style="cursor: pointer" src="images/plus.png" />
                                <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                                    <asp:GridView ID="grdItems" runat="server" AutoGenerateColumns="false" CssClass = "mGrid">
                                        <Columns>
                                            <asp:BoundField ItemStyle-Width="150px" DataField="id_pos_ped" HeaderText="Posicion" />
                                            <asp:BoundField ItemStyle-Width="150px" DataField="ejercicio" HeaderText="Ejercicio" />
                                            <asp:BoundField ItemStyle-Width="150px" DataField="id_entrega" HeaderText="Entrega" />
                                            <asp:BoundField ItemStyle-Width="150px" DataField="id_pos_ent" HeaderText="PosEntrega" />
                                            <asp:BoundField ItemStyle-Width="150px" DataField="cantidad" HeaderText="Cantidad" />
                                            <asp:BoundField ItemStyle-Width="150px" DataField="importe" HeaderText="Importe" />
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField ItemStyle-Width="50px" DataField="Folio" HeaderText="Folio" />
                        <asp:BoundField ItemStyle-Width="250px" DataField="UUID" HeaderText="UUID" />
                        <asp:BoundField ItemStyle-Width="50px" DataField="importe" HeaderText="Importe" />
                        <asp:TemplateField   ItemStyle-CssClass="tableOpcitions"  ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnVerPDF" runat="server" CausesValidation="false" CommandName="VerPDF" CommandArgument="<%# Container.DataItemIndex %>" 
                                    ImageUrl="~/Images/btnGridPDF.png" OnCommand="btnVerPDF_Command" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField   ItemStyle-CssClass="tableOpcitions"  ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnVerXML" runat="server" CausesValidation="false" CommandName="VerXML" CommandArgument="<%# Container.DataItemIndex %>" 
                                    ImageUrl="~/Images/btnGridXML.png" OnCommand="btnVerXML_Command" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>--%>
            </td>
        </tr>
    </table>
</asp:Content>
