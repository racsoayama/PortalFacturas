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
using OfficeOpenXml;
using OfficeOpenXml.Style;
using NegocioPF;

namespace PortalFacturas
{
    public partial class ConsFactNal : FormaPadre
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ValidaVariables();
                    EstableceIdioma((Idioma)Session["oIdioma"]);
                    btnExportar.Visible = false;

                    //Si el usuario es un usuario del proveedor, por defautl se muestran todas sus facturas 
                    NegocioPF.Proveedor oProveedor = new NegocioPF.Proveedor(((Usuario)Session["oUsuario"]).Id);
                    oProveedor.Cargar();
                    if (oProveedor.Nombre != "" && oProveedor.Nombre != null)
                    {
                        NegocioPF.Pedidos oPedidos = new NegocioPF.Pedidos();
                        oPedidos.Cargar(oProveedor.Id, "", "Pendiente");
                        ArmarTabla(ref oPedidos);
                        btnExportar.Visible = (oPedidos.Datos.Tables[0].Rows.Count > 0);
                        lblProveedor.Visible = false;
                        txtProveedor.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
                }
            }

            AgregaScriptCliente();
        }

        //protected void grdFacturas_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    int index;
        //    try
        //    {
        //        ValidaVariables();

        //        if (e.Row.RowType == DataControlRowType.DataRow)
        //        {
        //            ImageButton btnConsultar = (ImageButton)e.Row.FindControl("btnVerPDF");
        //            btnConsultar.ToolTip = ((Idioma)Session["oIdioma"]).Texto("VerFactura");

        //            //Recupera la clave de la factura
        //            index = e.Row.RowIndex;
        //            NegocioPF.Factura oFactura = new NegocioPF.Factura(Convert.ToInt32(grdFacturas.DataKeys[e.Row.RowIndex].Values[0]),
        //                                                               Convert.ToString(grdFacturas.DataKeys[e.Row.RowIndex].Values[1]));
        //            oFactura.Cargar();

        //            GridView grdItems = e.Row.FindControl("grdItems") as GridView;
        //            grdItems.DataSource = oFactura.Materiales;
        //            grdItems.DataBind();

        //            //string customerId = gvCustomers.DataKeys[e.Row.RowIndex].Value.ToString();
        //            //GridView gvOrders = e.Row.FindControl("gvOrders") as GridView;
        //            //gvOrders.DataSource = GetData(string.Format("select top 3 * from Orders where CustomerId='{0}'", customerId));
        //            //gvOrders.DataBind();

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
        //    }
        //}

        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ValidaVariables();

                NegocioPF.Pedidos oPedidos = new NegocioPF.Pedidos();

                NegocioPF.Proveedor oProveedor = new NegocioPF.Proveedor(((Usuario)Session["oUsuario"]).Id);
                oProveedor.Cargar();
                if (oProveedor.Nombre != "" && oProveedor.Nombre != null)
                    oPedidos.Cargar(oProveedor.Id, txtOrden.Text, "Pendiente");
                else
                    oPedidos.Cargar(txtProveedor.Text, txtOrden.Text, "");

                btnExportar.Visible = (oPedidos.Datos.Tables[0].Rows.Count > 0);
                ArmarTabla(ref oPedidos);
            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }


        private void ArmarTabla(ref NegocioPF.Pedidos oPedidos)
        {
            TableRow row;
            TableCell cell;
            string proveedor = "";
            try
            {
                NegocioPF.Proveedor oProveedor = new NegocioPF.Proveedor(((Usuario)Session["oUsuario"]).Id);
                oProveedor.Cargar();
                if (oProveedor.Nombre != "" && oProveedor.Nombre != null)
                {
                    proveedor = oProveedor.Id;
                }

                foreach (DataRow r in oPedidos.Datos.Tables[0].Rows)
                {
                    NegocioPF.Pedido oPedido = new NegocioPF.Pedido(r["id_sociedad"].ToString(), r["id_pedido"].ToString());
                    oPedido.Cargar(proveedor);

                    //Crea los encabezados
                    tabPedidos.CssClass = "table";

                    // Llena la tabla con los resultados
                    row = new TableRow();
                    cell = new TableCell();
                    cell.Text = ((Idioma)Session["oIdioma"]).Texto("Pedido");
                    cell.CssClass = "cellTitulo";
                    cell.ColumnSpan = 12;
                    row.Cells.Add(cell);
                    tabPedidos.Rows.Add(row);

                    //Encabezados del Pedido
                    row = new TableRow();
                    row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto("NoPedido")));
                    row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto("Sociedad")));
                    row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto("OrgCompras")));
                    row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto("GpoCompras")));
                    row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto("ClaseDocto")));
                    row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto("Proveedor")));
                    cell = NewCell(((Idioma)Session["oIdioma"]).Texto("NomProveedor"));
                    cell.ColumnSpan = 3;
                    row.Cells.Add(cell);
                    row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto("Estatus")));
                    row.Cells.Add(NewCell(""));
                    row.Cells.Add(NewCell(""));
                    tabPedidos.Rows.Add(row);

                    //Valores del pedido
                    row = new TableRow();
                    row.Cells.Add(NewCell(Convert.ToString(r["id_pedido"])));
                    row.Cells.Add(NewCell(Convert.ToString(r["id_sociedad"])));
                    row.Cells.Add(NewCell(Convert.ToString(r["id_orgcomp"])));
                    row.Cells.Add(NewCell(Convert.ToString(r["id_gpocomp"])));
                    row.Cells.Add(NewCell(Convert.ToString(r["id_clasedoc"])));
                    row.Cells.Add(NewCell(Convert.ToString(r["id_proveedor"])));
                    cell = NewCell(Convert.ToString(r["nombre"]));
                    cell.ColumnSpan = 3;
                    row.Cells.Add(cell);
                    row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto(Convert.ToString(oPedido.Status))));
                    row.Cells.Add(NewCell(""));
                    row.Cells.Add(NewCell(""));
                    tabPedidos.Rows.Add(row);

                    //Encabezados de Entregas
                    if (oPedido.Entregas.Tables[0].Rows.Count > 0)
                    {
                        row = new TableRow();
                        cell = new TableCell();
                        cell.Text = ((Idioma)Session["oIdioma"]).Texto("Entregas");
                        cell.CssClass = "cellSubtitulo";
                        cell.ColumnSpan = 12;
                        row.Cells.Add(cell);
                        tabPedidos.Rows.Add(row);

                        row = new TableRow();
                        row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto("NoPedido")));
                        row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto("Posicion")));
                        row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto("Entrega")));
                        row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto("PosEntrega")));
                        row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto("Cantidad")));
                        row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto("ImporteML")));
                        row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto("Importe")));
                        row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto("Moneda")));
                        row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto("Material")));
                        row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto("Descripcion")));
                        row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto("NotaEntrega")));
                        row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto("Estatus")));
                        tabPedidos.Rows.Add(row);

                        //Valores de las entregas
                        foreach (DataRow e in oPedido.Entregas.Tables[0].Rows)
                        {
                            row = new TableRow();
                            row.Cells.Add(NewCell(Convert.ToString(e["id_pedido"])));
                            row.Cells.Add(NewCell(Convert.ToString(e["id_pos_ped"])));
                            row.Cells.Add(NewCell(Convert.ToString(e["id_entrega"])));
                            row.Cells.Add(NewCell(Convert.ToString(e["id_posicion"])));
                            row.Cells.Add(NewCell(Convert.ToString(e["cantidad"])));
                            cell = NewCell(String.Format("{0:n2}", Convert.ToDouble(e["importeML"])));
                            cell.HorizontalAlign = HorizontalAlign.Right;
                            row.Cells.Add(cell);
                            cell = NewCell(String.Format("{0:n2}", Convert.ToDouble(e["importe"])));
                            cell.HorizontalAlign = HorizontalAlign.Right;
                            row.Cells.Add(cell);
                            row.Cells.Add(NewCell(Convert.ToString(e["moneda"])));
                            row.Cells.Add(NewCell(Convert.ToString(e["id_material"])));
                            row.Cells.Add(NewCell(Convert.ToString(e["descripcion"])));
                            row.Cells.Add(NewCell(Convert.ToString(e["nota_entrega"])));
                            row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto(Convert.ToString(e["status"]))));
                            tabPedidos.Rows.Add(row);
                        }
                    }

                    if (oPedido.Costos.Tables[0].Rows.Count > 0)
                    {
                        row = new TableRow();
                        cell = new TableCell();
                        cell.Text = ((Idioma)Session["oIdioma"]).Texto("CostosInd");
                        cell.CssClass = "cellSubtitulo";
                        cell.ColumnSpan = 12;
                        row.Cells.Add(cell);
                        tabPedidos.Rows.Add(row);

                        //Encabezados de Costos indirectos 
                        row = new TableRow();
                        row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto("NoPedido")));
                        row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto("Posicion")));
                        row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto("DocRefer")));
                        row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto("Posicion")));
                        row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto("Cantidad")));
                        row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto("ImporteML")));
                        row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto("Importe")));
                        row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto("Moneda")));
                        row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto("Proveedor")));
                        row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto("TipoCond")));
                        row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto("NotaEntrega")));
                        row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto("Estatus")));
                        tabPedidos.Rows.Add(row);

                        //Valores de las entregas
                        foreach (DataRow c in oPedido.Costos.Tables[0].Rows)
                        {
                            row = new TableRow();
                            row.Cells.Add(NewCell(Convert.ToString(c["id_pedido"])));
                            row.Cells.Add(NewCell(Convert.ToString(c["id_pos_ped"])));
                            row.Cells.Add(NewCell(Convert.ToString(c["id_entrega"])));
                            row.Cells.Add(NewCell(Convert.ToString(c["id_posicion"])));
                            row.Cells.Add(NewCell(Convert.ToString(c["cantidad"])));
                            cell = NewCell(String.Format("{0:n2}", Convert.ToDouble(c["importeML"])));
                            cell.HorizontalAlign = HorizontalAlign.Right;
                            row.Cells.Add(cell);
                            cell = NewCell(String.Format("{0:n2}", Convert.ToDouble(c["importe"])));
                            cell.HorizontalAlign = HorizontalAlign.Right;
                            row.Cells.Add(cell);
                            row.Cells.Add(NewCell(Convert.ToString(c["moneda"])));
                            row.Cells.Add(NewCell(Convert.ToString(c["id_proveedor"])));
                            row.Cells.Add(NewCell(Convert.ToString(c["id_tipoCond"])));
                            row.Cells.Add(NewCell(Convert.ToString(c["ref_docto"])));
                            row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto(Convert.ToString(c["status"]))));
                            tabPedidos.Rows.Add(row);
                        }
                    }

                    //Encabezados de Servicios
                    if (oPedido.Servicios.Tables[0].Rows.Count > 0)
                    {
                        row = new TableRow();
                        cell = new TableCell();
                        cell.Text = ((Idioma)Session["oIdioma"]).Texto("Servicios");
                        cell.CssClass = "cellSubtitulo";
                        cell.ColumnSpan = 12;
                        row.Cells.Add(cell);
                        tabPedidos.Rows.Add(row);

                        row = new TableRow();
                        row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto("NoPedido")));
                        row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto("Posicion")));
                        row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto("Documento")));
                        row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto("Posicion")));
                        row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto("Cantidad")));
                        row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto("ImporteML")));
                        row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto("Importe")));
                        row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto("Moneda")));
                        row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto("Material")));
                        row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto("Descripcion")));
                        row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto("Referencia")));
                        row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto("Estatus")));
                        tabPedidos.Rows.Add(row);

                        //Valores de las entregas
                        foreach (DataRow e in oPedido.Servicios.Tables[0].Rows)
                        {
                            row = new TableRow();
                            row.Cells.Add(NewCell(Convert.ToString(e["id_pedido"])));
                            row.Cells.Add(NewCell(Convert.ToString(e["id_pos_ped"])));
                            row.Cells.Add(NewCell(Convert.ToString(e["id_documento"])));
                            row.Cells.Add(NewCell(Convert.ToString(e["id_posicion"])));
                            row.Cells.Add(NewCell(Convert.ToString(e["cantidad"])));
                            cell = NewCell(String.Format("{0:n2}", Convert.ToDouble(e["importeML"])));
                            cell.HorizontalAlign = HorizontalAlign.Right;
                            row.Cells.Add(cell);
                            cell = NewCell(String.Format("{0:n2}", Convert.ToDouble(e["importe"])));
                            cell.HorizontalAlign = HorizontalAlign.Right;
                            row.Cells.Add(cell);
                            row.Cells.Add(NewCell(Convert.ToString(e["moneda"])));
                            row.Cells.Add(NewCell(Convert.ToString(e["material"])));
                            row.Cells.Add(NewCell(Convert.ToString(e["descripcion"])));
                            row.Cells.Add(NewCell(Convert.ToString(e["ref_docto"])));
                            row.Cells.Add(NewCell(((Idioma)Session["oIdioma"]).Texto(Convert.ToString(e["status"]))));
                            tabPedidos.Rows.Add(row);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox(null, null, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }

        protected void btnExportar_Click(object sender, ImageClickEventArgs e)
        {
            int ren = 0;
            try
            {
                ValidaVariables();

                NegocioPF.Pedidos oPedidos = new NegocioPF.Pedidos();

                NegocioPF.Proveedor oProveedor = new NegocioPF.Proveedor(((Usuario)Session["oUsuario"]).Id);
                oProveedor.Cargar();
                if (oProveedor.Nombre != "" && oProveedor.Nombre != null)
                    oPedidos.Cargar(oProveedor.Id, txtOrden.Text, "Pendiente");
                else
                    oPedidos.Cargar(txtProveedor.Text, txtOrden.Text, "");

                ArmarArchivo(ref oPedidos);


                ////string filename = Server.MapPath("") + "\\Facturas\\Pedidos.xls";
                //FileInfo newFile = new FileInfo(Server.MapPath("") + @"\sample1.xlsx");
                //if (newFile.Exists)
                //{
                //    newFile.Delete();  // ensures we create a new workbook
                //    newFile = new FileInfo(Server.MapPath("") + @"\sample1.xlsx");
                //}
                //using (ExcelPackage package = new ExcelPackage(newFile))
                //{
                //    // add a new worksheet to the empty workbook
                //    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Pedidos");

                //    //Add the headers
                //    worksheet.Cells[1, 1].Value = "ID";
                //    worksheet.Cells[1, 2].Value = "Product";
                //    worksheet.Cells[1, 3].Value = "Quantity";
                //    worksheet.Cells[1, 4].Value = "Price";
                //    worksheet.Cells[1, 5].Value = "Value";

                //    //Add some items...
                //    worksheet.Cells["A2"].Value = 12001;
                //    worksheet.Cells["B2"].Value = "Nails";
                //    worksheet.Cells["C2"].Value = 37;
                //    worksheet.Cells["D2"].Value = 3.99;

                //    worksheet.Cells["A3"].Value = 12002;
                //    worksheet.Cells["B3"].Value = "Hammer";
                //    worksheet.Cells["C3"].Value = 5;
                //    worksheet.Cells["D3"].Value = 12.10;

                //    worksheet.Cells["A4"].Value = 12003;
                //    worksheet.Cells["B4"].Value = "Saw";
                //    worksheet.Cells["C4"].Value = 12;
                //    worksheet.Cells["D4"].Value = 15.37;

                //    //Add a formula for the value-column
                //    worksheet.Cells["E2:E4"].Formula = "C2*D2";

                //    //Ok now format the values;
                //    using (var range = worksheet.Cells[1, 1, 1, 5])
                //    {
                //        range.Style.Font.Bold = true;
                //        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                //        range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DarkBlue);
                //        range.Style.Font.Color.SetColor(System.Drawing.Color.White);
                //    }

                //    worksheet.Cells["A5:E5"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                //    worksheet.Cells["A5:E5"].Style.Font.Bold = true;

                //    worksheet.Cells[5, 3, 5, 5].Formula = string.Format("SUBTOTAL(9,{0})", new ExcelAddress(2, 3, 4, 3).Address);
                //    worksheet.Cells["C2:C5"].Style.Numberformat.Format = "#,##0";
                //    worksheet.Cells["D2:E5"].Style.Numberformat.Format = "#,##0.00";

                //    //Create an autofilter for the range
                //    worksheet.Cells["A1:E4"].AutoFilter = true;

                //    worksheet.Cells["A2:A4"].Style.Numberformat.Format = "@";   //Format as text

                //    //There is actually no need to calculate, Excel will do it for you, but in some cases it might be useful. 
                //    //For example if you link to this workbook from another workbook or you will open the workbook in a program that hasn't a calculation engine or 
                //    //you want to use the result of a formula in your program.
                //    worksheet.Calculate();

                //    worksheet.Cells.AutoFitColumns(0);  //Autofit columns for all cells

                //    // lets set the header text 
                //    worksheet.HeaderFooter.OddHeader.CenteredText = "&24&U&\"Arial,Regular Bold\" Inventory";
                //    // add the page number to the footer plus the total number of pages
                //    worksheet.HeaderFooter.OddFooter.RightAlignedText =
                //        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);
                //    // add the sheet name to the footer
                //    worksheet.HeaderFooter.OddFooter.CenteredText = ExcelHeaderFooter.SheetName;
                //    // add the file path to the footer
                //    worksheet.HeaderFooter.OddFooter.LeftAlignedText = ExcelHeaderFooter.FilePath + ExcelHeaderFooter.FileName;

                //    worksheet.PrinterSettings.RepeatRows = worksheet.Cells["1:2"];
                //    worksheet.PrinterSettings.RepeatColumns = worksheet.Cells["A:G"];

                //    // Change the sheet view to show it in page layout mode
                //    worksheet.View.PageLayoutView = true;

                //    // set some document properties
                //    package.Workbook.Properties.Title = "Invertory";
                //    package.Workbook.Properties.Author = "Jan Källman";
                //    package.Workbook.Properties.Comments = "This sample demonstrates how to create an Excel 2007 workbook using EPPlus";

                //    // set some extended property values
                //    package.Workbook.Properties.Company = "AdventureWorks Inc.";

                //    // set some custom property values
                //    package.Workbook.Properties.SetCustomPropertyValue("Checked by", "Jan Källman");
                //    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", "EPPlus");
                //    // save our new workbook and we are done!
                //    package.Save();

                //}

                //return newFile.FullName;




            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }

        private void ArmarArchivo(ref NegocioPF.Pedidos oPedidos)
        {
            string path;
            int ren=0;
            try
            {

                path = Server.MapPath("") + @"\\Facturas\\Pedidos.xlsx";
                FileInfo newFile = new FileInfo(path);
                if (newFile.Exists)
                {
                    newFile.Delete();  // ensures we create a new workbook
                    newFile = new FileInfo(path);
                }
                using (ExcelPackage package = new ExcelPackage(newFile))
                {
                    // add a new worksheet to the empty workbook
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Pedidos");

                    foreach (DataRow r in oPedidos.Datos.Tables[0].Rows)
                    {
                        NegocioPF.Pedido oPedido = new NegocioPF.Pedido(r["id_sociedad"].ToString(), r["id_pedido"].ToString());
                        oPedido.Cargar();

                        //Add the headers
                        ren++;
                        worksheet.Cells[ren, 1].Value = ((Idioma)Session["oIdioma"]).Texto("Pedido");
                        using (var range = worksheet.Cells[ren, 1, ren, 10])
                        {
                            range.Style.Font.Bold = true;
                            range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Silver);
                            //range.Style.Font.Color.SetColor(System.Drawing.Color.White);
                        }

                        //Agrega los datos del pedido
                        ren++;
                        worksheet.Cells[ren, 1].Value = ((Idioma)Session["oIdioma"]).Texto("NoPedido");
                        worksheet.Cells[ren, 2].Value = ((Idioma)Session["oIdioma"]).Texto("Sociedad");
                        worksheet.Cells[ren, 3].Value = ((Idioma)Session["oIdioma"]).Texto("OrgCompras");
                        worksheet.Cells[ren, 4].Value = ((Idioma)Session["oIdioma"]).Texto("GpoCompras");
                        worksheet.Cells[ren, 5].Value = ((Idioma)Session["oIdioma"]).Texto("ClaseDocto");
                        worksheet.Cells[ren, 6].Value = ((Idioma)Session["oIdioma"]).Texto("Proveedor");
                        worksheet.Cells[ren, 7].Value = ((Idioma)Session["oIdioma"]).Texto("NomProveedor");
                        using (var range = worksheet.Cells[ren, 7, ren, 9])
                        {
                            range.Merge = true;
                        }
                        worksheet.Cells[ren, 10].Value = ((Idioma)Session["oIdioma"]).Texto("Estatus");

                        using (var range = worksheet.Cells[ren, 1, ren, 10])
                        {
                            range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.WhiteSmoke);
                        }

                        //Agrega los datos del pedido
                        ren++;
                        worksheet.Cells[ren, 1].Value = Convert.ToString(r["id_pedido"]);
                        worksheet.Cells[ren, 2].Value = Convert.ToString(r["id_sociedad"]);
                        worksheet.Cells[ren, 3].Value = Convert.ToString(r["id_orgcomp"]);
                        worksheet.Cells[ren, 4].Value = Convert.ToString(r["id_gpocomp"]);
                        worksheet.Cells[ren, 5].Value = Convert.ToString(r["id_clasedoc"]);
                        worksheet.Cells[ren, 6].Value = Convert.ToString(r["id_proveedor"]);
                        worksheet.Cells[ren, 7].Value = Convert.ToString(r["nombre"]);
                        using (var range = worksheet.Cells[ren, 7, ren, 9])
                        {
                            range.Merge = true;
                        }
                        worksheet.Cells[ren, 10].Value = ((Idioma)Session["oIdioma"]).Texto(oPedido.Status);

                        //Encabezados de Entregas
                        if (oPedido.Entregas.Tables[0].Rows.Count > 0)
                        {
                            ren++;
                            worksheet.Cells[ren, 1].Value = ((Idioma)Session["oIdioma"]).Texto("Entregas");
                            using (var range = worksheet.Cells[ren, 1, ren, 10])
                            {
                                range.Style.Font.Bold = true;
                                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Lavender);
                                range.Merge = true;
                                //range.Style.Font.Color.SetColor(System.Drawing.Color.White);
                            }

                            ren++;
                            worksheet.Cells[ren, 1].Value = ((Idioma)Session["oIdioma"]).Texto("NoPedido");
                            worksheet.Cells[ren, 2].Value = ((Idioma)Session["oIdioma"]).Texto("Posicion");
                            worksheet.Cells[ren, 3].Value = ((Idioma)Session["oIdioma"]).Texto("Entrega");
                            worksheet.Cells[ren, 4].Value = ((Idioma)Session["oIdioma"]).Texto("PosEntrega");
                            worksheet.Cells[ren, 5].Value = ((Idioma)Session["oIdioma"]).Texto("Cantidad");
                            worksheet.Cells[ren, 6].Value = ((Idioma)Session["oIdioma"]).Texto("Importe");
                            worksheet.Cells[ren, 7].Value = ((Idioma)Session["oIdioma"]).Texto("Material");
                            worksheet.Cells[ren, 8].Value = ((Idioma)Session["oIdioma"]).Texto("Descripcion");
                            worksheet.Cells[ren, 9].Value = ((Idioma)Session["oIdioma"]).Texto("NotaEntrega");
                            worksheet.Cells[ren, 10].Value = ((Idioma)Session["oIdioma"]).Texto("Estatus");

                            using (var range = worksheet.Cells[ren, 1, ren, 10])
                            {
                                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.WhiteSmoke);
                            }

                            //Valores de las entregas
                            foreach (DataRow e in oPedido.Entregas.Tables[0].Rows)
                            {
                                ren++;
                                worksheet.Cells[ren, 1].Value = Convert.ToString(e["id_pedido"]);
                                worksheet.Cells[ren, 2].Value = Convert.ToString(e["id_pos_ped"]);
                                worksheet.Cells[ren, 3].Value = Convert.ToString(e["id_entrega"]);
                                worksheet.Cells[ren, 4].Value = Convert.ToString(e["id_posicion"]);
                                worksheet.Cells[ren, 5].Value = Convert.ToInt32(e["cantidad"]);
                                worksheet.Cells[ren, 5].Style.Numberformat.Format = "#,##0";
                                worksheet.Cells[ren, 6].Value = Convert.ToDouble(e["importe"]); //String.Format("{0:n2}", Convert.ToDouble(e["importe"]));
                                worksheet.Cells[ren, 6].Style.Numberformat.Format = "#,##0.00"; 
                                worksheet.Cells[ren, 7].Value = Convert.ToString(e["id_material"]);
                                worksheet.Cells[ren, 8].Value = Convert.ToString(e["descripcion"]);
                                worksheet.Cells[ren, 9].Value = Convert.ToString(e["nota_entrega"]);
                                worksheet.Cells[ren, 10].Value = ((Idioma)Session["oIdioma"]).Texto(Convert.ToString(e["status"]));
                            }
                        }

                        if (oPedido.Costos.Tables[0].Rows.Count > 0)
                        {
                            ren++;
                            worksheet.Cells[ren, 1].Value =  ((Idioma)Session["oIdioma"]).Texto("CostosInd");
                            using (var range = worksheet.Cells[ren, 1, ren, 10])
                            {
                                range.Style.Font.Bold = true;
                                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Lavender);
                                //range.Style.Font.Color.SetColor(System.Drawing.Color.White);
                            }

                            //Encabezados de Costos indirectos 
                            ren++;
                            worksheet.Cells[ren, 1].Value = ((Idioma)Session["oIdioma"]).Texto("NoPedido");
                            worksheet.Cells[ren, 2].Value = ((Idioma)Session["oIdioma"]).Texto("Posicion");
                            worksheet.Cells[ren, 3].Value = ((Idioma)Session["oIdioma"]).Texto("DocRefer");
                            worksheet.Cells[ren, 4].Value = ((Idioma)Session["oIdioma"]).Texto("Posicion");
                            worksheet.Cells[ren, 5].Value = ((Idioma)Session["oIdioma"]).Texto("Cantidad");
                            worksheet.Cells[ren, 6].Value = ((Idioma)Session["oIdioma"]).Texto("Importe");
                            worksheet.Cells[ren, 7].Value = ((Idioma)Session["oIdioma"]).Texto("Proveedor");
                            worksheet.Cells[ren, 8].Value = ((Idioma)Session["oIdioma"]).Texto("TipoCond");
                            worksheet.Cells[ren, 9].Value = ((Idioma)Session["oIdioma"]).Texto("NotaEntrega");
                            worksheet.Cells[ren, 10].Value = ((Idioma)Session["oIdioma"]).Texto("Estatus");

                            using (var range = worksheet.Cells[ren, 1, ren, 10])
                            {
                                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.WhiteSmoke);
                            }
                            
                            //Valores de los costos
                            foreach (DataRow c in oPedido.Costos.Tables[0].Rows)
                            {
                                ren++;
                                worksheet.Cells[ren, 1].Value = Convert.ToString(c["id_pedido"]);
                                worksheet.Cells[ren, 2].Value = Convert.ToString(c["id_pos_ped"]);
                                worksheet.Cells[ren, 3].Value = Convert.ToString(c["id_entrega"]);
                                worksheet.Cells[ren, 4].Value = Convert.ToString(c["id_posicion"]);
                                worksheet.Cells[ren, 5].Value = Convert.ToInt32(c["cantidad"]);
                                worksheet.Cells[ren, 5].Style.Numberformat.Format = "#,##0";
                                worksheet.Cells[ren, 6].Value = Convert.ToDouble(c["importe"]);
                                worksheet.Cells[ren, 6].Style.Numberformat.Format = "#,##0.00";
                                worksheet.Cells[ren, 7].Value = Convert.ToString(c["id_proveedor"]);
                                worksheet.Cells[ren, 8].Value = Convert.ToString(c["id_tipoCond"]);
                                worksheet.Cells[ren, 9].Value = Convert.ToString(c["ref_docto"]);
                                worksheet.Cells[ren, 10].Value = ((Idioma)Session["oIdioma"]).Texto(Convert.ToString(c["status"]));
                            }
                        }

                    }

                    //Autofit columns for all cells
                    worksheet.Cells.AutoFitColumns(0);  

                    // set some document properties
                    package.Workbook.Properties.Title = "Pedidos";
                    //package.Workbook.Properties.Author = "Jan Källman";
                    //package.Workbook.Properties.Comments = "This sample demonstrates how to create an Excel 2007 workbook using EPPlus";

                    //// set some extended property values
                    //package.Workbook.Properties.Company = "AdventureWorks Inc.";

                    //// set some custom property values
                    //package.Workbook.Properties.SetCustomPropertyValue("Checked by", "Jan Källman");
                    //package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", "EPPlus");
                    //// save our new workbook and we are done!
                    package.Save();

                }



                if (ren > 0)
                {
                    //string txtPath = dirDestino + nomFile;
                    Response.ContentType = "application/vnd.ms-excel"; // "text/plain";
                    Response.AppendHeader("content-disposition",
                            "attachment; filename=" + "Pedidos.xls");
                    Response.TransmitFile(path);
                    Response.End();
                }

            }
            catch (Exception ex)
            {
                MessageBox(null, null, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }

        private TableCell NewCell(string valor)
        {
            TableCell cell = new TableCell();
            cell.Text = valor;
            return cell;
        }

        private void EstableceIdioma(Idioma oIdioma)
        {
            lblTitConsPed.Text = oIdioma.Texto("ConsStatusPed");
            lblProveedor.Text = oIdioma.Texto("Proveedor") + ":";
            lblOrden.Text = oIdioma.Texto("Pedido") + ":";

            EstableceIdiomaBotones(oIdioma.Id, this.Controls);
        }

        private void AgregaScriptCliente()
        {
            try
            {
                string codigo;

                codigo = "$(\"[src*=plus]\").live(\"click\", function () { ";
                codigo += "$(this).closest(\"tr\").after(\"<tr><td></td><td colspan = '999'>\" + $(this).next().html() + \"</td></tr>\"); ";
                codigo += "$(this).attr(\"src\", \"images/minus.png\"); ";
                codigo += "}); ";
                codigo += "$(\"[src*=minus]\").live(\"click\", function () { ";
                codigo += "$(this).attr(\"src\", \"images/plus.png\"); ";
                codigo += "$(this).closest(\"tr\").next().remove(); ";
                codigo += "}); ";

                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Funcion", codigo, true);

            }
            catch (Exception ex)
            {
                MessageBox(null, null, ex.Message);
            }
        }

    }


}
