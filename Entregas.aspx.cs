using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NegocioPF;

namespace PortalFacturas
{
    public partial class Entregas : FormaPadre
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ValidaVariables();
                    EstableceIdioma((Idioma)Session["oIdioma"]);

                    //Si el usuario es un usuario del proveedor, por defautl se muestra sus pedidos 
                    NegocioPF.Proveedor oProveedor = new NegocioPF.Proveedor(((Usuario)Session["oUsuario"]).Id);
                    oProveedor.Cargar();
                    NegocioPF.Entregas oEntregas = new NegocioPF.Entregas();
                    if (oProveedor.Nombre != "" && oProveedor.Nombre != null)
                        oEntregas.Cargar(((Usuario)Session["oUsuario"]).Id, "");
                    else
                        oEntregas.Cargar();
                    grdEntregas.DataSource = oEntregas.Datos;
                    grdEntregas.DataBind();

                    Perfil oPerfil = new Perfil();
                    Permisos permisos = oPerfil.CargarPermisos(((Usuario)Session["oUsuario"]).Id, "Entregas.aspx");
                    btnImportar.Visible = permisos.Importar;

                    divFiltros.Visible = true;
                    divImportar.Visible = false;

                }
                catch (Exception ex)
                {
                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
                }
            }

            AgregaScriptCliente();
        }

        protected void grdEntregas_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            try
            {
                ValidaVariables();
                grdEntregas.PageIndex = e.NewPageIndex;

                NegocioPF.Proveedor oProveedor = new NegocioPF.Proveedor(((Usuario)Session["oUsuario"]).Id);
                oProveedor.Cargar();
                NegocioPF.Entregas oEntregas = new NegocioPF.Entregas();
                if (oProveedor.Nombre != "" && oProveedor.Nombre != null)
                    oEntregas.Cargar(((Usuario)Session["oUsuario"]).Id, "");
                else
                    oEntregas.Cargar();
                grdEntregas.DataSource = oEntregas.Datos;
                grdEntregas.DataBind();

            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                ValidaVariables();

                NegocioPF.Proveedor oProveedor = new NegocioPF.Proveedor(((Usuario)Session["oUsuario"]).Id);
                oProveedor.Cargar();
                NegocioPF.Entregas oEntregas = new NegocioPF.Entregas();
                if (oProveedor.Nombre != "" && oProveedor.Nombre != null)
                    oEntregas.Cargar(((Usuario)Session["oUsuario"]).Id, txtFilPedido.Text);
                else
                    oEntregas.Cargar(txtFilProv.Text, txtFilPedido.Text);
                grdEntregas.DataSource = oEntregas.Datos;
                grdEntregas.DataBind();

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
                txtFilPedido.Text = "";

                NegocioPF.Proveedor oProveedor = new NegocioPF.Proveedor(((Usuario)Session["oUsuario"]).Id);
                oProveedor.Cargar();
                NegocioPF.Entregas oEntregas = new NegocioPF.Entregas();
                if (oProveedor.Nombre != "" && oProveedor.Nombre != null)
                    oEntregas.Cargar(((Usuario)Session["oUsuario"]).Id, "");
                else
                    oEntregas.Cargar();
                grdEntregas.DataSource = oEntregas.Datos;
                grdEntregas.DataBind();

            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }

        protected void btnImportar_Click(object sender, EventArgs e)
        {
            divImportar.Visible = true;
        }

        protected void btnAceptarImportar_Click(object sender, EventArgs e)
        {
            int registros = 0;
            string sPathArchivo;
            try
            {
                ValidaVariables();

                if ((File1.PostedFile == null) || (File1.PostedFile.ContentLength == 0))
                    throw new Exception("MsgSelArchHdr");

                //Copia el archivo
                string fn = System.IO.Path.GetFileName(File1.PostedFile.FileName);

                sPathArchivo = Server.MapPath("") + "\\Data\\Entregas." + fn.Substring(fn.Length - 3, 3);

                try
                {
                    File1.PostedFile.SaveAs(sPathArchivo);
                }
                catch (Exception ex)
                {
                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgErrorCopiarArchivo"));
                }

                try
                {
                    NegocioPF.Entregas oEntregas = new NegocioPF.Entregas();

                    registros = oEntregas.ImportarTXT(((Usuario)Session["oUsuario"]).Id, sPathArchivo,
                                                       System.IO.Path.GetFileName(File1.PostedFile.FileName));

                    oEntregas.Cargar();
                    grdEntregas.DataSource = oEntregas.Datos;
                    grdEntregas.DataBind();

                    divImportar.Visible = false;

                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgRegActInsertados") + registros.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox(sender, e, "Error:" + ((Idioma)Session["oIdioma"]).Texto(ex.Message));
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
            lblTitulo.Text = oIdioma.Texto("EntMerc");
            lblFilProv.Text = oIdioma.Texto("NumProveedor");
            lblFilPedido.Text = oIdioma.Texto("Pedido") + ":";
            lblLeyArchivo.Text = oIdioma.Texto("MsgSelArchivos") + ":";

            foreach (DataControlField c in grdEntregas.Columns)
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
                codigo = "function validaNombreArchivo() { ";
                codigo += "obj = document.getElementById('" + File1.ClientID + "').value; ";
                codigo += "if (obj == null || obj == \"\") { ";
                codigo += "alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgSeleccioneArchivo") + "\");";
                codigo += "return false; ";
                codigo += "} ";
                codigo += "if (obj.substring(obj.length - 3).toUpperCase() != \"TXT\") { ";
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