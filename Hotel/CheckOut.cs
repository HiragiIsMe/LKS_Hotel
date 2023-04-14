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
    public partial class CheckOut : Form
    {
        private int IdReser;
        public CheckOut()
        {
            InitializeComponent();
        }
        void onload()
        {
            FormBorderStyle = FormBorderStyle.None;
            ControlBox = false;
            MinimizeBox = false;
            MaximizeBox = false;
            loadRoom();
            loadItem();
            loadItemStatus();
        }
        void loadItem()
        {
            string query = "select Item.ID, Item.Name from ReservationRequestItem join Item on ReservationRequestItem.ItemID = Item.ID where ReservationRoomID = "+ IdReser +"";
            comboBoxItem.DataSource = Connection.getData(query);
            comboBoxItem.ValueMember = "ID";
            comboBoxItem.DisplayMember = "Name";
        }
        void loadRoom()
        {
            string query = "select * from Room where Status = '0'";

            comboBoxRoom.DataSource = Connection.getData(query);
            comboBoxRoom.DisplayMember = "RoomNumber";
            comboBoxRoom.ValueMember = "ID";

            SqlCommand cmd = new SqlCommand("select top(1) id from reservationRoom where roomId = " + comboBoxRoom.SelectedValue + " order by id desc", Connection.conn);
            Connection.conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            IdReser = reader.GetInt32(0);
            Connection.conn.Close();
        }
        void loadItemStatus()
        {
            string query = "select * from ItemStatus";
            comboBoxStatus.DataSource = Connection.getData(query);
            comboBoxStatus.ValueMember = "ID";
            comboBoxStatus.DisplayMember = "Name";
        }
        private void CheckOut_Load(object sender, EventArgs e)
        {
            onload();
        }

        private void comboBoxRoom_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select top(1) id from reservationRoom where roomId = " + comboBoxRoom.SelectedValue + " order by id desc", Connection.conn);
            Connection.conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            IdReser = reader.GetInt32(0);
            Connection.conn.Close();
        }
    }
}
