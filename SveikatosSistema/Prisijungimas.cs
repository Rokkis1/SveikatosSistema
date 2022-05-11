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
using System.Security.Cryptography;

namespace SveikatosSistema
{
    public partial class Prisijungimas : Form
    {
        string username;
        string password;
        public Prisijungimas()
        {
            InitializeComponent();
        }

        private void Prisijungimas_Load(object sender, EventArgs e)
        {
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            username = LoginName.Text;
            password = PasswordName.Text;
            bool hash = Bcrypt_validator(password, username);
            string userid = UserloginID(username);
            string firsttimer = Firsttime(username);
            string isadmin = IsAdmin(username);
            if (hash == true)
            {
                if (isadmin == "1")
                {
                    using (var adminmenu = new AdministratoriausMeniu())
                    {
                        adminmenu.userID(userid);
                        Prisijungimas.ActiveForm.Hide();
                        adminmenu.ShowDialog();

                    }
                }
                else
                {
                    if (firsttimer == "False")
                    {
                        string message = "paaiškinimas kaip tvarkomi duomenys";
                        string caption = "Duomenų apsauga";
                        MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                        DialogResult result;

                        result = MessageBox.Show(message, caption, buttons);
                        if (result == System.Windows.Forms.DialogResult.Yes)
                        {
                            FirsttimeUpdated(username);
                            using (var menu = new Meniu())
                            {
                                menu.userID(userid);
                                Prisijungimas.ActiveForm.Hide();
                                menu.ShowDialog();

                            }
                        }
                        else
                        {
                            System.Windows.Forms.Application.Exit();
                        }
                    }
                    else
                        using (var menu = new Meniu())
                        {
                            menu.userID(userid);
                            Prisijungimas.ActiveForm.Hide();
                            menu.ShowDialog();
                        }
                }


            }
            else
            {
                MessageBox.Show("Blogai įvestas slaptažodis arba slapyvardis");
            }
        }
        public bool Bcrypt_validator(string input, string username)
        {
            string usrn;
            string MySQLConnetionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=usersdb";
            try
            {
                MySqlConnection databaseConnection = new MySqlConnection(MySQLConnetionString);

                databaseConnection.Open();
                MySqlCommand validatelogin = new MySqlCommand("SELECT * FROM registeredusers WHERE (Slapyvardis = @Slapyvardis)", databaseConnection);
                validatelogin.Parameters.AddWithValue("@Slapyvardis", username);

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

        public void FirsttimeUpdated(string username)
        {
            string firsttime = "1";
            string MySQLConnetionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=usersdb";
            try
            {
                MySqlConnection databaseConnection = new MySqlConnection(MySQLConnetionString);

                databaseConnection.Open();
                MySqlCommand cmd = new MySqlCommand(@"UPDATE `registeredusers` SET `Pirmaskartas` = @Pirmaskartas WHERE `Slapyvardis` = @Slapyvardis", databaseConnection);
                cmd.Parameters.AddWithValue("Pirmaskartas", firsttime);
                cmd.Parameters.AddWithValue("Slapyvardis", username);

                cmd.ExecuteNonQuery();
                databaseConnection.Close();
            }
            catch
            {

                MessageBox.Show("Nepavyko užmegzti ryšio su duomenų baze, bandykite dar kartą");
            }
        }
        public string Firsttime(string username)
        {
            string firsttime;
            string MySQLConnetionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=usersdb";
            try
            {
                MySqlConnection databaseConnection = new MySqlConnection(MySQLConnetionString);

                databaseConnection.Open();
                MySqlCommand validatelogin = new MySqlCommand("SELECT Pirmaskartas FROM registeredusers WHERE (Slapyvardis = @Slapyvardis)", databaseConnection);
                validatelogin.Parameters.AddWithValue("@Slapyvardis", username);
                //validatelogin.Parameters.AddWithValue("@Slaptažodis", password);
                MySqlDataReader reader = validatelogin.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    firsttime = reader.GetValue(0).ToString();
                    return firsttime;
                }

            }
            catch
            {
                MessageBox.Show("Nepavyko užmegzti ryšio su duomenų baze, bandykite dar kartą");
            }
            return null;
        }
        public string IsAdmin(string username)
        {
            string isadmin;
            string MySQLConnetionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=usersdb";
            try
            {
                MySqlConnection databaseConnection = new MySqlConnection(MySQLConnetionString);

                databaseConnection.Open();
                MySqlCommand validatelogin = new MySqlCommand("SELECT Administratorius FROM registeredusers WHERE (Slapyvardis = @Slapyvardis)", databaseConnection);
                validatelogin.Parameters.AddWithValue("@Slapyvardis", username);
                //validatelogin.Parameters.AddWithValue("@Slaptažodis", password);
                MySqlDataReader reader = validatelogin.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    isadmin = reader.GetValue(0).ToString();
                    return isadmin;
                }

            }
            catch
            {
                MessageBox.Show("Nepavyko užmegzti ryšio su duomenų baze, bandykite dar kartą");
            }
            return null;
        }
        public string UserloginID(string username)
        {
            string userid;
            string MySQLConnetionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=usersdb";
            try
            {
                MySqlConnection databaseConnection = new MySqlConnection(MySQLConnetionString);

                databaseConnection.Open();
                MySqlCommand validatelogin = new MySqlCommand("SELECT VartotojoNR FROM registeredusers WHERE (Slapyvardis = @Slapyvardis)", databaseConnection);
                validatelogin.Parameters.AddWithValue("@Slapyvardis", username);
                //validatelogin.Parameters.AddWithValue("@Slaptažodis", password);
                MySqlDataReader reader = validatelogin.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    userid = reader.GetValue(0).ToString();
                    return userid;
                }

            }
            catch
            {
                MessageBox.Show("Nepavyko užmegzti ryšio su duomenų baze, bandykite dar kartą");
            }
            return null;

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (var registracija = new Registracija())
            {

                registracija.ShowDialog();
            }
        }
    }
}
