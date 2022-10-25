using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace capstone
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
 

   
    public class WebService1 : System.Web.Services.WebService
    {

        [System.Web.Services.WebMethod()] 

        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> GetListofProducts(string prefixText)

        {

            using (SqlConnection sqlconn = new SqlConnection("Data Source=INL380;Initial Catalog=ecom;Integrated security=True"))

            {

                sqlconn.Open();

                SqlCommand cmd = new SqlCommand("select name from Product where name like '%'+@Name+'%'", sqlconn);

                cmd.Parameters.AddWithValue("@Name", prefixText);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                da.Fill(dt);

                List<string> productNames = new List<string>();

                for (int i = 0; i < dt.Rows.Count; i++)

                {

                    productNames.Add(dt.Rows[i]["name"].ToString());

                }

                return productNames;

            }

        }

    }
}
