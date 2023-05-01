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
    public partial class MainEmployee : Form
    {
        public MainEmployee()
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
        private void MainEmployee_Load(object sender, EventArgs e)
        {
            onload();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            labelClock.Text = dateTime.ToString();
        }

        private void employeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reservation form = new Reservation()
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
            CheckIn form = new CheckIn()
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
            RequestAddItem form = new RequestAddItem()
            {
                TopLevel = false,
                TopMost = true
            };
            panelMain.Controls.Clear();
            panelMain.Controls.Add(form);
            form.Show();
        }

        private void roomTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckOut form = new CheckOut()
            {
                TopLevel = false,
                TopMost = true
            };
            panelMain.Controls.Clear();
            panelMain.Controls.Add(form);
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are You Sure To Exit?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void checkInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportCheckIn form = new ReportCheckIn()
            {
                TopLevel = false,
                TopMost = true
            };
            panelMain.Controls.Clear();
            panelMain.Controls.Add(form);
            form.Show();
        }

        private void guestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportGuest form = new ReportGuest()
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
