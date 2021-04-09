<?php
     
     $dbhost = 'localhost';
     $dbname = 'u1025104';
     $dbtablename = 'acnumber';
     $dbuser = 'u1025104';
     $dbpass = 'zxc5679120';
     mysql_connect("$dbhost","$dbuser","$dbpass") or die(mysql_error() );
     mysql_query("set names utf8");
     mysql_select_db($dbname) or die( mysql_error() );
     

     if( $_POST['getuser'] != NULL && $_POST['savelevel'] != NULL && $_POST['savelevel2'] != NULL)
         {        
         $giveuser = $_POST['getuser'];
         $givelevel = $_POST['savelevel'];
         $givelevel2 = $_POST['savelevel2'];
         
         $query ="update $dbtablename set a_attent = '$givelevel',a_medita = '$givelevel2' where a_acnum = '$giveuser'"; 
         $result = mysql_query($query) or die( mysql_error() );           
 
       }
     
?>
