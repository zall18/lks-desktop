using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace latihan_lks6
{
    public partial class Admin_kl : Form
    {
        SqlConnection conn = connection.Connect();
        SqlCommand cmd;
        SqlDataAdapter dr;
        DataTable dt;
        SqlDataReader rd;
        public Admin_kl()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd = new SqlCommand("SELECT * FROM [tbl_transaksi] WHERE tgl_transaksi BETWEEN '" + awal.Text + "' AND '" + akhir.Text + "'", conn);
            rd = cmd.ExecuteReader();
            if (rd.HasRows)
            {
                conn.Close();
                dr = new SqlDataAdapter(cmd);
                dt = new DataTable();
                dr.Fill(dt);
                guna2DataGridView1.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Data kosong!");
                conn.Close();
            }

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd = new SqlCommand("SELECT tgl_transaksi AS date, SUM(tbl_transaksi.total_bayar) AS harga FROM [tbl_transaksi] WHERE tgl_transaksi BETWEEN '" + awal.Value + "' AND '" + akhir.Value + "' GROUP BY tgl_transaksi", conn);
            rd = cmd.ExecuteReader();
            int i = 0;
            while(rd.Read())
            {
                string date = rd["date"].ToString();
                double harga = Convert.ToDouble(rd["harga"]);

                chart1.Series[i].Points.AddXY(date, harga);
             
            }
            conn.Close ();
        }
    }
}
