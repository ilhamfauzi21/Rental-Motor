using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Rental_Motor
{
    public partial class Form_Admin : Form
    {
        private string stringConnection = "data source =REZHAA\\REZHA;" +
           "database=rentalmotor;user ID=sa; password=123";
        private SqlConnection koneksi;
        private string admin, nama;

        public Form_Admin()
        {
            InitializeComponent();
            koneksi = new SqlConnection(stringConnection);
            refreshform();
        }
        private void refreshform()
        {
            txtadmin.Text = "";
            txtnama.Text = "";
            txtadmin.Enabled = true;
            txtnama.Enabled = true;
            btnAdd.Enabled = true;
            BtnDelete.Enabled = true;
        }
        private void datagridview()
        {
            koneksi.Open();
            string str = "select id_admin, nama_admin from dbo.admin";
            SqlDataAdapter da = new SqlDataAdapter(str, koneksi);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            koneksi.Close();
        }

        private void Form_Admin_Load(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            new Form_Utama().Show();
        }

        private void BtnOpen_Click(object sender, EventArgs e)
        {
            datagridview();
            BtnOpen.Enabled = true;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            string dlt = "DELETE FROM admin WHERE id_admin = @id_admin";
            using (SqlConnection conn = new SqlConnection(stringConnection))
            {
                using (SqlCommand cmd = new SqlCommand(dlt, conn))
                {
                    cmd.Parameters.AddWithValue("id_admin", txtadmin.Text);
                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        MessageBox.Show("Data Berhasil Dihapus");
                        datagridview();

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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string upd = "UPDATE admin SET id_admin = @id_admin, nama_admin = @nama_admin where id_admin = @id_admin";

            using (SqlConnection conn = new SqlConnection(stringConnection))
            {
                using (SqlCommand cmd = new SqlCommand(upd, conn))
                {
                    cmd.Parameters.AddWithValue("id_admin", txtadmin.Text);
                    cmd.Parameters.AddWithValue("nama_admin", txtnama.Text);
                    


                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        MessageBox.Show("Data Berhasil di Updated");
                        datagridview();
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            
            admin = txtadmin.Text;
            nama = txtnama.Text;

            koneksi.Open();
            string str = "insert into dbo.admin (id_admin, nama_admin)" + "values(@id_admin, @nama_admin)";
            SqlCommand cmd = new SqlCommand(str, koneksi);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter("id_admin", admin));
            cmd.Parameters.Add(new SqlParameter("nama_admin", nama));

            cmd.ExecuteNonQuery();
            koneksi.Close();

            MessageBox.Show("data berhasil disimpan", "sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

            refreshform();
        }
    }
}
