using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace capstone
{
    public partial class profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userID"] != null)
            {
                if (!IsPostBack)
                {
                    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ecomConnectionString"].ToString();
                    EcomDataClassesDataContext db = new EcomDataClassesDataContext();
                    SqlConnection con = new SqlConnection("Data Source=INL380;Initial Catalog=ecom;Integrated security=True");
                    con.Open();


                    SqlCommand cmd10 = new SqlCommand("select* from Category", con);

                    var cats = cmd10.ExecuteReader();
                    categories.DataSource = cats;
                    categories.DataBind();
                    user1 u = db.user1s.FirstOrDefault(x => x.userID.Equals(Convert.ToInt32(Session["userID"])));
                    if (Session["userID"] != null)
                    {
                        
                        if (u.profilePicture != null)
                            profilePicc.Src = getImg(u.profilePicture.ToArray());
                    }
                    userName.Text = u.userName;
                    firstName.Text = u.firstName;
                    surname.Text = u.lastName;
                    MobileNumber.Text = u.phone.ToString();
                    //change
                    
                    if (u.addressID != null)
                    {
                        address userAddress = db.addresses.Single(x => x.id.Equals(u.addressID));
                        AddressLine1.Text = userAddress.address_line1;
                        AddressLine2.Text = userAddress.address_line2;
                        State.Text = userAddress.state;
                        Country.Text = userAddress.country;
                        City.Text = userAddress.city;
                        Postcode.Text = userAddress.pincode.ToString();
                    }
                    if (u.profilePicture != null)
                            profilePic.ImageUrl = (getImg(u.profilePicture.ToArray()));
                   
                }
            }
            else
                Response.Redirect("loginPage.aspx");
        }

        protected void save(object sender, EventArgs e)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ecomConnectionString"].ToString();
            EcomDataClassesDataContext db = new EcomDataClassesDataContext();

            user1 u = db.user1s.FirstOrDefault(x => x.userID.Equals(Convert.ToInt32(Session["userID"])));
            address userAddress = new address();
            //change
            if (u.addressID != null)
            {
                userAddress = db.addresses.FirstOrDefault(x => x.id.Equals(u.addressID));

                u.userName = userName.Text;
                u.firstName = firstName.Text;
                u.lastName = surname.Text;
                u.phone = Convert.ToDecimal(MobileNumber.Text);
                userAddress.address_line1 = AddressLine1.Text;
                userAddress.address_line2 = AddressLine2.Text;
                userAddress.state = State.Text;
                userAddress.country = Country.Text;
                userAddress.city = City.Text;
                userAddress.pincode = Convert.ToInt32(Postcode.Text);

                db.SubmitChanges();
            }
            else
            {
                u.userName = userName.Text;
                u.firstName = firstName.Text;
                u.lastName = surname.Text;
                u.phone = Convert.ToDecimal(MobileNumber.Text);
                userAddress.address_line1 = AddressLine1.Text;
                userAddress.address_line2 = AddressLine2.Text;
                userAddress.state = State.Text;
                userAddress.country = Country.Text;
                userAddress.city = City.Text;
                userAddress.pincode = Convert.ToInt32(Postcode.Text);
                db.addresses.InsertOnSubmit(userAddress);
                db.SubmitChanges();
            }

            Page.Response.Redirect(Page.Request.Url.ToString(), true);

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
        protected void updatePassword(object sender, EventArgs e)
        {
            try
            {
                if (true)
                {
                    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ecomConnectionString"].ToString();
                    EcomDataClassesDataContext db = new EcomDataClassesDataContext();
                    
                    //change
                    user1 newUser = db.user1s.SingleOrDefault(x=>x.userID.Equals(Convert.ToInt32(Session["userID"])));
                    password newPassword = new password();
                    password oldPassword = db.passwords.SingleOrDefault(x => x.id.Equals(newUser.passwordID));
                    

                    var newSalt = GenerateSalt();
                    var newHash = GenerateSaltedHash(Encoding.UTF8.GetBytes(confirmPassword.Text), newSalt);

                    newPassword.passwordHash = newHash;
                    newPassword.salt = newSalt;

                   
                    
                    db.passwords.InsertOnSubmit(newPassword);
                    db.SubmitChanges();

                    newUser.passwordID = newPassword.id;
                    db.SubmitChanges();



                    db.passwords.DeleteOnSubmit(oldPassword);
                    db.SubmitChanges();
                    msg.Text = "Succesfully changed";
                    Response.AddHeader("REFRESH", "1;URL=profile.aspx");
                    //Page.Response.Redirect(Page.Request.Url.ToString(), true);

                }
            }
            catch (Exception ex)
            {
                msg.Text = ex.Message;
            }
        }

            public string getImg(Object byt)
        {
            try
            {
                byte[] imageBytes = (byte[])byt;

                return ("data:image/png;base64," + Convert.ToBase64String(imageBytes));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "error";
            }

        }

        protected void uploadImage(object sender, EventArgs e)
        {
            

            if (FileUpload1.HasFile)
            {
                try
                {
                   

                    //saving the file
                    string path = "C:\\Users\\jpaul\\Pictures\\" + FileUpload1.FileName;
                    FileUpload1.SaveAs(path);

                    //Showing the file information
                    
                    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ecomConnectionString"].ToString();
                    SqlConnection con = new SqlConnection("Data Source=INL380;Initial Catalog=ecom;Integrated security=True");
                    con.Open();



                    SqlCommand cmd = new SqlCommand("update [user]  set profilePicture = (SELECT * FROM OPENROWSET(BULK N'" + "C:\\Users\\jpaul\\Pictures\\" + FileUpload1.FileName + "', SINGLE_BLOB) as T1) where userID = "+ Session["userID"].ToString() + " ", con);
                    var data = cmd.ExecuteReader();


                    File.Delete(@"C:\\Users\\jpaul\\Pictures\\" + FileUpload1.FileName);
                    Page.Response.Redirect(Page.Request.Url.ToString(), true);
                }
                catch (Exception ex)
                {
                    lblmessage.Text = ex.Message;
                }
            }
            else
            {
                lblmessage.Text = "no file selected";
            }
        }

        protected void Search(object sender, EventArgs e)
        {
            string inp = SearchBox.Text;
            Session["prefix"] = inp;
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ecomConnectionString"].ToString();
            EcomDataClassesDataContext db = new EcomDataClassesDataContext();

            SqlConnection con = new SqlConnection("Data Source=INL380;Initial Catalog=ecom;Integrated security=True");
            con.Open();

            Category category = new Category();

            if (searchCategory.Text != "All")
            {
                category = db.Categories.SingleOrDefault(x => x.name == searchCategory.Text);
                //product = db.Products.FirstOrDefault(x => x.name == inp && x.categoryId == category.id);


                Session["searchedCategoryID"] = category.id;

            }
            else
            {
                Session["searchedCategoryID"] = -1;

            }

            Response.AddHeader("REFRESH", "0;URL=searchResult.aspx");
        }

        protected void categorySelect(object sender, EventArgs e)
        {
            LinkButton ClickedButton = (LinkButton)sender;

            string categoryName = (ClickedButton.CommandArgument).ToString();
            searchCategory.Text = categoryName;

        }
    }
}