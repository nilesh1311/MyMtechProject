<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="EncryptedParameterAuthentication.Registration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <h2>
                        Create a New Account
                    </h2>
                    <p>
                        Use the form below to create a new account.
                    </p>
                    <table>
                    <tr> <td><asp:Label ID="Label1" runat="server" >Name:</asp:Label></td>
                              <td>  <asp:TextBox ID="Name" runat="server" CssClass="textEntry"></asp:TextBox></td>
                    </tr>

                    <tr> 
                   <td> <asp:Label ID="EmailLabel" runat="server" >User Name (E-mail):</asp:Label></td>
                              <td>  <asp:TextBox ID="UserName" runat="server" CssClass="textEntry" AutoPostBack="True" OnTextChanged="UserName_TextChanged"></asp:TextBox></td>
                   </tr>
                   <tr> 
                       <td> <asp:Label ID="PasswordLabel" runat="server" >Password:</asp:Label></td>
                    <td><asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox></td>
                    </tr>
                   <tr> 
                      <td>  <asp:Label ID="ConfirmPasswordLabel" runat="server" >Confirm Password:</asp:Label></td>
                             <td>   <asp:TextBox ID="ConfirmPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox></td>
                    </tr>
                   <tr>  <td><asp:Label ID="Label2" runat="server" >Contact Number:</asp:Label></td>
                             <td>   <asp:TextBox ID="ContactNumber" runat="server" CssClass="textEntry"></asp:TextBox></td>
                    </tr>
                    <tr>  <td><asp:Label ID="Label3" runat="server" >Address:</asp:Label></td>
                             <td>   <asp:TextBox ID="Address" runat="server" CssClass="textEntry" Rows="4" TextMode="MultiLine" Width="98.5%"></asp:TextBox></td>
                    </tr>
                     <tr>
                     <td><br /></td>
                     </tr>
                    <tr>
                    <td></td>
                            <td><asp:Button ID="BTSubmit" runat="server" Text="Register" OnClick="BTSubmit_Click" BackColor="#3AC9DB" Width="100px" BorderColor="#0066FF" />
                            </td>
                    </tr>
                   </p>
                   </table>
</asp:Content>
