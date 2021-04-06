<?php
     
     $dbhost = 'localhost';
     $dbname = 'u1025104';
     $dbtablename = 'checkplayer';
     $dbtablename2 = 'acnumber';
     $dbuser = 'u1025104';
     $dbpass = 'zxc5679120';
     mysql_connect("$dbhost","$dbuser","$dbpass") or die(mysql_error() );
     mysql_query("set names utf8");
     mysql_select_db($dbname) or die( mysql_error() ); 
     
    
     if( $_POST['oldac'] != NULL && $_POST['oldpw'] != NULL && $_POST['download'] != NULL)
         {        
         $oldac = $_POST['oldac'];
         $oldpw = $_POST['oldpw'];
         
         $query2 = "select a_acnum,a_psword from $dbtablename2 where a_acnum = '$oldac'";
         $result2 = mysql_query($query2) or die( mysql_error() );           
         $nums=mysql_num_rows($result2);//計算查詢筆數，是否有查到資料
         $row2 = mysql_fetch_array($result2);
         
         $now = date("Y-m-d H:i:s");
       if($oldac != NULL && $oldpw != NULL)
       {
         if($nums > 0)//有查詢到該筆資料
           {
          
          if($oldpw == $row2[1])//密碼比對成功
              {
              $query3 ="update $dbtablename2 set a_date = '$now' where a_acnum = '$oldac'"; 
              $result3 = mysql_query($query3) or die( mysql_error() );
               echo "1";
              }
          else if ($oldpw != $row2[1])//密碼比對錯誤
              {
               echo "2";
              }
           }
         else if($nums <= 0) //沒查詢到該筆資料
           {
            echo "0";//沒查詢到
           }
        }
       }
?>
