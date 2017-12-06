using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;
using NegocioPF;
using System.Data;

namespace PortalFacturas
{
    public partial class Proveedores : FormaPadre
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ValidaVariables();
                    EstableceIdioma((Idioma)Session["oIdioma"]);

                    NegocioPF.Proveedores oProveedores = new NegocioPF.Proveedores();
                    oProveedores.CargarOrdenado("id_proveedor");
                    grdProveedores.DataSource = oProveedores.Datos;
                    grdProveedores.DataBind();

                    //Status
                    Catalogo oCatalogo = new Catalogo();
                    oCatalogo.ID = "STATUS_PRO";
                    oCatalogo.Cargar(((Usuario)Session["oUsuario"]).Idioma);
                    cboStatus.DataSource = oCatalogo.Datos;
                    cboStatus.DataTextField = "descripcion";
                    cboStatus.DataValueField = "id_valor";
                    cboStatus.DataBind();
                    cboStatus.Items.Insert(0, new ListItem(((Idioma)Session["oIdioma"]).Texto("Seleccionar") + " ...", "0"));

                    NegocioPF.Sociedades oSociedades = new NegocioPF.Sociedades();
                    oSociedades.Cargar();
                    lstSociedades.DataSource = oSociedades.Datos;
                    lstSociedades.DataTextField = "nombre";
                    lstSociedades.DataValueField = "id_sociedad";
                    lstSociedades.DataBind();

                    rbtIntermediario.Items.Clear();
                    rbtIntermediario.Items.Add(new ListItem(((Idioma)Session["oIdioma"]).Texto("Si"), "1"));
                    rbtIntermediario.Items.Add(new ListItem(((Idioma)Session["oIdioma"]).Texto("No"), "2"));
                    rbtIntermediario.SelectedIndex = 1;

                    divImportar.Visible = false;

                    Perfil oPerfil = new Perfil();
                    Permisos permisos = oPerfil.CargarPermisos(((Usuario)Session["oUsuario"]).Id, "Proveedores.aspx");
                    grdProveedores.Columns[6].Visible = permisos.Consulta;
                    grdProveedores.Columns[7].Visible = permisos.Alta;
                    grdProveedores.Columns[8].Visible = permisos.Edicion;
                    grdProveedores.Columns[9].Visible = permisos.Baja;
                    btnImportar.Visible = permisos.Importar;

                    divDetalle.Visible = (oProveedores.Datos.Tables[0].Rows.Count == 0 && (permisos.Alta || permisos.Edicion));

