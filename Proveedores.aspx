<%@ Page Title="" Language="C#" MasterPageFile="~/PortalFacturas.Master" AutoEventWireup="true" CodeBehind="Proveedores.aspx.cs" Inherits="PortalFacturas.Proveedores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <table>
    <tr>
        <td>
            <asp:label ID="lblTitProveedores" runat="server" CssClass="h2">Proveedores</asp:label>
        </td>
    </tr>
    <tr>
        <td >
          <div id="divFiltros" runat="server">
            <table class="filtersTable">
                <tr>
                    <td><asp:Label ID="lblFilProv" runat="server" CssClass="h3" Width="110"># de proveedor:</asp:Label></td>
                    <td><asp:Label ID="lblFilRFC" runat="server" CssClass="h3" Width="110">RFC:</asp:Label></td>
                    <td><asp:Label ID="lblFilNombre" runat="server" CssClass="h3" Width="200">Nombre o razón social:</asp:Label></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="txtFilProv" runat="server" CssClass="text" Width="110" MaxLength="10" onKeyUp="validateNumeros(this.id);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtFilRFC" runat="server" CssClass="text" Width="110" MaxLength="14" onKeyUp="changeToUpperCase(this.id);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtFilNombre" runat="server" CssClass="text" Width="200" MaxLength="40"></asp:TextBox></td>
                    <td><asp:Button ID="btnBuscar" runat="server" Valuee="Buscar" 
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
                        <td><asp:label ID="lblLeyArchivo" runat="server" CssClass="h3">Seleccione el archivo con los datos a cargar:</asp:label></td>
                    </tr>
                    <tr>
                        <td><INPUT type=file id=File1 name=File1 runat="server" size="50" class="file"/></td>
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
        <td valign="top" colspan="2">
            <div id="divDetalle" runat="server" style="display: block">
              <table frame="border" rules="none" bordercolor="#4c206c" border="1" cellspacing="0" style="border-color: #C0C0C0">
               <tr>
                 <td>
                 <table>
                    <tr>
                        <td colspan="2" style="height: 40px"><asp:label ID="lblLeyProveedor" runat="server" CssClass="h4">Proveedor</asp:label></td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 523px"><asp:Label ID="lblNumProv" runat="server" CssClass="label" Width="110"># Proveedor:</asp:Label></td>
                        <td><asp:TextBox ID="txtNumProv" runat="server" CssClass="dropdownlist" MaxLength="10" 
                                Width="150px" onKeyUp="validateNumeros(this.id);" Height="16px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 523px"><asp:Label ID="lblRFC" runat="server" CssClass="label" Width="110">RFC:</asp:Label></td>
                        <td><asp:TextBox ID="txtRFC" runat="server" CssClass="dropdownlist" MaxLength="13" 
                                Width="150px" Height="16px" onKeyUp="validateAlpha(this.id);"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 523px"><asp:Label ID="lblNombre" runat="server" CssClass="label" Width="110">Nombre:</asp:Label></td>
                        <td><asp:TextBox ID="txtNombre" runat="server" CssClass="dropdownlist" MaxLength="70" Width="150px" Height="16px" ></asp:TextBox></td>
                    </tr>
