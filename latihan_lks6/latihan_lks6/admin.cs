using Guna.UI2.WinForms;
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

namespace latihan_lks6
{
    public partial class admin : Form
    {
        SqlConnection conn = connection.Connect();
        SqlCommand cmd;
        DataTable dt;
        SqlDataAdapter dr;
        admin_ku UserPage = new admin_ku();
        Admin_kl LaporPage = new Admin_kl();

        public void tabel()
        {
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "SELECT l.id_log as ID_log, u.username AS Username, l.waktu AS Waktu, l.aktifitas AS Aktifitas FROM [tbl_log] l INNER JOIN [tbl_user] u ON l.id_user = u.id_user";
            dt = new DataTable();
            dr = new SqlDataAdapter(cmd);
            dr.Fill(dt);

            if(dt.Rows.Count < 1)
            {
                MessageBox.Show("Data Kosong!", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                guna2DataGridView1.DataSource = dt;
            }
            conn.Close();

        }

        public admin()
        {
            InitializeComponent();
            tabel();
        }

        private void filter_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "SELECT l.id as ID_log, u.username AS Username, l.waktu AS Waktu, l.aktifitas AS Aktifitas FROM [tbl_log] l INNER JOIN [tbl_user] u ON l.id_user = u.id_user WHERE l.waktu = @waktu";
            cmd.Parameters.AddWithValue("@waktu", date);
            dt = new DataTable();
            dr = new SqlDataAdapter(cmd);
            dr.Fill(dt);
            guna2DataGridView1.DataSource = dt;
            conn.Close();

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand log = conn.CreateCommand();
            log.CommandType = CommandType.Text;
            log.CommandText = "INSERT INTO [tbl_log] VALUES (@waktu, 'LOGOUT', @id)";
            log.Parameters.AddWithValue("@waktu", DateTime.Now);
            log.Parameters.AddWithValue("@id", session.id_user);
            log.ExecuteNonQuery();
            MessageBox.Show("Berhasil Login!", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            conn.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }
    }
}
