using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Button = System.Web.UI.WebControls.Button;
using ListViewItem = System.Web.UI.WebControls.ListViewItem;
using TextBox = System.Web.UI.WebControls.TextBox;

namespace capstone
{
    public partial class Cart : System.Web.UI.Page
    {
        bool reload = false;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userID"] != null)
            {
                if (!IsPostBack || reload)
                {
                    try
                    {
                        EcomDataClassesDataContext db = new EcomDataClassesDataContext();

                        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ecomConnectionString"].ToString();
                        SqlConnection con = new SqlConnection("Data Source=INL380;Initial Catalog=ecom;Integrated security=True");
                        con.Open();


                        SqlCommand cmd3 = new SqlCommand("select offer.name as offerName,offer.discountPercent as disc, offer.status as oStatus, offer.expiry as expiry, Product.offerID as offer, Product.description as description, Product.id as prodID, Product.picture as pic, Product.name as name, cart.quantity as quantity, Product.price as price from Product, cart, offer where cart.userID = "+ Session["userID"].ToString() + " and Product.id = cart.productID and Product.offerID = offer.id ", con);

                        SqlDataReader reader = cmd3.ExecuteReader();
                        int itemsCost = 0;
                        int totalCost = 0;
                        while (reader.Read())
                        {
                            if (Convert.ToBoolean(reader["oStatus"]))
                                itemsCost = (Convert.ToInt32(reader["price"]) - (Convert.ToInt32(reader["price"]) * Convert.ToInt32(reader["disc"]) / 100)) * Convert.ToInt32(reader["quantity"]);
                            else
                                itemsCost = Convert.ToInt32(reader["price"]) * Convert.ToInt32(reader["quantity"]);
                            totalCost += itemsCost;
                        }
                        total.Text = totalCost.ToString();
                        productCost.Text = totalCost.ToString();
                        reader.Close();

                        if (Session["userID"] != null)
                        {
                            user1 u = db.user1s.Single(x => x.userID == Convert.ToInt32(Session["userID"]));
                            if (u.profilePicture != null)
                                profilePic.Src = getImg(u.profilePicture.ToArray());
                        }


                        SqlCommand cmd = new SqlCommand("select offer.name as offerName,offer.discountPercent as disc, offer.status as oStatus, offer.expiry as expiry, Product.offerID as offer, Product.description as description, Product.id as prodID, Product.picture as pic, Product.name as name, cart.quantity as quantity, Product.price as price from Product, cart, offer where cart.userID = "+ Session["userID"].ToString() + " and Product.id = cart.productID and Product.offerID = offer.id ", con);
                        var data = cmd.ExecuteReader();
                        if (true)
                        {

                            ListView1.DataSource = data;
                            ListView1.DataBind();
                            data.Close();
                            SqlCommand cmd2 = new SqlCommand("select offer.name as offerName,offer.discountPercent as disc, offer.status as oStatus, offer.expiry as expiry, Product.offerID as offer, Product.description as description, Product.id as prodID, Product.picture as pic, Product.name as name, Product.price as price from Product, later, offer where later.userID = "+ Session["userID"].ToString() + " and Product.id = later.productID and Product.offerID = offer.id ", con);
                            data = cmd2.ExecuteReader();
                            Later.DataSource = data;
                            Later.DataBind();
                            data.Close();
                            SqlCommand cmd10 = new SqlCommand("select* from Category", con);

                            var cats = cmd10.ExecuteReader();
                            categories.DataSource = cats;
                            categories.DataBind();
                            reload = false;

                        }
                        else
                        {
                            //cart cartItem = db.carts.FirstOrDefault(x => x.productID.Equals(2));
                            //Button1.Text = "Added to Cart";
                            //cartItem.quantity += 1;
                            //db.SubmitChanges();

                        }
                    }
                    catch (Exception ex)
                    {
                        //Label8.Text = ex.Message;
                    }
                }
            }
            else
            {
                Response.AddHeader("REFRESH", "0;URL=loginPage.aspx");
            }
        }


       

