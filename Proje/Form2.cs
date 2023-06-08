using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Proje
{
    public partial class Form2 : Form
    {

        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-9CH3VCJ1;Initial Catalog=proje2;Integrated Security=True");



        private void CalculateLatePenalty()
        {
            using (SqlConnection connection = new SqlConnection("Data Source=LAPTOP-9CH3VCJ1;Initial Catalog=proje2;Integrated Security=True"))
            {
                connection.Open();

                // Tarihleri kontrol etmek için bir SQL sorgusu oluşturulur
                string sqlQuery = "SELECT chekout_id, return_date, actual_return_day, DATEDIFF(day, return_date, actual_return_day) AS late_days " +
                                  "FROM Checkouts " +
                                  "WHERE actual_return_day > return_date";

                SqlCommand command = new SqlCommand(sqlQuery, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int chekoutId = reader.GetInt32(0);
                    DateTime returnDate = reader.GetDateTime(1);
                    DateTime actualReturnDate = reader.GetDateTime(2);
                    int lateDays = reader.GetInt32(3);

                    // Günlük 5 dolar ceza hesaplanır
                    decimal latePenalty = lateDays * 5;

                    // late_penalty sütunu güncellenir
                    string updateQuery = "UPDATE Checkouts SET late_penalty = @latePenalty WHERE chekout_id = @chekoutId";
                    SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@latePenalty", latePenalty);
                    updateCommand.Parameters.AddWithValue("@chekoutId", chekoutId);
                    updateCommand.ExecuteNonQuery();
                }

                reader.Close();
                connection.Close();
            }
        }
        public Form2()
        {
            
            InitializeComponent();
            LoadCombinedData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            Close();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private void LoadCombinedData()
        {
            string sqlQuery = "SELECT c.chekout_id, b.title, bo.name, bo.mobile_number, c.actual_return_day, c.return_date, c.late_penalty " +
                    "FROM Checkouts c " +
                    "INNER JOIN Borrowers bo ON c.borrower_id = bo.borrower_id " +
                    "INNER JOIN Books b ON c.book_id = b.book_id";

            DataTable combinedData = new DataTable();

            using (SqlConnection connection = new SqlConnection("Data Source=LAPTOP-9CH3VCJ1;Initial Catalog=proje2;Integrated Security=True"))
            {
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(combinedData);
            }

            dataGridView1.DataSource = combinedData;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CalculateLatePenalty();
            LoadCombinedData() ;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secim = dataGridView1.SelectedCells[0].RowIndex;
            label6.Text = dataGridView1.Rows[secim].Cells[1].Value.ToString();
            label7.Text = dataGridView1.Rows[secim].Cells[2].Value.ToString();
            label8.Text = dataGridView1.Rows[secim].Cells[3].Value.ToString();
            label9.Text = dataGridView1.Rows[secim].Cells[6].Value.ToString();
            label10.Text = dataGridView1.Rows[secim].Cells[0].Value.ToString();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            label11.Text = "Lütfen iade etmek istediğiniz kitabı seçiniz";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string labelText = label10.Text;

            if (string.IsNullOrEmpty(labelText))
            {
                MessageBox.Show("Lütfen listeden bir kişi seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                baglanti.Open();
                string name = label7.Text;
                string query = "SELECT borrower_id FROM Borrowers WHERE Name = @Name";
                SqlCommand command = new SqlCommand(query, baglanti);
                command.Parameters.AddWithValue("@Name", name);
                int borrowerId = (int)command.ExecuteScalar();
                SqlCommand sil = new SqlCommand("Delete From Checkouts Where chekout_id=@p1", baglanti);
                sil.Parameters.AddWithValue("@p1", label10.Text);
                sil.ExecuteNonQuery();
                SqlCommand sil2 = new SqlCommand("Delete From Borrowers Where borrower_id=@p2", baglanti);
                sil2.Parameters.AddWithValue("@p2", borrowerId);
                sil2.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("İade işlemiz başarı ile gerçekleşmiştir");
            }

        }



    }
}
