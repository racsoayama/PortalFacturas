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
                <table frame="border" rules="none" bordercolor="#4c206c" border="1" cellspacing="0" style="border-color: #C0C0C0">
                    <tr>
                        <td colspan="2" style="height: 39px"><asp:label ID="lblLeyIndicador" runat="server" CssClass="h4">Indicador</asp:label></td>
                    </tr>
                    <tr>
                        <td align="right" ><asp:Label ID="lblID" runat="server" class="label" >ID:</asp:Label></td>
                        <td style="height: 36px"><asp:TextBox ID="txtID" runat="server" CssClass="dropdownlist" MaxLength="2"  onKeyUp="validateAlpha(this.id);"  ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right" ><asp:Label ID="lblNombre" runat="server" CssClass="label" >Nombre:</asp:Label></td>
                        <td style="height: 36px"><asp:TextBox ID="txtNombre" runat="server" CssClass="dropdownlist" MaxLength="50"   ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right" ><asp:Label ID="lblTasa" runat="server" CssClass="label" >Tasa:</asp:Label></td>
                        <td style="height: 36px"><asp:TextBox ID="txtTasa" runat="server" CssClass="dropdownlist" MaxLength="3"  onKeyUp="validateNumeros(this.id);"  ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" style="height: 29px">
                            <asp:Button ID="btnGuardar" runat="server" 
                                btnGenerar="Guardar" 
                                OnClientClick="return ValidaDatos();"  onclick="btnGuardar_Click" /> 
                            <asp:Button ID="btnCancelar" runat="server"  
                                value="Cancelar" 
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
                        <ItemStyle  HorizontalAlign="Center"/>
                    </asp:BoundField>    
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre">
                        <ItemStyle  />
                    </asp:BoundField>
                    <asp:BoundField DataField="Tasa" HeaderText="TasaImp">
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
                                ImageUrl="~/Images/btnGridEliminar.png" OnCommand="btnEliminar_Command" Text="Eliminar" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
      </tr>
    </table>

</asp:Content>
