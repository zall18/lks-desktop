using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace latihan_lks6
{
    static class connection
    {
        public static SqlConnection conn = null;
        public static string con = "Data Source=DESKTOP-FIK5SPH\\SQLEXPRESS;Initial Catalog=latihan6;Integrated Security=True;MultipleActiveResultSets=True;";

        public static SqlConnection Connect()
        {
            if(conn == null)
            {
                conn = new SqlConnection(con);
                return conn;

            }
            return conn;
        }
    }
}
