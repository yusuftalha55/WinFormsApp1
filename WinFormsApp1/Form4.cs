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

namespace WinFormsApp1
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=ep-calm-dust-854360-pooler.us-east-1.postgres.vercel-storage.com;port=5432; Database=verceldb; user ID=default; password=MK1lamIUr6fb");

        private void button3_Click(object sender, EventArgs e)
        {
            string sorgu = "select * from musteritablosu";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1sec = new Form1();
            form1sec.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                int id = (int)selectedRow.Cells["id"].Value;

                // Güncellenecek yeni değerleri al
                string ad = textBox2.Text;
                string soyad = textBox3.Text;
                int telefon = Convert.ToInt32(textBox4.Text);

                using (NpgsqlConnection connection = new NpgsqlConnection("server=ep-calm-dust-854360-pooler.us-east-1.postgres.vercel-storage.com;port=5432; Database=verceldb; user ID=default; password=MK1lamIUr6fb"))
                {
                    connection.Open();

                    using (NpgsqlCommand command = new NpgsqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "UPDATE musteritablosu SET ad = @ad, soyad = @soyad, telefon = @telefon WHERE id = @id";
                        command.Parameters.AddWithValue("@ad", ad);
                        command.Parameters.AddWithValue("@soyad", soyad);
                        command.Parameters.AddWithValue("@telefon", telefon);
                        command.Parameters.AddWithValue("@id", id);

                        try
                        {
                            int affectedRows = command.ExecuteNonQuery();

                            if (affectedRows > 0)
                            {
                                MessageBox.Show("Veri başarıyla güncellendi.");
                            }
                            else
                            {
                                MessageBox.Show("Güncelleme işlemi başarısız oldu.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Hata: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen güncellemek için bir satır seçin.");
            }
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
            string deleteQuery = "DELETE FROM musteritablosu WHERE id = @id";

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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(textBox1.Text);
                string ad = textBox2.Text;
                string soyad = textBox3.Text;
                int telefon = Convert.ToInt32(textBox4.Text);

                using (NpgsqlConnection connection = new NpgsqlConnection("server=ep-calm-dust-854360-pooler.us-east-1.postgres.vercel-storage.com;port=5432; Database=verceldb; user ID=default; password=MK1lamIUr6fb"))
                {
                    connection.Open();

                    using (NpgsqlCommand command = new NpgsqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "INSERT INTO musteritablosu (id, ad, soyad, telefon) VALUES (@id, @ad, @soyad, @telefon)";
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@ad", ad);
                        command.Parameters.AddWithValue("@soyad", soyad);
                        command.Parameters.AddWithValue("@telefon", telefon);

                        int affectedRows = command.ExecuteNonQuery();

                        if (affectedRows > 0)
                        {
                            MessageBox.Show("Veri başarıyla eklendi.");
                        }
                        else
                        {
                            MessageBox.Show("Veri eklenirken bir hata oluştu.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

    }
}
