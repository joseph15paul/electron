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
    public partial class adminRemoveUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void delete(object sender, EventArgs e)
        {
            EcomDataClassesDataContext db = new EcomDataClassesDataContext();

            SqlConnection con = new SqlConnection("Data Source=INL380;Initial Catalog=ecom;Integrated security=True");
            con.Open();

            user1 selectedUser = db.user1s.SingleOrDefault(x => x.userID == Convert.ToInt32(userID.Value));
            db.user1s.DeleteOnSubmit(selectedUser);
            db.SubmitChanges();
            deleteMsg.Text = "Successfully Removed";
            Response.AddHeader("REFRESH", "1;URL=adminRemoveOffer.aspx");

        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=INL380;Initial Catalog=ecom;Integrated security=True");
            con.Open();
            SqlCommand ViewCmd = new SqlCommand("select * from [user] where [user].userID = " + userID.Value, con);
            SqlDataAdapter adapter = new SqlDataAdapter();

            adapter.SelectCommand = ViewCmd;

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            selectedUser.DataSource = dt;
            selectedUser.DataBind();
        }
    }
}