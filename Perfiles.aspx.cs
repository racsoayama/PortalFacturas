using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NegocioPF;

namespace PortalFacturas
{
    public partial class Perfiles : FormaPadre
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    //Recupera el usuario de las variables
                    ValidaVariables();
                    EstableceIdioma((Idioma)Session["oIdioma"]);

                    NegocioPF.Perfiles oPerfiles = new NegocioPF.Perfiles();
                    oPerfiles.Cargar();
                    grdPerfiles.DataSource = oPerfiles.Datos;
                    grdPerfiles.DataBind();

                    dvDetallePerfil.Visible = false;

                    NegocioPF.Perfil oPerfil = new NegocioPF.Perfil();
                    NegocioPF.Permisos permisos = oPerfil.CargarPermisos(((Usuario)Session["oUsuario"]).Id, "Perfiles.aspx");
                    grdPerfiles.Columns[2].Visible = permisos.Consulta;
                    grdPerfiles.Columns[3].Visible = permisos.Alta;
                    grdPerfiles.Columns[4].Visible = permisos.Edicion;
                    grdPerfiles.Columns[5].Visible = permisos.Baja;

                }
                catch (Exception ex)
                {
                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message)); 
                }
            
            }
            AgregaScriptCliente();

        }

        protected void grdPerfiles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            ValidaVariables();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton btnVerDetalles = (ImageButton)e.Row.FindControl("btnVerDetalles");
                btnVerDetalles.ToolTip = ((Idioma)Session["oIdioma"]).Texto("Consultar");

                ImageButton btnAgregar = (ImageButton)e.Row.FindControl("btnAgregar");
                btnAgregar.ToolTip = ((Idioma)Session["oIdioma"]).Texto("Agregar");

                ImageButton btnEditar = (ImageButton)e.Row.FindControl("btnEditar");
                btnEditar.ToolTip = ((Idioma)Session["oIdioma"]).Texto("Editar");

                ImageButton btnEliminar = (ImageButton)e.Row.FindControl("btnEliminar");
                btnEliminar.ToolTip = ((Idioma)Session["oIdioma"]).Texto("Eliminar");
                btnEliminar.OnClientClick = "return confirm('" + ((Idioma)Session["oIdioma"]).Texto("ConfirmaBaja") + "')";
            }
        }

        protected void grdFunciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Permisos perFuncion = new Permisos(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "permisos")));

                CheckBox chkAlta = (CheckBox)e.Row.FindControl("chkAlta");
                if (chkAlta != null)
                    chkAlta.Visible = perFuncion.Alta;

                CheckBox chkBaja = (CheckBox)e.Row.FindControl("chkBaja");
                if (chkBaja != null)
                    chkBaja.Visible = perFuncion.Baja;

                CheckBox chkConsulta = (CheckBox)e.Row.FindControl("chkConsulta");
                if (chkConsulta != null)
                    chkConsulta.Visible = perFuncion.Consulta;

                CheckBox chkEdicion = (CheckBox)e.Row.FindControl("chkEdicion");
                if (chkEdicion != null)
                    chkEdicion.Visible = perFuncion.Edicion;

                CheckBox chkImportar = (CheckBox)e.Row.FindControl("chkImportar");
                if (chkImportar != null)
                    chkImportar.Visible = perFuncion.Importar;

                CheckBox chkImprimir = (CheckBox)e.Row.FindControl("chkImprimir");
                if (chkImprimir != null)
                    chkImprimir.Visible = perFuncion.Imprimir;

                e.Row.Cells[0].Text = ((Idioma)Session["oIdioma"]).Texto(e.Row.Cells[0].Text);
                e.Row.Cells[1].Text = ((Idioma)Session["oIdioma"]).Texto(e.Row.Cells[1].Text);

            }

        }

        protected void grdFunciones_PreRender(object sender, EventArgs e)
        {
            MergeRows(grdFunciones, 1);
        }

        private static void MergeRows(GridView gridView, int cols)
        {
            for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow row = gridView.Rows[rowIndex];
                GridViewRow previousRow = gridView.Rows[rowIndex + 1];

                for (int i = 0; i < cols; i++) //row.Cells.Count
                {
                    if ((row.Cells[i].Text == previousRow.Cells[i].Text && row.Cells[1].Text == previousRow.Cells[1].Text) ||
                        (i == 0 && row.Cells[0].Text == previousRow.Cells[0].Text))
                    {
                        row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 :
                                               previousRow.Cells[i].RowSpan + 1;
                        previousRow.Cells[i].Visible = false;
                    }
                }
            }
        }


        protected void btnVerDetalles_Command(object sender, CommandEventArgs e)
        {
            try
            {
                ValidaVariables();

                if (e.CommandName == "Consultar")
                {
                    //Obtiene indice de la linea a actualizar
                    int index = Convert.ToInt32(e.CommandArgument);

                    CargaGrids(Convert.ToInt32(grdPerfiles.DataKeys[index].Value));

                    Session["id_perfil"] = Convert.ToInt32(grdPerfiles.DataKeys[index].Value);

                    txtNombre.Enabled = true;
                    dvDetallePerfil.Visible = true;
                    grdFunciones.Enabled = false;
                    divPerfiles.Visible = false;
                    btnGuardar.Visible = false;
                }
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
                ValidaVariables();
                
                LimpiarControles();

                CargaGrids(0);

                btnGuardar.Visible = true;
                grdFunciones.Enabled = true;
                dvDetallePerfil.Visible = true;
                divPerfiles.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message)); 
            }
        }

        protected void btnEditar_Command(object sender, CommandEventArgs e)
        {
            try
            {
                ValidaVariables();

                //obtiene indice de la linea aactualizar
                int index = Convert.ToInt32(e.CommandArgument);

                CargaGrids(Convert.ToInt32(grdPerfiles.DataKeys[index].Value));

                //Muestra los datos en los controles
                Session["id_perfil"] = Convert.ToInt32(grdPerfiles.DataKeys[index].Value);

                txtNombre.Enabled = true;
                dvDetallePerfil.Visible = true;
                divPerfiles.Visible = false;

                grdFunciones.Enabled = true;
                btnGuardar.Visible = true;
                
            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message)); 
            }
        }

        protected void btnEliminaPerfil_Command(object sender, CommandEventArgs e)
        {
            try
            {
                ValidaVariables();

                //Obtiene indice de la linea a actualizar
                int index = Convert.ToInt32(e.CommandArgument);

                if (Convert.ToInt32(grdPerfiles.DataKeys[index].Value) != 1)
                {
                    //Carga la información a eliminar
                    NegocioPF.Perfil oPerfil = new NegocioPF.Perfil(Convert.ToInt32(grdPerfiles.DataKeys[index].Value));
                    oPerfil.Eliminar(((Usuario)Session["oUsuario"]).Id);

                    NegocioPF.Perfiles oPerfiles = new NegocioPF.Perfiles();
                    oPerfiles.Cargar();
                    grdPerfiles.DataSource = oPerfiles.Datos;
                    grdPerfiles.DataBind();

                    LimpiarControles();

                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgPerfilEliminado"));
                }
                else
                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgPerfilAdminNoBorrar"));

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

                Perfil oPerfil = new Perfil(Convert.ToInt32(Session["id_perfil"]));
                oPerfil.Nombre = txtNombre.Text;
                
                //string permisos;

                RecuperaFuncionesGrid(ref oPerfil, grdFunciones);

                if (oPerfil.Validar())
                {
                    oPerfil.Guardar(((Usuario)Session["oUsuario"]).Id);

                    NegocioPF.Perfiles oPerfiles = new NegocioPF.Perfiles();
                    oPerfiles.Cargar();
                    grdPerfiles.DataSource = oPerfiles.Datos;
                    grdPerfiles.DataBind();

                    LimpiarControles();

                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgPerfilActualizado"));

                    dvDetallePerfil.Visible = false;
                    divPerfiles.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message)); 
            }

        }

        private void RecuperaFuncionesGrid(ref Perfil oPerfil, GridView grid)
        {
                string permisos;
                foreach (GridViewRow row in grid.Rows)
                {
                    permisos = "";
                    CheckBox cb = (CheckBox)row.FindControl("chkAlta");
                    if (cb != null && cb.Checked && cb.Visible)
                    {
                        permisos += "A";
                    }

                    cb = (CheckBox)row.FindControl("chkBaja");
                    if (cb != null && cb.Checked && cb.Visible)
                    {
                        permisos += "B";
                    }

                    cb = (CheckBox)row.FindControl("chkEdicion");
                    if (cb != null && cb.Checked && cb.Visible)
                    {
                        permisos += "C";
                    }

                    cb = (CheckBox)row.FindControl("chkConsulta");
                    if (cb != null && cb.Checked && cb.Visible)
                    {
                        permisos += "D";
                    }

                    cb = (CheckBox)row.FindControl("chkImportar");
                    if (cb != null && cb.Checked && cb.Visible)
                    {
                        permisos += "F";
                    }

                    cb = (CheckBox)row.FindControl("chkImprimir");
                    if (cb != null && cb.Checked && cb.Visible)
                    {
                        permisos += "G";
                    }

                    if (permisos.Length > 0)
                        oPerfil.Funciones.Add(new Funcion(Convert.ToInt32(grid.DataKeys[row.RowIndex].Value), permisos));
                }

        }

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            LimpiarControles();
            dvDetallePerfil.Visible = false;
            divPerfiles.Visible = true;
        }

        protected void LimpiarControles()
        {
            Session["id_perfil"] = 0;
            txtNombre.Text = "";
        }

        private void CargaGrids(int perfil)
        {
            try
            {
                //Carga los datos del perfil
                NegocioPF.Perfil oPerfil = new NegocioPF.Perfil(perfil);
                oPerfil.Cargar();

                txtNombre.Text = oPerfil.Nombre;

                grdFunciones.DataSource = oPerfil.Datos; // FuncionesMenu(1);
                grdFunciones.DataBind();

            }
            catch (Exception ex)
            {
                MessageBox(this, null, ex.Message);
            }
        }

        private void EstableceIdioma(Idioma oIdioma)
        {
            lblPerfiles.Text = oIdioma.Texto("Perfiles");
            lblConfPerfil.Text = oIdioma.Texto("ConfiguracionPerfil");
            lblNombre.Text = oIdioma.Texto("Nombre") + ":";

            foreach (DataControlField c in grdPerfiles.Columns)
            {
                c.HeaderText = oIdioma.Texto(c.HeaderText);
            }

            foreach (DataControlField c in grdFunciones.Columns)
            {
                c.HeaderText = oIdioma.Texto(c.HeaderText);
            }

            EstableceIdiomaBotones(oIdioma.Id, this.Controls);
        }

        private void AgregaScriptCliente()
        {
            try
            {
                ValidaVariables();

                string codigo;
                codigo = "function ValidaDatos() { ";
                codigo += "obj = document.getElementById('" + txtNombre.ClientID + "').value; ";
                codigo += "if (obj == null || obj == \"\") { ";
                codigo += "    alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCaptureNombrePerfil") + "\"); ";
                codigo += "    return false; ";
                codigo += "} ";
                codigo += "return true; ";
                codigo += "} ";

                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ValidaDatos", codigo, true);

            }
            catch (Exception ex)
            {
                MessageBox(this, null, ex.Message);
            }
        }

    }
}
