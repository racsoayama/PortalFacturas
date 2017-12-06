using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using NegocioPF;

namespace PortalFacturas
{
    public partial class Configuracion : FormaPadre
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ValidaVariables();
                    EstableceIdioma((Idioma)Session["oIdioma"]);

                    //Llena combo de perfiles
                    NegocioPF.Perfiles oPerfiles = new NegocioPF.Perfiles();
                    oPerfiles.Cargar();
                    cboPerfiles.DataSource = oPerfiles.Datos;
                    cboPerfiles.DataTextField = "Nombre";
                    cboPerfiles.DataValueField = "id_perfil";
                    cboPerfiles.DataBind();
                    cboPerfiles.Items.Insert(0, new ListItem(((Idioma)Session["oIdioma"]).Texto("Seleccionar") + " ...", "0"));

                    Idiomas oIdiomas = new Idiomas();
                    oIdiomas.Cargar(((Usuario)Session["oUsuario"]).Idioma);
                    cboIdioma.DataSource = oIdiomas.Datos;
                    cboIdioma.DataTextField = "nombre";
                    cboIdioma.DataValueField = "id_idioma";
                    cboIdioma.DataBind();
                    cboIdioma.SelectedValue = ((Usuario)Session["oUsuario"]).Idioma;

                    rbtPDFObligatorio.Items.Clear();
                    rbtPDFObligatorio.Items.Add(new ListItem(((Idioma)Session["oIdioma"]).Texto("Si"), "1"));
                    rbtPDFObligatorio.Items.Add(new ListItem(((Idioma)Session["oIdioma"]).Texto("No"), "2"));
                    rbtPDFObligatorio.SelectedIndex = 2;

                    rbtGuardarArch.Items.Clear();
                    rbtGuardarArch.Items.Add(new ListItem(((Idioma)Session["oIdioma"]).Texto("Si"), "1"));
                    rbtGuardarArch.Items.Add(new ListItem(((Idioma)Session["oIdioma"]).Texto("No"), "2"));
                    rbtGuardarArch.SelectedIndex = 2;

                    rbtValidaSAT.Items.Clear();
                    rbtValidaSAT.Items.Add(new ListItem(((Idioma)Session["oIdioma"]).Texto("Si"), "1"));
                    rbtValidaSAT.Items.Add(new ListItem(((Idioma)Session["oIdioma"]).Texto("No"), "2"));
                    rbtValidaSAT.SelectedIndex = 1;

                    rbtConexERP.Items.Clear();
                    rbtConexERP.Items.Add(new ListItem(((Idioma)Session["oIdioma"]).Texto("EnLinea"), "1"));
                    rbtConexERP.Items.Add(new ListItem(((Idioma)Session["oIdioma"]).Texto("Desconectado"), "2"));
                    rbtConexERP.SelectedIndex = 1;

                    NegocioPF.Configuracion oConfig = new NegocioPF.Configuracion();
                    oConfig.Cargar();
                    rbtPDFObligatorio.SelectedValue = (oConfig.PDFObligatorio ? "1" : "2");
                    rbtGuardarArch.SelectedValue = (oConfig.GuardarArchBD ? "1" : "2");
                    txtMesesAtras.Text = oConfig.MesesAtras.ToString();
                    txtMesesAdelante.Text = oConfig.MesesAdelante.ToString();
                    txtLongOrden.Text = oConfig.LongitudOrden.ToString();
                    cboPerfiles.SelectedValue = oConfig.PerfilProveedor.ToString();
                    cboIdioma.SelectedValue = oConfig.Idioma;
                    txtMensaje.Text = oConfig.Mensaje;
                    txtMailContacto.Text = oConfig.MailContacto;
                    rbtValidaSAT.SelectedValue = (oConfig.ValidacionSAT ? "1" : "2");
                    rbtConexERP.SelectedValue = (oConfig.EnLineaERP ? "1" : "2");

