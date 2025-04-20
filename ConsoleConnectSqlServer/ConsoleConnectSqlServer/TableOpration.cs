using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleConnectSqlServer.EF6;

namespace ConsoleConnectSqlServer
{
    public class TableOpration
    {
        //用户登录
        public static TableModel Login(string username, string password)
        {
            TableModel model = new TableModel();
            DataTable table;
            string sql = $"Select * from Table_1 where UserName='{username}' and PassWord='{password}'";
            table = SqlHelper.SelectData(sql);
            if (table.Rows.Count <=0)
            {
                return null;
            }

            model = TableOpration.DataRowToTableModel(table.Rows[0]);
            return model;
        }
        //查询所有用户数据
        public static List<TableModel> QureyAllUsers()
        {
            List<TableModel> users = new List<TableModel>();

            DataTable AllTable;
            string AllDataSql = "Select * from Table_1";
            AllTable = SqlHelper.SelectData(AllDataSql);

            for(int i=0; i<AllTable.Rows.Count; i++)
            {
                TableModel model = TableOpration.DataRowToTableModel(AllTable.Rows[i]);
                users.Add(model);
            }

            return users;
        }

        //public static void CollectGender(TableModel userTM)
        public static void CollectGender(TableModel userTM)
        {
            //获取收集数据
            string gender = userTM.Gender;

            if (string.IsNullOrEmpty(gender))
            {
                //收集用户的性别
                InfoHelper.Info7 = InfoHelper.Info7.Replace("@NickName", userTM.NickName);
                Console.WriteLine(InfoHelper.Info7);
                while (true)
                {
                    Console.WriteLine(InfoHelper.Info8);
                    Console.WriteLine(InfoHelper.Info9);
                    gender = Console.ReadLine();
                    if (gender == "1")
                    {
                        //gender = "男";
                    }
                    else if (gender == "2")
                    {
                        //gender = "女";
                    }
                    else
                    {
                        continue;
                    }

                    string updateSql = $"update Table_1 set Gender='{gender}' where UserName='{userTM.UserName}'";
                    int EditdataResult = SqlHelper.EditData(updateSql);
                    if (EditdataResult <= 0)
                    {
                        Console.WriteLine(InfoHelper.Info10);
                    }
                    else
                    {
                        Console.WriteLine(InfoHelper.Info11);
                    }
                    break;
                }
            }
        }


        public static TableModel DataRowToTableModel(DataRow row)
        {
            TableModel model = new TableModel();
            model.UserName = row["UserName"].ToString();
            model.PassWord = row["PassWord"].ToString();
            model.NickName = row["NickName"].ToString();
            model.Gender   = row["Gender"].ToString();
            return model;

        }



    }
}
