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
    public partial class Sveikatospokyciai : Form
    {
        string userid;
        public Sveikatospokyciai()
        {
            InitializeComponent();
        }
        public void userID(string identification)
        {
            userid = identification;
        }
        private void btnGoBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string comment = textBox1.Text;
            string formatted = DateTime.Today.ToString("dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string MySQLConnetionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=usersdb";
            try
            {
                MySqlConnection databaseConnection = new MySqlConnection(MySQLConnetionString);

                databaseConnection.Open();
                MySqlCommand cmd = new MySqlCommand(@"INSERT INTO sveikatospokyciai (VartotojoNR, Sveikatos_pokytis, Data) VALUES (@VartotojoNR, @Sveikatos_pokytis, @Data)", databaseConnection);
                cmd.Parameters.AddWithValue("VartotojoNR", userid);
                cmd.Parameters.AddWithValue("Sveikatos_pokytis", comment);
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
        private void Datagridloader()
        {
            dataGridView1.DataSource = null;

            dataGridView1.Refresh();

            string MySQLConnetionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=usersdb";

            String selectQuery = @"SELECT Sveikatos_pokytis, Data FROM sveikatospokyciai WHERE (VartotojoNR = @VartotojoNR)";
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

        private void Sveikatospokyciai_Load(object sender, EventArgs e)
        {
            Datagridloader();
        }
    }
}
