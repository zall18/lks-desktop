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
    public partial class gudang : Form
    {
        SqlConnection conn = connection.Connect();
        SqlCommand cmd;
        SqlDataReader rd;
        SqlDataAdapter dr;
        DataTable dt;

        public void tabel()
        {
            conn.Open();
            cmd = new SqlCommand("SELECT * FROM [tbl_barang]", conn);
            dr = new SqlDataAdapter(cmd);
            dt = new DataTable();
            dr.Fill(dt);
            guna2DataGridView1.DataSource = dt;
            conn.Close();
        }

        public void clear()
        {
            kode.Text = null;
            nama.Text = null;
            exp.Value = DateTime.Now;
            jml.Text = null;
            satuan.Text = null;
            harga.Text = null;
            edit.Enabled = false;
            hapus.Enabled = false;
            tambah.Enabled = true;
        }

        string value;
        public gudang()
        {
            InitializeComponent();
            tabel();
            edit.Enabled = false;
            hapus.Enabled=false;
        }

        private void gudang_Load(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void harga_TextChanged(object sender, EventArgs e)
        {

        }

        private void tambah_Click(object sender, EventArgs e)
        {
            if(kode.Text.Length == 0)
            {
                MessageBox.Show("Kode barang Kosong!", "NULL", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }else if(nama.Text.Length == 0)
            {
                MessageBox.Show("Nama barang Kosong!", "NULL", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }else if(exp.Value == DateTime.Now)
            {
                MessageBox.Show("Expired date belum diubah!", "NULL", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }else if(jml.Text.Length == 0)
            {
                MessageBox.Show("Jumlah barang masih kosong Kosong!", "NULL", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }else if(satuan.Text.Length == 0)
            {
                MessageBox.Show("satuan barang belum dipilih!", "NULL", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }else if(harga.Text.Length == 0)
            {
                MessageBox.Show("Harga Kosong!", "NULL", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                conn.Open();
                cmd = new SqlCommand("INSERT INTO [tbl_barang] VALUES(@id, @kode, @nama, @exp, @jml, @satuan, @harga)", conn);
                cmd.Parameters.AddWithValue("@id", generateId.IdBarang());
                cmd.Parameters.AddWithValue("@kode", kode.Text);
                cmd.Parameters.AddWithValue("@nama", nama.Text);
                cmd.Parameters.AddWithValue("@exp", exp.Value);
                cmd.Parameters.AddWithValue("@jml", jml.Text);
                cmd.Parameters.AddWithValue("@satuan", satuan.Text);
                cmd.Parameters.AddWithValue("@harga", value);
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
            cmd = new SqlCommand("UPDATE [tbl_barang] SET kode_barang = @kode, nama_barang = @nama, expired_date = @exp, jumlah_barang = @jml, satuan = @satuan, harga_satuan = @harga WHERE id_barang = @id", conn);
            cmd.Parameters.AddWithValue("@id", id.Text);
            cmd.Parameters.AddWithValue("@kode", kode.Text);
            cmd.Parameters.AddWithValue("@nama", nama.Text);
            cmd.Parameters.AddWithValue("@exp", exp.Value);
            cmd.Parameters.AddWithValue("@jml", jml.Text);
            cmd.Parameters.AddWithValue("@satuan", satuan.Text);
            cmd.Parameters.AddWithValue("@harga", value);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Data berhasil diubah!", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            conn.Close();
            tabel();
            clear();
        }

        private void hapus_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd = new SqlCommand("DELETE FROM [tbl_barang] WHERE id_barang = @id", conn);
            cmd.Parameters.AddWithValue("@id", id.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Data berhasil diubah!", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            conn.Close();
            tabel();
            clear();
        }

        private void search_TextChanged(object sender, EventArgs e)
        {
            conn.Open();
            cmd = new SqlCommand("SELECT * FROM [tbl_barang] WHERE nama_barang LIKE '%"+this.search.Text+"%' OR kode_barang LIKE '%"+this.search.Text+"%'", conn);
            dr = new SqlDataAdapter(cmd);
            dt = new DataTable();
            dr.Fill(dt);
            guna2DataGridView1.DataSource = dt;
            conn.Close();
        }

        private void harga_KeyUp(object sender, KeyEventArgs e)
        {
            value = harga.Text.Replace("Rp. ", "");
            value = value.Replace(",00", "");
            value = value.Replace(".", "");
            harga.Text = "Rp. " + value.ToString();
            harga.SelectionStart = harga.Text.Length;
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow data = this.guna2DataGridView1.Rows[e.RowIndex];

                id.Text = data.Cells["id_barang"].Value.ToString();
                kode.Text = data.Cells["kode_barang"].Value.ToString();
                nama.Text = data.Cells["nama_barang"].Value.ToString();
                jml.Text = data.Cells["jumlah_barang"].Value.ToString();
                satuan.Text = data.Cells["satuan"].Value.ToString();
                harga.Text = data.Cells["harga_satuan"].Value.ToString();

                harga.Focus();

                edit.Enabled = true;
                hapus.Enabled = true;
                tambah.Enabled = false;
            }
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
    }
}
