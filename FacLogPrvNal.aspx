
<%@ Page Language="C#" MasterPageFile="~/PortalFacturas.Master" AutoEventWireup="true" CodeBehind="FacLogPrvNal.aspx.cs" Inherits="PortalFacturas.FacLogPrvNal" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainPlaceHolder" runat="server">

    <table>
    <tr>
        <td>
            <asp:label ID="lblTitulo" runat="server" CssClass="h2">Registrar facturas</asp:label>
        </td>
    </tr>
    <tr>
        <td valign="top" style="width:650px;">
            <table>
                <%--Datos iniciales para cargar la factura--%>
                <tr>
                    <td>
                        <div id="divImportar" runat="server">
                            <table>
                                <tr>
                                    <td colspan="2"><asp:label ID="lblArchFacturas" runat="server" CssClass="h3">Archivos de la factura:</asp:label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td><asp:Label ID="lblSociedad" runat="server" CssClass="label" Width="120">Sociedad:</asp:Label></td>
                                                <td><asp:DropDownList ID="cboSociedades" runat="server" 
                                                        CssClass="dropdownlist" Width ="200px"></asp:DropDownList></td>
                                            </tr>
                                            <tr>
                                                <td><asp:Label ID="lblOrden" runat="server" class="label" Width="120">Orden de compra:</asp:Label></td>
                                                <td><asp:TextBox ID="txtOrden" runat="server" CssClass="dropdownlist" MaxLength="10" 
                                                        Width="150px" onKeyUp="validateNumeros(this.id);" Height="16px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td colspan="2"><asp:label ID="lblLeyArchPDF" runat="server" CssClass="label" Width="330">Seleccione el archivo PDF de la factura a subir:</asp:label></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="height: 36px"><INPUT type=file id=File1 name=File1 runat="server" size="50" accept="image/*.pdf" /></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2"><asp:label ID="lblLayArchXML" runat="server" CssClass="label" Width="330">Seleccione el archivo XML de la factura a subir:</asp:label></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="height: 36px"><INPUT type=file id=File2 name=File1 runat="server" size="50" accept="image/*.xml" /></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2"><asp:label ID="lblOtrosArchivos" runat="server" CssClass="h3" Width="330">Otros archivos:</asp:label></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 36px"><asp:label ID="lblArchivo1" runat="server" CssClass="label" Width="70">Archivo 1:</asp:label></td>
                                                <td style="height: 36px"><INPUT type=file id=File3 name=File1 runat="server" size="37" accept="*.*" /></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 36px"><asp:label ID="lblArchivo2" runat="server" CssClass="label" Width="70">Archivo 2:</asp:label></td>
                                                <td style="height: 36px"><INPUT type=file id=File4 name=File2 runat="server" size="37" accept="*.*" /></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 36px"><asp:label ID="lblArchivo3" runat="server" CssClass="label" Width="70">Archivo 3:</asp:label></td>
                                                <td style="height: 36px"><INPUT type=file id=File5 name=File4 runat="server" size="37" accept="*.*" /></td>
                                            </tr>
                                            </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="right">
                                        <asp:ImageButton ID="btnAceptarImportar" runat="server"  ImageUrl="~/Images/btnAceptar.png" 
                                            CommandName="Cancelar" OnClientClick="return ValidaNombreArchivo();" onclick="btnAceptarImportar_Click"/>&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <%--Detalle de la factura--%>
                <tr>
                    <td>
                        <div id="divDetalle" runat="server">
                            <table>
                                <tr>
                                    <td colspan="2"><asp:label ID="lblDetFactura" runat="server" CssClass="h3">Detalle de la factura:</asp:label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td><asp:label ID="lblUUID" runat="server" CssClass="label" Width="100">Certificado SAT:</asp:label></td>
                                                <td><asp:textbox ID="txtUUID" runat="server" CssClass="dropdownlist" Width="150px" MaxLength="36" Height="16px"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td><asp:label ID="lblNumFactura" runat="server" CssClass="label" Width="100">Factura:</asp:label></td>
                                                <td><asp:textBox ID="txtFactura" runat="server" CssClass="dropdownlist" Width="150px" MaxLength="10" Height="16px"></asp:textBox></td>
                                            </tr>
                                            <tr>
                                                <td><asp:label ID="lblEmisor" runat="server" CssClass="label" Width="100">Emisor:</asp:label></td>
                                                <td><asp:textBox ID="txtEmisor" runat="server" CssClass="dropdownlist" Width="150px" MaxLength="13" Height="16px"></asp:textBox></td>
                                            </tr>
                                            <tr>
                                                <td><asp:label ID="lblReceptor" runat="server" CssClass="label" Width="100">Receptor:</asp:label></td>
                                                <td><asp:textBox ID="txtReceptor" runat="server" CssClass="dropdownlist" Width="150px" Height="16px"></asp:textBox></td>
                                            </tr>
                                            <tr>
                                                <td><asp:label ID="lblFecha" runat="server" CssClass="label" Width="100">Fecha:</asp:label></td>
                                                <td><asp:textBox ID="txtFecha" runat="server" CssClass="dropdownlist" Width="150px" MaxLength="10" Height="16px"></asp:textBox></td>
                                            </tr>
                                            <tr>
                                                <td><asp:label ID="lblImporte" runat="server" CssClass="label" Width="100">Importe:</asp:label></td>
                                                <td><asp:textBox ID="txtImporte" runat="server" CssClass="dropdownlist" Width="150px" Height="16px"></asp:textBox></td>
                                            </tr>
                                            <tr>
                                                <td><asp:label ID="lblMoneda" runat="server" CssClass="label" Width="100">Moneda:</asp:label></td>
                                                <td><asp:DropDownList ID="cboMoneda" runat="server" 
                                                        CssClass="dropdownlist" Width ="150px" Height="16px"></asp:DropDownList></td>
                                            </tr>
                                            <tr>
                                                <td><asp:label ID="lblValidacion" runat="server" CssClass="label" Width="100">Validación:</asp:label></td>
                                                <td><asp:textBox ID="txtValidacion" runat="server" CssClass="dropdownlist" Width="150px" Height="16px"></asp:textBox></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                      <asp:GridView ID="grdDetalle" runat="server" 
                                            CssClass="mGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                                            AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center" DataKeyNames="Descripcion"
                                            OnRowDataBound="grdDetalle_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="descripcion" HeaderText="Descripcion">
                                                    <ItemStyle Width="200px" HorizontalAlign="Left"/>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad">
                                                    <ItemStyle Width="100px" HorizontalAlign="Center"/>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Unidad" HeaderText="Unidad">
                                                    <ItemStyle Width="50px" HorizontalAlign="Center"/>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="importe" HeaderText="Importe" DataFormatString="{0:N}">
                                                    <ItemStyle Width="70px" HorizontalAlign="Right"/>
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>                                    
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:ImageButton ID="btnAceptarFactura" runat="server" ImageUrl="~/Images/btnAceptar.png" 
                                            CommandName="Cancelar" OnClientClick="return ValidaMoneda();" onclick="btnAceptarFactura_Click"/>&nbsp;
                                        <asp:ImageButton ID="btnCancelarAgregar" runat="server"  
                                            ImageUrl="~/Images/btnCancelar.png" CommandName="Cancelar" 
                                            onclick="btnCancelarAgregar_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>

            </table>
        </td>
        <td valign="top" style="width:600px;">
            <table>
                <%--Visor de PDF--%>
                <tr>
                    <td>
                        <div id="divVisor" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <Iframe id="oViewer" runat="server" style="height: 550px; width: 600px"></Iframe> 
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>

            </table>
        </td>
    </tr>
    <%--Items la factura--%>
    <tr>
        <td colspan="2">
            <div id="divItemsFac" runat="server">
                <table>
                <tr>
                    <td><asp:label ID="Label2" runat="server" CssClass="h3">Items de la factura</asp:label></td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="grdDetFactura" runat="server" 
                            CssClass="mGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                            AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center" DataKeyNames="Descripcion"
                            OnRowDataBound="grdDetalle_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="factura" HeaderText="Factura">
                                    <ItemStyle Width="80px" HorizontalAlign="Left"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}">
                                    <ItemStyle Width="80px" HorizontalAlign="Left"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="descripcion" HeaderText="Descripcion">
                                    <ItemStyle Width="280px" HorizontalAlign="Left"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad">
                                    <ItemStyle Width="100px" HorizontalAlign="Center"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="Unidad" HeaderText="Unidad">
                                    <ItemStyle Width="50px" HorizontalAlign="Center"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="importe" HeaderText="Importe" DataFormatString="{0:N}">
                                    <ItemStyle Width="70px" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="Impuestos" HeaderText="Iva" DataFormatString="{0:N}">
                                    <ItemStyle Width="70px" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="ImpNeto" HeaderText="Total" DataFormatString="{0:N}">
                                    <ItemStyle Width="70px" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="Moneda" HeaderText="Moneda">
                                    <ItemStyle Width="70px" HorizontalAlign="Center"/>
                                </asp:BoundField>
<%--                                <asp:BoundField DataField="Nota_ent" HeaderText="Referencia">
                                    <ItemStyle Width="70px" HorizontalAlign="Center"/>
                                </asp:BoundField>--%>
<%--                                            <asp:BoundField DataField="Entrega" HeaderText="Entrega">
                                    <ItemStyle Width="100px" HorizontalAlign="Center"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="Posicion" HeaderText="Posicion">
                                    <ItemStyle Width="60px" HorizontalAlign="Center"/>
                                </asp:BoundField>--%>
                            </Columns>
                        </asp:GridView>                                    
                    </td>
                </tr>
                </table>  
            </div>
        </td>
    </tr>
    <%--Entregas--%>
    <tr>
        <td colspan="2">
            <div id="divEntregas" runat="server">
                <table>
                    <tr>
                        <td>
                            <asp:label ID="lblTitEntregas" runat="server" CssClass="h3">Seleccione las entradas de mercancía:</asp:label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="grdEntregas" runat="server" 
                                CssClass="mGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                                AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center" DataKeyNames="ejercicio, id_entrega, id_posicion"
                                OnRowDataBound="grdEntregas_RowDataBound">
                                <Columns>
                                    <asp:TemplateField   ItemStyle-CssClass="tableOptions"  ShowHeader="True"  HeaderText="">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSeleccion" runat="server" Width="30"/>
                                        </ItemTemplate>
                                        <ItemStyle Width="30px" HorizontalAlign ="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="id_pedido" HeaderText="Pedido">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Id_pos_ped" HeaderText="PosPedido">
                                        <ItemStyle Width="50px" HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Id_entrega" HeaderText="Entrega">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Id_posicion" HeaderText="PosEntrega">
                                        <ItemStyle Width="50px" HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Cantidad" HeaderText="Cantidad">
                                        <ItemStyle Width="80px" HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Importe" HeaderText="Importe" DataFormatString="{0:N}">
                                        <ItemStyle Width="80px" HorizontalAlign="Right"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Id_material" HeaderText="Material">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Descripcion" HeaderText="Descripcion">
                                        <ItemStyle Width="200px" HorizontalAlign="Left"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Nota_entrega" HeaderText="Referencia">
                                        <ItemStyle Width="100px" HorizontalAlign="Left"/>
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>                                    
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:label ID="lblTitCostos" runat="server" CssClass="h3">Seleccione los Costos indirectos:</asp:label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="grdCostos" runat="server" 
                                CssClass="mGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                                AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center" DataKeyNames="ejercicio, id_entrega, id_posicion"
                                OnRowDataBound="grdCostos_RowDataBound">
                                <Columns>
                                    <asp:TemplateField   ItemStyle-CssClass="tableOptions"  ShowHeader="True"  HeaderText="">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSeleccion" runat="server" Width="30"/>
                                        </ItemTemplate>
                                        <ItemStyle Width="30px" HorizontalAlign ="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="id_pedido" HeaderText="Pedido">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Id_pos_ped" HeaderText="PosPedido">
                                        <ItemStyle Width="50px" HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Id_entrega" HeaderText="Entrega">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Id_posicion" HeaderText="PosEntrega">
                                        <ItemStyle Width="50px" HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Cantidad" HeaderText="Cantidad">
                                        <ItemStyle Width="80px" HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Importe" HeaderText="Importe" DataFormatString="{0:N}">
                                        <ItemStyle Width="80px" HorizontalAlign="Right"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="id_proveedor" HeaderText="Proveedor">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Id_tipoCond" HeaderText="TipoCond">
                                        <ItemStyle Width="80px" HorizontalAlign="Left"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ref_docto" HeaderText="Referencia">
                                        <ItemStyle Width="100px" HorizontalAlign="Left"/>
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>                                    
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:ImageButton ID="btnAceptarEntregas" runat="server"  ImageUrl="~/Images/btnAceptar.png" 
                                CommandName="Aceptar" OnClientClick="return ConfirmarNotas();" onclick="btnAceptarEntregas_Click"/>
                            <asp:ImageButton ID="btnCancelarEntregas" runat="server"  ImageUrl="~/Images/btnCancelar.png" 
                                CommandName="Aceptar" onclick="btnCancelarEntregas_Click"/>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>

    <%--Servicios--%>
    <tr>
        <td colspan="2">
            <div id="divServicios" runat="server">
                <table>
                    <tr>
                        <td>
                            <asp:label ID="lblTitServicios" runat="server" CssClass="h3">Seleccione los servicios:</asp:label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="grdServicios" runat="server" 
                                CssClass="mGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                                AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center" DataKeyNames="id_pos_ped, id_documento, ejercicio, id_posicion"
                                OnRowDataBound="grdServicios_RowDataBound">
                                <Columns>
                                    <asp:TemplateField   ItemStyle-CssClass="tableOptions"  ShowHeader="True"  HeaderText="">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSeleccion" runat="server" Width="30"/>
                                        </ItemTemplate>
                                        <ItemStyle Width="30px" HorizontalAlign ="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="id_documento" HeaderText="Documento">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Id_posicion" HeaderText="Posicion">
                                        <ItemStyle Width="50px" HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Cantidad" HeaderText="Cantidad">
                                        <ItemStyle Width="80px" HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Importe" HeaderText="Importe" DataFormatString="{0:N}">
                                        <ItemStyle Width="80px" HorizontalAlign="Right"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Moneda" HeaderText="Moneda">
                                        <ItemStyle Width="80px" HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Ref_docto" HeaderText="Referencia">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>                                    
                        </td>
                    </tr>
                    <tr>
                        <td align="right" >
                            <asp:ImageButton ID="btnAceptarServ" runat="server"  ImageUrl="~/Images/btnAceptar.png" 
                                CommandName="Aceptar" onclick="btnAceptarEntregas_Click"/>
                            <asp:ImageButton ID="btnCancelar2" runat="server"  ImageUrl="~/Images/btnCancelar.png" 
                                CommandName="Aceptar" onclick="btnCancelarEntregas_Click"/>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>

    <%--Facturas--%>
    <tr>
        <td colspan="2">
            <div id="divFacturas" runat="server">
                <table>
                    <tr>
                        <td colspan="2">
                           <asp:GridView ID="grdFacturas" runat="server" 
                                CssClass="mGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                                AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center" DataKeyNames="UUID"
                                OnRowDataBound="grdFacturas_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="id_sociedad" HeaderText="Sociedad">
                                        <ItemStyle Width="80px" HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UUID" HeaderText="UUID">
                                        <ItemStyle Width="250px" HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="folioFact" HeaderText="Factura">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="emisor" HeaderText="Emisor">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Importe" HeaderText="Importe" DataFormatString="{0:N}">
                                        <ItemStyle Width="100px" HorizontalAlign="Right"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="id_moneda" HeaderText="Moneda" >
                                        <ItemStyle Width="50px" HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}">
                                        <ItemStyle Width="80px" HorizontalAlign="Center"/>
                                    </asp:BoundField>    
                                    <asp:BoundField DataField="OrdenCompra" HeaderText="Orden">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:TemplateField   ItemStyle-CssClass="tableOptions"  ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnVerPDF" runat="server" CausesValidation="false" CommandName="VerPDF" CommandArgument="<%# Container.DataItemIndex %>" 
                                                ImageUrl="~/Images/btnGridPDF.png" OnCommand="btnVerPDF_Command" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField   ItemStyle-CssClass="tableOptions"  ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnAgregar" runat="server" CausesValidation="false" CommandName="Agregar" CommandArgument="<%# Container.DataItemIndex %>" 
                                                ImageUrl="~/Images/btnGridNuevo.png" OnCommand="btnAgregar_Command" Text="Nuevo" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField   ItemStyle-CssClass="tableOptions"  ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEliminar" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument="<%# Container.DataItemIndex %>" 
                                                ImageUrl="~/Images/btnGridEliminar.png" OnCommand="btnEliminar_Command" Text="Eliminar" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                </Columns>
                            </asp:GridView>                                    
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="2">
                            <asp:ImageButton ID="btnAceptar" runat="server" 
                                ImageUrl="~/Images/btnSubir.png" CommandName="Aceptar" onclick="btnAceptarSubir_Click"/>&nbsp;
                            <asp:ImageButton ID="btnCancelar" runat="server"  
                                ImageUrl="~/Images/btnCancelar.png" CommandName="Cancelar" onclick="btnCancelarSubir_Click" />
                        </td>
                    </tr>
                    
                </table>
            </div>
        </td>
    </tr>
    </table>

</asp:Content>
