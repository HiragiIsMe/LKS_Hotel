using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel
{
    public partial class CheckIn : Form
    {
        private int id_cust, Condition;
        public CheckIn()
        {
            InitializeComponent();
        }
        void onload()
        {
            FormBorderStyle = FormBorderStyle.None;
            MinimizeBox = false;
            MaximizeBox = false;
            ControlBox = false;
        }
        private void CheckIn_Load(object sender, EventArgs e)
        {
            onload();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from Reservation where BookingCode=@code", Connection.conn);
            cmd.Parameters.AddWithValue("@code", textBoxCode.Text);

            Connection.conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            if (reader.HasRows)
            {
                Connection.conn.Close();
                string query = "select ReservationRoom.id, Room.RoomNumber, Room.RoomFloor, RoomType.Name, ReservationRoom.StartDateTime from Reservation join ReservationRoom on Reservation.ID = ReservationRoom.ReservationID join Room on ReservationRoom.RoomID = Room.ID join RoomType on Room.RoomTypeID = RoomType.ID where Reservation.BookingCode = '"+ textBoxCode.Text +"' and CheckInDateTime > getdate()";
                dataGridView1.DataSource = Connection.getData(query);
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            else
            {
                Connection.conn.Close();
                MessageBox.Show("Booking Code Not Found", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if(textBoxName.Text == "" || textBoxEmail.Text == "" || textBoxAge.Text == "" || textBoxNIK.Text == "")
            {
                MessageBox.Show("Customer Field Must Be Filled", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if(radioButton1.Checked && radioButton2.Checked)
            {
                MessageBox.Show("Gender Must Be Selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if(textBoxNIK.TextLength != 16)
            {
                MessageBox.Show("Gender Must Be 16 Digit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (isValidEmail(textBoxEmail.Text) == false)
            {
                MessageBox.Show("Customer Email Doesn't Valid", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            SqlCommand nik = new SqlCommand("select * from Customer where NIK=@nik", Connection.conn);
            nik.Parameters.AddWithValue("@nik", textBoxNIK.Text);
            Connection.conn.Open();
            SqlDataReader reader = nik.ExecuteReader();
            reader.Read();
            if (reader.HasRows)
            {
                Connection.conn.Close();
                MessageBox.Show("NIK Has Been Used", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            Connection.conn.Close();

            SqlCommand phone = new SqlCommand("select * from Customer where PhoneNumber=@phone", Connection.conn);
            phone.Parameters.AddWithValue("@phone", textBoxPhone.Text);
            Connection.conn.Open();
            SqlDataReader read = phone.ExecuteReader();
            read.Read();
            if (read.HasRows)
            {
                Connection.conn.Close();
                MessageBox.Show("Phone Number Has Been Used", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            Connection.conn.Close();

            return true;
        }

        bool validateUp()
        {
            if (textBoxName.Text == "" || textBoxEmail.Text == "" || textBoxAge.Text == "" || textBoxNIK.Text == "")
            {
                MessageBox.Show("Customer Field Must Be Filled", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (radioButton1.Checked && radioButton2.Checked)
            {
                MessageBox.Show("Gender Must Be Selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (textBoxNIK.TextLength != 16)
            {
                MessageBox.Show("Gender Must Be 16 Digit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (isValidEmail(textBoxEmail.Text) == false)
            {
                MessageBox.Show("Customer Email Doesn't Valid", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            SqlCommand nik = new SqlCommand("select * from Customer where NIK=@nik", Connection.conn);
            nik.Parameters.AddWithValue("@nik", textBoxNIK.Text);
            Connection.conn.Open();
            SqlDataReader reader = nik.ExecuteReader();
            reader.Read();
            if (reader.HasRows && id_cust != reader.GetInt32(0))
            {
                Connection.conn.Close();
                MessageBox.Show("NIK Has Been Used", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            Connection.conn.Close();

            SqlCommand phone = new SqlCommand("select * from Customer where PhoneNumber=@phone", Connection.conn);
            phone.Parameters.AddWithValue("@phone", textBoxPhone.Text);
            Connection.conn.Open();
            SqlDataReader read = phone.ExecuteReader();
            read.Read();
            if (read.HasRows && id_cust != read.GetInt32(0))
            {
                Connection.conn.Close();
                MessageBox.Show("Phone Number Has Been Used", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            Connection.conn.Close();

            return true;
        }
        private void buttonIn_Click(object sender, EventArgs e)
        {
            char Gender;
            if (radioButton1.Checked)
            {
                Gender = '1';
            }
            else
            {
                Gender = '0';
            }
            if(Condition == 1)
            {
                if (validateIn())
                {
                    SqlCommand cmd = new SqlCommand("insert into Customer values(@name,@nik,@email,@gender,@phone,@dob)", Connection.conn);
                    cmd.Parameters.AddWithValue("@name", textBoxName.Text);
                    cmd.Parameters.AddWithValue("@nik", textBoxNIK.Text);
                    cmd.Parameters.AddWithValue("@email", textBoxEmail.Text);
                    cmd.Parameters.AddWithValue("@gender", Gender);
                    cmd.Parameters.AddWithValue("@phone", textBoxPhone.Text);
                    cmd.Parameters.AddWithValue("@dob", textBoxAge.Text);

                    Connection.conn.Open();
                    cmd.ExecuteNonQuery();
                    Connection.conn.Close();

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        SqlCommand cmd1 = new SqlCommand("update ReservationRoom set CheckInDateTime=getdate(), StartDateTime=getdate() where ID=" + dataGridView1.Rows[i].Cells[0].Value + "", Connection.conn);
                        Connection.conn.Open();
                        cmd1.ExecuteNonQuery();
                        Connection.conn.Close();

                        MessageBox.Show("Success", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }

            if(Condition == 2)
            {
                if (validateUp())
                {
                    SqlCommand cmd = new SqlCommand("update Customer set Name=@name,NIK=@nik,Email=@email,Gender=@gender,PhoneNumber=@phone,Age=@dob where ID="+ id_cust +"", Connection.conn);
                    cmd.Parameters.AddWithValue("@name", textBoxName.Text);
                    cmd.Parameters.AddWithValue("@nik", textBoxNIK.Text);
                    cmd.Parameters.AddWithValue("@email", textBoxEmail.Text);
                    cmd.Parameters.AddWithValue("@gender", Gender);
                    cmd.Parameters.AddWithValue("@phone", textBoxPhone.Text);
                    cmd.Parameters.AddWithValue("@dob", textBoxAge.Text);

                    Connection.conn.Open();
                    cmd.ExecuteNonQuery();
                    Connection.conn.Close();

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        SqlCommand cmd1 = new SqlCommand("update ReservationRoom set CheckInDateTime=getdate(), StartDateTime=getdate() where ID=" + dataGridView1.Rows[i].Cells[0].Value + "", Connection.conn);
                        Connection.conn.Open();
                        cmd1.ExecuteNonQuery();
                        Connection.conn.Close();

                        MessageBox.Show("Success", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            this.Close();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxNIK_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled= true;
            }
        }

        private void textBoxPhone_TextChanged(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select top(1) * from Customer where PhoneNumber like '%"+ textBoxPhone.Text + "%'", Connection.conn);

            Connection.conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            if (reader.HasRows)
            {
                Condition = 2;
                id_cust = reader.GetInt32(0);
                textBoxName.Text = reader.GetString(1);
                textBoxNIK.Text = reader.GetString(2);
                textBoxEmail.Text = reader.GetString(3);
                if(reader["Gender"].ToString() == "1")
                {
                    radioButton1.Checked = true;
                }
                else
                {
                    radioButton2.Checked = true;
                }
                textBoxAge.Text = reader.GetInt32(6).ToString();
                Connection.conn.Close();
            }
            else
            {
                id_cust = 0;
                textBoxName.Text = "";
                textBoxNIK.Text = "";
                textBoxEmail.Text = "";
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                textBoxAge.Text = "";
                Connection.conn.Close();
                Condition = 1;
            }
        }
    }
}
