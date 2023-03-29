using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel
{
    public partial class MasterRoomType : Form
    {
        private int Condition, id;
        public MasterRoomType()
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
            numericUpDownCapacity.Enabled = false;
            textBoxPrice.Enabled = false;
            buttonSav.Enabled = false;
            buttonCan.Enabled = false;
            buttonBrow.Enabled = false;
        }
        void Active()
        {
            textBoxName.Enabled = true;
            numericUpDownCapacity.Enabled = true;
            textBoxPrice.Enabled = true;
            buttonSav.Enabled = true;
            buttonCan.Enabled = true;
            buttonBrow.Enabled = true;
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
            textBoxName.Text = "";
            numericUpDownCapacity.Value = 0;
            textBoxPrice.Text = "";
            pictureBox1.Image = null;
        }
        void Datagrid()
        {
            string query = "select * from RoomType";
            dataGridView1.DataSource = Connection.getData(query);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[4].Visible = false;
        }
        private void MasterRoomType_Load(object sender, EventArgs e)
        {
            onload();
            nonActive();
            Datagrid();
        }

        private void buttonIn_Click(object sender, EventArgs e)
        {
            btnNonActive();
            Active();
            Condition = 1;
            ClearInput();
        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            if (id > 0)
            {
                btnNonActive();
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
            if(id > 0)
            {
                DialogResult result = MessageBox.Show("Are You Sure To Delete "+ dataGridView1.SelectedRows[0].Cells[1].Value +" ?", "Alert", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if(result == DialogResult.OK)
                {
                    SqlCommand cmd = new SqlCommand("delete from RoomType where id=" + id + "", Connection.conn);
                    Connection.conn.Open();
                    cmd.ExecuteNonQuery();
                    Connection.conn.Close();

                    MessageBox.Show("Data Success Deleted", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Datagrid();
                    id = 0; 
                }
            }
            else
            {
                MessageBox.Show("Please Select One Row To Delete", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCan_Click(object sender, EventArgs e)
        {
            nonActive();
            btnActive();
            ClearInput();
        }
        bool validate()
        {
            if(textBoxName.Text == "" || numericUpDownCapacity.Value == null || textBoxPrice.Text == "" || pictureBox1.Image == null)
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
                if (Condition == 1)
                {
                    SqlCommand cmd = new SqlCommand("insert into RoomType values(@name,@capacity,@price,@photo)", Connection.conn);
                    cmd.Parameters.AddWithValue("@name", textBoxName.Text);
                    cmd.Parameters.AddWithValue("@capacity", numericUpDownCapacity.Value);
                    cmd.Parameters.AddWithValue("@price", textBoxPrice.Text);
                    cmd.Parameters.AddWithValue("@photo", Enc.getImage(pictureBox1.Image));

                    Connection.conn.Open();
                    cmd.ExecuteNonQuery();
                    Connection.conn.Close();
                    MessageBox.Show("Data Success Inserted", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Datagrid();
                    id = 0;
                    buttonCan.PerformClick();
                }

                if (Condition == 2)
                {
                    SqlCommand cmd = new SqlCommand("update RoomType set Name=@name,Capacity=@capacity,Price=@price,Photo=@photo where id = " + id + "", Connection.conn);
                    cmd.Parameters.AddWithValue("@name", textBoxName.Text);
                    cmd.Parameters.AddWithValue("@capacity", numericUpDownCapacity.Value);
                    cmd.Parameters.AddWithValue("@price", textBoxPrice.Text);
                    cmd.Parameters.AddWithValue("@photo", Enc.getImage(pictureBox1.Image));

                    Connection.conn.Open();
                    cmd.ExecuteNonQuery();
                    Connection.conn.Close();
                    MessageBox.Show("Data Success Updated", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Datagrid();
                    id = 0;
                    buttonCan.PerformClick();
                }
            }
        }

        private void buttonBrow_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Images|*.jpg;*.jpeg;*.png;";
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(dialog.FileName);
                Bitmap bmp = (Bitmap)img;
                pictureBox1.Image = bmp;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
        private void textBoxPrice_KeyPress(object sender, KeyPressEventArgs e)
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
                numericUpDownCapacity.Value = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
                textBoxPrice.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();

                if (dataGridView1.Rows[e.RowIndex].Cells[4].Value != null)
                {
                    MemoryStream stream = new MemoryStream((byte[])dataGridView1.Rows[e.RowIndex].Cells[4].Value);
                    pictureBox1.Image = Image.FromStream(stream);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else
                {
                    pictureBox1.Image = null;
                }
            }
        }
    }
}
