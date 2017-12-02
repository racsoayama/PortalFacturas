using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.IO;
using System.Xml;
using NegocioPF;
using System.Data;
using System.Net.Mail;
using System.Net.Mime;
using PortalFacturas.wsValidaCFDIs;

namespace PortalFacturas
{
    public partial class FacFinPrvExt : FormaPadre
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
                    cboSociedades.DataSource = oSociedades.Datos;
                    cboSociedades.DataTextField = "Nombre";
                    cboSociedades.DataValueField = "id_sociedad";
                    cboSociedades.DataBind();
                    cboSociedades.Items.Insert(0, new ListItem(((Idioma)Session["oIdioma"]).Texto("Seleccionar") + " ...", "0"));

                    //Monedas
                    NegocioPF.Monedas oMonedas = new NegocioPF.Monedas();
                    oMonedas.Cargar();
                    cboMoneda.DataSource = oMonedas.Datos;
                    cboMoneda.DataTextField = "Id_moneda";
                    cboMoneda.DataValueField = "MonedaSAP";
                    cboMoneda.DataBind();
                    cboMoneda.Items.Insert(0, new ListItem(((Idioma)Session["oIdioma"]).Texto("Seleccionar") + " ...", "0"));

                    //Unidades de medida
                    NegocioPF.UnidadesMedida oUnidades = new NegocioPF.UnidadesMedida();
                    oUnidades.Cargar();
                    cboUnidad.DataSource = oUnidades.Datos;
                    cboUnidad.DataTextField = "Id_unidad";
                    cboUnidad.DataValueField = "UnidadSAP";
                    cboUnidad.DataBind();
                    cboUnidad.Items.Insert(0, new ListItem(((Idioma)Session["oIdioma"]).Texto("Seleccionar") + " ...", "0"));

                    NegocioPF.Facturas oFacturas = new NegocioPF.Facturas();
                    oFacturas.Cargar(0);
                    Session["oFacturas"] = oFacturas;

                    txtFecha.Attributes.Add("onclick", "scwShow(this,event);");
                    txtTotal.Attributes.Add("readonly", "readonly");

