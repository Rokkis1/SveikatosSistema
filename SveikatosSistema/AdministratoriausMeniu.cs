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
    
    public partial class AdministratoriausMeniu : Form
    {
        string userid;
        public AdministratoriausMeniu()
        {
            InitializeComponent();
        }
        public void userID(string identification)
        {
            userid = identification;
        }

        private void btnCreateAdmin_Click(object sender, EventArgs e)
        {
            using (var admincreate = new Registracijaadministratoriaus())
            {
                admincreate.ShowDialog();
            }
        }

        private void AdministratoriausMeniu_Load(object sender, EventArgs e)
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

        private void btnReviewReport_Click(object sender, EventArgs e)
        {
            using (var reviewreport = new Perziuretinusiskundimus())
            {
                reviewreport.ShowDialog();
            }
        }

        private void btnReviewHealthchanges_Click(object sender, EventArgs e)
        {
            using (var reviewhealthchanges = new Perziuretisveikatospokycius())
            {
                reviewhealthchanges.ShowDialog();
            }
        }

        private void btnReviewPhysicalevaluation_Click(object sender, EventArgs e)
        {
            using (var reviewphysicalhealth = new Perziuretifizinesveikata())
            {
                reviewphysicalhealth.ShowDialog();
            }
        }

        private void btnReviewPsychologicalevaluation_Click(object sender, EventArgs e)
        {
            using (var reviewpsychologicalhealth = new Perziuretipsichinesveikata())
            {
                reviewpsychologicalhealth.ShowDialog();
            }
        }

        private void btnControlImages_Click(object sender, EventArgs e)
        {
            using (var controlimages = new SveikatosPažymos())
            {
                controlimages.userID(userid);
                controlimages.ShowDialog();
            }
        }

        private void btnRemoveUser_Click(object sender, EventArgs e)
        {
            using (var removeuser = new Pasalintivartotoja())
            {
                removeuser.userID(userid);
                removeuser.ShowDialog();
            }
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            using (var menu = new Meniu())
            {
                this.Hide();
                menu.userID(userid);
                menu.ShowDialog();

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
    }
    
}
