using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Presentation.User
{
    public partial class frmUserCardInfo : Form
    {
        private int _UserID=-1;
        public frmUserCardInfo(int UserId)
        {
            InitializeComponent();
            _UserID = UserId;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUserCardInfo_Load(object sender, EventArgs e)
        {
            ctrlUserInfo1.LoadUserInfo(_UserID);

        }
    }
}
