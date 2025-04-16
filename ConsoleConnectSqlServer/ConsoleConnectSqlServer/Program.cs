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
            //选择语言
            InfoHelper.ChangeLanguage();




            //用户登录
            Console.WriteLine(InfoHelper.Info1);
            Console.WriteLine(InfoHelper.Info2);

            String InputUserName = "";
            String InputPassWord = "";
            
            
            TableModel userTM = new TableModel();


            while(true) 
            {
                Console.Write(InfoHelper.Info3);
                InputUserName = Console.ReadLine();

                Console.Write(InfoHelper.Info4);
                InputPassWord = Console.ReadLine();

                userTM = TableOpration.Login(InputUserName, InputPassWord);
                if (userTM == null)
                {
                    Console.WriteLine(InfoHelper.Info5);
                    continue;
                }
                break;
            } 




            //欢迎页面
            //Console.WriteLine("欢迎您登录本系统，尊敬的{0}!!!", table.Rows[0]["NickName"]);
            //Console.WriteLine($"欢迎您登录本系统，尊敬的{table.Rows[0]["NickName"]}!!!");
            InfoHelper.Info6 = InfoHelper.Info6.Replace("@NickName", userTM.NickName);
            Console.WriteLine(InfoHelper.Info6);

            //获取收集数据
            TableOpration.CollectGender(userTM);
            
            //获取数据库表格
            List<TableModel> users = TableOpration.QureyAllUsers();

            Console.WriteLine(InfoHelper.Info12);

            //打印数据库表格
            for (int i=0; i<users.Count; i++)
            {
                //获取用户成绩表
                UserScoreTModel userScores = UserScoreOpration.GetUserScoresByUsernameOderByRecordTime(users[i].UserName);

                


                string genderForSql = users[i].Gender;
                if (genderForSql == "1")
                {
                    genderForSql = InfoHelper.Gender1;
                }
                else if(genderForSql == "2")
                {
                    genderForSql = InfoHelper.Gender2;
                }

                if (userScores == null)
                {
                    Console.WriteLine($"{users[i].UserName} {users[i].NickName} {genderForSql} ");
                    continue;
                }
                Console.WriteLine($"{users[i].UserName} {users[i].NickName} {genderForSql} {userScores.Chinese} {userScores.English} {userScores.Math} {userScores.RecordTime}");
                //for (int j = 0; j < userScores.Count; j++)
                //{
                //    Console.WriteLine($"{users[i].UserName} {users[i].NickName} {genderForSql} {userScores[j].Chinese} {userScores[j].English} {userScores[j].Math} {userScores[j].RecordTime}");
                //}

            }








            Console.ReadKey();
        }

        

    }
}
