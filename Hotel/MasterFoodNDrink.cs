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
    public partial class MasterFoodNDrink : Form
    {
        private int id, Condition;
        public MasterFoodNDrink()
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
            comboBoxType.Enabled = false;
            textBoxPrice.Enabled = false;
            buttonSav.Enabled = false;
            buttonCan.Enabled = false;
            buttonIn.Enabled = true;
            buttonUp.Enabled = true;
            buttonDel.Enabled = true;
        }
        void Active()
        {
            textBoxName.Enabled = true;
            comboBoxType.Enabled= true;
            textBoxPrice.Enabled = true;
            buttonSav.Enabled = true;
            buttonCan.Enabled = true;
            buttonIn.Enabled = false;
            buttonUp.Enabled = false;
            buttonDel.Enabled = false;
        }
        void Clear()
        {
            textBoxName.Text = "";
            comboBoxType.Text = "";
            textBoxPrice.Text = "";
        }
        void loadtype()
        {
            Dictionary<int, string> data = new Dictionary<int, string>();
            data.Add(1, "Food");
            data.Add(2, "Drink");

            comboBoxType.DataSource = new BindingSource(data, null);
            comboBoxType.ValueMember = "Key";
            comboBoxType.DisplayMember = "Value";
        }
        void Datagrid()
        {
            string query = "select * from FoodsAndDrinks";

            dataGridView1.DataSource = Connection.getData(query);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[4].Visible = false;

            for(int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if(dataGridView1.Rows[i].Cells[2].Value.ToString() == "1")
                {
                    dataGridView1.Rows[i].Cells[2].Value = "Food";
                }
                else
                {
                    dataGridView1.Rows[i].Cells[2].Value = "Drink";
                }
            }
        }
        private void MasterFoodNDrink_Load(object sender, EventArgs e)
        {
            onload();
            nonActive();
            Datagrid();
            loadtype();
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
                textBoxPrice.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();

                comboBoxType.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                if(comboBoxType.Text == "Food")
                {
                    comboBoxType.SelectedValue = 1;
                }
                else
                {
                    comboBoxType.SelectedValue = 2;
                }

                MemoryStream stream = new MemoryStream((byte[])dataGridView1.Rows[e.RowIndex].Cells[4].Value);
                pictureBox1.Image = Image.FromStream(stream);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            if (id > 0)
            {
                Condition = 2;
                Active();
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
                    SqlCommand cmd = new SqlCommand("delete from FoodsAndDrinks where ID=" + id + "", Connection.conn);
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

        private void buttonCan_Click(object sender, EventArgs e)
        {
            id = 0;
            Datagrid();
            nonActive();
            Clear();
        }
        bool validate()
        {
            if(textBoxName.Text == "" || comboBoxType.Text == "" || textBoxPrice.Text == "" || pictureBox1.Image == null)
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
                    SqlCommand cmd = new SqlCommand("insert into FoodsAndDrinks values(@name,@type,@price,@photo)", Connection.conn);
                    cmd.Parameters.AddWithValue("@name", textBoxName.Text);
                    cmd.Parameters.AddWithValue("@type", comboBoxType.SelectedValue);
                    cmd.Parameters.AddWithValue("@price", textBoxPrice.Text);
                    cmd.Parameters.AddWithValue("@photo", Enc.getImage(pictureBox1.Image));

                    Connection.conn.Open();
                    cmd.ExecuteNonQuery();
                    Connection.conn.Close();

                    MessageBox.Show("Data Success Inserted", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    buttonCan.PerformClick();
                }

                if(Condition == 2)
                {
                    SqlCommand cmd = new SqlCommand("update FoodsAndDrinks set Name=@name,Type=@type,Price=@price,Photo=@photo where ID = "+ id +"", Connection.conn);
                    cmd.Parameters.AddWithValue("@name", textBoxName.Text);
                    cmd.Parameters.AddWithValue("@type", comboBoxType.SelectedValue);
                    cmd.Parameters.AddWithValue("@price", textBoxPrice.Text);
                    cmd.Parameters.AddWithValue("@photo", Enc.getImage(pictureBox1.Image));

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
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void buttonBrow_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Images|*.png;*.jpg;*.jpeg";

            if(dialog.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(dialog.FileName);
                Bitmap bmp = (Bitmap)img;
                pictureBox1.Image = bmp;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }  
        }

        private void buttonIn_Click(object sender, EventArgs e)
        {
            Condition = 1;
            Clear();
            Active();
        }
    }
}
