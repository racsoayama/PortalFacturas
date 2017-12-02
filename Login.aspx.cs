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
    public partial class Login : System.Web.UI.Page
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


        protected void LoginButton_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Usuario oUsuario = new Usuario(txtUsuario.Text, txtPassword.Text);
                oUsuario.Cargar();

                ////Si el usuario no existe
                //if (oUsuario.Nombre == "" || oUsuario.Nombre == null)
                //{
                //    //Valida si existe como empleado
                //    Proveedor oEmpleado = new Proveedor();
                //    oEmpleado.Buscar(oUsuario.Id);

                //    //Si existe como empleado
                //    if (oEmpleado.Nombre != "" && oEmpleado.Nombre != null)
                //    {
                //        NegocioPF.Configuracion oConfig = new NegocioPF.Configuracion(oEmpleado.Empresa);
                //        oConfig.Cargar();

                //        //Da de alta al usuario
                //        oUsuario.Id = oEmpleado.Id;
                //        oUsuario.Nombre = oEmpleado.Nombre;
                //        oUsuario.Empresa = oEmpleado.Empresa;
                //        oUsuario.Idioma = ((Idioma)Session["oIdioma"]).Id.ToString();
                //        oUsuario.Empleado = oEmpleado.Id;
                //        oUsuario.Password = "Temporal1";
                //        oUsuario.Perfil = oConfig.PerfilEmpleado;
                //        oUsuario.Pregunta = "";
                //        oUsuario.Respuesta = "";
                //        oUsuario.Status = "ACTIVO";
                //        oUsuario.Guardar("UsuarioPF");
                //        txtPassword.Text = "Temporal1";
                //    }
                //}

                if (txtCaptcha.Text.ToString() != Session["randomStr"].ToString())  
                {
                    txtCaptcha.Text = "";
                    throw new Exception("MsgErrCaptchaIncorrecto");
                } 


                //Si el usuario existe
                if (oUsuario.Nombre != "" && oUsuario.Nombre != null)
                {
                    if (oUsuario.Validar(txtPassword.Text))
                    {
                        oUsuario.Idioma = cboIdioma.SelectedValue;
                        oUsuario.ActualizaIdioma(txtUsuario.Text);

                        //Genera una cookie con el número de usuario
                        HttpCookie aCookie = new HttpCookie("UsuarioPF");
                        aCookie.Value = txtUsuario.Text;
                        aCookie.Expires = DateTime.Now.AddDays(1);
                        Response.Cookies.Add(aCookie);

                        Bitacora.Registra(oUsuario.Id, "Login", "Acceso al sistema");

                        Session["oUsuario"] = oUsuario;

                        Idioma oIdioma = new Idioma(oUsuario.Idioma);
                        Session["oIdioma"] = oIdioma;

                        if (txtPassword.Text == "Temporal1")
                        {
                            btnCambioPassword_Click(sender, e);
                        }
                        else
                        {
                            Response.Redirect("Principal.aspx");
                        }
                    }
                }
                else
                {
                    throw new Exception("UsuarioInexistente");
                }
            }
            catch (Exception ex)
            {
                this.Form.DefaultButton = this.btnAceptar.UniqueID;
                MessageBox(null, null, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }

        protected void btnCambioPassword_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Usuario oUsuario = new Usuario(txtUsuario.Text);
                oUsuario.Cargar();

                txtUsuario2.Text = txtUsuario.Text;

                lblTitulo.Text = ((Idioma)Session["oIdioma"]).Texto("CambioPassword");
                divLogin.Visible = false;
                divCambioPasw.Visible = true;
                this.Form.DefaultButton = this.btnAceptarCambio.UniqueID;

            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }

        protected void btnAceptarCambio_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Usuario oUsuario = new Usuario(txtUsuario.Text, txtPassActual.Text);
                oUsuario.Cargar();

                if (oUsuario.Validar(txtPassActual.Text))
                {
                    oUsuario.Password = txtNuevoPassword.Text;
                    oUsuario.Guardar(txtUsuario.Text);

                    Bitacora.Registra(oUsuario.Id, "Login", "Cambio de password.");

                    if (txtPassActual.Text == "Temporal1" && (oUsuario.Email == ""))
                    {
                        //Pide la captura de los datos personales
                        txtPassword.Text = txtNuevoPassword.Text;
                        divCambioPasw.Visible = false;
                    }
                    else
                    {
                        //Regresa a la pantalla de Login
                        txtPassword.Text = txtNuevoPassword.Text;
                        lblTitulo.Text = ((Idioma)Session["oIdioma"]).Texto("Bienvenido");
                        divLogin.Visible = true;
                        divCambioPasw.Visible = false;
                    }
                    //Response.Redirect("Principal.aspx");
                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("ContraseñaActualizada"));
                }
                else
                {
                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("ContraseñaIncorrecta"));
                }
            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }


        protected void btnCancelarCambio_Click(object sender, ImageClickEventArgs e)
        {
            lblTitulo.Text = ((Idioma)Session["oIdioma"]).Texto("Bienvenido");
            divLogin.Visible = true;
            divCambioPasw.Visible = false;
        }

        protected void btnRecuperaPassword_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Usuario oUsuario = new Usuario(txtUsuario.Text);
                oUsuario.Cargar();

                if (oUsuario.Status == "ACTIVO")
                {
                    EnviaCorreoPassword(oUsuario);
                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgCorrPassEnviado"));
                }
                else
                {
                    throw new Exception("MsgUsuarioInactivo");
                }

            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }

        private void EnviaCorreoPassword(NegocioPF.Usuario oUsuario)
        {
            try
            {
                NegocioPF.Proveedor oProveedor = new NegocioPF.Proveedor(oUsuario.Proveedor);
                oProveedor.Cargar();

                if (oProveedor.eMail != "" || oUsuario.Email != "")
                {
                    string sHtml = "<html>";
                    sHtml += "<table style='font-family:arial;color:navy;font-size:12px; text-align:justify' border='0' width=\"800\">";
                    sHtml += "<tr><td><p>" + ((Idioma)Session["oIdioma"]).Texto("MsgSaludo") + "</p></td></tr>";
                    sHtml += "<tr><td colspan=\"4\"><p>" + ((Idioma)Session["oIdioma"]).Texto("MsgPasswordUsuario") + " " + oUsuario.Password + "</p></td></tr>";
                    sHtml += "<tr><td></td></tr>";
                    sHtml += "<tr><td>" + ((Idioma)Session["oIdioma"]).Texto("Saludos") + "</td></tr>";
                    sHtml += "<tr><td></td></tr>";
                    sHtml += "<tr><td><img src=cid:FirmaPF></td></tr>";
                    sHtml += "</table>";
                    sHtml += "</Html>";

                    EmailTemplate oEmail = new EmailTemplate("");

                    if (oProveedor.eMail != "")
                        oEmail.To.Add(oProveedor.eMail);
                    else
                        oEmail.To.Add(oUsuario.Email);

                    oEmail.From = new MailAddress(@System.Configuration.ConfigurationSettings.AppSettings["EmailFrom"], "PortalFacturas", System.Text.Encoding.UTF8);
                    oEmail.Subject = ((Idioma)Session["oIdioma"]).Texto("RecuperaPassword");

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

        #region NuevaCuenta

        protected void btnCrearCta_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                lblTitulo.Text = ((Idioma)Session["oIdioma"]).Texto("AltaCuenta");
                divLogin.Visible = false;
                divCambioPasw.Visible = false;
                divNuevaCta.Visible = true;
            }
            catch (Exception ex)
            {
            }

        }

        protected void btnAceptarRegistro_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                NegocioPF.Usuario oUsuario = new NegocioPF.Usuario(txtProveedor.Text);
                oUsuario.Cargar();

                NegocioPF.Proveedor oProveedor = new NegocioPF.Proveedor(txtProveedor.Text);
                oProveedor.Cargar();

                NegocioPF.Configuracion oConfig = new NegocioPF.Configuracion();
                oConfig.Cargar();

                if (oUsuario.Nombre != "")
                    throw new Exception("UsuarioProvExistente");
                else
                {
                    if (oProveedor.Nombre == "")
                        throw new Exception("NumProvInexistente");
                    else
                    {
                        if (oProveedor.RFC != txtRFCProv.Text)
                            throw new Exception("RFCIncorrecto");
                        else
                        {
                            oUsuario = new Usuario();
                            oUsuario.Id = txtProveedor.Text;
                            oUsuario.Nombre = oProveedor.Nombre;
                            oUsuario.Perfil = oConfig.PerfilProveedor;
                            oUsuario.Proveedor = txtProveedor.Text;
                            oUsuario.Status = "ACTIVO";
                            oUsuario.Password = NegocioPF.Usuarios.GeneratePassword();
                            oUsuario.Guardar(txtProveedor.Text);

                            //Enviar el correo a la cuenta del proveedor
                            if (oProveedor.eMail != "" && oProveedor.eMail != null)
                            {
                                EnviaCorreoNuevaCuenta(oUsuario);
                                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgCtaCreada"));
                            }
                            else
                            {
                                throw new Exception("MsgUsuarioPwdInicial");
                            }
                        }
                    }
                }
                lblTitulo.Text = ((Idioma)Session["oIdioma"]).Texto("Bienvenido");
                divLogin.Visible = true;
                divCambioPasw.Visible = false;
                divNuevaCta.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }

        }

        protected void btnCancelarRegistro_Click(object sender, ImageClickEventArgs e)
        {
            lblTitulo.Text = ((Idioma)Session["oIdioma"]).Texto("Bienvenido");
            divNuevaCta.Visible = false;
            divLogin.Visible = true;
        }

        private void EnviaCorreoNuevaCuenta(NegocioPF.Usuario oUsuario)
        {
            try
            {
                NegocioPF.Proveedor oProveedor = new NegocioPF.Proveedor(oUsuario.Proveedor);
                oProveedor.Cargar();

                string sHtml = "<html>";
                sHtml += "<table style='font-family:arial;color:navy;font-size:12px; text-align:justify' border='0' width=\"800\">";
                sHtml += "<tr><td><p>" + ((Idioma)Session["oIdioma"]).Texto("MsgSaludo") + "</p></td></tr>";
                sHtml += "<tr><td colspan=\"4\"><p>" + ((Idioma)Session["oIdioma"]).Texto("MsgAltaUsuario") + "</p></td></tr>";
                sHtml += "<tr><td></td></tr>";
                sHtml += "<tr><td colspan=\"4\">" + ((Idioma)Session["oIdioma"]).Texto("Usuario") + ":" + oUsuario.Id + ".</td></tr>";
                sHtml += "<tr><td colspan=\"4\">" + ((Idioma)Session["oIdioma"]).Texto("Contraseña") + ":" + oUsuario.Password + ".</td></tr>";
                sHtml += "<tr><td></td></tr>";
                sHtml += "<tr><td colspan=\"4\">" + ((Idioma)Session["oIdioma"]).Texto("MsgLeyendaLigaPF") + ": " + @System.Configuration.ConfigurationSettings.AppSettings["PF_Link"] + "</td></tr>";
                sHtml += "<tr><td></td></tr>";
                sHtml += "<tr><td>" + ((Idioma)Session["oIdioma"]).Texto("Saludos") + "</td></tr>";
                sHtml += "<tr><td></td></tr>";
                sHtml += "<tr><td><img src=cid:FirmaPF></td></tr>";
                sHtml += "</table>";
                sHtml += "</Html>";

                EmailTemplate oEmail = new EmailTemplate("");

                oEmail.To.Add(oProveedor.eMail);

                oEmail.From = new MailAddress(@System.Configuration.ConfigurationSettings.AppSettings["EmailFrom"], "PortalFacturas", System.Text.Encoding.UTF8);
                oEmail.Subject = ((Idioma)Session["oIdioma"]).Texto("NuevaCuenta");

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
                    throw new Exception("MsgCtaCreErrEnvCorreo");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion NuevaCuenta

        protected void btnVerManual_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Session["archivo"] = "Manuales/Manual.pdf";

                string newWin = "OpenPopupCenter('VisorPDF.aspx','Manual',670,700,0);";

                ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);

            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }

        }

        protected void cboIdioma_SelectedIndexChanged(object sender, EventArgs e)
        {
            Idioma oIdioma = new Idioma(cboIdioma.SelectedValue);
            Session["oIdioma"] = oIdioma;

            EstableceIdioma(oIdioma);

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
            lblTitulo.Text = oIdioma.Texto("Bienvenido");
            lblUsuario.Text = oIdioma.Texto("Usuario") + ":";
            lblPassword.Text = oIdioma.Texto("Contraseña") + ":";
            lblIdioma.Text = oIdioma.Texto("Idioma") + ":";

            lblUsuario2.Text = oIdioma.Texto("Usuario") + ":";
            lblPassNuevo.Text = oIdioma.Texto("ContraseñaNueva") + ":";
            lblPassActual.Text = oIdioma.Texto("ContraseñaActual") + ":";
            lblConfPass.Text = oIdioma.Texto("ConfirmeContraseña") + ":";

            btnCambioPassword.ToolTip = oIdioma.Texto("CambioPassword") + ":";



            //Establece el idioma de los botones
            //if (oIdioma.Id != "es-MX")
            //{
                foreach (Control c in this.Controls)
                {
                    foreach (Control ch in c.Controls)
                    {
                        foreach (Control x in ch.Controls)
                        {
                            if (x is ImageButton)
                            {
                                ((ImageButton)x).ImageUrl = @"~/Images/" + oIdioma.Id + @"/" + ((ImageButton)x).ImageUrl.Substring(((ImageButton)x).ImageUrl.LastIndexOf("/") + 1);
                            }
                        }
                    }
                }
            //}


        }

        private void AgregaScriptCliente()
        {
            try
            {
                string codigo;
                codigo = "function ValidaLogin() { ";
                codigo += "var selectedvalue; ";
                codigo += "obj = document.getElementById('" + txtUsuario.ClientID + "').value; ";
                codigo += "if (obj == null || obj == \"\") { ";
                codigo += "    alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCaptureUsuario") + "\"); ";
                codigo += "    return false; ";
                codigo += "} ";
                codigo += "obj = document.getElementById('" + txtPassword.ClientID + "').value; ";
                codigo += "if (obj == null || obj == \"\") { ";
                codigo += "    alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCapturePassword") + "\"); ";
                codigo += "    return false; ";
                codigo += "} ";
                codigo += "obj = document.getElementById('" + txtCaptcha.ClientID + "').value; ";
                codigo += "if (obj == null || obj == \"\") { ";
                codigo += "    alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCaptureCaptcha") + "\"); ";
                codigo += "    return false; ";
                codigo += "} ";
                codigo += "return true; ";
                codigo += "} ";

                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ValidaLogin", codigo, true);

                codigo = "function ValidaCambio() { ";
                codigo += "  obj = document.getElementById('" + txtPassActual.ClientID + "').value; ";
                codigo += "  if (obj == null || obj == \"\") { ";
                codigo += "    alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCapturePassActual") + "\"); ";
                codigo += "    return false; ";
                codigo += "  } ";
                codigo += "  obj2 = document.getElementById('" + txtNuevoPassword.ClientID + "').value; ";
                codigo += "  if (obj2 == null || obj2 == \"\") { ";
                codigo += "    alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCaptureNuevoPassword") + "\"); ";
                codigo += "    return false; ";
                codigo += "  } ";
                codigo += "  obj3 = document.getElementById('" + txtConfPassword.ClientID + "').value; ";
                codigo += "  if (obj3 == null || obj3 == \"\") { ";
                codigo += "    alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCapConfPassword") + "\"); ";
                codigo += "    return false; ";
                codigo += "  } ";
                codigo += "  if (obj2 != obj3) { ";
                codigo += "    alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgConfPassIncorrecta") + "\"); ";
                codigo += "    return false; ";
                codigo += "  } ";

                codigo += "  if (obj == obj2) { ";
                codigo += "    alert(\"" + ((Idioma)Session["oIdioma"]).Texto("ContraseñaIgualAnterior") + "\"); ";
                codigo += "    return false; ";
                codigo += "  } else { ";
                codigo += "    if (obj2.length < 8)  { ";
                codigo += "      alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgContraseñaLongMenor") + "\"); ";
                codigo += "      return false; ";
                codigo += "    } else { ";
                codigo += "      if (!hasNumbers(obj2)) { ";
                codigo += "        alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgContraseñaSinNumeros") + "\"); ";
                codigo += "        return false; ";
                codigo += "      } else { ";
                codigo += "        if (!hasLetters(obj2)) { ";
                codigo += "           alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgContraseñaSinLetras") + "\"); ";
                codigo += "           return false; ";
                codigo += "        } ";
                codigo += "      } ";
                codigo += "    } ";
                codigo += "  } ";
                codigo += "return true; ";
                codigo += "} ";

                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ValidaCambio", codigo, true);


                codigo = "function ValidaDatosRegistro() { ";
                codigo += "  obj = document.getElementById('" + txtProveedor.ClientID + "').value; ";
                codigo += "  if (obj == null || obj == \"\") { ";
                codigo += "    alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCapNumProveedor") + "\"); ";
                codigo += "    return false; ";
                codigo += "  } ";
                codigo += "  obj = document.getElementById('" + txtRFCProv.ClientID + "').value; ";
                codigo += "  if (obj == null || obj == \"\") { ";
                codigo += "    alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCapRFCProv") + "\"); ";
                codigo += "    return false; ";
                codigo += "  } ";
                //codigo += "  obj = document.getElementById('" + txtNIP.ClientID + "').value; ";
                //codigo += "  if (obj == null || obj == \"\") { ";
                //codigo += "    alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCapNIP") + "\"); ";
                //codigo += "    return false; ";
                //codigo += "  } ";
                codigo += "return true; ";
                codigo += "} ";

                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ValidaDatosRegistro", codigo, true);


            }
            catch (Exception ex)
            {
                MessageBox(this, null, ex.Message);
            }
        }


    }
}
