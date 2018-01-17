
<%@ Page Language="C#" MasterPageFile="~/PortalFacturas.Master" AutoEventWireup="true" CodeBehind="FacLogPrvNal.aspx.cs" Inherits="PortalFacturas.FacLogPrvNal" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainPlaceHolder" runat="server">

    <table>
    <tr>
        <td style="border-color: 1pz sollid #C0C0C0">
            <asp:label ID="lblTitulo" runat="server" CssClass="h2">Registrar facturas</asp:label>
        </td>
    </tr>
    <tr>
        <td valign="top" style="width:650px; ">
            <table>
                <%--Datos iniciales para cargar la factura--%>
                <tr>
                    <td>
                        <div id="divImportar" runat="server" style="border-color: 1px solid #C0C0C0">
                            <table>
                                <tr>
                                    <td colspan="2"><asp:label ID="lblArchFacturas" runat="server" CssClass="h3">Archivos de la factura:</asp:label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td><asp:Label ID="lblSociedad" runat="server" CssClass="label" >Sociedad:</asp:Label></td>
                                                <td><asp:DropDownList ID="cboSociedades" runat="server" 
                                                        CssClass="dropdownlist" ></asp:DropDownList></td>
                                            </tr>
                                            <tr>
                                                <td><asp:Label ID="lblOrden" runat="server" class="label" >Orden de compra:</asp:Label></td>
                                                <td><asp:TextBox ID="txtOrden" runat="server" CssClass="dropdownlist" MaxLength="10" 
                                                         onKeyUp="validateNumeros(this.id);" Height="16px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td colspan="2"><asp:label ID="lblLeyArchPDF" runat="server" CssClass="label" >Seleccione el archivo PDF de la factura a subir:</asp:label></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="height: 54px" align="center"><INPUT type=file id=File1 name=File1 runat="server" size="50" accept="image/*.pdf" /></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2"><asp:label ID="lblLayArchXML" runat="server" CssClass="label" >Seleccione el archivo XML de la factura a subir:</asp:label></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="height: 60px" align="center"><INPUT type=file id=File2 name=File1 runat="server" size="50" accept="image/*.xml" /></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2"><asp:label ID="lblOtrosArchivos" runat="server" CssClass="h3" >Otros archivos:</asp:label></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 36px"><asp:label ID="lblArchivo1" runat="server" CssClass="label" >Archivo 1:</asp:label></td>
                                                <td style="height: 36px"><INPUT type=file id=File3 name=File1 runat="server" size="37" accept="*.*" /></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 36px"><asp:label ID="lblArchivo2" runat="server" CssClass="label" >Archivo 2:</asp:label></td>
                                                <td style="height: 36px"><INPUT type=file id=File4 name=File2 runat="server" size="37" accept="*.*" /></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 36px"><asp:label ID="lblArchivo3" runat="server" CssClass="label" >Archivo 3:</asp:label></td>
                                                <td style="height: 36px"><INPUT type=file id=File5 name=File4 runat="server" size="37" accept="*.*" /></td>
                                            </tr>
                                            </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="right">
                                        <asp:Button ID="btnAceptarImportar" runat="server"  Value="Aceptar" OnClientClick="return ValidaNombreArchivo();" onclick="btnAceptarImportar_Click"/>&nbsp;
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
                                                <td><asp:label ID="lblUUID" runat="server" CssClass="label" >Certificado SAT:</asp:label></td>
                                                <td><asp:textbox ID="txtUUID" runat="server" CssClass="dropdownlist"  MaxLength="36" Height="16px"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td><asp:label ID="lblNumFactura" runat="server" CssClass="label" >Factura:</asp:label></td>
                                                <td><asp:textBox ID="txtFactura" runat="server" CssClass="dropdownlist"  MaxLength="10" Height="16px"></asp:textBox></td>
                                            </tr>
                                            <tr>
                                                <td><asp:label ID="lblEmisor" runat="server" CssClass="label" >Emisor:</asp:label></td>
                                                <td><asp:textBox ID="txtEmisor" runat="server" CssClass="dropdownlist"  MaxLength="13" Height="16px"></asp:textBox></td>
                                            </tr>
                                            <tr>
                                                <td><asp:label ID="lblReceptor" runat="server" CssClass="label" >Receptor:</asp:label></td>
                                                <td><asp:textBox ID="txtReceptor" runat="server" CssClass="dropdownlist"  Height="16px"></asp:textBox></td>
                                            </tr>
                                            <tr>
                                                <td><asp:label ID="lblFecha" runat="server" CssClass="label" >Fecha:</asp:label></td>
                                                <td><asp:textBox ID="txtFecha" runat="server" CssClass="dropdownlist" MaxLength="10" Height="16px"></asp:textBox></td>
                                            </tr>
                                            <tr>
                                                <td><asp:label ID="lblImporte" runat="server" CssClass="label" >Importe:</asp:label></td>
                                                <td><asp:textBox ID="txtImporte" runat="server" CssClass="dropdownlist"  Height="16px"></asp:textBox></td>
                                            </tr>
                                            <tr>
                                                <td><asp:label ID="lblMoneda" runat="server" CssClass="label" >Moneda:</asp:label></td>
                                                <td><asp:DropDownList ID="cboMoneda" runat="server" 
                                                        CssClass="dropdownlist" Height="16px"></asp:DropDownList></td>
                                            </tr>
                                            <tr>
                                                <td><asp:label ID="lblValidacion" runat="server" CssClass="label" >Validación:</asp:label></td>
                                                <td><asp:textBox ID="txtValidacion" runat="server" CssClass="dropdownlist"  Height="16px"></asp:textBox></td>
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
                                                    <ItemStyle  HorizontalAlign="Left"/>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad">
                                                    <ItemStyle  HorizontalAlign="Center"/>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Unidad" HeaderText="Unidad">
                                                    <ItemStyle  HorizontalAlign="Center"/>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="importe" HeaderText="Importe" DataFormatString="{0:N}">
                                                    <ItemStyle  HorizontalAlign="Right"/>
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>                                    
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:Button ID="btnAceptarFactura" runat="server" 
                                            Value="Aceptar" OnClientClick="return ValidaMoneda();" onclick="btnAceptarFactura_Click"/>&nbsp;
                                        <asp:Button ID="btnCancelarAgregar" runat="server"  
                                            IValue="Cancelar" 
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
                                    <ItemStyle  HorizontalAlign="Left"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}">
                                    <ItemStyle  HorizontalAlign="Left"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="descripcion" HeaderText="Descripcion">
                                    <ItemStyle  HorizontalAlign="Left"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad">
                                    <ItemStyle  HorizontalAlign="Center"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="Unidad" HeaderText="Unidad">
                                    <ItemStyle  HorizontalAlign="Center"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="importe" HeaderText="Importe" DataFormatString="{0:N}">
                                    <ItemStyle HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="Impuestos" HeaderText="Iva" DataFormatString="{0:N}">
                                    <ItemStyle  HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="ImpNeto" HeaderText="Total" DataFormatString="{0:N}">
                                    <ItemStyle  HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="Moneda" HeaderText="Moneda">
                                    <ItemStyle  HorizontalAlign="Center"/>
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
                                            <asp:CheckBox ID="chkSeleccion" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle  HorizontalAlign ="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="id_pedido" HeaderText="Pedido">
                                        <ItemStyle  HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Id_pos_ped" HeaderText="PosPedido">
                                        <ItemStyle  HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Id_entrega" HeaderText="Entrega">
                                        <ItemStyle  HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Id_posicion" HeaderText="PosEntrega">
                                        <ItemStyle  HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Cantidad" HeaderText="Cantidad">
                                        <ItemStyle  HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Importe" HeaderText="Importe" DataFormatString="{0:N}">
                                        <ItemStyle  HorizontalAlign="Right"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Id_material" HeaderText="Material">
                                        <ItemStyle  HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Descripcion" HeaderText="Descripcion">
                                        <ItemStyle  HorizontalAlign="Left"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Nota_entrega" HeaderText="Referencia">
                                        <ItemStyle  HorizontalAlign="Left"/>
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
                                            <asp:CheckBox ID="chkSeleccion" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle  HorizontalAlign ="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="id_pedido" HeaderText="Pedido">
                                        <ItemStyle  HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Id_pos_ped" HeaderText="PosPedido">
                                        <ItemStyle  HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Id_entrega" HeaderText="Entrega">
                                        <ItemStyle  HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Id_posicion" HeaderText="PosEntrega">
                                        <ItemStyle  HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Cantidad" HeaderText="Cantidad">
                                        <ItemStyle  HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Importe" HeaderText="Importe" DataFormatString="{0:N}">
                                        <ItemStyle  HorizontalAlign="Right"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="id_proveedor" HeaderText="Proveedor">
                                        <ItemStyle  HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Id_tipoCond" HeaderText="TipoCond">
                                        <ItemStyle  HorizontalAlign="Left"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ref_docto" HeaderText="Referencia">
                                        <ItemStyle  HorizontalAlign="Left"/>
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>                                    
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Button ID="btnAceptarEntregas" runat="server"  Value="Aceptar"  OnClientClick="return ConfirmarNotas();" onclick="btnAceptarEntregas_Click"/>
                            <asp:Button ID="btnCancelarEntregas" runat="server" Value="Cancelar" onclick="btnCancelarEntregas_Click"/>
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
                                            <asp:CheckBox ID="chkSeleccion" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle  HorizontalAlign ="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="id_documento" HeaderText="Documento">
                                        <ItemStyle  HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Id_posicion" HeaderText="Posicion">
                                        <ItemStyle  HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Cantidad" HeaderText="Cantidad">
                                        <ItemStyle  HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Importe" HeaderText="Importe" DataFormatString="{0:N}">
                                        <ItemStyle  HorizontalAlign="Right"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Moneda" HeaderText="Moneda">
                                        <ItemStyle  HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Ref_docto" HeaderText="Referencia">
                                        <ItemStyle  HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>                                    
                        </td>
                    </tr>
                    <tr>
                        <td align="right" >
                            <asp:Button ID="btnAceptarServ" runat="server"  Value="Aceptar" onclick="btnAceptarEntregas_Click"/>
                            <asp:Button ID="btnCancelar2" runat="server"  Value="Cancelar" onclick="btnCancelarEntregas_Click"/>
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
                                        <ItemStyle  HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UUID" HeaderText="UUID">
                                        <ItemStyle  HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="folioFact" HeaderText="Factura">
                                        <ItemStyle  HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="emisor" HeaderText="Emisor">
                                        <ItemStyle  HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Importe" HeaderText="Importe" DataFormatString="{0:N}">
                                        <ItemStyle  HorizontalAlign="Right"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="id_moneda" HeaderText="Moneda" >
                                        <ItemStyle  HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}">
                                        <ItemStyle  HorizontalAlign="Center"/>
                                    </asp:BoundField>    
                                    <asp:BoundField DataField="OrdenCompra" HeaderText="Orden">
                                        <ItemStyle  HorizontalAlign="Center"/>
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
                            <asp:Button ID="btnAceptar" runat="server" 
                            Value="Aceptar" onclick="btnAceptarSubir_Click"/>&nbsp;
                            <asp:Button ID="btnCancelar" runat="server"  
                               Value="Cancelar" onclick="btnCancelarSubir_Click" />
                        </td>
                    </tr>
                    
                </table>
            </div>
        </td>
    </tr>
    </table>

</asp:Content>
