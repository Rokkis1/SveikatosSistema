using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SveikatosSistema
{
    public partial class Atnaujintipaskyrosduomenis : Form
    {
        string userid;
        public Atnaujintipaskyrosduomenis()
        {
            InitializeComponent();
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string oldpassword, newpassword, newpasswordrepeated;

            oldpassword = maskedtxtboxCurrentpw.Text;
            newpassword = maskedTextBoxNewpw.Text;
            newpasswordrepeated = maskedTextBoxNewrepeatedpw.Text;

            string oldhash = CreateMD5Hash(oldpassword);
            Boolean match = ContainsNumber(newpassword);
            Boolean pass = false;
            pass = Userlogin(oldhash);

            if (pass == true)
            {
                if (oldpassword != newpassword)
                {
                    if (newpassword.Length >= 8 && match == true)
                    {
                        if (newpassword == newpasswordrepeated)
                        {
                            string hash = CreateMD5Hash(newpassword);
                            update(userid, hash);
                            maskedTextBoxNewpw.Clear();
                            maskedTextBoxNewrepeatedpw.Clear();
                            maskedtxtboxCurrentpw.Clear();
                            MessageBox.Show("Atnaujintas slaptažodis");

                        }
                        else
                        {
                            MessageBox.Show("Slaptažodžiai nesutampa");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Slaptažodį turi sudaryti bent 8 symboliai iš kurių 1 privalo būti skaičius.");
                    }
                }
                else
                {
                    MessageBox.Show("Naujas slaptažodis negali būti toks pat kaip ir senas");
                }
            }
            else
            {
                MessageBox.Show("Blogas senas slaptažodis");
            }
        }
        public void update(string USERID, string password)
        {

            string MySQLConnetionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=usersdb";
            try
            {
                MySqlConnection databaseConnection = new MySqlConnection(MySQLConnetionString);

                databaseConnection.Open();
                MySqlCommand cmd = new MySqlCommand(@"UPDATE `registeredusers` SET `Slaptažodis` = @Slaptažodis WHERE `VartotojoNR` = @VartotojoNR", databaseConnection);
                cmd.Parameters.AddWithValue("Slaptažodis", password);
                cmd.Parameters.AddWithValue("VartotojoNR", USERID);

                cmd.ExecuteNonQuery();
                databaseConnection.Close();
            }
            catch
            {

                MessageBox.Show("Nepavyko užmegzti ryšio su duomenų baze, bandykite dar kartą");
            }

        }
        public string CreateMD5Hash(string input)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
        public bool ContainsNumber(string password)
        {
            char[] numbers = "1234567890".ToCharArray();
            foreach (var specialChar in numbers.Where(password.Contains))
            {
                return true;
            }
            return false;
        }
        public void userID(string identification)
        {
            userid = identification;
        }
        public bool Userlogin(string password)
        {
            string MySQLConnetionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=usersdb";
            try
            {
                MySqlConnection databaseConnection = new MySqlConnection(MySQLConnetionString);

                databaseConnection.Open();
                MySqlCommand validatelogin = new MySqlCommand("SELECT * FROM registeredusers WHERE (VartotojoNR = @VartotojoNR) AND (Slaptažodis = @Slaptažodis)", databaseConnection);
                validatelogin.Parameters.AddWithValue("@VartotojoNR", userid);
                validatelogin.Parameters.AddWithValue("@Slaptažodis", password);
                MySqlDataReader reader = validatelogin.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        databaseConnection.Close();
                        return true;
                    }
                    else
                    {
                        databaseConnection.Close();
                        return false;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Nepavyko užmegzti ryšio su duomenų baze, bandykite dar kartą");
            }
            return false;
        }
    }
}
