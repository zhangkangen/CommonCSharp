<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Info.aspx.cs" Inherits="TestLog4net.Info" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="Scripts/jquery-2.2.0.js"></script>
    <script>
        $(function () {
            $('#btn').click(function () {
                if ($('#iframe').attr('src') == 'http://www.baidu.com') {
                    $('#iframe').attr('src', 'http://www.qq.com');
                } else {
                    $('#iframe').attr('src', 'http://www.baidu.com');
                }
            });
        });
    </script>
</head>
<body>
    <input id="btn" type="button" name="name" value="刷新" />
    <form id="form1" runat="server">
        <div>
            Info页面
        </div>
    </form>
    <iframe id="iframe" src="http://www.baidu.com" />

</body>
</html>
