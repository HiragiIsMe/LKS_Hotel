using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel
{
    public partial class MasterEmployee : Form
    {
        private int id, Condition;
        public MasterEmployee()
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
        void nonActive()
        {
            textBoxUsername.Enabled = false;
            textBoxPassword.Enabled = false;
            textBoxConf.Enabled = false;
            textBoxName.Enabled = false;
            textBoxEmail.Enabled = false;
            dateTimePicker1.Enabled = false;
            comboBoxJob.Enabled = false;
            richTextBoxAddress.Enabled = false;
            buttonBrow.Enabled = false;
            buttonSav.Enabled = false;
            buttonCan.Enabled = false;
            buttonIn.Enabled = true;
            buttonUp.Enabled = true;
            buttonDel.Enabled = true;
        }
        void Active()
        {
            textBoxUsername.Enabled = true;
            textBoxPassword.Enabled = true;
            textBoxConf.Enabled = true;
            textBoxName.Enabled = true;
            textBoxEmail.Enabled = true;
            dateTimePicker1.Enabled = true;
            comboBoxJob.Enabled = true;
            richTextBoxAddress.Enabled = true;
            buttonBrow.Enabled = true;
            buttonSav.Enabled = true;
            buttonCan.Enabled = true;
            buttonIn.Enabled = false;
            buttonUp.Enabled = false;
            buttonDel.Enabled = false;
        }
        void Clear()
        {
            textBoxUsername.Text = "";
            textBoxPassword.Text = "";
            textBoxConf.Text = "";
            richTextBoxAddress.Text = "";
            textBoxName.Text = "";
            pictureBox1.Image = null;
            textBoxEmail.Text = "";
        }
        void Datagrid()
        {
            string query = "select Employee.*, Job.id, Job.Name as 'Job' from Employee join Job on Employee.jobID = Job.ID";
            dataGridView1.DataSource = Connection.getData(query);
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        void Job()
        {
            string query = "select * from Job";
            comboBoxJob.DataSource = Connection.getData(query);
            comboBoxJob.DisplayMember = "Name";
            comboBoxJob.ValueMember = "ID";
        }
        private void MasterEmployee_Load(object sender, EventArgs e)
        {
            onload();
            Datagrid();
            Job();
            nonActive();
            textBoxPassword.UseSystemPasswordChar = true;
            textBoxConf.UseSystemPasswordChar = true;
        }

        private void buttonBrow_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Images|*.jpg;*.png;*.jpeg;";
            if(open.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(open.FileName);
                Bitmap bmp = (Bitmap)img;
                pictureBox1.Image = bmp;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void buttonIn_Click(object sender, EventArgs e)
        {
            Condition = 1;
            Active();
            Clear();
        }

        private void buttonCan_Click(object sender, EventArgs e)
        {
            Clear();
            id = 0;
            nonActive();
            Datagrid();
        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            if(id > 0)
            {
                Active();
                Condition = 2;
            }
            else
            {
                MessageBox.Show("Please Select One Row To Update", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (id > 0)
            {
                DialogResult result = MessageBox.Show("Are You Sure To Delete " + dataGridView1.SelectedRows[0].Cells[1].Value + " ?", "Alert", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    SqlCommand cmd = new SqlCommand("delete from Employee where id=" + id + "", Connection.conn);
                    Connection.conn.Open();
                    cmd.ExecuteNonQuery();
                    Connection.conn.Close();

                    MessageBox.Show("Data Success Deleted", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    buttonCan.PerformClick();
                }
            }
            else
            {
                MessageBox.Show("Please Select One Row To Delete", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        bool isValidEmail(string email)
        {
            var trimmedEmail = email.Trim();
            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }

            try
            {
                var mail = new MailAddress(email);
                return mail.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
        bool validateIn()
        {
            if(textBoxUsername.Text == "" || textBoxPassword.Text == "" || textBoxConf.Text == "" || textBoxName.Text == "" || dateTimePicker1.Value == null || comboBoxJob.Text == "" || richTextBoxAddress.Text == "" || textBoxEmail.Text == "" || pictureBox1.Image == null)
            {
                MessageBox.Show("All Field Must Be Filled", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            SqlCommand username = new SqlCommand("select * from Employee where username=@username", Connection.conn);
            username.Parameters.AddWithValue("@username", textBoxUsername.Text);
            Connection.conn.Open();
            SqlDataReader reader = username.ExecuteReader();
            reader.Read();
            if (reader.HasRows)
            {
                Connection.conn.Close();
                MessageBox.Show("Username Has Been Used", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            Connection.conn.Close();

            if (textBoxPassword.Text != textBoxConf.Text)
            {
                MessageBox.Show("Password Must Be Same With Confirm Password", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if(textBoxPassword.TextLength < 8)
            {
                MessageBox.Show("Password Must Be At Least 8 Character", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if(isValidEmail(textBoxEmail.Text) == false)
            {
                MessageBox.Show("Email Not Valid", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            SqlCommand email = new SqlCommand("select * from Employee where email=@email",Connection.conn);
            email.Parameters.AddWithValue("@email", textBoxEmail.Text);
            Connection.conn.Open();
            SqlDataReader reader1 = email.ExecuteReader();
            reader1.Read();
            if(reader1.HasRows)
            {
                Connection.conn.Close();
                MessageBox.Show("Email Has Been Used", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            Connection.conn.Close();

            return true;
        }
        bool validateUp()
        {
            if (textBoxUsername.Text == "" || textBoxName.Text == "" || dateTimePicker1.Value == null || comboBoxJob.Text == "" || richTextBoxAddress.Text == "" || textBoxEmail.Text == "" || pictureBox1.Image == null)
            {
                MessageBox.Show("All Field Must Be Filled", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if(textBoxUsername.Text != dataGridView1.SelectedRows[0].Cells[1].Value.ToString())
            {
                SqlCommand username = new SqlCommand("select * from Employee where username=@username", Connection.conn);
                username.Parameters.AddWithValue("@username", textBoxUsername.Text);
                Connection.conn.Open();
                SqlDataReader reader = username.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    Connection.conn.Close();
                    MessageBox.Show("Username Has Been Used", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                Connection.conn.Close();
            }

            if(textBoxPassword.Text != "")
            {
                if (textBoxPassword.Text != textBoxConf.Text)
                {
                    MessageBox.Show("Password Must Be Same With Confirm Password", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (textBoxPassword.TextLength < 8)
                {
                    MessageBox.Show("Password Must Be At Least 8 Character", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            if(textBoxEmail.Text != dataGridView1.SelectedRows[0].Cells[4].Value.ToString())
            {
                if (isValidEmail(textBoxEmail.Text) == false)
                {
                    MessageBox.Show("Email Not Valid", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                SqlCommand email = new SqlCommand("select * from Employee where email=@email", Connection.conn);
                email.Parameters.AddWithValue("@email", textBoxEmail.Text);
                Connection.conn.Open();
                SqlDataReader reader1 = email.ExecuteReader();
                reader1.Read();
                if (reader1.HasRows)
                {
                    Connection.conn.Close();
                    MessageBox.Show("Email Has Been Used", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                Connection.conn.Close();
            }
            return true;
        }
        private void buttonSav_Click(object sender, EventArgs e)
        {
            if(Condition == 1)
            {
                if (validateIn())
                {
                    SqlCommand cmd = new SqlCommand("insert into Employee values(@username,@password,@name,@email,@address,@dob,@jobId,@photo)",Connection.conn);
                    cmd.Parameters.AddWithValue("@username", textBoxUsername.Text);
                    cmd.Parameters.AddWithValue("@password", Enc.getPass(textBoxPassword.Text));
                    cmd.Parameters.AddWithValue("@name", textBoxName.Text);
                    cmd.Parameters.AddWithValue("@email", textBoxEmail.Text);
                    cmd.Parameters.AddWithValue("@address", richTextBoxAddress.Text);
                    cmd.Parameters.AddWithValue("@dob", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@jobId", comboBoxJob.SelectedValue);
                    cmd.Parameters.AddWithValue("@photo", Enc.getImage(pictureBox1.Image));

                    Connection.conn.Open();
                    cmd.ExecuteNonQuery();
                    Connection.conn.Close();

                    MessageBox.Show("Data Success Inserted", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    buttonCan.PerformClick();
                }
            }

            if(Condition == 2)
            {
                if (validateUp())
                {
                    SqlCommand cmd = new SqlCommand("update Employee set Username=@username,Password=@password,Name=@name,Email=@email,Adress=@address,DateOfBirth=@dob,JobID=@jobId,Photo=@photo where ID=" + id + "", Connection.conn);
                    cmd.Parameters.AddWithValue("@username", textBoxUsername.Text);
                    cmd.Parameters.AddWithValue("@password", Enc.getPass(textBoxPassword.Text));
                    cmd.Parameters.AddWithValue("@name", textBoxName.Text);
                    cmd.Parameters.AddWithValue("@email", textBoxEmail.Text);
                    cmd.Parameters.AddWithValue("@address", richTextBoxAddress.Text);
                    cmd.Parameters.AddWithValue("@dob", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@jobId", comboBoxJob.SelectedValue);
                    cmd.Parameters.AddWithValue("@photo", Enc.getImage(pictureBox1.Image));

                    Connection.conn.Open();
                    cmd.ExecuteNonQuery();
                    Connection.conn.Close();

                    MessageBox.Show("Data Success Updated", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    buttonCan.PerformClick();
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex < 0)
            {

            }
            else
            {
                dataGridView1.CurrentRow.Selected = true;
                id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                textBoxUsername.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBoxName.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                textBoxEmail.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[6].Value);

                comboBoxJob.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
                comboBoxJob.SelectedValue = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[9].Value);
                richTextBoxAddress.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();

                MemoryStream stream = new MemoryStream((byte[])dataGridView1.Rows[e.RowIndex].Cells[8].Value);
                pictureBox1.Image = Image.FromStream(stream);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
    }
}
