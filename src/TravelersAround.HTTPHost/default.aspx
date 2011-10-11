<%@ Page Language="C#"  %>
<%@ Import Namespace="System.IO" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <ul>
        <li>Cache file loaded: <%=  File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Bin\\cache.db"))%></li>
        <li>Geo database loaded: <%=  File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data\\GeoLiteCity.dat"))%></li>
    </ul>
</body>
</html>
