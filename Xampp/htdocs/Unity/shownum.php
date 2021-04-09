<?php

     $dbhost = 'localhost';
     $dbname = 'u1025104'; 
     $dbuser = 'u1025104';
     $dbpass = 'zxc5679120';
     mysql_connect("$dbhost","$dbuser","$dbpass") or die(mysql_error() );
     mysql_query("set names utf8");
     mysql_select_db($dbname) or die( mysql_error() );
     
     if($_POST['SgiveLS'] != NULL)
         {        
         $SgiveLS = $_POST['SgiveLS'];
         //$query = "Select q_num From word0 where q_num != 0 $SgiveLS";      
         $query = "Select * From word0 where q_num != 0 $SgiveLS";  
         $result = mysql_query($query) or die( mysql_error() );
         
         $total_records=mysql_num_rows($result);  // 取得記錄數
         
       /*  while($row = mysql_fetch_array($result)){//印出資料
         echo $row['q_num'];
         echo "\n";
         echo "\n";    
         }*/
         for ($i=0;$i<$total_records;$i++) 
         {$row = mysql_fetch_assoc($result); 
          echo $row['q_num'];  
          echo "   ";  
          echo "|";  
          echo "   ";  
          echo $row['q_en'];  
           echo "   ";  
          echo "|";  
          echo "   ";  
          echo $row['q_ch']; 
          echo "\n";
         }
        }

       
?>



