using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net.Mime;
using NegocioPF;

namespace PortalFacturas
{
    public partial class Usuarios : FormaPadre
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ValidaVariables();
                    EstableceIdioma((Idioma)Session["oIdioma"]);

                    Catalogo oCatalogo = new Catalogo();
                    oCatalogo.ID = "STATUS_USU";
                    oCatalogo.Cargar(((Usuario)Session["oUsuario"]).Idioma);
                    cboStatus.DataSource = oCatalogo.Datos;
                    cboStatus.DataTextField = "descripcion";
                    cboStatus.DataValueField = "id_valor";
                    cboStatus.DataBind();
                    cboStatus.SelectedValue = "ACTIVO";

                    cboFilStatus.DataSource = oCatalogo.Datos;
                    cboFilStatus.DataTextField = "descripcion";
                    cboFilStatus.DataValueField = "id_valor";
                    cboFilStatus.DataBind();
                    cboFilStatus.Items.Insert(0, new ListItem(((Idioma)Session["oIdioma"]).Texto("Todos") + " ...", "0"));
                    cboFilStatus.SelectedValue = "0";

                    //Tipos de empleado 
                    oCatalogo.ID = "TIPOUSER";
                    oCatalogo.Cargar(((Usuario)Session["oUsuario"]).Idioma);
                    cboTipoUsuario.DataSource = oCatalogo.Datos;
                    cboTipoUsuario.DataTextField = "descripcion";
                    cboTipoUsuario.DataValueField = "id_valor";
                    cboTipoUsuario.DataBind();
                    cboTipoUsuario.SelectedValue = "1";
                    lblProveedor.Visible = false;
                    txtProveedor.Visible = false;

                    //Carga lista de usuarios
                    NegocioPF.Usuarios oUsuarios = new NegocioPF.Usuarios();
                    oUsuarios.Cargar(txtFilNumero.Text, txtFilNombre.Text, cboFilPerfil.SelectedValue, cboFilStatus.SelectedValue);
                    grdUsuarios.DataSource = oUsuarios.Datos;
                    grdUsuarios.DataBind();

                    //Llena combo de perfiles
                    NegocioPF.Perfiles oPerfiles = new NegocioPF.Perfiles();
                    oPerfiles.Cargar();
                    cboPerfiles.DataSource = oPerfiles.Datos;
                    cboPerfiles.DataTextField = "Nombre";
                    cboPerfiles.DataValueField = "id_perfil";
                    cboPerfiles.DataBind();
                    cboPerfiles.Items.Insert(0, new ListItem(((Idioma)Session["oIdioma"]).Texto("Seleccionar") + " ...", "0"));

                    cboFilPerfil.DataSource = oPerfiles.Datos;
                    cboFilPerfil.DataTextField = "Nombre";
                    cboFilPerfil.DataValueField = "id_perfil";
                    cboFilPerfil.DataBind();
                    cboFilPerfil.Items.Insert(0, new ListItem(((Idioma)Session["oIdioma"]).Texto("Seleccionar") + " ...", "0"));

                    //Establece los permisos
                    Perfil oPerfil = new Perfil();
                    Permisos permisos = oPerfil.CargarPermisos(((Usuario)Session["oUsuario"]).Id, "Usuarios.aspx");
                    grdUsuarios.Columns[3].Visible = permisos.Alta;
                    grdUsuarios.Columns[4].Visible = permisos.Edicion;
                    grdUsuarios.Columns[5].Visible = permisos.Baja;

