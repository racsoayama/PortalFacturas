using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Drawing;
using System.Web.UI.WebControls;

namespace PortalFacturas
{
    public partial class Captcha : System.Web.UI.Page
    {
        private void Page_Load(object sender, System.EventArgs e)
        {
            Bitmap objBMP = new System.Drawing.Bitmap(60, 20);
            Graphics objGraphics = System.Drawing.Graphics.FromImage(objBMP);
            objGraphics.Clear(Color.Gray);

            objGraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            //' Configure font to use for text
            Font objFont = new Font("Arial", 10, FontStyle.Bold);
            string randomStr = "";
            char[] myIntArray = new char[5];
            int x;

            //That is to create the random # and add it to our string
            Random autoRand = new Random();

            for (x = 0; x < 5; x++)
            {
                int z = autoRand.Next(0, 4);  

                 if (z == 1)  
                     myIntArray[x] = System.Convert.ToChar(autoRand.Next(65, 90));  
                 else if (z == 2)
                     myIntArray[x] = System.Convert.ToChar(autoRand.Next(97, 122));  
                 else
                    myIntArray[x] = System.Convert.ToChar(autoRand.Next(49, 57));

                randomStr += (myIntArray[x].ToString());
            }

            //This is to add the string to session cookie, to be compared later
            Session.Add("randomStr", randomStr);

            //' Write out the text
            objGraphics.DrawString(randomStr, objFont, Brushes.White, 3, 3);

            //' Set the content type and return the image
            Response.ContentType = "image/GIF";
            objBMP.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);

            objFont.Dispose();
            objGraphics.Dispose();
            objBMP.Dispose();
        }

    }
}