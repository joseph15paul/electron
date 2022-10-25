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
    public partial class adminViewOffer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=INL380;Initial Catalog=ecom;Integrated security=True");
            con.Open();
            
            SqlCommand ViewCmd = new SqlCommand("select * from offer", con);
            SqlDataAdapter adapter = new SqlDataAdapter();

            adapter.SelectCommand = ViewCmd;

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            offerss.DataSource = dt;
            offerss.DataBind();
        }
    }
}