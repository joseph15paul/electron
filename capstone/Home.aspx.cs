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
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            EcomDataClassesDataContext db = new EcomDataClassesDataContext();

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ecomConnectionString"].ToString();
            SqlConnection con = new SqlConnection("Data Source=INL380;Initial Catalog=ecom;Integrated security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Category", con);
            var data = cmd.ExecuteReader();
            if (Session["userID"] != null)
            {
                user1 u = db.user1s.Single(x => x.userID == Convert.ToInt32(Session["userID"]));
                if (u.profilePicture != null)
                    profilePic.Src = getImg(u.profilePicture.ToArray());
            }
            if (true)
            {

                ListView1.DataSource = data;
                ListView1.DataBind();
                data.Close();

                SqlCommand cmd10 = new SqlCommand("select* from Category", con);

                var cats = cmd10.ExecuteReader();
                categories.DataSource = cats;
                categories.DataBind();

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

        protected void goToProducts(object sender, EventArgs e)
        {
            LinkButton ClickedButton = (LinkButton)sender;

            int id = Convert.ToInt32(ClickedButton.CommandArgument);
            Session["CategoryHomeID"] = id;
            

            Response.Redirect("Products.aspx");
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