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
    public partial class Fizinesveikata : Form
    {
        string userid;
        public Fizinesveikata()
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
            double average = 0;
            string activity = comboBox1.SelectedItem.ToString();
            string outsidetime = comboBox2.SelectedItem.ToString();
            string workload = comboBox3.SelectedItem.ToString();
            string pain = comboBox4.SelectedItem.ToString();
            string formatted = DateTime.Today.ToString("dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
            average = (Convert.ToDouble(activity) + Convert.ToDouble(outsidetime) + Convert.ToDouble(workload) + Convert.ToDouble(pain)) / 4;
            average = Math.Round(average, 2);
            if (average > 1 && average < 6)
            {
                MessageBox.Show("Jūsų fizinė sveikata yra žemo lygio, rekomenduojama pasikalbėti su darbo vadovu dėl krūvio ir darbo");
            }
            if (average >= 6 && average < 8)
            {
                MessageBox.Show("Jūsų fizinė sveikata yra vidutinio lygio, rekomenduojama gerai pailsėti");
            }
            if (average >= 8 && average <= 10)
            {
                MessageBox.Show("Jūsų fizinė sveikata yra puikaus lygio");
            }

            string MySQLConnetionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=usersdb";
            try
            {
                MySqlConnection databaseConnection = new MySqlConnection(MySQLConnetionString);

                databaseConnection.Open();
                MySqlCommand cmd = new MySqlCommand(@"INSERT INTO fizinesveikata (VartotojoNR, Aktyvumas, Laikas_lauke, Darbo_krūvis, Skausmas, Data, Indeksas) VALUES (@VartotojoNR, @Aktyvumas, @Laikas_lauke, @Darbo_krūvis, @Skausmas, @Data, @Indeksas)", databaseConnection);
                cmd.Parameters.AddWithValue("VartotojoNR", userid);
                cmd.Parameters.AddWithValue("Aktyvumas", activity);
                cmd.Parameters.AddWithValue("Laikas_lauke", outsidetime);
                cmd.Parameters.AddWithValue("Darbo_krūvis", workload);
                cmd.Parameters.AddWithValue("Skausmas", pain);
                cmd.Parameters.AddWithValue("Data", formatted);
                cmd.Parameters.AddWithValue("Indeksas", average);
                cmd.ExecuteNonQuery();
                databaseConnection.Close();
                comboBox1.ResetText();
                comboBox2.ResetText();
                comboBox3.ResetText();
                comboBox4.ResetText();
            }
            catch
            {
                MessageBox.Show("Nepavyko užmegzti ryšio su duomenų baze, bandykite dar kartą");
            }
            fillChart();
            fillChartindividual();
        }

        private void Fizinesveikata_Load(object sender, EventArgs e)
        {
            fillChart();
            fillChartindividual();
            int[] values = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            for (int i = 0; i < values.Length; i++)
            {
                comboBox1.Items.Add(values[i]);
                comboBox2.Items.Add(values[i]);
                comboBox3.Items.Add(values[i]);
                comboBox4.Items.Add(values[i]);
            }
        }
        private void fillChart()
        {
            chart1.Series["Bendras vidurkis"].Points.Clear();
            this.chart1.Titles.Clear();
            this.chart1.Titles.Add("Savijautos rodiklis");
            chart1.ChartAreas[0].AxisX.Title = "Datos";
            chart1.ChartAreas[0].AxisY.Title = "Indeksas";
            List<Indeksai> indeksai = new List<Indeksai>();
            string MySQLConnetionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=usersdb";
            try
            {
                MySqlConnection databaseConnection = new MySqlConnection(MySQLConnetionString);

                databaseConnection.Open();
                MySqlCommand validatelogin = new MySqlCommand(@"SELECT Data, Indeksas FROM fizinesveikata", databaseConnection);
                MySqlDataReader reader = validatelogin.ExecuteReader();
                while (reader.Read())
                {
                    Indeksai indk = new Indeksai();
                    indk.date = (string)reader["Data"];
                    indk.value = (double)reader["Indeksas"];
                    indeksai.Add(indk);
                }

            }
            catch
            {
                MessageBox.Show("Nepavyko užmegzti ryšio su duomenų baze, kad būtų užkrautas vizualus duomenų atvaizdavimas, bandykite dar kartą");
            }
            var result = from indk in indeksai
                         orderby indk.date
                         group indeksai by indk.date into grp
                         let sum = indeksai.Where(x => x.date == grp.Key).Sum(x => x.value)
                         select new
                         {
                             Date = grp.Key,
                             Sum = sum,
                         };
            var q = indeksai.GroupBy(x => x.date)
            .Select(g => new { Value = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count);
            foreach (var x in q)
            {
                foreach (var y in result)
                {
                    if (x.Value == y.Date)
                    {
                        chart1.Series["Bendras vidurkis"].Points.AddXY(x.Value, y.Sum / x.Count);
                    }
                }
            }

        }
        private void fillChartindividual()
        {
            chart1.Series["Individualus vidurkis"].Points.Clear();
            List<Indeksai> indeksai = new List<Indeksai>();
            string MySQLConnetionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=usersdb";
            try
            {
                MySqlConnection databaseConnection = new MySqlConnection(MySQLConnetionString);

                databaseConnection.Open();
                MySqlCommand validatelogin = new MySqlCommand(@"SELECT Data, Indeksas FROM fizinesveikata WHERE (VartotojoNR = @VartotojoNR)", databaseConnection);
                validatelogin.Parameters.AddWithValue("VartotojoNR", userid);
                MySqlDataReader reader = validatelogin.ExecuteReader();
                while (reader.Read())
                {
                    Indeksai indk = new Indeksai();
                    indk.date = (string)reader["Data"];
                    indk.value = (double)reader["Indeksas"];
                    indeksai.Add(indk);
                }

            }
            catch
            {
                MessageBox.Show("Nepavyko užmegzti ryšio su duomenų baze, kad būtų užkrautas vizualus duomenų atvaizdavimas, bandykite dar kartą");
            }
            var result = from indk in indeksai
                         orderby indk.date
                         group indeksai by indk.date into grp
                         let sum = indeksai.Where(x => x.date == grp.Key).Sum(x => x.value)
                         select new
                         {
                             Date = grp.Key,
                             Sum = sum,
                         };
            var q = indeksai.GroupBy(x => x.date)
            .Select(g => new { Value = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count);
            foreach (var x in q)
            {
                foreach (var y in result)
                {
                    if (x.Value == y.Date)
                    {
                        chart1.Series["Individualus vidurkis"].Points.AddXY(x.Value, y.Sum / x.Count);
                    }
                }
            }
        }

    }
    public class Indeksai
    {
        public string date { get; set; }
        public double value { get; set; }
    }
}
