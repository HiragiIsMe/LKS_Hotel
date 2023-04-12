using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel
{
    public partial class AddCustomer : Form
    {
        public AddCustomer()
        {
            InitializeComponent();
        }
        void onload()
        {
            ControlBox = false;
            MinimizeBox = false;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.None;
        }
        void loadGender()
        {
            Dictionary<int, string> gender = new Dictionary<int, string>();
            gender.Add(1, "Male");
            gender.Add(2, "Female");
            comboBoxGender.DataSource = new BindingSource(gender, null);
            comboBoxGender.DisplayMember = "Value";
            comboBoxGender.ValueMember = "Key";
        }
        private void AddCustomer_Load(object sender, EventArgs e)
        {
            onload();
            loadGender();
        }

        private void textBoxNIK_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
