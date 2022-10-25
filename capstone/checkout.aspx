<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="checkout.aspx.cs" Inherits="capstone.checkout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!-- CSS only -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-iYQeCzEYFbKjA/T2uDLTpkwGzCiq6soy8tYaI1GyVh/UjpbCx/TYkiZhlZB6+fzT" crossorigin="anonymous" />
    <!-- JavaScript Bundle with Popper -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-u1OknCvxWvY5kfmNBILK2hRnQC3Pr17a+RTT6rIHI7NnikvbZlHgTPOOmMi466C8" crossorigin="anonymous"></script>
        <link rel="stylesheet" href="checkout.css" />


</head>
<body class="bgcolor">
    <form id="form1" runat="server">
        <div >

            <main class="mt-2 pt-2">
                <div class="container ">


                    <h2 class="my-5 h2 text-center">Checkout</h2>

                    <div class="row">
                        <div class="col-md-2 mb-4"></div>

                        <div class="col-md-8 mb-4">


                            <div class="card">

                                <div class="card-body">

                                    <div class="md-form mb-5">
                                        <input runat="server" type="text" id="address" class="form-control" required />
                                        <label for="address" class="">Address</label>
                                    </div>

                                    <div class="md-form mb-5">
                                        <input  runat="server" type="text" id="address2" class="form-control"  />
                                        <label for="address2" class="">Address 2 (optional)</label>
                                    </div>


                                    <div class="row">


                                        <div class="col-lg-4 col-md-12 mb-4">

                                            <label for="country">Country</label>
                                            <input type="text" id="country" class="form-control custom-select d-block w-100" placeholder="country" required runat="server"/>

                                        </div>

                                        <div class="col-lg-4 col-md-6 mb-4">

                                            <label for="state">State</label>
                                            <input type="text" id="state" class="form-control custom-select d-block w-100" placeholder="state" required runat="server"/>



                                        </div>

                                        <div class="col-lg-4 col-md-6 mb-4">

                                            <label for="zip">Zip</label>
                                            <input type="text" class="form-control" id="zip" placeholder="" required runat="server"/>
                                            <div class="invalid-feedback">
                                                Zip code required.
                                            </div>

                                        </div>


                                    </div>


                                    <hr />



                                    <div class="d-block my-3">
                                        <asp:RadioButtonList ID="mode" runat="server"></asp:RadioButtonList>
                                        
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6 mb-3">
                                            <label for="cc-name">Name on card</label>
                                            <input type="text" class="form-control" id="Text1" placeholder="" required runat="server"/>
                                            <small class="text-muted">Full name as displayed on card</small>
                                            <div class="invalid-feedback">
                                                Name on card is required
                                            </div>
                                        </div>
                                        <div class="col-md-6 mb-3">
                                            <label for="cc-number">Card number</label>
                                            <asp:TextBox TextMode="Number" class="form-control" ID="card"  runat="server"/>
                                            <asp:CustomValidator class="h6 text-danger" ID="CustomValidator1" runat="server" ErrorMessage="Enter a correct 12 digit number" ControlToValidate="card" OnServerValidate="CustomValidator1_ServerValidate"></asp:CustomValidator>

                                            <div class="invalid-feedback">
                                                Card number is required
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3 mb-3">
                                            <label for="cc-expiration">Expiration</label>
                                            <input type="date" class="form-control" id="Text3" placeholder="" required runat="server"/>
                                            <div class="invalid-feedback">
                                                Expiration date required
                                            </div>
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <label for="cc-expiration">CVV</label>
                                            <asp:TextBox   TextMode="number" class="form-control"  ID="cvv"  runat="server"/>
                                            <asp:CustomValidator class="h6 text-danger" ID="CustomValidator2" runat="server" ErrorMessage="Enter a correct 3 digit number" ControlToValidate="cvv" OnServerValidate="CustomValidator2_ServerValidate"></asp:CustomValidator>

                                            <div class="invalid-feedback">
                                                Security code required
                                            </div>
                                        </div>
                                    </div>
                                    <hr class="mb-3" />
                                    <div class="text-center">
                                    <asp:Button runat="server" OnClick="orderNow" class="btn   btn-primary btn-lg btn-block"  Text="Order Now" /></div>
                                    <br />

                                    <div class="text-center">
                                    <asp:Button runat="server" OnClick="cancel" class="btn   btn-primary btn-lg btn-block"  Text="Cancel" /></div>
                                </div>

                            </div>


                        </div>




                    </div>


                </div>
            </main>
        </div>
    </form>
</body>
</html>
