using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using NegocioPF;

namespace PortalFacturas
{
    public partial class Contacto : FormaPadre
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ValidaVariables();
                    EstableceIdioma((Idioma)Session["oIdioma"]);

                }
                catch (Exception ex)
                {
                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
                }
            }
            AgregaScriptCliente();
        }

        protected void btnEnviar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ValidaVariables();

                NegocioPF.Contacto oContacto = new NegocioPF.Contacto();

                oContacto.Nombre = txtNombre.Text;
                oContacto.Correo = txtMail.Text;
                oContacto.Mensaje = txtMensaje.Text;
                oContacto.Guardar(((Usuario)Session["oUsuario"]).Id);

                try
                {
                    EnviaCorreo();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgMsgRecibido"));
            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }

        private void EnviaCorreo()
        {
            try
            {
                NegocioPF.Configuracion oConfig = new NegocioPF.Configuracion();
                oConfig.Cargar();

                if (oConfig.MailContacto != "" || oConfig.MailContacto != "")
                {
                    string sHtml = "<html>";
                    sHtml += "<table style='font-family:arial;color:navy;font-size:12px; text-align:justify' border='0' width=\"800\">";
                    sHtml += "<tr><td><p>" + ((Idioma)Session["oIdioma"]).Texto("MsgSaludo") + "</p></td></tr>";
                    sHtml += "<tr><td colspan=\"4\"><p>" + ((Idioma)Session["oIdioma"]).Texto("MsgMsgContRecibido") + "</p></td></tr>";
                    sHtml += "<tr><td></td></tr>";
                    sHtml += "<tr><td>" + ((Idioma)Session["oIdioma"]).Texto("Nombre") + ": " + txtNombre.Text + "</td></tr>";
                    sHtml += "<tr><td></td></tr>";
                    sHtml += "<tr><td>" + ((Idioma)Session["oIdioma"]).Texto("Email") + ": " + txtMail.Text + "</td></tr>";
                    sHtml += "<tr><td></td></tr>";
                    sHtml += "<tr><td>" + ((Idioma)Session["oIdioma"]).Texto("Mensaje") + ": " + txtMensaje.Text + "</td></tr>";
                    sHtml += "<tr><td></td></tr>";
                    sHtml += "<tr><td>" + ((Idioma)Session["oIdioma"]).Texto("Saludos") + "</td></tr>";
                    sHtml += "<tr><td></td></tr>";
                    sHtml += "<tr><td><img src=cid:FirmaPF></td></tr>";
                    sHtml += "</table>";
                    sHtml += "</Html>";

                    EmailTemplate oEmail = new EmailTemplate("");

                    oEmail.To.Add(oConfig.MailContacto);

                    oEmail.From = new MailAddress(@System.Configuration.ConfigurationSettings.AppSettings["EmailFrom"], "PortalFacturas", System.Text.Encoding.UTF8);
                    oEmail.Subject = ((Idioma)Session["oIdioma"]).Texto("MensajeProveedor");

                    //Agrega Logo
                    AlternateView altView = AlternateView.CreateAlternateViewFromString(sHtml, null, MediaTypeNames.Text.Html);

                    string imageSource = (Server.MapPath("") + "\\Images\\FirmaPF.jpg");
                    LinkedResource PictureRes = new LinkedResource(imageSource, MediaTypeNames.Image.Jpeg);
                    PictureRes.ContentId = "FirmaPF";
                    altView.LinkedResources.Add(PictureRes);

                    oEmail.AlternateViews.Add(altView);

                    try
                    {
                        oEmail.Send();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("MsgErrorEnvioCorreo");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Principal.aspx");
        }


        private void EstableceIdioma(Idioma oIdioma)
        {
            lblLeyTitulo.Text = oIdioma.Texto("Contacto");
            lblNombre.Text = oIdioma.Texto("Nombre") + ":";
            lblMail.Text = oIdioma.Texto("Email") + ":";
            lblMensaje.Text = oIdioma.Texto("Mensaje") + ":";
            valMensaje.ErrorMessage = oIdioma.Texto("MsgTxtMenor300Chr");

            EstableceIdiomaBotones(oIdioma.Id, this.Controls);
        }

        private void AgregaScriptCliente()
        {
            try
            {
                string codigo;
                codigo = "function ValidaDatos() { ";
                codigo += "obj = document.getElementById('" + txtNombre.ClientID + "').value; ";
                codigo += "if (obj == null || obj == \"\") { ";
                codigo += "    alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCapSuNombre") + "\"); ";
                codigo += "    return false; ";
                codigo += "} ";
                codigo += "obj = document.getElementById('" + txtMail.ClientID + "').value; ";
                codigo += "if (obj == null || obj == \"\") { ";
                codigo += "    alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCapDirCorreo") + "\"); ";
                codigo += "    return false; ";
                codigo += "} ";
                codigo += "obj = document.getElementById('" + txtMensaje.ClientID + "').value; ";
                codigo += "if (obj == null || obj == \"\") { ";
                codigo += "    alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCapMensaje") + "\"); ";
                codigo += "    return false; ";
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
