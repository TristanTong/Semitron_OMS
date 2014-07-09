<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadifyPage.aspx.cs"
    Inherits="Semitron_OMS.UI.Admin.UserControl.UploadifyPage" %>

<%@ Register Src="~/Admin/UserControl/UploadifyControl.ascx" TagName="UploadifyControl"
    TagPrefix="uc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="background-color: #F7F7F7">
    <form id="form1" runat="server">
    <div>
        <uc:UploadifyControl ID="UploadifyControl1" runat="server" />
    </div>
    </form>
</body>
</html>
