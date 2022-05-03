using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SveikatosSistema
{
    public partial class Sveikatospazymos : Form
    {
        string userid;
        public Sveikatospazymos()
        {
            InitializeComponent();
        }
        public void userID(string identification)
        {
            userid = identification;
        }
        private void btnOpenDB_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show("Nepasirinkta jokia reikšmė atidaryti nuotraukai");
            }
            else
            {
                string comboboxdata = comboBox1.SelectedItem.ToString();


                string MySQLConnetionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=usersdb";

                String selectQuery = @"SELECT * FROM sveikatospazymos WHERE (Nuotraukos_pavadinimas = @Nuotraukos_pavadinimas)";
                MySqlConnection databaseConnection = new MySqlConnection(MySQLConnetionString);
                try
                {
                    databaseConnection.Open();
                    MySqlDataAdapter da;

                    MySqlCommand command = new MySqlCommand(selectQuery, databaseConnection);
                    command.Parameters.AddWithValue("@Nuotraukos_pavadinimas", comboboxdata);
                    da = new MySqlDataAdapter(command);

                    DataTable table = new DataTable();

                    da.Fill(table);

                    byte[] img = (byte[])table.Rows[0][0];

                    MemoryStream ms = new MemoryStream(img);

                    this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox1.Image = Image.FromStream(ms);

                    da.Dispose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Nepavyko užmegzti ryšio su duomenų baze, bandykite dar kartą");
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string formatted = DateTime.Today.ToString("dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
            if (txtboxPicturename.Text.Length == 0)
            {
                MessageBox.Show("Įveskite nuotraukos pavadinimą");
            }
            else
            {
                string MySQLConnetionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=usersdb";

                MemoryStream ms = new MemoryStream();
                pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                byte[] img = ms.ToArray();
                MySqlConnection databaseConnection = new MySqlConnection(MySQLConnetionString);
                String insertQuery = @"INSERT INTO sveikatospazymos( Nuotrauka, VartotojoNR, Nuotraukos_pavadinimas, Sukūrimo_data) VALUES (@Nuotrauka, @VartotojoNR, @Nuotraukos_pavadinimas, @Sukūrimo_data)";
                databaseConnection.Open();

                MySqlCommand command = new MySqlCommand(insertQuery, databaseConnection);

                command.Parameters.Add("@Nuotrauka", MySqlDbType.MediumBlob);
                command.Parameters.Add("@VartotojoNR", MySqlDbType.Int16, 11);
                command.Parameters.Add("Nuotraukos_pavadinimas", MySqlDbType.String, 50);
                command.Parameters.Add("Sukūrimo_data", MySqlDbType.String, 10);

                command.Parameters["@Nuotrauka"].Value = img;
                command.Parameters["@VartotojoNR"].Value = userid;
                command.Parameters["Nuotraukos_pavadinimas"].Value = txtboxPicturename.Text.ToString();
                command.Parameters["Sukūrimo_data"].Value = formatted;
                command.ExecuteNonQuery();

                databaseConnection.Close();
                MessageBox.Show("Nuotrauka išsaugota duomenų bazėje");
                txtboxPicturename.Clear();
                comboBox1.ResetText();
                comboBox1.Items.Clear();
                duombaze();
            }
        }
        public void duombaze()
        {
            string MySQLConnetionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=usersdb";
            try
            {
                MySqlConnection databaseConnection = new MySqlConnection(MySQLConnetionString);

                databaseConnection.Open();
                MySqlCommand pictureselector = new MySqlCommand("SELECT Nuotraukos_pavadinimas FROM sveikatospazymos WHERE (VartotojoNR = @VartotojoNR)", databaseConnection);
                pictureselector.Parameters.AddWithValue("@VartotojoNR", userid);
                MySqlDataReader reader = pictureselector.ExecuteReader();
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

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

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
                    MySqlCommand cmd = new MySqlCommand(@"DELETE FROM `sveikatospazymos` WHERE (Nuotraukos_pavadinimas = @Nuotraukos_pavadinimas)", databaseConnection);
                    cmd.Parameters.AddWithValue("Nuotraukos_pavadinimas", comboboxdata);

                    cmd.ExecuteNonQuery();
                    databaseConnection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Nepavyko užmegzti ryšio su duomenų baze, bandykite dar kartą");
                }
            }
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
            }
            comboBox1.ResetText();
            comboBox1.Items.Clear();
            duombaze();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.SaveFileDialog saveDlg = new System.Windows.Forms.SaveFileDialog();
            saveDlg.InitialDirectory = @"C:\";
            saveDlg.Filter = "JPG|*.jpg|PNG|*.png|Bitmap|*.bmp";
            saveDlg.FilterIndex = 0;
            saveDlg.RestoreDirectory = true;
            if (saveDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    string path = saveDlg.FileName;
                    pictureBox1.Image.Save(path, ImageFormat.Jpeg);
                }
                catch (System.Runtime.InteropServices.COMException)
                {
                    MessageBox.Show("Įvyko klaida failo išsaugojime, pabandykite dar kartą");
                }
                MessageBox.Show("Išsaugota sėkmingai");

            }
        }

        private void btnBacktoMain_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "JPG|*.jpg|PNG|*.png|Bitmap|*.bmp", ValidateNames = true, Multiselect = false })
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                        pictureBox1.Image = Image.FromFile(ofd.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
