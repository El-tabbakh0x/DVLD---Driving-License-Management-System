using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Presentation.People
{
    public partial class frmPersonCard : Form
    {
        public frmPersonCard( int PersonId)
        {
            InitializeComponent();
            ctrlPersonInfo1.LoadPersonInfo(PersonId);
        }
        public frmPersonCard(string NationalNo)
        {
            InitializeComponent();
            ctrlPersonInfo1.LoadPersonInfo(NationalNo);
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
