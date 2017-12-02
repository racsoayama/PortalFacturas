using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NegocioPF;

namespace PortalFacturas
{
    public partial class Sociedades : FormaPadre
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ValidaVariables();
                    EstableceIdioma((Idioma)Session["oIdioma"]);

                    NegocioPF.Sociedades oSociedades = new NegocioPF.Sociedades();
                    oSociedades.Cargar();
                    grdSociedades.DataSource = oSociedades.Datos;
                    grdSociedades.DataBind();


                    Perfil oPerfil = new Perfil();
                    Permisos permisos = oPerfil.CargarPermisos(((Usuario)Session["oUsuario"]).Id, "Sociedades.aspx");
                    grdSociedades.Columns[8].Visible = permisos.Alta;
                    grdSociedades.Columns[9].Visible = permisos.Edicion;
                    grdSociedades.Columns[10].Visible = permisos.Baja;

                    divDetalle.Visible = oSociedades.Datos.Tables[0].Rows.Count == 0 && (permisos.Alta || permisos.Edicion);
                    Session["Accion"] = "Agregar";

                }
                catch (Exception ex)
                {
                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message)); 
                }
            }

            AgregaScriptCliente();

        }

        protected void grdSociedades_RowDataBound(object sender, GridViewRowEventArgs e)
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
                Session["Accion"] = "Agregar";
                txtID.Text = "";
                txtNombre.Text = "";
                txtRFC.Text = "";
                txtCtaProv.Text = "";
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

                //Carga los datos del Sociedad
                Sociedad oSociedad = new Sociedad(grdSociedades.DataKeys[index].Value.ToString());
                oSociedad.Cargar();

                //Muestra los datos en los controles
                Session["Accion"] = "Editar";
                txtID.Text = oSociedad.ID.ToString();
                txtNombre.Text = oSociedad.Nombre;
                txtRFC.Text = oSociedad.RFC;
                txtBusArea.Text = oSociedad.BusinessArea;
                txtContArea.Text = oSociedad.ControllingArea;
                txtGralLeyerAcc.Text = oSociedad.GeneralLeyerAccount;
                txtCostCenter.Text = oSociedad.CostCenter;
                txtCtaProv.Text = oSociedad.CuentaProveedor; 

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
                Sociedad oSociedad = new Sociedad(grdSociedades.DataKeys[index].Value.ToString());
                if (oSociedad.ValidaBaja())
                {
                    oSociedad.Eliminar(((Usuario)Session["oUsuario"]).Id);

                    NegocioPF.Sociedades oSociedades = new NegocioPF.Sociedades();
                    oSociedades.Cargar();
                    grdSociedades.DataSource = oSociedades.Datos;
                    grdSociedades.DataBind();

                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgSociedadEliminada"));
                }
                else
                {
                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgSociedadAsociada"));
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

                NegocioPF.Sociedad oSociedad = new NegocioPF.Sociedad(txtID.Text, txtNombre.Text, txtRFC.Text);
                oSociedad.BusinessArea = txtBusArea.Text;
                oSociedad.ControllingArea = txtContArea.Text;
                oSociedad.GeneralLeyerAccount = txtGralLeyerAcc.Text;
                oSociedad.CostCenter = txtCostCenter.Text;
                oSociedad.CuentaProveedor = txtCtaProv.Text;
                oSociedad.ValidaDatos(Session["Accion"].ToString());
                oSociedad.Guardar(((Usuario)Session["oUsuario"]).Id);

                NegocioPF.Sociedades oSociedades = new NegocioPF.Sociedades();
                oSociedades.Cargar();
                grdSociedades.DataSource = oSociedades.Datos;
                grdSociedades.DataBind();

                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgSociedadGuardada"));
                divDetalle.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            txtNombre.Text = "";
            txtBusArea.Text = "";
            txtContArea.Text = "";
            txtGralLeyerAcc.Text = "";
            txtCostCenter.Text = "";
            txtCtaProv.Text = "";
            divDetalle.Visible = false;
        }

        private void EstableceIdioma(Idioma oIdioma)
        {
            lblLeyTitulo.Text = oIdioma.Texto("Sociedades");
            lblLeySociedad.Text = oIdioma.Texto("Sociedad");
            lblID.Text = oIdioma.Texto("SociedadID") + ":";
            lblNombre.Text = oIdioma.Texto("Nombre") + ":";
            lblRFC.Text = oIdioma.Texto("RFC") + ":";
            lblBusinessArea.Text = oIdioma.Texto("BusinessArea") + ":";
            lblCntrlArea.Text = oIdioma.Texto("ControllingArea") + ":";
            lblGralLeyerAcc.Text = oIdioma.Texto("GeneralLeyerAccount") + ":";
            lblCostCenter.Text = oIdioma.Texto("CostCenter") + ":";
            lblCtaProv.Text = oIdioma.Texto("CtaProv") + ":";

            foreach (DataControlField c in grdSociedades.Columns)
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
                codigo += "if (obj == null || obj == \"\") { ";
                codigo += "    alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCaptureIDSociedad") + "\"); ";
                codigo += "    return false; ";
                codigo += "} ";
                codigo += "obj = document.getElementById('" + txtNombre.ClientID + "').value; ";
                codigo += "if (obj == null || obj == \"\") { ";
                codigo += "    alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCaptureNombreSociedad") + "\"); ";
                codigo += "    return false; ";
                codigo += "} ";
                codigo += "  obj = document.getElementById('" + txtRFC.ClientID + "').value; ";
                codigo += "  if (obj == null || obj == \"\") { ";
                codigo += "    alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCapRFCProv") + "\"); ";
                codigo += "    return false; ";
                codigo += "  } ";
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
