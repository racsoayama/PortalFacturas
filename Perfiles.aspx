<%@ Page Title="" Language="C#" MasterPageFile="~/PortalFacturas.Master" AutoEventWireup="true" CodeBehind="Perfiles.aspx.cs" Inherits="PortalFacturas.Perfiles" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPlaceHolder" runat="server">

    <script language="JavaScript" type="text/javascript">
        var modificado = false;

           function SelectAllCheckboxes(spanChk) {
               // Added as ASPX uses SPAN for checkbox
               var oItem = spanChk.children;
               var theBox = (spanChk.type == "checkbox") ?
                    spanChk : spanChk.children.item[0];
               xState = theBox.checked;
               elm = theBox.form.elements;

               for (i = 0; i < elm.length; i++)
                   if (elm[i].type == "checkbox" &&
                            elm[i].id != theBox.id) {
                   if (elm[i].checked != xState)
                       elm[i].click();
               }
           }

        function validateChange()
        {
            modificado = true;
        }
        
        function confirmExit()
        {
            if (modificado)
            {
                return confirm('¿Desea salir sin guardar los cambios?');
            }
            else
            {
                return true;
            }
        }

    </script> 

    <table>
    <tr><td colspan="2"><asp:label ID="lblPerfiles" runat="server" CssClass="h2">Perfiles</asp:label></td></tr>
    <tr>
        <td>
           <div id="dvDetallePerfil" runat="server">
            <table>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblConfPerfil" runat="server" CssClass="h3">Configuración del perfil:</asp:Label>
                    </td>                
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblNombre" runat="server" CssClass="label">Nombre:</asp:Label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="text" MaxLength="30" Width="200" onKeyUp="validateChange()"></asp:TextBox>
                    </td>                
                </tr>
                <tr>
                    <td valign="top">
                      <table>
                         <tr>
                            <td>
                                <asp:GridView ID="grdFunciones" runat="server" 
                                    CssClass="mGridMerge" PagerStyle-CssClass="pgr" 
                                    AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center"
                                    OnRowDataBound="grdFunciones_RowDataBound"
                                    onPrerender="grdFunciones_PreRender"
                                    DataKeyNames="Id_funcion"  >
                                    <Columns>
                                        <asp:BoundField DataField="Modulo" HeaderText="Modulo" >
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Descripcion" HeaderText="Opcion" >
                                            <ItemStyle Width="250px" />
                                        </asp:BoundField>
                                        <asp:TemplateField   ItemStyle-CssClass="tableOptions"  ShowHeader="True"  HeaderText="Consulta">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkConsulta" runat="server" Checked='<%#Convert.ToBoolean(Eval("consulta")) %>' Width="50"/>
                                            </ItemTemplate>
                                            <ItemStyle Width="50px" HorizontalAlign ="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField   ItemStyle-CssClass="tableOptions"  ShowHeader="False" HeaderText="Alta">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkAlta" runat="server" Checked='<%#Convert.ToBoolean(Eval("alta")) %>' Width="50"/>
                                            </ItemTemplate>
                                            <ItemStyle Width="50px" HorizontalAlign ="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField   ItemStyle-CssClass="tableOptions"  ShowHeader="False" HeaderText="Baja">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkBaja" runat="server" Checked='<%#Convert.ToBoolean(Eval("baja")) %>' Width="50"/>
                                            </ItemTemplate>
                                            <ItemStyle Width="50px" HorizontalAlign ="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField   ItemStyle-CssClass="tableOptions"  ShowHeader="False"  HeaderText="Editar">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkEdicion" runat="server" Checked='<%#Convert.ToBoolean(Eval("edicion")) %>' Width="50"/>
                                            </ItemTemplate>
                                            <ItemStyle Width="50px" HorizontalAlign ="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField   ItemStyle-CssClass="tableOptions"  ShowHeader="False"  HeaderText="Importar">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkImportar" runat="server" Checked='<%#Convert.ToBoolean(Eval("importar")) %>' Width="50"/>
                                            </ItemTemplate>
                                            <ItemStyle Width="50px" HorizontalAlign ="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField   ItemStyle-CssClass="tableOptions"  ShowHeader="False"  HeaderText="Imprimir">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkImprimir" runat="server" Checked='<%#Convert.ToBoolean(Eval("imprimir")) %>' Width="50"/>
                                            </ItemTemplate>
                                            <ItemStyle Width="50px" HorizontalAlign ="Center" />
                                        </asp:TemplateField>
                                        
                                    </Columns>
                                </asp:GridView>
                            </td>
                         </tr>
 
                      </table>
                    </td>
                    <td valign="top">
                      <table>
                      </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="right">
                        <asp:ImageButton ID="btnGuardar" runat="server" 
                            ImageUrl="~/Images/btnGuardar.png" CommandName="Guardar" 
                            OnClientClick="return ValidaDatos();" onclick="btnGuardar_Click"/>
                        <asp:ImageButton ID="btnCancelar" runat="server"  
                            ImageUrl="~/Images/btnCancelar.png" CommandName="Cancelar" 
                            OnClientClick="return confirmExit();" onclick="btnCancelar_Click"/>
                    </td>
                </tr>
            </table>
          </div>  
        </td>
    </tr>
    <tr>
        <td style="width: 396px" valign="top" align="left" >
          <div id="divPerfiles" runat="server">
            <asp:GridView ID="grdPerfiles" runat="server" 
                CssClass="mGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center" 
                OnRowDataBound="grdPerfiles_RowDataBound"
                DataKeyNames="Id_Perfil" >
                <Columns>
                    <asp:BoundField DataField="Id_Perfil" HeaderText="Id"  ShowHeader ="false"/>
                    <asp:BoundField DataField="Nombre" HeaderText="Perfil" >
                        <ItemStyle  />
                    </asp:BoundField>
                    <asp:TemplateField   ItemStyle-CssClass="tableOptions"  ShowHeader="False">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnVerDetalles" runat="server" CausesValidation="false" CommandName="Consultar" CommandArgument="<%# Container.DataItemIndex %>" 
                                ImageUrl="~/Images/btnGridVerDetalles.png" OnCommand="btnVerDetalles_Command" Text="Detalles" />
                        </ItemTemplate>
                    </asp:TemplateField>
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
                                ImageUrl="~/Images/btnGridEliminar.png" OnCommand="btnEliminaPerfil_Command" Text="Eliminar" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
          </div>  
        </td>
    </tr>
</table>
</asp:Content>
