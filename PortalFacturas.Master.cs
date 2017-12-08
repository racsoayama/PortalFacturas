using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using NegocioPF;

namespace PortalFacturas
{
    public partial class PortalFacturas : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string usuario = "";
            if (Request.Cookies["UsuarioPF"] != null)
            {
                if (Request.Cookies["UsuarioPF"].Value != null)
                    usuario = Request.Cookies["UsuarioPF"].Value;
            }

            //System.Security.Principal.IPrincipal User = HttpContext.Current.User;

            Funciones oMenu = new Funciones();
            oMenu.CargarMenu(usuario); //User.Identity.Name.Substring(User.Identity.Name.IndexOf("\\") + 1));

            NegocioPF.Usuario oUsuario = new NegocioPF.Usuario();
            try
            {
                oUsuario = (Usuario)Session["oUsuario"];
            }
            catch { /*Maneja el error*/ }
            if (oUsuario == null || oUsuario.Id == "")
            {
                oUsuario = new Usuario(usuario); //User.Identity.Name.Substring(User.Identity.Name.IndexOf("\\") + 1));
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

            //Establece la imagen de logo del portal
            imgLogo.Attributes["src"] = "~/images/" + oUsuario.Idioma + "/" + "LogoPF.png";

            //Carga al proveedor
            NegocioPF.Proveedor oProveedor = new NegocioPF.Proveedor(((Usuario)Session["oUsuario"]).Id);
            oProveedor.Cargar();

            lista.Controls.Clear();
            foreach (DataRow r in oMenu.Datos.Tables[0].Rows)
            {
                if (Convert.ToInt32(r["id_parent"]) == 0)
                {
                    HtmlGenericControl li = new HtmlGenericControl("li");
                    li.Attributes.Add("class", "has-sub");
                    lista.Controls.Add(li);

                    HtmlGenericControl anchor = new HtmlGenericControl("a");
                    anchor.Attributes.Add("href", "#");
                    //anchor.InnerText = oIdioma.Texto(r["Descripcion"].ToString());
                    anchor.InnerHtml = "<spam>" + oIdioma.Texto(r["Descripcion"].ToString()) + "</spam>";

                    li.Controls.Add(anchor);

                    AddChildItems(oMenu.Datos.Tables[0], li, Convert.ToInt32(r["id_funcion"]), ref oIdioma);
                }
            }

            HtmlGenericControl li2;
            HtmlGenericControl anchor1;

            //Si es un proveedor, agrega la opción del Contacto
            if (oProveedor.Nombre != "" && oProveedor.Nombre != null)
            {
                li2 = new HtmlGenericControl("li");
                li2.Attributes.Add("class", "dropdown");
                lista.Controls.Add(li2);

                anchor1 = new HtmlGenericControl("a");
                anchor1.Attributes.Add("href", "#");
                //anchor1.Attributes.Add("href", "Contacto.aspx");
                anchor1.Attributes.Add("onclick", "OpenPopupCenter('Contacto1.aspx','Contacto',390,300,0);");
                anchor1.InnerHtml = "<spam>" + oIdioma.Texto("Contactanos") + "</spam>";
                li2.Controls.Add(anchor1);
            }

            //Agrega la opción del Manual, dependiendo de si es usuario o proveedor
            li2 = new HtmlGenericControl("li");
            li2.Attributes.Add("class", "dropdown");
            lista.Controls.Add(li2);

            anchor1 = new HtmlGenericControl("a");
            anchor1.Attributes.Add("href", "#");

            if (oProveedor.Nombre != "" && oProveedor.Nombre != null)
                Session["archivo"] = "Manuales/ManualProv.pdf";
            else
                Session["archivo"] = "Manuales/ManualUsuario.pdf";

            anchor1.Attributes.Add("onclick", "OpenPopupCenter('VisorPDF.aspx','Manual',670,700,0);");
            anchor1.InnerHtml = "<spam>" + oIdioma.Texto("ManualUsuario") + "</spam>";
            li2.Controls.Add(anchor1);

            //anchor.Attributes.Add("href", (Convert.ToInt32(r["hijos"]) == 0 ? r["url"].ToString() : "#"));

            NegocioPF.Configuracion oConfig = new NegocioPF.Configuracion();
            oConfig.Cargar();
            marMensaje.InnerText = oConfig.Mensaje;

        }

        private static void AddChildItems(DataTable table, HtmlGenericControl li, int id_parent, ref Idioma oIdioma)
        {

            HtmlGenericControl ul = new HtmlGenericControl("ul");
            ul.Attributes.Add("class", "dropdown");
            li.Controls.Add(ul);

            foreach (DataRow r in table.Rows)
            {
                if (Convert.ToInt32(r["id_parent"]) == id_parent)
                {
                    HtmlGenericControl li2 = new HtmlGenericControl("li");
                    ul.Controls.Add(li2);

                    HtmlGenericControl anchor = new HtmlGenericControl("a");
                    if (Convert.ToInt32(r["hijos"]) == 0)
                        anchor.Attributes.Add("href", (Convert.ToInt32(r["hijos"]) == 0 ? r["url"].ToString() : "#"));
                    //anchor.InnerText = oIdioma.Texto(r["descripcion"].ToString());
                    anchor.InnerHtml = "<spam>" + oIdioma.Texto(r["Descripcion"].ToString()) + "</spam>";

                    li2.Controls.Add(anchor);

                    if (Convert.ToInt32(r["hijos"]) > 0)
                    {
                        li2.Attributes.Add("class", "has-sub");
                        AddChildItems(table, li2, Convert.ToInt32(r["id_funcion"]), ref oIdioma);
                    }
                }
            }
        }

        public static void AbandonSession()
        {
            HttpContext.Current.Session.Abandon();
        }

        protected void btnlogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();

            Session.Contents.RemoveAll();

            System.Web.Security.FormsAuthentication.SignOut();

            Response.Redirect("../Login.aspx");
        }

        public string GetCurrentCulture
        {
            get
            {
                return System.Threading.Thread.CurrentThread.CurrentCulture.Name.ToString();
            }
        }


    }
}