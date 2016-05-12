<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="EncryptedParameterAuthentication._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
     <h2>
        Authentication using Additional Encrypted Parameter
    </h2>
    <div style="margin-left:20px">
                    <p>
                        Login
                    </p>
                    <table>
                        <tr> 
                   <td> <asp:Label ID="EmailLabel" runat="server" >User Name:</asp:Label></td>
                              <td>  <asp:TextBox ID="UserName" runat="server" CssClass="textEntry"></asp:TextBox></td>
                   </tr>
                   <tr> 
                       <td> <asp:Label ID="PasswordLabel" runat="server" >Password:</asp:Label></td>
                    <td><asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox></td>
                    </tr>
                    <tr>
                     <td><br /></td>
                     </tr>
                    <tr>
                    <td></td>
                            <td>
                            <asp:Button ID="BTLogin" runat="server" Text="Login" OnClick="BTLogin_Click" BackColor="#3AC9DB" Width="100px" BorderColor="#0066FF" />
                            </td>
                    </tr>
                    </table>
    </div>
</asp:Content>
