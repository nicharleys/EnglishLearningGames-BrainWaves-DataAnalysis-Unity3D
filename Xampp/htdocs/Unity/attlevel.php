<?php
     
     $dbhost = 'localhost';
     $dbname = 'u1025104';
     $dbtablename = 'acnumber';
     $dbuser = 'u1025104';
     $dbpass = 'zxc5679120';
     mysql_connect("$dbhost","$dbuser","$dbpass") or die(mysql_error() );
     mysql_query("set names utf8");
     mysql_select_db($dbname) or die( mysql_error() ); 
     
     
     if( $_POST['user'] != NULL)
         {   
         $user =  $_POST['user'];
         $query = "select a_attent from $dbtablename where a_acnum = '$user'";
         $result = mysql_query($query) or die( mysql_error() ); 
         $row = mysql_fetch_array($result);

         
         if($row[0] == "F")
         {
          echo "-10";
         }  
         else if($row[0] == "E")
         {
          echo "-5";
         } 
         else if ($row[0] == "D")
         {
          echo "0";
         }
         else if($row[0] == "C")
         {
          echo "5";
         }
         else if($row[0] == "B")
         {
          echo "10";
         } 
         else if($row[0] == "A")
         {
          echo "15";
         } 
         else if($row[0] == "S")
         {
          echo "20";
         } 
                    
      }

?>
