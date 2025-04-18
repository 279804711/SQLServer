using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleConnectSqlServer
{
    public class UserAndUserScoresOpration
    {
        public static List<UserAndUserScoresModel> GetDataList()
        {
            List<UserAndUserScoresModel> users = new List<UserAndUserScoresModel>();

            DataTable AllTable;
            string AllDataSql = "select s.UserName, s.PassWord, s.NickName, s.Gender, u.Chinese, u.English, u.Math, u.RecordTime from Table_1 as s left join UserScoresT as u on s.UserName = u.UserName";
            AllTable = SqlHelper.SelectData(AllDataSql);

            for (int i = 0; i < AllTable.Rows.Count; i++)
            {
                UserAndUserScoresModel model = UserAndUserScoresOpration.DataRowToModel(AllTable.Rows[i]);
                users.Add(model);
            }

            return users;
        }

        public static List<UserAndUserScoresModel> Get1FirstDataList()
        {
            List<UserAndUserScoresModel> users = new List<UserAndUserScoresModel>();

            DataTable AllTable;
            string AllDataSql = "select s.UserName, s.PassWord, s.NickName, s.Gender, u.Chinese, u.English, u.Math, u.RecordTime from Table_1 as s\r\nleft join\r\n(select UserScoresT.* from UserScoresT inner join\r\n(select UserName,Max(RecordTime) as RecordTime from UserScoresT\r\ngroup by UserName) as groupt\r\non UserScoresT.UserName = groupt.UserName and UserScoresT.RecordTime = groupt.RecordTime) as u\r\non s.UserName = u.UserName";
            AllTable = SqlHelper.SelectData(AllDataSql);

            for (int i = 0; i < AllTable.Rows.Count; i++)
            {
                UserAndUserScoresModel model = UserAndUserScoresOpration.DataRowToModel(AllTable.Rows[i]);
                users.Add(model);
            }

            return users;
        }

        public static UserAndUserScoresModel DataRowToModel(DataRow row)
        {
            UserAndUserScoresModel model = new UserAndUserScoresModel();
            model.UserName = row["UserName"].ToString();
            model.PassWord = row["PassWord"].ToString();
            model.NickName = row["NickName"].ToString();
            model.Gender   = row["Gender"].ToString();
            model.Chinese  = string.IsNullOrEmpty(row["Chinese"].ToString()) ? 0 : float.Parse(row["Chinese"].ToString());
            model.English  = string.IsNullOrEmpty(row["English"].ToString()) ? 0 : float.Parse(row["English"].ToString());
            model.Math     = string.IsNullOrEmpty(row["Math"].ToString()) ? 0 : float.Parse(row["Math"].ToString());
            if(string.IsNullOrEmpty(row["RecordTime"].ToString()))
            {
                model.RecordTime = null;
            }
            else
            {
                model.RecordTime = DateTime.Parse(row["RecordTime"].ToString());
            }
            
            
            return model;

        }


    }
}
