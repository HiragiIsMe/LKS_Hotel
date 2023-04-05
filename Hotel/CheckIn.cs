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

        private void textBoxPhone_TextChanged(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select top(1) * from Customer where PhoneNumber like '%"+ textBoxPhone.Text + "%'", Connection.conn);

            Connection.conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            if (reader.HasRows)
            {
                Condition = 1;
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
                Condition = 2;
            }
        }
    }
}
