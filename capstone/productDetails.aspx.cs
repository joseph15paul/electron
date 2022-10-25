using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace capstone
{
    public partial class productDetails : System.Web.UI.Page
    {
        bool added = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ecomConnectionString"].ToString();
                EcomDataClassesDataContext db = new EcomDataClassesDataContext();
                //Product p = db.Products.FirstOrDefault(x => (x.id.Equals(Session["productID"])));

                Product p = db.Products.FirstOrDefault(x => (x.id.Equals(Convert.ToInt32(Session["prod123ID"]))));

                if (Session["userID"] != null)
                {
                    user1 u = db.user1s.Single(x => x.userID == Convert.ToInt32(Session["userID"]));
                    if (u.profilePicture != null)
                        profilePic.Src = getImg(u.profilePicture.ToArray());
                }
                byte[] imageBytes = p.picture.ToArray();

                ProductImage.ImageUrl = ("data:image/png;base64," + Convert.ToBase64String(imageBytes));
                nameLabel.Text = p.name;
                priceLabel.Text = p.price.ToString();
                crossedPrice.Text = p.price.ToString();
                offerPrice.Text = (Convert.ToInt32(p.price) - (Convert.ToInt32(p.price) * Convert.ToInt32(p.offer.discountPercent) / 100)).ToString();
                description.Text = p.description;
                qty.Text = p.quantity.ToString();
                if (p.offerID == 2)
                {
                    crossedPrice.Visible = false;
                    offerPrice.Visible = false;
                    Label2.Visible = false;
                    Label3.Visible = false;

                }
                else
                {
                    Label1.Visible = false;

                    priceLabel.Visible = false;
                }



                SqlConnection con = new SqlConnection("Data Source=INL380;Initial Catalog=ecom;Integrated security=True");
                con.Open();


                SqlCommand cmd = new SqlCommand("select top 6  * from Product where rand() <= .5 and categoryId = " + p.categoryId.ToString(), con);
                //SqlCommand cmd = new SqlCommand("select top 6 * from Product where categoryId = 1", con);
                SqlCommand cmd2 = new SqlCommand("select* from Category", con);

                var data = cmd.ExecuteReader();


                if (true)
                {

                    ListView1.DataSource = data;
                    ListView1.DataBind();
                    data.Close();


                    var cats = cmd2.ExecuteReader();
                    categories.DataSource = cats;
                    categories.DataBind();


                }
                con.Close();


            }
        }

        protected void categorySelect(object sender, EventArgs e)
        {
            LinkButton ClickedButton = (LinkButton)sender;

            string categoryName = (ClickedButton.CommandArgument).ToString();
            searchCategory.Text = categoryName;

        }
        protected bool isAdded(int id)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ecomConnectionString"].ToString();
            EcomDataClassesDataContext db = new EcomDataClassesDataContext();



            if (db.carts.Any(x => (x.productID.Equals(id) && x.userID.Equals(Convert.ToInt32(Session["userID"])))))
                added = true;
            else
                added = false;

            return added;

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            //add to cart button
            if (Session["userID"] != null)
            {
                try
                {
                    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ecomConnectionString"].ToString();
                    EcomDataClassesDataContext db = new EcomDataClassesDataContext();
                    int userID = Convert.ToInt32(Session["userID"]);
                    int productID = Convert.ToInt32(Session["prod123ID"]);

                    if (!isAdded(productID))
                    {


                        cart newItem = new cart();

                        //change this
                        Product selectedProduct = db.Products.FirstOrDefault(x => x.id.Equals(productID));

                        newItem.productID = selectedProduct.id;
                        newItem.quantity = 1;
                        try
                        {
                            later l = db.laters.First(x => x.productID.Equals(selectedProduct.id) && x.userID.Equals(Convert.ToInt32(Session["userID"])));
                            db.laters.DeleteOnSubmit(l);
                            db.SubmitChanges();
                        }
                        catch
                        {


                            //change this
                            newItem.userID = Convert.ToInt32(Session["userID"]);

                            db.carts.InsertOnSubmit(newItem);
                            db.SubmitChanges();

                            AddToCart.Text = "Added to Cart";


                        }
                    }
                    else
                    {
                        cart cartItem = db.carts.FirstOrDefault(x => x.productID.Equals(Convert.ToInt32(Session["prod123ID"])) && x.userID.Equals(Convert.ToInt32(Session["userID"])));
                        AddToCart.Text = "Added to Cart";
                        cartItem.quantity += 1;
                        db.SubmitChanges();

                    }
                }
                catch (Exception ex)
                {
                    Label8.Text = ex.Message;
                }

            }
            else
            {
                Response.Redirect("loginPage.aspx");
            }
        }

        protected void goToDetails(object sender, EventArgs e)
        {
            LinkButton ClickedButton = (LinkButton)sender;

            int id = Convert.ToInt32(ClickedButton.CommandArgument);
            Session["prod123ID"] = id;

            Response.Redirect("productDetails.aspx");
        }



        public string getImg(Object byt)
        {
            try
            {
                byte[] imageBytes = (byte[])byt;

                return ("data:image/png;base64," + Convert.ToBase64String(imageBytes));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "error";
            }

        }

        protected void BuyNow_Click(object sender, EventArgs e)
        {
            if (Session["userID"] != null)
            {
                try
                {
                    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ecomConnectionString"].ToString();
                    EcomDataClassesDataContext db = new EcomDataClassesDataContext();
                    int userID = Convert.ToInt32(Session["userID"]);
                    int productID = Convert.ToInt32(Session["prod123ID"]);

                    if (!isAdded(productID))
                    {


                        cart newItem = new cart();

                        //change this
                        Product selectedProduct = db.Products.FirstOrDefault(x => x.id.Equals(productID));

                        newItem.productID = selectedProduct.id;
                        newItem.quantity = 1;
                        try
                        {
                            later l = db.laters.First(x => x.productID.Equals(selectedProduct.id) && x.userID.Equals(Convert.ToInt32(Session["userID"])));
                            db.laters.DeleteOnSubmit(l);
                            db.SubmitChanges();
                        }
                        catch
                        {


                            //change this
                            newItem.userID = Convert.ToInt32(Session["userID"]);

                            db.carts.InsertOnSubmit(newItem);
                            db.SubmitChanges();

                            AddToCart.Text = "Added to Cart";

                            Response.Redirect("Cart.aspx");
                        }
                    }
                    else
                    {
                        cart cartItem = db.carts.FirstOrDefault(x => x.productID.Equals(Convert.ToInt32(Session["prod123ID"])) && x.userID.Equals(Convert.ToInt32(Session["userID"])));
                        AddToCart.Text = "Added to Cart";
                        cartItem.quantity += 1;
                        db.SubmitChanges();
                        Response.Redirect("Cart.aspx");
                    }
                }
                catch (Exception ex)
                {
                    Label8.Text = ex.Message;
                }

            }
            else
            {
                Response.Redirect("loginPage.aspx");
            }
            
        }

        protected void Search(object sender, EventArgs e)
        {
            string inp = SearchBox.Text;
            Session["prefix"] = inp;
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ecomConnectionString"].ToString();
            EcomDataClassesDataContext db = new EcomDataClassesDataContext();

            SqlConnection con = new SqlConnection("Data Source=INL380;Initial Catalog=ecom;Integrated security=True");
            con.Open();

            Category category = new Category();

            if (searchCategory.Text != "All")
            {
                category = db.Categories.SingleOrDefault(x => x.name == searchCategory.Text);
                //product = db.Products.FirstOrDefault(x => x.name == inp && x.categoryId == category.id);


                Session["searchedCategoryID"] = category.id;

            }
            else
            {
                Session["searchedCategoryID"] = -1;

            }

            Response.AddHeader("REFRESH", "0;URL=searchResult.aspx");
        }
    }
}