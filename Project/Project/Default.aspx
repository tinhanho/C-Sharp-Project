<%@ Page Title="我愛天音" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Project._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <section class="row" aria-labelledby="aspnetTitle">
        </section>
            <div style="display: flex; justify-content: center">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/fig/ap1.png" Style="width:30%; height:30%;" onclick="switchImage()" />
                <asp:Label ID="Label1" runat="server" Text="👊👊👊: 0" Font-Size="Large" Style="margin-left: 10px; margin-top: 50px;"></asp:Label>
            </div>
        
        <audio id="clickSound" src="fig/ap_audio.mp3" preload="auto"></audio>

        <script>
            const images = [
                "/fig/ap1.png",
                "/fig/ap2.png",
                "/fig/ap3.png",
            ];
            let current = 0;
            let timer = null;

            function switchImage() {
                if (timer !== null) return;
                var audio = document.getElementById("clickSound");
                audio.play();
                let switchCount = 0;
                timer = setInterval(() => {
                    current++;
                    if (current >= images.length) current = 0;
                    document.getElementById('<%= Image1.ClientID %>').src = images[current];

                switchCount++;
                if (switchCount >= 3) {
                    clearInterval(timer);
                    timer = null;
                }
                }, 300);
                var label = document.getElementById("<%= Label1.ClientID %>");
                const text = label.innerText || label.textContent;
                const parts = text.split(":");
                const prefix = parts[0];
                const number = parseInt(parts[1]);
                const newCount = number + 1;
                label.innerText = `${prefix}: ${newCount}`;
            }
        </script>

    </main>
</asp:Content>

