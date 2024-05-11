using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QuanLyThietBi_Winform_NguyenPhuocVinh
{
    public class MySQLConnector
    {
        private string server = "localhost";
        private string database = "quanlythietbi";
        private string username = "root";
        private string password = "";
        private int port = 3306;
        private MySqlConnection connection;

        public MySqlConnection Connection { get; internal set; }

        public MySQLConnector()
        {
            Initialize();
        }

        private void Initialize()
        {
            string connectionString = $"server={server};port={port};database={database};uid={username};password={password};";
            connection = new MySqlConnection(connectionString);
        }

        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                // Xử lý lỗi kết nối
                Console.WriteLine("Lỗi kết nối đến cơ sở dữ liệu: " + ex.Message);
                return false;
            }
        }

        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                // Xử lý lỗi đóng kết nối
                Console.WriteLine("Lỗi khi đóng kết nối: " + ex.Message);
                return false;
            }
        }

        public DataTable Select(string query)
        {
            DataTable dataTable = new DataTable();
            if (this.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dataTable);
                this.CloseConnection();
            }
            return dataTable;
        }



        public void ExecuteQuery(string query)
        {
            if (this.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }
        public void ExecuteQueryWithImageParameter(string query, byte[] imageData)
        {
            string connectionString = $"server={server};database={database};uid={username};password={password};";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@HinhAnhBaoTri", SqlDbType.Image).Value = imageData;
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }


        internal object ExecuteScalar(string query)
        {
            object result = null;
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                MySqlCommand command = new MySqlCommand(query, connection);
                result = command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thực thi truy vấn: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return result;
        }



    }
}

