using DVLD.Presentation.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Presentation
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void tsmPeople_Click(object sender, EventArgs e)
        {
            Form frm1 = new frmManageAndListePeople();
            frm1.ShowDialog();
        }
    }
}
