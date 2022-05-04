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
    public partial class Psichinesveikata : Form
    {
        string userid;
        public Psichinesveikata()
        {
            InitializeComponent();
        }
        public void userID(string identification)
        {
            userid = identification;
        }
        private void btnGetHelp_Click(object sender, EventArgs e)
        {
            var uri = "https://www.jaunimolinija.lt/lt/psichologine-pagalba";
            var psi = new System.Diagnostics.ProcessStartInfo();
            psi.UseShellExecute = true;
            psi.FileName = uri;
            System.Diagnostics.Process.Start(psi);
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            double average = 0;
            string temp = comboBox1.Text.ToString();
            string work = comboBox1.Text.ToString();
            if (Convert.ToDouble(temp) > 10 || Convert.ToDouble(temp) < 1) //ar verta realizuoti papildoma patikra, kad parinkta reiksme yra skaicius
            {
                MessageBox.Show("Patikrinkite ar gerai pasirinkote reikšmę ties šiandienos darbu");
            }
            string stress = comboBox2.Text.ToString();
            string news = comboBox3.Text.ToString();
            string sleepquality = comboBox4.Text.ToString();
            string eatingquality = comboBox5.Text.ToString();
            string wellness = comboBox6.Text.ToString();
            string formatted = DateTime.Today.ToString("dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
            average = (Convert.ToDouble(work) + Convert.ToDouble(stress) + Convert.ToDouble(news) + Convert.ToDouble(sleepquality) + Convert.ToDouble(eatingquality) + Convert.ToDouble(wellness)) / 6;
            average = Math.Round(average, 2);
            if (average > 1 && average < 6)
            {
                MessageBox.Show("Jūsų psichinė sveikata yra žemo lygio, rekomenduojama pasikalbėti su darbo vadovu dėl krūvio ir darbo, taip pat pasikalbėti su psichologu");
            }
            if (average >= 6 && average < 8)
            {
                MessageBox.Show("Jūsų psichinė sveikata yra vidutinio lygio, rekomenduojama atipalaiduoti ir vengti stresinių situacijų");
            }
            if (average >= 8 && average <= 10)
            {
                MessageBox.Show("Jūsų psichinė sveikata yra puikaus lygio");
            }
            string MySQLConnetionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=usersdb";
            try
            {
                MySqlConnection databaseConnection = new MySqlConnection(MySQLConnetionString);

                databaseConnection.Open();
                MySqlCommand cmd = new MySqlCommand(@"INSERT INTO psichinesveikata (VartotojoNR, Šiandienos_darbo_įvertinimas, Stresas, Naujienų_paveikiamumas, Miego_kokybė, Valgymo_kokybė, Savijauta, Data, Indeksas) VALUES (@VartotojoNR, @Šiandienos_darbo_įvertinimas, @Stresas, @Naujienų_paveikiamumas, @Miego_kokybė, @Valgymo_kokybė, @Savijauta, @Data, @Indeksas)", databaseConnection);
                cmd.Parameters.AddWithValue("VartotojoNR", userid);
                cmd.Parameters.AddWithValue("Šiandienos_darbo_įvertinimas", work);
                cmd.Parameters.AddWithValue("Stresas", stress);
                cmd.Parameters.AddWithValue("Naujienų_paveikiamumas", news);
                cmd.Parameters.AddWithValue("Miego_kokybė", sleepquality);
                cmd.Parameters.AddWithValue("Valgymo_kokybė", eatingquality);
                cmd.Parameters.AddWithValue("Savijauta", wellness);
                cmd.Parameters.AddWithValue("Data", formatted);
                cmd.Parameters.AddWithValue("Indeksas", average);
                cmd.ExecuteNonQuery();
                comboBox1.ResetText();
                comboBox2.ResetText();
                comboBox3.ResetText();
                comboBox4.ResetText();
                comboBox5.ResetText();
                comboBox6.ResetText();
                databaseConnection.Close();
            }
            catch
            {
                MessageBox.Show("Nepavyko užmegzti ryšio su duomenų baze, bandykite dar kartą");
            }
            fillChart();
            fillChartindividual();
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
                MySqlCommand validatelogin = new MySqlCommand(@"SELECT Data, Indeksas FROM psichinesveikata", databaseConnection);
                MySqlDataReader reader = validatelogin.ExecuteReader();
                while (reader.Read())
                {
                    Indeksai indk = new Indeksai();
                    indk.date = (string) reader["Data"];
                    indk.value = (double) reader["Indeksas"];
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
                    if(x.Value == y.Date)
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
                MySqlCommand validatelogin = new MySqlCommand(@"SELECT Data, Indeksas FROM psichinesveikata WHERE (VartotojoNR = @VartotojoNR)", databaseConnection);
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
        private void Psichinesveikata_Load(object sender, EventArgs e)
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
                comboBox5.Items.Add(values[i]);
                comboBox6.Items.Add(values[i]);
            }
        }
        public class Indeksai
        {
            public string date { get; set; }
            public double value { get; set; }
        }
    }
}
