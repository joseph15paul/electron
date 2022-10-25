using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace capstone
{
    public partial class adminSalesReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void submit(object sender, EventArgs e)
        {

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ecomConnectionString"].ToString();
            EcomDataClassesDataContext db = new EcomDataClassesDataContext();
            long  totalIncome = 0;
            SqlConnection con = new SqlConnection("Data Source=INL380;Initial Catalog=ecom;Integrated security=True");
            con.Open();
            string st = Convert.ToDateTime(start.Value).ToString("yyyy/MM/dd");
            string en = Convert.ToDateTime(end.Value).ToString("yyyy/MM/dd");
            SqlCommand ViewCmd = new SqlCommand("select * from  [order] where  creation between '"+ st+ "' and '" + en + "' ", con);
            SqlDataAdapter adapter = new SqlDataAdapter();

            
            adapter.SelectCommand = ViewCmd;

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            orders.DataSource = dt;
            orders.DataBind();
            con.Close();
            con.Open();
            SqlDataReader re = ViewCmd.ExecuteReader();

            while (re.Read())
            {
                totalIncome += Convert.ToInt64(re["total"]);
            }

            msg.Text = "Total Income During the Period: " + totalIncome.ToString();
            

        }
    }
}