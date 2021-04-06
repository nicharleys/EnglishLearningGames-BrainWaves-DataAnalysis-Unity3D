<?php

     $dbhost = 'localhost';
     $dbname = 'u1025104';
     $dbtablename = 'save3';
     $dbuser = 'u1025104';
     $dbpass = 'zxc5679120';
     mysql_connect("$dbhost","$dbuser","$dbpass") or die(mysql_error() );
     mysql_query("set names utf8");
     mysql_select_db($dbname) or die( mysql_error() );
     
     
     if( $_POST['att'] != NULL && $_POST['med'] != NULL && $_POST['del'] != NULL && $_POST['the'] != NULL
      && $_POST['la'] != NULL && $_POST['ha'] != NULL && $_POST['lb'] != NULL && $_POST['hb'] != NULL
      && $_POST['lg'] != NULL && $_POST['hg'] != NULL)
         {
         
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

         
        if($nums > 0)//有查詢到該筆資料
           {
             
           }
         else if ($nums <= 0) 
         {
         $query ="insert into $dbtablename(m_att,m_med,m_D,m_T,m_LA,m_HA,m_LB,m_HB,m_LG,m_HG) "
                 . "values ('$att','$med','$del','$the','$la','$ha','$lb','$hb','$lg','$hg')";        
         $result = mysql_query($query) or die( mysql_error() );
       
  
         }
       }
     
?>
