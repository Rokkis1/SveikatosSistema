using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SveikatosSistema
{
    public partial class Registracija : Form
    {
        public Registracija()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string name, surname, username, password, passwordrepeated, formatted;
            DateTime? selectedDate;

            name = NameText.Text;
            surname = SurnameText.Text;
            selectedDate = dateTimePicker1.Value;
            formatted = selectedDate.Value.ToString("dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
            username = UsernameText.Text;
            password = MaskedPasswordText.Text;
            passwordrepeated = MaskedRepeatedPasswordText.Text;

            Boolean match = ContainsNumber(password);
            Boolean usernameexists;

            if (name.Length != 0 && surname.Length != 0)
            {
                if (selectedDate < DateTime.Today.AddYears(-18))
                {
                    if (username.Length != 0)
                    {
                        usernameexists = UserValidator(username);
                        if (usernameexists == false)
                        {
                            if (password.Length >= 8 && match == true)
                            {
                                if (password == passwordrepeated)
                                {
                                    string hash = SHA_256Hash(password);
                                    save(name, surname, formatted, username, hash);
                                    MessageBox.Show("Sukurta sėkmingai");
                                    this.Hide();
                                    using (var prisijungimas = new Prisijungimas())
                                    {

                                        prisijungimas.ShowDialog();
                                    }
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
                            MessageBox.Show("Šis slapyvardis jau yra užimtas");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Įveskite slapyvardį");
                    }
                }
                else
                {
                    MessageBox.Show("Jums dar nėra 18 metų!");
                }
            }
            else
            {
                MessageBox.Show("Neįvestas vardas arba pavardė!");
            }
        }
        public string SHA_256Hash(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes("kisonas1"));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public bool UserValidator(string username)
        {
            string MySQLConnetionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=usersdb";
            try
            {
                MySqlConnection databaseConnection = new MySqlConnection(MySQLConnetionString);

                databaseConnection.Open();
                MySqlCommand check_User_Name = new MySqlCommand("SELECT * FROM registeredusers WHERE (Slapyvardis = @Slapyvardis)", databaseConnection);
                check_User_Name.Parameters.AddWithValue("@Slapyvardis", username);
                MySqlDataReader reader = check_User_Name.ExecuteReader();
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
        public bool ContainsNumber(string password)
        {
            char[] numbers = "1234567890".ToCharArray();
            foreach (var specialChar in numbers.Where(password.Contains))
            {
                return true;
            }
            return false;
        }

        public void save(string name, string surname, string formatted, string username, string password)
        {

            string MySQLConnetionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=usersdb";
            try
            {
                MySqlConnection databaseConnection = new MySqlConnection(MySQLConnetionString);

                databaseConnection.Open();
                MySqlCommand cmd = new MySqlCommand(@"INSERT INTO registeredusers (Vardas, Pavardė, Slapyvardis, Slaptažodis, Gimimo_data) VALUES (@Vardas, @Pavardė, @Slapyvardis, @Slaptažodis, @Gimimo_data)", databaseConnection);
                cmd.Parameters.AddWithValue("Vardas", name);
                cmd.Parameters.AddWithValue("Pavardė", surname);
                cmd.Parameters.AddWithValue("Slapyvardis", username);
                cmd.Parameters.AddWithValue("Slaptažodis", password);
                cmd.Parameters.AddWithValue("Gimimo_data", formatted);

                cmd.ExecuteNonQuery();
                databaseConnection.Close();
            }
            catch
            {
                MessageBox.Show("Nepavyko užmegzti ryšio su duomenų baze, bandykite dar kartą");
            }
        }
        private void btnGoBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (var prisijungimas = new Prisijungimas())
            {

                  prisijungimas.ShowDialog();
            }
        }
    }
}
