using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace capstone
{
    public partial class checkout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                EcomDataClassesDataContext db = new EcomDataClassesDataContext();
                user1 u = db.user1s.Single(x => x.userID == Convert.ToInt32(Session["userID"]));
                if (u.addressID != null)
                {
                    address.Value = u.address.address_line1;
                    address2.Value = u.address.address_line2;
                    zip.Value = u.address.pincode.ToString();
                    state.Value = u.address.state;
                    country.Value = u.address.country;
                }
                SqlConnection con = new SqlConnection("Data Source=INL380;Initial Catalog=ecom;Integrated security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from mode", con);
                var modes = cmd.ExecuteReader();

                mode.DataTextField = "modeOfPay";
                mode.DataValueField = "id";
                mode.DataSource = modes;
                mode.DataBind();
                modes.Close();

                mode.Items[0].Selected = true;


            }

        }

        protected void orderNow(object sender, EventArgs e)
        {
            EcomDataClassesDataContext db = new EcomDataClassesDataContext();
            SqlConnection con = new SqlConnection("Data Source=INL380;Initial Catalog=ecom;Integrated security=True");
            con.Open();
            payment newPayment = db.payments.SingleOrDefault(x => x.id == Convert.ToInt32(Session["payID"]));
            newPayment.amount = Convert.ToDecimal(Session["amount"]);
            newPayment.modeID = Convert.ToInt32(mode.SelectedValue);
            newPayment.statusID = 2;
            db.SubmitChanges();
            order o = db.orders.SingleOrDefault(x => x.id == Convert.ToInt32(Session["orderID"]));
            o.total = newPayment.amount;

            SqlCommand cmd3 = new SqlCommand("select * from orderItems where orderID = " + Session["orderID"].ToString(), con);

            SqlDataReader reader = cmd3.ExecuteReader();

            while (reader.Read())
            {
                
                Product p = db.Products.SingleOrDefault(x => x.id == Convert.ToInt32(reader["productID"]));
                p.quantity -= 1;
                
                
                db.SubmitChanges();

            }
            reader.Close();
            SqlCommand cmd4 = new SqlCommand("delete from cart where userID = " + Session["userID"].ToString(), con);
            cmd4.ExecuteNonQuery();

            db.SubmitChanges();
            Response.Redirect("Home.aspx");
        }

        protected void cancel(object sender, EventArgs e)
        {
            EcomDataClassesDataContext db = new EcomDataClassesDataContext();

            payment newPayment = db.payments.SingleOrDefault(x => x.id == Convert.ToInt32(Session["payID"]));
            newPayment.amount = Convert.ToDecimal(Session["amount"]);
            newPayment.modeID = Convert.ToInt32(mode.SelectedValue);
            newPayment.statusID = 3;
            order o = db.orders.SingleOrDefault(x => x.id == Convert.ToInt32(Session["orderID"]));
            db.orders.DeleteOnSubmit(o);

            SqlConnection con = new SqlConnection("Data Source=INL380;Initial Catalog=ecom;Integrated security=True");
            con.Open();

            SqlCommand cmd4 = new SqlCommand("delete from orderItems where orderID = " + Session["orderID"].ToString(), con);
            cmd4.ExecuteNonQuery();

            SqlCommand cmd5 = new SqlCommand("delete from [order] where id = " + Session["orderID"].ToString(), con);
            cmd5.ExecuteNonQuery();

            db.SubmitChanges();
            Response.AddHeader("REFRESH", "0;URL=Home.aspx");

        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (card.Text.Length != 12)
                args.IsValid = false;
            else
                args.IsValid = true;
        }

        protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (cvv.Text.Length != 3)
                args.IsValid = false;
            else
                args.IsValid = true;

        }
    }
}