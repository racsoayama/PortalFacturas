﻿<%@ Page Title="" Language="C#" MasterPageFile="~/PortalFacturas.Master" AutoEventWireup="true" CodeBehind="Unidades.aspx.cs" Inherits="PortalFacturas.Unidades" %>
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
                <table frame="border" rules="none" bordercolor="#4c206c" border="3" cellspacing="0">
                    <tr>
                        <td colspan="2"><asp:label ID="lblLeyUnidades" runat="server" CssClass="h3">Uniddes</asp:label></td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="lblID" runat="server" class="label" Width="100">ID Unidad:</asp:Label></td>
                        <td><asp:TextBox ID="txtID" runat="server" CssClass="text" MaxLength="20" Width="200" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="lblUnidadSAP" runat="server" CssClass="label" Width="100">Unidad SAP:</asp:Label></td>
                        <td><asp:TextBox ID="txtUnidadSAP" runat="server" CssClass="text" MaxLength="3" Width="40" onKeyUp="changeToUpperCase(this.id);" ></asp:TextBox></td>
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
            <asp:GridView ID="grdUnidades" runat="server" 
                CssClass="mGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center"
                DataKeyNames="Id_unidad, unidadSAP" 
                OnRowDataBound="grdUnidades_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="Id_unidad" HeaderText="Unidad">
                        <ItemStyle Width="150px" HorizontalAlign="Left"/>
                    </asp:BoundField>    
                    <asp:BoundField DataField="UnidadSAP" HeaderText="UnidadSAP">
                        <ItemStyle Width="100px" HorizontalAlign="Center"/>
                    </asp:BoundField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnAgregar" runat="server" CausesValidation="false" CommandName="Agregar" CommandArgument="<%# Container.DataItemIndex %>" 
                                ImageUrl="~/Images/btnGridNuevo.jpg" OnCommand="btnNuevo_Command" Text="Nuevo" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnEditar" runat="server" CausesValidation="false" CommandName="Editar" CommandArgument="<%# Container.DataItemIndex %>" 
                                ImageUrl="~/Images/btnGridEditar.jpg" OnCommand="btnEditar_Command" Text="Editar"/>
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
    </table>

</asp:Content>
