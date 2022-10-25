using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace capstone
{
    public partial class adminDeleteProd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void delete(object sender, EventArgs e)
        {
            EcomDataClassesDataContext db = new EcomDataClassesDataContext();

            SqlConnection con = new SqlConnection("Data Source=INL380;Initial Catalog=ecom;Integrated security=True");
            con.Open();

            Product selectedProduct = db.Products.SingleOrDefault(x => x.id == Convert.ToInt32(prodID.Value));
            db.Products.DeleteOnSubmit(selectedProduct);
            db.SubmitChanges();

            deleteMsg.Text = "Successfully Removed";
            Response.AddHeader("REFRESH", "1;URL=adminDeleteProd.aspx");
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=INL380;Initial Catalog=ecom;Integrated security=True");
            con.Open();
            SqlCommand ViewCmd = new SqlCommand("select * from Product where Product.id = " + prodID.Value, con);
            SqlDataAdapter adapter = new SqlDataAdapter();

            adapter.SelectCommand = ViewCmd;

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            selectedProd.DataSource = dt;
            selectedProd.DataBind();
        }
    }
}