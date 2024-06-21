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

namespace Film_Arsiv_Mini_Uygulama
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-3JI920O\SQLEXPRESS;Initial Catalog=DbFilmArsivi;Integrated Security=True;");
        void filmler()
        { 
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBLFILMLER", conn);
            DataTable dt = new DataTable(); //veritablosu nesnesi oluşturduk
            da.Fill(dt); //veritabanındaki verileri doldurduk
            dataGridView1.DataSource = dt; //datagridview1'in veri kaynağı dt oldu
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            filmler();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("insert into TBLFILMLER (AD,KATEGORI,LINK) values (@p1,@p2,@p3)", conn);
            cmd.Parameters.AddWithValue("@p1", txtFilmAd.Text);
            cmd.Parameters.AddWithValue("@p2", txtKategori.Text);
            cmd.Parameters.AddWithValue("@p3", txtLink.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Film Eklendi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            filmler();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex; //seçilen satırın indexini aldık
            string link = dataGridView1.Rows[secilen].Cells[3].Value.ToString(); //seçilen satırın 3. hücresini aldık

            webBrowser1.Navigate(link); //webbrowser1'e linki gönderdik
        }

        private void btnHak_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bu uygulama Hüseyin AKTAŞ tarafından kodlanmıştır.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
