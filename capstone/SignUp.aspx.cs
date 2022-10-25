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
    public partial class SignUp : System.Web.UI.Page
    {
        bool valid = false;
        protected void Page_Load(object sender, EventArgs e)
        {

            

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

        public byte[] GenerateSalt()
        {
            var bytes = new byte[64];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return (bytes);
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (valid)
                {
                    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ecomConnectionString"].ToString();
                    EcomDataClassesDataContext db = new EcomDataClassesDataContext();
                    user1 newUser = new user1();
                    password newPassword = new password();
                    newUser.userName = username.Text;

                    var newSalt = GenerateSalt();
                    var newHash = GenerateSaltedHash(Encoding.UTF8.GetBytes(password.Text), newSalt);

                    newPassword.passwordHash = newHash;
                    newPassword.salt = newSalt;

                    newUser.firstName = firstName.Text;
                    newUser.lastName = lastName.Text;
                    if(phone.Text != "")
                        newUser.phone = Convert.ToDecimal(phone.Text);
                    

                    db.passwords.InsertOnSubmit(newPassword);
                    db.SubmitChanges();

                    newUser.passwordID = newPassword.id;

                    db.user1s.InsertOnSubmit(newUser);
                    db.SubmitChanges();

                    Label8.Text = "Succesfully Created";

                    Response.AddHeader("REFRESH", "1;URL=loginPage.aspx");

                }
            }
            catch(Exception ex)
            {
               Label8.Text = ex.Message;
            }

        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string inputData = args.Value;
            args.IsValid = false;
            bool exists = false;
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ecomConnectionString"].ToString();
            EcomDataClassesDataContext db = new EcomDataClassesDataContext();
            string name = username.Text;
            user1 u = db.user1s.FirstOrDefault(x => x.userName.Equals(username.Text));
            if (u != null)
                exists = true;
            else
                exists = false;
            args.IsValid = !exists;
            valid = args.IsValid;
        }

        protected void username_TextChanged(object sender, EventArgs e)
        {

        }

        protected void CustomValidator1_ServerValidate1(object source, ServerValidateEventArgs args)
        {
            string inputData = args.Value;
            args.IsValid = false;

            try
            {
                Decimal num = Convert.ToDecimal(inputData);
                args.IsValid = true;

            }
            catch
            {
                args.IsValid = false;
            }

            if(inputData.Length != 10)
            {
                args.IsValid = false;

            }

            valid = args.IsValid;

        }
    }
}