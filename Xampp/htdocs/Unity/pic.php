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
     $acnum=@$_POST['acnumT'];
//$res = mysql_query("select * from cmind where m_date = ");
$res = @mysql_query("SELECT * FROM `cmind` WHERE `m_time` BETWEEN '$start' and '$end' and m_acnum = '$acnum'");
while($row = @mysql_fetch_array($res)){
    $attention[] = intval(@$row['m_att']);  //注意这里必须要用intval强制转换，不然图表不能显示
    $meditation[] = intval(@$row['m_med']);
    $delta[] = intval(@$row['m_D']); 
    $theta[] = intval(@$row['m_T']);
    $lowAlpha[] = intval(@$row['m_LA']);
    $highAlpha[] = intval(@$row['m_HA']);
    $lowBeta[] = intval(@$row['m_LB']);
    $highBeta[] = intval(@$row['m_HB']);
    $lowGamma[] = intval(@$row['m_LG']);
    $highGamma[] = intval(@$row['m_HG']);
     
}
@$data = array(array("name"=>"attention","data"=>@$attention)
             ,array("name"=>"meditation","data"=>@$meditation)
             ,array("name"=>"delta","data"=>@$delta)
             ,array("name"=>"theta","data"=>@$theta)
             ,array("name"=>"lowAlpha","data"=>@$lowAlpha)
             ,array("name"=>"highAlpha","data"=>@$highAlpha)
             ,array("name"=>"lowBeta","data"=>@$lowBeta)
             ,array("name"=>"highBeta","data"=>@$highBeta)
             ,array("name"=>"lowGamma","data"=>@$lowGamma)
             ,array("name"=>"highGamma","data"=>@$highGamma)
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

<form name="form" method="post" action="PicSearchDate.php">
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
