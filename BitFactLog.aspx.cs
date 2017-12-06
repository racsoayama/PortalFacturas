using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NegocioPF;

namespace PortalFacturas
{
    public partial class BitFactLog : FormaPadre
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet dsDatos;

            if (!IsPostBack)
            {
                try
                {
                    ValidaVariables();
                    EstableceIdioma((Idioma)Session["oIdioma"]);

                    NegocioPF.Bitacora oBitacora = new NegocioPF.Bitacora();
                    dsDatos = oBitacora.Buscar("Facturas.GenerarTXTLogisticas", new DateTime(1900, 1, 1));
                    grdBitacora.DataSource = dsDatos;
                    grdBitacora.DataBind();

                    txtFecha.Attributes.Add("onclick", "scwShow(this,event);");

                }
                catch (Exception ex)
                {
                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
                }
            }
            AgregaScriptCliente();
        }

        protected void grdBitacora_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                ValidaVariables();
                DataSet dsDatos;
                grdBitacora.PageIndex = e.NewPageIndex;

                NegocioPF.Bitacora oBitacora = new NegocioPF.Bitacora();
                dsDatos = oBitacora.Buscar("Facturas.GenerarTXTLogisticas", NegocioPF.Rutinas.ConvierteTextToFecha(txtFecha.Text));
                grdBitacora.DataSource = dsDatos;
                grdBitacora.DataBind();
            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            DataSet dsDatos;
            try
            {
                ValidaVariables();

                NegocioPF.Bitacora oBitacora = new NegocioPF.Bitacora();
                dsDatos = oBitacora.Buscar("Facturas.GenerarTXTLogisticas", NegocioPF.Rutinas.ConvierteTextToFecha(txtFecha.Text));
                grdBitacora.DataSource = dsDatos;
                grdBitacora.DataBind();

                if (dsDatos.Tables[0].Rows.Count == 0)
                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgNoHayInfBit"));
            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }

        private void EstableceIdioma(Idioma oIdioma)
        {
            lblTitulo.Text = oIdioma.Texto("BitFactLog");

            foreach (DataControlField c in grdBitacora.Columns)
            {
                c.HeaderText = oIdioma.Texto(c.HeaderText);
            }
            EstableceIdiomaBotones(oIdioma.Id, this.Controls);
        }

        private void AgregaScriptCliente()
        {
            try
            {
                string codigo;
                codigo = "function ValidaDatos() { ";
                codigo += "obj = document.getElementById('" + txtFecha.ClientID + "').value; ";
                codigo += "if (obj == null || obj == \"\") { ";
                codigo += "    alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCapFecha") + "\"); ";
                codigo += "    return false; ";
                codigo += "} ";
                codigo += "return true; ";
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