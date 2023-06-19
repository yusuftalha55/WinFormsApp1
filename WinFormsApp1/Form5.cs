using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp1
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=ep-calm-dust-854360-pooler.us-east-1.postgres.vercel-storage.com;port=5432; Database=verceldb; user ID=default; password=MK1lamIUr6fb");
        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1sec = new Form1();
            form1sec.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sorgu = "select *from satislar";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form1 form1sec = new Form1();
            form1sec.Show();
            this.Hide();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Veritabanından da silme işlemini gerçekleştir
                int id = (int)selectedRow.Cells["id"].Value;

                SilVeritabanindan(id);

                dataGridView1.Rows.Remove(selectedRow);
            }
            else
            {
                MessageBox.Show("Lütfen silmek için bir satır seçin.");
            }
        }

        private void SilVeritabanindan(int id)
        {
            // Veritabanından silme sorgusu
            string deleteQuery = "DELETE FROM satislar WHERE id = @id";

            using (NpgsqlConnection connection = new NpgsqlConnection("server=ep-calm-dust-854360-pooler.us-east-1.postgres.vercel-storage.com;port=5432; Database=verceldb; user ID=default; password=MK1lamIUr6fb"))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    try
                    {
                        connection.Open();
                        int affectedRows = command.ExecuteNonQuery();

                        if (affectedRows > 0)
                        {
                            MessageBox.Show("Kayıt başarıyla silindi.");
                        }
                        else
                        {
                            MessageBox.Show("Kayıt bulunamadı.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata: " + ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBox1.Text);
            string urunAd = textBox2.Text;
            int satisAdetGunluk = Convert.ToInt32(textBox3.Text);
            int satisAdetAylik = Convert.ToInt32(textBox4.Text);
            int satisAdetYillik = Convert.ToInt32(textBox5.Text);

            using (NpgsqlConnection connection = new NpgsqlConnection("server=ep-calm-dust-854360-pooler.us-east-1.postgres.vercel-storage.com;port=5432; Database=verceldb; user ID=default; password=MK1lamIUr6fb"))
            {
                connection.Open();

                string insertQuery = "INSERT INTO satislar (id, urunad, satisadetgunluk, satisadetaylık, satisadetyıllık) " +
                    "VALUES (@id, @urunAd, @satisAdetGunluk, @satisAdetAylik, @satisAdetYillik)";

                using (NpgsqlCommand command = new NpgsqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@urunAd", urunAd);
                    command.Parameters.AddWithValue("@satisAdetGunluk", satisAdetGunluk);
                    command.Parameters.AddWithValue("@satisAdetAylik", satisAdetAylik);
                    command.Parameters.AddWithValue("@satisAdetYillik", satisAdetYillik);

                    int affectedRows = command.ExecuteNonQuery();

                    if (affectedRows > 0)
                    {
                        MessageBox.Show("Ürün başarıyla eklendi.");
                    }
                    else
                    {
                        MessageBox.Show("Ürün eklenirken bir hata oluştu.");
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                int id = (int)selectedRow.Cells["id"].Value;

                // Güncellenecek ürünün bilgilerini al
                string urunAd = textBox2.Text;
                int satisAdetGunluk = Convert.ToInt32(textBox3.Text);
                int satisAdetAylik = Convert.ToInt32(textBox4.Text);
                int satisAdetYillik = Convert.ToInt32(textBox5.Text);

                using (NpgsqlConnection connection = new NpgsqlConnection("server=ep-calm-dust-854360-pooler.us-east-1.postgres.vercel-storage.com;port=5432; Database=verceldb; user ID=default; password=MK1lamIUr6fb"))
                {
                    connection.Open();

                    string updateQuery = "UPDATE satislar SET urunad = @urunAd, satisadetgunluk = @satisAdetGunluk, satisadetaylık = @satisAdetAylik, satisadetyıllık = @satisAdetYillik WHERE id = @id";

                    using (NpgsqlCommand command = new NpgsqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@urunAd", urunAd);
                        command.Parameters.AddWithValue("@satisAdetGunluk", satisAdetGunluk);
                        command.Parameters.AddWithValue("@satisAdetAylik", satisAdetAylik);
                        command.Parameters.AddWithValue("@satisAdetYillik", satisAdetYillik);

                        int affectedRows = command.ExecuteNonQuery();

                        if (affectedRows > 0)
                        {
                            MessageBox.Show("Ürün başarıyla güncellendi.");
                        }
                        else
                        {
                            MessageBox.Show("Güncellenecek ürün bulunamadı.");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen güncellemek için bir satır seçin.");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        

        }
    }
}
