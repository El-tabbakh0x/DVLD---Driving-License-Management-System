using DVLD.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Presentation.Controls.User
{
    public partial class ctrlUserInfo : UserControl
    {
        private int _UserID = -1;
        private clsUser _User;
        public int UserID
        {
            get { return _UserID; }
        }
        public ctrlUserInfo()
        {
            InitializeComponent();
        }

        private void _ResetPersonInfo()
        {

            ctrlPersonInfo1.ResetPersonInfo();
            lblUserID.Text = "????";
            lblUserName.Text = "?????";
            lblIsActive.Text = "????";
        
        }
        private void _FillUserInfo()
        {

            ctrlPersonInfo1.LoadPersonInfo(_User.PersonID);
            lblUserID.Text = _User.UserID.ToString();
            lblUserName.Text = _User.UserName.ToString();

            if (_User.IsActive)
                lblIsActive.Text = "Yes";
            else
                lblIsActive.Text = "No";

        }
        public void LoadUserInfo(int UserId)
        {
            _User = clsUser.FindUserInfoByUserID(UserId);
            if (_User==null)
            {
                _ResetPersonInfo();
                MessageBox.Show("No User with UserID = " + UserID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
