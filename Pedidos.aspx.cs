using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NegocioPF;

namespace PortalFacturas
{
    public partial class Pedidos : FormaPadre
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ValidaVariables();
                    EstableceIdioma((Idioma)Session["oIdioma"]);

                    //Si el usuario es un usuario del proveedor, por defautl se muestra sus pedidos 
                    NegocioPF.Proveedor oProveedor = new NegocioPF.Proveedor(((Usuario)Session["oUsuario"]).Id);
                    oProveedor.Cargar();
                    NegocioPF.Pedidos oPedidos = new NegocioPF.Pedidos();
                    if (oProveedor.Nombre != "" && oProveedor.Nombre != null)
                    {
                        oPedidos.Cargar(((Usuario)Session["oUsuario"]).Id, "", "Pendiente");
                        txtFilProv.Visible = false;
                        lblFilProv.Visible = false;
                    }
                    else
                        oPedidos.Cargar();

                    grdPedidos.DataSource = oPedidos.Datos;
                    grdPedidos.DataBind();

                    Perfil oPerfil = new Perfil();
                    Permisos permisos = oPerfil.CargarPermisos(((Usuario)Session["oUsuario"]).Id, "Pedidos.aspx");
                    btnImportar.Visible = (permisos.Importar);

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

        protected void grdPedidos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int index;
            try
            {
                ValidaVariables();

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //Recupera la clave del pedido
                    index = e.Row.RowIndex;

                    NegocioPF.Pedido oPedido = new NegocioPF.Pedido(Convert.ToString(grdPedidos.DataKeys[e.Row.RowIndex].Values[0]),
                                                                   Convert.ToString(grdPedidos.DataKeys[e.Row.RowIndex].Values[1]));
                    oPedido.Cargar();

                    GridView grdDetalle = e.Row.FindControl("grdDetalle") as GridView;

                    foreach (DataControlField c in grdDetalle.Columns)
                    {
                        c.HeaderText = ((Idioma)Session["oIdioma"]).Texto(c.HeaderText);
                    }

                    grdDetalle.DataSource = oPedido.Materiales;
                    grdDetalle.DataBind();


                    //string customerId = gvCustomers.DataKeys[e.Row.RowIndex].Value.ToString();
                    //GridView gvOrders = e.Row.FindControl("gvOrders") as GridView;
                    //gvOrders.DataSource = GetData(string.Format("select top 3 * from Orders where CustomerId='{0}'", customerId));
                    //gvOrders.DataBind();

                }
            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }

        protected void grdPedidos_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            try
            {
                ValidaVariables();
                grdPedidos.PageIndex = e.NewPageIndex;

                //Si el usuario es un usuario del proveedor, por defautl se muestra sus pedidos 
                NegocioPF.Proveedor oProveedor = new NegocioPF.Proveedor(((Usuario)Session["oUsuario"]).Id);
                oProveedor.Cargar();
                NegocioPF.Pedidos oPedidos = new NegocioPF.Pedidos();
                if (oProveedor.Nombre != "" && oProveedor.Nombre != null)
                    oPedidos.Cargar(((Usuario)Session["oUsuario"]).Id, txtFilPedido.Text, "Pendiente");
                else
                    oPedidos.Cargar(txtFilProv.Text, txtFilPedido.Text, "");
                grdPedidos.DataSource = oPedidos.Datos;
                grdPedidos.DataBind();
            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }

        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ValidaVariables();
                //Carga los datos del producto
                NegocioPF.Proveedor oProveedor = new NegocioPF.Proveedor(((Usuario)Session["oUsuario"]).Id);
                oProveedor.Cargar();
                NegocioPF.Pedidos oPedidos = new NegocioPF.Pedidos();
                if (oProveedor.Nombre != "" && oProveedor.Nombre != null)
                    oPedidos.Cargar(((Usuario)Session["oUsuario"]).Id, txtFilPedido.Text, "Pendiente");
                else
                    oPedidos.Cargar(txtFilProv.Text, txtFilPedido.Text, "");
                grdPedidos.DataSource = oPedidos.Datos;
                grdPedidos.DataBind();

            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }

        protected void btnMostrarTodos_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ValidaVariables();
                txtFilProv.Text = "";
                txtFilPedido.Text = "";

                NegocioPF.Proveedor oProveedor = new NegocioPF.Proveedor(((Usuario)Session["oUsuario"]).Id);
                oProveedor.Cargar();
                NegocioPF.Pedidos oPedidos = new NegocioPF.Pedidos();
                if (oProveedor.Nombre != "" && oProveedor.Nombre != null)
                    oPedidos.Cargar(((Usuario)Session["oUsuario"]).Id, txtFilPedido.Text, "Pendiente");
                else
                    oPedidos.Cargar(txtFilProv.Text, txtFilPedido.Text, "");
                grdPedidos.DataSource = oPedidos.Datos;
                grdPedidos.DataBind();
            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }
        }

        protected void btnImportar_Click(object sender, ImageClickEventArgs e)
        {
            divImportar.Visible = true;
        }

        protected void btnAceptarImportar_Click(object sender, ImageClickEventArgs e)
        {
            int registros = 0;
            string sPathArchCab;
            string sPathArchDet;
            try
            {
                ValidaVariables();

                if ((File1.PostedFile == null) || (File1.PostedFile.ContentLength == 0))
                    throw new Exception ("MsgSelArchHdr");

                if ((File2.PostedFile == null) || (File2.PostedFile.ContentLength == 0))
                    throw new Exception ("MsgSelArchDet");

                //Copia el archivo de encabezados
                string fn = System.IO.Path.GetFileName(File1.PostedFile.FileName);

                sPathArchCab = Server.MapPath("") + "\\Data\\PedidosHdr." + fn.Substring(fn.Length - 3, 3);

                try
                {
                    File1.PostedFile.SaveAs(sPathArchCab);
                }
                catch (Exception ex)
                {
                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgErrorCopiarArchivo"));
                }

                //Copia el archivo de detalles
                fn = System.IO.Path.GetFileName(File2.PostedFile.FileName);

                sPathArchDet = Server.MapPath("") + "\\Data\\PedidosDet." + fn.Substring(fn.Length - 3, 3);

                try
                {
                    File2.PostedFile.SaveAs(sPathArchDet);
                }
                catch (Exception ex)
                {
                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgErrorCopiarArchivo"));
                }


                try
                {
                    NegocioPF.Pedidos oPedidos = new NegocioPF.Pedidos();

                    //if (fn.Substring(fn.Length - 3, 3).ToLower() == "xls")
                    //{
                    //    registros = oPedidos.ImportarExcel(((Usuario)Session["oUsuario"]).Id, SaveLocation);
                    //}
                    //else
                    //{
                    registros = oPedidos.ImportarTXT(((Usuario)Session["oUsuario"]).Id, sPathArchCab, sPathArchDet, 
                        System.IO.Path.GetFileName(File1.PostedFile.FileName),
                        System.IO.Path.GetFileName(File2.PostedFile.FileName));
                    //}

                    oPedidos.Cargar();
                    grdPedidos.DataSource = oPedidos.Datos;
                    grdPedidos.DataBind();

                    divImportar.Visible = false;

                    MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto("MsgRegActInsertados") + registros.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox(sender, e, "Error:" + ((Idioma)Session["oIdioma"]).Texto(ex.Message));
                }

            }
            catch (Exception ex)
            {
                MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
            }

        }

        protected void btnCancelarImportar_Click(object sender, ImageClickEventArgs e)
        {
            divImportar.Visible = false;
        }

        private void EstableceIdioma(Idioma oIdioma)
        {
            lblTitPedidos.Text = oIdioma.Texto("Pedidos");
            lblFilProv.Text = oIdioma.Texto("NumProveedor");
            lblFilPedido.Text = oIdioma.Texto("Pedido") + ":";
            lblLeyArchivo.Text = oIdioma.Texto("MsgSelArchivos") + ":";

            foreach (DataControlField c in grdPedidos.Columns)
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