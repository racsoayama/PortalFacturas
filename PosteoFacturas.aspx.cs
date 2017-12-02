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
    public partial class PosteoFacturas : FormaPadre
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ValidaVariables();
                    EstableceIdioma((Idioma)Session["oIdioma"]);

                    //Llena el combo con los tipos de factura
                    cboTipoFact.Items.Add(new ListItem(((Idioma)Session["oIdioma"]).Texto("FactLogEM"), "1"));
                    cboTipoFact.Items.Add(new ListItem(((Idioma)Session["oIdioma"]).Texto("FactLogCI"), "2"));
                    cboTipoFact.Items.Add(new ListItem(((Idioma)Session["oIdioma"]).Texto("FactFin"), "3"));
                    cboTipoFact.Items.Add(new ListItem(((Idioma)Session["oIdioma"]).Texto("FactExt"), "4"));
                    cboTipoFact.Items.Add(new ListItem(((Idioma)Session["oIdioma"]).Texto("Seleccionar"), "0"));
                    cboTipoFact.SelectedValue = "0";

                }
                catch (Exception ex)
                {
                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
                }
            }

            AgregaScriptCliente();

        }

        protected void btnAceptar_Click(object sender, ImageClickEventArgs e)
        {
            int registros = 0;
            string nomFile = "";
            try
            {
                ValidaVariables();

                NegocioPF.Facturas oFacturas = new NegocioPF.Facturas();

                string dirDestino = Server.MapPath("") + "\\Facturas\\";

                
                if (cboTipoFact.SelectedValue == "1")
                    //registros = oFacturas.GenerarTXTLogisticas(((Usuario)Session["oUsuario"]).Id, dirDestino, ref nomFile);

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

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Principal.aspx");
        }

        private void EstableceIdioma(Idioma oIdioma)
        {
            lblTitulo.Text = oIdioma.Texto("GenArchFacPark");
            lblTipoFacturas.Text = oIdioma.Texto("SelTipoFact") + ":";

            EstableceIdiomaBotones(oIdioma.Id, this.Controls);
        }

        private void AgregaScriptCliente()
        {
            try
            {
                string codigo;

                codigo = "function validaDirectorio() { ";
                codigo += "obj = document.getElementById('" + cboTipoFact.ClientID + "').value; ";
                codigo += "if (obj == null || obj == \"0\") { ";
                codigo += "alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgSelTipoFact") + "\");";
                codigo += "return false; ";
                codigo += "} ";
                codigo += "} ";

                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ValidaDatos", codigo, true);

            }
            catch (Exception ex)
            {
                MessageBox(null, null, ex.Message);
            }
        }

    }


}
