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
using static Guna.UI2.Native.WinApi;
using static System.Collections.Specialized.BitVector32;

namespace latihan_lks6
{
    public partial class kasir : Form
    {
        string value;
        SqlConnection conn = connection.Connect();
        SqlCommand cmd;
        SqlDataAdapter dr;
        SqlDataReader rd;
        DataTable dt;
        int number = 1;
        

        string nama_barang, kode_barang;
        
        
        public void listMenu()
        {
            conn.Open();
            cmd = new SqlCommand("SELECT * from [tbl_barang]", conn);
            rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                menu.Items.Add(rd["kode_barang"].ToString() + " - " + rd["nama_barang"].ToString());
            }
            conn.Close();

        }

        public void tabel_load()
        {
            guna2DataGridView1.Columns.Clear();
            guna2DataGridView1.ColumnCount = 7;
            guna2DataGridView1.Columns[0].Name = "Id Transaksi";
            guna2DataGridView1.Columns[1].Name = "Kode Barang";
            guna2DataGridView1.Columns[2].Name = "Nama Barang";
            guna2DataGridView1.Columns[3].Name = "Harga Satuan";
            guna2DataGridView1.Columns[4].Name = "Quantitas";
            guna2DataGridView1.Columns[5].Name = "Subtotal";
            guna2DataGridView1.Columns[6].Name = "id barang";
            guna2DataGridView1.Columns[6].Visible = false;
        }

        public kasir()
        {
            InitializeComponent();
            listMenu();
            tabel_load();
            idT.Text = generateId.IdTransaksi();
        }

        private void guna2TextBox3_KeyUp(object sender, KeyEventArgs e)
        {
            value = th.Text.Replace("Rp. ", "");
            value = value.Replace(",00", "");
            value = value.Replace(".", "");
            th.Text = "Rp. " + value.ToString();
            th.SelectionStart = th.Text.Length;
        }

        private void menu_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            string kode = menu.Text.Split('-')[0];
            cmd.CommandText = "SELECT * FROM [tbl_barang] WHERE kode_barang = @kode";
            cmd.Parameters.AddWithValue("@kode", kode);
            rd = cmd.ExecuteReader();
            rd.Read();
            if(rd.HasRows)
            {
                harga.Text = rd["harga_satuan"].ToString();
                idB.Text = rd["id_barang"].ToString();
            }
            conn.Close();


        }

        private void quan_TextChanged(object sender, EventArgs e)
        {
            int hb = int.Parse(harga.Text);
            int q;
            if (int.TryParse(quan.Text, out q))
            {
                int total = hb * q;
                th.Text = total.ToString();
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            int total = int.Parse(tb.Text);
            int bayar;

            if (int.TryParse(ub.Text, out bayar))
            {
                if (bayar < total)
                {

                    MessageBox.Show("Pembayaran Kurang!", "FAILED", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ub.Text = null;
                }
                else
                {
                    foreach (DataGridViewRow row in guna2DataGridView1.Rows)
                    {
                        conn.Open();
                        SqlCommand sql = new SqlCommand("UPDATE [tbl_barang] SET jumlah_barang=jumlah_barang-" + Convert.ToDecimal(row.Cells[4].Value)+" WHERE id_barang = '" + row.Cells[6].Value +"'", conn);
                        sql.ExecuteNonQuery();
                        conn.Close();
                    }
                    MessageBox.Show("Pembayaran Berhasil!", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    int kembali = bayar - total;
                    uk.Text = kembali.ToString();

                    tb.Text = "-";
                    ub.Text = null;
                }
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand det = new SqlCommand("INSERT INTO [tbl_transaksi] VALUES(@id, '1', @tgl, @tot, @ids)", conn);
            det.Parameters.AddWithValue("@id", idT.Text);
            det.Parameters.AddWithValue("@tgl", DateTime.Now);
            det.Parameters.AddWithValue("@tot", tb.Text);
            det.Parameters.AddWithValue("@ids", session.id_user);
            det.ExecuteNonQuery();
            conn.Close();
            for (int i = 0; i < guna2DataGridView1.Rows.Count - 1; i++)
            {
                conn.Open();
                cmd = new SqlCommand("INSERT INTO [tbl_detail] VALUES('"+ guna2DataGridView1.Rows[i].Cells[2].Value + "','"+ guna2DataGridView1.Rows[i].Cells[4].Value + "', '"+ guna2DataGridView1.Rows[i].Cells[6].Value + "', '"+idT.Text+"')", conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                
            }
            MessageBox.Show("Data berhasil disimpan");
        }

        private void reset_Click(object sender, EventArgs e)
        {
            idB.Text = null;
            menu.Text = null;
            harga.Text = null;
            quan.Text = null;
            th.Text = null;
            uk.Text = "-";
            ub.Text = null;
            tb.Text = "-";
            guna2DataGridView1.Columns.Clear();

         

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            report report = new report();
            report.Show();
        }

        private void tambah_Click(object sender, EventArgs e)
        {
            decimal total = 0;
            bool cek = false;
            string id = "TRS00" + number.ToString();
            string[] hasil = menu.Text.Split('-');
            string kb = hasil[0];
            string nb = hasil[1];
/*            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                if (row.Cells[0].Value.ToString() == kb)
                {
                    cek = true;
                }
            }*/
            if(!cek)
            {
                guna2DataGridView1.Rows.Add(1);
                guna2DataGridView1.Rows[guna2DataGridView1.Rows.Count - 2].Cells[0].Value = id;
                guna2DataGridView1.Rows[guna2DataGridView1.Rows.Count - 2].Cells[1].Value = kb;
                guna2DataGridView1.Rows[guna2DataGridView1.Rows.Count - 2].Cells[2].Value = nb;
                guna2DataGridView1.Rows[guna2DataGridView1.Rows.Count - 2].Cells[3].Value = harga.Text;
                guna2DataGridView1.Rows[guna2DataGridView1.Rows.Count - 2].Cells[4].Value = quan.Text;
                guna2DataGridView1.Rows[guna2DataGridView1.Rows.Count - 2].Cells[5].Value = th.Text;
                guna2DataGridView1.Rows[guna2DataGridView1.Rows.Count - 2].Cells[6].Value = idB.Text;
            }
            number = number + 1;

            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    decimal subtotal = Convert.ToDecimal(row.Cells[5].Value);
                    total += subtotal;
                }
            }
            MessageBox.Show("Data ditambahkan");
            
            th.Text = null;
            quan.Text = null;
            tb.Text = total.ToString();
        }
    }
}
