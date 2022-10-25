using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace capstone
{
    public partial class adminAddOffer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void submit(object sender, EventArgs e)
        {

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ecomConnectionString"].ToString();
            EcomDataClassesDataContext db = new EcomDataClassesDataContext();

            offer newOffer = new offer();
            newOffer.name = name.Value;
            newOffer.status = true;

            newOffer.discountPercent = Convert.ToInt32(Discount.Value);
            newOffer.description = description.Value;
            newOffer.expiry = Convert.ToDateTime(expiry.Value);
            db.offers.InsertOnSubmit(newOffer);
            db.SubmitChanges();
            msg.Text = "Successfully Added";
            

        }
    }
}