<?php

     $dbhost = 'localhost';
     $dbname = 'u1025104';
     $dbtablename2 = 'random';
     $dbuser = 'u1025104';
     $dbpass = 'zxc5679120';
     mysql_connect("$dbhost","$dbuser","$dbpass") or die(mysql_error() );
     mysql_query("set names utf8");
     mysql_select_db($dbname) or die( mysql_error() ); 
 
     if( $_POST['NowQ'] != NULL)
         {          
     //    $query = "select which from $dbtablename2 where w_num = 1";
       //  $result = mysql_query($query) or die( mysql_error() ); 
      //   $row = mysql_fetch_array($result);   
         $NowQ = $_POST['NowQ'];
         
         //$query2 = "select q_ch from $dbtablename where q_num = $row[0]";
         //$query2 = "select q_ch from $FinalLS where q_num = $row[0]";
         $query2 = "select q_ch from word0 where q_num = $NowQ Union Select q_ch From word1 where q_num = $NowQ Union Select q_ch From word2 where q_num = $NowQ Union Select q_ch From word3 where q_num = $NowQ Union Select q_ch From word4 where q_num = $NowQ Union Select q_ch From word5 where q_num = $NowQ Union Select q_ch From word6 where q_num = $NowQ Union Select q_ch From word7 where q_num = $NowQ Union Select q_ch From word8 where q_num = $NowQ Union Select q_ch From word9 where q_num = $NowQ Union Select q_ch From word10 where q_num = $NowQ Union Select q_ch From word11 where q_num = $NowQ Union Select q_ch From word12 where q_num = $NowQ Union Select q_ch From word13 where q_num = $NowQ Union Select q_ch From word14 where q_num = $NowQ Union Select q_ch From word15 where q_num = $NowQ";
         $result2 = mysql_query($query2) or die( mysql_error() );
         
         
         
      
             $row2 = mysql_fetch_array($result2);
             echo $row2[0];

          
         }

?>