                    divDetalle.Visible = false;
                    divVisor.Visible = false;
                    btnAceptar.Visible = false;
                    btnCancelar.Visible = false;
                    btnAceptar.Attributes.Add("onclick", "document.body.style.cursor = 'wait';");

                }
                catch (Exception ex)
                {
                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
                }
            }

            //txtOrden.Text = txtOrdenHdn.Text;
            AgregaScriptCliente();
        }

        protected void grdFacturas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            ValidaVariables();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton btnVerPDF = (ImageButton)e.Row.FindControl("btnVerPDF");
                btnVerPDF.ToolTip = ((Idioma)Session["oIdioma"]).Texto("VerPDF");

                ImageButton btnAgregar = (ImageButton)e.Row.FindControl("btnAgregar");
                btnAgregar.ToolTip = ((Idioma)Session["oIdioma"]).Texto("Agregar");

                ImageButton btnEliminar = (ImageButton)e.Row.FindControl("btnEliminar");
                btnEliminar.ToolTip = ((Idioma)Session["oIdioma"]).Texto("Eliminar");
                btnEliminar.OnClientClick = "return confirm('" + ((Idioma)Session["oIdioma"]).Texto("ConfirmaBaja") + "')";

            }
        }

        protected void grdDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            ValidaVariables();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton btnEliminar = (ImageButton)e.Row.FindControl("btnEliminar");
                btnEliminar.ToolTip = ((Idioma)Session["oIdioma"]).Texto("Eliminar");
                btnEliminar.OnClientClick = "return confirm('" + ((Idioma)Session["oIdioma"]).Texto("ConfirmaBaja") + "')";

            }
        }

        protected void btnAceptarItem_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //Determina la tasa de impuestos
                Double tasa = Convert.ToDouble(txtImpuestos.Text) / Convert.ToDouble(txtImpSinIva.Text);
                NegocioPF.Factura oFactura = (NegocioPF.Factura)Session["oFactura"];
                DataRow r = oFactura.Materiales.NewRow();
                r["Factura"] = txtFactura.Text;
                r["Fecha"] = txtFecha.Text;
                r["Descripcion"] = txtMaterial.Text;
                r["Cantidad"] = txtCantidad.Text;
                r["Unidad"] = cboUnidad.SelectedValue;
                //r["ValorUnitario"] = Convert.ToDouble(txtImporteMat.Text) / Convert.ToInt32(txtCantidad.Text);
                r["Importe"] = txtImporteMat.Text;
                r["Impuestos"] = Convert.ToDouble(txtImporteMat.Text) * tasa;
                r["ImpNeto"] = Convert.ToDouble(r["Importe"]) + Convert.ToDouble(r["Impuestos"]);
                r["Moneda"] = cboMoneda.SelectedValue;
                r["Nota_ent"] = "";
                r["Posicion"] = "0";
                r["Entrega"] = "";
                r["Anio_ent"] = "0";
                r["Pos_ent"] = "0";
                r["TipoCond"] = "";

                txtMaterial.Text = "";
                txtCantidad.Text = "";
                cboUnidad.SelectedValue = "0";
                txtImporteMat.Text = "";

                oFactura.Materiales.Rows.Add(r);

                grdDetalle.DataSource = oFactura.Materiales;
                grdDetalle.DataBind();
            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }

        protected void btnEliminarItem_Command(object sender, CommandEventArgs e)
        {
            try
            {
                ValidaVariables();

                //Obtiene indice de la linea a actualizar
                int index = Convert.ToInt32(e.CommandArgument);

                string desc = grdDetalle.DataKeys[index].Value.ToString();

                NegocioPF.Factura oFactura = (NegocioPF.Factura)Session["oFactura"];

                //Busca la factura a eliminar
                foreach (DataRow r in oFactura.Materiales.Rows)
                {
                    if (r["descripcion"].ToString() == desc)
                    {
                        oFactura.Materiales.Rows.Remove(r);
                        break;
                    }
                }

                Session["oFactura"] = oFactura;

                grdDetalle.DataSource = oFactura.Materiales;
                grdDetalle.DataBind();

            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }

        protected void btnAceptarImportar_Click(object sender, ImageClickEventArgs e)
        {
            //string tmp;
            bool existe;

            try
            {
                //Limpia los controles
                txtFactura.Text = "";
                txtFecha.Text = "";
                cboMoneda.SelectedValue = "0";
                txtImpSinIva.Text = "";
                txtImpuestos.Text = "";
                txtTotal.Text = "";

                //Copia los archivos en la carpeta destino
                //string dirDestino = @System.Configuration.ConfigurationSettings.AppSettings["PathArchivos"].ToString();
                string dirDestino = Server.MapPath("") + "\\Facturas\\";
                string dirDestPDF = dirDestino;

                if ((File1.PostedFile != null) && (File1.PostedFile.ContentLength > 0))
                {
                    dirDestPDF += System.IO.Path.GetFileName(File1.PostedFile.FileName);
                    try
                    {
                        File1.PostedFile.SaveAs(dirDestPDF);
                    }
                    catch (Exception ex)
                    {
                        MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgErrorCopiarArchivo"));
                    }
                }

                if ((File3.PostedFile != null) && (File3.PostedFile.ContentLength > 0))
                {
                    dirDestino += System.IO.Path.GetFileName(File3.PostedFile.FileName);
                    try
                    {
                        File3.PostedFile.SaveAs(dirDestino);
                    }
                    catch (Exception ex)
                    {
                        MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgErrorCopiarArchivo"));
                    }
                }

                if ((File4.PostedFile != null) && (File4.PostedFile.ContentLength > 0))
                {
                    dirDestino += System.IO.Path.GetFileName(File4.PostedFile.FileName);
                    try
                    {
                        File4.PostedFile.SaveAs(dirDestino);
                    }
                    catch (Exception ex)
                    {
                        MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgErrorCopiarArchivo"));
                    }
                }

                if ((File5.PostedFile != null) && (File5.PostedFile.ContentLength > 0))
                {
                    dirDestino += System.IO.Path.GetFileName(File5.PostedFile.FileName);
                    try
                    {
                        File5.PostedFile.SaveAs(dirDestino);
                    }
                    catch (Exception ex)
                    {
                        MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgErrorCopiarArchivo"));
                    }
                }

                NegocioPF.Factura oFactura = new NegocioPF.Factura();
                
                NegocioPF.Sociedad oSociedad = new Sociedad(cboSociedades.SelectedValue);
                oSociedad.Cargar();

                txtReceptor.Text = oSociedad.RFC + " " + oSociedad.Nombre;

                oFactura.PDF = System.IO.Path.GetFileName(File1.PostedFile.FileName);
                oFactura.Sociedad = cboSociedades.SelectedValue;
                oFactura.Receptor = oSociedad.RFC;
                //oFactura.Emisor = oProveedor.RFC;

                //oFactura.PDF = System.IO.Path.GetFileName(File1.PostedFile.FileName);
                oFactura.Archivos.Add(new NegocioPF.Archivo(System.IO.Path.GetFileName(File1.PostedFile.FileName), 1));
                if (File3.PostedFile.FileName != "")
                    oFactura.Archivos.Add(new NegocioPF.Archivo(System.IO.Path.GetFileName(File3.PostedFile.FileName), 3));
                if (File4.PostedFile.FileName != "")
                    oFactura.Archivos.Add(new NegocioPF.Archivo(System.IO.Path.GetFileName(File4.PostedFile.FileName), 4));
                if (File5.PostedFile.FileName != "")
                    oFactura.Archivos.Add(new NegocioPF.Archivo(System.IO.Path.GetFileName(File5.PostedFile.FileName), 5));

                Session["pdf"] = System.IO.Path.GetFileName(File1.PostedFile.FileName);
                Session["oFactura"] = oFactura;

                //Muestra la factura en el visor
                if (File1.PostedFile.FileName.Length > 0)
                {
                    oViewer.Attributes.Add("src", "Facturas/" + System.IO.Path.GetFileName(File1.PostedFile.FileName));
                    oViewer.Visible = true;
                    divVisor.Visible = true;
                }

                divImportar.Visible = false;
                divDetalle.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }

        protected void btnVerPDF_Command(object sender, CommandEventArgs e)
        {
            string archivo = "";
            try
            {
                ValidaVariables();

                //Obtiene indice de la factura a consultar
                int index = Convert.ToInt32(e.CommandArgument);

                string cert = grdFacturas.DataKeys[index][0].ToString();

                //Busca el nombre de la factura
                NegocioPF.Facturas oFacturas = (NegocioPF.Facturas)Session["oFacturas"];

                //Busca el nombre del archivo
                foreach (DataRow r in oFacturas.Datos.Tables[0].Rows)
                {
                    if (r["UUID"].ToString() == cert)
                    {
                        archivo = r["pdf"].ToString();
                        break;
                    }
                }

                if (archivo.Length > 0)
                {
                    Session["archivo"] = "Facturas/" + archivo;

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
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }

        protected void btnAgregar_Command(object sender, CommandEventArgs e)
        {
            try
            {
                divImportar.Visible = true;
                divVisor.Visible = false;
                divDetalle.Visible = false;
                divFacturas.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }

        protected void btnEliminar_Command(object sender, CommandEventArgs e)
        {
            try
            {
                ValidaVariables();

                //Obtiene indice de la linea a actualizar
                int index = Convert.ToInt32(e.CommandArgument);

                string cert = grdFacturas.DataKeys[index].Value.ToString();

                NegocioPF.Facturas oFacturas = (NegocioPF.Facturas)Session["oFacturas"];

                //Busca la factura a eliminar
                foreach (DataRow r in oFacturas.Datos.Tables[0].Rows)
                {
                    if (r["UUID"].ToString() == cert)
                    {
                        oFacturas.Datos.Tables[0].Rows.Remove(r);
                        break;
                    }
                }

                Session["oFacturas"] = oFacturas;

                grdFacturas.DataSource = oFacturas.Datos;
                grdFacturas.DataBind();


            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }

        protected void btnAceptarAgregar_Click(object sender, ImageClickEventArgs e)
        {
            bool existe=false;
            try
            {

                if (txtMaterial.Text != "")
                    btnAceptarItem_Click(null, null);

                NegocioPF.Factura oFactura = (Factura)Session["oFactura"];

                NegocioPF.Proveedor oProveedor = new Proveedor(((Usuario)Session["oUsuario"]).Id);
                oProveedor.Cargar();

                //Recupera el emisor
                string emisor;
                if (txtEmisor.Text.IndexOf(" ") >= 0)
                    emisor = txtEmisor.Text.Substring(0, txtEmisor.Text.IndexOf(" "));
                else
                    emisor = txtEmisor.Text.Trim();

                //Si es un proveedor, valida si puede subir la factura, propia o de un tercero
                if (oProveedor.RFC != "")
                {
                    if (oProveedor.RFC != emisor)
                    {
                        if (oProveedor.Intermediario == false)
                            throw new Exception("MsgErrFacDifProv");
                    }
                }

                //Carga al emisor
                oProveedor.Cargar(emisor);

                //Valida que el proveedor exista
                if (oProveedor.RFC == "")
                    throw new Exception("MsgProvInexistente");
                else
                    txtEmisor.Text = oProveedor.RFC + " " + oProveedor.Nombre;

                //Valida que el Emisor pueda facturar en la sociedad seleccionada
                existe = false;
                foreach (string sociedad in oProveedor.Sociedades)
                {
                    if (sociedad == cboSociedades.SelectedValue)
                        existe = true;
                }

                if (!existe)
                    throw new Exception("MsgSocNoActProv");

                //Establece el emisor en la factura
                oFactura.Emisor = emisor;

                //Valida si la factura ya fue registrada
                oFactura.UUID = txtFactura.Text;

                if (oFactura.Existe())
                {
                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgFactYaRegistrada"));
                    return;
                }

                //Valida la fecha de la factura
                NegocioPF.Configuracion oConfig = new NegocioPF.Configuracion();
                oConfig.Cargar();

                DateTime dFechaMinima = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                dFechaMinima = dFechaMinima.AddMonths(oConfig.MesesAtras * -1);

                DateTime dFechaMaxima = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                dFechaMaxima = dFechaMaxima.AddMonths(oConfig.MesesAdelante);

                DateTime dFecha = NegocioPF.Rutinas.ConvierteTextToFecha(txtFecha.Text);

                if (dFecha < dFechaMinima)
                    throw new Exception("MsgErrFactMuyAtrasada");

                if (dFecha > dFechaMaxima)
                    throw new Exception("MsgErrFactFecMuyAdelantada");

                //Establece los datos de la factura
                oFactura.Sociedad = cboSociedades.SelectedValue;
                oFactura.NumFactura = txtFactura.Text;
                oFactura.Fecha = NegocioPF.Rutinas.ConvierteTextToFecha(txtFecha.Text);
                oFactura.Importe = Convert.ToDouble(txtImpSinIva.Text);
                oFactura.Impuestos = Convert.ToDouble(txtImpuestos.Text);
                oFactura.Moneda = cboMoneda.SelectedValue;
                oFactura.TipoFactura = "EFI";

                //Determina el indicador de impuestos
                if (oFactura.Impuestos > 0)
                {
                    oFactura.Tasa = Convert.ToInt32((Convert.ToDouble(txtImpuestos.Text) / Convert.ToDouble(txtImpSinIva.Text)) * 100);
                    NegocioPF.Indicador oIndicador = new Indicador();
                    oIndicador.Buscar(oFactura.Tasa);
                    if (oIndicador.ID != "")
                        oFactura.IndImpuestos = oIndicador.ID;
                    else
                        throw new Exception("MsgErrImpuestos");
                }

                //Agrega la factura a la colección de facturas
                NegocioPF.Facturas oFacturas = (NegocioPF.Facturas)Session["oFacturas"];
                DataRow r = oFacturas.Datos.Tables[0].NewRow();
                r["id_sociedad"] = cboSociedades.SelectedValue;
                r["folio"] = 0;
                r["UUID"] = txtFactura.Text;
                r["folioFact"] = txtFactura.Text;
                r["emisor"] = txtEmisor.Text.Substring(0, txtEmisor.Text.IndexOf(" "));
                r["receptor"] = txtReceptor.Text.Substring(0, txtReceptor.Text.IndexOf(" "));
                r["ordenCompra"] = "";
                r["fecha"] = NegocioPF.Rutinas.ConvierteTextToFecha(txtFecha.Text);
                r["importe"] = txtTotal.Text;
                r["id_moneda"] = cboMoneda.SelectedValue;
                r["pdf"] = Session["pdf"].ToString();
                oFacturas.Datos.Tables[0].Rows.Add(r);
                oFacturas.Relacion.Add(oFactura);
                Session["oFacturas"] = oFacturas;

                grdFacturas.DataSource = oFacturas.Datos;
                grdFacturas.DataBind();

                oViewer.Visible = false;
                divDetalle.Visible = false;
                divFacturas.Visible = true;

                btnAceptar.Visible = (oFacturas.Datos.Tables[0].Rows.Count > 0);
                btnCancelar.Visible = (oFacturas.Datos.Tables[0].Rows.Count > 0);

                divImportar.Visible = true;

                //ScriptManager.RegisterStartupScript(this, typeof(Page), "confirm", "<script>confirmation();</script>", false);

            }
            catch (Exception ex)
            {
                if (ex.Message.Substring(0, 3) == "Msg")
                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
                else
                    MessageBox(sender, e, ex.Message);
            }
        }

        protected void btnCancelarAgregar_Click(object sender, ImageClickEventArgs e)
        {
            divDetalle.Visible = false;
            divVisor.Visible = false;
            divFacturas.Visible = false;

            NegocioPF.Facturas oFacturas = (NegocioPF.Facturas)Session["oFacturas"];
            if (oFacturas.Datos == null)
                divImportar.Visible = true;
            else
            {
                if (oFacturas.Datos.Tables[0].Rows.Count > 0)
                    divFacturas.Visible = true;
                else
                    divImportar.Visible = true;
            }
        }


        protected void btnAceptarSubir_Click(object sender, ImageClickEventArgs e)
        {
            int folio = 0;
            try
            {
                NegocioPF.Facturas oFacturas = (NegocioPF.Facturas)Session["oFacturas"];
                folio = oFacturas.Guardar(((Usuario)Session["oUsuario"]).Id, Server.MapPath("") + "\\Facturas\\");

                //Mueve los archivos por FTP
                try
                {
                    oFacturas.MueveArchivos(((Usuario)Session["oUsuario"]).Id, Server.MapPath("") + "\\Facturas\\");
                }
                catch (Exception ex)
                {
                    //Maneja el error, pendiente de definir si se manda correo a alguien técnico o de soporte
                }

                try
                {
                    EnviaRelFacRegXCorreo(ref oFacturas, folio);
                }
                catch (Exception ex)
                {
                    /* Se maneja el error */
                    oFacturas = new NegocioPF.Facturas();
                    oFacturas.Cargar(0);
                    Session["oFacturas"] = oFacturas;
                    grdFacturas.DataSource = oFacturas.Datos;
                    grdFacturas.DataBind();
                }

                divFacturas.Visible = false;
                divImportar.Visible = true;

                oFacturas = new NegocioPF.Facturas();
                oFacturas.Cargar(0);
                Session["oFacturas"] = oFacturas;
                grdFacturas.DataSource = oFacturas.Datos;
                grdFacturas.DataBind();

                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgFactsGuardadas") + " " + folio.ToString());
            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }

        private void EnviaRelFacRegXCorreo(ref NegocioPF.Facturas oFacturas, int folio)
        {
            try
            {
                NegocioPF.Proveedor oProveedor = new NegocioPF.Proveedor();
                oProveedor.Cargar(txtEmisor.Text.Substring(0, txtEmisor.Text.IndexOf(" ")));

                string sHtml = "<html>";
                sHtml += "<table style='font-family:arial;color:black;font-size:12px; text-align:justify' border='0' width=\"800\">";
                sHtml += "<tr><td><p>" + ((Idioma)Session["oIdioma"]).Texto("MsgSaludo") + "</p></td></tr>";
                sHtml += "<tr><td></td></tr>";
                sHtml += "<tr><td><p>" + ((Idioma)Session["oIdioma"]).Texto("MsgRelFacReg") + " " + folio.ToString() + ":</p></td></tr>";
                sHtml += "<tr><td></td></tr>";

                if (oFacturas.Datos.Tables[0].Rows.Count > 0)
                {
                    sHtml += "<tr><td><table style='font-family:arial;color:black;font-size:12px; text-align:justify' border='1' cellspacing='0' cellpadding='2' width=\"800\">";
                    sHtml += "<tr>";
                    sHtml += "<td style='background:#F8EFFB; text-align:center; width=80px'> " + grdFacturas.Columns[0].HeaderText + "</td>";
                    sHtml += "<td style='background:#F8EFFB; text-align:center; width=150px'>" + grdFacturas.Columns[1].HeaderText + "</td>";
                    sHtml += "<td style='background:#F8EFFB; text-align:center; width=100px'>" + grdFacturas.Columns[2].HeaderText + "</td>";
                    sHtml += "<td style='background:#F8EFFB; text-align:center; width=100px'>" + grdFacturas.Columns[3].HeaderText + "</td>";
                    sHtml += "<td style='background:#F8EFFB; text-align:center; width=50px'>" + grdFacturas.Columns[4].HeaderText + "</td>";
                    //sHtml += "<td style='background:#F8EFFB; text-align:center; width=80px'>" + grdFacturas.Columns[5].HeaderText + "</td>";
                    //sHtml += "<td style='background:#F8EFFB; text-align:center; width=100px'>" + grdFacturas.Columns[6].HeaderText + "</td>";
                    //sHtml += "<td style='background:#F8EFFB; text-align:center; width=100px'>" + grdFacturas.Columns[7].HeaderText + "</td>";
                    sHtml += "</tr>";

                    foreach (GridViewRow r in grdFacturas.Rows)
                    {
                        sHtml += "<tr>";
                        sHtml += "<td style='width=80px;'>" + r.Cells[0].Text + "</td>";
                        sHtml += "<td style='width=150px;'>" + r.Cells[1].Text + "</td>";
                        sHtml += "<td style='width=100px;'>" + r.Cells[2].Text + "</td>";
                        sHtml += "<td style='width=100px;'>" + r.Cells[3].Text + "</td>";
                        sHtml += "<td style='width=50px;'>" + r.Cells[4].Text + "</td>";
                        //sHtml += "<td style='width=80px;'>" + r.Cells[5].Text + "</td>";
                        //sHtml += "<td style='width=100px;'>" + r.Cells[6].Text + "</td>";
                        //sHtml += "<td style='width=100px;'>" + r.Cells[7].Text + "</td>";
                        sHtml += "</tr>";
                    }
                    sHtml += "</table></td></tr>";
                }
                sHtml += "<tr><td></td></tr>";
                sHtml += "<tr><td>" + ((Idioma)Session["oIdioma"]).Texto("Saludos") + "</td></tr>";
                sHtml += "<tr><td></td></tr>";
                sHtml += "<tr><td><img src=cid:FirmaPF></td></tr>";
                sHtml += "</table>";
                sHtml += "</Html>";

                EmailTemplate oEmail = new EmailTemplate("");

                oEmail.To.Add(oProveedor.eMail);

                oEmail.From = new MailAddress(@System.Configuration.ConfigurationSettings.AppSettings["EmailFrom"], "PortalFacturas", System.Text.Encoding.UTF8);
                oEmail.Subject = ((Idioma)Session["oIdioma"]).Texto("FacturasRegistradas") + " " + ((Idioma)Session["oIdioma"]).Texto("ConElFolio").ToLower() + " " + folio.ToString();

                //Agrega Logo
                AlternateView altView = AlternateView.CreateAlternateViewFromString(sHtml, null, MediaTypeNames.Text.Html);

                string imageSource = (Server.MapPath("") + "\\Images\\FirmaPF.jpg");
                LinkedResource PictureRes = new LinkedResource(imageSource, MediaTypeNames.Image.Jpeg);
                PictureRes.ContentId = "FirmaPF";
                altView.LinkedResources.Add(PictureRes);

                oEmail.AlternateViews.Add(altView);

                try
                {
                    oEmail.Send();
                }
                catch (Exception ex)
                {
                    throw new Exception("MsgFacRegEnvCorreo");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void btnCancelarSubir_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Principal.aspx");
        }

        private void EstableceIdioma(Idioma oIdioma)
        {
            lblTitulo.Text = oIdioma.Texto("FacFinPrvExt");
            lblArchFacturas.Text = oIdioma.Texto("ArchivosFactura");
            lblDetFactura.Text = oIdioma.Texto("DetalleFactura");
            lblSociedad.Text = oIdioma.Texto("Sociedad");
            lblLeyArchPDF.Text = oIdioma.Texto("MsgSelArchPDF") + ":";
            lblOtrosArchivos.Text = oIdioma.Texto("OtrosArchivos") + ":";
            lblArchivo1.Text = oIdioma.Texto("Archivo1") + ":";
            lblArchivo2.Text = oIdioma.Texto("Archivo2") + ":";
            lblArchivo3.Text = oIdioma.Texto("Archivo2") + ":";
            lblNumFactura.Text = oIdioma.Texto("Factura") + ":";
            lblEmisor.Text = oIdioma.Texto("Emisor") + ":";
            lblReceptor.Text = oIdioma.Texto("Receptor") + ":";
            lblFecha.Text = oIdioma.Texto("Fecha") + ":";
            lblImpSinIva.Text = oIdioma.Texto("ImporteSinIva") + ":";
            lblImpuestos.Text = oIdioma.Texto("Impuestos") + ":";
            lblTotal.Text = oIdioma.Texto("Total") + ":";
            lblMoneda.Text = oIdioma.Texto("Moneda") + ":";
            lblDetPos.Text = oIdioma.Texto("DetPosicion") + ":";
            lblMaterial.Text = oIdioma.Texto("Material") + ":";
            lblCantidad.Text = oIdioma.Texto("Cantidad") + ":";
            lblUnidad.Text = oIdioma.Texto("UnidadMedida") + ":";
            lblImporteMat.Text = oIdioma.Texto("Importe") + ":";

            foreach (DataControlField c in grdFacturas.Columns)
            {
                c.HeaderText = oIdioma.Texto(c.HeaderText);
            }
            foreach (DataControlField c in grdDetalle.Columns)
            {
                c.HeaderText = oIdioma.Texto(c.HeaderText);
            }

            EstableceIdiomaBotones(oIdioma.Id, this.Controls);
        }

        private void AgregaScriptCliente()
        {
            try
            {
                NegocioPF.Configuracion oConfig = new NegocioPF.Configuracion();
                oConfig.Cargar();

                string codigo;
                codigo = "function ValidaNombreArchivo() { ";
                codigo += "obj = document.getElementById('" + cboSociedades.ClientID + "').value; ";
                codigo += "if (obj == null || obj == \"0\") { ";
                codigo += "  alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgSelSociedad") + "\");";
                codigo += "  return false; ";
                codigo += "} ";
                codigo += "obj = document.getElementById('" + File1.ClientID + "').value; ";
                codigo += "if (obj == null || obj == \"\") { ";
                if (oConfig.PDFObligatorio)
                {
                    codigo += "  alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgSeleccioneArchivo") + "\");";
                    codigo += "  return false; ";
                }
                codigo += "} else { ";
                codigo += "  if (obj.substring(obj.length - 3).toUpperCase() != \"PDF\") { ";
                codigo += "     alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgFormatoPDFIncorrecto") + "\"); ";
                codigo += "     return false; ";
                codigo += "   } ";
                codigo += "} ";
                codigo += "return true; ";
                codigo += "} ";

                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ValidaNombreArchivo", codigo, true);

                codigo = "function ValidaDatosItem() { ";
                codigo += "objDesc = document.getElementById('" + txtMaterial.ClientID + "').value; ";
                codigo += "if (objDesc == null || objDesc == \"\") { ";
                codigo += "  alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCapDescMat") + "\");";
                codigo += "  return false; ";
                codigo += "} ";
                codigo += "obj = document.getElementById('" + txtCantidad.ClientID + "').value; ";
                codigo += "if (obj == null || obj == \"\") { ";
                codigo += "  alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCapCantidad") + "\");";
                codigo += "  return false; ";
                codigo += "} ";
                codigo += "obj = document.getElementById('" + cboUnidad.ClientID + "').value; ";
                codigo += "if (obj == null || obj == \"0\") { ";
                codigo += "  alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCapUnidad") + "\");";
                codigo += "  return false; ";
                codigo += "} ";
                codigo += "obj = document.getElementById('" + txtImporteMat.ClientID + "').value; ";
                codigo += "if (obj == null || obj == \"\") { ";
                codigo += "  alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCapImpMaterial") + "\");";
                codigo += "  return false; ";
                codigo += "} ";
                //Valida que el item no esté ya registrado
                codigo += "var grid = document.getElementById('" + grdDetalle.ClientID + "'); ";
                codigo += "if (grid != null) { ";
                codigo += "  if (grid.rows.length > 0) { ";
                codigo += "    for (i = 1; i < grid.rows.length; i++) { ";
                codigo += "      var cell = grid.rows[i].cells[0].innerHTML; ";
                //codigo += "      alert(cell); ";
                codigo += "      if (cell.toUpperCase().trim() == objDesc.toUpperCase().trim()) { ";
                codigo += "        alert ('" + ((Idioma)Session["oIdioma"]).Texto("MsgMatServExistente") + "'); ";
                codigo += "        return false; ";
                codigo += "      } ";
                codigo += "    } ";
                codigo += "  } ";
                codigo += "} ";
                codigo += "return true; ";
                codigo += "} ";

                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ValidaDatosItem", codigo, true);

                codigo = "function ValidaDatosFact() { ";
                codigo += "obj = document.getElementById('" + txtFactura.ClientID + "').value; ";
                codigo += "if (obj == null || obj == \"\") { ";
                codigo += "  alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCapFactura") + "\");";
                codigo += "  return false; ";
                codigo += "} ";
                codigo += "obj = document.getElementById('" + txtFecha.ClientID + "').value; ";
                codigo += "if (obj == null || obj == \"\") { ";
                codigo += "  alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCapFecFact") + "\");";
                codigo += "  return false; ";
                codigo += "} ";
                codigo += "obj = document.getElementById('" + cboMoneda.ClientID + "').value; ";
                codigo += "if (obj == null || obj == \"0\") { ";
                codigo += "  alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgSelMoneda") + "\");";
                codigo += "  return false; ";
                codigo += "} ";
                codigo += "objSub = document.getElementById('" + txtImpSinIva.ClientID + "').value; ";
                codigo += "if (objSub == null || objSub == \"\") { ";
                codigo += "  alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCapImpSinIva") + "\");";
                codigo += "  return false; ";
                codigo += "} ";
                codigo += "obj = document.getElementById('" + txtImpuestos.ClientID + "').value; ";
                codigo += "if (obj == null || obj == \"\") { ";
                codigo += "  alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCapImpuestos") + "\");";
                codigo += "  return false; ";
                codigo += "} ";
                codigo += "obj = document.getElementById('" + txtTotal.ClientID + "').value; ";
                codigo += "if (obj == null || obj == \"\") { ";
                codigo += "  alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCapTotal") + "\");";
                codigo += "  return false; ";
                codigo += "} ";
                //Valida el último Item capturado, si no ha sido agregado a la lista
                codigo += "objM = document.getElementById('" + txtMaterial.ClientID + "').value; ";
                codigo += "objC = document.getElementById('" + txtCantidad.ClientID + "').value; ";
                codigo += "objU = document.getElementById('" + cboUnidad.ClientID + "').value; ";
                codigo += "objI = document.getElementById('" + txtImporteMat.ClientID + "').value; ";
                codigo += "if ((objM != null && objM != \"\") || (objC != null && objC != \"\") || (objU != null && objU != \"0\") || (objI != null && objI != \"\")) {";
                codigo += "  if (!ValidaDatosItem()) { ";
                codigo += "  return false; } ";
                codigo += "} ";

                //Valida el total de la factura
                codigo += "var sum = 0; ";
                codigo += "var grid = document.getElementById('" + grdDetalle.ClientID + "'); ";
                codigo += "if (grid != null) { ";
                codigo += "  for (var i = 1; i < grid.rows.length; i++) { ";
                codigo += "    if (!isNaN(grid.rows[i].cells[3].innerText != null)) { ";
                codigo += "      if (grid.rows[i].cells[3].innerText != null) { ";
                codigo += "        sum = (parseFloat(sum) + parseFloat(grid.rows[i].cells[3].innerText)).toString(); ";
                codigo += "      } ";
                codigo += "    } ";
                codigo += "  } ";
                codigo += "} ";
                codigo += "obj = document.getElementById('" + txtImporteMat.ClientID + "').value; ";
                codigo += "if (obj != null && obj != \"\") { ";
                codigo += "   sum = sum + parseFloat(obj); ";
                codigo += "} ";
                codigo += "if (sum == 0) { ";
                codigo += "  alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCapDetalle") + "\");";
                codigo += "  return false; ";
                codigo += "} ";
                codigo += "if (parseFloat(sum) != parseFloat(objSub)) {";
                codigo += "  alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgImpNoCoincide") + "\");";
                codigo += "  return false; ";
                codigo += "} ";
                codigo += "return true; ";
                codigo += "} ";


                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ValidaDatosFact", codigo, true);

                codigo = "function sum() { ";
                codigo += "  var txtFirstNumberValue = document.getElementById('" + txtImpSinIva.ClientID + "').value; ";
                codigo += "  var txtSecondNumberValue = document.getElementById('" + txtImpuestos.ClientID + "').value; ";
                codigo += "  var result = parseFloat(txtFirstNumberValue) + parseFloat(txtSecondNumberValue); ";
                codigo += "  if (!isNaN(result)) { ";
                codigo += "     document.getElementById('" + txtTotal.ClientID + "').value = result; ";
                codigo += "  } ";
                codigo += "} ";

                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Sum", codigo, true);

            }
            catch (Exception ex)
            {
                MessageBox(null, null, ex.Message);
            }
        }

        private string ObtenerValorAtributo(ref XmlDocument doc, string atributo)
        {
            try
            {
                return doc.DocumentElement.Attributes[atributo.ToLower()].Value;
            }
            catch { return ""; }
        }
    }
}
