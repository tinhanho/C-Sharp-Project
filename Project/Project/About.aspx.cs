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

        }

        protected void Button1_Click(object sender,EventArgs e) {
            Response.Redirect("Default");
        }
    }
}

/*
        static string playerScore = "";
        static string nickname = "";
        static bool dropWitoutNickName = false;
        static string GameValue = "";
        static SqlConnection cn = new SqlConnection();
        static bool mylock = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            try{
                cn.ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; " +
                "AttachDbFilename = C:\\Users\\hotin\\Desktop\\C-Sharp-Project\\Project\\Project\\App_Data\\Ranking.mdf;" +
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
            Button5.Visible = true;
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
            playerScore = labelValue;
            GameValue = Game;
            mylock = false;

            return "收到的數據是: " + labelValue;
        }

        //Nickname sending button
        protected void Button4_Click(object sender, EventArgs e)
        {
            nickname = TextBox1.Text;

            while(mylock){ };

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


    <style>
        .red-box {
            width: 20px;
            height: 20px;
            background-color: red;
            position: absolute;
        }
        </style>
            <!-- 加載 JavaScript -->
            <script type="text/javascript">

                var currentTime = 0;
                function updateTime() {
                    var label = document.getElementById('<%= Label1.ClientID %>'); // 獲取 Label 控制項的 ID
                    currentTime += 0.1;
                    label.innerText = currentTime.toFixed(1).toString();
                }


                var moveBox = document.getElementById("moveBox");
                var moveArea = document.getElementById("moveArea");

                var moveSpeed = 5;  // 移動速度
                var boxX = 250;
                var boxY = 150;

                moveBox.style.left = boxX + "px";
                moveBox.style.top = boxY + "px";

                let keysPressed = {}; // 存儲當前按下的按鍵

                // 設置鍵盤事件來控制方塊移動

                function move() {

                    if (keysPressed["ArrowUp"]) {
                        boxY -= moveSpeed; // 上移
                    } else if (keysPressed["ArrowDown"]) {
                        boxY += moveSpeed; // 下移
                    } else if (keysPressed["ArrowLeft"]) {
                        boxX -= moveSpeed; // 左移
                    } else if (keysPressed["ArrowRight"]) {
                        boxX += moveSpeed; // 右移
                    }

                    // 限制方塊在 moveArea 內移動
                    boxX = Math.max(0, Math.min(moveArea.clientWidth - moveBox.offsetWidth, boxX));
                    boxY = Math.max(0, Math.min(moveArea.clientHeight - moveBox.offsetHeight, boxY));

                    moveBox.style.left = boxX + "px";
                    moveBox.style.top = boxY + "px";
                    requestAnimationFrame(move);
                };
                requestAnimationFrame(move);


                function hideBox() {
                    document.getElementById("moveBox").style.display = 'none';
                }

                function showBox() {
                    document.getElementById("moveBox").style.display = 'block';
                }
                function getRandomPosition(container) {
                    const containerWidth = container.offsetWidth;
                    const containerHeight = container.offsetHeight;
                    const x = Math.random() * (containerWidth - 20); // 避免超出邊界
                    const y = Math.random() * (containerHeight - 20);
                    return { x, y };
                }

                const redBoxes = [];
                var s1, s2, s3, s4;
                let alive = 1;
                var GameValue;

                function generateRedBox() {
                    gRB = 1;
                    const moveArea = document.getElementById('moveArea');
                    const { x, y } = getRandomPosition(moveArea);

                    // 創建新的方塊元素
                    const redBox = document.createElement('div');
                    redBox.classList.add('red-box');
                    redBox.style.left = `${x}px`;
                    redBox.style.top = `${y}px`;

                    const speedX = 1;
                    const speedY = 1;

                    // 儲存方塊資訊
                    redBoxes.push({ element: redBox, x, y, speedX, speedY });

                    // 添加到容器中
                    moveArea.appendChild(redBox);
                }
                function moveRedBoxes() {
                    var moveBox = document.getElementById("moveBox");
                    redBoxes.forEach(box => {
                        // 更新位置
                        if (box.x < parseInt(moveBox.style.left)) box.x += box.speedX;
                        else box.x -= box.speedX;

                        if (box.y < parseInt(moveBox.style.top)) box.y += box.speedY;
                        else box.y -= box.speedY;

                        // 檢查邊界碰撞
                        if (box.x <= 0 || box.x >= moveArea.offsetWidth - 20) {
                            box.speedX *= -1; // 水平方向反轉
                        }
                        if (box.y <= 0 || box.y >= moveArea.offsetHeight - 20) {
                            box.speedY *= -1; // 垂直方向反轉
                        }

                        // 更新元素位置
                        box.element.style.left = `${box.x}px`;
                        box.element.style.top = `${box.y}px`;
                    });
                }
                function sendToBackend(GameValue) {
                    // 獲取Label1的值
                    var labelValue = document.getElementById('<%= Label1.ClientID %>').innerText;

                    // 使用AJAX發送數據到後端
                    var xhr = new XMLHttpRequest();
                    xhr.open("POST", "Default.aspx/SendLabelDataToBackend", true);
                    xhr.withCredentials = true;
                    xhr.setRequestHeader('Content-type', 'application/json');
                    // 傳送JSON格式的數據

                    //xhr.onload = function () {
                    //    if (xhr.status === 200) {
                    //        // 解析後端的 JSON 回應
                    //        var response = JSON.parse(xhr.responseText);
                    //        // 回應中的 d 屬性包含 WebMethod 回傳的資料
                    //        console.log("後端回應:", response.d);
                    //        alert("後端回應: " + response.d);
                    //    } else {
                    //        console.error("錯誤: 無法取得回應，狀態碼:", xhr.status);
                    //    }
                    //};


                    if (GameValue === 1) xhr.send(JSON.stringify({ labelValue: labelValue, Game: 1 }));
                    else if(GameValue === 2) xhr.send(JSON.stringify({ labelValue: labelValue, Game: 2 }));
                }
                function CheckIsAlive() {
                    var moveBox = document.getElementById("moveBox");
                    var mvLeft = parseInt(moveBox.style.left);
                    var mvTop = parseInt(moveBox.style.top);
                    redBoxes.forEach(box => {
                        if ((box.x > mvLeft && (box.x - mvLeft) < 10) || (mvLeft > box.x && (mvLeft - box.x) < 20) ) {
                            if ((box.y > mvTop && (box.y - mvTop) < 10) || (mvTop > box.y && (mvTop - box.y ) < 20) ) {
                                console.log(`box.x: ${box.x}, moveBox.style.left: ${parseInt(moveBox.style.left)}`);
                                console.log(`box.y: ${box.y}, moveBox.style.top: ${parseInt(moveBox.style.top)}`);                               
                                document.removeEventListener("keydown", handleKeydown);
                                document.removeEventListener("keyup", handleKeyUp);
                                clearInterval(s1);
                                clearInterval(s2);
                                clearInterval(s3);
                                clearInterval(s4);
                                keysPressed["ArrowUp"] = false;
                                keysPressed["ArrowDown"] = false;
                                keysPressed["ArrowLeft"] = false;
                                keysPressed["ArrowRight"] = false;
                                //document.getElementById('GG').style.display = 'block';
                                setTimeout(function () {
                                    document.getElementById('RecordArea').style.display = 'block';
                                }, 1000);
                                document.getElementById('<%= Button4.ClientID %>').addEventListener('click', function () {
                                    sendToBackend(1);
                                });
                            }
                        }
                    });
                }
                function CheckAllRedBoxIsClicked() {
                    const remainingBoxes = document.querySelectorAll('.red-box'); // 獲取仍然存在的紅色方塊
                    if (remainingBoxes.length === 0) {
                        clearInterval(s3);
                        clearInterval(s4);
                        setTimeout(function () {
                            document.getElementById('RecordArea').style.display = 'block';
                        }, 1000);
                        document.getElementById('<%= Button4.ClientID %>').addEventListener('click', function () {
                            sendToBackend(2);
                        });
                    }
                }
                function handleKeydown(event) {
                    keysPressed[event.key] = true; // 記錄按下的按鍵
                }
                function handleKeyUp(event) {
                    keysPressed[event.key] = false; // 記錄按下的按鍵
                }
            </script>

*/