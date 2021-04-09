<?php
     
     $dbhost = 'localhost';
     $dbname = 'u1025104';
     $dbtablename = 'cend';
     $dbtablename2 = 'ctimea';
     $dbuser = 'u1025104';
     $dbpass = 'zxc5679120';
     mysql_connect("$dbhost","$dbuser","$dbpass") or die(mysql_error() );
     mysql_query("set names utf8");
     mysql_select_db($dbname) or die( mysql_error() ); 
      
     if( $_POST['start'] != NULL && $_POST['end'] != NULL && $_POST['acnum'] != NULL)
         {        
         $start = $_POST['start'];
         $end = $_POST['end'];
         $acnum = $_POST['acnum'];
         
         $query = "select (sum(e_score)/count(*)) from $dbtablename where e_acnum = '$acnum' and e_time between '$start' and '$end'";
         $result = mysql_query($query) or die( mysql_error() ); 
         $score = mysql_fetch_row($result);
         
         $query2 = "select (sum(e_gametime)/count(*)) from $dbtablename where e_acnum = '$acnum' and e_time between '$start' and '$end'";
         $result2 = mysql_query($query2) or die( mysql_error() ); 
         $gametime = mysql_fetch_row($result2);
         
         $query3 = "select (sum(e_thingd)/count(*)) from $dbtablename where e_acnum = '$acnum' and e_time between '$start' and '$end'";
         $result3 = mysql_query($query3) or die( mysql_error() ); 
         $thingd = mysql_fetch_row($result3);
         
        
         
         $query5 = "select (sum(e_die)/count(*)) from $dbtablename where e_acnum = '$acnum' and e_time between '$start' and '$end'";
         $result5 = mysql_query($query5) or die( mysql_error() ); 
         $die = mysql_fetch_row($result5);
         
         $query6 = "select (sum(ta_att)/count(*)) from $dbtablename2 where ta_acnum = '$acnum' and ta_time between '$start' and '$end'";
         $result6 = mysql_query($query6) or die( mysql_error() );
         $atttime = mysql_fetch_row($result6);
                 
         $minute = $gametime[0]/60;
         echo "目前登入帳號 ： $acnum";
         echo "\n";
         echo "平均專注花費時間：$atttime[0] 秒";
         echo "\n";
         echo "平均英文分數 ： $score[0] 分";
         echo "\n";
         echo "平均遊戲時間 ： $minute 分鐘";
         echo "\n";
         echo "平均物品掉落次數 ： $thingd[0] 次";
         echo "\n";
         echo "平均人物死亡次數 ： $die[0] 次";
         }

?>
