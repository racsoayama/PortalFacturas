using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NegocioPF;

namespace PortalFacturas
{
    public partial class Monedas : FormaPadre
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ValidaVariables();
                    EstableceIdioma((Idioma)Session["oIdioma"]);

                    NegocioPF.Monedas oMonedas = new NegocioPF.Monedas();
                    oMonedas.Cargar();
                    grdMonedas.DataSource = oMonedas.Datos;
                    grdMonedas.DataBind();


                    Perfil oPerfil = new Perfil();
                    Permisos permisos = oPerfil.CargarPermisos(((Usuario)Session["oUsuario"]).Id, "Monedas.aspx");
                    grdMonedas.Columns[2].Visible = permisos.Alta;
                    grdMonedas.Columns[3].Visible = permisos.Edicion;
                    grdMonedas.Columns[4].Visible = permisos.Baja;

                    divDetalle.Visible = oMonedas.Datos.Tables[0].Rows.Count == 0 && (permisos.Alta || permisos.Edicion);
                    Session["Accion"] = "Agregar";

                    if (oMonedas.Datos.Tables[0].Rows.Count == 0)
                    {
                        Session["Moneda"] = "";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
                }
            }

            AgregaScriptCliente();

        }

        protected void grdMonedas_RowDataBound(object sender, GridViewRowEventArgs e)
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
                txtMonSAP.Text = "";
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
                NegocioPF.Moneda oMoneda = new NegocioPF.Moneda(grdMonedas.DataKeys[index].Value.ToString());
                oMoneda.Cargar();

                //Muestra los datos en los controles
                Session["Accion"] = "Edicion";
                txtID.Text = oMoneda.ID;
                txtMonSAP.Text = oMoneda.MonedaSAP;

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
                NegocioPF.Moneda oMoneda = new NegocioPF.Moneda(grdMonedas.DataKeys[index].Values[0].ToString(), 
                                                                grdMonedas.DataKeys[index].Values[1].ToString());
                if (oMoneda.ValidaBaja())
                {
                    oMoneda.Eliminar(((Usuario)Session["oUsuario"]).Id);

                    NegocioPF.Monedas oMonedas = new NegocioPF.Monedas();
                    oMonedas.Cargar();
                    grdMonedas.DataSource = oMonedas.Datos;
                    grdMonedas.DataBind();

                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgMonedaEliminada"));
                }
                else
                {
                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgMonedaAsociada"));
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

                NegocioPF.Moneda oMoneda = new NegocioPF.Moneda(txtID.Text, txtMonSAP.Text);

                if (Session["Accion"].ToString() == "Alta")
                    oMoneda.ValidaDatos();

                oMoneda.Guardar(((Usuario)Session["oUsuario"]).Id, Session["Accion"].ToString());

                NegocioPF.Monedas oMonedas = new NegocioPF.Monedas();
                oMonedas.Cargar();
                grdMonedas.DataSource = oMonedas.Datos;
                grdMonedas.DataBind();

                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgMonedaGuardada"));
                divDetalle.Visible = false;
                //}
                //else
                //{
                //    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgMonedaExistente"));
                //}
            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            txtID.Text = "";
            txtMonSAP.Text = "";
            divDetalle.Visible = false;
        }

        private void EstableceIdioma(Idioma oIdioma)
        {
            lblTitulo.Text = oIdioma.Texto("Monedas");
            lblLeyMonedas.Text = oIdioma.Texto("Monedas");
            lblID.Text = oIdioma.Texto("MonedaID") + ":";
            lblMonSAP.Text = oIdioma.Texto("MonedaSAP") + ":";

            foreach (DataControlField c in grdMonedas.Columns)
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
                codigo += "    alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCapIDMoneda") + "\"); ";
                codigo += "    return false; ";
                codigo += "} ";
                codigo += "obj = document.getElementById('" + txtMonSAP.ClientID + "').value; ";
                codigo += "if (obj == null || obj == \"\") { ";
                codigo += "    alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCapNomMoneda") + "\"); ";
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
