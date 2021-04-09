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

</style>
</p>
<html>
<head>
<title>Daily Mindset</title>
</head>
<body>

<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<div>
<form name="form" method="post" action="PicShowLess.php">
<table width="300" height="80" align="center">
<tr><td align="center">
        
範例：開始時間：2016-01-01 00:00:00 <br>
<br>
&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
結束時間：2016-12-31 23:59:59 <br>
<br>
開始時間：<input type="text" name="start" /> <br>
<br>
結束時間：<input type="text" name="end" /> <br>
<br>
<input type="hidden" name="acnumT" value="<?php echo $_POST['acnumT'] ?>">


<input type="submit" name="send" value="確定" />
</td></tr>
</table>

</form>
    
    <form name="form" method="post" action="PicChoseMenu.php">
<table width="300" height="80" align="center">
<tr><td align="center">    
<input type="hidden" name="acnumT" value="<?php echo $_POST['acnumT'] ?>">
<input type="submit" name="send" value="返回" />
</td>
</tr>
</table>
</form>

</div>

</body>
</html>