<%--                    <tr>
                        <td><asp:Label ID="lblCuenta" runat="server" CssClass="label" Width="110">Cuenta:</asp:Label></td>
                        <td><asp:TextBox ID="txtCuenta" runat="server" CssClass="text" MaxLength="10" Width="100" onKeyUp="validateNumeros(this.id);"></asp:TextBox></td>
                    </tr>--%>
                    <tr>
                        <td align="right" style="width: 523px"><asp:Label ID="lblCorreo" runat="server" CssClass="label" Width="110">Correo:</asp:Label></td>
                        <td><asp:TextBox ID="txtCorreo" runat="server" CssClass="dropdownlist" MaxLength="40" Width="150px" Height="16px"></asp:TextBox></asp:DropDownList></td>
                    </tr>                    
                    <tr>
                        <td align="right" style="width: 523px; height: 36px"><asp:Label ID="lblIntermediario" runat="server" CssClass="label" Width="134px" style="margin-left: 0px">Es intermediario?:</asp:Label></td>
                        <td style="height: 36px"><asp:RadioButtonList ID="rbtIntermediario" runat="server" CssClass="checklist" RepeatColumns="2" RepeatDirection="Horizontal" BorderStyle="None" Height="16px" Width="200px">
                                        </asp:RadioButtonList></td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 523px; height: 35px"><asp:Label ID="lblStatus" runat="server" CssClass="label" Width="110">Status:</asp:Label></td>
                        <td style="height: 35px"><asp:DropDownList ID="cboStatus" runat="server" CssClass="dropdownlist" Width ="150px" Height="16px"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td valign="top" align="right" style="width: 523px"><asp:Label ID="lblSociedades" runat="server" CssClass="label" Width="110">Sociedades:</asp:Label></td>
                        <td>
                         <asp:CheckBoxList ID="lstSociedades" runat="server" CssClass="checklist" 
                               Width="150px" RepeatColumns="1" BorderStyle="None" Height="113px">
                            <asp:ListItem Text="Soc 1" Value="Soc1"></asp:ListItem>
                            <asp:ListItem Text="Soc 2" Value="Soc2"></asp:ListItem>
                            <asp:ListItem Text="Soc 3" Value="Soc3"></asp:ListItem>
                            <asp:ListItem Text="Soc 4" Value="Soc4"></asp:ListItem>
                            <asp:ListItem Text="Soc 5" Value="Soc5"></asp:ListItem>
                         </asp:CheckBoxList>
                        </td>
                    </tr>
                    
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Button ID="btnGuardar" runat="server" 
                                Value="Guardar" 
                                OnClientClick="return ValidaDatos();"  onclick="btnGuardar_Click" /> 
                            <asp:Button ID="btnCancelar" runat="server"  
                                Value="Cancelar" 
                                onclick="btnCancelar_Click"/>
                        </td>    
                    </tr>
                </table>
                </td>
               </tr>
              </table>
            </div>
        </td>
      </tr>
        <tr>
            <td valign="top"  colspan="2">
                <asp:GridView ID="grdProveedores" runat="server" 
                    CssClass="mGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                    AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center" DataKeyNames="id_proveedor"
                    AllowPaging="true" PageSize="20" onpageindexchanging="grdProveedores_PageIndexChanging1"
                    OnRowDataBound="grdProveedores_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="id_proveedor" HeaderText="NumProveedor">
                            <ItemStyle Width="100px" />
                        </asp:BoundField>    
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre">
                            <ItemStyle Width="300px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="rfc" HeaderText="RFC">
                            <ItemStyle Width="100px" />
                        </asp:BoundField>                            
<%--                        <asp:BoundField DataField="cuenta" HeaderText="Cuenta">
                            <ItemStyle Width="110px" HorizontalAlign="center" />
                        </asp:BoundField>--%>
                        <asp:BoundField DataField="email" HeaderText="Email">
                            <ItemStyle Width="200px" />
                        </asp:BoundField>                            
                        <asp:BoundField DataField="Intermediario" HeaderText="Intermediario"  >
                            <ItemStyle Width="80px" HorizontalAlign="Center"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="Status" HeaderText="Status" >
                            <ItemStyle Width="70px" HorizontalAlign="Center"/>
                        </asp:BoundField>
                        <asp:TemplateField   ItemStyle-CssClass="tableOptions"  ShowHeader="False"  >
                            <ItemTemplate>
                                <asp:ImageButton ID="btnVerDetalles" runat="server" CausesValidation="false" CommandName="Consultar" CommandArgument="<%# Container.DataItemIndex %>" 
                                    ImageUrl="~/Images/btnGridVerDetalles.png" OnCommand="btnEditar_Command" Text="Detalles"  />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField   ItemStyle-CssClass="tableOptions"  ShowHeader="False" >
                            <ItemTemplate>
                                <asp:ImageButton ID="btnAgregar" runat="server" CausesValidation="false" CommandName="Agregar" CommandArgument="<%# Container.DataItemIndex %>" 
                                    ImageUrl="~/Images/btnGridNuevo.png" OnCommand="btnNuevo_Command" Text="Nuevo" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField   ItemStyle-CssClass="tableOptions"  ShowHeader="False"  >
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CausesValidation="false" CommandName="Editar" CommandArgument="<%# Container.DataItemIndex %>" 
                                    ImageUrl="~/Images/btnGridEditar.png" OnCommand="btnEditar_Command" Text="Editar"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField   ItemStyle-CssClass="tableOptions"  ShowHeader="False"  >
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEliminar" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument="<%# Container.DataItemIndex %>" 
                                    ImageUrl="~/Images/btnGridEliminar.png" OnCommand="btnEliminar_Command" Text="Eliminar" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
         </tr>
    </table>

</asp:Content>
