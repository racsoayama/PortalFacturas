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
    public partial class GenArchFact : FormaPadre
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ValidaVariables();
                    EstableceIdioma((Idioma)Session["oIdioma"]);

                    txtLogFecIni.Attributes.Add("onclick", "scwShow(this,event);");
                    txtLogFecFin.Attributes.Add("onclick", "scwShow(this,event);");
                    txtFinFecIni.Attributes.Add("onclick", "scwShow(this,event);");
                    txtFinFecFin.Attributes.Add("onclick", "scwShow(this,event);");

                }
                catch (Exception ex)
                {
                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
                }
            }

            AgregaScriptCliente();

        }

        protected void btnGenerarLog_Click(object sender, EventArgs e)
        {
            int registros = 0;
            string nomFile = "";
            try
            {
                ValidaVariables();

                NegocioPF.Facturas oFacturas = new NegocioPF.Facturas();

                string dirDestino = Server.MapPath("") + "\\Facturas\\";


                registros = oFacturas.GenerarTXTLogisticas(((Usuario)Session["oUsuario"]).Id, 
                            NegocioPF.Rutinas.ConvierteTextToFecha(txtLogFecIni.Text),
                            NegocioPF.Rutinas.ConvierteTextToFecha(txtLogFecFin.Text), 
                            dirDestino, 
                            ref nomFile);

                if (registros > 0)
                {
                    string txtPath = dirDestino + nomFile;
                    Response.ContentType = "text/plain";
                    Response.AppendHeader("content-disposition",
                            "attachment; filename=" + nomFile);
                    Response.TransmitFile(txtPath);
                    Response.End();
                }

                if (registros == 0)
                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgNoHayFactPend"));
            }
            catch (Exception ex)
            {
                MessageBox(sender, e, "Error:" + ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }

        protected void btnGenerarFin_Click(object sender, EventArgs e)
        {
            int registros = 0;
            string nomFile = "";
            try
            {
                ValidaVariables();

                NegocioPF.Facturas oFacturas = new NegocioPF.Facturas();

                string dirDestino = Server.MapPath("") + "\\Facturas\\";


                registros = oFacturas.GenerarTXTFinancieras(((Usuario)Session["oUsuario"]).Id,
                            NegocioPF.Rutinas.ConvierteTextToFecha(txtFinFecIni.Text),
                            NegocioPF.Rutinas.ConvierteTextToFecha(txtFinFecFin.Text),
                            dirDestino,
                            ref nomFile);

                if (registros > 0)
                {
                    string txtPath = dirDestino + nomFile;
                    Response.ContentType = "text/plain";
                    Response.AppendHeader("content-disposition",
                            "attachment; filename=" + nomFile);
                    Response.TransmitFile(txtPath);
                    Response.End();
                }

                if (registros == 0)
                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgNoHayFactPend"));
            }
            catch (Exception ex)
            {
                MessageBox(sender, e, "Error:" + ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }

        //protected void btnCancelar_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("Principal.aspx");
        //}

        private void EstableceIdioma(Idioma oIdioma)
        {
            lblTitulo.Text = oIdioma.Texto("GenArchPed");
            lblFactLog.Text = oIdioma.Texto("FactLogisticas");
            lblFactFin.Text = oIdioma.Texto("FactFinancieras");
            lblLogFechIni.Text = oIdioma.Texto("Fecha") + ":";
            lblLogFechFin.Text = oIdioma.Texto("Al") + ":";
            lblFinFechIni.Text = oIdioma.Texto("Fecha") + ":";
            lblFinFechFin.Text = oIdioma.Texto("Al") + ":";

            EstableceIdiomaBotones(oIdioma.Id, this.Controls);
        }

        private void AgregaScriptCliente()
        {
            try
            {
                string codigo;

                codigo = "function ValidaDatosLog() { ";
                codigo += "obj = document.getElementById('" + txtLogFecIni.ClientID + "').value; ";
                codigo += "if (obj == null || obj == \"0\") { ";
                codigo += "alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCapFecIni") + "\");";
                codigo += "return false; ";
                codigo += "} ";
                codigo += "obj = document.getElementById('" + txtLogFecFin.ClientID + "').value; ";
                codigo += "if (obj == null || obj == \"0\") { ";
                codigo += "alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCapFecFin") + "\");";
                codigo += "return false; ";
                codigo += "} ";
                codigo += "} ";

                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ValidaDatosLog", codigo, true);

                codigo = "function ValidaDatosFin() { ";
                codigo += "obj = document.getElementById('" + txtFinFecIni.ClientID + "').value; ";
                codigo += "if (obj == null || obj == \"0\") { ";
                codigo += "alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCapFecIni") + "\");";
                codigo += "return false; ";
                codigo += "} ";
                codigo += "obj = document.getElementById('" + txtFinFecFin.ClientID + "').value; ";
                codigo += "if (obj == null || obj == \"0\") { ";
                codigo += "alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCapFecFin") + "\");";
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
