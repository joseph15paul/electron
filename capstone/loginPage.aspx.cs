using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;

namespace capstone
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            EcomDataClassesDataContext db = new EcomDataClassesDataContext();
            if (Session["userID"] != null)
            {
                shoppingSession ss = db.shoppingSessions.FirstOrDefault(x => x.userID == Convert.ToInt32(Session["userID"]));
                db.shoppingSessions.DeleteOnSubmit(ss);
                db.SubmitChanges();
            }
            Session.RemoveAll();
        }

        static byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
        {
            HashAlgorithm algorithm = new SHA512Managed();

            byte[] plainTextWithSaltBytes =
              new byte[plainText.Length + salt.Length];

            for (int i = 0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainText[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            }

            return algorithm.ComputeHash(plainTextWithSaltBytes);
        }

        public static bool CompareByteArrays(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
            {
                return false;
            }

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }

            return true;
        }

        protected void signIn(object sender, EventArgs e)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ecomConnectionString"].ToString();
            EcomDataClassesDataContext db = new EcomDataClassesDataContext();
            string name = username.Text;
            user1 u = db.user1s.FirstOrDefault(x => x.userName.Equals(username.Text));
            password userPassword = db.passwords.FirstOrDefault(p => p.id.Equals(u.passwordID));
            shoppingSession ss = new shoppingSession();
            
            if (u != null)
                if (CompareByteArrays((userPassword.passwordHash).ToArray(), GenerateSaltedHash(Encoding.UTF8.GetBytes(password.Text), (userPassword.salt).ToArray())))
                {
                    msg.Text = ("Signing in");
                    Session.RemoveAll();
                    Session["userName"] = username.Text;
                    Session["userID"] = u.userID;
                    ss.userID = u.userID;
                    ss.cost = 0;

                    db.shoppingSessions.InsertOnSubmit(ss);
                    db.SubmitChanges();


                    Response.AddHeader("REFRESH", "0;URL=Home.aspx");

                }
                else
                    msg.Text = "incorrect password";
            else
                msg.Text = ("no such user found");


        }


        
    }
}