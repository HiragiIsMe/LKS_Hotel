﻿using System;
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
    public partial class RequestAddItem : Form
    {
        private int IdReser;
        public RequestAddItem()
        {
            InitializeComponent();
        }
        void onload()
        {
            ControlBox = false;
            MinimizeBox = false;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.None;
            loadRoom();
            ItemGrid();
            loadItem();
        }
        void loadItem()
        {
            string query = "select * from Item";
            comboBoxItem.DataSource = Connection.getData(query);
            comboBoxItem.ValueMember = "ID";
            comboBoxItem.DisplayMember = "Name";
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
        private void RequestAddItem_Load(object sender, EventArgs e)
        {
            onload();
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

        private void numericUpDownQty_ValueChanged(object sender, EventArgs e)
        {
            getPrice();
        }
        bool CheckItem()
        {
            for (int i = 0; i < dataGridViewItem.Rows.Count; i++)
            {
                if (dataGridViewItem.Rows[i].Cells[0].Value.ToString() == comboBoxItem.SelectedValue.ToString())
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
        void getTotal()
        {
            int item = 0;
            for (int x = 0; x < dataGridViewItem.Rows.Count; x++)
            {
                item += Convert.ToInt32(dataGridViewItem.Rows[x].Cells[4].Value);
            }
            labelPrice.Text = item.ToString();
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (numericUpDownQty.Value == null || textBoxPrice.Text == "" || textBoxSubtotal.Text == "")
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
            if (e.ColumnIndex == 5)
            {
                dataGridViewItem.CurrentRow.Selected = true;
                dataGridViewItem.Rows.RemoveAt(dataGridViewItem.SelectedRows[0].Index);
                getTotal();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(dataGridViewItem.Rows.Count != 0)
            {
                for (int j = 0; j < dataGridViewItem.Rows.Count; j++)
                {
                    SqlCommand inItem = new SqlCommand("insert into ReservationRequestItem values(" + IdReser + ", " + Convert.ToInt32(dataGridViewItem.Rows[j].Cells[0].Value) + ", " + Convert.ToInt32(dataGridViewItem.Rows[j].Cells[2].Value) + ", " + Convert.ToInt32(dataGridViewItem.Rows[j].Cells[4].Value) + ")", Connection.conn);
                    Connection.conn.Open();
                    inItem.ExecuteNonQuery();
                    Connection.conn.Close();
                }

                MessageBox.Show("Request Item Success Added", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridViewItem.Rows.Clear();
            }
            else
            {
                MessageBox.Show("Please Fill At Least One Request Item", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);  
            }
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
