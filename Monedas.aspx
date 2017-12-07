<%@ Page Language="C#" MasterPageFile="~/PortalFacturas.Master" AutoEventWireup="true" CodeBehind="Monedas.aspx.cs" Inherits="PortalFacturas.Monedas" Title="Monedas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <table>
    <tr>
        <td>
            <asp:label ID="lblTitulo" runat="server" CssClass="h2">Monedas</asp:label>
        </td>
    </tr>
      <tr>
        <td valign="top">
            <div id="divDetalle" runat="server" style="display: block">
                <table frame="border" rules="none" bordercolor="#4c206c" border="1" cellspacing="0" style="border-color: #C0C0C0">
                    <tr>
                        <td colspan="2" style="height: 39px"><asp:label ID="lblLeyMonedas" runat="server" CssClass="h4">Monedas</asp:label></td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 600px; height: 36px"><asp:Label ID="lblID" runat="server" class="label" Width="100">ID Moneda:</asp:Label></td>
                        <td style="height: 36px"><asp:TextBox ID="txtID" runat="server" CssClass="dropdownlist" MaxLength="20" Width="150px" Height="16px" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 600px; height: 35px"><asp:Label ID="lblMonSAP" runat="server" CssClass="label" Width="100">Moneda SAP:</asp:Label></td>
                        <td style="height: 35px"><asp:TextBox ID="txtMonSAP" runat="server" CssClass="dropdownlist" MaxLength="3" Width="150px" onKeyUp="changeToUpperCase(this.id);" Height="16px" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Button ID="btnGuardar" runat="server" 
                                Value="Guardar" 
                                OnClientClick="return ValidaDatos();"  onclick="btnGuardar_Click" /> 
                            <asp:Button ID="btnCancelar" runat="server" Value="Cancelar" 
                                onclick="btnCancelar_Click"/>
                        </td>    
                    </tr>
                </table>
            </div>
        </td>
      </tr>
      <tr>
        <td valign="top">
            <asp:GridView ID="grdMonedas" runat="server" 
                CssClass="mGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center"
                DataKeyNames="Id_moneda, monedaSAP" 
                OnRowDataBound="grdMonedas_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="Id_Moneda" HeaderText="Moneda">
                        <ItemStyle Width="150px" HorizontalAlign="Left"/>
                    </asp:BoundField>    
                    <asp:BoundField DataField="MonedaSAP" HeaderText="MonedaSAP">
                        <ItemStyle Width="100px" HorizontalAlign="Center"/>
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
