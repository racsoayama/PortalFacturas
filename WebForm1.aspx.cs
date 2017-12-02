using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace PortalFacturas
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
//                BindGrid();
            }
        }



        protected void Upload(object sender, EventArgs e)
        {
            string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string contentType = FileUpload1.PostedFile.ContentType;
            using (Stream fs = FileUpload1.PostedFile.InputStream)
           {
                using (BinaryReader br = new BinaryReader(fs))
               {
                    byte[] bytes = br.ReadBytes((Int32)fs.Length);
                   // string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
                   // using (SqlConnection con = new SqlConnection(constr))
                   //{
                   //     string query = "insert into tblFiles values (@Name, @ContentType, @Data)";
                   //     using (SqlCommand cmd = new SqlCommand(query))
                   //     {
                   //         cmd.Connection = con;
                   //         cmd.Parameters.AddWithValue("@Name", filename);
                   //         cmd.Parameters.AddWithValue("@ContentType", contentType);
                   //         cmd.Parameters.AddWithValue("@Data", bytes);
                   //         con.Open();
                   //         cmd.ExecuteNonQuery();
                   //         con.Close();
                   //     }
                   // }


                }
            }
            Response.Redirect(Request.Url.AbsoluteUri);

        }
    }


}