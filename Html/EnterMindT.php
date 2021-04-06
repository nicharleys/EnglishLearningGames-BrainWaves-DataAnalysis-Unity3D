<p>
<style type="text/css">
div 
{
  width: 600px;
  height: 300px;
  position: absolute;
  top: 65%;
  left: 50%;
  margin: -150px 0 0 -300px;
}

html 
{
height: 100%;
}

body
{
background-image: url(minds2.jpg);
background-repeat: no-repeat;
background-attachment: fixed;
background-position: center;
background-size: cover;

}
.mysize2 {font-size:150%;}
</style>
</p>
<html>
<head>
<title>Daily Mindset</title>
<?php
     $dbhost = 'localhost';
     $dbname = 'u1025104';
     $dbtablename2 = 'acnumber';
     $dbuser = 'u1025104';
     $dbpass = 'zxc5679120';
     mysql_connect("$dbhost","$dbuser","$dbpass") or die(mysql_error() );
     mysql_query("set names utf8");
     mysql_select_db($dbname) or die( mysql_error() ); 
     if( $_POST['acnum'] != NULL && $_POST['psword'] != NULL)
         {        
         $oldac = $_POST['acnum'];
         $oldpw = $_POST['psword'];
         $check = "";
         
         $query2 = "select a_acnum,a_psword from $dbtablename2 where a_acnum = '$oldac'";
         $result2 = mysql_query($query2) or die( mysql_error() );           
         $nums=mysql_num_rows($result2);//計算查詢筆數，是否有查到資料
         $row2 = mysql_fetch_array($result2);

       if($oldac != NULL && $oldpw != NULL)
       {
         if($nums > 0)//有查詢到該筆資料
           {
          
          if($oldpw == $row2[1])//密碼比對成功
              {
             
               $check = "1";
              }
          else if ($oldpw != $row2[1])//密碼比對錯誤
              {
               $check = "2";
              }
           }
         else if($nums <= 0) //沒查詢到該筆資料
           {
            $check = "0";//沒查詢到
           }
         }
         
        }
?>
</head>
<body>

<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<div>

<table width="600" height="80" align="center">
<tr><td align="center">
 
<?php        
if($check == 1)
{
 ?>
 <b><p class='mysize2'>帳號：<?php echo "$oldac"; ?></p></b><br>
 <b><p class='mysize2'>登入成功</p></b><br>
<form action="PicChoseMenu.php" method="post">
<input type="hidden" name="acnumT" value="<?php echo "$oldac"; ?>">
<input type="submit" name="send" value="確定">
</form>
<?php } 
 else if($check == 2)
 {
 ?>
 <b><p class='mysize2'>帳號：<?php echo "$oldac"; ?></p></b><br>
 <b><p class='mysize2'>登入失敗</p></b><br>
 <input type = "button" value="確定" onclick="location.href='EnterMind.php'">
<?php } 
else if($check == 0)
 {
 ?>
 <b><p class='mysize2'>帳號：<?php echo "$oldac"; ?></p></b><br>
 <b><p class='mysize2'>無此帳號</p></b><br>
 <input type = "button" value="確定" onclick="location.href='EnterMind.php'">
<?php } ?>



</td></tr>
</table>

</div>

</body>
</html>
