<%@ Page Language="C#" MasterPageFile="~/PortalFacturas.Master" AutoEventWireup="true" CodeBehind="ConsultaFacturas.aspx.cs" Inherits="PortalFacturas.ConsultaFacturas" Title="Consultar facturas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <table>
    <tr>
        <td>
            <asp:label ID="lblTitConsFact" runat="server" CssClass="h2">Consultar facturas</asp:label>
        </td>
    </tr>
    <tr>
        <td>
            <div id="divFiltros" runat="server">
                <table>
                    <tr>
                        <td><asp:label ID="lblProveedor" runat="server" CssClass="label" ># Proveedor:</asp:label></td>
                        <td><asp:textbox ID="txtProveedor" runat="server" CssClass="text"  MaxLength="10" onKeyUp="validateNumeros(this.id);"></asp:TextBox></td>
                        <td><asp:label ID="lblEmisor" runat="server" CssClass="label" >Emisor:</asp:label></td>
                        <td><asp:textbox ID="txtEmisor" runat="server" CssClass="text"  MaxLength="13"></asp:TextBox></td>
                        <td><asp:label ID="lblNombre" runat="server" CssClass="label" >Nombre:</asp:label></td>
                        <td><asp:textbox ID="txtNombre" runat="server" CssClass="text"  MaxLength="40"></asp:TextBox></td>
                        <td><asp:label ID="lblFactura" runat="server" CssClass="label" ># Factura:</asp:label></td>
                        <td><asp:textbox ID="txtFactura" runat="server" CssClass="text"  MaxLength="10" onKeyUp="validateNumeros(this.id);"></asp:TextBox></td>
                     
                        <td><asp:label ID="lblSociedad" runat="server" CssClass="label" >Sociedad:</asp:label></td>
                        <td><asp:DropDownList ID="cboFilSociedad" runat="server" CssClass="dropdownlist"></asp:DropDownList></td>
                      </tr>
                    <tr>
                        <td><asp:label ID="lblOrden" runat="server" CssClass="label" >#ODC:</asp:label></td>
                        <td><asp:textbox ID="txtOrden" runat="server" CssClass="text"  MaxLength="10" onKeyUp="validateNumeros(this.id);"></asp:TextBox></td>
                        <td><asp:label ID="lblEntrega" runat="server" CssClass="label" >Entrega:</asp:label></td>
                        <td><asp:textbox ID="txtEntrega" runat="server" CssClass="text"  MaxLength="10" onKeyUp="validateNumeros(this.id);"></asp:TextBox></td>
                        <td><asp:label ID="lblFolInicial" runat="server" CssClass="label" >Folio inicial:</asp:label></td>
                        <td><asp:textbox ID="txtFolIni" runat="server" CssClass="dropdownlist"  MaxLength="8" onKeyUp="validateNumeros(this.id);"  ></asp:TextBox></td>
                        <td><asp:label ID="lblFolFinal" runat="server" CssClass="label" >Folio final:</asp:label></td>
                        <td><asp:textbox ID="txtFolFin" runat="server" CssClass="text"  MaxLength="8" onKeyUp="validateNumeros(this.id);"></asp:TextBox></td>
                       
                        <td><asp:label ID="lblFecFacIni" runat="server" CssClass="label" >Fecha factura inicial:</asp:label></td>
                        <td><asp:textbox ID="txtFecFacIni" runat="server" CssClass="text"  MaxLength="10"></asp:TextBox></td>                   
                       </tr>
                         <tr>
                        <td><asp:label ID="lblFecFacFin" runat="server" CssClass="label" >Fecha factura final:</asp:label></td>
                        <td><asp:textbox ID="txtFecFacFin" runat="server" CssClass="text"  MaxLength="10"></asp:TextBox></td>
                        <td><asp:label ID="lblFecRegIni" runat="server" CssClass="label" >Fecha registro inicial:</asp:label></td>
                        <td><asp:textbox ID="txtFecRegIni" runat="server" CssClass="text"  MaxLength="10"></asp:TextBox></td>
                        <td><asp:label ID="lblFecRegFin" runat="server" CssClass="label" >Fecha registro final:</asp:label></td>
                        <td><asp:textbox ID="txtFecRegFin" runat="server" CssClass="text"  MaxLength="10"></asp:TextBox></td>

                      
                         </tr>
                        <tr>
                        <td colspan="5" align="right"><asp:Button ID="btnBuscar" runat="server" Value="Buscar" 
                             onclick="btnBuscar_Click"/>
                            </td>
                                <td colspan="5" align="left">
                        <asp:Button ID="btnImportar" runat="server" Value="Importar" 
                                     onclick="btnImportar_Click"/></td>
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
                        <td><asp:label ID="lblLeyArchivo" runat="server" CssClass="h3">Seleccione el archivo con los datos a cargar:</asp:label></td>
                    </tr>
                    <tr>
                        <td><INPUT type=file id=File1 name=File1 runat="server" size="50" class="file"/></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Button ID="btnAceptarImportar" runat="server"  Value="Cancelar" OnClientClick="return validaNombreArchivo();" onclick="btnAceptarImportar_Click"/>&nbsp;
                            <asp:Button ID="btnCancelarImportar" runat="server"  Value="Cancelar" onclick="btnCancelarImportar_Click"/>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr>
        <td>
                <asp:GridView ID="grdFacturas" runat="server" 
                    CssClass="mGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                    AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center" DataKeyNames="folio, UUID"
                    AllowPaging="true" PageSize="27" onpageindexchanging="grdFacturas_PageIndexChanging1"
                    OnRowDataBound="grdFacturas_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="folio" HeaderText="Folio">
                            <ItemStyle  HorizontalAlign="Center"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="UUID" HeaderText="UUID">
                            <ItemStyle  HorizontalAlign="Center"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="folioFact" HeaderText="Factura">
                            <ItemStyle  HorizontalAlign="Center"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="Id_proveedor" HeaderText="Proveedor">
                            <ItemStyle  />
                        </asp:BoundField>    
                        <asp:BoundField DataField="Proveedor" HeaderText="Nombre">
                            <ItemStyle  />
                        </asp:BoundField>    
                        <asp:BoundField DataField="Emisor" HeaderText="Emisor">
                            <ItemStyle  />
                        </asp:BoundField>    
                        <asp:BoundField DataField="Receptor" HeaderText="Receptor">
                            <ItemStyle  />
                        </asp:BoundField>    
                        <asp:BoundField DataField="fecha" HeaderText="FecFactura" DataFormatString="{0:dd/MM/yyyy}">
                            <ItemStyle  HorizontalAlign="Center"/>
                        </asp:BoundField>    
                        <asp:BoundField DataField="ordenCompra" HeaderText="Orden">
                            <ItemStyle  HorizontalAlign="Center"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="Importe" HeaderText="Importe" DataFormatString="{0:N}">
                            <ItemStyle  HorizontalAlign="Right"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="id_moneda" HeaderText="Moneda" >
                            <ItemStyle  HorizontalAlign="Center"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="fecha_reg" HeaderText="FecRegistro" DataFormatString="{0:dd/MM/yyyy}">
                            <ItemStyle  HorizontalAlign="Center"/>
                        </asp:BoundField>    
                        <asp:BoundField DataField="status" HeaderText="Status">
                            <ItemStyle  HorizontalAlign="Center"/>
                        </asp:BoundField>    
                        <asp:BoundField DataField="fecha_prop" HeaderText="FechaPropPago"  DataFormatString="{0:dd/MM/yyyy}">
                            <ItemStyle  HorizontalAlign="Center"/>
                        </asp:BoundField>    
                        <asp:BoundField DataField="fecha_pago" HeaderText="FechaPago"  DataFormatString="{0:dd/MM/yyyy}">
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
                                <asp:ImageButton ID="btnVerXML" runat="server" CausesValidation="false" CommandName="VerXML" CommandArgument="<%# Container.DataItemIndex %>" 
                                    ImageUrl="~/Images/btnGridXML.png" OnCommand="btnVerXML_Command" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField   ItemStyle-CssClass="tableOptions"  ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDownload" runat="server" CausesValidation="false" CommandName="Download" CommandArgument="<%# Container.DataItemIndex %>" 
                                    ImageUrl="~/Images/btnGridDown.png" OnCommand="btnDownload_Command" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
        
        </td>
    </tr>
</table>
</asp:Content>
