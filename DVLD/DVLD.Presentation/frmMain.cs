using DVLD.Business.GlobalClasses;
using DVLD.Presentation.Application.ApplicationType;
using DVLD.Presentation.Applications.International_License;
using DVLD.Presentation.Applications.Local_Driving_License;
using DVLD.Presentation.Applications.Renew_Local_License;
using DVLD.Presentation.Applications.ReplaceLostOrDamagedLicense;
using DVLD.Presentation.Applications.Rlease_Detained_License;
using DVLD.Presentation.Drivers;
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
            frmAddUpdateLocalDrivingLicesnseApplication frm = new frmAddUpdateLocalDrivingLicesnseApplication();
            frm.ShowDialog();
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManagementLocalDrivingLicesnseApplications frm = new frmManagementLocalDrivingLicesnseApplications();
            frm.ShowDialog();
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

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication frm = new frmNewInternationalLicenseApplication();
            frm.ShowDialog();
        }

        private void newDrivingLicenswToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmRenewLocalDrivingLicenseApplication frm = new frmRenewLocalDrivingLicenseApplication();
            frm.ShowDialog();

        }

        private void releaseDetainedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication();
            frm.ShowDialog();
        }
        private void replacementForLostOrDemagedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReplaceLostOrDamagedLicenseApplication frm = new frmReplaceLostOrDamagedLicenseApplication();
            frm.ShowDialog();
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmManagementLocalDrivingLicesnseApplications frm = new frmManagementLocalDrivingLicesnseApplications();
            frm.ShowDialog();
        }

        private void internationalLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListInternationalLicesnseApplications frm = new frmListInternationalLicesnseApplications();
            frm.ShowDialog();
        }

        private void tsmDrivers_Click(object sender, EventArgs e)
        {
            frmManagementDrivers frm = new frmManagementDrivers();
            frm.ShowDialog();

        }

        private void datlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDetainedLicenses frm = new frmListDetainedLicenses();
            frm.ShowDialog();
        }
    }
}