                    Perfil oPerfil = new Perfil();
                    Permisos permisos = oPerfil.CargarPermisos(((Usuario)Session["oUsuario"]).Id, "Configuracion.aspx");
                    btnGuardar.Visible = (permisos.Alta || permisos.Edicion);

                }
                catch (Exception ex)
                {
                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
                }
            }
            AgregaScriptCliente();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                ValidaVariables();

                NegocioPF.Configuracion oConfig = new NegocioPF.Configuracion();

                oConfig.PDFObligatorio = (rbtPDFObligatorio.SelectedValue == "1" ? true : false);
                oConfig.GuardarArchBD = (rbtGuardarArch.SelectedValue == "1" ? true : false);
                oConfig.MesesAtras = Convert.ToInt32(txtMesesAtras.Text);
                oConfig.MesesAdelante = Convert.ToInt32(txtMesesAdelante.Text);
                oConfig.LongitudOrden = Convert.ToInt32(txtLongOrden.Text);
                oConfig.PerfilProveedor = Convert.ToInt32(cboPerfiles.SelectedValue);
                oConfig.Idioma = cboIdioma.SelectedValue;
                oConfig.Mensaje = txtMensaje.Text;
                oConfig.MailContacto = txtMailContacto.Text;
                oConfig.ValidacionSAT = (rbtValidaSAT.SelectedValue == "1" ? true : false);
                oConfig.Guardar(((Usuario)Session["oUsuario"]).Id);

                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgConfigGuardada"));
            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Principal.aspx");
        }


        private void EstableceIdioma(Idioma oIdioma)
        {
            lblLeyConfig.Text = oIdioma.Texto("Configuracion");
            lblLongOrden.Text = oIdioma.Texto( "LongOrden") + ":";
            lblMesesAtras.Text = oIdioma.Texto("MesesAtras") + ":";
            lblMesesAdelante.Text = oIdioma.Texto("MesesAdelante") + ":";
            lblPDFObligatorio.Text = oIdioma.Texto("PDFObligatorio") + ":";
            lblGuardArchBD.Text = oIdioma.Texto("GuardarArchivosBD") + ":";
            lblConex.Text = oIdioma.Texto("ConexERP") + "?:";

            EstableceIdiomaBotones(oIdioma.Id, this.Controls);
        }

        private void AgregaScriptCliente()
        {
            try
            {
                string codigo;
                codigo = "function ValidaDatos() { ";
                codigo += "obj = document.getElementById('" + txtLongOrden.ClientID + "').value; ";
                codigo += "if (obj == null || obj == \"\") { ";
                codigo += "    alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCapLongOrden") + "\"); ";
                codigo += "    return false; ";
                codigo += "} ";
                codigo += "obj = document.getElementById('" + txtMesesAtras.ClientID + "').value; ";
                codigo += "if (obj == null || obj == \"\") { ";
                codigo += "    alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCapMesTolerancia") + "\"); ";
                codigo += "    return false; ";
                codigo += "} ";
                codigo += "obj = document.getElementById('" + txtMesesAdelante.ClientID + "').value; ";
                codigo += "if (obj == null || obj == \"\") { ";
                codigo += "    alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCapMesesAdelante") + "\"); ";
                codigo += "    return false; ";
                codigo += "} ";
                codigo += "obj = document.getElementById('" + cboPerfiles.ClientID + "').value; ";
                codigo += "if (obj == null || obj == \"0\") { ";
                codigo += "    alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgSelPerfilProv") + "\"); ";
                codigo += "    return false; ";
                codigo += "} ";
                codigo += "obj = document.getElementById('" + cboIdioma.ClientID + "').value; ";
                codigo += "if (obj == null || obj == \"0\") { ";
                codigo += "    alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgSelIdiomaCaptura") + "\"); ";
                codigo += "    return false; ";
                codigo += "} ";
                codigo += "obj = document.getElementById('" + txtMensaje.ClientID + "').value; ";
                codigo += "if (obj != null && obj != \"\") { ";
                codigo += "   if (obj.length > 256) { ";
                codigo += "      alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgLongMsgGrande") + "\"); ";
                codigo += "      return false; ";
                codigo += "   } ";
                codigo += "} ";
                codigo += "return true; ";
                codigo += "} ";

                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ValidaDatos1", codigo, true);

            }
            catch (Exception ex)
            {
                MessageBox(this, null, ex.Message);
            }
        }


    }
}
