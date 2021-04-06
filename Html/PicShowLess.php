<p>
<style type="text/css">
div2 
{
  width: 1200px;
  height: 600px;
  position: absolute;
  top: 50%;
  left: 26%;
  margin: -200px 0 0 -300px;
}

html 
{
height: 100%;
}

body
{
background-image: url(minds2.jpg);
background-repeat: no-repeat;
background-attachment: fixed;
background-position: center;
background-size: cover;

}
.mysize2 {font-size:150%;}
</style>
</p>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>Daily Mindset</title>

<?php

     $dbhost = 'localhost';
     $dbname = 'u1025104';
     $dbuser = 'u1025104';
     $dbpass = 'zxc5679120';
     mysql_connect("$dbhost","$dbuser","$dbpass") or die(mysql_error() );
     mysql_query("set names utf8");
     mysql_select_db($dbname) or die( mysql_error() ); 
     
     
     $start=@$_POST['start'];
     $end=@$_POST['end'];
     $start2=@$_POST['start2'];
     $end2=@$_POST['end2'];
     $acnum=@$_POST['acnumT'];
  
//$res = mysql_query("select * from cmind where m_date = ");
$attLevel = 0;
$res0 = "SELECT `a_attent` FROM `acnumber` WHERE a_acnum = '$acnum'";
$result0 = @mysql_query($res0) or die( mysql_error() );  
$row0 = @mysql_fetch_array($result0);

         if($row0[0] == "F")
         {
          $attLevel = 10;
         }  
         else if($row0[0] == "E")
         {
          $attLevel = 5;
         } 
         else if ($row0[0] == "D")
         {
          $attLevel = 0;
         }
         else if($row0[0] == "C")
         {
          $attLevel = -5;
         }
         else if($row0[0] == "B")
         {
          $attLevel = -10;
         } 
         else if($row0[0] == "A")
         {
          $attLevel = -15;
         } 
         else if($row0[0] == "S")
         {
          $attLevel = -20;
         } 
         
$medLevel = 0;
$resT0 = "SELECT `a_medita` FROM `acnumber` WHERE a_acnum = '$acnum'";
$resultT0 = @mysql_query($resT0) or die( mysql_error() );  
$rowT0 = @mysql_fetch_array($resultT0);

         if($rowT0[0] == "F")
         {
          $medLevel = 10;
         }  
         else if($rowT0[0] == "E")
         {
          $medLevel = 5;
         } 
         else if ($rowT0[0] == "D")
         {
          $medLevel = 0;
         }
         else if($rowT0[0] == "C")
         {
          $medLevel = -5;
         }
         else if($rowT0[0] == "B")
         {
          $medLevel = -10;
         } 
         else if($rowT0[0] == "A")
         {
          $medLevel = -15;
         } 
         else if($rowT0[0] == "S")
         {
          $medLevel = -20;
         } 
         
$res = "SELECT count(m_att) FROM `cmind` WHERE `m_time` BETWEEN '$start' and '$end' and m_acnum = '$acnum'";
$res1 = "SELECT count(m_att) FROM `cmind` WHERE `m_time` BETWEEN '$start' and '$end' and m_acnum = '$acnum' and m_att >= (50-$attLevel)";
$res2 = "SELECT count(m_att) FROM `cmind` WHERE `m_time` BETWEEN '$start' and '$end' and m_acnum = '$acnum' and m_att <= (49-$attLevel) and m_att >= (26-$attLevel)";
$res3 = "SELECT count(m_att) FROM `cmind` WHERE `m_time` BETWEEN '$start' and '$end' and m_acnum = '$acnum' and m_att <= (25-$attLevel)";

$resT = "SELECT count(m_med) FROM `cmind` WHERE `m_time` BETWEEN '$start' and '$end' and m_acnum = '$acnum' and (m_med/1000) >= (25-$medLevel)";
$resT1 = "SELECT count(m_med) FROM `cmind` WHERE `m_time` BETWEEN '$start' and '$end' and m_acnum = '$acnum' and (m_med/1000) >= (55-$medLevel)";
$resT2 = "SELECT count(m_med) FROM `cmind` WHERE `m_time` BETWEEN '$start' and '$end' and m_acnum = '$acnum' and (m_med/1000) < (55-$medLevel) and (m_med/1000) >= (35-$medLevel)";
$resT3 = "SELECT count(m_med) FROM `cmind` WHERE `m_time` BETWEEN '$start' and '$end' and m_acnum = '$acnum' and (m_med/1000) < (35-$medLevel) and (m_med/1000) >= (25-$medLevel)";


$result = @mysql_query($res) or die( mysql_error() );  
$result1 = @mysql_query($res1) or die( mysql_error() );   
$result2 = @mysql_query($res2) or die( mysql_error() );   
$result3 = @mysql_query($res3) or die( mysql_error() );    

$resultT = @mysql_query($resT) or die( mysql_error() );  
$resultT1 = @mysql_query($resT1) or die( mysql_error() );   
$resultT2 = @mysql_query($resT2) or die( mysql_error() );   
$resultT3 = @mysql_query($resT3) or die( mysql_error() );    

