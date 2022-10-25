﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="adminSalesReport.aspx.cs" Inherits="capstone.adminSalesReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   <link rel="stylesheet" href="adminControls.css" />
    <!-- CSS only -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-iYQeCzEYFbKjA/T2uDLTpkwGzCiq6soy8tYaI1GyVh/UjpbCx/TYkiZhlZB6+fzT" crossorigin="anonymous" />
    <!-- JavaScript Bundle with Popper -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-u1OknCvxWvY5kfmNBILK2hRnQC3Pr17a+RTT6rIHI7NnikvbZlHgTPOOmMi466C8" crossorigin="anonymous"></script>

    <script src="https://kit.fontawesome.com/a076d05399.js" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#sidebarCollapse').on('click', function () {
                $('#sidebar').toggleClass('active');
            });
        });




    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
             <div class="wrapper">
                <!-- Sidebar  -->
                
                <nav id="sidebar"  >
                    <div class="sidebar-header ">
                        <h3>Admin Controls  </h3>
                    </div>

                    <ul class="list-unstyled components">
                        <li class="side-bar-item ">
                            <a style="text-decoration: none; color: black"   href="adminControls.aspx">Product Entry</a>
                        </li>

                        <li class="side-bar-item ">
                            <a style="text-decoration: none; color: black" href="adminViewProducts.aspx">View All Products</a>
                        </li>
                        <li class="side-bar-item ">
                            <a style="text-decoration: none; color: black"  href="adminDeleteProd.aspx">Delete Product</a>
                        </li>
                        <li class="side-bar-item ">
                            <a style="text-decoration: none; color: black"  href="adminAddOffer.aspx">Add Offers</a>
                        </li>
                        <li class="side-bar-item ">
                            <a style="text-decoration: none; color: black"  href="adminViewOffer.aspx">View All Offers</a>
                        </li>

                        <li class="side-bar-item ">
                            <a style="text-decoration: none; color: black"  href="adminRemoveOffer.aspx">Remove Offers</a>
                        </li>                     
                        <li class="side-bar-item ">
                            <a style="text-decoration: none; color: black"  href="adminViewUsers.aspx">View All Users</a>
                        </li>                     
                        <li class="side-bar-item ">
                            <a style="text-decoration: none; color: black"  href="adminRemoveUser.aspx">Remove a User</a>
                        </li>
                        <li class="side-bar-item ">
                            <a style="text-decoration: none; color: black"  href="adminViewAdmins.aspx">View All Admins</a>
                        </li>                     
                        <li class="side-bar-item ">
                            <a style="text-decoration: none; color: black"  href="adminAddAdmin.aspx">Add An Admin</a>
                        </li>                     
                        
                         <li class="side-bar-item ">
                            <a style="text-decoration: none; color: black"  href="adminSalesReport.aspx">View Sales Report</a>
                        </li>
                        <li class="side-bar-item ">
                            <a style="text-decoration: none; color: black"  href="adminOnlineUsers.aspx">View Online Users</a>
                        </li>



                    </ul>

                </nav>
                    
                <!-- Page Content  -->

                <div id="ProductEntry" class="card shadow w-100 m-2 cont">
                    <div class="container  p-4 m-4 border-1" >
                        <div class="row">
                            <div class="col-3">
                            </div>
                            <div class="col-6 ">

                                <label class="display-4">Sales</label>
                                <br />
                                <br />
                                <br />
                                <div class="form-outline mb-4">
                                    <input runat="server" type="date" id="start" class="form-control" required />
                                    <label class="form-label h5" for="form2Example1">Start Date</label>
                                </div>

                                <div class="form-outline mb-4">
                                    <input runat="server" id="end" type="date" class="form-control" />
                                    <label class="form-label h5" for="form2Example1">End Date</label>
                                </div>

                                

                                
                                <asp:Button runat="server" OnClick="submit" Text="View" class="btn btn-primary btn-block mb-4"></asp:Button>
                                <br />

                                <asp:GridView ID="orders" runat="server" CssClass="p-3 m-3 table table-striped-columns table-bordered table-secondary"></asp:GridView>
                                <br />
                                <br />
                                <asp:Label runat="server" class="form-label text text- text-dark" ID="msg" Font-Size="Larger"></asp:Label>

                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </form>
</body>
</html>
