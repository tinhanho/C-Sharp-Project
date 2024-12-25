<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Project._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .red-box {
            width: 20px;
            height: 20px;
            background-color: red;
            position: absolute;
        }
        .auto-style4 {
            position: absolute;
            left: 143px;
            top: 84px;
        }
        .auto-style5 {
            position: absolute;
            left: 98px;
            top: 87px;
            width: 40px;
            right: 262px;
        }
        </style>

    <main>
        <section class="row" aria-labelledby="aspnetTitle">
            <h1 id="aspnetTitle">Game
            </h1>
        </section>
            <div style="display: flex;">
                <div id="moveArea" class="outer" style= "margin-top:10px; width: 500px; height: 300px; background-color: lightgray; position: relative; border: 2px solid #000;">
                    <!-- 可移動的方塊 -->
                    <div id="moveBox" style="width: 10px; height: 10px; background-color: black; position: absolute; display: none;">
                    </div>
                    <!--<div id = "GG" class="auto-style6" style="color: white; background-color: black; display:none">
                        Game Over
                    </div>-->
                    <asp:Button ID="Button6" runat="server" OnClick="Button6_Click" Text="遊戲2" style="position:absolute; top: 130px; left: 300px; width: 99px; height: 47px;" />
                    <asp:Button ID="Button1" runat="server" Height="48px" OnClick="Button1_Click" Text="開始" style="margin-left: 190px; margin-top: 70px;" Width="92px"/>
                    <asp:Label ID="Label1" runat="server" Text="Label" style="position: absolute; top: -1px; right: 0; width: 48px; height: 29px;"></asp:Label>
                    <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="遊戲1" style="position:absolute; top: 130px; left: 80px; width: 99px; height: 47px;" />
                    <asp:Button ID="Button2" runat="server" Height="48px" OnClick="Button2_Click" Text="排行榜" style="margin-left: 190px; margin-top: 10px;" Width="92px"/>
                    <div id="RecordArea" class="inner" style="width: 400px; height: 200px; background-color: lightgray; position: absolute;top: 50%; left: 50%; transform: translate(-50%, -50%); border: 2px solid #000; display:none; z-index:1000">
                        <label id="nameLabel" type="text" style="display: block; " class="auto-style5">暱稱</label>
                        <asp:TextBox runat="server" id="TextBox1" type="text" MaxLength="10" style="display: block; " class="auto-style4" />
                        <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="送出" IsDefault="True" style="margin-left: 320px; margin-top: 140px;"/>
                    </div>
                    <asp:Button ID="Button7" runat="server" Height="48px" OnClick="Button7_Click" Text="說明" style="margin-left: 190px; margin-top: 10px;" Width="92px"/>
                </div>
                <div style="width: 100px; height: 300px; margin-left: 10px;">
                           <asp:Button ID="Button3" runat="server" Height="48px" OnClick="Button3_Click" Text="離開" style="margin-left: 0px; margin-top: 260px;" Width="92px"/>
                </div>
            </div>
    
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
                                    document.getElementById('RecordArea').focus();
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
    </main>
</asp:Content>

