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
    public partial class Perziuretifizinesveikata : Form
    {
        public Perziuretifizinesveikata()
        {
            InitializeComponent();
        }

        private void Perziuretifizinesveikata_Load(object sender, EventArgs e)
        {
            Datagridloader();
            ComboBoxPopulator();
            fillChart();
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
                    MySqlCommand cmd = new MySqlCommand(@"DELETE FROM `fizinesveikata` WHERE (Fizinės_sveikatosNR = @Fizinės_sveikatosNR)", databaseConnection);
                    cmd.Parameters.AddWithValue("Fizinės_sveikatosNR", comboboxdata);

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
            fillChart();
        }
        private void Datagridloader()
        {
            dataGridView1.DataSource = null;

            dataGridView1.Refresh();

            string MySQLConnetionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=usersdb";

            String selectQuery = @"SELECT Fizinės_sveikatosNR, Aktyvumas, Laikas_lauke, Darbo_krūvis, Skausmas, VartotojoNR, Data FROM fizinesveikata";
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
        public void ComboBoxPopulator()
        {
            string MySQLConnetionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=usersdb";
            try
            {
                MySqlConnection databaseConnection = new MySqlConnection(MySQLConnetionString);

                databaseConnection.Open();
                MySqlCommand validatelogin = new MySqlCommand("SELECT Fizinės_sveikatosNR FROM fizinesveikata", databaseConnection);
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
        private void btnGoBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void fillChart()
        {
            chart1.Series["Įrašai"].Points.Clear();
            this.chart1.Titles.Clear();
            this.chart1.Titles.Add("Įrašų kiekis per dieną");
            chart1.ChartAreas[0].AxisX.Title = "Datos";
            chart1.ChartAreas[0].AxisY.Title = "Kiekis";
            List<String> datos = new List<String>();
            string MySQLConnetionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=usersdb";
            try
            {
                MySqlConnection databaseConnection = new MySqlConnection(MySQLConnetionString);

                databaseConnection.Open();
                MySqlCommand validatelogin = new MySqlCommand(@"SELECT Data FROM fizinesveikata", databaseConnection);
                MySqlDataReader reader = validatelogin.ExecuteReader();
                while (reader.Read())
                {
                    datos.Add(reader.GetValue(0).ToString());
                }

            }
            catch
            {
                MessageBox.Show("Nepavyko užmegzti ryšio su duomenų baze, kad būtų užkrautas vizualus duomenų atvaizdavimas, bandykite dar kartą");
            }
            var q = datos.GroupBy(x => x)
            .Select(g => new { Value = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count);

            foreach (var x in q)
            {
                chart1.Series["Įrašai"].Points.AddXY(x.Value, x.Count);
            }
        }

        private void btnGetReport_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.SaveFileDialog saveDlg = new System.Windows.Forms.SaveFileDialog();
            saveDlg.InitialDirectory = @"C:\";
            saveDlg.Filter = "Excel files *.xlsx|*.xlsx";
            saveDlg.FilterIndex = 0;
            saveDlg.RestoreDirectory = true;
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            worksheet = workbook.Sheets[1];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "Turinys";
            for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
            }
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                }
            }
            if (saveDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    string path = saveDlg.FileName;
                    workbook.SaveAs(path, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                }
                catch (System.Runtime.InteropServices.COMException)
                {
                    MessageBox.Show("Įvyko klaida failo išsaugojime, pabandykite dar kartą");
                }
                MessageBox.Show("Išsaugota sėkmingai");

            }
        }
    }
}
