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
     
     if( $_POST['newac'] != NULL && $_POST['newpw'] != NULL && $_POST['download'] != NULL)
         {        
         $newac = $_POST['newac'];
         $newpw = $_POST['newpw'];
         
         $query2 = "select a_acnum,a_psword from $dbtablename2 where a_acnum = '$newac'";
         $result2 = mysql_query($query2) or die( mysql_error() );           
         $nums=mysql_num_rows($result2);//計算查詢筆數，是否有查到資料
         $row2 = mysql_fetch_array($result2);
         
        
       if($newac != NULL && $newpw != NULL)
       {
         if($nums > 0)//有查詢到該筆資料
           {
              echo "1";          
           }
         else if($nums <= 0) //沒查詢到該筆資料
           {
             $query ="insert into $dbtablename2(a_acnum,a_psword) values ('$newac','$newpw')";        
             $result = mysql_query($query) or die( mysql_error() );
       
              echo "0";//沒查詢到
           }
        }
       
       }
     
?>
