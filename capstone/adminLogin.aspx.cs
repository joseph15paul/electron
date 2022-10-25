using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace capstone
{
    public partial class adminLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["admin"] = null;
        }

        protected void signIn(object sender, EventArgs e)
        {
            EcomDataClassesDataContext db = new EcomDataClassesDataContext();
            admin u = db.admins.SingleOrDefault(x => x.userName == username.Value);




            if (u != null)
            {
                password userPassword = db.passwords.FirstOrDefault(p => p.id.Equals(u.passwordID));
                if (CompareByteArrays((userPassword.passwordHash).ToArray(), GenerateSaltedHash(Encoding.UTF8.GetBytes(password.Value), (userPassword.salt).ToArray())))
                {
                    msg.Text = ("Signing in");
                    Session.RemoveAll();
                    Session["userID"] = username.Value;
                    Session["admin"] = "In";

                    Response.AddHeader("REFRESH", "1;URL=adminControls.aspx");
                }
                else
                    msg.Text = ("incorrect password");
            }
            else
                msg.Text = ("no such user found");


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
    }
}