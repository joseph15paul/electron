using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace capstone
{
    public partial class adminAddAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void submit(object sender, EventArgs e)
        {

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ecomConnectionString"].ToString();
            EcomDataClassesDataContext db = new EcomDataClassesDataContext();

            admin newAdmin = new admin();
            newAdmin.userName = userName.Value;

            user1 u = db.user1s.SingleOrDefault(x => x.userName == userName.Value);
            newAdmin.passwordID = u.passwordID;

            
            db.admins.InsertOnSubmit(newAdmin);
            db.SubmitChanges();
            msg.Text = "Successfully Added";


        }
    }
}