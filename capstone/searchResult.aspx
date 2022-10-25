<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="searchResult.aspx.cs" Inherits="capstone.searchResult" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <!-- CSS only -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-iYQeCzEYFbKjA/T2uDLTpkwGzCiq6soy8tYaI1GyVh/UjpbCx/TYkiZhlZB6+fzT" crossorigin="anonymous" />
    <!-- JavaScript Bundle with Popper -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-u1OknCvxWvY5kfmNBILK2hRnQC3Pr17a+RTT6rIHI7NnikvbZlHgTPOOmMi466C8" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="Products.css" />

</head>
<body>
    <form id="form1" runat="server">
        <div>
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
                            <img runat="server" data-bs-toggle="tooltip" data-bs-placement="left" title="View Profile" id="profilePic" src="Images/profileDefaultGreen.png" class="rounded-circle" alt="..." style="height: 50px;" />
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
            <div >
            <div class="container-md m-auto p-3 ">
                <div class="m-auto p-3">
                <h4 class="display-6">Results
                   
                    </h4>
                    </div>
                    <asp:ListView ID="searchRes"  runat="server">

                         <EmptyDataTemplate>
                                        <div class="row justify-content-center">
                                            <div class="col p-3 m-3">
                                            <p class=" display-6 text-secondary">No Results</p>
                                                </div>
                                        </div>
                         </EmptyDataTemplate>

                        <ItemTemplate>
                            <div>
                                <asp:LinkButton Style="text-decoration: none !important;" CommandArgument='<%#Eval("[id]")%>' runat="server" OnClick="goToDetails">
                                    <div class="card  shadow p-3 mb-5 bg-white rounded" style="width: 18rem;">
                                        <img  src='<%# getImg(Eval("[picture]")) %>' class="card-img-top" alt="..." style="width: 16rem; height:18rem;"/>
                                        <div class="card-body">
                                            <h5 class="text text-dark card-title"><%# Eval("[name]") %></h5>
                                            <p class="text text-dark card-text"><%# Eval("[description]") %></p>
                                            <br />
                                            <asp:LinkButton class="btn btn-primary fa"  CommandArgument='<%#Eval("[id]")%>' OnClick="addToCart" runat="server"  Text='<%# getText(Eval("[id]")) %>' />
                                        </div>
                                    </div>
                                </asp:LinkButton>
                            </div>
                        </ItemTemplate>

                        <LayoutTemplate>
                            <div class="container">

                                <div id="itemPlaceHolderContainer" class="row row-cols-md-3" runat="server">
                                    <div id="itemPlaceHolder" class="col" runat="server"></div>
                                </div>

                            </div>
                        </LayoutTemplate>
                    </asp:ListView>

            </div>

        </div>

            <div class="container-md m-auto p-3 ">
                <div class="m-auto p-3">
                <h4 class="display-6">Similar Products
                   
                    </h4>
                    </div>
                    <asp:ListView ID="similar"  runat="server">

                         <EmptyDataTemplate>
                                        <div class="row justify-content-center">
                                            <div class="col p-3 m-3">
                                            <p class=" display-6 text-secondary">No Results</p>
                                                </div>
                                        </div>
                         </EmptyDataTemplate>

                        <ItemTemplate>
                            <div>
                                <asp:LinkButton Style="text-decoration: none !important;" CommandArgument='<%#Eval("[id]")%>' runat="server" OnClick="goToDetails">
                                    <div class="card  shadow p-3 mb-5 bg-white rounded" style="width: 20rem;">
                                        <img  src='<%# getImg(Eval("[picture]")) %>' class="card-img-top" alt="..." style="width: 18rem; height:18rem;"/>
                                        <div class="card-body">
                                            <h5 class="text text-dark card-title"><%# Eval("[name]") %></h5>
                                            <p class="text text-dark card-text"><%# Eval("[description]") %></p>
                                            <br />
                                        </div>
                                    </div>
                                </asp:LinkButton>
                            </div>
                        </ItemTemplate>

                        <LayoutTemplate>
                            <div class="container">

                                <div id="itemPlaceHolderContainer" class="row row-cols-md-3" runat="server">
                                    <div id="itemPlaceHolder" class="col" runat="server"></div>
                                </div>

                            </div>
                        </LayoutTemplate>
                    </asp:ListView>

            </div>
        </div>
        <asp:Label runat="server" ID="msg"></asp:Label>
    </form>
</body>
</html>
