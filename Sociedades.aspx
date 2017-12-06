<%@ Page Title="" Language="C#" MasterPageFile="~/PortalFacturas.Master" AutoEventWireup="true" CodeBehind="Sociedades.aspx.cs" Inherits="PortalFacturas.Sociedades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <table>
    <tr>
        <td>
            <asp:label ID="lblLeyTitulo" runat="server" CssClass="h2">Sociedades</asp:label>
        </td>
    </tr>
      <tr>
        <td valign="top">
            <div id="divDetalle" runat="server" style="display: block">
                <table frame="border" rules="none" bordercolor="#4c206c" border="3" cellspacing="0">
                    <tr>
                        <td colspan="2"><asp:label ID="lblLeySociedad" runat="server" CssClass="h3">Sociedad</asp:label></td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="lblID" runat="server" CssClass="label" Width="170">Sociedad ID:</asp:Label></td>
                        <td><asp:TextBox ID="txtID" runat="server" CssClass="text" MaxLength="4" Width="50" onKeyUp="changeToUpperCase(this.id);"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="lblNombre" runat="server" CssClass="label" Width="170">Nombre:</asp:Label></td>
                        <td><asp:TextBox ID="txtNombre" runat="server" CssClass="text" MaxLength="30" Width="250" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="lblRFC" runat="server" class="label" Width="170">RFC:</asp:Label></td>
                        <td><asp:TextBox ID="txtRFC" runat="server" class="text" Width="110" MaxLength="13" onKeyUp="validateAlpha(this.id);"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="lblBusinessArea" runat="server" class="label" Width="170">BusinessArea:</asp:Label></td>
                        <td><asp:TextBox ID="txtBusArea" runat="server" class="text" Width="80" MaxLength="4" onKeyUp="validateNumeros(this.id);"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="lblCntrlArea" runat="server" class="label" Width="170">Controlling area:</asp:Label></td>
                        <td><asp:TextBox ID="txtContArea" runat="server" class="text" Width="50" MaxLength="4" onKeyUp="validateAlpha(this.id);"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="lblGralLeyerAcc" runat="server" class="label" Width="170">General Leyer Account:</asp:Label></td>
                        <td><asp:TextBox ID="txtGralLeyerAcc" runat="server" class="text" Width="80" MaxLength="10" onKeyUp="validateNumeros(this.id);"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="lblCostCenter" runat="server" class="label" Width="170">Cost Center:</asp:Label></td>
                        <td><asp:TextBox ID="txtCostCenter" runat="server" class="text" Width="80" MaxLength="10" onKeyUp="validateNumeros(this.id);"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="lblCtaProv" runat="server" CssClass="label" Width="170">Cuenta Proveedor:</asp:Label></td>
                        <td><asp:TextBox ID="txtCtaProv" runat="server" CssClass="text" MaxLength="10" Width="80" onKeyUp="validateNumeros(this.id);"></asp:TextBox></td>
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
            <asp:GridView ID="grdSociedades" runat="server" 
                CssClass="mGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center"
                DataKeyNames="Id_Sociedad" 
                OnRowDataBound="grdSociedades_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="Id_Sociedad" HeaderText="Sociedad">
                        <ItemStyle Width="50px" HorizontalAlign="Center"/>
                    </asp:BoundField>    
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre">
                        <ItemStyle Width="200px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="RFC" HeaderText="RFC">
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="BusinessArea" HeaderText="BusinessArea">
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ControllingArea" HeaderText="ControllingArea">
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="GeneralLeyerAcc" HeaderText="GeneralLeyerAccount">
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CostCenter" HeaderText="CostCenter">
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CtaProv" HeaderText="CtaProv">
                        <ItemStyle Width="110px" HorizontalAlign="center" />
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
