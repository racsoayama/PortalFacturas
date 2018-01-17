﻿<%@ Page Title="" Language="C#" MasterPageFile="~/PortalFacturas.Master" AutoEventWireup="true" CodeBehind="Sociedades.aspx.cs" Inherits="PortalFacturas.Sociedades" %>
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
                <table frame="border" rules="none" bordercolor="#4c206c" border="1" cellspacing="0" style="border-color: #C0C0C0">
                    <tr>
                        <td colspan="2" style="height: 40px"><asp:label ID="lblLeySociedad" runat="server" CssClass="h4">Sociedad</asp:label></td>
                    </tr>
                    <tr>
                        <td align="right" style="height: 34px"><asp:Label ID="lblID" runat="server" CssClass="label" >Sociedad ID:</asp:Label></td>
                        <td style="height: 34px"><asp:TextBox ID="txtID" runat="server" CssClass="dropdownlist" MaxLength="4"  onKeyUp="changeToUpperCase(this.id);"  ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right" style="height: 35px"><asp:Label ID="lblNombre" runat="server" CssClass="label" >Nombre:</asp:Label></td>
                        <td style="height: 35px"><asp:TextBox ID="txtNombre" runat="server" CssClass="dropdownlist" MaxLength="30"    ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right"><asp:Label ID="lblRFC" runat="server" class="label" >RFC:</asp:Label></td>
                        <td><asp:TextBox ID="txtRFC" runat="server" class="text"  MaxLength="13" onKeyUp="validateAlpha(this.id);"  ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right"><asp:Label ID="lblBusinessArea" runat="server" class="label" >BusinessArea:</asp:Label></td>
                        <td><asp:TextBox ID="txtBusArea" runat="server" class="text"  MaxLength="4" onKeyUp="validateNumeros(this.id);"  ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right"><asp:Label ID="lblCntrlArea" runat="server" class="label" >Controlling area:</asp:Label></td>
                        <td><asp:TextBox ID="txtContArea" runat="server" class="text"  MaxLength="4" onKeyUp="validateAlpha(this.id);"  ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right" style="height: 34px"><asp:Label ID="lblGralLeyerAcc" runat="server" class="label" >General Leyer Account:</asp:Label></td>
                        <td style="height: 34px"><asp:TextBox ID="txtGralLeyerAcc" runat="server" class="text"  MaxLength="10" onKeyUp="validateNumeros(this.id);"  ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right"><asp:Label ID="lblCostCenter" runat="server" class="label" >Cost Center:</asp:Label></td>
                        <td><asp:TextBox ID="txtCostCenter" runat="server" class="text"  MaxLength="10" onKeyUp="validateNumeros(this.id);"  ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right"><asp:Label ID="lblCtaProv" runat="server" CssClass="label" >Cuenta Proveedor:</asp:Label></td>
                        <td><asp:TextBox ID="txtCtaProv" runat="server" CssClass="dropdownlist" MaxLength="10"  onKeyUp="validateNumeros(this.id);"  ></asp:TextBox></td>
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
                        <ItemStyle  HorizontalAlign="Center"/>
                    </asp:BoundField>    
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre">
                        <ItemStyle  />
                    </asp:BoundField>
                    <asp:BoundField DataField="RFC" HeaderText="RFC">
                        <ItemStyle  HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="BusinessArea" HeaderText="BusinessArea">
                        <ItemStyle  HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ControllingArea" HeaderText="ControllingArea">
                        <ItemStyle  HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="GeneralLeyerAcc" HeaderText="GeneralLeyerAccount">
                        <ItemStyle  HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CostCenter" HeaderText="CostCenter">
                        <ItemStyle  HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CtaProv" HeaderText="CtaProv">
                        <ItemStyle  HorizontalAlign="center" />
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
