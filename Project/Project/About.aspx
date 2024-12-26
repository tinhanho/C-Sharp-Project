<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="Project.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     <style>
         .outer-style {
            display: flex;       /* 設置父元素為 flex 容器，讓子元素並排 */
            gap: 10px;           /* 子元素間的間距（可選） */
         }

        .rounded-box {
            margin-top: 10px;
            border: 2px solid #000;  /* 邊框顏色 */
            border-radius: 15px;      /* 圓角半徑，調整數值來改變圓角大小 */
            padding: 10px;            /* 內邊距 */
            width: 400px;             /* 寬度 */
            height: 150px;            /* 高度 */
        }
        .rounded-box2 {
            margin-top: 10px;
            border: 2px solid #000;  /* 邊框顏色 */
            border-radius: 15px;      /* 圓角半徑，調整數值來改變圓角大小 */
            padding: 10px;            /* 內邊距 */
            width: 400px;             /* 寬度 */
            height: 150px;            /* 高度 */
        }
    </style>
    <main aria-labelledby="title">
        <h2 id="title"><%: Title %></h2>
        <asp:Button ID="Button1" runat="server" Height="34px" Text="回到首頁" Width="129px" OnClick="Button1_Click" />

        <div class="outer-style">
            <div id="rb1" class="rounded-box">
            <h5> 遊戲一：<br>
                鬼抓人遊戲</h5>
                <br>用鍵盤上下左右控制黑色方塊躲避出生的紅色方塊，<br>
                碰到紅色方塊遊戲結束。

            </div>
            <div id="rb2" class="rounded-box2">
            <h5> 遊戲二：<br>
                打地鼠遊戲</h5>
                <br>用鼠標點擊10秒內出生的所有紅色方塊，<br>
                當點擊了所有紅色方塊遊戲結束。
            </div>
        </div>
    </main>
</asp:Content>
