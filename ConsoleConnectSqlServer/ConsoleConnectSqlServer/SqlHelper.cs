using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleConnectSqlServer
{
    public class SqlHelper
    {
        public static int EditData(String SQL)
        {
            int count = 0;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=localhost;Database=TestDB;Trusted_Connection=true";
            conn.Open();

            SqlCommand cmd = new SqlCommand(SQL, conn);

            try
            {
                count = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            conn.Close();


            return count;
        }

        public static DataTable SelectData(String SQL)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=localhost;Database=TestDB;Trusted_Connection=true";
            conn.Open();


            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = SQL;

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            DataSet ds = new DataSet();
            adapter.Fill(ds);


            DataTable table = ds.Tables[0];

            conn.Close();

            return table;

        }
    }
}
