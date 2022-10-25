<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="adminLogin.aspx.cs" Inherits="capstone.adminLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <!-- CSS only -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-iYQeCzEYFbKjA/T2uDLTpkwGzCiq6soy8tYaI1GyVh/UjpbCx/TYkiZhlZB6+fzT" crossorigin="anonymous" />
    <!-- JavaScript Bundle with Popper -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-u1OknCvxWvY5kfmNBILK2hRnQC3Pr17a+RTT6rIHI7NnikvbZlHgTPOOmMi466C8" crossorigin="anonymous"></script>

</head>
<body>
    <form id="form2" runat="server">


        <div class="container text-center p-5 m-5 border-1">
            <div class="row">
                <div class="col-3">
                </div>
                <div class="col-6 card shadow p-4 mt-6">

                    <label class="display-4">Admin Login</label>
                    <br />
                    <br />
                    <br />
                    <!-- Email input -->
                    <div class="form-outline mb-4">
                        <input runat="server" id="username" class="form-control" required/>
                        <label class="form-label" for="form2Example1">UserName</label>
                    </div>

                    <!-- Password input -->
                    <div class="form-outline mb-4">
                        <input runat="server" type="password" id="password" class="form-control" required/>
                        <label class="form-label" for="password">Password</label>
                    </div>



                    <!-- Submit button -->
                    <asp:Button runat="server" OnClick="signIn" Text="Sign in" class="btn btn-primary btn-block mb-4"></asp:Button>
                    <br />
                    <asp:Label runat="server" class="form-label  text-success" ID="msg"></asp:Label>




                </div>
            </div>
        </div>
    </form>
</body>
</html>
