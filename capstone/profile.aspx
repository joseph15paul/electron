<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="profile.aspx.cs" Inherits="capstone.profile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!-- CSS only -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-iYQeCzEYFbKjA/T2uDLTpkwGzCiq6soy8tYaI1GyVh/UjpbCx/TYkiZhlZB6+fzT" crossorigin="anonymous" />
    <!-- JavaScript Bundle with Popper -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-u1OknCvxWvY5kfmNBILK2hRnQC3Pr17a+RTT6rIHI7NnikvbZlHgTPOOmMi466C8" crossorigin="anonymous"></script>
    <link rel="stylesheet" type="text/css" href="profile.css" />

    <script src="https://kit.fontawesome.com/a076d05399.js" crossorigin="anonymous"></script>
</head>
<body>
    <form id="form1" runat="server">

        <nav class="navbar navbar-expand-md navbar-dark bg-dark mb-3">
            <div class="container">
                <a class="navbar-brand" href="Home.aspx">
                    <img id="logo"
                        src="Images/electro1.jpg" alt="Logo"
                        draggable="false" height="50" /></a>





                <div class="d-flex align-items-center w-100 form-search">
                    <div class="input-group">

                        <asp:Button ID="searchCategory" runat="server" class="btn btn-light dropdown-toggle shadow-0" type="button" data-bs-toggle="dropdown"
                            aria-expanded="false" Style="padding-bottom: 0.4rem;" Text="All"></asp:Button>
                        <div class=" dropdown-menu dropdown-menu-dark fa-ul">

                            <asp:LinkButton class="dropdown-item" CommandArgument='All' OnClick="categorySelect" runat="server"><span class="fa-li pe-2"><i class="fas fa-search"></i></span>All</asp:LinkButton>


                            <asp:ListView ID="categories" runat="server">
                                <ItemTemplate>
                                    <asp:LinkButton class="dropdown-item" CommandArgument='<%#Eval("[name]")%>' OnClick="categorySelect" runat="server"><span class="fa-li pe-2"><i class="fas fa-search"></i></span><%#: Eval("name") %></asp:LinkButton>

                                </ItemTemplate>
                            </asp:ListView>

                        </div>
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                            <Services>

                                <asp:ServiceReference Path="~/WebService1.asmx" />

                            </Services>
                        </asp:ScriptManager>
                        <asp:TextBox runat="server" TextMode="Search" ID="SearchBox" class="form-control" placeholder="Search" aria-label="Search" />



                    </div>
                    <asp:LinkButton runat="server" OnClick="Search" class="text-white"><i class="fas fa-search ps-3"></i></asp:LinkButton>
                </div>

                <ul class="navbar-nav ms-3 align-items-center">

                    <li class="nav-item">
                        <a class="nav-link d-flex align-items-center ms-3 me-3" href="Cart.aspx">
                            <i class="fas fa-cart-plus"></i>Cart</a>
                    </li>

                    <li class="nav-item align-items-center">
                        <a class="nav-link d-flex me-3" style="width: 100px;" href="loginPage.aspx">Sign In/Out</a>
                    </li>


                    <li class="nav-item ">
                        <div class="nav-link d-flex align-items-center ms-6 btn-group  me-3 ">
                            <img runat="server" data-bs-toggle="tooltip" data-bs-placement="left" title="View Profile" id="profilePicc" src="Images/profileDefaultGreen.png" class="rounded-circle" alt="..." style="height: 50px;" />
                            <button type="button" class="btn btn-dark dropdown-toggle dropdown-toggle-split" data-bs-toggle="dropdown" aria-expanded="false">
                                <span class="visually-hidden">Toggle Dropdown</span>
                            </button>
                            <ul class="dropdown-menu">
                                <li><a class="dropdown-item" href="profile.aspx">Profile</a></li>
                                <li><a class="dropdown-item" href="orders.aspx">View Orders</a></li>

                            </ul>
                        </div>
                    </li>
                </ul>
            </div>
            <!-- Collapsible wrapper -->

            <!-- Container wrapper -->
        </nav>
        <!-- Navbar -->
        <div class="container rounded bg-white mt-5 mb-5">
            <div class="row">
                <div class="col-md-3 border-right align-items-center">
                    <div class="d-flex flex-column align-items-center text-center p-3 py-5">
                        <asp:Image ID="profilePic" runat="server" ImageUrl='Images/profileDefaultBlue.png' class="rounded-circle shadow" style=" height:150px;" /><br />
                        <asp:Label ID="userName" runat="server" Text="username" class="font-weight-bold h4"></asp:Label><span> </span></div>
                    <div>
                                    <label class="h5" for = "Image">Change profile picture</label>

                        <asp:FileUpload ID="FileUpload1" CssClass="change-password" runat="server" />
                        <br /><br />
         <asp:Label ID="lblmessage" CssClass="text-danger text-capitalize" runat="server" />
                    </div>
                    <div class="text-center"><asp:Button CssClass="btn btn-primary " runat="server" OnClick="uploadImage" Text="Upload"/></div>
                </div>

                <div class="col-md-5 border-right">
                    <div class="p-3 py-5">
                        <div class="d-flex justify-content-between align-items-center mb-3">
                            <h4 class="text-right">Profile Settings</h4>
                        </div>
                        <div class="row mt-2">
                            <div class="col-md-6">
                                <label class="labels">Name</label><asp:TextBox ID="firstName" runat="server" class="text form-control" placeholder="first name" ></asp:TextBox></div>
                            <div class="col-md-6">
                                <label class="labels">Surname</label><asp:TextBox ID="surname" runat="server" class="text form-control" placeholder="surname" ></asp:TextBox></div>
                        </div>
                        <div class="row mt-3">
                            <div class="col-md-12">
                                <label class="labels">Mobile Number</label> <asp:TextBox ID="MobileNumber" runat="server" class="text form-control" placeholder="Mobile Numbe" TextMode="Phone"></asp:TextBox></div>
                            <div class="col-md-12">                         
                                <label class="labels">Address Line 1</label><asp:TextBox ID="AddressLine1" runat="server" class="text form-control" placeholder="Address Line 1" ></asp:TextBox></div>
                            <div class="col-md-12">                         
                                <label class="labels">Address Line 2</label><asp:TextBox ID="AddressLine2" runat="server" class="text form-control" placeholder="Address Line 2" ></asp:TextBox></div>
                            <div class="col-md-12">                         
                                <label class="labels">Postcode</label><asp:TextBox ID="Postcode" runat="server" class="text form-control" placeholder="Postcode" ></asp:TextBox></div>
                            <div class="col-md-12">                         
                                <label class="labels">City</label><asp:TextBox ID="City" runat="server" class="text form-control" placeholder="City" ></asp:TextBox></div>
                                                     
                        </div>                                              
                        <div class="row mt-3">                              
                            <div class="col-md-6">                          
                                <label class="labels">Country</label><asp:TextBox ID="Country" runat="server" class="text form-control" placeholder="Country" ></asp:TextBox></div>
                            <div class="col-md-6">                          
                                <label class="labels">State</label><asp:TextBox ID="State" runat="server" class="text form-control" placeholder="State" ></asp:TextBox></div>
                        </div>
                        <div class="mt-5 text-center">
                            <asp:Button class="btn btn-primary profile-button" runat="server" OnClick="save" Text="Save Profile" /></div>
                    </div>
                </div>
                
                <div class="col-md-4">
            <div class="p-3 py-5">
                <div class="d-flex justify-content-between align-items-center experience"><span>Change Password</span><span class="border px-3 p-1 change-password"></span></div><br/>
                <div class="col-md-12"><label class="labels">Enter new password</label>
                    <asp:TextBox runat="server" class="form-control" ID="newPassword" placeholder="new password" TextMode="Password" /></div> <br/>
                <div class="col-md-12"><label class="labels">Confirm password</label>
                    <asp:TextBox  runat="server" class="form-control" placeholder="Enter password again" ID="confirmPassword" TextMode="Password" />
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Passwords don't match" ControlToCompare="newPassword" ControlToValidate="confirmPassword"></asp:CompareValidator>

                </div>
                <div class="text-center">
                            <asp:Button class="btn btn-primary profile-button" runat="server" OnClick="updatePassword" Text="Change password" /><br />
                    <asp:Label runat="server" ID="msg" CssClass="text-success" Text=""></asp:Label>
                </div>
                    </div>
            </div>
        </div>
            </div>
        

    </form>
</body>
</html>
