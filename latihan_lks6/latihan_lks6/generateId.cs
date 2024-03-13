using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace latihan_lks6
{
    static class generateId
    {
        private static SqlConnection conn = connection.Connect();
        private static SqlCommand cmd;
        private static SqlDataReader rd;

        public static string IdBarang()
        {
            string hasil;
            cmd = new SqlCommand("SELECT id_barang FROM [tbl_barang] ORDER BY id_barang DESC", conn);
            rd = cmd.ExecuteReader();
            rd.Read();
            if(rd.HasRows)
            {
                
                int urut = int.Parse(rd["id_barang"].ToString().Substring(4)) + 1;
                hasil = "BRG" + urut.ToString("000");
            }
            else
            {
                hasil = "BRG0001";
            }
            return hasil;
        }

        public static string IdTransaksi()
        {
            string hasil;
            conn.Open();
            cmd = new SqlCommand("SELECT id_transaksi FROM [tbl_transaksi] ORDER BY id_transaksi DESC", conn);
            rd = cmd.ExecuteReader();
            rd.Read();
            if (rd.HasRows)
            {

                int urut = int.Parse(rd["id_transaksi"].ToString().Substring(4)) + 1;
                hasil = "TRS" + urut.ToString("000");
            }
            else
            {
                hasil = "TRS0001";
            }
            conn.Close();
            return hasil;
        }

    }
}
