<p>
<style type="text/css">
div 
{
  width: 600px;
  height: 300px;
  position: absolute;
  top: 65%;
  left: 50%;
  margin: -150px 0 0 -300px;
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
.mysize2 {font-size:320%;}
</style>
</p>
<html>
<head>
<title>Daily Mindset</title>
</head>
<body>

<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<div>

<table width="600" height="80" align="center">
<tr><td align="center">
<p class='mysize2'>Daily Mindset</p><br><br>
<form action="PicSearchDate.php" method="post">
<input type="hidden" name="acnumT" value="<?php echo $_POST['acnumT'] ?>">
<input type="submit" name="send" value="查詢單筆資料">
</form>

<form action="PicComparison.php" method="post">
<input type="hidden" name="acnumT" value="<?php echo $_POST['acnumT'] ?>">
<input type="submit" name="send" value="比對兩筆資料">
</form>

<form action="PicShowLessCheck.php" method="post">
<input type="hidden" name="acnumT" value="<?php echo $_POST['acnumT'] ?>">
<input type="submit" name="send" value="簡化資料">
</form>

<input type = "button" value="返回" onclick="location.href='EnterMind.php'">
</td></tr>
</table>


</div>

</body>
</html>
