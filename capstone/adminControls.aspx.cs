using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Drawing;

namespace capstone
{
    public partial class adminControls : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] != null)
            {
                if (!IsPostBack)
                {
                    SqlConnection con = new SqlConnection("Data Source=INL380;Initial Catalog=ecom;Integrated security=True");
                    con.Open();

                    //Product Entry

                    SqlCommand cmd2 = new SqlCommand("select id,name from Category", con);
                    var cats = cmd2.ExecuteReader();
                    categori.DataTextField = "name";
                    categori.DataValueField = "id";
                    categori.DataSource = cats;
                    categori.DataBind();
                    categori.Items[0].Selected = true;
                    cats.Close();
                    SqlCommand cmd = new SqlCommand("select id,name from offer", con);
                    var offrs = cmd.ExecuteReader();

                    offers.DataTextField = "name";
                    offers.DataValueField = "id";
                    offers.DataSource = offrs;
                    offers.DataBind();
                    offrs.Close();



                }
            }
            else
                Response.AddHeader("REFRESH", "0;URL=adminLogin.aspx");
        }

        protected void submit(object sender, EventArgs e)
        {

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ecomConnectionString"].ToString();
            EcomDataClassesDataContext db = new EcomDataClassesDataContext();

            Product newProduct = new Product();
            newProduct.name = name.Value;
            newProduct.categoryId = Convert.ToInt32( categori.SelectedValue);
            newProduct.quantity = Convert.ToInt32(quantity.Value);
            newProduct.price = Convert.ToInt32(price.Value);
            newProduct.description = description.Value;
            newProduct.offerID = Convert.ToInt32(offers.SelectedValue);
            db.Products.InsertOnSubmit(newProduct);
            db.SubmitChanges();


            if (FileUpload1.HasFile)
            {
                try
                {


                    //saving the file
                    string path = "C:\\Users\\jpaul\\Pictures\\" + FileUpload1.FileName;
                    FileUpload1.SaveAs(path);

                    //Showing the file information

                   
                    SqlConnection con = new SqlConnection("Data Source=INL380;Initial Catalog=ecom;Integrated security=True");
                    con.Open();



                    SqlCommand cmd = new SqlCommand("update Product  set picture = (SELECT * FROM OPENROWSET(BULK N'" + "C:\\Users\\jpaul\\Pictures\\" + FileUpload1.FileName + "', SINGLE_BLOB) as T1) where id = " + newProduct.id, con);
                    var data = cmd.ExecuteReader();


                    File.Delete(@"C:\\Users\\jpaul\\Pictures\\" + FileUpload1.FileName);
                }
                catch (Exception ex)
                {
                    lblmessage.Text = ex.Message;
                }
            }
            else
            {
                lblmessage.Text = "no file selected";
            }
            msg.Text = "Successfully added";


           
        }


        

       

       

        
    }
}