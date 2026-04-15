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

namespace DVLD.Presentation.Controls.Person
{
    public partial class ctrlPersonInfoWithFilter : UserControl
    {
        public event Action<int> OnPersonSelected;

        protected virtual void PersonSelected(int PersonID)
        {
            Action<int> handler = OnPersonSelected;
            if (handler != null)
            {
                handler(PersonID); 
            }
        }
        private int _PersonID;
        public int PersonID
        {
            get { return ctrlPersonInfo1.PersonID; }
        }
        public clsPerson SelectedPersonInfo
        {
            get { return ctrlPersonInfo1.SelectedPersonInfo; }
        }
        
        public ctrlPersonInfoWithFilter()
        {
            InitializeComponent();
        }

        //=======================
        // handeling Componanse
        //=======================
        private bool _Filtering = true;
        public bool Filtering
        {
            get { return _Filtering; }
            set {
                _Filtering = value;
                gbFilters.Enabled = _Filtering;
            }
        }

        private bool _UseAddPerson = true;
        public bool UseAddPerson
        {
            get { return _UseAddPerson; }
            set
            {
                _UseAddPerson = value;
                gbFilters.Enabled = _UseAddPerson;
            }
        }
        //=======================
        //
        //=======================
        private void FindProcess()
        {
            switch (cbFilterBy.Text)
            {
                case "Person ID":
                    ctrlPersonInfo1.LoadPersonInfo(int.Parse(txtFilterValue.Text));

                    break;

                case "National No.":
                    ctrlPersonInfo1.LoadPersonInfo(txtFilterValue.Text);
                    break;

                default:
                    break;
            }
            if (OnPersonSelected != null && Filtering)
                OnPersonSelected(ctrlPersonInfo1.PersonID);
        }
        public void LoadData(int PersonId)
        {
            cbFilterBy.SelectedIndex = 1;
            txtFilterValue.Text = PersonId.ToString();
            FindProcess();
        }
        public void FilterFocus()
        {
            txtFilterValue.Focus();
        }
        //=======================
        // Form Event
        //=======================
        private void DataBackEvent(object sender, int PersonID)
        {
            // Handle the data received

            cbFilterBy.SelectedIndex = 1;
            txtFilterValue.Text = PersonID.ToString();
            ctrlPersonInfo1.LoadPersonInfo(PersonID);
        }
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Text = "";
            txtFilterValue.Focus();
        }
        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the pressed key is Enter (character code 13)
            if (e.KeyChar == (char)13)
            {

                btnFind.PerformClick();
            }

            //this will allow only digits if person id is selected
            if (cbFilterBy.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            FindProcess();
        }

        private void ctrlPersonInfoWithFilter_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Focus();
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPersonInfo frm1 = new frmAddEditPersonInfo();
            frm1.DataBack += DataBackEvent; // Subscribe to the event
            frm1.ShowDialog();
        }

        private void txtFilterValue_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtFilterValue.Text.Trim()))
    {
                e.Cancel = true;
                errorProvider1.SetError(txtFilterValue, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(txtFilterValue, null);
            }
        }

       
    }
}

