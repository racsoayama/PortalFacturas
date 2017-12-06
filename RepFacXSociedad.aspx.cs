using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;
using System.Data;
using NegocioPF;

namespace PortalFacturas
{
    public partial class RepFacXSociedad : FormaPadre
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ValidaVariables();
                    EstableceIdioma((Idioma)Session["oIdioma"]);

                    NegocioPF.Sociedades oSociedades = new NegocioPF.Sociedades();
                    oSociedades.Cargar();
                    foreach(DataRow r in oSociedades.Datos.Tables[0].Rows)
                    {
                        cboSociedades.Items.Add(new ListItem(r["nombre"].ToString(), r["id_sociedad"].ToString()));
                    }
                    cboSociedades.DataBind();


                    divReporte.Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
                }
            }
        }


        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            string sProveedor = "";
            string sSociedades = "";
            string sNomSoc = "";
            try
            {
                //Ocultar los botones
                ReportViewer1.ShowPageNavigationControls = false;
                ReportViewer1.ShowBackButton = false;
                ReportViewer1.ShowFindControls = false;
                ReportViewer1.ShowPrintButton = true;
                ReportViewer1.ShowExportControls = true;
                ReportViewer1.LocalReport.EnableExternalImages = true;
                //ReportViewer1.LocalReport.ExecuteReportInCurrentAppDomain(AppDomain.CurrentDomain.Evidence);

                ReportViewer1.Reset();
                ReportViewer1.LocalReport.Dispose();
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.ReportPath = "Reports\\RepFacXFolio_" + ((Usuario)Session["oUsuario"]).Idioma + ".rdlc";
                ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;

                //Si el usuario es un usuario del proveedor, agrega el proveedor a los filtros
                NegocioPF.Proveedor oProveedor = new NegocioPF.Proveedor(((Usuario)Session["oUsuario"]).Id);
                oProveedor.Cargar();
                if (oProveedor.Nombre != "" && oProveedor.Nombre != null)
                {
                    sProveedor = "'" + oProveedor.Id + "'";
                }

                //Recupera las sociedades seleccionadas
                foreach (ListItem item in cboSociedades.Items)
                {
                    if (item.Selected)
                    {
                        sSociedades += "'" + item.Value + "',";
                        sNomSoc += item.Text + ",";
                    }
                }
                if (sSociedades.Length > 0)
                {
                    sSociedades = sSociedades.Substring(0, sSociedades.Length - 1);
                    sNomSoc = sNomSoc.Substring(0, sNomSoc.Length - 1);
                }

                DateTime fecFact = new DateTime(1900, 1, 1);
                DateTime fecIni = new DateTime(1900, 1, 1);
                DateTime fecFin = new DateTime(1900, 1, 1);

                NegocioPF.Facturas oFacturas = new NegocioPF.Facturas();
                oFacturas.Cargar(sProveedor, "", "", "", fecFact, fecFact, fecIni, fecFin, 0, 0, sSociedades, "", "");

                string subtitulo = ((Idioma)Session["oIdioma"]).Texto("Sociedades") + ":" + sNomSoc;

                ReportParameter[] reportParameter = new ReportParameter[2];
                reportParameter[0] = new ReportParameter("Titulo", ((Idioma)Session["oIdioma"]).Texto("RepFacXSociedad"));
                reportParameter[1] = new ReportParameter("Subtitulo", subtitulo);
                ReportViewer1.LocalReport.SetParameters(reportParameter);
                ReportViewer1.LocalReport.Refresh();

                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DsFactReg_DsFactReg", oFacturas.Datos.Tables[0]));
                ReportViewer1.Visible = true;
                ReportViewer1.LocalReport.Refresh();

                divReporte.Visible = true;
                lblTitulo.Visible = false;
                divFiltros.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }

        private void EstableceIdioma(Idioma oIdioma)
        {
            lblTitulo.Text = oIdioma.Texto("RepFacXSociedad");
            lblSociedad.Text = oIdioma.Texto("Sociedad") + ":";
        }

    }
}
