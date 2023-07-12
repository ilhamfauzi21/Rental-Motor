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

namespace Rental_Motor
{
    public partial class Jaminan : Form
    {
        private string stringConnection = "data source =REZHAA\\REZHA;" +
           "database=rentalmotor;user ID=sa; password=123";
        private SqlConnection koneksi;
        private string id_jaminan, jenis_jaminan, bukti_jaminan;
        public Jaminan()
        {
            InitializeComponent();
            koneksi = new SqlConnection(stringConnection);
            refreshform();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            id_jaminan = txtJaminan.Text;
            jenis_jaminan = textBox1.Text;
            bukti_jaminan = txtBukti.Text;

            koneksi.Open();
            string str = "insert into dbo.jaminan (id_jaminan, jenis_jaminan, bukti_jaminan)" + "values(@idj, @jjmn, @bkjmn)";
            SqlCommand cmd = new SqlCommand(str, koneksi);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter("idj", id_jaminan));
            cmd.Parameters.Add(new SqlParameter("jjmn", jenis_jaminan));
            cmd.Parameters.Add(new SqlParameter("bkjmn", bukti_jaminan));



            cmd.ExecuteNonQuery();
            koneksi.Close();

            MessageBox.Show("data berhasil disimpan", "sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

            refreshform();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            DataGridView();
            btnOpen.Enabled = false;
        }

        private void DataGridView()
        {
            koneksi.Open();
            string str = "select * from dbo.jaminan";
            SqlDataAdapter da = new SqlDataAdapter(str, koneksi);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            koneksi.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string dlt = "DELETE FROM jaminan WHERE id_jaminan = @id_jaminan";
            using (SqlConnection conn = new SqlConnection(stringConnection))
            {
                using (SqlCommand cmd = new SqlCommand(dlt, conn))
                {
                    cmd.Parameters.AddWithValue("id_jaminan", txtJaminan.Text);
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string upd = "UPDATE jaminan SET id_jaminan = @id_jaminan, jenis_jaminan = @jenis_jaminan, bukti_jaminan = @bukti_jaminan where id_jaminan = @id_jaminan";

            using (SqlConnection conn = new SqlConnection(stringConnection))
            {
                using (SqlCommand cmd = new SqlCommand(upd, conn))
                {
                    cmd.Parameters.AddWithValue("id_jaminan", txtJaminan.Text);
                    cmd.Parameters.AddWithValue("jenis_jaminan", textBox1.Text);
                    cmd.Parameters.AddWithValue("bukti_jaminan", txtBukti.Text);
                  

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

        private void refreshform()
        {
            txtJaminan.Text = "";
            textBox1.Text = "";
            txtBukti.Text = "";
            txtJaminan.Enabled = true;
            textBox1.Enabled = true;
            txtBukti.Enabled = true;    
            btnAdd.Enabled = true;
            btnDelete.Enabled = true;
        }
        private void Jaminan_Load(object sender, EventArgs e)
        {

        }
    }
}
