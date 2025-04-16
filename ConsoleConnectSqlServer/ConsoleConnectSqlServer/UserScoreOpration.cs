using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleConnectSqlServer
{
    public class UserScoreOpration
    {
        //根据用户姓名查询成绩数据
        public static List<UserScoreTModel> GetUserScoresByUsername(string username)
        {
            List<UserScoreTModel> userscores = new List<UserScoreTModel>();

            DataTable tableUserScores;
            string sqlUserScores = $"Select * from UserScoresT where UserName='{username}'";
            tableUserScores = SqlHelper.SelectData(sqlUserScores);

            for (int i = 0; i < tableUserScores.Rows.Count; i++)
            {
                UserScoreTModel model = UserScoreOpration.DataRowToTableModel(tableUserScores.Rows[i]);
                userscores.Add(model);
            }

            return userscores;
        }
        //根据用户名查询，根据记录时间排序，查询时间最近的成绩数据
        public static UserScoreTModel GetUserScoresByUsernameOderByRecordTime(string username)
        {
            

            DataTable tableUserScores;
            string sqlUserScores = $"Select top 1 * from UserScoresT where UserName='{username}' Order by RecordTime desc";
            tableUserScores = SqlHelper.SelectData(sqlUserScores);
            if(tableUserScores.Rows.Count <= 0 )
            {
                return null;
            }
            UserScoreTModel model = UserScoreOpration.DataRowToTableModel(tableUserScores.Rows[0]);

            return model;
        }

        public static UserScoreTModel DataRowToTableModel(DataRow row)
        {
            UserScoreTModel model = new UserScoreTModel();
            model.UserName = row["UserName"].ToString();
            model.Chinese = float.Parse(row["Chinese"].ToString());
            model.English = float.Parse(row["English"].ToString());
            model.Math = float.Parse(row["Math"].ToString());
            model.RecordTime = DateTime.Parse(row["RecordTime"].ToString());
            return model;

        }


    }
}
