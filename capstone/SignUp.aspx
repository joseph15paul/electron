<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="capstone.SignUp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!-- CSS only -->
<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-iYQeCzEYFbKjA/T2uDLTpkwGzCiq6soy8tYaI1GyVh/UjpbCx/TYkiZhlZB6+fzT" crossorigin="anonymous"/>
    <!-- JavaScript Bundle with Popper -->
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-u1OknCvxWvY5kfmNBILK2hRnQC3Pr17a+RTT6rIHI7NnikvbZlHgTPOOmMi466C8" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="SignUp.css" />

</head>
<body>
    <form id="form1" runat="server">
        <div class="container border border-secondary border-3 rounded center shadow mt-5">
            <div class="col-md-12 text-center">
                    <asp:Label class="display-5 " ID="Label7" runat="server" Text="Create Account"></asp:Label>
                </div>
            <br />
            <div class="row">
                <div class="col-md-4">
                    <asp:Label class="h5 float-end" ID="Label1" runat="server" Text="Create Username"></asp:Label>
                </div>
                <div  class="col-md-4">
                    <asp:TextBox class="form-control textbox-border float-left" ID="username" runat="server" OnTextChanged="username_TextChanged"></asp:TextBox>
                </div>
                <div  class="col-md-4">
                    <asp:RequiredFieldValidator class="h6 text-danger" ID="userNameRequiredFieldValidator" runat="server" BorderColor="#FF6600" ControlToValidate="username" ErrorMessage="Username is required" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:CustomValidator class="h6 text-danger" ID="userNameExistence" runat="server" ControlToValidate="username" ErrorMessage="Username already exists" OnServerValidate="CustomValidator1_ServerValidate" Display="Dynamic"></asp:CustomValidator>
                </div>
            </div>

            <br />

             <div class="row">
                <div class="col-md-4">
                    <asp:Label  class="h5 float-end"  ID="Label2" runat="server" Text="Enter first name"></asp:Label>
                 </div>
                <div  class="col-md-4">
                    <asp:TextBox class="form-control textbox-border" ID="firstName" runat="server"></asp:TextBox>
                </div>
                <div  class="col-md-4">
                       <asp:RequiredFieldValidator class="h6 text-danger" ID="FirstNameRequiredFieldValidator" runat="server" BorderColor="#FF6600" ControlToValidate="firstName" ErrorMessage="first name is required" ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
              </div>
            <br />

            <div class="row">
                <div class="col-md-4">
                    <asp:Label class="h5 float-end" ID="Label3" runat="server" Text="Enter lastname"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox class="form-control textbox-border" ID="lastName" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-4">
                    <asp:RequiredFieldValidator class="h6 text-danger" ID="LastNameRequiredFieldValidator1" runat="server" BorderColor="#FF6600" ControlToValidate="lastName" ErrorMessage="last name is required" ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
            </div>
            <br />

            <div class="row">
                <div class="col-md-4">
                    <asp:Label class="h5 float-end" ID="Label4" runat="server" Text="Enter password"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox class="form-control textbox-border" TextMode="Password" ID="password" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-4">
                    <asp:RequiredFieldValidator class="h6 text-danger" ID="passwordRequiredFieldValidator2" runat="server" BorderColor="#FF6600" ControlToValidate="password" ErrorMessage="password is required" ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
                
            </div>
            <br />

            <div class="row">
                <div class="col-md-4">
                    <asp:Label class="h5 float-end"  runat="server" Text="Enter password again"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox class="form-control textbox-border" ID="confirmPassword" TextMode="Password" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-4">
                    <asp:RequiredFieldValidator class="h6 text-danger" ID="RequiredFieldValidator1" runat="server" BorderColor="#FF6600" ControlToValidate="password" ErrorMessage="password is required" ForeColor="Red"></asp:RequiredFieldValidator>
                    <br />
                    <asp:CompareValidator ID="CompareValidator1" runat="server" class="h6 text-danger" BorderColor="#FF6600" ControlToValidate="confirmPassword" ErrorMessage="passwords are not the same" ForeColor="Red" ControlToCompare="password"></asp:CompareValidator>
                </div>
                
            </div>
            <br />


            <div class="row">
                <div class="col-md-4">
                    <asp:Label class="h5 float-end" ID="Label5" runat="server" Text="Enter phone number (optional)"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox class="form-control textbox-border" ID="phone" runat="server"></asp:TextBox>
                </div>

                <div class="col-md-4">
                    <asp:CustomValidator class="h6 text-danger" ID="CustomValidator1" runat="server" ErrorMessage="Enter a correct 10 digit phone number" ControlToValidate="phone" OnServerValidate="CustomValidator1_ServerValidate1"></asp:CustomValidator>
                </div>
            </div>
            <br />

          

            <div class="col-md-12 text-center">
                <asp:Button class="btn button-yellow" ID="Button1" runat="server" OnClick="Button1_Click" Text="Create Account" />
            </div>

            <div class="col-md-12 text-center">
                    <asp:Label class="h6 text-success" ID="Label8" runat="server" Text=""></asp:Label>
            </div>
         </div>
    </form>
</body>
</html>
