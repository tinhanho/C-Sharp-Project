using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Project
{
    public partial class Contact : Page
    {
        static SqlConnection cn;
        protected void Page_Load(object sender, EventArgs e)
        {
            cn = new SqlConnection();
            cn.ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; " +
                "AttachDbFilename = C:\\C-Sharp-Project\\Project\\Project\\App_Data\\Ranking.mdf;" +
                "Integrated Security=True;";
            cn.Open();
            string mycmd = "SELECT * FROM Game1 ORDER BY CAST(Score AS FLOAT) DESC";
            string mycmd2 = "SELECT * FROM Game2 ORDER BY CAST(Score AS FLOAT) ASC";
            SqlCommand cmd = new SqlCommand(mycmd, cn);
            SqlDataReader dr = cmd.ExecuteReader();

            GridView1.DataSource = dr;
            GridView1.DataBind();
            dr.Close();

            SqlCommand cmd2 = new SqlCommand(mycmd2, cn);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            GridView2.DataSource = dr2;
            GridView2.DataBind();
            dr2.Close();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            cn.Close();
            Response.Redirect("Default");
        }

        [WebMethod(EnableSession = true)]
        public static string SendLabelDataToBackend(string password, string GameValue, string PlayerName)
        {
            Debug.Write(password);
            Debug.Write(GameValue);

            if (password == "70317031" && PlayerName != "")
            {

                string mycmd = "DELETE FROM Game1 WHERE Name=@PlayerName";
                SqlCommand cmd = new SqlCommand(mycmd, cn);
                cmd.Parameters.AddWithValue("@PlayerName", PlayerName);
                cmd.ExecuteNonQuery();
                
                return "response succesful";
            }

            else if (password == "70317031" && GameValue == "1")
            {

                string mycmd = "DELETE FROM Game1";
                SqlCommand cmd = new SqlCommand(mycmd, cn);
                cmd.ExecuteNonQuery();
                
                return "response succesful";
            }
            else if (password == "70317031" && GameValue == "2")
            {

                string mycmd = "DELETE FROM Game2";
                SqlCommand cmd = new SqlCommand(mycmd, cn);
                cmd.ExecuteNonQuery();
                
                return "response succesful";
            }
            else return "response wrong password";
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text == "") return;

            string OutputString = "";
            bool Isread = false;            
            

            string mycmd = "SELECT * FROM Game1 WHERE Name=@PlayerName";
            SqlCommand cmd = new SqlCommand(mycmd, cn);
            cmd.Parameters.AddWithValue("@PlayerName", TextBox1.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                OutputString = "遊戲一： " + dr["Score"].ToString() + "<br/>";
                Isread = true;
            }
            dr.Close();
            string mycmd2 = "SELECT * FROM Game2 WHERE Name=@PlayerName2";
            SqlCommand cmd2 = new SqlCommand(mycmd2, cn);
            cmd2.Parameters.AddWithValue("@PlayerName2", TextBox1.Text);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            if (dr2.Read())
            {
                OutputString = OutputString + "遊戲二： " + dr2["Score"].ToString();
                Isread = true;
            }
            dr2.Close();

            

            if (Isread)
            {
                Label1.Text = TextBox1.Text + "<br/>" + OutputString;
            }
            else Label1.Text = TextBox1.Text + "沒有紀錄！";
            string script = @"
                    document.getElementById(""SearchArea"").style.display=""block"";
                ";
            ClientScript.RegisterStartupScript(this.GetType(), "SearchAreaScript", script, true);
        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            string script = @"
                    document.getElementById(""SearchArea"").style.display=""none"";
                ";
            ClientScript.RegisterStartupScript(this.GetType(), "SearchAreaScript2", script, true);

        }
    }
}