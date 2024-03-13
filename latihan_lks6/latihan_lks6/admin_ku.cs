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
    public partial class admin_ku : Form
    {
        SqlConnection conn = connection.Connect();
        SqlCommand cmd;
        SqlDataAdapter dr;
        SqlDataReader rd;
        DataTable dt;

        public void tabel()
        {
            conn.Open();
            cmd = new SqlCommand("SELECT * FROM [tbl_user]", conn);
            dr = new SqlDataAdapter(cmd);
            dt = new DataTable();
            dr.Fill(dt);
            guna2DataGridView1.DataSource = dt;
            conn.Close();
        }

        public void clear()
        {
            tipe.Text = null;
            nama.Text = null;
            alamat.Text = null;
            telpon.Text = null;
            username.Text = null;
            password.Text = null;
            tambah.Enabled = true;
            edit.Enabled = false;
            hapus.Enabled = false;
        }

        public admin_ku()
        {
            InitializeComponent();
            tabel();
            edit.Enabled = false;
            hapus.Enabled = false;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if(tipe.Text.Length == 0)
            {
                MessageBox.Show("Tipe User Kosong!", "NULL", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }else if(nama.Text.Length == 0)
            {
                MessageBox.Show("Nama Kosong!", "NULL", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }else if(alamat.Text.Length == 0)
            {
                MessageBox.Show("Alamat Kosong!", "NULL", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }else if (telpon.Text.Length == 0)
            {
                MessageBox.Show("telpon Kosong!", "NULL", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }else if (username.Text.Length == 0)
            {
                MessageBox.Show("Username Kosong!", "NULL", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }else if(password.Text.Length == 0)
            {
                MessageBox.Show("Passwird Kosong!", "NULL", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                conn.Open();
                cmd = new SqlCommand("INSERT INTO [tbl_user] VALUES (@tipe, @nama, @alamat, @telpon, @username, @password)", conn);
                cmd.Parameters.AddWithValue("@tipe", tipe.Text);
                cmd.Parameters.AddWithValue("@nama", nama.Text);
                cmd.Parameters.AddWithValue("@alamat", alamat.Text);
                cmd.Parameters.AddWithValue("@telpon", telpon.Text);
                cmd.Parameters.AddWithValue("@username", username.Text);
                cmd.Parameters.AddWithValue("password", hash.hasGenerate(password.Text));
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data berhasil ditambahkan!", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conn.Close();
                tabel();
                clear();
            }


        }

        private void edit_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd = new SqlCommand("UPDATE [tbl_user] SET tipe_user = @tipe, nama = @nama, telpon = @telpon, alamat = @alamat WHERE id_user = @id", conn);
            cmd.Parameters.AddWithValue("@id", id.Text);
            cmd.Parameters.AddWithValue("@tipe", tipe.Text);
            cmd.Parameters.AddWithValue("@nama", nama.Text);
            cmd.Parameters.AddWithValue("@alamat", alamat.Text);
            cmd.Parameters.AddWithValue("@telpon", telpon.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Data berhasil diubah!", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            conn.Close();
            tabel();
            clear();
        }

        private void hapus_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Yakin hapus data ini?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                conn.Open();
                cmd = new SqlCommand("DELETE FROM [tbl_user] WHERE id_user = @id", conn);
                cmd.Parameters.AddWithValue("@id", id.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data berhasil dihapus!", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conn.Close();
                tabel();
                clear();
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow data = this.guna2DataGridView1.Rows[e.RowIndex];

                tipe.Text = data.Cells["tipe_user"].Value.ToString();
                nama.Text = data.Cells["nama"].Value.ToString();
                alamat.Text = data.Cells["alamat"].Value.ToString();
                telpon.Text = data.Cells["telpon"].Value.ToString();
                username.Text = data.Cells["username"].Value.ToString();
                id.Text = data.Cells["id_user"].ToString();

                edit.Enabled = true;
                hapus.Enabled = true;
                tambah.Enabled = false;
            }
        }

        private void search_TextChanged(object sender, EventArgs e)
        {
            conn.Open();
            cmd = new SqlCommand("SELECT * FROM [tbl_user] WHERE nama LIKE '%"+this.search.Text+"%' OR username LIKE '%"+this.search.Text+"%' ", conn);
            dr = new SqlDataAdapter(cmd);
            dt = new DataTable();
            dr.Fill(dt);
            guna2DataGridView1.DataSource = dt;
            conn.Close();
        }
    }
}
