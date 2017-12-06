using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NegocioPF;

namespace PortalFacturas
{
    public partial class Indicadores : FormaPadre
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ValidaVariables();
                    EstableceIdioma((Idioma)Session["oIdioma"]);

                    NegocioPF.Indicadores oIndicadores = new NegocioPF.Indicadores();
                    oIndicadores.Cargar();
                    grdIndicadores.DataSource = oIndicadores.Datos;
                    grdIndicadores.DataBind();


                    Perfil oPerfil = new Perfil();
                    Permisos permisos = oPerfil.CargarPermisos(((Usuario)Session["oUsuario"]).Id, "Indicadores.aspx");
                    grdIndicadores.Columns[3].Visible = permisos.Alta;
                    grdIndicadores.Columns[4].Visible = permisos.Edicion;
                    grdIndicadores.Columns[5].Visible = permisos.Baja;

                    divDetalle.Visible = oIndicadores.Datos.Tables[0].Rows.Count == 0 && (permisos.Alta || permisos.Edicion);
                    //Session["Accion"] = "Agregar";

                    if (oIndicadores.Datos.Tables[0].Rows.Count == 0)
                    {
                        Session["Indicador"] = "";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
                }
            }

            AgregaScriptCliente();

        }

        protected void grdIndicadores_RowDataBound(object sender, GridViewRowEventArgs e)
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
                Session["Indicador"] = "";
                txtID.Text = "";
                txtNombre.Text = "";
                txtTasa.Text = "";
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

                //Carga los datos del Indicador
                NegocioPF.Indicador oIndicador = new NegocioPF.Indicador(grdIndicadores.DataKeys[index].Value.ToString());
                oIndicador.Cargar();

                //Muestra los datos en los controles
                Session["Indicador"] = oIndicador.ID.ToString();
                txtID.Text = oIndicador.ID;
                txtNombre.Text = oIndicador.Nombre;
                txtTasa.Text = oIndicador.Tasa.ToString();

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
                NegocioPF.Indicador oIndicador = new NegocioPF.Indicador(grdIndicadores.DataKeys[index].Value.ToString());
                //if (oIndicador.ValidaBaja())
                //{
                    oIndicador.Eliminar(((Usuario)Session["oUsuario"]).Id);

                    NegocioPF.Indicadores oIndicadores = new NegocioPF.Indicadores();
                    oIndicadores.Cargar();
                    grdIndicadores.DataSource = oIndicadores.Datos;
                    grdIndicadores.DataBind();

                    Perfil oPerfil = new Perfil();
                    Permisos permisos = oPerfil.CargarPermisos(((Usuario)Session["oUsuario"]).Id, "Indicadores.aspx");
                    divDetalle.Visible = oIndicadores.Datos.Tables[0].Rows.Count == 0 && (permisos.Alta || permisos.Edicion);

                    if (oIndicadores.Datos.Tables[0].Rows.Count == 0)
                    {
                        //Session["Accion"] = "Agregar";
                        Session["Indicador"] = "";
                        txtID.Text = "";
                        txtNombre.Text = "";
                        txtTasa.Text = "";
                        txtID.Enabled = true;
                    }

                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgIndicadorEliminado"));
                //}
                //else
                //{
                //    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgIndicadorAsociado"));
                //}
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

                NegocioPF.Indicador oIndicador = new NegocioPF.Indicador(txtID.Text, txtNombre.Text, Convert.ToInt32(txtTasa.Text));

                if (oIndicador.ValidaDatos())
                {
                    oIndicador.Guardar(((Usuario)Session["oUsuario"]).Id);

                    NegocioPF.Indicadores oIndicadores = new NegocioPF.Indicadores();
                    oIndicadores.Cargar();
                    grdIndicadores.DataSource = oIndicadores.Datos;
                    grdIndicadores.DataBind();

                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgIndicadorGuardado"));
                    divDetalle.Visible = false;
                }
                else
                {
                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgIndicadorExistente"));
                }
            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            txtID.Text = "";
            txtNombre.Text = "";
            divDetalle.Visible = false;
        }

        private void EstableceIdioma(Idioma oIdioma)
        {
            lblTitulo.Text = oIdioma.Texto("IndImpuestos");
            lblLeyIndicador.Text = oIdioma.Texto("IndImpuesto");
            lblID.Text = oIdioma.Texto("IndicadorID") + ":";
            lblNombre.Text = oIdioma.Texto("Nombre") + ":";
            lblTasa.Text = oIdioma.Texto("TasaImp") + ":";

            foreach (DataControlField c in grdIndicadores.Columns)
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
                codigo += "    alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCapIDIndicador") + "\"); ";
                codigo += "    return false; ";
                codigo += "} ";
                codigo += "obj = document.getElementById('" + txtNombre.ClientID + "').value; ";
                codigo += "if (obj == null || obj == \"\") { ";
                codigo += "    alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCapDescIndicador") + "\"); ";
                codigo += "    return false; ";
                codigo += "} ";
                codigo += "obj = document.getElementById('" + txtTasa.ClientID + "').value; ";
                codigo += "if (obj == null || obj == \"\") { ";
                codigo += "    alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCapTasaImp") + "\"); ";
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
