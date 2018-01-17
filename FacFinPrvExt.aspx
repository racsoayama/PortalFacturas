<%@ Page Title="" Language="C#" MasterPageFile="~/PortalFacturas.Master" AutoEventWireup="true" CodeBehind="FacFinPrvExt.aspx.cs" Inherits="PortalFacturas.FacFinPrvExt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <table>
    <tr>
        <td>
            <asp:label ID="lblTitulo" runat="server" CssClass="h2">Registrar facturas</asp:label>
        </td>
    </tr>
    <tr>
        <td valign="top">
            <table>
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
                                                <td><asp:Label ID="lblSociedad" runat="server" CssClass="label" >Sociedad:</asp:Label></td>
                                                <td><asp:DropDownList ID="cboSociedades" runat="server" 
                                                        CssClass="dropdownlist" ></asp:DropDownList></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table >
                                            <tr>
                                                <td colspan="2"><asp:label ID="lblLeyArchPDF" runat="server" CssClass="label" >Seleccione el archivo PDF de la factura a subir:</asp:label></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="height: 36px"><INPUT type=file id=File1 name=File1 runat="server" size="50" accept="image/*.pdf" /></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2"><asp:label ID="lblOtrosArchivos" runat="server" CssClass="h3">Otros archivos:</asp:label></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 36px"><asp:label ID="lblArchivo1" runat="server" CssClass="label" >Archivo 1:</asp:label></td>
                                                <td style="height: 36px"><INPUT type=file id=File3 name=File3 runat="server" size="37" accept="*.*" /></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 36px"><asp:label ID="lblArchivo2" runat="server" CssClass="label" >Archivo 2:</asp:label></td>
                                                <td style="height: 36px"><INPUT type=file id=File4 name=File3 runat="server" size="37" accept="*.*" /></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 36px"><asp:label ID="lblArchivo3" runat="server" CssClass="label" >Archivo 3:</asp:label></td>
                                                <td style="height: 36px"><INPUT type=file id=File5 name=File3 runat="server" size="37" accept="*.*" /></td>
                                            </tr>

                                        </table>
                                    </td>
                                </tr>
                                <tr>

                                    <td colspan="2"  align="right" style="height: 53px">
                                        <asp:Button ID="btnAceptarImportar" runat="server"  Value="Aceptar" OnClientClick="return ValidaNombreArchivo();" onclick="btnAceptarImportar_Click"/>&nbsp;


                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
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
                                                <td><asp:label ID="lblNumFactura" runat="server" CssClass="label">Factura:</asp:label></td>
                                                <td><asp:textBox ID="txtFactura" runat="server" CssClass="dropdownlist"  MaxLength="10" onKeyUp="validateAlpha(this.id);" ></asp:textBox></td>
                                            </tr>
                                            <tr>
                                                <td><asp:label ID="lblEmisor" runat="server" CssClass="label" >Emisor:</asp:label></td>
                                                <td><asp:textBox ID="txtEmisor" runat="server" CssClass="dropdownlist"  MaxLength="13" onKeyUp="validateAlpha(this.id);" ></asp:textBox></td>
                                            </tr>
                                            <tr>
                                                <td><asp:label ID="lblReceptor" runat="server" CssClass="label" >Receptor:</asp:label></td>
                                                <td><asp:textBox ID="txtReceptor" runat="server" CssClass="dropdownlist"  Enabled ="false" ></asp:textBox></td>
                                            </tr>
                                            <tr>
                                                <td><asp:label ID="lblFecha" runat="server" CssClass="label" >Fecha:</asp:label></td>
                                                <td><asp:textBox ID="txtFecha" runat="server" CssClass="dropdownlist"  MaxLength="10" ></asp:textBox></td>
                                            </tr>
                                            <tr>
                                                <td><asp:label ID="lblMoneda" runat="server" CssClass="label" >Moneda:</asp:label></td>
                                                <td><asp:DropDownList ID="cboMoneda" runat="server" 
                                                                    CssClass="dropdownlist"   ></asp:DropDownList></td>
                                            </tr>
                                            <tr>
                                                <td><asp:Label ID="lblImpSinIva" runat="server" CssClass="label" >Importe sin iva:</asp:Label></td>
                                                <td><asp:TextBox ID="txtImpSinIva" runat="server" CssClass="dropdownlist" MaxLength="10" onkeypress="return ValidaDecimales(event, this.id, 2);" onkeyup="sum();" ></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td><asp:Label ID="lblImpuestos" runat="server" CssClass="label" >Impuestos:</asp:Label></td>
                                                <td><asp:TextBox ID="txtImpuestos" runat="server" CssClass="dropdownlist" MaxLength="10"  onkeypress="return ValidaDecimales(event, this.id, 2);" onkeyup="sum();" ></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td><asp:Label ID="lblTotal" runat="server" CssClass="label" >Total:</asp:Label></td>
                                                <td><asp:TextBox ID="txtTotal" runat="server" CssClass="dropdownlist" MaxLength="10"   ></asp:TextBox></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div id="divItem">
                                            <table>
                                                <tr>
                                                    <td colspan="2"><asp:label ID="lblDetPos" runat="server" CssClass="h3">Detalle por posición:</asp:label></td>
                                                </tr>
                                                <tr>
                                                    <td><asp:Label ID="lblMaterial" runat="server" CssClass="label" >Material:</asp:Label></td>
                                                    <td><asp:TextBox ID="txtMaterial" runat="server" CssClass="dropdownlist" MaxLength="40"  ></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td><asp:Label ID="lblCantidad" runat="server" CssClass="label">Cantidad:</asp:Label></td>
                                                    <td><asp:TextBox ID="txtCantidad" runat="server" CssClass="dropdownlist" MaxLength="10"  onKeyUp="validateNumeros(this.id);"  ></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td><asp:Label ID="lblUnidad" runat="server" CssClass="label" >Unidad:</asp:Label></td>
                                                    <td><asp:DropDownList ID="cboUnidad" runat="server" 
                                                                    CssClass="dropdownlist"   ></asp:DropDownList></td>
                                                </tr>
                                                <tr>
                                                    <td><asp:Label ID="lblImporteMat" runat="server" CssClass="label" >Importe:</asp:Label></td>
                                                    <td><asp:TextBox ID="txtImporteMat" runat="server" CssClass="dropdownlist" MaxLength="10"  onkeypress="return ValidaDecimales(event, this.id, 2);"  ></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" align="right">
                                                        <asp:Button ID="btnAceptarItem" runat="server"  Value="Aceptar" 
                                                            OnClientClick="return ValidaDatosItem();" onclick="btnAceptarItem_Click"/>&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                      <asp:GridView ID="grdDetalle" runat="server" 
                                            CssClass="mGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                                            AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center" DataKeyNames="Descripcion"
                                           OnRowDataBound ="grdDetalle_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="descripcion" HeaderText="Descripcion">
                                                    <ItemStyle  HorizontalAlign="Left"/>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad">
                                                    <ItemStyle  HorizontalAlign="Center"/>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Unidad" HeaderText="Unidad">
                                                    <ItemStyle HorizontalAlign="Center"/>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="importe" HeaderText="Importe">
                                                    <ItemStyle  HorizontalAlign="Center"/>
                                                </asp:BoundField>
                                                <asp:TemplateField   ItemStyle-CssClass="tableOptions"  ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnEliminar" runat="server" CausesValidation="false" Value="Eliminar" CommandArgument="<%# Container.DataItemIndex %>" 
                                                           OnCommand="btnEliminarItem_Command" Text="Eliminar" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>                                    
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:Button ID="btnAceptarFactura" runat="server"  Value="Aceptar" OnClientClick="return ValidaDatosFact();" onclick="btnAceptarAgregar_Click"/>&nbsp;
                                        <asp:Button ID="btnCancelarAgregar" runat="server"  
                                            Value="Cancelar" 
                                            onclick="btnCancelarAgregar_Click"/>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </td>
        <td valign="top" >
            <div id="divVisor" runat="server">
                <table>
                    <tr>
                        <td>
                                 <Iframe id="oViewer" runat="server" style="height: 550px; width: 700px"></Iframe> 
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <div id="divFacturas" runat="server">
                <table>
                    <tr>
                        <td colspan="2">
                           <asp:GridView ID="grdFacturas" runat="server" 
                                CssClass="mGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                                AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center" DataKeyNames="folioFact"
                                OnRowDataBound="grdFacturas_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="id_sociedad" HeaderText="Sociedad">
                                        <ItemStyle  HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="folioFact" HeaderText="Factura">
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
                                Value="Subir" onclick="btnAceptarSubir_Click"/>&nbsp;
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
