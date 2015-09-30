<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmailSignupExport.aspx.cs" Inherits="Landmark.layouts.Landmark.EmailSignupExport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h1>Landmark Email Signup Export</h1>
        <div>
            <label>From Date:</label>
            <asp:calendar runat="server" ID="fromDate"></asp:calendar>
            <label>To Date:</label>
            <asp:calendar runat="server" ID="toDate"></asp:calendar>
        </div>
    </div>
    </form>
</body>
</html>
