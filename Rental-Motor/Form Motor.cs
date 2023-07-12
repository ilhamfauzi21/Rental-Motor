using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Rental_Motor
{
    public partial class Form_Motor : Form
    {
        private string stringConnection = "data source =REZHAA\\REZHA;" +
           "database=rentalmotor;user ID=sa; password=123";
        private SqlConnection koneksi;
        private string id_motor, merk_motor, id_cabang;
        public Form_Motor()
        {
            InitializeComponent();
            koneksi = new SqlConnection(stringConnection);
            refreshform();
        }

        private void refreshform()
        {
            txtidmotor.Text = "";
            txtmerk.Text = "";
            cbxcabang.Text = "";
            txtidmotor.Enabled = true;
            txtmerk.Enabled = true;
            cbxcabang.Enabled = true;
            btnInsert.Enabled = true;
            btndelete.Enabled = true;
        }

        private void DataGridView()
        {
            koneksi.Open();
            string str = "select * from dbo.motor";
            SqlDataAdapter da = new SqlDataAdapter(str, koneksi);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            koneksi.Close();
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            string dlt = "DELETE FROM motor WHERE id_motor = @id_motor";
            using (SqlConnection conn = new SqlConnection(stringConnection))
            {
                using (SqlCommand cmd = new SqlCommand(dlt, conn))
                {
                    cmd.Parameters.AddWithValue("id_motor", txtidmotor.Text);
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

        private void btnupdate_Click(object sender, EventArgs e)
        {
            string upd = "UPDATE motor SET id_motor = @id_motor, merk_motor = @merk_motor, id_cabang = @id_cabang where id_motor = @id_motor";

            using (SqlConnection conn = new SqlConnection(stringConnection))
            {
                using (SqlCommand cmd = new SqlCommand(upd, conn))
                {
                    cmd.Parameters.AddWithValue("id_motor", txtidmotor.Text);
                    cmd.Parameters.AddWithValue("merk_motor", txtmerk.Text);
                    cmd.Parameters.AddWithValue("id_cabang", cbxcabang.Text);


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

        private void btnBack_Click(object sender, EventArgs e)
        {
            new Form_Utama().Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            id_motor = txtidmotor.Text;
            merk_motor = txtmerk.Text;
            id_cabang = cbxcabang.Text;
            idcabang();
            if (id_motor == "" || merk_motor == "" || id_cabang == "" )
            {
                MessageBox.Show("Masukkan Semuannya", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                koneksi.Open();
                string str = "insert into dbo.motor (id_motor, merk_motor, id_cabang) VALUES (@id_motor, @merk_motor, @id_cabang)";
                SqlCommand cmd = new SqlCommand(str, koneksi);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@id_motor", id_motor));
                cmd.Parameters.Add(new SqlParameter("@merk_motor", merk_motor));
                cmd.Parameters.Add(new SqlParameter("@id_cabang", id_cabang));
                

                cmd.ExecuteNonQuery();
                MessageBox.Show("Data Berhasil Disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                koneksi.Close();
                DataGridView();
                refreshform();
            }
        }
        private void idcabang() 
        {
            koneksi.Open();
            string str = "select id_cabang from dbo.cabang";
            SqlCommand cmd = new SqlCommand(str, koneksi);
            SqlDataAdapter da = new SqlDataAdapter(str, koneksi);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cmd.ExecuteReader();
            koneksi.Close();
            cbxcabang.DisplayMember = "id_cabang";
            cbxcabang.ValueMember = "id_cabang";
            cbxcabang.DataSource = ds.Tables[0];
        }
        private void btnopen_Click(object sender, EventArgs e)
        {
            DataGridView();
            btnInsert.Enabled = true;
        }
    }
}
