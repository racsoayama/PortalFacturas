<%@ Page Title="" Language="C#" MasterPageFile="~/PortalFacturas.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs"  Inherits="PortalFacturas.Usuarios"%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPlaceHolder" runat="server">

    <table>
    <tr>
        <td>
            <asp:label ID="lblLeyendaUsuario" runat="server" CssClass="h2">Usuarios</asp:label>
        </td>
    </tr>
    <tr>
        <td >
          <div id="divFiltros" runat="server">
            <table>
                <tr>
                    <td><asp:Label ID="lblFilNumero" runat="server" CssClass="h3" >Usuario ID:</asp:Label></td>
                    <td><asp:Label ID="lblFilNombre" runat="server" CssClass="h3" >Nombre:</asp:Label></td>
                    <td><asp:Label ID="lblFilPerfil" runat="server" CssClass="h3" >Perfil:</asp:Label></td>
                    <td><asp:Label ID="lblFilStatus" runat="server" CssClass="h3" >Estatus:</asp:Label></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="txtFilNumero" runat="server" CssClass="text"  MaxLength="10" onKeyUp="changeToUpperCase(this.id);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtFilNombre" runat="server" CssClass="text"  MaxLength="40"></asp:TextBox></td>
                    <td><asp:DropDownList ID="cboFilPerfil" runat="server" CssClass="dropdownlist" ></asp:DropDownList></td>
                    <td><asp:DropDownList ID="cboFilStatus" runat="server" CssClass="dropdownlist" ></asp:DropDownList></td>
                    <td><asp:Button ID="btnBuscar" runat="server" value="Buscar" 
                         onclick="btnBuscar_Click"/></td>
                    <td><asp:Button ID="btnMostrarTodos" runat="server" value="Mostrar" 
                         onclick="btnMostrarTodos_Click"/>
                    </td>
                   
                </tr>
            </table>
          </div>
        </td>
    </tr>
     <tr>
        <td valign="top">
            <div id="divDetalle" runat="server" style="display: block">
                <table frame="border" rules="none" bordercolor=#4c206c border="1" cellspacing="0" style="border-color: #C0C0C0">
                    <tr>
                        <td colspan="2"><asp:label ID="lblLeyUsuario" runat="server" CssClass="h4">Usuario</asp:label></td>
                    </tr>
                    <tr>
                        <td align="right"><asp:Label ID="lblTipoUsuario" runat="server" CssClass="label" >Tipo de usuario:</asp:Label></td>
                        <td><asp:DropDownList ID="cboTipoUsuario" runat="server" CssClass="dropdownlist" 
                                 AutoPostBack="true" 
                                onselectedindexchanged="cboTipoUsuario_SelectedIndexChanged"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td align="right"><asp:Label ID="lblUsuario" runat="server" CssClass="label">Usuario ID:</asp:Label></td>
                        <td><asp:TextBox ID="txtUsuario" runat="server" CssClass="dropdownlist" MaxLength="10"  onKeyUp="changeToUpperCase(this.id);" Height="18px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right"><asp:Label ID="lblNombre" runat="server" CssClass="label" >Nombre:</asp:Label></td>
                        <td><asp:TextBox ID="txtNombre" runat="server" CssClass="dropdownlist" MaxLength="50"  Height="18px" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right"><asp:Label ID="lblProveedor" runat="server" CssClass="label" >Proveedor:</asp:Label></td>
                        <td><asp:TextBox ID="txtProveedor" runat="server" CssClass="dropdownlist" MaxLength="10"  onKeyUp="validateNumeros(this.id);" Height="18px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right" style="height: 36px"><asp:Label ID="lblCorreo" runat="server" CssClass="label" >Correo:</asp:Label></td>
                        <td style="height: 36px"><asp:TextBox ID="txtCorreo" runat="server" CssClass="dropdownlist" MaxLength="40"  ></asp:TextBox></td>
                    </tr>                    
                    <tr>
                        <td align="right" style="height: 36px"><asp:Label ID="lblPerfil" runat="server" CssClass="label" >Perfil:</asp:Label></td>
                        <td style="height: 36px"><asp:DropDownList ID="cboPerfiles" runat="server" CssClass="dropdownlist"  ></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td align="right" style="height: 36px"><asp:Label ID="lblStatus" runat="server" CssClass="label" >Estatus:</asp:Label></td>
                        <td style="height: 36px"><asp:DropDownList ID="cboStatus" runat="server" CssClass="dropdownlist"   ></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td align="right" colspan="2" style="height: 54px">
                            <asp:Button ID="btnGuardar" runat="server" 
                               Value="Guardar" 
                                OnClientClick="return ValidaDatos();"  onclick="btnGuardar_Click" /> 
                            <asp:Button ID="btnCancelar" runat="server"  
                                Value="Cancelar" 
                                onclick="btnCancelar_Click"/>
                        </td>    
                    </tr>
                </table>
            </div>
        </td>
      </tr>
    <tr>
        <td valign="top">
            <asp:GridView ID="grdUsuarios" runat="server" 
                CssClass="mGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center"
                OnRowDataBound="grdUsuarios_RowDataBound"
                AllowPaging="true" PageSize="50" onpageindexchanging="grdUsuarios_PageIndexChanging"
                DataKeyNames="Id_Usuario" >
                <Columns>
                    <asp:BoundField DataField="Id_usuario" HeaderText="Usuario">
                        <ItemStyle  />
                    </asp:BoundField>    
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre">
                        <ItemStyle  />
                    </asp:BoundField>
                    <asp:BoundField DataField="Nombre_Perfil" HeaderText="Perfil" >
                        <ItemStyle />
                    </asp:BoundField>
                    <asp:BoundField DataField="Status" HeaderText="Estatus"  >
                        <ItemStyle  HorizontalAlign="Center"/>
                    </asp:BoundField>
                    <asp:TemplateField   ItemStyle-CssClass="tableOptions"  ShowHeader="False">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnAgregar" runat="server" CausesValidation="false" CommandName="Agregar" CommandArgument="<%# Container.DataItemIndex %>" 
                                ImageUrl="~/Images/btnGridNuevo.png" OnCommand="btnNuevo_Command" Text="Nuevo" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField   ItemStyle-CssClass="tableOptions"  ShowHeader="False">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnEditar" runat="server" CausesValidation="false" CommandName="Editar" CommandArgument="<%# Container.DataItemIndex %>" 
                                ImageUrl="~/Images/btnGridEditar.png" OnCommand="btnEditar_Command" Text="Editar"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField   ItemStyle-CssClass="tableOptions"  ShowHeader="False">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnEliminar" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument="<%# Container.DataItemIndex %>" 
                                ImageUrl="~/Images/btnGridEliminar.png" OnCommand="btnEliminaUsuario_Command" Text="Eliminar" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </table>

</asp:Content>