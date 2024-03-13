using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace latihan_lks6
{
    public partial class report : Form
    {
        
        public report()
        {
            InitializeComponent();
        }

        private void report_Load(object sender, EventArgs e)
        {
            kasir kasir = new kasir();  
            ReportDocument rpt = new ReportDocument();
            rpt.Load("C:\\Users\\Administrator\\source\\repos\\latihan_lks6\\latihan_lks6\\CrystalReport1.rpt");
            crystalReportViewer1.ReportSource = rpt;
            crystalReportViewer1.SelectionFormula = "{tbl_transaksi.id_transaksi}='" + kasir.idT.Text + "'";
            crystalReportViewer1.RefreshReport();
        }
    }
}
