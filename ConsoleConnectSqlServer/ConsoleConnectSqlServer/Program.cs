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
            Console.WriteLine("欢迎访问系统！");
            Console.WriteLine("请在下方输入您的用户名和密码！！！");

            String InputUserName = "";
            String InputPassWord = "";
            DataTable table;
            DataTable AllTable;

            do
            {
                Console.Write("请输入用户名：");
                InputUserName = Console.ReadLine();

                Console.Write("请输入密码：");
                InputPassWord = Console.ReadLine();

                string sql = $"Select * from Table_1 where UserName='{InputUserName}' and PassWord='{InputPassWord}'";
                table = SelectData(sql);
                if (table.Rows.Count <= 0)
                {
                    Console.WriteLine("用户名或密码错误！！！");

                }
            } while (table.Rows.Count <= 0);


            //欢迎页面
            //Console.WriteLine("欢迎您登录本系统，尊敬的{0}!!!", table.Rows[0]["NickName"]);
            Console.WriteLine($"欢迎您登录本系统，尊敬的{table.Rows[0]["NickName"]}!!!");

            string gender = table.Rows[0]["Gender"].ToString();
            //获取数据
            if (string.IsNullOrEmpty(gender))
            {
                //收集用户的性别
                Console.WriteLine($"尊敬的{table.Rows[0]["NickName"]}，您的性别是空，所以需要您提供性别内容。");
                while(true)
                {
                    Console.WriteLine("请在下方输入您的性别");
                    Console.WriteLine("性别选项：1、男 2、女");
                    gender = Console.ReadLine();
                    if(gender == "1")
                    {
                        gender = "男";
                    }
                    else if(gender == "2")
                    {
                        gender = "女";
                    }else
                    { 
                        continue;
                    }

                    string updateSql = $"update Table_1 set Gender='{gender}' where UserName='{table.Rows[0]["UserName"]}'";
                    int EditdataResult = EditData(updateSql);
                    if(EditdataResult <= 0)
                    {
                        Console.WriteLine("数据收集失败，本次不做更新！");
                    }
                    else
                    {
                        Console.WriteLine("数据更新成功！");
                    }
                    break;
                }
            }
            
            //打印数据库表格
            string AllDataSql = "Select * from Table_1";
            AllTable = SelectData(AllDataSql);
            for(int i=0; i<AllTable.Rows.Count; i++)
            {
                Console.WriteLine("用户名 昵称 性别");
                Console.WriteLine($"{AllTable.Rows[i]["UserName"]} {AllTable.Rows[i]["NickName"]} {AllTable.Rows[i]["Gender"]}");
            }





            //String userName = "111";
            //String passWord = "111";
            //String nickName = "111";
            //String insertSQL = $"insert into Table_1 values('{userName}','{passWord}','{nickName}');";
            //String deleteSQL = $"delete from Table_1 where UserName='{userName}';";

            ////增、删、改
            //int count = EditData(deleteSQL);

            //if(count > 0)
            //{
            //    Console.WriteLine("执行成功，受影响的行数是：{0}", count);
            //}
            //else
            //{
            //    Console.WriteLine("执行失败");
            //}
            ////查询
            //String selectSQL = "select * from Table_1;";
            //DataTable table = SelectData(selectSQL);

            //for (int i = 0; i < table.Rows.Count; i++)
            //{
            //    Console.WriteLine($"{table.Rows[i]["UserName"]}  {table.Rows[i]["PassWord"]}  {table.Rows[i]["NickName"]}");
            //}

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
