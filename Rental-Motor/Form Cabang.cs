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
    public partial class Form_Cabang : Form
    {
        private string stringConnection = "Data Source=REZHAA\\REZHA;" + "database=rentalmotor;User ID=sa;Password=123";
        private SqlConnection koneksi;
        private string IdCbn, Idmtr, idpplgn, idadmin;
        
        public Form_Cabang()
        {
            InitializeComponent();
            koneksi = new SqlConnection(stringConnection);
            refreshform();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataGridView();
            btnOpen.Enabled = true;
        }

        private void btnDlt_Click(object sender, EventArgs e)
        {
            string dlt = "DELETE FROM cabang WHERE id_cabang = @id_cabang";
            using (SqlConnection conn = new SqlConnection(stringConnection))
            {
                using (SqlCommand cmd = new SqlCommand(dlt, conn))
                {
                    cmd.Parameters.AddWithValue("id_cabang", txtCabang.Text);
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

        private void refreshform()
        {
            txtCabang.Text = "";
            txtMotor.Text = "";
            txtpelanggan.Text = "";
            cbxAdmin.Enabled = true;
            txtCabang.Enabled = true;
            txtMotor.Enabled = true;
            txtpelanggan.Enabled = true;
            btnAdd.Enabled = true;
            btnDlt.Enabled = true;
            idAdmincbx();
            
        }

        private void Form_Cabang_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'rentalmotorDataSet1.admin' table. You can move, or remove it, as needed.
            this.adminTableAdapter1.Fill(this.rentalmotorDataSet1.admin);
            // TODO: This line of code loads data into the 'rentalmotorDataSet.admin' table. You can move, or remove it, as needed.
            this.adminTableAdapter.Fill(this.rentalmotorDataSet.admin);

        }
        private void DataGridView()
        {
            koneksi.Open();
            string str = "select * from dbo.cabang";
            SqlDataAdapter da = new SqlDataAdapter(str, koneksi);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            koneksi.Close();
        }

       


        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.adminTableAdapter.FillBy(this.rentalmotorDataSet.admin);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void fillByToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnUpd_Click(object sender, EventArgs e)
        {
            string upd = "UPDATE cabang SET id_cabang = @id_cabang, id_motor = @id_motor, id_pelanggan = @id_pelanggan where id_cabang = @id_cabang";

            using (SqlConnection conn = new SqlConnection(stringConnection))
            {
                using (SqlCommand cmd = new SqlCommand(upd, conn))
                {
                    cmd.Parameters.AddWithValue("id_cabang", txtCabang.Text);
                    cmd.Parameters.AddWithValue("id_motor", txtMotor.Text);
                    cmd.Parameters.AddWithValue("id_pelanggan", txtpelanggan.Text);


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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            IdCbn = txtCabang.Text;
            Idmtr = txtMotor.Text;
            idpplgn = txtpelanggan.Text;
            idadmin = cbxAdmin.Text;
            idAdmincbx();
            if (IdCbn == "" || Idmtr == "" || idpplgn == "" || idadmin == "")
            {
                MessageBox.Show("Masukkan Semuannya", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                koneksi.Open();
                string str = "insert into dbo.cabang (id_cabang, id_motor, id_pelanggan, id_admin) VALUES (@id_cabang, @id_motor, @id_pelanggan, @id_admin)";
                SqlCommand cmd = new SqlCommand(str, koneksi);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@id_cabang", IdCbn));
                cmd.Parameters.Add(new SqlParameter("@id_motor", Idmtr));
                cmd.Parameters.Add(new SqlParameter("@id_pelanggan", idpplgn));
                cmd.Parameters.Add(new SqlParameter("@id_admin", idadmin));


                cmd.ExecuteNonQuery();
                MessageBox.Show("Data Berhasil Disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                koneksi.Close();
                DataGridView();
                refreshform();
            }
        }
        private void idAdmincbx()
        {
            koneksi.Open();
            string str = "select nama_admin from dbo.admin";
            SqlCommand cmd = new SqlCommand(str, koneksi);
            SqlDataAdapter da = new SqlDataAdapter(str, koneksi);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cmd.ExecuteReader();
            koneksi.Close();
            cbxAdmin.DisplayMember = "id_admin";
            cbxAdmin.ValueMember = "id_admin";
            cbxAdmin.DataSource = ds.Tables[0];
        }
    }
}