                    divFiltros.Visible = true; // (oProveedores.Datos.Tables[0].Rows.Count > 0);

                }
                catch (Exception ex)
                {
                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message)); 
                }
            }

            AgregaScriptCliente();
        }

        protected void grdProveedores_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            ValidaVariables();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton btnConsultar = (ImageButton)e.Row.FindControl("btnVerDetalles");
                btnConsultar.ToolTip = ((Idioma)Session["oIdioma"]).Texto("Consultar");

                ImageButton btnAgregar = (ImageButton)e.Row.FindControl("btnAgregar");
                btnAgregar.ToolTip = ((Idioma)Session["oIdioma"]).Texto("Agregar");

                ImageButton btnEditar = (ImageButton)e.Row.FindControl("btnEditar");
                btnEditar.ToolTip = ((Idioma)Session["oIdioma"]).Texto("Editar");

                ImageButton btnEliminar = (ImageButton)e.Row.FindControl("btnEliminar");
                btnEliminar.ToolTip = ((Idioma)Session["oIdioma"]).Texto("Eliminar");
                btnEliminar.OnClientClick = "return confirm('" + ((Idioma)Session["oIdioma"]).Texto("ConfirmaBaja") + "')";

                if (DataBinder.Eval(e.Row.DataItem, "Intermediario").ToString() == "True")
                    e.Row.Cells[4].Text = ((Idioma)Session["oIdioma"]).Texto("Si");
                else
                    e.Row.Cells[4].Text = ((Idioma)Session["oIdioma"]).Texto("No");

            }
        }

        protected void grdProveedores_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            try
            {
                ValidaVariables();
                grdProveedores.PageIndex = e.NewPageIndex;

                NegocioPF.Proveedores oProveedores = new NegocioPF.Proveedores();
                oProveedores.Cargar(txtFilProv.Text, txtFilRFC.Text, txtFilNombre.Text); 
                grdProveedores.DataSource = oProveedores.Datos;
                grdProveedores.DataBind();
            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }

        protected void btnNuevo_Command(object sender, CommandEventArgs e)
        {
            try
            {
                txtNumProv.Text = "";
                txtNombre.Text = "";
                txtRFC.Text = "";
                txtCorreo.Text = "";
                txtCorreo.Text = "";
                rbtIntermediario.SelectedIndex = 0;

                foreach (ListItem i in lstSociedades.Items)
                {
                    i.Selected = false;
                }

                cboStatus.SelectedValue = "ACTIVO";
                txtNumProv.Enabled = true;
                btnGuardar.Visible = true;
                divFiltros.Visible = false;
                divDetalle.Visible = true;
            }
            catch(Exception ex)
            {
                /* */
            }
        }

        protected void btnEditar_Command(object sender, CommandEventArgs e)
        {
            try
            {
                ValidaVariables();

                //obtiene indice de la linea actualizar
                int index = Convert.ToInt32(e.CommandArgument) - (grdProveedores.PageIndex * grdProveedores.PageSize);

                //Carga los datos del usuario
                NegocioPF.Proveedor oProveedor = new NegocioPF.Proveedor(grdProveedores.DataKeys[index].Value.ToString());
                oProveedor.Cargar();

                //Muestra los datos en los controles
                txtNumProv.Text = oProveedor.Id;
                txtRFC.Text = oProveedor.RFC;
                txtNombre.Text = oProveedor.Nombre;
                //txtCuenta.Text = oProveedor.Cuenta;
                txtCorreo.Text = oProveedor.eMail;
                rbtIntermediario.SelectedIndex = (oProveedor.Intermediario ? 0 : 1);
                cboStatus.SelectedValue = oProveedor.Status;
                foreach (ListItem i in lstSociedades.Items)
                {
                    i.Selected = false;
                    foreach (string sociedad in oProveedor.Sociedades)
                    {
                        if (sociedad == i.Value)
                            i.Selected = true;
                    }
                }


                txtNumProv.Enabled = false;
                btnGuardar.Visible = (e.CommandName == "Editar" ? true : false);
                divFiltros.Visible = false;
                divDetalle.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }

        protected void btnEliminar_Command(object sender, CommandEventArgs e)
        {
            try
            {
                ValidaVariables();

                //Obtiene indice de la linea a actualizar
                int index = Convert.ToInt32(e.CommandArgument) - (grdProveedores.PageIndex * grdProveedores.PageSize);

                //Carga la información a eliminar
                NegocioPF.Proveedor oProveedor = new Proveedor(grdProveedores.DataKeys[index].Value.ToString());

                try
                {
                    oProveedor.Eliminar(((Usuario)Session["oUsuario"]).Id);
                }
                catch (Exception ex)
                {
                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
                }

                NegocioPF.Proveedores oProveedores = new NegocioPF.Proveedores();
                oProveedores.CargarOrdenado("id_proveedor");
                grdProveedores.DataSource = oProveedores.Datos;
                grdProveedores.DataBind();

                //MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgProveedorEliminado"));
                
            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                ValidaVariables();

                NegocioPF.Proveedor oProveedor = new NegocioPF.Proveedor(txtNumProv.Text,
                                                                      txtRFC.Text, 
                                                                      txtNombre.Text,
                                                                      "", //txtCuenta.Text,
                                                                      txtCorreo.Text,
                                                                      (rbtIntermediario.SelectedIndex == 0 ? true : false),
                                                                      cboStatus.SelectedValue);

                //Recupera las sociedades
                foreach (ListItem i in lstSociedades.Items)
                {
                    if (i.Selected)
                        oProveedor.Sociedades.Add(i.Value);
                }

                oProveedor.Guardar(((Usuario)Session["oUsuario"]).Id);


                NegocioPF.Proveedores oProveedores = new NegocioPF.Proveedores();
                oProveedores.Cargar(txtFilProv.Text, txtFilRFC.Text, txtFilNombre.Text);
                grdProveedores.DataSource = oProveedores.Datos;
                grdProveedores.DataBind();

                divFiltros.Visible = true;
                divDetalle.Visible = false;

                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgProveedorGuardado"));

            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message)); 
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            txtFilRFC.Text = "";
            txtNombre.Text = "";
            txtCorreo.Text = "";
            rbtIntermediario.SelectedIndex = 0;
            cboStatus.SelectedValue = "0";

            divFiltros.Visible = true;
            divDetalle.Visible = false;
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                ValidaVariables();
                //Carga los datos del producto
                NegocioPF.Proveedores oProveedores = new NegocioPF.Proveedores();
                oProveedores.Cargar(txtFilProv.Text, txtFilRFC.Text, txtFilNombre.Text);
                grdProveedores.DataSource = oProveedores.Datos;
                grdProveedores.DataBind();

            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message)); 
            }
        }

        protected void btnMostrarTodos_Click(object sender, EventArgs e)
        {
            try
            {
                ValidaVariables();

                txtFilProv.Text = "";
                txtFilRFC.Text = "";
                txtFilNombre.Text = "";

                NegocioPF.Proveedores oProveedores = new NegocioPF.Proveedores();
                oProveedores.CargarOrdenado("rfc");
                grdProveedores.DataSource = oProveedores.Datos;
                grdProveedores.DataBind();
            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message)); 
            }
        }


        protected void btnImportar_Click(object sender, EventArgs e)
        {
            divDetalle.Visible = false;
            divImportar.Visible = true;
        }

        protected void btnAceptarImportar_Click(object sender, EventArgs e)
        {
            int registros = 0;
            try
            {
                ValidaVariables();
                if ((File1.PostedFile != null) && (File1.PostedFile.ContentLength > 0))
                {
                    
                    string fn = System.IO.Path.GetFileName(File1.PostedFile.FileName);

                    string SaveLocation = Server.MapPath("") + "\\Data\\Proveedores." + fn.Substring(fn.Length - 3, 3);

                    try
                    {
                        File1.PostedFile.SaveAs(SaveLocation);
                    }
                    catch (Exception ex)
                    {
                        MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgErrorCopiarArchivo"));
                    }

                    try
                    {
                        NegocioPF.Proveedores oProveedores = new NegocioPF.Proveedores();

                        if (fn.Substring(fn.Length - 3, 3).ToLower() == "xls")
                        {
                            registros = oProveedores.ImportarExcel(((Usuario)Session["oUsuario"]).Id, SaveLocation);
                        }
                        else
                        {
                            registros = oProveedores.ImportarTXT(((Usuario)Session["oUsuario"]).Id, SaveLocation);
                        }

                        oProveedores.CargarOrdenado("id_proveedor");
                        grdProveedores.DataSource = oProveedores.Datos;
                        grdProveedores.DataBind();

                        divImportar.Visible = false;

                        MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgRegActInsertados") + registros.ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox(sender, e, "Error:" + ((Idioma)Session["oIdioma"]).Texto(ex.Message));
                    }
                }
                else
                {
                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgSeleccioneArchivo"));
                }
            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }

        }

        protected void btnCancelarImportar_Click(object sender, EventArgs e)
        {
            divImportar.Visible = false;
        }

        private void EstableceIdioma(Idioma oIdioma)
        {
            lblTitProveedores.Text = oIdioma.Texto("Proveedores");
            lblFilProv.Text = oIdioma.Texto("NumProveedor");
            lblFilRFC.Text = oIdioma.Texto("RFC") + ":";
            lblFilNombre.Text = oIdioma.Texto("NomRazSoc") + ":";
            lblLeyProveedor.Text = oIdioma.Texto("Proveedor");
            lblNumProv.Text = oIdioma.Texto("NumProveedor") + ":";
            lblRFC.Text = oIdioma.Texto("RFC")+ ":";
            lblNombre.Text = oIdioma.Texto("Nombre") + ":";
            lblCorreo.Text = oIdioma.Texto("Correo") + ":";
            //lblCuenta.Text = oIdioma.Texto("Cuenta") + ":";
            lblStatus.Text = oIdioma.Texto("Status") + ":";
            lblIntermediario.Text = oIdioma.Texto("EsIntermediario") + "?:";
            lblSociedades.Text = oIdioma.Texto("Sociedades") + ":";
            lblLeyArchivo.Text = oIdioma.Texto("MsgSelArchivo") + ":";

            foreach (DataControlField c in grdProveedores.Columns)
            {
                c.HeaderText = oIdioma.Texto(c.HeaderText);
            }
            EstableceIdiomaBotones(oIdioma.Id, this.Controls);
        }

        private void AgregaScriptCliente()
        {
            try
            {
                string codigo;
                codigo = "function ValidaDatos() { ";
                codigo += "obj = document.getElementById('" + txtNumProv.ClientID + "').value; ";
                codigo += "if (obj == null || obj == \"\") { ";
                codigo += "    alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCapNumProv") + "\"); ";
                codigo += "    return false; ";
                codigo += "} ";
                codigo += "obj = document.getElementById('" + txtRFC.ClientID + "').value; ";
                codigo += "if (obj == null || obj == \"\") { ";
                codigo += "    alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCapRFCProv") + "\"); ";
                codigo += "    return false; ";
                codigo += "} ";
                codigo += "obj = document.getElementById('" + txtNombre.ClientID + "').value; ";
                codigo += "if (obj == null || obj == \"\") { ";
                codigo += "    alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCapNomProv") + "\"); ";
                codigo += "    return false; ";
                codigo += "} ";
                codigo += "obj = document.getElementById('" + txtCorreo.ClientID + "').value; ";
                codigo += "if (obj == null || obj == \"\") { ";
                codigo += "    alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCapEmailProv") + "\"); ";
                codigo += "    return false; ";
                codigo += "} ";
                codigo += "return true; ";
                codigo += "} ";

                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ValidaDatos", codigo, true);

                codigo = "function validaNombreArchivo() { ";
                codigo += "obj = document.getElementById('" + File1.ClientID + "').value; ";
                codigo += "if (obj == null || obj == \"\") { ";
                codigo += "alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgSeleccioneArchivo") + "\");";
                codigo += "return false; ";
                codigo += "} ";
                codigo += "if (obj.substring(obj.length - 3).toUpperCase() != \"XLS\" && obj.substring(obj.length - 3).toUpperCase() != \"TXT\") { ";
                codigo += "alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgFormatoIncorrecto") + "\"); ";
                codigo += "return false; ";
                codigo += "} ";
                codigo += "} ";

                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ValidaDatos2", codigo, true);

            }
            catch (Exception ex)
            {
                MessageBox(null, null, ex.Message);
            }
        }

    }
}
