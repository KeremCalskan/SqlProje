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
using System.Text.RegularExpressions;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Proje
{
    public partial class Form3 : Form
    {
       
        SqlConnection baglanti= new SqlConnection("Data Source=LAPTOP-9CH3VCJ1;Initial Catalog=proje2;Integrated Security=True");
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            this.booksTableAdapter.Fill(this.proje2DataSet.Books);
            LoadCombinedData();
            // TODO: Bu kod satırı 'proje2DataSet.Checkouts' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            //this.checkoutsTableAdapter.Fill(this.proje2DataSet.Checkouts);
            label10.Text = "Lütfen kiralamak istediğiniz kitaba tıklayınız";

        }
       

        private void LoadCombinedData()
        {
            string sqlQuery = "SELECT c.chekout_id, b.title, bo.name, bo.borrower_id, c.chekout_date, c.return_date " +
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
            LoadCombinedData();
            this.booksTableAdapter.Fill(this.proje2DataSet.Books);
        }

        private void button3_Click(object sender, EventArgs e)
        {
           /* string phoneNumber = textBox2.Text; // TextBox'tan telefon numarasını al

            string pattern = @"^\d{2}-\d{4} \d{3}$"; // İstenilen format için düzenli ifade deseni                   
           

            if (!Regex.IsMatch(phoneNumber, pattern))
            {
                MessageBox.Show("Lütfen geçerli bir telefon numarası giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Focus();
            }             
            else
            {   
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into Borrowers (name,mobile_number,national_id) values (@p1,@p2,@p3)",baglanti);
                komut.Parameters.AddWithValue("@p1", textBox1.Text);
                komut.Parameters.AddWithValue("@p2", textBox2.Text);
                komut.Parameters.AddWithValue("@p3", textBox3.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                

                string name = textBox1.Text;

                DateTime startDate = DateTime.Now.Date;
                int daysToAdd = 15;
                DateTime endDate = startDate.AddDays(daysToAdd);
                decimal ceza = 0;
                baglanti.Open();
                string query = "SELECT borrower_id FROM Borrowers WHERE Name = @Name";
                SqlCommand command = new SqlCommand(query, baglanti);
                command.Parameters.AddWithValue("@Name", name);
                int borrowerId = (int)command.ExecuteScalar();


                SqlCommand odunc = new SqlCommand("insert into Checkouts (book_id,borrower_id,chekout_date,return_date,actual_return_day,late_penalty) values (@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);
                odunc.Parameters.AddWithValue("@p1", Convert.ToInt32(label9.Text));
                odunc.Parameters.AddWithValue("@p2", borrowerId);
                odunc.Parameters.AddWithValue("@p3", startDate);
                odunc.Parameters.AddWithValue("@p4", endDate);
                odunc.Parameters.AddWithValue("@p5", startDate);
                odunc.Parameters.AddWithValue("@p6", ceza);
                odunc.ExecuteNonQuery();
                baglanti.Close();


                



            



            }*/
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DateTime startDate = DateTime.Now.Date;
            int daysToAdd = 15;
            DateTime endDate = startDate;

            for (int i = 0; i < daysToAdd; i++)
            {
                endDate = endDate.AddDays(1);
              
                if (endDate.DayOfWeek == DayOfWeek.Saturday || endDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    daysToAdd++;
                }

                
            }            
            label2.Text=endDate.ToString();
            int secim = dataGridView2.SelectedCells[0].RowIndex;
            label9.Text = dataGridView2.Rows[secim].Cells[0].Value.ToString();
            label8.Text = dataGridView2.Rows[secim].Cells[1].Value.ToString();
        }   

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string phoneNumber = textBox2.Text; // TextBox'tan telefon numarasını al

            string pattern = @"^\d{2}-\d{4} \d{3}$"; // İstenilen format için düzenli ifade deseni                   
            string nationalId = textBox3.Text;
            string kitapp = label9.Text;

            if (!Regex.IsMatch(phoneNumber, pattern))
            {
                MessageBox.Show("Lütfen geçerli bir telefon numarası giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Focus();
            }
            else if (!string.IsNullOrEmpty(nationalId) && (nationalId.Length != 11 || !nationalId.All(char.IsDigit)))
            {
                // Geçersiz national_id değeri
                MessageBox.Show("National ID 11 haneli olmalı ve sadece rakamlardan oluşmalıdır!");
            }
            else if (string.IsNullOrEmpty(kitapp))
            {
                MessageBox.Show("Lütfen bir kitap seçin");
            }
            else
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into Borrowers (name,mobile_number,national_id) values (@p1,@p2,@p3)", baglanti);
                komut.Parameters.AddWithValue("@p1", textBox1.Text);
                komut.Parameters.AddWithValue("@p2", textBox2.Text);
                komut.Parameters.AddWithValue("@p3", textBox3.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();



                DateTime startDate = DateTime.Now.Date;
                int daysToAdd = 15;
                DateTime endDate = startDate;

                for (int i = 0; i < daysToAdd; i++)
                {
                    endDate = endDate.AddDays(1);
                    if (endDate.DayOfWeek == DayOfWeek.Saturday || endDate.DayOfWeek == DayOfWeek.Sunday)
                    {
                        daysToAdd++;
                    }

                }
                // endDate, iş günlerine göre 15 gün sonrasını temsil ediyor
                decimal ceza = 0;
                baglanti.Open();
                string name = textBox1.Text;
                string query = "SELECT borrower_id FROM Borrowers WHERE Name = @Name";
                SqlCommand command = new SqlCommand(query, baglanti);
                command.Parameters.AddWithValue("@Name", name);
                int borrowerId = (int)command.ExecuteScalar();


                SqlCommand odunc = new SqlCommand("insert into Checkouts (book_id,borrower_id,chekout_date,return_date,actual_return_day,late_penalty) values (@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);
                odunc.Parameters.AddWithValue("@p1", Convert.ToInt32(label9.Text));
                odunc.Parameters.AddWithValue("@p2", borrowerId);
                odunc.Parameters.AddWithValue("@p3", startDate);
                odunc.Parameters.AddWithValue("@p4", endDate);
                odunc.Parameters.AddWithValue("@p5", startDate);
                odunc.Parameters.AddWithValue("@p6", ceza);
                odunc.ExecuteNonQuery();


                MessageBox.Show("Kiralama işleminiz başarılı");

            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }


}
