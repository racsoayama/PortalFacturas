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
    public partial class ConsultaFacturas : FormaPadre
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ValidaVariables();
                    EstableceIdioma((Idioma)Session["oIdioma"]);

                    //Si el usuario es un usuario del proveedor, por defautl se muestran todas sus facturas 
                    NegocioPF.Proveedor oProveedor = new NegocioPF.Proveedor(((Usuario)Session["oUsuario"]).Id);
                    oProveedor.Cargar();
                    if (oProveedor.Nombre != "" && oProveedor.Nombre != null)
                    {
                        DateTime fecNull = new DateTime(1900,1,1);

                        NegocioPF.Facturas oFacturas = new NegocioPF.Facturas();
                        //oFacturas.ValidarStatus(oProveedor.RFC);
                        oFacturas.Cargar("'" + oProveedor.Id + "'", "", "", "", fecNull, fecNull, fecNull, fecNull, 0, 0, "", "", "");
                        grdFacturas.DataSource = oFacturas.Datos;
                        grdFacturas.DataBind();
                    }


                    NegocioPF.Sociedades oSociedades = new NegocioPF.Sociedades();
                    oSociedades.Cargar();
                    cboFilSociedad.DataSource = oSociedades.Datos;
                    cboFilSociedad.DataTextField = "Nombre";
                    cboFilSociedad.DataValueField = "id_sociedad";
                    cboFilSociedad.DataBind();
                    cboFilSociedad.Items.Insert(0, new ListItem(((Idioma)Session["oIdioma"]).Texto("Seleccionar") + " ...", "0"));

                    Perfil oPerfil = new Perfil();
                    Permisos permisos = oPerfil.CargarPermisos(((Usuario)Session["oUsuario"]).Id, "ConsultaFacturas.aspx");
                    btnImportar.Visible = (permisos.Importar);

                    btnBuscar.Visible = true;

                    txtFecFacIni.Attributes.Add("onmouseover", "scwShow(this,event);");
                    txtFecFacFin.Attributes.Add("onmouseover", "scwShow(this,event);");
                    txtFecRegIni.Attributes.Add("onmouseover", "scwShow(this,event);");
                    txtFecRegFin.Attributes.Add("onmouseover", "scwShow(this,event);");

                    divFiltros.Visible = true;
                    divImportar.Visible = false;

                }
                catch (Exception ex)
                {
                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
                }
            }

            AgregaScriptCliente();

        }

        protected void grdFacturas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try{
                ValidaVariables();

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    ImageButton btnConsultar = (ImageButton)e.Row.FindControl("btnVerPDF");
                    btnConsultar.ToolTip = ((Idioma)Session["oIdioma"]).Texto("VerFactura");
                }
            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }
        protected void grdFacturas_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            try
            {
                ValidaVariables();
                grdFacturas.PageIndex = e.NewPageIndex;

                NegocioPF.Proveedores oProveedores = new NegocioPF.Proveedores();
                oProveedores.Cargar();
                grdFacturas.DataSource = oProveedores.Datos;
                grdFacturas.DataBind();
            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }


        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string proveedor;
            try
            {
                ValidaVariables();
                
                NegocioPF.Facturas oFacturas = new NegocioPF.Facturas();

                NegocioPF.Proveedor oProveedor = new NegocioPF.Proveedor(((Usuario)Session["oUsuario"]).Id);
                oProveedor.Cargar();
                if (oProveedor.Nombre != "" && oProveedor.Nombre != null)
                    proveedor = oProveedor.Id;
                else
                    proveedor = txtProveedor.Text;

                DateTime fecFacIni = new DateTime(1900, 1, 1);
                if (txtFecFacIni.Text != "")
                    fecFacIni = ConvierteTextToFecha(txtFecFacIni.Text);

                DateTime fecFacFin = new DateTime(1900, 1, 1);
                if (txtFecFacIni.Text != "")
                    fecFacFin = ConvierteTextToFecha(txtFecFacIni.Text);

                DateTime fecRegIni = new DateTime(1900, 1, 1);
                if (txtFecRegIni.Text != "")
                    fecRegIni = ConvierteTextToFecha(txtFecRegIni.Text);

                DateTime fecRegFin = new DateTime(1900, 1, 1);
                if (txtFecRegFin.Text != "")
                    fecRegFin = ConvierteTextToFecha(txtFecRegFin.Text);

                string sociedad = "";
                if (cboFilSociedad.SelectedValue != "0")
                    sociedad = "'" + cboFilSociedad.SelectedValue + "'";

                int folInicial = 0;
                if (txtFolIni.Text != "")
                    folInicial = Convert.ToInt32(txtFolIni.Text);

                int folFinal = 0;
                if (txtFolFin.Text != "")
                    folFinal = Convert.ToInt32(txtFolFin.Text);

                //oFacturas.ValidarStatus(txtEmisor.Text);

                oFacturas.Cargar("'" + proveedor.Trim() + "'", txtEmisor.Text, txtNombre.Text, txtFactura.Text, 
                                fecFacIni, fecFacFin, fecRegIni, fecRegFin, folInicial, folFinal, sociedad, txtOrden.Text, txtEntrega.Text);

                grdFacturas.DataSource = oFacturas.Datos;
                grdFacturas.DataBind();
                divImportar.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }

        protected void btnVerPDF_Command(object sender, CommandEventArgs e)
        {
            try
            {
                ValidaVariables();

                //Obtiene indice de la factura a consultar
                int index = Convert.ToInt32(e.CommandArgument) - (grdFacturas.PageIndex * grdFacturas.PageSize);

                int folio = Convert.ToInt32(grdFacturas.DataKeys[index][0]);
                string cert = grdFacturas.DataKeys[index][1].ToString();

                NegocioPF.Factura oFactura = new NegocioPF.Factura(folio, cert);
                string archivo = oFactura.BajaPDF(Server.MapPath("") + "\\Facturas\\");

                if (archivo.Length > 0)
                {
                    Session["archivo"] = "Facturas/" + oFactura.BajaPDF(Server.MapPath("") + "\\Facturas\\" + archivo);

                    string newWin = "OpenPopupCenter('VisorPDF.aspx','Factura',670,700,0);";

                    ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
                }
                else
                {
                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgPDFInexistente"));
                }                

            }
            catch (Exception ex)
            {
                /* */
            }
        }

        protected void btnDownload_Command(object sender, CommandEventArgs e)
        {
            try
            {
                ValidaVariables();

                ////Obtiene indice de la factura a consultar
                //int index = Convert.ToInt32(e.CommandArgument) - (grdFacturas.PageIndex * grdFacturas.PageSize);

                //int folio = Convert.ToInt32(grdFacturas.DataKeys[index][0]);
                //string cert = grdFacturas.DataKeys[index][1].ToString();

                //NegocioPF.Factura oFactura = new NegocioPF.Factura(folio, cert);
                //string archivo = oFactura.BajaPDF(Server.MapPath("") + "\\Facturas\\");


            }
            catch (Exception ex)
            {
                /* */
            }
        }
        protected void btnVerXML_Command(object sender, CommandEventArgs e)
        {
            try
            {
                ValidaVariables();

                //Obtiene indice de la factura a consultar
                int index = Convert.ToInt32(e.CommandArgument) - (grdFacturas.PageIndex * grdFacturas.PageSize);

                int folio = Convert.ToInt32(grdFacturas.DataKeys[index][0]);
                string cert = grdFacturas.DataKeys[index][1].ToString();

                NegocioPF.Factura oFactura = new NegocioPF.Factura(folio, cert);
                string archivo = oFactura.BajaXML(Server.MapPath("") + "\\Facturas\\");

                if (archivo.Length > 0)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(Server.MapPath("") + "\\Facturas\\"+ archivo);
                    string s = doc.ToString();
                    
//                    Session["archivo"] = "Facturas/" + oFactura.BajaPDF(Server.MapPath("") + "\\Facturas\\" + archivo);
//                    string newWin = "OpenPopupCenter('VisorPDF.aspx','Factura',670,700,0);";
//                    ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);

                    //Response.Clear();
                    //Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", archivo));
                    //Response.ContentType = "application/octet-stream";
                    //Response.WriteFile(archivo);
                    //Response.End();

                    Response.Clear();
                    Response.AddHeader("content-disposition", "attachment;filename=" + archivo);
                    Response.ContentType = "text/xml";
                    Response.ContentEncoding = System.Text.Encoding.UTF8; 

                    //Response.Write(File.ReadAllText(Server.MapPath("~/temp.xml"))); //you may want to write the XML directly to output stream instead of creating an XML file first.
                    //Response.Write(doc.ToString());
                    doc.Save(Response.Output);
                    Response.End();
                }
                else
                {
                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgPDFInexistente"));
                }

            }
            catch (Exception ex)
            {
                /* */
            }
        }

        protected void btnImportar_Click(object sender, EventArgs e)
        {
            //divDetalle.Visible = false;
            divImportar.Visible = true;
        }

        protected void btnAceptarImportar_Click(object sender, EventArgs e)
        {
            int registros = 0;
            try
            {
                ValidaVariables();
                //NegocioPF.Facturas oFacturas = new NegocioPF.Facturas();

                if ((File1.PostedFile != null) && (File1.PostedFile.ContentLength > 0))
                {
                    string fn = System.IO.Path.GetFileName(File1.PostedFile.FileName);

                    string SaveLocation = Server.MapPath("") + "\\Data\\ESTATUS_FACT." + fn.Substring(fn.Length - 3, 3);

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

                        registros = oFacturas.ImportarStatusFact(((Usuario)Session["oUsuario"]).Id, SaveLocation);

                        divImportar.Visible = false;

                        btnBuscar_Click(null, null);

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

        protected void btnCancelarImportar_Click(object sender, EventArgs e)
        {
            divImportar.Visible = false;
        }

        private void EstableceIdioma(Idioma oIdioma)
        {
            lblTitConsFact.Text = oIdioma.Texto("ConsFacturas");
            lblProveedor.Text = oIdioma.Texto("Proveedor") + ":";
            lblEmisor.Text = oIdioma.Texto("Emisor") + ":";
            lblNombre.Text = oIdioma.Texto("Nombre") + ":";
            lblFactura.Text = oIdioma.Texto("Factura") + ":";
            lblFolInicial.Text = oIdioma.Texto("FolioInicial") + ":";
            lblFolFinal.Text = oIdioma.Texto("FolioFinal") + ":";
            lblFecFacIni.Text = oIdioma.Texto("FecFactIni") + ":";
            lblFecFacFin.Text = oIdioma.Texto("FecFactFin") + ":";
            lblFecRegIni.Text = oIdioma.Texto("FecRegIni") + ":";
            lblFecRegFin.Text = oIdioma.Texto("FecRegFin") + ":";
            lblSociedad.Text = oIdioma.Texto("Sociedad") + ":";
            lblOrden.Text = oIdioma.Texto("OrdenCompra") + ":";
            lblEntrega.Text = oIdioma.Texto("Entrega") + ":";
            lblLeyArchivo.Text = oIdioma.Texto("MsgSelArchivo") + ":";

            foreach (DataControlField c in grdFacturas.Columns)
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

                codigo = "function validaNombreArchivo() { ";
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

                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ValidaDatos2", codigo, true);

            }
            catch (Exception ex)
            {
                MessageBox(null, null, ex.Message);
            }
        }

    }


}
