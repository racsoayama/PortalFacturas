using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using NegocioPF;

namespace PortalFacturas
{
    public partial class ImpResPark : FormaPadre
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

        protected void btnAceptarLog_Click(object sender, ImageClickEventArgs e)
        {
            int registros = 0;
            try
            {
                ValidaVariables();

                if ((File1.PostedFile != null) && (File1.PostedFile.ContentLength > 0))
                {
                    string fn = System.IO.Path.GetFileName(File1.PostedFile.FileName);

                    string SaveLocation = Server.MapPath("") + "\\Data\\FACTLOGISTICA_LOG." + fn.Substring(fn.Length - 3, 3);

                    try
                    {
                        File1.PostedFile.SaveAs(SaveLocation);
                    }
                    catch (Exception ex)
                    {
                        MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgErrorCopiarArchivo"));
                    }

                    try
                    {
                        NegocioPF.Facturas oFacturas = new NegocioPF.Facturas();

                        registros = oFacturas.ImportarResLogTXT(((Usuario)Session["oUsuario"]).Id, SaveLocation);

                        MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgRegActInsertados") + registros.ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox(sender, e, "Error:" + ((Idioma)Session["oIdioma"]).Texto(ex.Message));
                    }
                }
                else
                {
                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgSeleccioneArchivo"));
                }
            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }

        protected void btnAceptarFin_Click(object sender, ImageClickEventArgs e)
        {
            int registros = 0;
            try
            {
                ValidaVariables();

                if ((File2.PostedFile != null) && (File2.PostedFile.ContentLength > 0))
                {
                    string fn = System.IO.Path.GetFileName(File2.PostedFile.FileName);

                    string SaveLocation = Server.MapPath("") + "\\Data\\FACTFINANCIERA_LOG." + fn.Substring(fn.Length - 3, 3);

                    try
                    {
                        File2.PostedFile.SaveAs(SaveLocation);
                    }
                    catch (Exception ex)
                    {
                        MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgErrorCopiarArchivo"));
                    }

                    try
                    {
                        NegocioPF.Facturas oFacturas = new NegocioPF.Facturas();

                        registros = oFacturas.ImportarResFinTXT(((Usuario)Session["oUsuario"]).Id, SaveLocation);

                        MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgRegActInsertados") + registros.ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox(sender, e, "Error:" + ((Idioma)Session["oIdioma"]).Texto(ex.Message));
                    }
                }
                else
                {
                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgSeleccioneArchivo"));
                }
            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }

        }

        private void EstableceIdioma(Idioma oIdioma)
        {
            lblTitulo.Text = oIdioma.Texto("ImpResPark");
            lblFactLog.Text = oIdioma.Texto("FactLogisticas");
            lblFactFin.Text = oIdioma.Texto("FactFinancieras");
            lblLeyArchLog.Text = oIdioma.Texto("SelArchImportar");
            lblLeyArchFin.Text = oIdioma.Texto("SelArchImportar");

            EstableceIdiomaBotones(oIdioma.Id, this.Controls);
        }

        private void AgregaScriptCliente()
        {
            try
            {
                string codigo;

                codigo = "function ValidaDatosLog() { ";
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

                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ValidaDatosLog", codigo, true);

                codigo = "function ValidaDatosFin() { ";
                codigo += "obj = document.getElementById('" + File2.ClientID + "').value; ";
                codigo += "if (obj == null || obj == \"\") { ";
                codigo += "alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgSeleccioneArchivo") + "\");";
                codigo += "return false; ";
                codigo += "} ";
                codigo += "if (obj.substring(obj.length - 3).toUpperCase() != \"TXT\") { ";
                codigo += "alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgFormatoIncorrecto") + "\"); ";
                codigo += "return false; ";
                codigo += "} ";
                codigo += "} ";

                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ValidaDatosFin", codigo, true);

            }
            catch (Exception ex)
            {
                MessageBox(null, null, ex.Message);
            }
        }

    }


}
