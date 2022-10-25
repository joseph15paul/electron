using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace capstone
{
    public partial class adminViewProducts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=INL380;Initial Catalog=ecom;Integrated security=True");
            con.Open();
            //View Products
            SqlCommand ViewCmd = new SqlCommand("select * from Product", con);
            SqlDataAdapter adapter = new SqlDataAdapter();

            adapter.SelectCommand = ViewCmd;

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            Products.DataSource = dt;
            Products.DataBind();
        }

        
    }
}