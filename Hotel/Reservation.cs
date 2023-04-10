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
    public partial class Reservation : Form
    {
        private int id_room, id_cust;
        private AddCustomer customer = new AddCustomer()
        {
            TopLevel = false,
            TopMost = true,
        };
        public Reservation()
        {
            InitializeComponent();
        }
        void onload()
        {
            ControlBox = false;
            MinimizeBox = false;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.None;
            loadRoomType();
            loadItem();
            DatagridSel();
            loadCustomerGrid();
            ItemGrid();
            radioButtonSearch.Select();
        }
        void getTotal()
        {
            int room = 0;
            int item = 0;
            int total = 0;
            for(int i = 0; i < dataGridViewSel.Rows.Count; i++)
            {
                room += Convert.ToInt32(dataGridViewSel.Rows[i].Cells[3].Value);
            }

            for(int x = 0; x < dataGridViewItem.Rows.Count; x++)
            {
                item += Convert.ToInt32(dataGridViewItem.Rows[x].Cells[4].Value);
            }
            total = room + item;
            labelPrice.Text = total.ToString();
        }
        void loadRoomType()
        {
            string query = "select * from RoomType";
            comboBoxType.DataSource = Connection.getData(query);
            comboBoxType.ValueMember = "ID";
            comboBoxType.DisplayMember = "Name";
        }
        void loadItem()
        {
            string query = "select * from Item";
            comboBoxItem.DataSource = Connection.getData(query);
            comboBoxItem.ValueMember = "ID";
            comboBoxItem.DisplayMember = "Name";
        }
        void DatagridSel()
        {
            dataGridViewSel.ColumnCount = 5;
            dataGridViewSel.Columns[0].Visible = false;
            dataGridViewSel.Columns[1].HeaderText = "RoomNumber";
            dataGridViewSel.Columns[2].HeaderText = "RoomFloor";
            dataGridViewSel.Columns[3].HeaderText = "RoomPrice";
            dataGridViewSel.Columns[4].HeaderText = "Description";
            dataGridViewSel.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        void loadCustomerGrid()
        {
            string query = "select ID, Name, Email, Gender from Customer";
            dataGridViewCustomer.DataSource = Connection.getData(query);
            dataGridViewCustomer.Columns[0].Visible = false;

            for(int i = 0; i < dataGridViewCustomer.Rows.Count; i++)
            {
                if(dataGridViewCustomer.Rows[i].Cells[3].Value.ToString() == "1")
                {
                    dataGridViewCustomer.Rows[i].Cells[3].Value = "Laki-Laki";
                }
                else
                {
                    dataGridViewCustomer.Rows[i].Cells[3].Value = "Perempuan";
                }
            }
            dataGridViewCustomer.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void Reservation_Load(object sender, EventArgs e)
        {
            onload();
        }

        private void radioButtonAdd_Click(object sender, EventArgs e)
        {
            labelSearch.Hide();
            textBoxSearch.Hide();
            dataGridViewCustomer.Hide();
            
            panelLoad.Controls.Clear();
            panelLoad.Controls.Add(customer);
            customer.Show();
        }

        private void radioButtonSearch_CheckedChanged(object sender, EventArgs e)
        {
            labelSearch.Show();
            textBoxSearch.Show();
            dataGridViewCustomer.Show();
            panelLoad.Controls.Clear();
            panelLoad.Controls.Add(dataGridViewCustomer);
        }

        private void buttonType_Click(object sender, EventArgs e)
        {
            string query = "select Room.ID, Room.RoomNumber, Room.RoomFloor, RoomType.Price, Room.Description from Room join RoomType on Room.RoomTypeID = RoomType.ID where Room.RoomTypeID=" + comboBoxType.SelectedValue + " and Room.Status='1'";
            dataGridViewAvb.DataSource = Connection.getData(query);
            dataGridViewAvb.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        bool CheckRoom()
        {
            for(int i = 0; i < dataGridViewSel.Rows.Count; i++)
            {
                if(dataGridViewSel.Rows[i].Cells[0].Value.ToString() == id_room.ToString())
                {
                    return false;
                }
            }

            return true;
        }
        private void buttonIn_Click(object sender, EventArgs e)
        {
            if (id_room > 0)
            {
                if (CheckRoom())
                {
                    string[] add = { id_room.ToString(), dataGridViewAvb.SelectedRows[0].Cells[1].Value.ToString(), dataGridViewAvb.SelectedRows[0].Cells[2].Value.ToString(), dataGridViewAvb.SelectedRows[0].Cells[3].Value.ToString(), dataGridViewAvb.SelectedRows[0].Cells[2].Value.ToString(), dataGridViewAvb.SelectedRows[0].Cells[3].Value.ToString() };
                    dataGridViewSel.Rows.Add(add);
                    id_room = 0;
                    getTotal();
                }
                else
                {
                    MessageBox.Show("Room Has Been Added", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Pleas Select One Row In Available Room", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewAvb_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex < 0)
            {

            }
            else
            {
                dataGridViewAvb.CurrentRow.Selected = true;
                id_room = Convert.ToInt32(dataGridViewAvb.Rows[e.RowIndex].Cells[0].Value);
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            dataGridViewSel.Rows.RemoveAt(dataGridViewSel.SelectedRows[0].Index);
        }

        private void dataGridViewSel_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex < 0)
            {

            }
            else
            {
                dataGridViewSel.CurrentRow.Selected = true;
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string query = "select ID, Name, Email, Gender from Customer where Name like '%"+ textBoxSearch.Text +"%'";

            dataGridViewCustomer.DataSource = Connection.getData(query);
            dataGridViewCustomer.Columns[0].Visible = false;

            for (int i = 0; i < dataGridViewCustomer.Rows.Count; i++)
            {
                if (dataGridViewCustomer.Rows[i].Cells[3].Value.ToString() == "1")
                {
                    dataGridViewCustomer.Rows[i].Cells[3].Value = "Laki-Laki";
                }
                else
                {
                    dataGridViewCustomer.Rows[i].Cells[3].Value = "Perempuan";
                }
            }
            dataGridViewCustomer.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        void getPrice()
        {
            SqlCommand cmd = new SqlCommand("select RequestPrice from Item where ID=" + comboBoxItem.SelectedValue + "", Connection.conn);
            Connection.conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            textBoxPrice.Text = reader["RequestPrice"].ToString();
            textBoxSubtotal.Text = (reader.GetInt32(0) * numericUpDownQty.Value).ToString();
            Connection.conn.Close();
        }
        private void comboBoxItem_SelectionChangeCommitted(object sender, EventArgs e)
        {
            getPrice();
        }
        void ItemGrid()
        {
            dataGridViewItem.ColumnCount = 5;
            dataGridViewItem.Columns[0].Visible = false;
            dataGridViewItem.Columns[1].HeaderText = "Item";
            dataGridViewItem.Columns[2].HeaderText = "Quantity";
            dataGridViewItem.Columns[3].HeaderText = "Price";
            dataGridViewItem.Columns[4].HeaderText = "Subtotal";
            DataGridViewButtonColumn button = new DataGridViewButtonColumn();
            {
                button.Name = "button";
                button.Text = "Remove";
                button.HeaderText = "Option";
                button.UseColumnTextForButtonValue = true;
                dataGridViewItem.Columns.Add(button);
            }
            dataGridViewItem.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        void ClearItem()
        {
            numericUpDownQty.Value = 1;
            textBoxPrice.Text = "";
            textBoxSubtotal.Text = "";
        }
        bool CheckItem()
        {
            for(int i = 0; i < dataGridViewItem.Rows.Count; i++)
            {
                if(dataGridViewItem.Rows[i].Cells[0].Value.ToString() == comboBoxType.SelectedValue.ToString())
                {
                    int a = Convert.ToInt32(numericUpDownQty.Value);
                    dataGridViewItem.Rows[i].Cells[2].Value = a;
                    dataGridViewItem.Rows[i].Cells[4].Value = textBoxSubtotal.Text;
                    ClearItem();

                    return false;
                }
            }
            return true;
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
           if(numericUpDownQty.Value == null || textBoxPrice.Text == "" || textBoxSubtotal.Text == "")
            {
                MessageBox.Show("All Field Item Must Be Filled", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (CheckItem())
                {
                    string[] add = { comboBoxItem.SelectedValue.ToString(), comboBoxItem.Text, numericUpDownQty.Value.ToString(), textBoxPrice.Text, textBoxSubtotal.Text };
                    dataGridViewItem.Rows.Add(add);
                    getTotal();
                    ClearItem();
                }
            }
        }

        private void dataGridViewItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 5)
            {
                dataGridViewItem.CurrentRow.Selected = true;
                dataGridViewItem.Rows.RemoveAt(dataGridViewItem.SelectedRows[0].Index);
                getTotal();
            }
        }
        bool Validate()
        {
            if(dateTimePickerIn.Value == null || dateTimePickerOut.Value == null || textBoxStaying.Text == "")
            {
                MessageBox.Show("Reservation's Information Field Must Be Filled", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (radioButtonAdd.Checked)
            {
                if(customer.textBoxName.Text == "" || customer.textBoxNIK.Text == "" || customer.textBoxEmail.Text == "" || customer.textBoxPhone.Text == "")
                {
                    MessageBox.Show("Customer Field Must Be Filled", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
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
                if (isValidEmail(customer.textBoxEmail.Text) == false)
                {
                    MessageBox.Show("Customer Email Doesn't Valid", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if(customer.textBoxNIK.TextLength != 16)
                {
                    MessageBox.Show("Customer NIK Must Be 16 Digit", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if(customer.dateTimePickerBirth.Value > DateTime.Now)
                {
                    MessageBox.Show("Customer Birth Is Not Valid", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            if (radioButtonSearch.Checked)
            {
                if(dataGridViewCustomer.CurrentRow.Selected == false)
                {
                    MessageBox.Show("Please Select One Customer", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            if(dataGridViewSel.Rows.Count == 0)
            {
                MessageBox.Show("Please Select At Least One Room", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        string getCode()
        {
            string code;

            SqlCommand cmd = new SqlCommand("select top(1) BookingCode from Reservation order by ID desc", Connection.conn);
            Connection.conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            if (reader.HasRows)
            {
                string getCode = reader.GetString(0);
                Connection.conn.Close();

                int a = 2;
                int b = getCode.Length - a;
                int c = Convert.ToInt32(getCode.Substring(a,b)) + 1;
                code = "BK" + c;

                return code;
            }
            else
            {

                Connection.conn.Close();
                code = "BK1";

                return code;
            }
            Connection.conn.Close();
        }
        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                if (radioButtonSearch.Checked)
                {
                    SqlCommand inResr = new SqlCommand("insert into Reservation values(getdate(), "+ Model.id +", "+ id_cust +", '"+ getCode() +"')", Connection.conn);
                    Connection.conn.Open();
                    inResr.ExecuteNonQuery();
                    Connection.conn.Close();
                }

                if (radioButtonAdd.Checked)
                {
                    SqlCommand cmd = new SqlCommand("insert into Customer values(@name,@nik,@email,@gender,@phone,@dob)", Connection.conn);
                    cmd.Parameters.AddWithValue("@name", customer.textBoxName.Text);
                    cmd.Parameters.AddWithValue("@nik", customer.textBoxNIK.Text);
                    cmd.Parameters.AddWithValue("@email", customer.textBoxEmail.Text);
                    cmd.Parameters.AddWithValue("@gender", customer.comboBoxGender.SelectedValue);
                    cmd.Parameters.AddWithValue("@phone", customer.textBoxPhone.Text);
                    int i = Convert.ToInt32(DateTime.Now.ToString("yyyy")) - Convert.ToInt32(customer.dateTimePickerBirth.Value.ToString("yyyy"));
                    cmd.Parameters.AddWithValue("@dob", i);

                    Connection.conn.Open();
                    cmd.ExecuteNonQuery();
                    Connection.conn.Close();

                    SqlCommand getCust = new SqlCommand("select top(1) id from Customer order by id desc", Connection.conn);
                    Connection.conn.Open();
                    SqlDataReader reader = getCust.ExecuteReader();
                    reader.Read();
                    int idCust = reader.GetInt32(0);
                    Connection.conn.Close();

                    SqlCommand inResr = new SqlCommand("insert into Reservation values(getdate(), " + Model.id + ", " + idCust + ", '" + getCode() + "')", Connection.conn);
                    Connection.conn.Open();
                    inResr.ExecuteNonQuery();
                    Connection.conn.Close();
                }

                SqlCommand getResrId = new SqlCommand("select top(1) id from Reservation order by id desc", Connection.conn);
                Connection.conn.Open();
                SqlDataReader read = getResrId.ExecuteReader();
                read.Read();
                int idRest = read.GetInt32(0);
                Connection.conn.Close();

                label1.Text = idRest.ToString();
                for(int i = 0; i < dataGridViewSel.Rows.Count; i++)
                {
                    SqlCommand inResrRoom = new SqlCommand("insert into ReservationRoom values("+ idRest +", "+ Convert.ToInt32(dataGridViewSel.Rows[i].Cells[0].Value) + ", '"+ dateTimePickerIn.Value.Date +"', @night, " + Convert.ToInt32(dataGridViewSel.Rows[i].Cells[3].Value) + ", '" + dateTimePickerIn.Value +"', '"+ dateTimePickerOut.Value+ "')", Connection.conn);
                    inResrRoom.Parameters.AddWithValue("@night", textBoxStaying.Text);
                    Connection.conn.Open();
                    inResrRoom.ExecuteNonQuery();
                    Connection.conn.Close();

                    SqlCommand upStatus = new SqlCommand("update Room set status='0' where ID="+ Convert.ToInt32(dataGridViewSel.Rows[i].Cells[0].Value) + "", Connection.conn);
                    Connection.conn.Open();
                    upStatus.ExecuteNonQuery();
                    Connection.conn.Close();
                }

                if(dataGridViewItem.Rows.Count > 0)
                {
                    SqlCommand getRoomID = new SqlCommand("select top(1) id from ReservationRoom order by id desc", Connection.conn);
                    Connection.conn.Open();
                    SqlDataReader reader = getRoomID.ExecuteReader();
                    reader.Read();
                    int RoomID = reader.GetInt32(0);
                    Connection.conn.Close();

                    for(int j = 0; j < dataGridViewItem.Rows.Count; j++)
                    {
                        SqlCommand inItem = new SqlCommand("insert into ReservationRequestItem values("+ RoomID +", "+ Convert.ToInt32(dataGridViewItem.Rows[j].Cells[0].Value) + ", " + Convert.ToInt32(dataGridViewItem.Rows[j].Cells[2].Value) + ", " + Convert.ToInt32(dataGridViewItem.Rows[j].Cells[4].Value) + ")", Connection.conn);
                        Connection.conn.Open();
                        inItem.ExecuteNonQuery();
                        Connection.conn.Close();
                    }
                }

                MessageBox.Show("Reservation Success", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                id_cust = 0;
                radioButtonSearch.Checked = true;
                customer.textBoxName.Text = "";
                customer.textBoxEmail.Text = "";
                customer.textBoxNIK.Text = "";
                customer.textBoxPhone.Text = "";
                dataGridViewAvb.DataSource = null;
                dataGridViewAvb.Rows.Clear();
                dataGridViewSel.Rows.Clear();
                dataGridViewItem.Rows.Clear();
            }
        }

        private void numericUpDownQty_ValueChanged(object sender, EventArgs e)
        {
            getPrice();
        }

        private void dataGridViewCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex < 0)
            {

            }
            else
            {
                dataGridViewCustomer.CurrentRow.Selected = true;
                id_cust = Convert.ToInt32(dataGridViewCustomer.Rows[e.RowIndex].Cells[0].Value);
            }
        }
    }
}
