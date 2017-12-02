using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NegocioPF;

namespace PortalFacturas
{
    public partial class Unidades : FormaPadre
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ValidaVariables();
                    EstableceIdioma((Idioma)Session["oIdioma"]);

                    NegocioPF.UnidadesMedida oUnidades = new NegocioPF.UnidadesMedida();
                    oUnidades.Cargar();
                    grdUnidades.DataSource = oUnidades.Datos;
                    grdUnidades.DataBind();


                    Perfil oPerfil = new Perfil();
                    Permisos permisos = oPerfil.CargarPermisos(((Usuario)Session["oUsuario"]).Id, "Unidades.aspx");
                    grdUnidades.Columns[2].Visible = permisos.Alta;
                    grdUnidades.Columns[3].Visible = permisos.Edicion;
                    grdUnidades.Columns[4].Visible = permisos.Baja;

                    divDetalle.Visible = oUnidades.Datos.Tables[0].Rows.Count == 0 && (permisos.Alta || permisos.Edicion);
                    Session["Accion"] = "Agregar";

                    if (oUnidades.Datos.Tables[0].Rows.Count == 0)
                    {
                        Session["Unidad"] = "";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
                }
            }

            AgregaScriptCliente();

        }

        protected void grdUnidades_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            ValidaVariables();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton btnAgregar = (ImageButton)e.Row.FindControl("btnAgregar");
                btnAgregar.ToolTip = ((Idioma)Session["oIdioma"]).Texto("Agregar");

                ImageButton btnEditar = (ImageButton)e.Row.FindControl("btnEditar");
                btnEditar.ToolTip = ((Idioma)Session["oIdioma"]).Texto("Editar");

                ImageButton btnEliminar = (ImageButton)e.Row.FindControl("btnEliminar");
                btnEliminar.ToolTip = ((Idioma)Session["oIdioma"]).Texto("Eliminar");
                btnEliminar.OnClientClick = "return confirm('" + ((Idioma)Session["oIdioma"]).Texto("ConfirmaBaja") + "')";
            }
        }

        protected void btnNuevo_Command(object sender, CommandEventArgs e)
        {
            try
            {
                Session["Accion"] = "Alta";
                txtID.Text = "";
                txtUnidadSAP.Text = "";
                txtID.Enabled = true;
                divDetalle.Visible = true;
            }
            catch
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
                int index = Convert.ToInt32(e.CommandArgument);

                //Carga los datos del Moneda
                NegocioPF.UnidadMedida oUnidad = new NegocioPF.UnidadMedida(grdUnidades.DataKeys[index].Value.ToString());
                oUnidad.Cargar();

                //Muestra los datos en los controles
                Session["Accion"] = "Edicion";
                txtID.Text = oUnidad.ID;
                txtUnidadSAP.Text = oUnidad.UnidadSAP;

                txtID.Enabled = false;
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
                int index = Convert.ToInt32(e.CommandArgument);

                //Carga la información a eliminar
                NegocioPF.UnidadMedida oUnidad = new NegocioPF.UnidadMedida(grdUnidades.DataKeys[index].Values[0].ToString(),
                                                                grdUnidades.DataKeys[index].Values[1].ToString());
                if (oUnidad.ValidaBaja())
                {
                    oUnidad.Eliminar(((Usuario)Session["oUsuario"]).Id);

                    NegocioPF.UnidadesMedida oUnidades = new NegocioPF.UnidadesMedida();
                    oUnidades.Cargar();
                    grdUnidades.DataSource = oUnidades.Datos;
                    grdUnidades.DataBind();

                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgUnidadEliminada"));
                }
                else
                {
                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgUnidadAsociada"));
                }
            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }


        protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ValidaVariables();

                NegocioPF.UnidadMedida oUnidad = new NegocioPF.UnidadMedida(txtID.Text, txtUnidadSAP.Text);

                if (Session["Accion"].ToString() == "Alta")
                    oUnidad.ValidaDatos();

                oUnidad.Guardar(((Usuario)Session["oUsuario"]).Id, Session["Accion"].ToString());

                NegocioPF.UnidadesMedida oUnidades = new NegocioPF.UnidadesMedida();
                oUnidades.Cargar();
                grdUnidades.DataSource = oUnidades.Datos;
                grdUnidades.DataBind();

                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgUnidadGuardada"));
                divDetalle.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            txtID.Text = "";
            txtUnidadSAP.Text = "";
            divDetalle.Visible = false;
        }

        private void EstableceIdioma(Idioma oIdioma)
        {
            lblTitulo.Text = oIdioma.Texto("Unidades");
            lblLeyUnidades.Text = oIdioma.Texto("Unidades");
            lblID.Text = oIdioma.Texto("UnidadID") + ":";
            lblUnidadSAP.Text = oIdioma.Texto("UnidadSAP") + ":";

            foreach (DataControlField c in grdUnidades.Columns)
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
                codigo += "obj = document.getElementById('" + txtID.ClientID + "').value; ";
                codigo += "if (obj == null || obj == \"0\") { ";
                codigo += "    alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCapIDUnidad") + "\"); ";
                codigo += "    return false; ";
                codigo += "} ";
                codigo += "obj = document.getElementById('" + txtUnidadSAP.ClientID + "').value; ";
                codigo += "if (obj == null || obj == \"\") { ";
                codigo += "    alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCapNomUnidad") + "\"); ";
                codigo += "    return false; ";
                codigo += "} ";
                codigo += "return true; ";
                codigo += "} ";

                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ValidaDatos", codigo, true);

            }
            catch (Exception ex)
            {
                MessageBox(null, null, ex.Message);
            }
        }


    }
}
