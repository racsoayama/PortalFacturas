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
using System.Xml.Linq;
using NegocioPF;

namespace PortalFacturas
{
    public partial class Prueba : FormaPadre
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    //ValidaVariables();
                    //EstableceIdioma((Idioma)Session["oIdioma"]);

                    //NegocioPF.Proveedores oProveedores = new NegocioPF.Proveedores();
                    //oProveedores.Cargar();
                    //CheckBoxList1.DataSource = oProveedores.Datos;
                    //CheckBoxList1.DataTextField = "Nombre";
                    //CheckBoxList1.DataValueField = "id_proveedor";
                    //CheckBoxList1.DataBind();
                    //CheckBoxList1.Items.Insert(0, new ListItem(((Idioma)Session["oIdioma"]).Texto("Seleccionar") + " ...", "0"));

                    //Si el usuario es un usuario del proveedor, selecciona el proveedor 
                    NegocioPF.Proveedor oProveedor = new NegocioPF.Proveedor(((Usuario)Session["oUsuario"]).Id);
                    oProveedor.Cargar();
                    //if (oProveedor.Nombre != "" && oProveedor.Nombre != null)
                    //{
                    //    divFiltros.Visible = false;
                    //    btnGenerar_Click(null, null);
                    //}
                    //else
                    //{
                    //    divReporte.Visible = false;
                    //}
                }
                catch (Exception ex)
                {
                    //MessageBox(sender, e, ((Idioma)Session["oIdioma"]).Texto(ex.Message));
                }
            }

        }

        protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string name = "";
            //for (int i = 0; i < CheckBoxList1.Items.Count; i++)
            //{
            //    if (CheckBoxList1.Items[i].Selected)
            //    {
            //        name += CheckBoxList1.Items[i].Text + ",";
            //    }
            //}
            //TextBox1.Text = name;
        }

    }
}
