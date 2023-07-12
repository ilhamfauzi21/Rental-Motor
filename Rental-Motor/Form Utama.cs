using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rental_Motor
{
    public partial class Form_Utama : Form
    {
        public Form_Utama()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Form_Motor().Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Form_Cabang().Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Form_Pelanggan().Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Jaminan().Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new Form_Admin().Show();
        }
    }
}
