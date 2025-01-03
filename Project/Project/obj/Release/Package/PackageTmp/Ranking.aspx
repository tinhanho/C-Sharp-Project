﻿<%@ Page Title="Ranking" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ranking.aspx.cs" Inherits="Project.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .myGridView td, .myGridView th {
            border: 1px solid black;
        }
        .auto-style1 {
            position: relative;
            left: 8px;
            top: -14px;
            width: 369px;
            height: 453px;
            margin-top: 5px;
            margin-bottom: 0px;
        }
        .auto-style2 {
            position: relative;
            left: 10px;
            top: 5px;
            width: 166px;
            height: 59px;
            margin-top: 5px;
            margin-bottom: 0px;
        }
        .auto-style3 {
            position: relative;
            left: 498px;
            height: 47px;
            margin-top: 5px;
            margin-bottom: 0px;
            top: -455px;
            width: 390px;
        }
    </style>

    <main aria-labelledby="title">
        <h2 id="title"><%: Title %>
        
        </h2>
        <div id="buttonArea" class="auto-style2">
            
            <asp:Button ID="Button1" runat="server" Height="34px" Text="回到首頁" Width="129px" OnClick="Button1_Click" />
            
       </div>
       <div id="rankingArea" class="auto-style1">
            <asp:GridView ID="GridView1" runat="server" Height="16px" style="font-size: medium; margin-bottom: 321px;" Width="200px" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="false" CssClass="myGridView">
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name">
                        <HeaderStyle Width="100px" Height="20px" HorizontalAlign="Left" />
                        <ItemStyle Width="100px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Score" HeaderText="Score" SortExpression="Score">
                        <HeaderStyle Width="100px" HorizontalAlign="Left" />
                        <ItemStyle Width="100px" HorizontalAlign="Left" />
                    </asp:BoundField>
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />
            </asp:GridView>
       </div>

        <div id="passwordInput" style="display:none" class="auto-style3">
            <label for="passwordField">請輸入密碼：</label>
            <input type="password" id="passwordField" style="border-radius: 5px">
            <button onclick="storePassword()" style="border-radius: 5px">確認</button>
        </div>


            <script type="text/javascript">
                window.onload = function () {
                    var grid = document.getElementById('<%= GridView1.ClientID %>');
                    grid.style.height = 'auto';
                    grid.style.maxHeight = '200px';
                    grid.style.overflowY = 'auto';
                };

                var password;
                function login_to_delete() {
                    document.getElementById('passwordInput').style.display = 'block';
                }
                function storePassword() {
                    password = document.getElementById('passwordField').value;
                    sendToBackend();
                }

                function sendToBackend() {
                    // 使用AJAX發送數據到後端
                    var xhr = new XMLHttpRequest();
                    xhr.open("POST", "Ranking.aspx/SendLabelDataToBackend", true);
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
                    xhr.onload = function () {
                        if (xhr.status === 200) {
                            var response = JSON.parse(xhr.responseText);
                            console.log("後端回應:", response.d);
                            if (response.d === "response succesful") {
                                window.location.href = "Ranking";
                            }
                            else if (response.d === "response wrong password") {
                                alert("請輸入正確的密碼");
                            }
                        } else {
                            console.error("錯誤: 無法取得回應，狀態碼:", xhr.status);
                        }
                    };
                    xhr.send(JSON.stringify({ password : password }));
                }
            </script>


    </main>
</asp:Content>
