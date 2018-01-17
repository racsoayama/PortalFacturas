<%@ Page Title="" Language="C#" MasterPageFile="~/PortalFacturas.Master" AutoEventWireup="true" CodeBehind="Unidades.aspx.cs" Inherits="PortalFacturas.Unidades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainPlaceHolder" runat="server">

  <table>
    <tr>
        <td>
            <asp:label ID="lblTitulo" runat="server" CssClass="h2">Unidades de medida</asp:label>
        </td>
    </tr>
      <tr>
        <td valign="top">
            <div id="divDetalle" runat="server" style="display: block">
                <table frame="border" rules="none" bordercolor="#4c206c" border="1" cellspacing="0" style="border-color: #C0C0C0">
                    <tr>
                        <td colspan="2" style="height: 40px"><asp:label ID="lblLeyUnidades" runat="server" CssClass="h4">Uniddes</asp:label></td>
                    </tr>
                    <tr>
                        <td align="right"><asp:Label ID="lblID" runat="server" class="label" >ID Unidad:</asp:Label></td>
                        <td><asp:TextBox ID="txtID" runat="server" CssClass="dropdownlist" MaxLength="20"   ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right" ><asp:Label ID="lblUnidadSAP" runat="server" CssClass="label"  style="margin-left: 0px">Unidad SAP:</asp:Label></td>
                        <td><asp:TextBox ID="txtUnidadSAP" runat="server" CssClass="dropdownlist" MaxLength="3"  onKeyUp="changeToUpperCase(this.id);"   ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right" colspan="2" style="height: 46px">
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
            <asp:GridView ID="grdUnidades" runat="server" 
                CssClass="mGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center"
                DataKeyNames="Id_unidad, unidadSAP" 
                OnRowDataBound="grdUnidades_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="Id_unidad" HeaderText="Unidad">
                        <ItemStyle  HorizontalAlign="Left"/>
                    </asp:BoundField>    
                    <asp:BoundField DataField="UnidadSAP" HeaderText="UnidadSAP">
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
