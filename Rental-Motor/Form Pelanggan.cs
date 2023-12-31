﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rental_Motor
{
    public partial class Form_Pelanggan : Form
    {
        private string stringConnection = "data source =REZHAA\\REZHA;" +
           "database=rentalmotor;user ID=sa; password=123";
        private SqlConnection koneksi;
        private string Nama, Idplgn, NoHp, Alamat;
        public Form_Pelanggan()
        {
            InitializeComponent();
            koneksi = new SqlConnection(stringConnection);
            refreshform();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            new Form_Utama().Show();
        }
        private void DataGridView()
        {
            koneksi.Open();
            string str = "select * from dbo.pelanggan";
            SqlDataAdapter da = new SqlDataAdapter(str, koneksi);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            koneksi.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string dlt = "DELETE FROM pelanggan WHERE id_pelanggan = @id_pelanggan";
            using (SqlConnection conn = new SqlConnection(stringConnection))
            {
                using (SqlCommand cmd = new SqlCommand(dlt, conn))
                {
                    cmd.Parameters.AddWithValue("id_pelanggan", txtPlgn.Text);
                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        MessageBox.Show("Data Berhasil Dihapus");
                        DataGridView();

                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("An Error Occurred: " + ex.Message + ("Error Code: " + ex.Number));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An Error occurred: " + ex.Message);
                    }
                }
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            DataGridView();
            btnOpen.Enabled = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string upd = "UPDATE pelanggan SET id_pelanggan = @id_pelanggan, nama_pelanggan = @nama_pelanggan, no_telp = @no_telp, alamat = @alamat where id_pelanggan = @id_pelanggan";

            using (SqlConnection conn = new SqlConnection(stringConnection))
            {
                using (SqlCommand cmd = new SqlCommand(upd, conn))
                {
                    cmd.Parameters.AddWithValue("id_pelanggan", txtPlgn.Text);
                    cmd.Parameters.AddWithValue("nama_pelanggan", txtNama.Text);
                    cmd.Parameters.AddWithValue("no_telp", txtNoHP.Text);
                    cmd.Parameters.AddWithValue("alamat", txtAlamat.Text);

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        MessageBox.Show("Data Berhasil di Updated");
                        DataGridView();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("An error occured: " + ex.Message + " (Error Code: " + ex.Number + ")");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occured: " + ex.Message);
                    }
                }
            }
        }

        private void refreshform()
        {
            txtNama.Text = "";
            txtPlgn.Text = "";
            txtNoHP.Text = "";
            txtAlamat.Text = "";
            txtNama.Enabled = true;
            txtPlgn.Enabled = true;
            txtNoHP.Enabled = true;
            txtAlamat.Enabled = true;
            btnAdd.Enabled = true;
            btnDelete.Enabled = true;
        }
        private void Form_Pelanggan_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Nama = txtNama.Text;
            Idplgn = txtPlgn.Text;
            NoHp = txtNoHP.Text;
            Alamat = txtAlamat.Text;

            koneksi.Open();
            string str = "insert into dbo.pelanggan (id_pelanggan, nama_pelanggan, no_telp , alamat)" + "values(@id_pelanggan, @nama_pelanggan, @no_telp , @alamat)";
            SqlCommand cmd = new SqlCommand(str, koneksi);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter("id_pelanggan", Idplgn));
            cmd.Parameters.Add(new SqlParameter("nama_pelanggan", Nama));
            cmd.Parameters.Add(new SqlParameter("no_telp", NoHp));
            cmd.Parameters.Add(new SqlParameter("alamat", Alamat));

            cmd.ExecuteNonQuery();
            koneksi.Close();

            MessageBox.Show("data berhasil disimpan", "sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

            refreshform();
        }
    }
}
