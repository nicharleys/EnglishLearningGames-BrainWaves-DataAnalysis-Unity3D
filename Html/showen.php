<?php

     $dbhost = 'localhost';
     $dbname = 'u1025104'; 
     $dbuser = 'u1025104';
     $dbpass = 'zxc5679120';
     mysql_connect("$dbhost","$dbuser","$dbpass") or die(mysql_error() );
     mysql_query("set names utf8");
     mysql_select_db($dbname) or die( mysql_error() );
     

     if($_POST['SgiveLSe'] != NULL)
         {        
         $SgiveLSe = $_POST['SgiveLSe'];
         $query = "Select q_en From word0 where q_num != 0 $SgiveLSe";      
         $result = mysql_query($query) or die( mysql_error() );
        while($row = mysql_fetch_array($result)){//印出資料
         echo $row['q_en'];
         echo "\n";
         echo "\n";
         }
        }

       
?>