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
    public partial class Reservation : Form
    {
        private int id_room, id_cust;
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
            AddCustomer customer = new AddCustomer()
            {
                TopLevel = false,
                TopMost = true,
            };
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
            dataGridViewAvb.Columns[0].Visible = false;
            dataGridViewAvb.Columns[3].HeaderText = "RoomPrice";
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

        private void comboBoxItem_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select RequestPrice from Item where ID="+ comboBoxItem.SelectedValue +"", Connection.conn);
            Connection.conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            textBoxPrice.Text = reader["RequestPrice"].ToString();
            Connection.conn.Close();
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
                AddCustomer add = new AddCustomer();
                if(add.textBoxName.Text == "" || add.textBoxNIK.Text == "" || add.textBoxEmail.Text == "" || add.textBoxPhone.Text == "")
                {
                    MessageBox.Show("Customer Field Must Be Filled", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            if (radioButtonSearch.Checked)
            {
                if(id_cust < 0)
                {
                    MessageBox.Show("Please Select One Customer", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            if(dataGridViewSel.Rows.Count < 0)
            {
                MessageBox.Show("Please Select At Least One Room", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            if (Validate())
            {

            }
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
