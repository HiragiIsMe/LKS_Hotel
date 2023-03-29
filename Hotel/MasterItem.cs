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
    public partial class MasterItem : Form
    {
        private int id, Condition;
        public MasterItem()
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
            textBoxName.Enabled = false;
            textBoxPrice.Enabled = false;
            textBoxFee.Enabled = false;
            buttonSav.Enabled = false;
            buttonCan.Enabled = false;
            buttonIn.Enabled = true;
            buttonUp.Enabled = true;
            buttonDel.Enabled = true;
        }
        void Active()
        {
            textBoxName.Enabled = true;
            textBoxPrice.Enabled = true;
            textBoxFee.Enabled = true;
            buttonSav.Enabled = true;
            buttonCan.Enabled = true;
            buttonIn.Enabled = false;
            buttonUp.Enabled = false;
            buttonDel.Enabled = false;
        }
        void Clear()
        {
            textBoxName.Text = "";
            textBoxPrice.Text = "";
            textBoxFee.Text = "";
        }
        void Datagrid()
        {
            string query = "select * from Item";

            dataGridView1.DataSource = Connection.getData(query);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Columns[0].Visible = false;
        }
        private void MasterItem_Load(object sender, EventArgs e)
        {
            Datagrid();
            onload();
            nonActive();
        }

        private void buttonIn_Click(object sender, EventArgs e)
        {
            Condition = 1;
            Clear();
            Active();
        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            if(id > 0)
            {
                Condition = 2;
                Active();
            }
            else
            {
                MessageBox.Show("Please Select One Row To Update", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCan_Click(object sender, EventArgs e)
        {
            Datagrid();
            Clear();
            id = 0;
            nonActive();
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (id > 0)
            {
                DialogResult result = MessageBox.Show("Are You Sure To Delete " + dataGridView1.SelectedRows[0].Cells[1].Value + " ?", "Alert", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    SqlCommand cmd = new SqlCommand("delete from Item where ID=" + id + "", Connection.conn);
                    Connection.conn.Open();
                    cmd.ExecuteNonQuery();
                    Connection.conn.Close();

                    MessageBox.Show("Data Success Deleted", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Datagrid();
                    buttonCan.PerformClick();
                }
            }
            else
            {
                MessageBox.Show("Please Select One Row To Delete", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        bool validate()
        {
            if(textBoxName.Text == "" || textBoxPrice.Text == "" || textBoxFee.Text == "")
            {
                MessageBox.Show("All Field Must Be Filled", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        private void buttonSav_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                if(Condition == 1)
                {
                    SqlCommand cmd = new SqlCommand("insert into Item values(@name,@price,@fee)", Connection.conn);
                    cmd.Parameters.AddWithValue("@name", textBoxName.Text);
                    cmd.Parameters.AddWithValue("@price", textBoxPrice.Text);
                    cmd.Parameters.AddWithValue("@fee", textBoxFee.Text);

                    Connection.conn.Open();
                    cmd.ExecuteNonQuery();
                    Connection.conn.Close();

                    MessageBox.Show("Data Success Inserted", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    buttonCan.PerformClick();
                }

                if(Condition == 2)
                {
                    SqlCommand cmd = new SqlCommand("update Item set Name=@name,RequestPrice=@price,CompensationFee=@fee where ID="+ id +"", Connection.conn);
                    cmd.Parameters.AddWithValue("@name", textBoxName.Text);
                    cmd.Parameters.AddWithValue("@price", textBoxPrice.Text);
                    cmd.Parameters.AddWithValue("@fee", textBoxFee.Text);

                    Connection.conn.Open();
                    cmd.ExecuteNonQuery();
                    Connection.conn.Close();

                    MessageBox.Show("Data Success Updated", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    buttonCan.PerformClick();
                }
            }
        }

        private void textBoxPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBoxFee_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
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
                textBoxName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBoxPrice.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBoxFee.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            }
        }
    }
}
