using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel
{
    public partial class MainLogin : Form
    {
        public MainLogin()
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
            textBoxPassword.UseSystemPasswordChar = true;
        }
        private void MainLogin_Load(object sender, EventArgs e)
        {
            onload();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are You Sure To Exit?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                Application.Exit();
            }
        }
        bool validate()
        {
            if (textBoxUsername.Text == "" || textBoxPassword.Text == "")
            {
                MessageBox.Show("All Field Must Be Filled", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if(textBoxPassword.TextLength < 8)
            {
                MessageBox.Show("Password Must Be At Least 8 Character", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(validate())
            {
                string password = Enc.getPass(textBoxPassword.Text);

                try
                {
                    SqlCommand cmd = new SqlCommand("select ID, Name, jobID from Employee where username=@username and password=@password", Connection.conn);
                    cmd.Parameters.AddWithValue("username", textBoxUsername.Text);
                    cmd.Parameters.AddWithValue("password", password);

                    Connection.conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    if (reader.HasRows)
                    {
                        Model.id = reader.GetInt32(0);
                        Model.name = reader.GetString(1);
                        Model.job = reader.GetInt32(2);

                        if(Model.job == 1)
                        {
                            Connection.conn.Close();

                            this.Hide();
                            MainAdmin admin = new MainAdmin();
                            admin.Show();
                        }

                        if(Model.job == 2)
                        {
                            Connection.conn.Close();

                            this.Hide();    
                            MainEmployee employee = new MainEmployee();
                            employee.Show();
                        }
                    }
                    else
                    {
                        Connection.conn.Close();
                        MessageBox.Show("Username Or Password Uncorrectly", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch
                {
                    throw;
                }
            }
        }

        private void textBoxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void textBoxUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }
    }
}
