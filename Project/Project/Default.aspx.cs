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
using System.Xml.Linq;
using System.Security.Cryptography;
using System.Data;
using System.Web.Razor.Parser.SyntaxTree;

namespace Project
{
    public partial class _Default : Page
    {
        static string nickname = "";
        static bool dropWitoutNickName = false;
        static string GameValue = "";
        static SqlConnection cn = new SqlConnection();
        static bool mylock = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Browser.IsMobileDevice)
            {
                Button1.Style["margin-left"] = "97px";
                Button2.Style["margin-left"] = "97px";
                Button7.Style["margin-left"] = "97px";
                Button6.Style["margin-left"] = "100px";
                Button6.Style["left"] = "0px";
            }
            try
            {
                cn.ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; " +
                "AttachDbFilename = C:\\C-Sharp-Project\\Project\\Project\\App_Data\\Ranking.mdf;" +
                "Integrated Security=True;";
                cn.Open();  
            }
            catch{ 
            }
           
            Label1.Visible = false;
            Button1.Visible = true;
            Button2.Visible = true;
            Button3.Visible = false;
            Button5.Visible = false;
            Button6.Visible = false;
        }
        
        //開始
        protected void Button1_Click(object sender, EventArgs e)
        {
            if(!Request.Browser.IsMobileDevice) Button5.Visible = true;
            Button6.Visible = true;
            Button1.Visible = false;
            Button2.Visible = false;
            Button3.Visible = true;
            Button7.Visible = false;
        }
        
        //Ranking
        protected void Button2_Click(object sender, EventArgs e)
        {
            cn.Close();
            Response.Redirect("Ranking");
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            dropWitoutNickName = true;
            Button7.Visible = true;
            Page_Load(sender, e);
        }

        [WebMethod(EnableSession = true)]
        public static string SendLabelDataToBackend(string labelValue, string Game)
        {
            HttpContext.Current.Session["playerScore"] = labelValue;
            GameValue = Game;
            mylock = false;

            return "收到的數據是: " + labelValue;
        }

        //Nickname sending button
        protected void Button4_Click(object sender, EventArgs e)
        {

            while(mylock){ };

            string playerScore = Session["playerScore"].ToString();
            nickname = TextBox1.Text;

            if (!dropWitoutNickName && nickname!="")
            {
                Debug.WriteLine(nickname);
                Debug.WriteLine(playerScore);
                if(GameValue=="1"){
                    string mycmd = @"
                        IF EXISTS (SELECT 1 FROM Game1 WHERE Name = @Name AND @Score > Score)
                        BEGIN
                            UPDATE Game1 SET Score = @Score WHERE Name = @Name;
                        END
                        ELSE
                        BEGIN
                            INSERT INTO Game1 (Name, Score) VALUES (@Name, @Score);
                        END";

                    SqlCommand cmd = new SqlCommand(mycmd, cn);
                    cmd.Parameters.AddWithValue("@Name", nickname);
                    cmd.Parameters.AddWithValue("@Score", playerScore);


                    try {
                        cmd.ExecuteNonQuery();
                    }
                    catch
                    {
                        Debug.Write("EXCEPTION");
                    }
                    
                }
                else if(GameValue=="2"){
                    string mycmd = @"
                        IF EXISTS (SELECT 1 FROM Game2 WHERE Name = @Name AND @Score < Score)
                        BEGIN
                            UPDATE Game2 SET Score = @Score WHERE Name = @Name;
                        END
                        ELSE
                        BEGIN
                            INSERT INTO Game2 (Name, Score) VALUES (@Name, @Score);
                        END";
                    SqlCommand cmd = new SqlCommand(mycmd, cn);
                    cmd.Parameters.AddWithValue("@Name", nickname);
                    cmd.Parameters.AddWithValue("@Score", playerScore);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch
                    {
                        Debug.Write("EXCEPTION");
                    }        
                    
                }
            }
            mylock = true;
            nickname = "";
            playerScore = "";
            dropWitoutNickName = false;
            Button7.Visible = true;

            Page_Load(sender, e);
        }
        
        //遊戲1
        protected void Button5_Click(object sender,EventArgs e) {
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

        protected void Button6_Click(object sender,EventArgs e) {
            Button1.Visible = false;
            Button2.Visible = false;
            Button3.Visible = true;
            Label1.Text = "0";
            Label1.Visible = true;
            string script = @"
                s1 = setInterval(generateRedBox, 500);
                setTimeout(function(){
                    clearInterval(s1);
                }
                , 10000);
                document.addEventListener('click', function(event) {
                    if (event.target && event.target.classList.contains('red-box')) {
                        event.target.remove();
                    }
                });
                s4 = setInterval(updateTime, 100); // 每 1 秒執行一次
                setTimeout(function() {
                    // 10 秒後第一次執行檢查
                    CheckAllRedBoxIsClicked();
                    s3 = setInterval(CheckAllRedBoxIsClicked, 5);
                }, 10000);        
                ";
            ClientScript.RegisterStartupScript(this.GetType(), "GameStartScript", script, true);
        }

        protected void Button7_Click(object sender,EventArgs e) {
            Response.Redirect("About");
        }
        /*
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            // 讀取 Session 中存儲的計數
            int currentValue = (int)(Session["Counter"] ?? 0);
            currentValue++; // 每秒增加
            Session["Counter"] = currentValue;

            // 更新 Literal 控件的文本
            Literal1.Text = "計數: " + currentValue.ToString();
        }
        */
    }
}