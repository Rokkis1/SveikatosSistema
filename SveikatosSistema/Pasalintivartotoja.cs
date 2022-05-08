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
    public partial class Pasalintivartotoja : Form
    {
        string userid;
        public Pasalintivartotoja()
        {
            InitializeComponent();
        }

        public void userID(string identification)
        {
            userid = identification;
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show("Nepasirinkta jokia reikšmė atidaryti nuotraukai");
            }
            else
            {
                string comboboxdata = comboBox1.SelectedItem.ToString();


                string MySQLConnetionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=usersdb";

                try
                {
                    MySqlConnection databaseConnection = new MySqlConnection(MySQLConnetionString);

                    databaseConnection.Open();
                    MySqlCommand cmd = new MySqlCommand(@"DELETE FROM `registeredusers` WHERE (VartotojoNR = @VartotojoNR)", databaseConnection);
                    cmd.Parameters.AddWithValue("VartotojoNR", comboboxdata);

                    cmd.ExecuteNonQuery();
                    databaseConnection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Nepavyko užmegzti ryšio su duomenų baze, bandykite dar kartą");
                }
            }
            comboBox1.Items.Clear();
            comboBox1.ResetText();
            ComboBoxPopulator();
            Datagridloader();
        }
        public void ComboBoxPopulator()
        {
            string MySQLConnetionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=usersdb";
            try
            {
                MySqlConnection databaseConnection = new MySqlConnection(MySQLConnetionString);

                databaseConnection.Open();
                MySqlCommand validatelogin = new MySqlCommand("SELECT VartotojoNR FROM registeredusers", databaseConnection);
                MySqlDataReader reader = validatelogin.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader.GetValue(0).ToString());
                }

            }
            catch
            {
                MessageBox.Show("Nepavyko užmegzti ryšio su duomenų baze, bandykite dar kartą");
            }
        }
        private void Datagridloader()
        {
            dataGridView1.DataSource = null;

            dataGridView1.Refresh();

            string MySQLConnetionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=usersdb";

            String selectQuery = @"SELECT Slapyvardis, Gimimo_data, VartotojoNR  FROM registeredusers";
            MySqlConnection databaseConnection = new MySqlConnection(MySQLConnetionString);
            try
            {
                databaseConnection.Open();
                MySqlDataAdapter da;

                MySqlCommand command = new MySqlCommand(selectQuery, databaseConnection);
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

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Pasalintivartotoja_Load(object sender, EventArgs e)
        {
            Datagridloader();
            ComboBoxPopulator();
        }
    }
}
