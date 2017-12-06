using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Reflection;
using System.Net.Mail;
using System.Net.Mime;
using NegocioPF;

namespace PortalFacturas
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string usuario = "";
                    divCambioPasw.Visible = false;
                    divNuevaCta.Visible = false;

                    //Leer el usuario en el cookie
                    if (Request.Cookies["UsuarioPF"] != null)
                    {
                        if (Request.Cookies["UsuarioPF"].Value != null)
                            usuario = Request.Cookies["UsuarioPF"].Value;
                    }

                    if (usuario == "")
                        usuario = User.Identity.Name.Substring(User.Identity.Name.IndexOf("\\") + 1);

                    Usuario oUsuario = new Usuario(usuario);
                    oUsuario.Cargar();

                    txtUsuario.Text = usuario;

                    //Obtiene el idioma del sistema
                    CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

                    //Si es la primera vez que entra el usuario y aún no tiene definido el idioma.
                    if (oUsuario.Idioma == "" || oUsuario.Idioma == null)
                        oUsuario.Idioma = currentCulture.ToString();


                    Idiomas oIdiomas = new Idiomas();
                    oIdiomas.Cargar(oUsuario.Idioma);
                    cboIdioma.DataSource = oIdiomas.Datos;
                    cboIdioma.DataTextField = "nombre";
                    cboIdioma.DataValueField = "id_idioma";
                    cboIdioma.DataBind();
                    cboIdioma.SelectedValue = oUsuario.Idioma;

                    Idioma oIdioma = new Idioma(oUsuario.Idioma);
                    Session["oIdioma"] = oIdioma;

                    EstableceIdioma(oIdioma);
                    this.Form.DefaultButton = this.btnAceptar.UniqueID;
                }
                catch (Exception ex)
                {
                    Idioma oIdioma = new Idioma(Thread.CurrentThread.CurrentCulture.ToString());
                    MessageBox(sender, e, oIdioma.Texto(ex.Message));
                }
            }
            AgregaScriptCliente();
        }


        protected void LoginButton_Click(object sender, EventArgs e)
        {
        }

        protected void btnCambioPassword_Click(object sender, EventArgs e)
        {
        }

        protected void btnAceptarCambio_Click(object sender, EventArgs e)
        {
        }


        protected void btnCancelarCambio_Click(object sender, EventArgs e)
        {
        }

        protected void btnRecuperaPassword_Click(object sender, EventArgs e)
        {
        }

        private void EnviaCorreoPassword(NegocioPF.Usuario oUsuario)
        {
        }

        #region NuevaCuenta

        protected void btnCrearCta_Click(object sender, EventArgs e)
        {
        }

        protected void btnAceptarRegistro_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelarRegistro_Click(object sender, EventArgs e)
        {
        }

        private void EnviaCorreoNuevaCuenta(NegocioPF.Usuario oUsuario)
        {
        }

        #endregion NuevaCuenta

        protected void btnVerManual_Click(object sender, EventArgs e)
        {
        }


        protected void MessageBox(object sender, EventArgs e, string strMensaje)
        {
            // Do something
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "redirect script",
            "alert('" + strMensaje + "'); ", true);

            //location.href='pagina.aspx';
        }

        private void EstableceIdioma(NegocioPF.Idioma oIdioma)
        {

        }

        private void AgregaScriptCliente()
        {
        }


    }
}
