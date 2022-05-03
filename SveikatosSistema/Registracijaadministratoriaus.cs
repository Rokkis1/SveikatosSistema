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
    public partial class Registracijaadministratoriaus : Form
    {
        public Registracijaadministratoriaus()
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
                                    string hash = CreateMD5Hash(password);
                                    save(name, surname, formatted, username, hash);
                                    MessageBox.Show("Sukurta sėkmingai");
                                    this.Close();
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
                MySqlCommand cmd = new MySqlCommand(@"INSERT INTO registeredusers (Vardas, Pavardė, Slapyvardis, Slaptažodis, Gimimo_data, Administratorius) VALUES (@Vardas, @Pavardė, @Slapyvardis, @Slaptažodis, @Gimimo_data, @Administratorius)", databaseConnection);
                cmd.Parameters.AddWithValue("Vardas", name);
                cmd.Parameters.AddWithValue("Pavardė", surname);
                cmd.Parameters.AddWithValue("Slapyvardis", username);
                cmd.Parameters.AddWithValue("Slaptažodis", password);
                cmd.Parameters.AddWithValue("Gimimo_data", formatted);
                cmd.Parameters.AddWithValue("Administratorius", 1);

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
            this.Close();
        }
    }
}
