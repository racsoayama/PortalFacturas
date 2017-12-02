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
    public partial class RepFacXProv : FormaPadre
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ValidaVariables();
                    EstableceIdioma((Idioma)Session["oIdioma"]);

                    NegocioPF.Proveedores oProveedores = new NegocioPF.Proveedores();
                    oProveedores.Cargar();
                    foreach(DataRow r in oProveedores.Datos.Tables[0].Rows)
                    {
                        DropDownCheckBoxes1.Items.Add(new ListItem(r["nombre"].ToString(), r["id_proveedor"].ToString()));
                    }

                    //Si el usuario es un usuario del proveedor, selecciona el proveedor 
                    NegocioPF.Proveedor oProveedor = new NegocioPF.Proveedor(((Usuario)Session["oUsuario"]).Id);
                    oProveedor.Cargar();
                    if (oProveedor.Nombre != "" && oProveedor.Nombre != null)
                    {
                        divFiltros.Visible = false;
                        btnGenerar_Click(null, null);
                    }
                    else
                    {
                        divReporte.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
                }
            }
        }


        protected void btnGenerar_Click(object sender, ImageClickEventArgs e)
        {
            //string sProveedor = "";
            string sProveedores = "";
            string sNomProv = "";
            int iNumProv = 0;
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

                NegocioPF.Proveedor oProveedor = new NegocioPF.Proveedor(((Usuario)Session["oUsuario"]).Id);
                oProveedor.Cargar();
                iNumProv = 0;
                if (oProveedor.Nombre != "" && oProveedor.Nombre != null)
                {
                    sProveedores = "'" + oProveedor.Id + "'";
                    sNomProv = oProveedor.Nombre;
                }
                else
                {
                    //Recupera los proveedores seleccionados
                    int cont = 0;
                    foreach (ListItem item in DropDownCheckBoxes1.Items)
                    {
                        if (item.Selected)
                        {
                            sProveedores += "'" + item.Value + "',";

                            if (cont <= 3)
                                sNomProv += item.Text + ",";
                            else
                                sNomProv = ((Idioma)Session["oIdioma"]).Texto("Varios") + ",";

                            cont++;
                        }
                    }

                    if (sProveedores.Length > 0)
                    {
                        sProveedores = sProveedores.Substring(0, sProveedores.Length - 1);
                        sNomProv = sNomProv.Substring(0, sNomProv.Length - 1);
                    }

                    //Si se seleccionan a todos los proveedores
                    if (DropDownCheckBoxes1.Items.Count == cont)
                    {
                        sProveedores = "";
                        sNomProv = ((Idioma)Session["oIdioma"]).Texto("Todos");
                    }

                }

                DateTime fecFact = new DateTime(1900, 1, 1);
                DateTime fecIni = new DateTime(1900, 1, 1);
                DateTime fecFin = new DateTime(1900, 1, 1);

                NegocioPF.Facturas oFacturas = new NegocioPF.Facturas();
                oFacturas.Cargar(sProveedores, "", "", "", fecFact, fecFact, fecIni, fecFin, 0, 0, "", "", "");

                string subtitulo = ((Idioma)Session["oIdioma"]).Texto("Proveedor") + ":" + (iNumProv > 5 ? ((Idioma)Session["oIdioma"]).Texto("Varios") : sNomProv);

                ReportParameter[] reportParameter = new ReportParameter[2];
                reportParameter[0] = new ReportParameter("Titulo", ((Idioma)Session["oIdioma"]).Texto("RepFacXProveedor"));
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
            lblTitulo.Text = oIdioma.Texto("RepFacXProveedor");
            lblProveedor.Text = oIdioma.Texto("Proveedor") + ":";
        }

    }
}
