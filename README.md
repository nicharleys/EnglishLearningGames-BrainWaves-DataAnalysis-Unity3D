# EnglishLearningGames-BrainWaves-DataAnalysis-Unity3D
 
### 作者:[nicharleys](https://github.com/nicharleys) 建置

<img src="https://hits.seeyoufarm.com/api/count/incr/badge.svg?url=https://github.com/nicharleys/EnglishLearningGames-BrainWaves-DataAnalysis-Unity3D" alt="Hits" data-canonical-src="https://hits.seeyoufarm.com/api/count/incr/badge.svg?url=https://github.com/nicharleys/EnglishLearningGames-BrainWaves-DataAnalysis-Unity3D" style="max-width:100%;"></a> 


<br><br><br>

<div align="center">
   <img src="https://github.com/nicharleys/EnglishLearningGames-BrainWaves-DataAnalysis-Unity3D/blob/main/Gif/1.gif"  width="400" height="300" "  />
   <img src="https://github.com/nicharleys/EnglishLearningGames-BrainWaves-DataAnalysis-Unity3D/blob/main/Gif/2.gif"  width="400" height="300" " /> <br>    
   <img src="https://github.com/nicharleys/EnglishLearningGames-BrainWaves-DataAnalysis-Unity3D/blob/main/Gif/3.gif"  width="400" height="300" "  />
   <img src="https://github.com/nicharleys/EnglishLearningGames-BrainWaves-DataAnalysis-Unity3D/blob/main/Gif/4.gif"  width="400" height="300" " /> 
</div>

<br><br><br>
# I、介紹

<div>
   <h3 styles={font-weight:bold;}>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp        這是一個使用腦波儀並且結合Unity3D的英語學習系統，串接方式是由PHP所寫成的網頁來做為資料的傳接過程，並藉由會員系統來記錄使用者的資料，遊戲前會進行測試，藉此將使用者的腦波分級，而遊戲內容是由使用者透過腦波數值來操控人物，遊戲的過程則會紀錄腦波，使用者可以在遊玩後查看腦波的時間段，並確認學習時是否處於專注且放鬆的狀態，以此增進學習效果。
</<h3> 
</div> 

<br>
<div align="center">
<table border="1">
    <tr>
        <td>
            <div>
            <h3><b>優點:</b><br></h3>
            <b>&nbsp; 1.會員系統提供查看資料。 </b><br>
            <b>&nbsp; 2.腦波分級降低使用難度。 </b><br>
            <b>&nbsp; 3.提供腦波時間段查看。 </b><br>
            <b>&nbsp; 4.分析腦波並提供學習評分。 </b><br>
            <b>&nbsp; 5.學習的同時增進學習效果。 </b>
           </div>
        </td>
        <td>
            <div>
            <h3><b>缺點:</b><br></h3><br>
            <b>&nbsp; 1.必須要有網路。 </b><br>
            <b>&nbsp; 2.必須要有腦波耳機。 </b><br>
            <b>&nbsp; 3.必須要有主機作為伺服器。 </b>
            <br><br><br>
           </div>
        </td>
    </tr>
</table>
<br>
<br>
 </div>
 
 
# II、資料庫設置
<div> 
<strong font-size:13px;>
&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 
本系統使用XAMPP作為Apache HTTP伺服器，並以MySQL功能作為資料庫，以此處理會員系統的各種資料儲存，因此在執行系統前需要建置資料庫環境，過程需要使用PHP來建立頁面，藉此才能夠以Post、Get的方式傳輸遊戲的資料內容，若有需要自行建置資料庫，在設定步驟結束後須自行修改XAMPP檔案內的PHP程式，以此設置成自訂的資料庫內容，並將Unity3D內的程式所設置的網址也一同修改成自訂網址。
</strong>
</div> 
<br/>
<div> 
<strong font-size:13px;>
&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 
XAMPP安裝版本為7.4.9，安裝作業系統為Windows，若建置過程失敗或介面問題可以參考此版本，以下是XAMPP的下載網址：
https://www.apachefriends.org/zh_tw/index.html
</strong>
</div>
<div>
   <h3 styles={font-weight:bold;}>(1)XAMPP設定</<h3> 
</div>
<table border="1">
    <tr>
        <td>
            <div>
            <b>1.</b><br>
            <b>安裝後開啟XAMPP Controll Panal介面，點選Apache的Config選項，並選擇phpMyAdmin(config.ini.php)，在設定中找到以下三行:</b><br>
            <b>$cfg['Servers'][$i]['auth_type'] = 'config';</b><br> 
            <b>$cfg['Servers'][$i]['user'] = 'root';</b><br> 
            <b>$cfg['Servers'][$i]['password'] = '';</b>
           </div>
        </td>
    </tr>
</table>   
<table border="1">
    <tr>
        <td>
            <div>
            <b>2.</b><br>
            <b>將auth_type項目中的config改為cookie，接著將密碼輸入至password的右方單引號後儲存。</b>
           </div>
        </td>
    </tr>
</table>
<table border="1">
    <tr>
        <td>
            <div>
            <b>3.</b><br>
            <b>開啟XAMPP Controll Panal介面，點選MySQL的Config選項，並選擇my.ini，在設定中找到以下一行:</b><br/>
            <b>max_allowed_packet = 16M</b>
           </div>
        </td>
    </tr>
</table>
<table border="1">
    <tr>
        <td>
            <div>
            <b>4.</b><br>
            <b>將數值改為1024M。</b>
           </div>
        </td>
    </tr>
</table>
<table border="1">
    <tr>
        <td>
            <div>
            <b>5.</b><br>
            <b>設定完成後，啟動XAMPP，並按下Apache與MySQL的Start選項。</b>
           </div>
        </td>
    </tr>
</table>
<table border="1">
    <tr>
        <td>
            <div>
            <b>6.</b><br>
            <b>打開網頁輸入網址：127.0.0.1 或 localhost，並點選phpMyAdmin便可進入操作頁面。</b>
           </div>
        </td>
    </tr>
</table>
<br>

</div>
<div>
   <h3 styles={font-weight:bold;}>(2)資料庫導入</<h3> 
</div>
<table border="1">
    <tr>
        <td>
            <div>
            <b>1.</b><br>
            <b>在資料庫頁面找到導入的選項</b>
           </div>
        </td>
    </tr>
</table>   
<table border="1">
    <tr>
        <td>
            <div>
            <b>2.</b><br>
            <b>將專案內的XAMPP資料夾取代原有資料夾</b>
           </div>
        </td>
    </tr>
</table>   

