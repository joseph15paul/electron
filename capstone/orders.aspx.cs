using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace capstone
{
    public partial class orders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userID"] != null)
            {
                EcomDataClassesDataContext db = new EcomDataClassesDataContext();
                if (Session["userID"] != null)
                {
                    user1 u = db.user1s.Single(x => x.userID == Convert.ToInt32(Session["userID"]));
                    if (u.profilePicture != null)
                        profilePic.Src = getImg(u.profilePicture.ToArray());
                }
                SqlConnection con = new SqlConnection("Data Source=INL380;Initial Catalog=ecom;Integrated security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand("select Product.name, OrderItems.quantity,OrderItems.cost from [order],OrderItems,Product where [order].userID = " + Session["userID"].ToString() + " and OrderItems.orderID = [order].id and OrderItems.productID = Product.id", con);
                var data = cmd.ExecuteReader();

                ordersHistory.DataSource = data;
                ordersHistory.DataBind();

                data.Close();

                SqlCommand cmd10 = new SqlCommand("select* from Category", con);

                
                var cats = cmd10.ExecuteReader();
                categories.DataSource = cats;
                categories.DataBind();
            }
            else
                Response.Redirect("loginPage.aspx");
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