        protected void ListView1_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)

        {

        }
        protected void later(object sender, EventArgs e)
        {
            LinkButton ClickedButton = (LinkButton)sender;

            int prodID = Convert.ToInt32(ClickedButton.CommandArgument);

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ecomConnectionString"].ToString();
            EcomDataClassesDataContext db = new EcomDataClassesDataContext();

            cart p = db.carts.FirstOrDefault(x => (x.productID.Equals(prodID)) && x.userID.Equals(Convert.ToInt32(Session["userID"])));
            SqlConnection con = new SqlConnection("Data Source=INL380;Initial Catalog=ecom;Integrated security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into later(productID,userID) values("+ p.productID+", "+p.userID + ") ", con);
            cmd.ExecuteReader();

           

            db.carts.DeleteOnSubmit(p);
            

            
            db.SubmitChanges();
            Page.Response.Redirect(Page.Request.Url.ToString(), true);

        }

        protected void cart(object sender, EventArgs e)
        {
            LinkButton ClickedButton = (LinkButton)sender;

            int prodID = Convert.ToInt32(ClickedButton.CommandArgument);

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ecomConnectionString"].ToString();
            EcomDataClassesDataContext db = new EcomDataClassesDataContext();

            later p = db.laters.FirstOrDefault(x => (x.productID.Equals(prodID)) && x.userID.Equals(Convert.ToInt32(Session["userID"])));
            SqlConnection con = new SqlConnection("Data Source=INL380;Initial Catalog=ecom;Integrated security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into cart(productID,userID,quantity) values(" + p.productID + ", " + p.userID + ",1 ) ", con);
            cmd.ExecuteReader();



            db.laters.DeleteOnSubmit(p);



            db.SubmitChanges();
            Page.Response.Redirect(Page.Request.Url.ToString(), true);

        }
        protected void deleteFromCart(object sender, EventArgs e)
        {
            LinkButton ClickedButton = (LinkButton)sender;

            int prodID = Convert.ToInt32(ClickedButton.CommandArgument);

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ecomConnectionString"].ToString();
            EcomDataClassesDataContext db = new EcomDataClassesDataContext();

            cart p = db.carts.FirstOrDefault(x => (x.productID.Equals(prodID)) && x.userID.Equals(Convert.ToInt32(Session["userID"])));
            

            db.carts.DeleteOnSubmit(p);
            db.SubmitChanges();
            Page.Response.Redirect(Page.Request.Url.ToString(), true);

        }


        protected void deleteFromLater(object sender, EventArgs e)
        {
            LinkButton ClickedButton = (LinkButton)sender;

            int prodID = Convert.ToInt32(ClickedButton.CommandArgument);

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ecomConnectionString"].ToString();
            EcomDataClassesDataContext db = new EcomDataClassesDataContext();

            later p = db.laters.FirstOrDefault(x => (x.productID.Equals(prodID)) && x.userID.Equals(Convert.ToInt32(Session["userID"])));


            db.laters.DeleteOnSubmit(p);
            db.SubmitChanges();
            Page.Response.Redirect(Page.Request.Url.ToString(), true);

        }


        public void updateCart(object sender, EventArgs e)
        {
            Label1.Text = "hello";

            LinkButton ClickedButton = (LinkButton)sender;

            int prodID = Convert.ToInt32(ClickedButton.CommandArgument);
            //quantity.value = categoryName;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ecomConnectionString"].ToString();
            EcomDataClassesDataContext db = new EcomDataClassesDataContext();
            //Product p = db.Products.FirstOrDefault(x => (x.id.Equals(Session["productID"])));

            cart p = db.carts.FirstOrDefault(x => (x.productID.Equals(prodID)) && x.userID.Equals(Convert.ToInt32(Session["userID"])));
            LinkButton btn = sender as LinkButton;

            if (btn != null)

            {

                ListViewItem lvi = btn.NamingContainer as ListViewItem;

                if (lvi != null)

                {

                    TextBox lbl1 = lvi.FindControl("quantity") as TextBox;

                    if (lbl1 != null)

                    {

                        Label1.Text = lbl1.Text.ToString();
                        p.quantity = Convert.ToInt32(lbl1.Text.ToString());
                        db.SubmitChanges();
                        Page.Response.Redirect(Page.Request.Url.ToString(), true);

                    }

                }

            }



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

        protected void checkOut_Click(object sender, EventArgs e)
        {
            int totalCost = 0;
            int itemsCost = 0;
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ecomConnectionString"].ToString();
            SqlConnection con = new SqlConnection("Data Source=INL380;Initial Catalog=ecom;Integrated security=True");
            con.Open();

            EcomDataClassesDataContext db = new EcomDataClassesDataContext();

            payment newPayment = new payment();
            newPayment.amount = 0;
            newPayment.modeID = 1;
            newPayment.statusID = 4;
            db.payments.InsertOnSubmit(newPayment);
            db.SubmitChanges();
            order newOrder = new order();
            newOrder.userID = Convert.ToInt32(Session["userID"]);
            newOrder.total = totalCost;
            newOrder.paymentID = newPayment.id;
            newOrder.creation = DateTime.Now;
            db.orders.InsertOnSubmit(newOrder);
            db.SubmitChanges();

            SqlCommand cmd3 = new SqlCommand("select offer.name as offerName,offer.discountPercent as disc, offer.status as oStatus, offer.expiry as expiry, Product.offerID as offer, Product.description as description, Product.id as prodID, Product.picture as pic, Product.name as name, cart.quantity as quantity, Product.price as price from Product, cart, offer where cart.userID = "+ Session["userID"].ToString() + " and Product.id = cart.productID and Product.offerID = offer.id ", con);

            SqlDataReader reader = cmd3.ExecuteReader();
            
            while (reader.Read())
            {
                OrderItem item = new OrderItem();
                if (Convert.ToBoolean(reader["oStatus"]))
                    itemsCost = (Convert.ToInt32(reader["price"]) - (Convert.ToInt32(reader["price"]) * Convert.ToInt32(reader["disc"]) / 100)) * Convert.ToInt32(reader["quantity"]);
                else
                    itemsCost = Convert.ToInt32(reader["price"]) * Convert.ToInt32(reader["quantity"]);
                totalCost += itemsCost;
                item.cost = itemsCost;
                item.quantity = Convert.ToInt32(reader["quantity"]);
                item.orderID = newOrder.id;
                item.productID = Convert.ToInt32(reader["prodID"]);
                db.OrderItems.InsertOnSubmit(item);
                db.SubmitChanges();
                

            }
            total.Text = totalCost.ToString();
            reader.Close();
            Session["payID"] = newPayment.id;
            Session["orderID"] = newOrder.id;
            Session["amount"] = totalCost;
            Response.Redirect("checkout.aspx");
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