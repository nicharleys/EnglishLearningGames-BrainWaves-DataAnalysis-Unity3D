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
     $acnum2=@$_POST['acnumT'];
//$res = mysql_query("select * from cmind where m_date = ");
$res = @mysql_query("SELECT * FROM `cmind` WHERE `m_time` BETWEEN '$start' and '$end' and m_acnum = '$acnum'");
$res2 = @mysql_query("SELECT * FROM `cmind` WHERE `m_time` BETWEEN '$start2' and '$end2' and m_acnum = '$acnum2'");
while($row = @mysql_fetch_array($res)){
    $attention[] = intval(@$row['m_att']);  //注意这里必须要用intval强制转换，不然图表不能显示
    $meditation[] = intval(@$row['m_med']);
   
     
}
while($row2 = @mysql_fetch_array($res2)){
    $attention2[] = intval(@$row2['m_att']);  //注意这里必须要用intval强制转换，不然图表不能显示
    $meditation2[] = intval(@$row2['m_med']);
     
}
@$data = array(array("name"=>"attention[$acnum]","data"=>@$attention)
             ,array("name"=>"meditation[$acnum]","data"=>@$meditation)
             ,array("name"=>"attention2[$acnum2]","data"=>@$attention2)
             ,array("name"=>"meditation2[$acnum2]","data"=>@$meditation2)
            );
@$data = json_encode($data);
?>
		<script type="text/javascript" src="http://cdn.hcharts.cn/jquery/jquery-1.8.3.min.js"></script>
		<style type="text/css">

		</style>
		<script type="text/javascript">
$(function () {
    $('#container').highcharts({
        title: {
            text: 'Daily Mindwave',
            x: -20 //center
        },
        subtitle: {
            text: 'Source : www.jscto.net',
            x: -20
        },
        xAxis: {
            categories: ['1', '2', '3', '4', '5', '6','7', '8', '9', '10',
                         '11', '12', '13', '14', '15', '16','17', '18', '19', '20',
                         '21', '22', '23', '24', '25', '26','27', '28', '29', '30', '31'
                        ]
        },
        yAxis: {
            title: {
                text: 'Value'
            },
            plotLines: [{
                value: 0,
                width: 1,
                color: '#808080'
            }]
        },
        tooltip: {
            valueSuffix: ''
        },
        legend: {
            layout: 'vertical',
            align: 'right',
            verticalAlign: 'middle',
            borderWidth: 0
        },
        series:<?php echo $data?>
    });
});
		</script>
	</head>
        <div2>
	<body align="center">
<script src="http://cdn.hcharts.cn/highcharts/highcharts.js"></script>
<script src="http://cdn.hcharts.cn/highcharts/modules/exporting.js"></script>

<div id="container" style="min-width: 310px; height: 400px; margin: 0 auto"></div>
<br>

<form name="form" method="post" action="PicComparison.php">
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
