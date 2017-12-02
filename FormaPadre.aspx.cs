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
using System.Data;
using NegocioPF;

namespace PortalFacturas
{
    public partial class FormaPadre : System.Web.UI.Page
    {
        protected void MessageBox(object sender, EventArgs e, string strMensaje)
        {
            // Do something
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "redirect script",
            "alert('" + strMensaje + "'); ", true);

            //location.href='pagina.aspx';
        }

        protected void ValidaVariables()
        {
            string usuario = "";
            try
            {
                NegocioPF.Usuario oUsuario = new Usuario();
                try
                {
                    oUsuario = (Usuario)Session["oUsuario"];
                }
                catch { /*Maneja el error*/ }

                if (oUsuario == null || oUsuario.Id == "")
                {
                    if (Request.Cookies["UsuarioPF"] != null)
                    {
                        if (Request.Cookies["UsuarioPF"].Value != null)
                            usuario = Request.Cookies["UsuarioPF"].Value;
                    }

                    oUsuario = new Usuario(usuario);
                    oUsuario.Cargar();
                    Session["oUsuario"] = oUsuario;
                }

                Idioma oIdioma = new Idioma("");
                try
                {
                    oIdioma = (Idioma)Session["oIdioma"];
                }
                catch { /*Maneja el error*/ }

                if (oIdioma == null || oIdioma.Textos.Name == "")
                {
                    oIdioma = new Idioma(oUsuario.Idioma);
                    Session["oIdioma"] = oIdioma;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void EstableceIdiomaBotones(string idioma, ControlCollection controls)
        {
            if (idioma != "es-MX")
            {
                foreach (Control c in controls)
                {
                    if (c is ImageButton)
                    {
                        ((ImageButton)c).ImageUrl = ((ImageButton)c).ImageUrl.Substring(0, ((ImageButton)c).ImageUrl.LastIndexOf("/")) + @"/" + idioma + @"/" + ((ImageButton)c).ImageUrl.Substring(((ImageButton)c).ImageUrl.LastIndexOf("/") + 1);
                    }
                    else if (c.Controls.Count > 0)
                    {
                        EstableceIdiomaBotones(idioma, c.Controls);
                    }
                }
            }

        }

        public static DateTime ConvierteTextToFecha(string fecha)
        {
            try
            {
                return new DateTime(Convert.ToInt32(fecha.Substring(6, 4)), Convert.ToInt32(fecha.Substring(3, 2)), Convert.ToInt32(fecha.Substring(0, 2)));
            }
            catch
            {
                throw new Exception("MsgFecFormIncorrecto");
            }
        }

    }
}
