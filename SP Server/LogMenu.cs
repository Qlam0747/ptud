using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SP_Server
{
    public partial class LogMenu : Form
    {
        public LogMenu()
        {
            InitializeComponent();
        }

        private void Log_Load(object sender, EventArgs e)
        {
            rtbxLog.Text = string.Empty;
        }
    }
}
