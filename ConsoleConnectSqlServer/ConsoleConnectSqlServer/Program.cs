using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleConnectSqlServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
         
            String userName = "111";
            String passWord = "111";
            String nickName = "111";
            String insertSQL = $"insert into Table_1 values('{userName}','{passWord}','{nickName}');";
            String deleteSQL = $"delete from Table_1 where UserName='{userName}';";

            //增、删、改
            int count = EditData(deleteSQL);

            if(count > 0)
            {
                Console.WriteLine("执行成功，受影响的行数是：{0}", count);
            }
            else
            {
                Console.WriteLine("执行失败");
            }
            //查询
            String selectSQL = "select * from Table_1;";
            DataTable table = SelectData(selectSQL);

            for (int i = 0; i < table.Rows.Count; i++)
            {
                Console.WriteLine($"{table.Rows[i]["UserName"]}  {table.Rows[i]["PassWord"]}  {table.Rows[i]["NickName"]}");
            }

            ////提交SQL语句
            ////得到返回数据
            ////展示




            Console.ReadKey();
        }

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

            conn.Close() ;

            return table;

        }

    }
}
