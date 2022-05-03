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
    public partial class Meniu : Form
    {
        string userid;
        public Meniu()
        {
            InitializeComponent();
        }
        public void userID(string identification)
        {
            userid = identification;
        }

        private void btnUpdateUser_Click(object sender, EventArgs e)
        {
            using (var updateUser = new Atnaujintipaskyrosduomenis())
            {
                updateUser.userID(userid);
                updateUser.ShowDialog();
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            using (var report = new Nusiskundimai())
            {
                report.userID(userid);
                report.ShowDialog();
            }
        }

        private void btnHealthchanges_Click(object sender, EventArgs e)
        {
            using (var healthchanges = new Sveikatospokyciai())
            {
                healthchanges.userID(userid);
                healthchanges.ShowDialog();
            }
        }

        private void btnPhysicalevaluation_Click(object sender, EventArgs e)
        {
            using (var physicalhealth = new Fizinesveikata())
            {
                physicalhealth.userID(userid);
                physicalhealth.ShowDialog();
            }
        }

        private void btnPsychologicalevaluation_Click(object sender, EventArgs e)
        {
            using (var psychologicalhealth = new Psichinesveikata())
            {
                psychologicalhealth.userID(userid);
                psychologicalhealth.ShowDialog();
            }
        }

        private void btnReadImage_Click(object sender, EventArgs e)
        {
            using (var healthcertificates = new Sveikatospazymos())
            {
                healthcertificates.userID(userid);
                healthcertificates.ShowDialog();
            }

        }

        private void Meniu_Load(object sender, EventArgs e)
        {
            duombaze();

        }

        public void duombaze()
        {
            string MySQLConnetionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=usersdb";
            try
            {
                MySqlConnection databaseConnection = new MySqlConnection(MySQLConnetionString);

                databaseConnection.Open();
                MySqlCommand pictureselector = new MySqlCommand("SELECT Slapyvardis FROM registeredusers WHERE (VartotojoNR = @VartotojoNR)", databaseConnection);
                pictureselector.Parameters.AddWithValue("@VartotojoNR", userid);
                MySqlDataReader reader = pictureselector.ExecuteReader();
                while (reader.Read())
                {
                    this.textBox1.AutoSize = true;
                    this.textBox1.Text = ("Prisijungęs kaip: " + reader.GetValue(0).ToString());
                }

            }
            catch
            {
                MessageBox.Show("Nepavyko užmegzti ryšio su duomenų baze, bandykite dar kartą");
            }
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            using (var logout = new Prisijungimas())
            {
                this.Hide();
                logout.ShowDialog();
                this.Close();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void btnDeleteallrecords_Click(object sender, EventArgs e)
        {
            string message = "Ar tikrai norite, jog būtų pašalinti visi duomenys susiję su jumis? (Paskyra išliks)";
            string caption = "Duomenų ištrinimas";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            result = MessageBox.Show(message, caption, buttons);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                delpsychological();
                delreports();
                delphysical();
                delhealthsertificates();
                delhealthchanges();
            }
            else
            {
            }
        }
        private void delpsychological()
        {
            string MySQLConnetionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=usersdb";

            try
            {
                MySqlConnection databaseConnection = new MySqlConnection(MySQLConnetionString);

                databaseConnection.Open();
                MySqlCommand cmd = new MySqlCommand(@"DELETE FROM `psichinesveikata` WHERE (VartotojoNR = @VartotojoNR)", databaseConnection);
                cmd.Parameters.AddWithValue("VartotojoNR", userid);

                cmd.ExecuteNonQuery();
                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nepavyko užmegzti ryšio su duomenų baze, bandykite dar kartą");
            }
        }
        private void delphysical()
        {
            string MySQLConnetionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=usersdb";

            try
            {
                MySqlConnection databaseConnection = new MySqlConnection(MySQLConnetionString);

                databaseConnection.Open();
                MySqlCommand cmd = new MySqlCommand(@"DELETE FROM `fizinesveikata` WHERE (VartotojoNR = @VartotojoNR)", databaseConnection);
                cmd.Parameters.AddWithValue("VartotojoNR", userid);

                cmd.ExecuteNonQuery();
                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nepavyko užmegzti ryšio su duomenų baze, bandykite dar kartą");
            }
        }
        private void delhealthchanges()
        {
            string MySQLConnetionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=usersdb";

            try
            {
                MySqlConnection databaseConnection = new MySqlConnection(MySQLConnetionString);

                databaseConnection.Open();
                MySqlCommand cmd = new MySqlCommand(@"DELETE FROM `sveikatospokyciai` WHERE (VartotojoNR = @VartotojoNR)", databaseConnection);
                cmd.Parameters.AddWithValue("VartotojoNR", userid);

                cmd.ExecuteNonQuery();
                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nepavyko užmegzti ryšio su duomenų baze, bandykite dar kartą");
            }
        }
        private void delhealthsertificates()
        {
            string MySQLConnetionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=usersdb";

            try
            {
                MySqlConnection databaseConnection = new MySqlConnection(MySQLConnetionString);

                databaseConnection.Open();
                MySqlCommand cmd = new MySqlCommand(@"DELETE FROM `sveikatospazymos` WHERE (VartotojoNR = @VartotojoNR)", databaseConnection);
                cmd.Parameters.AddWithValue("VartotojoNR", userid);

                cmd.ExecuteNonQuery();
                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nepavyko užmegzti ryšio su duomenų baze, bandykite dar kartą");
            }
        }
        private void delreports()
        {
            string MySQLConnetionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=usersdb";

            try
            {
                MySqlConnection databaseConnection = new MySqlConnection(MySQLConnetionString);

                databaseConnection.Open();
                MySqlCommand cmd = new MySqlCommand(@"DELETE FROM `pateiktiskundai` WHERE (VartotojoNR = @VartotojoNR)", databaseConnection);
                cmd.Parameters.AddWithValue("VartotojoNR", userid);

                cmd.ExecuteNonQuery();
                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nepavyko užmegzti ryšio su duomenų baze, bandykite dar kartą");
            }
        }
    }
    
}
