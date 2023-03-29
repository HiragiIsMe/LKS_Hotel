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
    public partial class MasterRoom : Form
    {
        private int id, Condition;
        public MasterRoom()
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
            textBoxNumber.Enabled = false;
            comboBoxType.Enabled = false;
            textBoxFloor.Enabled = false;
            buttonSav.Enabled = false;
            buttonCan.Enabled = false;
        }
        void Active()
        {
            textBoxNumber.Enabled = true;
            comboBoxType.Enabled = true;
            textBoxFloor.Enabled = true;
            buttonSav.Enabled = true;
            buttonCan.Enabled = true;
        }
        void btnActive()
        {
            buttonIn.Enabled = true;
            buttonUp.Enabled = true;
            buttonDel.Enabled = true;
        }
        void btnNonActive()
        {
            buttonIn.Enabled = false;
            buttonUp.Enabled = false;
            buttonDel.Enabled = false;
        }
        void ClearInput()
        {
            textBoxNumber.Text = "";
            textBoxFloor.Text = "";
            richTextBoxDesc.Text = "";
        }
        void Datagrid()
        {
            string query = "select Room.ID, RoomType.id, Room.RoomNumber, RoomType.Name, Room.RoomFloor, Room.Description from Room join RoomType on Room.RoomTypeID = RoomType.id";
            dataGridView1.DataSource = Connection.getData(query);
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        void RoomType()
        {
            string query = "select * from RoomType";
            comboBoxType.DataSource = Connection.getData(query);
            comboBoxType.ValueMember = "ID";
            comboBoxType.DisplayMember = "Name";    
        }
        private void MasterRoom_Load(object sender, EventArgs e)
        {
            onload();
            Datagrid();
            RoomType();
            nonActive();
        }

        private void buttonIn_Click(object sender, EventArgs e)
        {
            Active();
            btnNonActive();
            ClearInput();
            Condition = 1;
        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            if(id < 0)
            {
                MessageBox.Show("Please Select One Row To Update", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Active();
                btnNonActive();
                Condition = 2;
            }
        }
        bool validate()
        {
            if(textBoxNumber.Text == "" || textBoxFloor.Text == "" || comboBoxType.Text == "" || richTextBoxDesc.Text == "")
            {
                MessageBox.Show("All Field Must Be Filled", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            SqlCommand cmd = new SqlCommand("select ID from Room where RoomNumber=@number",Connection.conn);
            cmd.Parameters.AddWithValue("@number", textBoxNumber.Text);
            Connection.conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            if (reader.HasRows)
            {
                Connection.conn.Close();
                MessageBox.Show("Room Number Has Been Used", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            Connection.conn.Close();
            return true;
        }
        bool validateUp()
        {
            if (textBoxNumber.Text == "" || textBoxFloor.Text == "" || comboBoxType.Text == "" || richTextBoxDesc.Text == "")
            {
                MessageBox.Show("All Field Must Be Filled", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
           if(textBoxNumber.Text != dataGridView1.SelectedRows[0].Cells[2].Value.ToString())
            {
                SqlCommand cmd = new SqlCommand("select ID from Room where RoomNumber=@number", Connection.conn);
                cmd.Parameters.AddWithValue("@number", textBoxNumber.Text);
                Connection.conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    Connection.conn.Close();
                    MessageBox.Show("Room Number Has Been Used", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                Connection.conn.Close();
            }
            return true;
        }
        private void buttonSav_Click(object sender, EventArgs e)
        {
            if (Condition == 1)
            {
                if (validate())
                {
                    SqlCommand cmd = new SqlCommand("insert into Room values(@type,@number,@floor,@desc,1)", Connection.conn);
                    cmd.Parameters.AddWithValue("@type", comboBoxType.SelectedValue);
                    cmd.Parameters.AddWithValue("@number", textBoxNumber.Text);
                    cmd.Parameters.AddWithValue("@floor", textBoxFloor.Text);
                    cmd.Parameters.AddWithValue("@desc", richTextBoxDesc.Text);
                    Connection.conn.Open();
                    cmd.ExecuteNonQuery();
                    Connection.conn.Close();

                    MessageBox.Show("Data Success Inserted", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    buttonCan.PerformClick();
                }
            }

            if (Condition == 2)
            {
                if (validateUp())
                {
                    SqlCommand cmd = new SqlCommand("update Room set RoomTypeID=@type,RoomNumber=@number,RoomFloor=@floor,Description=@desc where ID=" + id + "", Connection.conn);
                    cmd.Parameters.AddWithValue("@type", comboBoxType.SelectedValue);
                    cmd.Parameters.AddWithValue("@number", textBoxNumber.Text);
                    cmd.Parameters.AddWithValue("@floor", textBoxFloor.Text);
                    cmd.Parameters.AddWithValue("@desc", richTextBoxDesc.Text);
                    Connection.conn.Open();
                    cmd.ExecuteNonQuery();
                    Connection.conn.Close();

                    MessageBox.Show("Data Success Updated", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    buttonCan.PerformClick();
                }
            }
        }

        private void textBoxNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBoxFloor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (id < 0)
            {
                MessageBox.Show("Please Select One Row To Delete", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result = MessageBox.Show("Are You Sure To Delete Room Number " + dataGridView1.SelectedRows[0].Cells[2].Value + " ?", "Alert", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    SqlCommand cmd = new SqlCommand("delete from Room where id=" + id + "", Connection.conn);
                    Connection.conn.Open();
                    cmd.ExecuteNonQuery();
                    Connection.conn.Close();

                    MessageBox.Show("Data Success Deleted", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Datagrid();
                    id = 0;
                }
            }
        }

        private void buttonCan_Click(object sender, EventArgs e)
        {
            btnActive();
            nonActive();
            ClearInput();
            id = 0;
            Datagrid();
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
                textBoxNumber.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBoxFloor.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                richTextBoxDesc.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                comboBoxType.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[1].Value;
                comboBoxType.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            }
        }
    }
}
