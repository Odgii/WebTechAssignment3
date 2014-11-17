<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="WebTechAssignment3.WebForm1"
    MasterPageFile="~/MainMaster.master" Title="Content Page 1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="Server">
    <form id="login" runat="server">
    <div class="login-box">
        <h1>
            Existing user?<br />
            Please login here</h1>
        <asp:Label ID="lbl_error" runat="server" Text=" " CssClass="error-message"></asp:Label>
        <asp:Label ID="lbl_emailSignin" runat="server" Text="E-Mail:" CssClass="label-login"></asp:Label>
        <asp:TextBox ID="txt_emailSignin" runat="server" Width="154px" Text="user@gmail.com"></asp:TextBox>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid email format"
            ControlToValidate="txt_emailSignin" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"> </asp:RegularExpressionValidator>
        <br />
        <asp:Label ID="lbl_passwordSignin" runat="server" Text="Password:" CssClass="label-login"></asp:Label>
        <asp:TextBox ID="txt_passwordSignin" runat="server" TextMode="Password" Width="148px" Text="user01"></asp:TextBox>


        <asp:Button ID="btn_login" runat="server" OnClick="loginButton_Click" Text="Sign in" CssClass="button"  />
        
    </div>
    <div class="login-box">
        <h1>
            New here?<br />
            Register an account for free</h1>

            <asp:Label ID="lbl_errorSignup" runat="server" Text=" " CssClass="error-message"></asp:Label>

            <asp:Label ID="lbl_firstname" runat="server" Text="First name:" CssClass="label-login"></asp:Label>
            <asp:TextBox ID="txt_firstname" runat="server" Width="148px"></asp:TextBox>
            <br />
            <asp:Label ID="lbl_lastname" runat="server" Text="Last name:" CssClass="label-login"></asp:Label>
            <asp:TextBox ID="txt_lastname" runat="server" Width="148px"></asp:TextBox>
            <br />
            <asp:Label ID="lbl_birthdate" runat="server" Text="Birthdate:" CssClass="label-login"></asp:Label>
            <asp:TextBox ID="txt_birthdate" runat="server" TextMode="Date" Width="148px"></asp:TextBox>
            <br />
            <asp:Label ID="lbl_emailSignup" runat="server" Text="E-Mail:" CssClass="label-login"></asp:Label>
            <asp:TextBox ID="txt_emailSignup" runat="server" Width="148px" Text="user@gmail.com"></asp:TextBox>
            <br />
            <asp:Label ID="lbl_passwordSignup" runat="server" Text="Password:" CssClass="label-login"></asp:Label>
            <asp:TextBox ID="txt_passwordSignup" runat="server" TextMode="Password" Width="148px"></asp:TextBox> <asp:Button ID="btn_signup" runat="server" OnClick="btn_signup_Click" Text="Sign up" CssClass="button" />


    </div>
    <div id="loginPage" align="center">
        <asp:Image ID="cover" runat="server" ImageUrl="/img/library.jpg" Height="212px" Width="900px" />
        <div id="left" style='float: left;'>          
        </div>
        <div id="right">
         
            <br />
            
            <br />
            
            <br />
        </div>
    </div>
    </form>
</asp:Content>
