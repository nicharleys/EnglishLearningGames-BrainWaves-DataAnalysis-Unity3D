<?php

     $dbhost = 'localhost';
     $dbname = 'u1025104';
     $dbtablename2 = 'random';
     $dbuser = 'u1025104';
     $dbpass = 'zxc5679120';
     mysql_connect("$dbhost","$dbuser","$dbpass") or die(mysql_error() );
     mysql_query("set names utf8");
     mysql_select_db($dbname) or die( mysql_error() ); 
     if( $_POST['download'] != NULL && $_POST['getLS'] != NULL)
         {        
         $getLS = $_POST['getLS'];
         //$query = "select * from $dbtablename order by q_num desc";
         $query = "Select q_num From word0 where q_num != 0 $getLS order by rand() limit 1";
        //$rows = mysql_num_rows($result);
         //$rand=rand($result2,$result3);
         $result = mysql_query($query) or die( mysql_error() );
         $number = mysql_fetch_row($result);
         //$query2 = "UPDATE $dbtablename2 SET which = $rand WHERE w_num = 1";
         $query2 = "UPDATE $dbtablename2 SET which = $number[0] WHERE w_num = 1";
         $result2 = mysql_query($query2) or die( mysql_error() );
         echo "$number[0]";
        }

       
?>