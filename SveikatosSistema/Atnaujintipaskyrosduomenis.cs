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

            bool oldhash = Bcrypt_validator(oldpassword, userid);
            Boolean match = ContainsNumber(newpassword);
            if (oldhash == true)
            {
                if (oldpassword != newpassword)
                {
                    if (newpassword.Length >= 8 && match == true)
                    {
                        if (newpassword == newpasswordrepeated)
                        {
                            string hash = Bcrypt_hash(newpassword);
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
        public bool Bcrypt_validator(string input, string userid)
        {
            string usrn;
            string MySQLConnetionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=usersdb";
            try
            {
                MySqlConnection databaseConnection = new MySqlConnection(MySQLConnetionString);

                databaseConnection.Open();
                MySqlCommand validatelogin = new MySqlCommand("SELECT * FROM registeredusers WHERE (VartotojoNR = @VartotojoNR)", databaseConnection);
                validatelogin.Parameters.AddWithValue("@VartotojoNR", userid);

                MySqlDataReader reader = validatelogin.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        usrn = reader.GetValue(3).ToString();
                        bool verified = BCrypt.Net.BCrypt.Verify(input, usrn);
                        return verified;
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

        public string Bcrypt_hash(string input)
        {
            string pass = BCrypt.Net.BCrypt.HashPassword(input);

            return pass;
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
    }
}
