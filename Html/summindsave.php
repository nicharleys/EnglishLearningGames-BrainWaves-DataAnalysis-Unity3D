<?php

     $dbhost = 'localhost';
     $dbname = 'u1025104'; 
     $dbtablename = 'ctimem';
     $dbuser = 'u1025104';
     $dbpass = 'zxc5679120';
     mysql_connect("$dbhost","$dbuser","$dbpass") or die(mysql_error() );
     mysql_query("set names utf8");
     mysql_select_db($dbname) or die( mysql_error() );
     
      
     if( $_POST['user'] != NULL && $_POST['tm'] != NULL)
         {
        $user = $_POST['user'];
        $tm = $_POST['tm'];     

         
         $query ="insert into $dbtablename(tm_acnum,tm_med) "
                 . "values ('$user','$tm')";        
         $result = mysql_query($query) or die( mysql_error() );

         }

     
?>
