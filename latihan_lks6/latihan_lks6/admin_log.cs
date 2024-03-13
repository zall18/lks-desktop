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
    public partial class admin_log : Form
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

            if (dt.Rows.Count < 1)
            {
                MessageBox.Show("Data Kosong!", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                guna2DataGridView1.DataSource = dt;
            }
            conn.Close();

        }
        public admin_log()
        {
            InitializeComponent();
            tabel();
        }

        private void filter_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "SELECT l.id_log as ID_log, u.username AS Username, l.waktu AS Waktu, l.aktifitas AS Aktifitas FROM [tbl_log] l INNER JOIN [tbl_user] u ON l.id_user = u.id_user WHERE l.waktu = @waktu";
            cmd.Parameters.AddWithValue("@waktu", date.Value.ToString());
            dt = new DataTable();
            dr = new SqlDataAdapter(cmd);
            dr.Fill(dt);
            if(dt.Rows.Count < 1)
            {
                MessageBox.Show("Data Kosong!", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            guna2DataGridView1.DataSource = dt;
            conn.Close();

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            UserPage.TopLevel = false;
            guna2Panel1.Controls.Add(UserPage);
            UserPage.BringToFront();
            UserPage.Show();
            LaporPage.Hide();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand log = new SqlCommand("INSERT INTO [tbl_log] VALUES (@waktu, 'LOGOUT', @id)", conn);
            log.Parameters.AddWithValue("@waktu", DateTime.Now);
            log.Parameters.AddWithValue("@id", session.id_user.ToString());
            log.ExecuteNonQuery();
            Form1 login = new Form1();
            login.Show();
            this.Close();
            conn.Close();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            LaporPage.TopLevel = false;
            guna2Panel1.Controls.Add(LaporPage);
            LaporPage.BringToFront();
            LaporPage.Show();
            UserPage.Hide();
        }
    }
}
