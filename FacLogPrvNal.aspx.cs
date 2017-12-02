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
    public partial class FacLogPrvNal : FormaPadre
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

                    NegocioPF.Monedas oMonedas = new NegocioPF.Monedas();
                    oMonedas.Cargar();
                    cboMoneda.DataSource = oMonedas.Datos;
                    cboMoneda.DataTextField = "id_moneda";
                    cboMoneda.DataValueField = "monedaSAP";
                    cboMoneda.DataBind();
                    cboMoneda.Items.Insert(0, new ListItem(((Idioma)Session["oIdioma"]).Texto("Seleccionar") + " ...", "0"));

                    NegocioPF.Facturas oFacturas = new NegocioPF.Facturas();
                    oFacturas.Cargar(0);
                    Session["oFacturas"] = oFacturas;

                    txtValidacion.Enabled = false;
                    divDetalle.Visible = false;
                    divVisor.Visible = false;
                    divEntregas.Visible = false;
                    divServicios.Visible = false;
                    divItemsFac.Visible = false;
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
            //ValidaVariables();

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
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //ImageButton btnVerPDF = (ImageButton)e.Row.FindControl("btnVerPDF");
                //btnVerPDF.ToolTip = ((Idioma)Session["oIdioma"]).Texto("VerPDF");

                //ImageButton btnAgregar = (ImageButton)e.Row.FindControl("btnAgregar");
                //btnAgregar.ToolTip = ((Idioma)Session["oIdioma"]).Texto("Agregar");

                //ImageButton btnEliminar = (ImageButton)e.Row.FindControl("btnEliminar");
                //btnEliminar.ToolTip = ((Idioma)Session["oIdioma"]).Texto("Eliminar");
                //btnEliminar.OnClientClick = "return confirm('" + ((Idioma)Session["oIdioma"]).Texto("ConfirmaBaja") + "')";
            }
        }

        protected void grdEntregas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox cb = (CheckBox)e.Row.FindControl("chkSeleccion");
                cb.Checked = (DataBinder.Eval(e.Row.DataItem, "seleccionado").ToString() == "1" ? true : false);
            }
        }

        protected void grdServicios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox cb = (CheckBox)e.Row.FindControl("chkSeleccion");
                cb.Checked = (DataBinder.Eval(e.Row.DataItem, "seleccionado").ToString() == "1" ? true : false);
            }
        }

        protected void grdCostos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox cb = (CheckBox)e.Row.FindControl("chkSeleccion");
                cb.Checked = (DataBinder.Eval(e.Row.DataItem, "seleccionado").ToString() == "1" ? true : false);
            }
        }

        protected void btnAceptarImportar_Click(object sender, ImageClickEventArgs e)
        {
            string tmp;

            try
            {
                //Copia los archivos en la carpeta destino
                //string dirDestino = @System.Configuration.ConfigurationSettings.AppSettings["PathArchivos"].ToString();
                string dirDestino = Server.MapPath("") + "\\Facturas\\";
                string dirDestPDF = dirDestino;
                string dirDestXML = dirDestino;

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

                if ((File2.PostedFile != null) && (File2.PostedFile.ContentLength > 0))
                {
                    dirDestXML += System.IO.Path.GetFileName(File2.PostedFile.FileName);
                    try
                    {
                        File2.PostedFile.SaveAs(dirDestXML);
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

                XmlReader xmlReader = XmlReader.Create(dirDestXML);

                try
                {

                    XmlDocument doc = new XmlDocument();
                    doc.Load(xmlReader);
                    //doc.Load(dirDestino);

                    oFactura.Sociedad = cboSociedades.SelectedValue;
                    oFactura.Orden = txtOrden.Text;

                    tmp = Rutinas.ObtenerValorAtributo(ref doc, "folio");
                    oFactura.NumFactura = (tmp != "" ? tmp : "");

                    tmp = Rutinas.ObtenerValorAtributo(ref doc, "serie");
                    oFactura.Serie = tmp;

                    tmp = Rutinas.ObtenerValorAtributo(ref doc, "fecha");
                    oFactura.Fecha = (tmp != "" ? new DateTime(Convert.ToInt32(tmp.Substring(0, 4)), Convert.ToInt32(tmp.Substring(5, 2)), Convert.ToInt32(tmp.Substring(8, 2))) : new DateTime(1900, 1, 1));

                    tmp = Rutinas.ObtenerValorAtributo(ref doc, "subTotal");
                    if (tmp != "")
                        oFactura.Importe = Convert.ToDouble(tmp);

                    tmp = Rutinas.ObtenerValorAtributo(ref doc, "moneda");
                    oFactura.Moneda = tmp;

                    NegocioPF.UnidadMedida oUnidadMedida = new UnidadMedida();

                    foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                    {
                        if (node.Name == "cfdi:Emisor")
                        {
                            oFactura.Emisor = Rutinas.ObtenerValorAtributoNodo(node, "rfc");
                            oFactura.NomEmisor = Rutinas.ObtenerValorAtributoNodo(node, "nombre");
                        }
                        if (node.Name == "cfdi:Receptor")
                        {
                            oFactura.Receptor = Rutinas.ObtenerValorAtributoNodo(node, "rfc");
                            oFactura.NomReceptor = Rutinas.ObtenerValorAtributoNodo(node, "nombre");
                        }
                        if (node.Name == "cfdi:Conceptos")
                        {
                            foreach (XmlNode child in node.ChildNodes)
                            {
                                if (child.Name == "cfdi:Concepto")
                                {
                                    DataRow r = oFactura.Materiales.NewRow();
                                    r["Posicion"] = 0;
                                    r["Descripcion"] = Rutinas.ObtenerValorAtributoNodo(child, "descripcion"); //child.Attributes["descripcion"].Value;
                                    tmp = Rutinas.ObtenerValorAtributoNodo(child, "cantidad");
                                    if (tmp != "")
                                        r["Cantidad"] = Convert.ToDouble(tmp); //Convert.ToDouble(child.Attributes["cantidad"].Value);

                                    tmp = Rutinas.ObtenerValorAtributoNodo(child, "valorUnitario");
                                    if (tmp != "")
                                        r["ValorUnitario"] = Convert.ToDouble(tmp); //child.Attributes["valorUnitario"].Value

                                    tmp = Rutinas.ObtenerValorAtributoNodo(child, "importe");
                                    if (tmp != "")
                                        r["Importe"] = Convert.ToDouble(tmp);

                                    tmp = Rutinas.ObtenerValorAtributoNodo(child, "unidad");
                                    if (tmp != "")
                                    {
                                        oUnidadMedida.Buscar(tmp);
                                        r["Unidad"] = oUnidadMedida.UnidadSAP;
                                    }
                                    r["Factura"] = oFactura.NumFactura;
                                    r["Fecha"] = oFactura.Fecha.ToString("dd-MM-yyyy");
                                    r["Anio_ent"] = "0";
                                    r["Entrega"] = "";
                                    r["Pos_ent"] = 0;
                                    oFactura.Materiales.Rows.Add(r);
                                }
                            }
                        }
                        if (node.Name == "cfdi:Impuestos")
                        {
                            //oFactura.Impuestos = Convert.ToDouble(node.Attributes["totalImpuestosTrasladados"].Value);

                            foreach (XmlNode child in node.ChildNodes)
                            {
                                if (child.Name == "cfdi:Traslados")
                                {
                                    foreach (XmlNode child1 in child.ChildNodes)
                                    {
                                        if (child1.Name == "cfdi:Traslado")
                                        {
                                            tmp = Rutinas.ObtenerValorAtributoNodo(child1, "tasa");
                                            if (tmp != "")
                                            {
                                                oFactura.Tasa = Convert.ToInt32(Convert.ToDouble(tmp));
                                            }
                                            else
                                            {
                                                tmp = Rutinas.ObtenerValorAtributoNodo(child1, "tasaOCuota");
                                                if (tmp != "")
                                                    oFactura.Tasa = Convert.ToInt32(Convert.ToDouble(tmp) * 100);
                                            }
                                            if (oFactura.Tasa < 1)
                                                oFactura.Tasa = oFactura.Tasa * 100;

                                        }
                                    }
                                }
                            }

                        }

                        if (node.Name == "cfdi:Complemento")
                        {
                            foreach (XmlNode child in node.ChildNodes)
                            {
                                if (child.Name == "tfd:TimbreFiscalDigital")
                                {
                                    oFactura.UUID = Rutinas.ObtenerValorAtributoNodo(child, "UUID");
                                }
                            }
                        }

                    }

                    xmlReader.Dispose();


                    //Busca la clave SAP de la moneda
                    NegocioPF.Moneda oMoneda = new NegocioPF.Moneda();
                    oMoneda.Buscar(oFactura.Moneda);
                    if (oMoneda.ID != "")
                    {
                        oFactura.Moneda = oMoneda.MonedaSAP;
                        cboMoneda.SelectedValue = oMoneda.MonedaSAP;
                    }

                    //Busca la clave del indicador de impuesto
                    if (oFactura.Tasa >= 0)
                    {
                        NegocioPF.Indicador oIndicador = new Indicador();
                        oIndicador.Buscar(oFactura.Tasa);
                        if (oIndicador.ID != "")
                            oFactura.IndImpuestos = oIndicador.ID;
                        else
                            throw new Exception("MsgErrImpuestos");

                        //Actualiza el importe del iva y neto
                        foreach (DataRow r in oFactura.Materiales.Rows)
                        {
                            r["impuestos"] = Convert.ToDouble(r["Importe"]) * (oFactura.Tasa / 100.0);
                            r["ImpNeto"] = Convert.ToDouble(r["Importe"]) + Convert.ToDouble(r["impuestos"]);
                            r["moneda"] = oFactura.Moneda;
                        }
                    }

                    Session["oFactura"] = oFactura;
                }
                catch (Exception ex)
                {
                    xmlReader.Dispose();
                    throw new Exception("MsgErrLeerXML");
                }

                //Valida que esté cargando una factura el proveedor
                NegocioPF.Proveedor oProveedor = new Proveedor(((Usuario)Session["oUsuario"]).Id);
                oProveedor.Cargar();

                //Si es un proveedor
                if (oProveedor.RFC != "")
                {
                    if (oProveedor.RFC != oFactura.Emisor)
                    {
                        if (oProveedor.Intermediario == false)
                            throw new Exception("MsgErrFacDifProv");
                    }
                }

                //Determina el id_de proveedor del emisor
                oProveedor.Cargar(oFactura.Emisor);
                oFactura.Proveedor = oProveedor.Id;
                
                ////Verifia si esta orden de compra ya fue registada con un proveedor diferente
                //NegocioPF.Facturas oFacturas = new Facturas();
                //if (oFacturas.ExisteOC(txtOrden.Text, oFactura.Emisor))
                //{
                //    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgOCRegXOtrProv"));
                //    return;
                //}


                //Verifica si la factura ya existe en el grid
                foreach (GridViewRow f in grdFacturas.Rows)
                {
                    if (f.Cells[1].Text == oFactura.UUID)
                    {
                        MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgFactDuplicada"));
                        return;
                    }
                    //Valida si es el mismo proveedor
                    if (f.Cells[3].Text != oFactura.Emisor)
                    {
                        MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgFactDifProveedor"));
                        return;
                    }
                }

                //Valida si debe hacer la validación ante el SAT
                NegocioPF.Configuracion oConfig = new NegocioPF.Configuracion();
                oConfig.Cargar();

                if (oConfig.ValidacionSAT)
                {
                    //Manda a validar el archivo CFD
                    //wsSAT.WsValidacionCFDsExtSoapClient obj = new WsValidacionCFDsExtSoapClient();
                    //resultado = obj.ValidarXmlCFD(doc.InnerXml);
                    //resultado = obj.ValidarXmlCFD(xml);

                    //Manda a verificar si exite el folio en el SAT
                    wsValidaCFDIs.Acuse objAcuse;
                    wsValidaCFDIs.ConsultaCFDIServiceClient obj = new ConsultaCFDIServiceClient();

                    //Arma la cadena
                    string cadena = "?re=" + oFactura.Emisor + "&rr=" + oFactura.Receptor + "&tt=" + oFactura.Importe.ToString() + "&id=" + oFactura.UUID;
                    //objAcuse = obj.Consulta("?re=CBJ030505UR7&rr=DEM8801152E9&tt=244.00&id=5B8281DD-4CE8-4CCB-8077-445B9D2AD750");
                    objAcuse = obj.Consulta(cadena);
                    txtValidacion.Text = objAcuse.CodigoEstatus;

                    obj.Close();

                    btnAceptarFactura.Visible = (objAcuse.CodigoEstatus.Substring(0, 1) == "S");
                }
                else
                    btnAceptarFactura.Visible = true;

                //oFactura.PDF = System.IO.Path.GetFileName(File1.PostedFile.FileName);
                //oFactura.XML = System.IO.Path.GetFileName(File2.PostedFile.FileName);

                oFactura.Archivos.Add(new NegocioPF.Archivo(System.IO.Path.GetFileName(File1.PostedFile.FileName), 1));
                oFactura.Archivos.Add(new NegocioPF.Archivo(System.IO.Path.GetFileName(File2.PostedFile.FileName), 2));
                if (File3.PostedFile.FileName != "")
                    oFactura.Archivos.Add(new NegocioPF.Archivo(System.IO.Path.GetFileName(File3.PostedFile.FileName), 3));
                if (File4.PostedFile.FileName != "")
                    oFactura.Archivos.Add(new NegocioPF.Archivo(System.IO.Path.GetFileName(File4.PostedFile.FileName), 4));
                if (File5.PostedFile.FileName != "")
                    oFactura.Archivos.Add(new NegocioPF.Archivo(System.IO.Path.GetFileName(File5.PostedFile.FileName), 5));

                Session["pdf"] = System.IO.Path.GetFileName(File1.PostedFile.FileName);
                Session["xml"] = System.IO.Path.GetFileName(File2.PostedFile.FileName);

                //Escribe los datos de la factura en los controles
                txtFactura.Text = oFactura.NumFactura.ToString();
                txtFecha.Text = oFactura.Fecha.ToString("yyyy-MM-dd");
                txtImporte.Text = oFactura.Importe.ToString("###,##0.#0");
                cboMoneda.SelectedValue = "0";
                try
                {
                    cboMoneda.SelectedValue = oFactura.Moneda;
                }
                catch { /*Maneja error*/ }

                txtEmisor.Text = oFactura.Emisor + " " + oFactura.NomEmisor;
                txtReceptor.Text = oFactura.Receptor + " " + oFactura.NomReceptor;
                txtUUID.Text = oFactura.UUID;
                grdDetalle.DataSource = oFactura.Materiales;
                grdDetalle.DataBind();

                //Inhabilita los controles
                txtFactura.Enabled = (txtFactura.Text.Trim().Length == 0);
                txtFecha.Enabled = (txtFecha.Text.Trim().Length == 0);
                txtImporte.Enabled = (txtImporte.Text.Trim().Length == 0);
                cboMoneda.Enabled = (cboMoneda.SelectedValue == "0");
                txtEmisor.Enabled = (txtEmisor.Text.Trim().Length == 0);
                txtReceptor.Enabled = (txtReceptor.Text.Trim().Length == 0);
                txtUUID.Enabled = false;

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
                btnAceptar.Visible = (oFacturas.Datos.Tables[0].Rows.Count > 0);
                btnCancelar.Visible = (oFacturas.Datos.Tables[0].Rows.Count > 0);

            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }

        protected void btnAceptarFactura_Click(object sender, ImageClickEventArgs e)
        {
            DataSet dsEntregas;
            DataSet dsCostos;
            DataSet dsServicios;

            try
            {
                NegocioPF.Factura oFactura = (Factura)Session["oFactura"];

                oFactura.Moneda = cboMoneda.SelectedValue;
                foreach (DataRow w in oFactura.Materiales.Rows)
                {
                    w["moneda"] = oFactura.Moneda;
                }

                //Valida si la factura ya fue registrada
                if (oFactura.Existe())
                {
                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgFactYaRegistrada"));
                    return;
                }

                //Valida si existe el Emisor en el catálogo de proveedores
                NegocioPF.Proveedor oProveedor = new Proveedor();
                oProveedor.Cargar(txtEmisor.Text.Substring(0, txtEmisor.Text.IndexOf(" ")));
                if (oProveedor.Nombre == "" || oProveedor.Nombre == null)
                    throw new Exception("MsgEmisorInexistente");

                //Valida que el receptor corresponda a la sociedad seleccionada
                NegocioPF.Sociedad oSociedad = new NegocioPF.Sociedad(cboSociedades.SelectedValue);
                oSociedad.Cargar();

                if (oSociedad.RFC != txtReceptor.Text.Substring(0, txtReceptor.Text.IndexOf(" ")))
                    throw new Exception("MsgErrFactNoCorrSociedad");

                //Valida que la sociedad esté activa para el proveedor
                bool existe = false;
                foreach (string sociedad in oProveedor.Sociedades)
                {
                    if (sociedad == cboSociedades.SelectedValue)
                        existe = true;
                }

                if (!existe)
                    throw new Exception("MsgSocNoActProv");

                //Valida la fecha de la factura
                NegocioPF.Configuracion oConfig = new NegocioPF.Configuracion();
                oConfig.Cargar();

                DateTime dFechaMinima = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                dFechaMinima = dFechaMinima.AddMonths(oConfig.MesesAtras * -1);

                DateTime dFechaMaxima = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                dFechaMaxima = dFechaMaxima.AddMonths(oConfig.MesesAdelante);

                DateTime dFecha = new DateTime(Convert.ToInt32(txtFecha.Text.Substring(0, 4)),
                                               Convert.ToInt32(txtFecha.Text.Substring(5, 2)),
                                               Convert.ToInt32(txtFecha.Text.Substring(8, 2)));

                if (dFecha < dFechaMinima)
                    throw new Exception("MsgErrFactMuyAtrasada");

                if (dFecha > dFechaMaxima)
                    throw new Exception("MsgErrFactFecMuyAdelantada");

                //Validad el pedido
                NegocioPF.Pedido oPedido = new NegocioPF.Pedido(cboSociedades.SelectedValue, txtOrden.Text);
                oPedido.Cargar();

                if (oPedido.Proveedor == "")
                    throw new Exception("MsgErrPedInexistente");

                if (oProveedor.RFC != oFactura.Emisor)
                    throw new Exception("MsgErrPedDifProv");

                if (oFactura.Sociedad != oPedido.Sociedad)
                    throw new Exception("MsgErrSocDiferente");

                //Se quita esta validación porque se puede ingresar una factura en diferente moneda
                //if (oFactura.Moneda != oPedido.Moneda)
                //    throw new Exception("MsgErrMonFacDifPed");

                Session["oPedido"] = oPedido;

                NegocioPF.Proveedor oProvReg = new Proveedor(((Usuario)Session["oUsuario"]).Id);
                oProvReg.Cargar();

                string filtroProv = "";
                if (oProvReg.Nombre != "")
                {
                    //Si es un proveedor intermediario
                    filtroProv = (oProvReg.Intermediario ? "" : oFactura.Proveedor);
                }
                else
                {
                    //Si es un usuario administrativo
                    filtroProv = "";
                }

                //Obtiene las entregas de materiales
                dsEntregas = oPedido.CargarEntregas(filtroProv, oFactura.Moneda);

                //Obtiene los Costos indirectos
                dsCostos = oPedido.CargarEntregasCI(filtroProv); //((Usuario)Session["oUsuario"]).Id);

                //Obtiene los servicios del pedido, independientemente del proveedor
                dsServicios = oPedido.CargarServicios(filtroProv, oFactura.Moneda);

                if (dsEntregas.Tables[0].Rows.Count > 0 || dsCostos.Tables[0].Rows.Count > 0)
                {
                    //Relaciona los items de la factura con los de las entregas por Cantidad, importe y referencia
                    foreach (DataRow mf in oFactura.Materiales.Rows)
                    {
                        bool encontrado = false;
                        //Lo busca en las entregas de materiales
                        foreach (DataRow r in dsEntregas.Tables[0].Rows)
                        {
                            if (Convert.ToInt32(mf["cantidad"]) == Convert.ToInt32(r["cantidad"]))
                            {
                                //Moneda local
                                if (oFactura.Moneda == "MXN" && Math.Round(Convert.ToDouble(mf["importe"]), 2) == Math.Round(Convert.ToDouble(r["importe"]), 2))
                                    encontrado = true;
                                else if (oFactura.Moneda == oPedido.Moneda && Math.Round(Convert.ToDouble(mf["importe"]), 2) == Math.Round(Convert.ToDouble(r["importe"]), 2))
                                    encontrado = true;
                                //else if (oFactura.Moneda != "MXN" && oFactura.Moneda != oPedido.Moneda)
                                //    encontrado = true;

                                if (encontrado)
                                {
                                    mf["Posicion"] = r["id_pos_ped"];
                                    mf["entrega"] = r["id_entrega"];
                                    mf["Anio_ent"] = r["ejercicio"];
                                    mf["Pos_ent"] = r["id_posicion"];
                                    r["seleccionado"] = 1;
                                    break;
                                }
                            }
                        }

                        if (!encontrado)
                        {
                            //Si no lo encuentra, lo busca en los costos indirectos
                            foreach (DataRow r in dsCostos.Tables[0].Rows)
                            {
                                //Se inhabilita la validación por importe en Costos indirectos, porque 
                                //&& Math.Round(Convert.ToDouble(mf["importe"]), 2) == Math.Round(Convert.ToDouble(r["importe"]), 2))
                                if (Convert.ToInt32(mf["cantidad"]) == Convert.ToInt32(r["cantidad"]))
                                {
                                    mf["Posicion"] = r["id_pos_ped"];
                                    mf["entrega"] = r["id_entrega"];
                                    mf["Anio_ent"] = r["ejercicio"];
                                    mf["Pos_ent"] = r["id_posicion"];
                                    mf["TipoCond"] = r["id_tipoCond"];
                                    r["seleccionado"] = 1;
                                    break;
                                }
                            }
                        }


                    }

                    lblTitEntregas.Visible = (dsEntregas.Tables[0].Rows.Count > 0);
                    grdEntregas.DataSource = dsEntregas;
                    grdEntregas.DataBind();

                    lblTitCostos.Visible = (dsCostos.Tables[0].Rows.Count > 0);
                    grdCostos.DataSource = dsCostos;
                    grdCostos.DataBind();

                    divEntregas.Visible = true;
                }
                else
                {

                    foreach (DataRow mf in oFactura.Materiales.Rows)
                    {
                        bool encontrado = false;
                        //Busca en bienes y servicios
                        if (!encontrado)
                        {
                            foreach (DataRow r in dsServicios.Tables[0].Rows)
                            {
                                if (Convert.ToInt32(mf["cantidad"]) == Convert.ToInt32(r["cantidad"]))
                                {
                                    //Moneda local
                                    if (oFactura.Moneda == "MXN" && Math.Round(Convert.ToDouble(mf["importe"]), 2) == Math.Round(Convert.ToDouble(r["importe"]), 2))
                                        encontrado = true;
                                    else if (oFactura.Moneda == oPedido.Moneda && Math.Round(Convert.ToDouble(mf["importe"]), 2) == Math.Round(Convert.ToDouble(r["importe"]), 2))
                                        encontrado = true;

                                    if (encontrado)
                                    {
                                        mf["Posicion"] = r["id_pos_ped"];
                                        mf["entrega"] = r["id_documento"];
                                        mf["Anio_ent"] = r["ejercicio"];
                                        mf["Pos_ent"] = r["id_posicion"];
                                        r["seleccionado"] = 1;
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    lblTitServicios.Visible = (dsServicios.Tables[0].Rows.Count > 0);
                    grdServicios.DataSource = dsServicios;
                    grdServicios.DataBind();
                    divServicios.Visible = true;
                }

                Session["oFactura"] = oFactura;

                grdDetFactura.DataSource = oFactura.Materiales;
                grdDetFactura.DataBind();

                divItemsFac.Visible = true;
                divDetalle.Visible = false;
                divVisor.Visible = false;
                btnAceptarImportar.Visible = false;
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

        protected void btnAceptarEntregas_Click(object sender, ImageClickEventArgs e)
        {
            int itemsMat = 0;
            int itemsCtos = 0;
            int itemsServ = 0;
            try
            {
                NegocioPF.Facturas oFacturas = (NegocioPF.Facturas)Session["oFacturas"];

                NegocioPF.Factura oFactura = (Factura)Session["oFactura"];

                NegocioPF.Pedido oPedido = (Pedido)Session["oPedido"];

                DataTable dtItems = oFactura.Materiales.Copy();
                dtItems.Rows.Clear();

                //Checa que todos los productos de la factura tengan una entrega
                bool encontrado = false;
                Double importe = 0;
                foreach (DataRow item in oFactura.Materiales.Rows)
                {
                    int cant = Convert.ToInt32(item["Cantidad"]);
                    
                    foreach (GridViewRow en in grdEntregas.Rows)
                    {
                        bool isSelected = (en.Cells[0].Controls[1] as CheckBox).Checked;
                        if (isSelected)
                        {
                            if (Convert.ToInt32(item["Cantidad"]) == Convert.ToInt32(en.Cells[5].Text))
                            {
                                //Moneda local
                                //if (oFactura.Moneda == "MXN")
                                //{
                                //    double limInf = Convert.ToDouble(en.Cells[6].Text) - Convert.ToDouble(en.Cells[6].Text) * 0.04;
                                //    double limSup = Convert.ToDouble(en.Cells[6].Text) + Convert.ToDouble(en.Cells[6].Text) * 0.04;

                                //    if (Math.Round(Convert.ToDouble(en.Cells[6].Text), 2) >= limInf && Math.Round(Convert.ToDouble(en.Cells[6].Text), 2) <= limSup)
                                //        encontrado = true;
                                //}
                                //else if (oFactura.Moneda == oPedido.Moneda)
                                //{
                                //    double limInf = Convert.ToDouble(en.Cells[6].Text) - Convert.ToDouble(en.Cells[6].Text) * 0.04;
                                //    double limSup = Convert.ToDouble(en.Cells[6].Text) + Convert.ToDouble(en.Cells[6].Text) * 0.04;

                                //    if (Math.Round(Convert.ToDouble(en.Cells[6].Text), 2) >= limInf && Math.Round(Convert.ToDouble(en.Cells[6].Text), 2) <= limSup)
                                //        encontrado = true;
                                //}

                                //Valida que la diferencia en importes no supere el +/- 4%
                                double limInf = Convert.ToDouble(item["importe"]) - Convert.ToDouble(item["importe"]) * 0.04;
                                double limSup = Convert.ToDouble(item["importe"]) + Convert.ToDouble(item["importe"]) * 0.04;

                                if (Math.Round(Convert.ToDouble(en.Cells[6].Text), 2) >= limInf && Math.Round(Convert.ToDouble(en.Cells[6].Text), 2) <= limSup)
                                    encontrado = true;

                                if (encontrado)
                                {
                                    DataRow nr = dtItems.NewRow();
                                    nr["Material"] = en.Cells[7].Text;
                                    nr["Posicion"] = en.Cells[2].Text;
                                    nr["Descripcion"] = en.Cells[8].Text;
                                    nr["Cantidad"] = item["Cantidad"]; // Convert.ToInt16(en.Cells[5].Text);
                                    //nr["ValorUnitario"] = Convert.ToDouble(item["ValorUnitario"]);
                                    nr["Importe"] = item["Importe"]; // Convert.ToDouble(en.Cells[6].Text);
                                    nr["Unidad"] = item["Unidad"];
                                    nr["Entrega"] = en.Cells[3].Text;
                                    nr["Anio_ent"] = grdEntregas.DataKeys[en.RowIndex].Values[0];
                                    nr["Pos_ent"] = en.Cells[4].Text;
                                    if (en.Cells[9].Text != "&nbsp;")
                                        nr["Nota_ent"] = en.Cells[9].Text;
                                    dtItems.Rows.Add(nr);
                                    cant -= Convert.ToInt16(en.Cells[5].Text);
                                    (en.Cells[0].Controls[1] as CheckBox).Checked = false;
                                    itemsMat++;
                                    importe += Convert.ToDouble(item["Importe"]);
                                    break;
                                }
                            }
                        }
                    }

                    //Busca en los costos indirectos
                    foreach (GridViewRow en in grdCostos.Rows)
                    {
                        bool isSelected = (en.Cells[0].Controls[1] as CheckBox).Checked;
                        if (isSelected)
                        {
                            if (Convert.ToInt32(item["Cantidad"]) == Convert.ToInt32(en.Cells[5].Text))
                            {
                                DataRow nr = dtItems.NewRow();
                                //nr["Material"] = en.Cells[5].Text;
                                nr["Posicion"] = en.Cells[2].Text;
                                nr["Descripcion"] = item["Descripcion"];
                                nr["Cantidad"] = item["Cantidad"]; // Convert.ToInt16(en.Cells[5].Text);
                                nr["Importe"] = item["Importe"]; // Convert.ToDouble(en.Cells[6].Text);
                                nr["Unidad"] = item["Unidad"];
                                nr["Entrega"] = en.Cells[3].Text;
                                nr["Anio_ent"] = grdCostos.DataKeys[en.RowIndex].Values[0];
                                nr["Pos_ent"] = en.Cells[4].Text;
                                if (en.Cells[9].Text != "&nbsp;")
                                    nr["Nota_ent"] = en.Cells[9].Text;
                                nr["TipoCond"] = en.Cells[8].Text;
                                dtItems.Rows.Add(nr);
                                cant -= Convert.ToInt16(en.Cells[5].Text);
                                (en.Cells[0].Controls[1] as CheckBox).Checked = false;
                                encontrado = true;
                                itemsCtos++;
                                importe += Convert.ToDouble(item["Importe"].ToString());
                                break;
                            }
                        }
                    }

                    //Busca en los servicios
                    foreach (GridViewRow en in grdServicios.Rows)
                    {
                        bool isSelected = (en.Cells[0].Controls[1] as CheckBox).Checked;
                        if (isSelected)
                        {
                            if (Convert.ToInt32(item["Cantidad"]) == Convert.ToInt32(en.Cells[3].Text))
                            {
                                double limInf = Math.Round(Convert.ToDouble(en.Cells[4].Text),2) - Convert.ToDouble(en.Cells[4].Text) * 0.04;
                                double limSup = Math.Round(Convert.ToDouble(en.Cells[4].Text),2) + Convert.ToDouble(en.Cells[4].Text) * 0.04;

                                if (Convert.ToDouble(item["importe"]) >= limInf && Convert.ToDouble(item["importe"]) <= limSup)
                                    encontrado = true;

                                if (encontrado)
                                {
                                    DataRow nr = dtItems.NewRow();
                                    //nr["Material"] = en.Cells[5].Text;
                                    nr["Posicion"] = grdServicios.DataKeys[en.RowIndex].Values[0];
                                    nr["Descripcion"] = item["Descripcion"];
                                    nr["Cantidad"] = item["Cantidad"]; // Convert.ToInt16(en.Cells[5].Text);
                                    nr["Importe"] = item["Importe"]; // Convert.ToDouble(en.Cells[6].Text);
                                    nr["Unidad"] = item["Unidad"];
                                    nr["Entrega"] = grdServicios.DataKeys[en.RowIndex].Values[1];
                                    nr["Anio_ent"] = grdServicios.DataKeys[en.RowIndex].Values[2];
                                    nr["Pos_ent"] = grdServicios.DataKeys[en.RowIndex].Values[3];
                                    if (en.Cells[6].Text != "&nbsp;")
                                        nr["Nota_ent"] = en.Cells[6].Text;
                                    //nr["TipoCond"] = en.Cells[8].Text;
                                    dtItems.Rows.Add(nr);
                                    cant -= Convert.ToInt16(en.Cells[3].Text);
                                    (en.Cells[0].Controls[1] as CheckBox).Checked = false;
                                    encontrado = true;
                                    itemsServ++;
                                    importe += Convert.ToDouble(item["Importe"].ToString());
                                    break;
                                }
                            }
                        }
                    }

                    if (cant > 0)
                        throw new Exception("MsgErrItemsSinEntrega");   //.Replace("&", item["material"].ToString()));
                    //else if (cant > 0)
                    //    throw new Exception(((Idioma)Session["oIdioma"]).Texto("MsgErrCantEntGTFact").Replace("&", item["material"].ToString()));
                }

                //Valida que todos los items sean de la misma nota de entrega
                //foreach (DataRow m in dtItems.Rows)
                //{
                //    if (m["Nota_ent"].ToString() != txtNotaEnt.Text)
                //        throw new Exception("MsgErrSelDifNE");
                //}

                //Valida que el importe de los items no sobrepase el importe de la factura
                if (importe != oFactura.Importe)
                    throw new Exception("MsgErrImpEntCISel");

                //Valida que todos los Items sean de Entrada de Materiales o de Costos indirectos
                if (itemsMat > 0 && itemsCtos > 0)
                    throw new Exception("MsgErrMixItems");

                oFactura.Materiales = dtItems;

                //Establece el tipo de factura
                if (itemsMat > 0)
                    oFactura.TipoFactura = "NEM"; //Nacional entrega mercancia
                if (itemsCtos > 0)
                    oFactura.TipoFactura = "NCI"; //Nacional costos indirectos
                if (itemsServ > 0)
                    oFactura.TipoFactura = "NGS"; //Nacional servicios

                //Recupera las facturas para agregar una a la colección
                DataRow r = oFacturas.Datos.Tables[0].NewRow();
                r["id_sociedad"] = cboSociedades.SelectedValue;
                r["folio"] = 0;
                r["UUID"] = txtUUID.Text;
                r["folioFact"] = txtFactura.Text;
                r["emisor"] = txtEmisor.Text.Substring(0, txtEmisor.Text.IndexOf(" "));
                r["receptor"] = txtReceptor.Text.Substring(0, txtReceptor.Text.IndexOf(" "));
                r["ordenCompra"] = txtOrden.Text;
                r["fecha"] = txtFecha.Text;
                r["importe"] = txtImporte.Text;
                r["id_moneda"] = cboMoneda.SelectedValue;
                r["pdf"] = Session["pdf"].ToString();
                r["xml"] = Session["xml"].ToString();
                oFacturas.Datos.Tables[0].Rows.Add(r);
                oFacturas.Relacion.Add(oFactura);
                Session["oFacturas"] = oFacturas;

                grdFacturas.DataSource = oFacturas.Datos;
                grdFacturas.DataBind();

                oViewer.Visible = false;
                divEntregas.Visible = false;
                divItemsFac.Visible = false;
                divDetalle.Visible = false;
                divFacturas.Visible = true;
                divServicios.Visible = false;
                btnAceptarFactura.Visible = false;
                btnCancelarAgregar.Visible = false;

                btnAceptar.Visible = (oFacturas.Datos.Tables[0].Rows.Count > 0);
                btnCancelar.Visible = (oFacturas.Datos.Tables[0].Rows.Count > 0);

                divImportar.Visible = true;
                btnAceptarImportar.Visible = true;

                ScriptManager.RegisterStartupScript(this, typeof(Page), "confirm", "<script>confirmation();</script>", false);

            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }

        protected void btnCancelarEntregas_Click(object sender, ImageClickEventArgs e)
        {
            oViewer.Visible = true;
            divItemsFac.Visible = false;
            divEntregas.Visible = false;
            divServicios.Visible = false;
            divDetalle.Visible = true;
            divFacturas.Visible = false;

            divVisor.Visible = true;
            divImportar.Visible = false;
            btnAceptarImportar.Visible = false;
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
                    sHtml += "<td style='background:#F8EFFB; text-align:center; width=80px'>" + grdFacturas.Columns[5].HeaderText + "</td>";
                    sHtml += "<td style='background:#F8EFFB; text-align:center; width=100px'>" + grdFacturas.Columns[6].HeaderText + "</td>";
                    sHtml += "<td style='background:#F8EFFB; text-align:center; width=100px'>" + grdFacturas.Columns[7].HeaderText + "</td>";
                    sHtml += "</tr>";

                    foreach (GridViewRow r in grdFacturas.Rows)
                    {
                        sHtml += "<tr>";
                        sHtml += "<td style='width=80px;'>" + r.Cells[0].Text + "</td>";
                        sHtml += "<td style='width=150px;'>" + r.Cells[1].Text + "</td>";
                        sHtml += "<td style='width=100px;'>" + r.Cells[2].Text + "</td>";
                        sHtml += "<td style='width=100px;'>" + r.Cells[3].Text + "</td>";
                        sHtml += "<td style='width=50px;'>" + r.Cells[4].Text + "</td>";
                        sHtml += "<td style='width=80px;'>" + r.Cells[5].Text + "</td>";
                        sHtml += "<td style='width=100px;'>" + r.Cells[6].Text + "</td>";
                        sHtml += "<td style='width=100px;'>" + r.Cells[7].Text + "</td>";
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
            lblTitulo.Text = oIdioma.Texto("FacLogPrvNal");
            lblArchFacturas.Text = oIdioma.Texto("ArchivosFactura");
            lblSociedad.Text = oIdioma.Texto("Sociedad");
            lblOrden.Text = oIdioma.Texto("OrdenCompra") + ":";
            lblLeyArchPDF.Text = oIdioma.Texto("MsgSelArchPDF") + ":";
            lblLayArchXML.Text = oIdioma.Texto("MsgSelArchXML") + ":";
            lblArchivo1.Text = oIdioma.Texto("Archivo1") + ":";
            lblArchivo2.Text = oIdioma.Texto("Archivo2") + ":";
            lblArchivo3.Text = oIdioma.Texto("Archivo2") + ":";
            lblOtrosArchivos.Text = oIdioma.Texto("OtrosArchivos") + ":";
            lblUUID.Text = oIdioma.Texto("UUID") + ":"; 
            lblNumFactura.Text = oIdioma.Texto("Factura") + ":";
            lblEmisor.Text = oIdioma.Texto("Emisor") + ":";
            lblReceptor.Text = oIdioma.Texto("Receptor") + ":";
            lblFecha.Text = oIdioma.Texto("Fecha") + ":";
            lblImporte.Text = oIdioma.Texto("Importe") + ":";
            lblMoneda.Text = oIdioma.Texto("Moneda") + ":";
            lblValidacion.Text = oIdioma.Texto("Validacion") + ":";
            lblTitEntregas.Text = oIdioma.Texto("MsgSelEntMerc") + ":";
            lblTitCostos.Text = oIdioma.Texto("MsgSelCostosInd") + ":";
            lblTitServicios.Text = oIdioma.Texto("MsgSelServicios") + ":";

            foreach (DataControlField c in grdFacturas.Columns)
            {
                c.HeaderText = oIdioma.Texto(c.HeaderText);
            }
            foreach (DataControlField c in grdDetalle.Columns)
            {
                c.HeaderText = oIdioma.Texto(c.HeaderText);
            }
            foreach (DataControlField c in grdDetFactura.Columns)
            {
                c.HeaderText = oIdioma.Texto(c.HeaderText);
            }
            foreach (DataControlField c in grdEntregas.Columns)
            {
                c.HeaderText = oIdioma.Texto(c.HeaderText);
            }
            foreach (DataControlField c in grdCostos.Columns)
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
                codigo += "obj = document.getElementById('" + txtOrden.ClientID + "').value; ";
                codigo += "if (obj == null || obj == \"\") { ";
                codigo += "  alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgCapOrdenCompra") + "\");";
                codigo += "  return false; ";
                codigo += "} ";
                codigo += "if (obj.length != " + oConfig.LongitudOrden + ") { ";
                codigo += "  alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgOrdenCompIncor") + "\");";
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
                codigo += "obj2 = document.getElementById('" + File2.ClientID + "').value; ";
                codigo += "if (obj2 == null || obj2 == \"\") { ";
                codigo += "  alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgSeleccioneArchivo") + "\");";
                codigo += "  return false; ";
                codigo += "} ";
                codigo += "if (obj2.substring(obj2.length - 3).toUpperCase() != \"XML\") { ";
                codigo += "  alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgFormatoXMLIncorrecto") + "\"); ";
                codigo += "  return false; ";
                codigo += "} ";
                codigo += "if (obj != null && obj != \"\" && obj2 != null && obj2 != \"\") ";
                codigo += "{ ";
                codigo += "   var arch1 = obj.substring(obj.lastIndexOf(\"\\\\\")+1, obj.lastIndexOf(\".\")-1);";
                codigo += "   var arch2 = obj2.substring(obj2.lastIndexOf(\"\\\\\")+1, obj2.lastIndexOf(\".\")-1);";
                codigo += "  if (arch1 != arch2) {";
                codigo += "    alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgArchNomDiferente") + "\"); ";
                codigo += "    return false; ";
                codigo += "  } ";
                codigo += "} ";
                codigo += "return true; ";
                codigo += "} ";

                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ValidaNombreArchivo", codigo, true);

                codigo = "function ValidaMoneda() { ";
                codigo += "obj = document.getElementById('" + cboMoneda.ClientID + "').value; ";
                codigo += "if (obj == null || obj == \"0\") { ";
                codigo += "  alert(\"" + ((Idioma)Session["oIdioma"]).Texto("MsgSelMoneda") + "\");";
                codigo += "  return false; ";
                codigo += "} ";
                codigo += "return true; ";
                codigo += "} ";

                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ValidaMoneda", codigo, true);

                codigo = "function confirmation() { ";
                codigo += "var answer = confirm('" + ((Idioma)Session["oIdioma"]).Texto("MsgConfOtraOrden")  + "'); ";
                codigo += "if (!answer) { ";
                codigo += "  var1 = document.getElementById('" + txtOrden.ClientID + "'); ";
                codigo += "  if (var1 != null) { ";
                codigo += "     document.getElementById('" + txtOrden.ClientID + "').value = ''; ";
                codigo += "  } ";
                codigo += "} ";
                codigo += "return; ";
                codigo += "} ";

                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Confirmacion", codigo, true);

                codigo = "function ConfirmarNotas() { ";
                codigo += "  return confirm('" + ((Idioma)Session["oIdioma"]).Texto("ConfSelNotas") + "');";
                codigo += "} ";

                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ConfirmaNotas", codigo, true);

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
