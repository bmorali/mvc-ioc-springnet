<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Error</title>
</head>
<body>
    <div align="center">
        Error occurred. Please check your log file for details.
        Try again loading again:
        <a href="<%= Url.Action("Index", "Authenticate") %>" >Login page</a>
    </div>
</body>
</html>
