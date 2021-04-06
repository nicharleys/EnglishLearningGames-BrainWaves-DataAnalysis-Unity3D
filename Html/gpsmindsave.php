<?php
     
     $dbhost = 'localhost';
     $dbname = 'u1025104';
     $dbtablename = 'ctimea';
     $dbuser = 'u1025104';
     $dbpass = 'zxc5679120';
     mysql_connect("$dbhost","$dbuser","$dbpass") or die(mysql_error() );
     mysql_query("set names utf8");
     mysql_select_db($dbname) or die( mysql_error() ); 
     
   
     if( $_POST['user'] != NULL && $_POST['ta'] != NULL)
         {
        $user = $_POST['user'];
        $ta = $_POST['ta'];     

         
         $query ="insert into $dbtablename(ta_acnum,ta_att) "
                 . "values ('$user','$ta')";        
         $result = mysql_query($query) or die( mysql_error() );

         }

     
?>