$row = @mysql_fetch_array($result);
$row1 = @mysql_fetch_array($result1);
$row2= @mysql_fetch_array($result2);
$row3 = @mysql_fetch_array($result3);

$rowT = @mysql_fetch_array($resultT);
$rowT1 = @mysql_fetch_array($resultT1);
$rowT2 = @mysql_fetch_array($resultT2);
$rowT3 = @mysql_fetch_array($resultT3);

$high = @round($row1[0]/$row[0] * 100, 2);
$midle = @round($row2[0]/$row[0] * 100, 2);
$low = @round($row3[0]/$row[0] * 100, 2);

$highT = @round($rowT1[0]/$rowT[0] * 100, 2);
$midleT = @round($rowT2[0]/$rowT[0] * 100, 2);
$lowT = @round($rowT3[0]/$rowT[0] * 100, 2);


?>
		<script type="text/javascript" src="http://cdn.hcharts.cn/jquery/jquery-1.8.3.min.js"></script>
		<style type="text/css">

		</style>
		<script type="text/javascript">

$(function () {
    Highcharts.chart('container', {
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false,
            type: 'pie'
        },
        title: {
            text: '<?php echo $start ?> to <?php echo $end ?>'
        },
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: true,
                    format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                    style: {
                        color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                    }
                }
            }
        },
        series: [{
            name: 'Brands',
            colorByPoint: true,
            data: [{
                name: 'high attention',
                y: <?php echo $high ?>
            }, {
                name: 'low attention',
                y: <?php echo $midle ?>
            }, {
                name: 'no attention',
                y: <?php echo $low ?>
            }]
        }]
    });
});

</script>

<script type="text/javascript">

$(function () {
    Highcharts.chart('container2', {
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false,
            type: 'pie'
        },
        title: {
            text: '<?php echo $start ?> to <?php echo $end ?>'
        },
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: true,
                    format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                    style: {
                        color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                    }
                }
            }
        },
        series: [{
            name: 'Brands',
            colorByPoint: true,
            data: [{
                name: 'high meditation',
                y: <?php echo $highT ?>
            }, {
                name: 'low meditation',
                y: <?php echo $midleT ?>
            }, {
                name: 'no meditation',
                y: <?php echo $lowT ?>
            }]
        }]
    });
});

</script>
	</head>
        <div2>
	<body align="center">
<script src="https://code.highcharts.com/highcharts.js"></script>
<script src="https://code.highcharts.com/modules/exporting.js"></script>

<div id="container" style="min-width: 310px; height: 400px; max-width: 600px; margin: 0 auto"></div>
<div id="container2" style="min-width: 310px; height: 400px; max-width: 600px; margin: 0 auto"></div>

<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<div>

<table width="600" height="80" align="center">
<tr><td align="center">
<br>
<?php
if($high >= 40 && $high < 45)
{
 ?>
 <b><p class='mysize2'>專注程度：F</p></b><br>
<?php 
} 
else if($high >= 45 && $high < 50)
{
?>
<b><p class='mysize2'>專注程度：E</p></b><br>
<?php
}
else if($high >= 50 && $high < 55)
{
?>
<b><p class='mysize2'>專注程度：D</p></b><br>
<?php
}
else if($high >= 55 && $high < 60)
{
?>
<b><p class='mysize2'>專注程度：C</p></b><br>
<?php
}
else if($high >=60 && $high < 65)
{
?>
<b><p class='mysize2'>專注程度：B</p></b><br>
<?php
}
else if($high >=65 && $high < 70)
{
?>
<b><p class='mysize2'>專注程度：A</p></b><br>
<?php
}
else if($high >=70 && $high < 75)
{
?>
<b><p class='mysize2'>專注程度：S</p></b><br>
<?php
}
?> 


<?php
if($highT >= 40 && $highT < 45)
{
 ?>
 <b><p class='mysize2'>放鬆程度：F</p></b><br>
<?php 
} 
else if($highT >= 45 && $highT < 50)
{
?>
<b><p class='mysize2'>放鬆程度：E</p></b><br>
<?php
}
else if($highT >= 50 && $highT < 55)
{
?>
<b><p class='mysize2'>放鬆程度：D</p></b><br>
<?php
}
else if($highT >= 55 && $highT < 60)
{
?>
<b><p class='mysize2'>放鬆程度：C</p></b><br>
<?php
}
else if($highT >=60 && $highT < 65)
{
?>
<b><p class='mysize2'>放鬆程度：B</p></b><br>
<?php
}
else if($highT >=65 && $highT < 70)
{
?>
<b><p class='mysize2'>放鬆程度：A</p></b><br>
<?php
}
else if($highT >=70)
{
?>
<b><p class='mysize2'>放鬆程度：S</p></b><br>
<?php
}
?> 

</td></tr>
</table>

</div>

<form name="form" method="post" action="PicShowLessCheck.php">
<table width="300" height="80" align="center">
<tr><td align="center">    
<input type="hidden" name="acnumT" value="<?php echo $_POST['acnumT'] ?>">
<input type="submit" name="send" value="返回" />
</td>
</tr>
</table>
</form>

	</body>
        </div2>
</html>

