<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="WebTechAssignment3.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Styles/Stylesheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="login" runat="server">
    <div id = "loginPage" align ="center" >  
    <asp:Image ID="cover"  runat="server" ImageUrl= "library.jpg" Height="212px" Width="900px" />   
    <div id = "left" style='float:left;'> 
        <br />
        <br /> 
        <asp:Label ID="lbl_emailSignin" runat="server" Text="E-Mail"></asp:Label><br />
        <asp:TextBox ID="txt_emailSignin" runat="server" Width="154px" Text = "user@gmail.com" ></asp:TextBox>
        <br/>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid email format" ControlToValidate="txt_emailSignin" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"> </asp:RegularExpressionValidator>
        <br />
        <br />
        <asp:Label ID="lbl_passwordSignin" runat="server" Text="Password"></asp:Label><br />
        <asp:TextBox ID="txt_passwordSignin"  runat="server" TextMode="Password" Width="148px" Text = "user01"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btn_login" runat="server" onclick="loginButton_Click" Text="Sign in"/>
        <br />
        <br />
        <asp:Label ID="lbl_error" runat="server" Text=" "></asp:Label>
    </div>
    <div id = "right">
        
        <br />
        <br />
        <asp:Label ID="lbl_firstname" runat="server" Text="Firstname"></asp:Label>
        <br />
        <asp:TextBox ID="txt_firstname" runat="server" Width="148px"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="lbl_lastname" runat="server" Text="Lastname"></asp:Label>
        <br />
        <asp:TextBox ID="txt_lastname" runat="server" Width="148px"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="lbl_birthdate" runat="server" Text="Birthdate"></asp:Label>
        <br />
        <asp:TextBox ID="txt_birthdate" runat="server" TextMode="Date" Width="148px"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="lbl_emailSignup" runat="server" Text="E-Mail"></asp:Label>
        <br />
        <asp:TextBox ID="txt_emailSignup" runat="server" Width="148px" Text = "user@gmail.com"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="lbl_passwordSignup" runat="server" Text="Password"></asp:Label>
        <br />
        <asp:TextBox ID="txt_passwordSignup" runat="server" TextMode ="Password" Width="148px"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btn_signup" runat="server" onclick="btn_signup_Click" 
            Text="Sign up" />
        <br />       
        <asp:Label ID="lbl_errorSignup" runat="server" Text=" "></asp:Label>
        <br />
    </div>
    
    </div>
    </form>
    
</body>
</html>
