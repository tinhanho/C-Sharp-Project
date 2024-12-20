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
using System.Reflection.Emit;
using System.Xml;
using System.Data.SqlClient;
using Microsoft.Ajax.Utilities;

namespace Project
{
    public partial class _Default : Page
    {
        public static bool myctrl = true;
        static string playerScore = "";
        static string nickname = "";
        static bool dropWitoutNickName = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (myctrl) 
            {

                Label1.Visible = false;
                Button1.Visible = true;
                Button2.Visible = true;
                Button3.Visible = false;
            }
            myctrl = false;
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
        
        //Ranking
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ranking");
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            myctrl = true;
            dropWitoutNickName = true;
            Page_Load(sender, e);
        }

        [WebMethod(EnableSession = true)]
        public static string SendLabelDataToBackend(string labelValue)
        {
            playerScore = labelValue;
            Debug.WriteLine(playerScore);
            return "收到的數據是: " + labelValue;
        }

        //Nickname sending button
        protected void Button4_Click(object sender, EventArgs e)
        {
            myctrl = true;
            nickname = TextBox1.Text;
            Debug.WriteLine(TextBox1.Text);
            Page_Load(sender, e);

            if (nickname == "") nickname = "匿名";

            if (!dropWitoutNickName)
            {
                using (SqlConnection cn = new SqlConnection())
                {
                    cn.ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; " +
                        "AttachDbFilename = C:\\Users\\hotin\\Desktop\\C-Sharp-Project\\Project\\Project\\App_Data\\Ranking.mdf;" +
                        "Integrated Security=True;";
                    cn.Open();
                    string mycmd = "INSERT INTO Level1(Name, Score) VALUES(@Name, @Score)";
                    SqlCommand cmd = new SqlCommand(mycmd, cn);
                    cmd.Parameters.AddWithValue("@Name", nickname);
                    cmd.Parameters.AddWithValue("@Score", playerScore);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Debug.Write(ex.ToString());
                    }
                }
            }
            nickname = "";
            playerScore = "";
            dropWitoutNickName = false;
        }
    }
}