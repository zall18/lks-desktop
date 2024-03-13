using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace latihan_lks6
{
    static class session
    {
        public static string nama_user = null;
        public static int id_user = 0;

        public static void session_start(string u, int id)
        {
            nama_user = u;
            id_user = id;
        }
        
    }
}
