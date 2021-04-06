<?php

     $dbhost = 'localhost';
     $dbname = 'u1025104';
     $dbtablename = 'cmind';
     $dbuser = 'u1025104';
     $dbpass = 'zxc5679120';
     mysql_connect("$dbhost","$dbuser","$dbpass") or die(mysql_error() );
     mysql_query("set names utf8");
     mysql_select_db($dbname) or die( mysql_error() ); 
        
     if( $_POST['user'] != NULL && $_POST['att'] != NULL && $_POST['med'] != NULL && $_POST['del'] != NULL && $_POST['the'] != NULL
      && $_POST['la'] != NULL && $_POST['ha'] != NULL && $_POST['lb'] != NULL && $_POST['hb'] != NULL
      && $_POST['lg'] != NULL && $_POST['hg'] != NULL)
         {
        $user = $_POST['user'];
        $att = $_POST['att'];     
        $med = $_POST['med'];
        $del = $_POST['del'];
        $the = $_POST['the'];
        $la = $_POST['la'];
        $ha = $_POST['ha'];
        $lb = $_POST['lb'];
        $hb = $_POST['hb'];
        $lg = $_POST['lg'];
        $hg = $_POST['hg'];

        $query2 = "select * from $dbtablename where m_att = '$att' and m_med = '$med' and m_acnum = '$user'";
        $result2 = mysql_query($query2) or die( mysql_error() );           
        $nums=mysql_num_rows($result2);//計算查詢筆數，是否有查到資料
        if($nums > 0)//有查詢到該筆資料
           {
             
           }
         else if ($nums <= 0) 
         {
         $query ="insert into $dbtablename(m_acnum,m_att,m_med,m_D,m_T,m_LA,m_HA,m_LB,m_HB,m_LG,m_HG) "
                 . "values ('$user','$att','$med','$del','$the','$la','$ha','$lb','$hb','$lg','$hg')";        
         $result = mysql_query($query) or die( mysql_error() );
       
  
         }
       }
     
?>
