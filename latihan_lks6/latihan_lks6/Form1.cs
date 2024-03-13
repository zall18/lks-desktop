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
    public partial class Form1 : Form
    {
        SqlConnection conn = connection.Connect();
        SqlCommand cmd;
        SqlDataReader reader;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void login_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM [tbl_user] WHERE username = @username AND password = @password";
            cmd.Parameters.AddWithValue("@username", username.Text);
            cmd.Parameters.AddWithValue("@password", hash.hasGenerate(password.Text));
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                String nama_user = reader["nama"].ToString();
                int id_user = (int)reader["id_user"];
                String tipe = reader["tipe_user"].ToString();
                session.session_start(nama_user, id_user);
                conn.Close();

                conn.Open();
                SqlCommand log = conn.CreateCommand();
                log.CommandType = CommandType.Text;
                log.CommandText = "INSERT INTO [tbl_log] VALUES (@waktu, 'LOGIN', @id)";
                log.Parameters.AddWithValue("@waktu", DateTime.Now);
                log.Parameters.AddWithValue("@id", id_user);
                log.ExecuteNonQuery();
                MessageBox.Show("Berhasil Login!", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conn.Close();

                if(tipe == "admin")
                {
                    this.Hide();
                    admin_log admin = new admin_log();
                    admin.Show();
                }else if(tipe == "gudang")
                {
                    this.Hide();
                    gudang g = new gudang();
                    g.Show();
                }else if(tipe == "kasir")
                {
                    this.Hide();
                    kasir k = new kasir();
                    k.Show();
                }
            }
            else
            {
                MessageBox.Show("gagal Login!", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conn.Close();
            }

        }

        private void reset_Click(object sender, EventArgs e)
        {
            username.Text = null;
            password.Text = null;
        }
    }
}
