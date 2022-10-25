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
    public partial class adminRemoveOffer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void delete(object sender, EventArgs e)
        {
            EcomDataClassesDataContext db = new EcomDataClassesDataContext();

            SqlConnection con = new SqlConnection("Data Source=INL380;Initial Catalog=ecom;Integrated security=True");
            con.Open();

            offer selectedOffer = db.offers.SingleOrDefault(x => x.id == Convert.ToInt32(offerID.Value));
            db.offers.DeleteOnSubmit(selectedOffer);
            db.SubmitChanges();
            deleteMsg.Text = "Successfully Removed";
            Response.AddHeader("REFRESH", "1;URL=adminRemoveOffer.aspx");

        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=INL380;Initial Catalog=ecom;Integrated security=True");
            con.Open();
            SqlCommand ViewCmd = new SqlCommand("select * from offer where offer.id = " + offerID.Value, con);
            SqlDataAdapter adapter = new SqlDataAdapter();

            adapter.SelectCommand = ViewCmd;

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            selectedOffer.DataSource = dt;
            selectedOffer.DataBind();
        }
    }
}