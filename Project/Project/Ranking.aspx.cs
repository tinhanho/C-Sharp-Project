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

namespace Project
{
    public partial class Contact : Page
    {
        SqlConnection cn = new SqlConnection();
        protected void Page_Load(object sender, EventArgs e)
        {

            cn.ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; " +
                "AttachDbFilename = C:\\Users\\hotin\\Desktop\\C-Sharp-Project\\Project\\Project\\App_Data\\Ranking.mdf;" +
                "Integrated Security=True;";
            cn.Open();
            string mycmd = "SELECT * FROM Game1 ORDER BY CAST(Score AS FLOAT) DESC";
            string mycmd2 = "SELECT * FROM Game2 ORDER BY CAST(Score AS FLOAT) ASC";
            SqlCommand cmd = new SqlCommand(mycmd, cn);
            SqlDataReader dr= cmd.ExecuteReader();

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
            Response.Redirect("Default");
        }

        [WebMethod(EnableSession = true)]
        public static string SendLabelDataToBackend(string password, string GameValue)
        {
            Debug.Write(password);
            Debug.Write(GameValue);
            if (password == "70317031" && GameValue == "1")
            {
                using (SqlConnection cn = new SqlConnection())
                {
                    cn.ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; " +
                        "AttachDbFilename = C:\\Users\\hotin\\Desktop\\C-Sharp-Project\\Project\\Project\\App_Data\\Ranking.mdf;" +
                        "Integrated Security=True;";
                    cn.Open();
                    string mycmd = "DELETE FROM Game1";
                    SqlCommand cmd = new SqlCommand(mycmd, cn);
                    cmd.ExecuteNonQuery();
                }
                return "response succesful";
            }
            else if (password == "70317031" && GameValue == "2")
            {
                using (SqlConnection cn = new SqlConnection())
                {
                    cn.ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; " +
                        "AttachDbFilename = C:\\Users\\hotin\\Desktop\\C-Sharp-Project\\Project\\Project\\App_Data\\Ranking.mdf;" +
                        "Integrated Security=True;";
                    cn.Open();
                    string mycmd = "DELETE FROM Game2";
                    SqlCommand cmd = new SqlCommand(mycmd, cn);
                    cmd.ExecuteNonQuery();
                }
                return "response succesful";
            }
            else return "response wrong password";
        }
    }
}