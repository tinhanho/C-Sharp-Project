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
        protected void Page_Load(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; " +
                    "AttachDbFilename = C:\\Users\\hotin\\Desktop\\C#期末專題\\Project\\Project\\App_Data\\Ranking.mdf;" +
                    "Integrated Security=True;";
                cn.Open();
                string mycmd = "SELECT * FROM Level1 ORDER BY CAST(Score AS FLOAT) DESC";
                SqlCommand cmd = new SqlCommand(mycmd, cn);
                SqlDataReader dr= cmd.ExecuteReader();

                GridView1.DataSource = dr;
                GridView1.DataBind();
                dr.Close();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            _Default.myctrl = true;
            Response.Redirect("Default");
        }

        [WebMethod(EnableSession = true)]
        public static string SendLabelDataToBackend(string password)
        {
            Debug.Write(password);
            if (password == "70317031")
            {
                using (SqlConnection cn = new SqlConnection())
                {
                    cn.ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; " +
                        "AttachDbFilename = C:\\Users\\hotin\\Desktop\\C#期末專題\\Project\\Project\\App_Data\\Ranking.mdf;" +
                        "Integrated Security=True;";
                    cn.Open();
                    string mycmd = "DELETE FROM Level1";
                    SqlCommand cmd = new SqlCommand(mycmd, cn);
                    cmd.ExecuteNonQuery();
                }
                return "response succesful";
            }
            else return "response wrong password";
        }
    }
}