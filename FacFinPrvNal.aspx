<%@ Page Title="" Language="C#" MasterPageFile="~/PortalFacturas.Master" AutoEventWireup="true" CodeBehind="FacFinPrvNal.aspx.cs" Inherits="PortalFacturas.FacFinPrvNal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainPlaceHolder" runat="server">

    <table>
    <tr>
        <td>
            <asp:label ID="lblTitulo" runat="server" CssClass="h2">Registrar facturas</asp:label>
        </td>
    </tr>
    <tr>
        <td valign="top" style="width:650px; ">
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
                                        <table>
                                            <tr>
                                                <td colspan="2"><asp:label ID="lblLeyArchPDF" runat="server" CssClass="label">Seleccione el archivo PDF de la factura a subir:</asp:label></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="height: 36px"><INPUT type=file id=File1 name=File1 runat="server" size="50" accept="image/*.pdf" /></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2"><asp:label ID="lblLayArchXML" runat="server" CssClass="label" >Seleccione el archivo XML de la factura a subir:</asp:label></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="height: 36px"><INPUT type=file id=File2 name=File1 runat="server" size="50" accept="image/*.xml" /></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2"><asp:label ID="lblOtrosArchivos" runat="server" CssClass="h3" >Otros archivos:</asp:label></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 36px"><asp:label ID="lblArchivo1" runat="server" CssClass="label">Archivo 1:</asp:label></td>
                                                <td style="height: 36px"><INPUT type=file id=File3 name=File3 runat="server" size="37" accept="*.*" /></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 36px"><asp:label ID="lblArchivo2" runat="server" CssClass="label" >Archivo 2:</asp:label></td>
                                                <td style="height: 36px"><INPUT type=file id=File4 name=File3 runat="server" size="37" accept="*.*" /></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 36px"><asp:label ID="lblArchivo3" runat="server" CssClass="label">Archivo 3:</asp:label></td>
                                                <td style="height: 36px"><INPUT type=file id=File5 name=File3 runat="server" size="37" accept="*.*" /></td>
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
                <tr>
                    <td>
                        <div id="divDetalle" runat="server">
                            <table>
                                <tr>
                                    <td colspan="2"><asp:label ID="lblDetFactura" runat="server" CssClass="h3">Detalle de la factura:</asp:label></td>
                                </tr>
                                <tr>
                                    <td><asp:label ID="lblUUID" runat="server" CssClass="label" >Certificado SAT:</asp:label></td>
                                    <td><asp:textbox ID="txtUUID" runat="server" CssClass="dropdownlist"  Height="16px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td><asp:label ID="lblNumFactura" runat="server" CssClass="label" >Factura:</asp:label></td>
                                    <td><asp:textBox ID="txtFactura" runat="server" CssClass="dropdownlist"  Height="16px"></asp:textBox></td>
                                </tr>
                                <tr>
                                    <td><asp:label ID="lblEmisor" runat="server" CssClass="label" >Emisor:</asp:label></td>
                                    <td><asp:textBox ID="txtEmisor" runat="server" CssClass="dropdownlist"  Height="16px"></asp:textBox></td>
                                </tr>
                                <tr>
                                    <td><asp:label ID="lblReceptor" runat="server" CssClass="label">Receptor:</asp:label></td>
                                    <td><asp:textBox ID="txtReceptor" runat="server" CssClass="dropdownlist"  Height="16px"></asp:textBox></td>
                                </tr>
                                <tr>
                                    <td><asp:label ID="lblFecha" runat="server" CssClass="label">Fecha:</asp:label></td>
                                    <td><asp:textBox ID="txtFecha" runat="server" CssClass="text" ></asp:textBox></td>
                                </tr>
                                <tr>
                                    <td><asp:label ID="lblImporte" runat="server" CssClass="label">Importe:</asp:label></td>
                                    <td><asp:textBox ID="txtImporte" runat="server" CssClass="dropdownlist"  Height="16px"></asp:textBox></td>
                                </tr>
                                <tr>
                                    <td><asp:label ID="lblMoneda" runat="server" CssClass="label" >Moneda:</asp:label></td>
                                    <td><asp:DropDownList ID="cboMoneda" runat="server" 
                                                        CssClass="dropdownlist"  Height="16px"></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td><asp:label ID="lblValidacion" runat="server" CssClass="label" >Validación:</asp:label></td>
                                    <td><asp:textbox ID="txtValidacion" runat="server" CssClass="dropdownlist"  Height="16px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:Button ID="btnAgregarFactura" runat="server"  Value="Agregar" 
                                            OnClientClick="return ValidaMoneda();"
                                            onclick="btnAceptarAgregar_Click"/>&nbsp;
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
                                        <ItemStyle  HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UUID" HeaderText="UUID">
                                        <ItemStyle  HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="folioFact" HeaderText="Factura">
                                        <ItemStyle  HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Importe" HeaderText="Importe" DataFormatString="{0:N}">
                                        <ItemStyle HorizontalAlign="Right"/>
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
                               Value="Cancelar" onclick="btnCancelarSubir_Click"/>
                        </td>
                    </tr>
                    
                </table>
            </div>
        </td>
    </tr>
    </table>

</asp:Content>
