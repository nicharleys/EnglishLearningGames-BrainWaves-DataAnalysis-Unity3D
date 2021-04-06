<?php

     $dbhost = 'localhost';
     $dbname = 'u1025104';
     $dbtablename = 'cend';
     $dbuser = 'u1025104';
     $dbpass = 'zxc5679120';
     mysql_connect("$dbhost","$dbuser","$dbpass") or die(mysql_error() );
     mysql_query("set names utf8");
     mysql_select_db($dbname) or die( mysql_error() );
     
    
     if( $_POST['user'] != NULL && $_POST['score'] != NULL && $_POST['gametime'] != NULL && $_POST['thingd'] != NULL && $_POST['die'] != NULL)
         {   
         $user = $_POST['user']; 
         $score = $_POST['score'];
         $gametime = $_POST['gametime'];
         $thingd = $_POST['thingd'];
        
         $die = $_POST['die'];
   
         $query = "insert into $dbtablename(e_acnum,e_score,e_gametime,e_thingd,e_die) values ('$user','$score','$gametime','$thingd','$die')";
         $result = mysql_query($query) or die( mysql_error() ); 
     
         

      }

?>
