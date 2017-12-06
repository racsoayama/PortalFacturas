<%@ Page Title="" Language="C#" MasterPageFile="~/PortalFacturas.Master" AutoEventWireup="true" CodeBehind="Indicadores.aspx.cs" Inherits="PortalFacturas.Indicadores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainPlaceHolder" runat="server">
        <table>
    <tr>
        <td>
            <asp:label ID="lblTitulo" runat="server" CssClass="h2">Indicadores de impuestos</asp:label>
        </td>
    </tr>
      <tr>
        <td valign="top">
            <div id="divDetalle" runat="server" style="display: block">
                <table frame="border" rules="none" bordercolor="#4c206c" border="3" cellspacing="0">
                    <tr>
                        <td colspan="2"><asp:label ID="lblLeyIndicador" runat="server" CssClass="h3">Indicador</asp:label></td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="lblID" runat="server" class="label" Width="150">ID:</asp:Label></td>
                        <td><asp:TextBox ID="txtID" runat="server" CssClass="text" MaxLength="2" Width="40" onKeyUp="validateAlpha(this.id);"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="lblNombre" runat="server" CssClass="label" Width="150">Nombre:</asp:Label></td>
                        <td><asp:TextBox ID="txtNombre" runat="server" CssClass="text" MaxLength="50" Width="250" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="lblTasa" runat="server" CssClass="label" Width="150">Tasa:</asp:Label></td>
                        <td><asp:TextBox ID="txtTasa" runat="server" CssClass="text" MaxLength="3" Width="50" onKeyUp="validateNumeros(this.id);"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right" colspan="2">
                            <asp:ImageButton ID="btnGuardar" runat="server" 
                                ImageUrl="~/Images/btnGuardar.png" CommandName="Guardar" 
                                OnClientClick="return ValidaDatos();"  onclick="btnGuardar_Click" /> 
                            <asp:ImageButton ID="btnCancelar" runat="server"  
                                ImageUrl="~/Images/btnCancelar.png" CommandName="Cancelar" 
                                onclick="btnCancelar_Click"/>
                        </td>    
                    </tr>
                </table>
            </div>
        </td>
      </tr>
      <tr>
        <td valign="top">
            <asp:GridView ID="grdIndicadores" runat="server" 
                CssClass="mGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center"
                DataKeyNames="Id_indicador" 
                OnRowDataBound="grdIndicadores_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="Id_indicador" HeaderText="IndImpuesto">
                        <ItemStyle Width="80px" HorizontalAlign="Center"/>
                    </asp:BoundField>    
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre">
                        <ItemStyle Width="200px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Tasa" HeaderText="TasaImp">
                        <ItemStyle Width="80px" HorizontalAlign="Center"/>
                    </asp:BoundField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnAgregar" runat="server" CausesValidation="false" CommandName="Agregar" CommandArgument="<%# Container.DataItemIndex %>" 
                                ImageUrl="~/Images/btnGridNuevo.png" OnCommand="btnNuevo_Command" Text="Nuevo" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnEditar" runat="server" CausesValidation="false" CommandName="Editar" CommandArgument="<%# Container.DataItemIndex %>" 
                                ImageUrl="~/Images/btnGridEditar.png" OnCommand="btnEditar_Command" Text="Editar"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
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