                    divDetalle.Visible = (oUsuarios.Datos.Tables[0].Rows.Count == 0);

                }
                catch (Exception ex)
                {
                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
                }

            }
            AgregaScriptCliente();
        }

        protected void grdUsuarios_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void grdUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                ValidaVariables();

                grdUsuarios.PageIndex = e.NewPageIndex;

                NegocioPF.Usuarios oUsuarios = new NegocioPF.Usuarios();
                oUsuarios.Cargar(txtFilNumero.Text, txtFilNombre.Text, cboFilPerfil.SelectedValue, cboFilStatus.SelectedValue);
                grdUsuarios.DataSource = oUsuarios.Datos;
                grdUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }

        protected void cboTipoUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Si es una cuenta de operador del sistema
            if (cboTipoUsuario.SelectedValue == "1")
            {
                lblProveedor.Visible = false;
                txtProveedor.Visible = false;
                lblCorreo.Visible = true;
                txtCorreo.Visible = true;
            }
            else
            {
                lblProveedor.Visible = true;
                txtProveedor.Visible = true;
                lblCorreo.Visible = false;
                txtCorreo.Visible = false;
                
            }
        }

        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ValidaVariables();
                //Carga relación de usuarios
                NegocioPF.Usuarios oUsuarios = new NegocioPF.Usuarios();
                oUsuarios.Cargar(txtFilNumero.Text, txtFilNombre.Text, cboFilPerfil.SelectedValue, cboFilStatus.SelectedValue);
                grdUsuarios.DataSource = oUsuarios.Datos;
                grdUsuarios.DataBind();

            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }

        protected void btnMostrarTodos_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ValidaVariables();
                txtFilNumero.Text = "";
                txtFilNombre.Text = "";
                NegocioPF.Usuarios oUsuarios = new NegocioPF.Usuarios();
                oUsuarios.Cargar();
                grdUsuarios.DataSource = oUsuarios.Datos;
                grdUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }


        //protected void cboPlantas_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        ValidaVariables();
        //        NegocioPF.Perfiles oPerfiles = new NegocioPF.Perfiles();
        //        oPerfiles.Cargar();
        //        cboPerfiles.DataSource = oPerfiles.Datos;
        //        cboPerfiles.DataTextField = "Nombre";
        //        cboPerfiles.DataValueField = "id_perfil";
        //        cboPerfiles.DataBind();
        //        cboPerfiles.Items.Insert(0, new ListItem(((Idioma)Session["oIdioma"]).Texto("Seleccionar") + " ...", "0"));

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
        //    }
        //}

        protected void btnNuevo_Command(object sender, CommandEventArgs e)
        {
            try
            {
                txtUsuario.Text = "";
                txtNombre.Text = "";
                //txtPassword.Text = "";
                //txtPassword.Enabled = false;
                txtProveedor.Text = "";
                cboPerfiles.SelectedValue = "0";
                txtCorreo.Text = "";
                cboStatus.SelectedValue = "ACTIVO";
                txtUsuario.Enabled = true;
                divDetalle.Visible = true;
                Session["accion"] = "Nuevo";
                divFiltros.Visible = false;
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
                int index = Convert.ToInt32(e.CommandArgument) - (grdUsuarios.PageIndex * grdUsuarios.PageSize);

                //Carga los datos del usuario
                Usuario oUsuario = new Usuario(grdUsuarios.DataKeys[index].Value.ToString());
                oUsuario.Cargar();

                //cboPlantas.SelectedValue = ((Usuario)Session["oUsuario"]).Empresa.ToString();

                //Carga los perfiles de la planta
                NegocioPF.Perfiles oPerfiles = new NegocioPF.Perfiles();
                oPerfiles.Cargar();
                cboPerfiles.DataSource = oPerfiles.Datos;
                cboPerfiles.DataTextField = "Nombre";
                cboPerfiles.DataValueField = "id_perfil";
                cboPerfiles.DataBind();
                cboPerfiles.Items.Insert(0, new ListItem(((Idioma)Session["oIdioma"]).Texto("Seleccionar") + " ...", "0"));

                //Muestra los datos en los controles
                txtUsuario.Text = oUsuario.Id;
                txtNombre.Text = oUsuario.Nombre;
                cboPerfiles.SelectedValue = oUsuario.Perfil.ToString();
                txtCorreo.Text = oUsuario.Email;
                //txtPassword.Text = oUsuario.Password;
                //txtProveedor.Text = oUsuario.Proveedor;
                cboStatus.SelectedValue = oUsuario.Status;

                txtUsuario.Enabled = false;
                //txtPassword.Enabled = true;
                divDetalle.Visible = true;
                divFiltros.Visible = false;
                Session["accion"] = "Edicion";
            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }

        
        protected void btnEliminaUsuario_Command(object sender, CommandEventArgs e)
        {
            try
            {
                ValidaVariables();

                if (e.CommandName == "Eliminar")
                {
                    //Obtiene indice de la linea a actualizar
                    int index = Convert.ToInt32(e.CommandArgument) - (grdUsuarios.PageIndex * grdUsuarios.PageSize);

                    //Carga la información a eliminar
                    Usuario oUsuario = new Usuario(grdUsuarios.DataKeys[index].Value.ToString());
                    oUsuario.Eliminar(((Usuario)Session["oUsuario"]).Id);

                    NegocioPF.Usuarios oUsuarios = new NegocioPF.Usuarios();
                    oUsuarios.Cargar(txtFilNumero.Text, txtFilNombre.Text, cboFilPerfil.SelectedValue, cboFilStatus.SelectedValue);
                    grdUsuarios.DataSource = oUsuarios.Datos;
                    grdUsuarios.DataBind();

                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgUsuarioEliminado"));
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }

        protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
        {
            NegocioPF.Usuario oUsuario;
            
            try
            {
                ValidaVariables();

                //Valida si existe otro usuario con el mismo User
                oUsuario = new NegocioPF.Usuario(txtUsuario.Text);
                oUsuario.Id = txtUsuario.Text;

                oUsuario.Cargar();

                if (oUsuario.Nombre != "" && oUsuario.Nombre != null && Session["accion"].ToString() == "Nuevo")
                    throw new Exception("MsgUsuarioExistente");

                oUsuario.Nombre = txtNombre.Text;
                oUsuario.Perfil = Convert.ToInt32(cboPerfiles.SelectedValue);
                oUsuario.Email = txtCorreo.Text;
                oUsuario.Proveedor = txtProveedor.Text;
                oUsuario.Idioma = "";
                oUsuario.Status = cboStatus.SelectedValue;

                //Verifica si el user es el Id de un proveedor
                NegocioPF.Proveedor oProveedor = new NegocioPF.Proveedor(txtUsuario.Text);
                oProveedor.Cargar();
                if (oProveedor.Nombre != "" && oProveedor.Nombre != null)
                    oUsuario.Email = oProveedor.eMail;

                //Si es un user para un proveedor, valida el perfil asignado
                if (oProveedor.Nombre != "" && oProveedor.Nombre != null)
                {
                    NegocioPF.Configuracion oConfig = new NegocioPF.Configuracion();
                    oConfig.Cargar();
                    if (oConfig.PerfilProveedor != Convert.ToInt32(cboPerfiles.SelectedValue))
                        throw new Exception("MsgPerfProvIncorrecto");
                }

                //Valida si existe el proveedor
                if (txtProveedor.Text != "")
                {
                    oProveedor = new NegocioPF.Proveedor(txtProveedor.Text);
                    oProveedor.Cargar();
                    if (oProveedor.Nombre == "" || oProveedor.Nombre == null)
                        throw new Exception("MsgProvInexistente");
                }

                //Si es un usuario nuevo, genera el password
                if (Session["accion"].ToString() == "Nuevo")
                    oUsuario.Password = NegocioPF.Usuarios.GeneratePassword();

                //oUsuario = new NegocioPF.Usuario(txtUsuario.Text, 
                //                                    txtNombre.Text,
                //                                    Convert.ToInt32(cboPerfiles.SelectedValue), 
                //                                    correo,
                //                                    password, 
                //                                    txtProveedor.Text, 
                //                                    "", 
                //                                    cboStatus.SelectedValue);

                oUsuario.Guardar(((Usuario)Session["oUsuario"]).Id);

                NegocioPF.Usuarios oUsuarios = new NegocioPF.Usuarios();
                oUsuarios.Cargar(txtFilNumero.Text, txtFilNombre.Text, cboFilPerfil.SelectedValue, cboFilStatus.SelectedValue);
                grdUsuarios.DataSource = oUsuarios.Datos;
                grdUsuarios.DataBind();

                divDetalle.Visible = false;
                divFiltros.Visible = true;

                if (Session["accion"].ToString() == "Nuevo" && oUsuario.Email != "")
                {
                    EnviaCorreoUsuario(txtUsuario.Text);
                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgCtaCreada"));
                }
                else
                {
                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgUsuarioGuardado"));
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Length > 3)
                {
                    if (ex.Message.Substring(0, 3) == "Msg")
                        MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
                    else
                        MessageBox(sender, e, ex.Message);
                }
            }
        }

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ValidaVariables();

                txtUsuario.Text = "";
                txtNombre.Text = "";
                //cboPerfiles.SelectedValue = "1";
                //txtPassword.Text = "";
                divDetalle.Visible = false;
                divFiltros.Visible = true;

                NegocioPF.Usuarios oUsuarios = new NegocioPF.Usuarios();
                oUsuarios.Cargar(txtFilNumero.Text, txtFilNombre.Text, cboFilPerfil.SelectedValue, cboFilStatus.SelectedValue);
                grdUsuarios.DataSource = oUsuarios.Datos;
                grdUsuarios.DataBind();

            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }

        private void EnviaCorreoUsuario(string usuario)
        {
            try
            {
                //Usuario
                Usuario oUsuario = new Usuario(usuario);
                oUsuario.Cargar();

                string sHtml = "<html>";
                sHtml += "<table style='font-family:arial;color:navy;font-size:12px; text-align:justify' border='0' width=\"800\">";
                sHtml += "<tr><td><p>" + ((Idioma)Session["oIdioma"]).Texto("MsgSaludo") + "</p></td></tr>";
                sHtml += "<tr><td colspan=\"4\"><p>" + ((Idioma)Session["oIdioma"]).Texto("MsgAltaUsuario") + "</p></td></tr>";
                sHtml += "<tr><td></td></tr>";
                sHtml += "<tr><td colspan=\"4\">" + ((Idioma)Session["oIdioma"]).Texto("Usuario") + ": " + oUsuario.Id + "</td></tr>";
                sHtml += "<tr><td colspan=\"4\">" + ((Idioma)Session["oIdioma"]).Texto("Contraseña") + ": " + oUsuario.Password + "</td></tr>";
                sHtml += "<tr><td></td></tr>";
                sHtml += "<tr><td colspan=\"4\">" + ((Idioma)Session["oIdioma"]).Texto("MsgLeyendaLigaPF") + ": " + @System.Configuration.ConfigurationSettings.AppSettings["PF_Link"] + "</td></tr>";
                sHtml += "<tr><td></td></tr>";
                sHtml += "<tr><td>" + ((Idioma)Session["oIdioma"]).Texto("Saludos") + "</td></tr>";
                sHtml += "<tr><td></td></tr>";
                sHtml += "<tr><td><img src=cid:FirmaPF></td></tr>";
                sHtml += "</table>";
                sHtml += "</Html>";

                EmailTemplate oEmail = new EmailTemplate("");

                if (oUsuario.Email != "")
                {
                    oEmail.To.Add(oUsuario.Email);

                    oEmail.From = new MailAddress(@System.Configuration.ConfigurationSettings.AppSettings["EmailFrom"], "Portal de facturación", System.Text.Encoding.UTF8);
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
                    catch
                    {
                        throw new Exception("MsgCtaCreErrEnvCorreo");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void EstableceIdioma(Idioma oIdioma)
        {
            lblLeyendaUsuario.Text = oIdioma.Texto("Usuarios");
            lblLeyUsuario.Text = oIdioma.Texto("Usuario");
            lblFilNumero.Text = oIdioma.Texto("UsuarioID") + ":";
            lblFilNombre.Text = oIdioma.Texto("Nombre") + ":";
            lblFilPerfil.Text = oIdioma.Texto("Perfil") + ":";
            lblFilStatus.Text = oIdioma.Texto("Status") + ":";
            lblTipoUsuario.Text = oIdioma.Texto("TipoUsuario") + ":";
            lblUsuario.Text = oIdioma.Texto("UsuarioID") + ":";
            lblNombre.Text = oIdioma.Texto("Nombre") + ":";
            lblProveedor.Text = oIdioma.Texto("Proveedor")+ ":";
            lblPerfil.Text = oIdioma.Texto("Perfil")+ ":";
            lblCorreo.Text = oIdioma.Texto("Correo") + ":";
            lblStatus.Text = oIdioma.Texto("Status") + ":";

            foreach (DataControlField c in grdUsuarios.Columns)
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
                codigo += "objUsuario = document.getElementById('" + txtUsuario.ClientID + "').value; ";
                codigo += "if (objUsuario == null || objUsuario == \"\") { ";
                codigo += "    alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCaptureUserID") + "\"); ";
                codigo += " return false; ";
                codigo += "} ";
                codigo += "obj = document.getElementById('" + txtNombre.ClientID + "').value; ";
                codigo += "if (obj == null || obj == \"\") { ";
                codigo += "    alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCaptureNombreUsuario") + "\"); ";
                codigo += "    return false; ";
                codigo += "} ";
                codigo += "var1 = document.getElementById('" + txtProveedor.ClientID + "'); ";
                codigo += "if (var1 != null) { ";
                codigo += "  obj = document.getElementById('" + txtProveedor.ClientID + "').value; ";
                codigo += "  if ((obj == null || obj == \"\") && objUsuario != 'ADMIN') { ";
                codigo += "    alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCaptureNumeroEmpleado") + "\"); ";
                codigo += "    return false; ";
                codigo += "  } ";
                codigo += "} ";
                codigo += "var1 = document.getElementById('" + txtCorreo.ClientID + "'); ";
                codigo += "if (var1 != null) { ";
                codigo += "  obj = document.getElementById('" + txtCorreo.ClientID + "').value; ";
                codigo += "  if (obj == null || obj == \"\")  { ";
                codigo += "     alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCaptureCorreo") + "\"); ";
                codigo += "     return false; ";
                codigo += "  } ";
                codigo += "} ";
                codigo += "obj = document.getElementById('" + cboPerfiles.ClientID + "').value; ";
                codigo += "if (obj == null || obj == \"0\") { ";
                codigo += "    alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgSeleccionePerfil") + "\"); ";
                codigo += "    return false; ";
                codigo += "} ";

                //codigo += "obj = document.getElementById('" + txtPassword.ClientID + "').value; ";
                //codigo += "if (obj == null || obj == \"\") { ";
                //codigo += "    alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCaptureContraseña") + "\"); ";
                //codigo += "    return false; ";
                //codigo += "} else { ";
                //codigo += "  if (obj.length < 8)  { ";
                //codigo += "    alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgContraseñaLongMenor") + "\"); ";
                //codigo += "    return false; ";
                //codigo += "  } else { ";
                //codigo += "    if (!hasNumbers(obj)) { ";
                //codigo += "    alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgContraseñaSinNumeros") + "\"); ";
                //codigo += "      return false; ";
                //codigo += "    } else { ";
                //codigo += "       if (!hasLetters(obj)) { ";
                //codigo += "          alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgContraseñaSinLetras") + "\"); ";
                //codigo += "          return false; ";
                //codigo += "       } ";
                //codigo += "    } ";
                //codigo += "  } ";
                //codigo += "} ";
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
