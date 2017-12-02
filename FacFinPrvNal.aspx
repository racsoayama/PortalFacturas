<%@ Page Title="" Language="C#" MasterPageFile="~/PortalFacturas.Master" AutoEventWireup="true" CodeBehind="FacFinPrvNal.aspx.cs" Inherits="PortalFacturas.FacFinPrvNal" %>
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
                                                <td colspan="2"><INPUT type=file id=File1 name=File1 runat="server" size="50" accept="image/*.pdf" /></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2"><asp:label ID="lblLayArchXML" runat="server" CssClass="label" Width="330">Seleccione el archivo XML de la factura a subir:</asp:label></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2"><INPUT type=file id=File2 name=File1 runat="server" size="50" accept="image/*.xml" /></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2"><asp:label ID="lblOtrosArchivos" runat="server" CssClass="h3" Width="330">Otros archivos:</asp:label></td>
                                            </tr>
                                            <tr>
                                                <td><asp:label ID="lblArchivo1" runat="server" CssClass="label" Width="70">Archivo 1:</asp:label></td>
                                                <td><INPUT type=file id=File3 name=File3 runat="server" size="37" accept="*.*" /></td>
                                            </tr>
                                            <tr>
                                                <td><asp:label ID="lblArchivo2" runat="server" CssClass="label" Width="70">Archivo 2:</asp:label></td>
                                                <td><INPUT type=file id=File4 name=File3 runat="server" size="37" accept="*.*" /></td>
                                            </tr>
                                            <tr>
                                                <td><asp:label ID="lblArchivo3" runat="server" CssClass="label" Width="70">Archivo 3:</asp:label></td>
                                                <td><INPUT type=file id=File5 name=File3 runat="server" size="37" accept="*.*" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="right">
                                        <asp:ImageButton ID="btnAceptarImportar" runat="server"  ImageUrl="~/Images/btnAceptar.png" CommandName="Cancelar" OnClientClick="return ValidaNombreArchivo();" onclick="btnAceptarImportar_Click"/>&nbsp;
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
                                    <td><asp:label ID="lblUUID" runat="server" CssClass="label" Width="120">Certificado SAT:</asp:label></td>
                                    <td><asp:textbox ID="txtUUID" runat="server" CssClass="text" Width="250"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td><asp:label ID="lblNumFactura" runat="server" CssClass="label" Width="120">Factura:</asp:label></td>
                                    <td><asp:textBox ID="txtFactura" runat="server" CssClass="text" Width="90"></asp:textBox></td>
                                </tr>
                                <tr>
                                    <td><asp:label ID="lblEmisor" runat="server" CssClass="label" Width="120">Emisor:</asp:label></td>
                                    <td><asp:textBox ID="txtEmisor" runat="server" CssClass="text" Width="300px"></asp:textBox></td>
                                </tr>
                                <tr>
                                    <td><asp:label ID="lblReceptor" runat="server" CssClass="label" Width="120">Receptor:</asp:label></td>
                                    <td><asp:textBox ID="txtReceptor" runat="server" CssClass="text" Width="300"></asp:textBox></td>
                                </tr>
                                <tr>
                                    <td><asp:label ID="lblFecha" runat="server" CssClass="label" Width="120">Fecha:</asp:label></td>
                                    <td><asp:textBox ID="txtFecha" runat="server" CssClass="text" Width="150"></asp:textBox></td>
                                </tr>
                                <tr>
                                    <td><asp:label ID="lblImporte" runat="server" CssClass="label" Width="120">Importe:</asp:label></td>
                                    <td><asp:textBox ID="txtImporte" runat="server" CssClass="text" Width="90px"></asp:textBox></td>
                                </tr>
                                <tr>
                                    <td><asp:label ID="lblMoneda" runat="server" CssClass="label" Width="120">Moneda:</asp:label></td>
                                    <td><asp:DropDownList ID="cboMoneda" runat="server" 
                                                        CssClass="dropdownlist" Width ="100px"></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td><asp:label ID="lblValidacion" runat="server" CssClass="label" Width="120">Validación:</asp:label></td>
                                    <td><asp:textbox ID="txtValidacion" runat="server" CssClass="text" Width="300px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:ImageButton ID="btnAgregarFactura" runat="server"  ImageUrl="~/Images/btnAgregar.png" CommandName="Cancelar" 
                                            OnClientClick="return ValidaMoneda();"
                                            onclick="btnAceptarAgregar_Click"/>&nbsp;
                                        <asp:ImageButton ID="btnCancelarAgregar" runat="server"  
                                            ImageUrl="~/Images/btnCancelar.png" CommandName="Cancelar" 
                                            onclick="btnCancelarAgregar_Click"/>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </td>
        <td valign="top" style="width:650px;">
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
                                    <asp:BoundField DataField="Importe" HeaderText="Importe" DataFormatString="{0:N}">
                                        <ItemStyle Width="80px" HorizontalAlign="Right"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="id_moneda" HeaderText="Moneda" >
                                        <ItemStyle Width="50px" HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}">
                                        <ItemStyle Width="80px" HorizontalAlign="Center"/>
                                    </asp:BoundField>    
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnVerPDF" runat="server" CausesValidation="false" CommandName="VerPDF" CommandArgument="<%# Container.DataItemIndex %>" 
                                                ImageUrl="~/Images/btnGridPDF.png" OnCommand="btnVerPDF_Command" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnAgregar" runat="server" CausesValidation="false" CommandName="Agregar" CommandArgument="<%# Container.DataItemIndex %>" 
                                                ImageUrl="~/Images/btnGridNuevo.jpg" OnCommand="btnAgregar_Command" Text="Nuevo" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEliminar" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument="<%# Container.DataItemIndex %>" 
                                                ImageUrl="~/Images/btnGridEliminar.jpg" OnCommand="btnEliminar_Command" Text="Eliminar" />
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
                                ImageUrl="~/Images/btnCancelar.png" CommandName="Cancelar" onclick="btnCancelarSubir_Click"/>
                        </td>
                    </tr>
                    
                </table>
            </div>
        </td>
    </tr>
    </table>

</asp:Content>
