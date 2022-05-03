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
    public partial class Nusiskundimai : Form
    {
        string userid;
        public Nusiskundimai()
        {
            InitializeComponent();
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (CheckboxAnonymously.Checked == true)
            {
                savetoDBwithoutUser();
            }
            else
            {
                savetoDBwithUser();
            }
        }
        private void Datagridloader()
        {
            dataGridView1.DataSource = null;

            dataGridView1.Refresh();

            string MySQLConnetionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=usersdb";

            String selectQuery = @"SELECT Pastebėjimas, Data FROM pateiktiskundai WHERE (VartotojoNR = @VartotojoNR)";
            MySqlConnection databaseConnection = new MySqlConnection(MySQLConnetionString);
            try
            {
                databaseConnection.Open();
                MySqlDataAdapter da;

                MySqlCommand command = new MySqlCommand(selectQuery, databaseConnection);
                command.Parameters.AddWithValue("@VartotojoNR", userid);
                da = new MySqlDataAdapter(command);
                DataTable table = new DataTable();

                da.Fill(table);
                dataGridView1.DataSource = table;
                dataGridView1.AutoResizeColumns();
                da.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nepavyko užmegzti ryšio su duomenų baze, kad būtų užkrauti jūsų pranėšimai, bandykite dar kartą");
            }
        }
        public void userID(string identification)
        {
            userid = identification;
        }
        public void savetoDBwithUser()
        {
            string comment = textBox1.Text;
            string formatted = DateTime.Today.ToString("dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string MySQLConnetionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=usersdb";
            try
            {
                MySqlConnection databaseConnection = new MySqlConnection(MySQLConnetionString);

                databaseConnection.Open();
                MySqlCommand cmd = new MySqlCommand(@"INSERT INTO pateiktiskundai (VartotojoNR, Pastebėjimas, Data) VALUES (@VartotojoNR, @Pastebėjimas, @Data)", databaseConnection);
                cmd.Parameters.AddWithValue("VartotojoNR", userid);
                cmd.Parameters.AddWithValue("Pastebėjimas", comment);
                cmd.Parameters.AddWithValue("Data", formatted);

                cmd.ExecuteNonQuery();
                databaseConnection.Close();
                textBox1.Clear();
            }
            catch
            {
                MessageBox.Show("Nepavyko užmegzti ryšio su duomenų baze, bandykite dar kartą");
            }
            Datagridloader();
        }
        public void savetoDBwithoutUser()
        {
            string comment = textBox1.Text;
            string formatted = DateTime.Today.ToString("dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string MySQLConnetionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=usersdb";
            try
            {
                MySqlConnection databaseConnection = new MySqlConnection(MySQLConnetionString);

                databaseConnection.Open();
                MySqlCommand cmd = new MySqlCommand(@"INSERT INTO pateiktiskundai (Pastebėjimas, Data) VALUES (@Pastebėjimas, @Data)", databaseConnection);
                cmd.Parameters.AddWithValue("Pastebėjimas", comment);
                cmd.Parameters.AddWithValue("Data", formatted);

                cmd.ExecuteNonQuery();
                databaseConnection.Close();
                textBox1.Clear();
            }
            catch
            {
                MessageBox.Show("Nepavyko užmegzti ryšio su duomenų baze, bandykite dar kartą");
            }
            Datagridloader();
        }

        private void Nusiskundimai_Load(object sender, EventArgs e)
        {
            Datagridloader();
        }
    }
}
