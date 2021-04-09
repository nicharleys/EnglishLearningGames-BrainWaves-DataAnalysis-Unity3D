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
     
     if( $_POST['changeac'] != NULL && $_POST['changepw'] != NULL && $_POST['download'] != NULL && $_POST['csuserac'] != NULL)
         {        
         $changeac = $_POST['changeac'];
         $changepw = $_POST['changepw'];
         
         $csuserac = $_POST['csuserac'];

         $query2 = "select a_acnum,a_psword from $dbtablename2 where a_acnum = '$changeac'";
         $result2 = mysql_query($query2) or die( mysql_error() );           
         $nums=mysql_num_rows($result2);//計算查詢筆數，是否有查到資料
         $row2 = mysql_fetch_array($result2);
         
        
       if($changeac != NULL && $changepw != NULL)
       {
         if($nums > 0)//有查詢到該筆資料
           {
             if($row2[0] == $csuserac)
             {
              $query ="update $dbtablename2 set a_acnum = '$changeac',a_psword = '$changepw' where a_acnum = '$csuserac'"; 
              $result = mysql_query($query) or die( mysql_error() );
                 
              echo "0";//有查詢到，但帳號為原本的
             }
             else
             {
              echo "1";    
             }
           }
         else if($nums <= 0) //沒查詢到該筆資料
           {
             $query ="update $dbtablename2 set a_acnum = '$changeac',a_psword = '$changepw' where a_acnum = '$csuserac'"; 
             $result = mysql_query($query) or die( mysql_error() );
       
              echo "0";//沒查詢到
           }
        }  
       }   
?>
