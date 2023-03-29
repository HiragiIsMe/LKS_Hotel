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
    public partial class MainAdmin : Form
    {
        public MainAdmin()
        {
            InitializeComponent();
        }
        void onload()
        {
            ControlBox = false;
            MinimizeBox = false;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.None;
            this.CenterToScreen();
            label1Log.Text = Model.name.ToString();
            timer1.Start();
        }
        private void MainAdmin_Load(object sender, EventArgs e)
        {
            onload();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            labelClock.Text = dt.ToString();
        }
        private void employeeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            MasterEmployee form = new MasterEmployee()
            {
                TopLevel = false,
                TopMost = true
            };

            panelMain.Controls.Clear();
            panelMain.Controls.Add(form);
            form.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are You Sure To Exit?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are You Sure To Logout?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                this.Close();
                MainLogin login = new MainLogin();
                login.Show();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are You Sure To Exit?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void roomTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MasterRoomType form = new MasterRoomType()
            {
                TopLevel = false,
                TopMost = true
            };
            panelMain.Controls.Clear();
            panelMain.Controls.Add(form);
            form.Show();
        }

        private void foodAndDrinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MasterFoodNDrink form = new MasterFoodNDrink()
            {
                TopLevel = false,
                TopMost = true
            };
            panelMain.Controls.Clear();
            panelMain.Controls.Add(form);
            form.Show();
        }

        private void itemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MasterItem form = new MasterItem()
            {
                TopLevel = false,
                TopMost = true
            };
            panelMain.Controls.Clear();
            panelMain.Controls.Add(form);
            form.Show();
        }

        private void roomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MasterRoom form = new MasterRoom()
            {
                TopLevel = false,
                TopMost = true
            };
            panelMain.Controls.Clear();
            panelMain.Controls.Add(form);
            form.Show();
        }
    }
}
