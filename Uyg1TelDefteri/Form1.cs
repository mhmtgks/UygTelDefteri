using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Uyg1TelDefteri
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataSet ds = DBIslemleri.ulkeleriCek();
            comboBox1.DisplayMember = "UlkeAdi";
            comboBox1.ValueMember = "UlkeID";
            comboBox1.DataSource = ds.Tables[0];
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ulkeID =(int) comboBox1.SelectedValue;
            DataSet ds2 = DBIslemleri.sehirleriCek(ulkeID);
            comboBox2.DisplayMember = "Sehir";
             comboBox2.ValueMember = "SehirID";
             comboBox2.DataSource = ds2.Tables[0];

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string adi = textBox1.Text;
            string soyadi = textBox2.Text;
            string tel = textBox3.Text;
            int sid = (int)comboBox2.SelectedValue;
            string adr = textBox4.Text;

            DBIslemleri.Ekle(adi, soyadi, tel, sid, adr);
            MessageBox.Show("Eklendi");
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            comboBox1.SelectedIndex = 0;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string aranan = textBox5.Text;
            DataSet sonuclar = DBIslemleri.Arama(aranan);
            dataGridView1.DataSource = sonuclar.Tables[0];
            if (sonuclar.Tables[0].Rows.Count == 0)
                MessageBox.Show("Kayıt Yok");
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int kisiID = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
                textBox6.Text= dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox7.Text= dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                textBox8.Text= dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                textBox9.Text= dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int kisiID = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
                string yeniTel = textBox8.Text;
                string yeniAdr = textBox9.Text;
                DBIslemleri.Guncelle(kisiID, yeniTel, yeniAdr);
                MessageBox.Show("Güncellendi");

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int kisiID = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
                DBIslemleri.Sil(kisiID);
                MessageBox.Show("Silindi");

            }
        }
    }
}
