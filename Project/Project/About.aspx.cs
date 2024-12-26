using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Browser.IsMobileDevice)
            {
                string script = @"
                    document.getElementById('rb1').style.height='250px';        
                    document.getElementById('rb2').style.height='250px';
                ";
                ClientScript.RegisterStartupScript(this.GetType(), "MobileDeviceAdjust", script, true);
            }
        }

        protected void Button1_Click(object sender,EventArgs e) {
            Response.Redirect("Default");
        }
    }
}