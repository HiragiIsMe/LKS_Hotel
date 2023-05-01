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
    public partial class ReportGuest : Form
    {
        public ReportGuest()
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
        void loadYear()
        {
            int year = 2023;

            for (int i = 1; i < 100; i++)
            {
                comboBox1.Items.Add(year.ToString());
                year += 1;
            }
        }
        private void ReportGuest_Load(object sender, EventArgs e)
        {
            onload();
            loadYear();
        }

        private void buttonType_Click(object sender, EventArgs e)
        {
            string[] bulan = { "Jan", "Feb", "Mar", "Apr", "May", "June", "July", "Aug", "Sep", "Oct", "Nov", "Dec" };
            chart1.Series.Clear();
            chart1.Series.Add("Guests");
            for (int i = 0; i <= 11; i++)
            {
                if (comboBox1.Text == "")
                {
                    int month = i + 1;
                    SqlCommand cmd = new SqlCommand("select Count(ID) from Reservation where month(Datetime) = " + month + " and year(Datetime) = 2023", Connection.conn);
                    Connection.conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    if (reader.IsDBNull(0))
                    {
                        chart1.Series["Guests"].Points.AddXY(bulan[i].ToString(), 0);
                    }
                    else
                    {
                        chart1.Series["Guests"].Points.AddXY(bulan[i].ToString(), reader.GetInt32(0));
                    }

                    Connection.conn.Close();
                }
                else
                {
                    int month = i + 1;
                    SqlCommand cmd1 = new SqlCommand("select Count(ID) from Reservation where month(Datetime) = " + month + " and year(Datetime) = 2023", Connection.conn);
                    Connection.conn.Open();
                    SqlDataReader reader1 = cmd1.ExecuteReader();
                    reader1.Read();
                    if (reader1.IsDBNull(0))
                    {
                        chart1.Series["Guests"].Points.AddXY(bulan[i].ToString(), 0);
                    }
                    else
                    {
                        chart1.Series["Guests"].Points.AddXY(bulan[i].ToString(), reader1.GetInt32(0));
                    }
                    Connection.conn.Close();
                }
            }
        }
    }
}
