<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="Trolley_Management_System._Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Page</title>

<%--&nbsp      <img src="images/LOGO.png" alt="Alternate Text" width="80px" />--%>

    <link href="style.css" rel="stylesheet" />
     <script type="text/javascript">  
        function alertMessage() {  
            alert('JavaScript Function Called!');  
        }  
     </script> 
</head>
<body>
    <br />
       <%-- <h2 style="text-align: center;margin-top: 10px;color: #fff;"><marquee direction="right">WMS FiFo & Component Tracibility/marquee></h2>--%>
    
  
    <div class="loginbox">   
        <img src="images/User.png" alt="Alternate Text" class="user"/>
   <%--     <h2><i>Log In Here</i></h2>--%>
        <br />
        <form runat="server">
        <asp:Label ID="lblEmail" runat="server" Text="Email" CssClass="lblemail"></asp:Label>
            <br>
        <asp:TextBox ID="txtEmail" runat="server" CssClass="txtemail" placeholder="Enter Email"></asp:TextBox>
            <br>
           <asp:Label ID="lblpass" runat="server" Text="Password" CssClass="lblpass"></asp:Label>
            <br>
        <asp:TextBox ID="txtpass" runat="server" CssClass="txtpass" placeholder="***********" TextMode="Password"></asp:TextBox>
<br>
        <asp:Button ID="btnsubmit" runat="server" Text="Login" CssClass="btnsubmit" />
            </form>
    </div>
</body>
</html>


