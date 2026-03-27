using DVLD.Business.GlobalClasses;
using DVLD.Presentation.Application.ApplicationType;
using DVLD.Presentation.Login;
using DVLD.Presentation.People;
using DVLD.Presentation.Tests.TestTypes;
using DVLD.Presentation.User;
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
        private frmLogin _frmLogin;
        public frmMain(frmLogin frmLogin)
        {
            InitializeComponent();
            _frmLogin = frmLogin;
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

        private void tsmUsers_Click(object sender, EventArgs e)
        {
            Form frm1 = new frmManageUsers();
            frm1.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser = null;
            _frmLogin.Show();
            this.Close();
        }

        private void currToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserCardInfo frm = new frmUserCardInfo(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
           // lblLoggedInUser.Text = "LoggedIn User: " + clsGlobal.CurrentUser.UserName;
            this.Refresh();
        }

        private void manageApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void manageApplicationsTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageApplicationTypes frm = new frmManageApplicationTypes();
            frm.ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageTestTypes frm = new frmManageTestTypes();
            frm.ShowDialog();
        }
    }
}
