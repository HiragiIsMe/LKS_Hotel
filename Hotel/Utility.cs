using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel
{
    class Connection
    {
        public static string connection = @"Data Source=LAPTOP-S8UCE514;Initial Catalog=Hotel;Integrated Security=True";

        public static SqlConnection conn = new SqlConnection(connection);

        public static DataTable getData(string query)
        {
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            conn.Close();

            return dt;
        }
    }

    class Enc
    {
        public static string getPass(string pass)
        {
            using(SHA256Managed sha = new SHA256Managed())
            {
                byte[] password = sha.ComputeHash(Encoding.UTF8.GetBytes(pass));
                string getpassword = Convert.ToBase64String(password);

                return getpassword;
            }
        }

        public static byte[] getImage(Image img)
        {
            ImageConverter converter = new ImageConverter();
            byte[] image = (byte[])converter.ConvertTo(img, typeof(byte[]));

            return image;   
        }
    }

    class Model
    {
        public static int id { set; get; }
        public static string name { set; get; }

        public static int job { set; get; }
    }

}


