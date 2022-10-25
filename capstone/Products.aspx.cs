using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Button = System.Web.UI.WebControls.Button;
using ListViewItem = System.Web.UI.WebControls.ListViewItem;

namespace capstone
{
    public partial class Products : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                EcomDataClassesDataContext db = new EcomDataClassesDataContext();

                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ecomConnectionString"].ToString();
                SqlConnection con = new SqlConnection("Data Source=INL380;Initial Catalog=ecom;Integrated security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand("select* from Product where categoryId = " + Session["CategoryHomeID"].ToString(), con);
                var data = cmd.ExecuteReader();
                if (Session["userID"] != null)
                {
                    user1 u = db.user1s.Single(x => x.userID == Convert.ToInt32(Session["userID"]));
                    if (u.profilePicture != null)
                        profilePic.Src = getImg(u.profilePicture.ToArray());
                }
                


                
                categories.DataBind();
                if (true)
                {

                    ListView1.DataSource = data;
                    ListView1.DataBind();

                }

                SqlCommand cmd10 = new SqlCommand("select* from Category", con);
                data.Close();
                var cats = cmd10.ExecuteReader();
                categories.DataSource = cats;
                categories.DataBind();
                con.Close();
            }
        }

        bool added = false;

        public bool isAdded(int id)
        {

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ecomConnectionString"].ToString();
            EcomDataClassesDataContext db = new EcomDataClassesDataContext();



            if (db.carts.Any(x => (x.productID.Equals(id) && x.userID.Equals(Convert.ToInt32(Session["userID"])))))
                added = true;
            else
                added = false;

            return added;

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

        protected void goToDetails(object sender, EventArgs e)
        {
            LinkButton ClickedButton = (LinkButton)sender;

            int id = Convert.ToInt32(ClickedButton.CommandArgument);
            Session["prod123ID"] = id;


            Response.AddHeader("REFRESH", "0;URL=productDetails.aspx");
        }




        protected void addToCart(object sender, EventArgs e)
        {
            //add to cart button
            if (Session["userID"] != null)
            {
                try
                {
                    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ecomConnectionString"].ToString();
                    EcomDataClassesDataContext db = new EcomDataClassesDataContext();
                    //Button btn = (Button)sender;
                    //int id = Convert.ToInt32(btn.CommandArgument.ToString());
                    LinkButton ClickedButton = (LinkButton)sender;

                    int id = Convert.ToInt32(ClickedButton.CommandArgument);
                   

                    if (!isAdded(id))
                    {


                        cart newItem = new cart();

                        Product selectedProduct = db.Products.FirstOrDefault(x => x.id.Equals(Convert.ToInt32(id)));

                        newItem.productID = selectedProduct.id;
                        newItem.quantity = 1;

                        //change this
                        newItem.userID = (Convert.ToInt32(Session["userID"]));

                        if (db.laters.Any(x => x.productID.Equals(selectedProduct.id) && x.userID.Equals(Convert.ToInt32(Session["userID"]))))
                        {
                            later l = db.laters.FirstOrDefault(x => x.productID.Equals(selectedProduct.id) && x.userID.Equals(Convert.ToInt32(Session["userID"])));

                            db.laters.DeleteOnSubmit(l);
                            db.SubmitChanges();



                        }

                        db.carts.InsertOnSubmit(newItem);
                        db.SubmitChanges();
                        ClickedButton.Text = "Added To Cart";

                        //Response.AddHeader("REFRESH", "1;URL=cart.aspx");

                    }
                    else
                    {
                        cart cartItem = db.carts.FirstOrDefault(x => x.productID.Equals(Convert.ToInt32(id)) && x.userID.Equals(Convert.ToInt32(Session["userID"])));

                        cartItem.quantity += 1;
                        db.SubmitChanges();
                        ClickedButton.Text = "Added To Cart";


                    }
                }
                catch (Exception ex)
                {
                    //Label8.Text = ex.Message;
                }
            }
            else
            {
                Response.Redirect("loginPage.aspx");
            }

        }

        public string getText(Object id)
        {
            if (isAdded(Convert.ToInt32(id)))
            {
                return "Added To Cart";
            }
            else
                return "Add To Cart";
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

        protected void categorySelect(object sender, EventArgs e)
        {
            LinkButton ClickedButton = (LinkButton)sender;

            string categoryName = (ClickedButton.CommandArgument).ToString();
            searchCategory.Text = categoryName;

        }
    }
}