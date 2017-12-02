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
    public partial class RepFacXEntrega : FormaPadre
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ValidaVariables();
                    EstableceIdioma((Idioma)Session["oIdioma"]);
                    divReporte.Visible = false;

                }
                catch (Exception ex)
                {
                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
                }
            }
        }


        protected void btnGenerar_Click(object sender, ImageClickEventArgs e)
        {
            string sProveedor = "";
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

                DateTime fecFacIni = new DateTime(1900, 1, 1);
                DateTime fecFacFin = new DateTime(1900, 1, 1);
                DateTime fecIni = new DateTime(1900, 1, 1);
                DateTime fecFin = new DateTime(1900, 1, 1);

                NegocioPF.Facturas oFacturas = new NegocioPF.Facturas();
                oFacturas.Cargar(sProveedor, "", "", "", fecFacIni, fecFacFin, fecIni, fecFin, 0, 0, "", "", txtEntrega.Text);

                string subtitulo = ((Idioma)Session["oIdioma"]).Texto("Entrega") + ":" + txtEntrega.Text;

                ReportParameter[] reportParameter = new ReportParameter[2];
                reportParameter[0] = new ReportParameter("Titulo", ((Idioma)Session["oIdioma"]).Texto("FacXEntrega"));
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
            lblTitulo.Text = oIdioma.Texto("FacXEntrega");
            lblEntrega.Text = oIdioma.Texto("Entrega") + ":";
        }

    }
}
