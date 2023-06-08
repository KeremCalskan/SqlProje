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




namespace Proje
{
    public partial class Form1 : Form
    {

        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-9CH3VCJ1;Initial Catalog=proje2;Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2= new Form2();
            form2.Show();
            Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3= new Form3();
            form3.Show();
            Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.booksTableAdapter.Fill(this.proje2DataSet.Books);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'proje2DataSet.Books' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.booksTableAdapter.Fill(this.proje2DataSet.Books);

        }
    }
}
