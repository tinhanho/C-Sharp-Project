using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.UI;
using System.Web.Services;
using System.Web.UI.WebControls;
using System.Web.Http;
using System.Diagnostics;

namespace Project
{
    public partial class _Default : Page
    {
        static int myctrl = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (myctrl==0) 
            {

                Label1.Visible = false;
                Button1.Visible = true;
                Button2.Visible = true;
                Button3.Visible = false;
            }
            myctrl = 1;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Button1.Visible = false;
            Button2.Visible = false;
            Button3.Visible = true;
            Label1.Text = "0";
            Label1.Visible = true;
            string script = @"
                showBox();

                document.addEventListener(""keydown"", handleKeydown);

                document.addEventListener(""keyup"", handleKeyUp);

                s1 = setInterval(moveRedBoxes, 20);
                s2 = setInterval(generateRedBox, 1000);
                s3 = setInterval(CheckIsAlive, 5);
                s4 = setInterval(updateTime, 100); // 每 1 秒執行一次
            ";
            ClientScript.RegisterStartupScript(this.GetType(), "GameStartScript", script, true);

        }
        protected void Button2_Click(object sender, EventArgs e)
        {

        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            myctrl = 0;
            Page_Load(sender, e);
        }

        [WebMethod(EnableSession = true)]
        public static string SendLabelDataToBackend(string labelValue)
        {
            Debug.WriteLine("進入方法");

            return "收到的數據是: " + labelValue;
        }
    }
}