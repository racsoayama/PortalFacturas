using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace PortalFacturas
{
    public partial class VisorPDF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string archivo = "Facturas/" + Session["archivo"].ToString();
            string archivo = Session["archivo"].ToString();
            oViewer.Attributes.Add("src", archivo);
            oViewer.Visible = true;
        }
    }
}
