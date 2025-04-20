using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleConnectSqlServer.EF6;

namespace ConsoleConnectSqlServer
{
    internal class Program
    {
        static void Main(string[] args)
        {




            //using(MyDBContext myDB = new MyDBContext())
            //{
            //    var lstUsers = myDB.table_1.ToList();
            //  
            //}



            //MyDBContext myDB = new MyDBContext();
            //var lstUsers = myDB.table_1.Where(e => e.UserName == "777").ToList();
            //var lstUsers = myDB.table_1.Where(e => e.UserName == "777").ToString();
            //List <TableModelForEF> lstUsers = myDB.table_1.ToList();

            //myDB.Dispose();

            //LINQ方法结合Lambda表达式简化
            List<string> litName = new List<String>() {"张三","李四","王五","赵六" };
            //List<string> litMatch = new List<string>();
            //for(int i = 0; i<litName.Count;i++)
            //{
            //    if(litName[i] == "王五")
            //    {
            //        litMatch.Add(litName[i]);
            //    }
            //}
            //List<string> litMatch = litName.Where(n => n == "王五").ToList();




            //选择语言
            InfoHelper.ChangeLanguage();




            //用户登录
            Console.WriteLine(InfoHelper.Info1);
            Console.WriteLine(InfoHelper.Info2);

            String InputUserName = "";
            String InputPassWord = "";
            
            
            //TableModel userTM = new TableModel();
            TableModelForEF userTM = new TableModelForEF();

            while(true) 
            {
                Console.Write(InfoHelper.Info3);
                InputUserName = Console.ReadLine();

                Console.Write(InfoHelper.Info4);
                InputPassWord = Console.ReadLine();

                //userTM = TableOpration.Login(InputUserName, InputPassWord);
                using(MyDBContext myDB = new MyDBContext())
                {
                   userTM = myDB.table_1.FirstOrDefault(e=>e.UserName == InputUserName && e.PassWord == InputPassWord);
                }


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
            //TableOpration.CollectGender(userTM);

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

                    using(MyDBContext myDB = new MyDBContext())
                    {
                        //TableModelForEF CurrentLoginUserTM = myDB.table_1.FirstOrDefault(n => n.UserName == userTM.UserName);
                        //CurrentLoginUserTM.Gender = gender;

                        myDB.table_1.Attach(userTM);
                        myDB.Entry(userTM).State = System.Data.Entity.EntityState.Modified;
                        userTM.Gender = gender;

                        myDB.SaveChanges();



                    }






                    //string updateSql = $"update Table_1 set Gender='{gender}' where UserName='{userTM.UserName}'";
                    //int EditdataResult = SqlHelper.EditData(updateSql);
                    //if (EditdataResult <= 0)
                    //{
                    //    Console.WriteLine(InfoHelper.Info10);
                    //}
                    //else
                    //{
                    //    Console.WriteLine(InfoHelper.Info11);
                    //}
                    break;
                }
            }

            //1、先查询UserT、再查询UserScoresT
            //1)展示所有的UserScoresT
            //2)展示最新的UserScoresT
            using(MyDBContext myDB = new MyDBContext())
            {
                List<TableModelForEF> lstAllUser = myDB.table_1.ToList();

                

                for(int i = 0; i < lstAllUser.Count; i++)
                {
                    string currentUserName =lstAllUser[i].UserName;

                    UserScoresModelForEF surrentScore = myDB.userScoreT.Where(e=> e.UserName == currentUserName).OrderByDescending(e=>e.RecordTime).FirstOrDefault();
                    
                    string genderForSql = lstAllUser[i].Gender;
                    if (genderForSql == "1")
                    {
                        genderForSql = InfoHelper.Gender1;
                    }
                    else if (genderForSql == "2")
                    {
                        genderForSql = InfoHelper.Gender2;
                    }
                    if (surrentScore != null)
                    {
                        Console.WriteLine($"{lstAllUser[i].UserName} {lstAllUser[i].NickName} {genderForSql} {surrentScore.Chinese} {surrentScore.English} {surrentScore.Math} {surrentScore.RecordTime}");
                    }
                    else
                    {
                        Console.WriteLine($"{lstAllUser[i].UserName} {lstAllUser[i].NickName} {genderForSql}");
                    }

                        //List <UserScoresModelForEF> lstAllUserScores = myDB.userScoreT.Where(e => e.UserName == currentUserName).ToList();

                        //foreach(UserScoresModelForEF item in lstAllUserScores)
                        //{
                        //    string genderForSql = lstAllUser[i].Gender;
                        //    if (genderForSql == "1")
                        //    {
                        //        genderForSql = InfoHelper.Gender1;
                        //    }
                        //    else if (genderForSql == "2")
                        //    {
                        //        genderForSql = InfoHelper.Gender2;
                        //    }
                        //    Console.WriteLine($"{lstAllUser[i].UserName} {lstAllUser[i].NickName} {genderForSql} {item.Chinese} {item.English} {item.Math} {item.RecordTime}");
                        //}
                    }
            }
            //2、连表查询
            //1)展示所有的UserScoresT
            //2)展示最新的UserScoresT

            using(MyDBContext myDB = new MyDBContext())
            {
                var query = from usert in myDB.table_1
                            join userscorest in myDB.userScoreT
                            on usert.UserName equals userscorest.UserName
                            into utus
                            //from userscorest in utus.DefaultIfEmpty()
                            from userscorest in utus.OrderByDescending(e=>e.RecordTime).Take(1).DefaultIfEmpty()
                            select new UserTAndUserScoresTModelForEF
                            {
                                UserName = usert.UserName,
                                Gender = usert.Gender,
                                NickName = usert.NickName,
                                PassWord = usert.PassWord,
                                Chinese = userscorest.Chinese,
                                English = userscorest.English,
                                Math = userscorest.Math,
                                RecordTime = userscorest.RecordTime,
                            };
                List<UserTAndUserScoresTModelForEF> lst = query.ToList();

            }





            //获取数据表
            //List<UserAndUserScoresModel> users = UserAndUserScoresOpration.Get1FirstDataList();
            //Console.WriteLine(InfoHelper.Info12);


            //for (int i = 0; i < users.Count; i++)
            //{
            //    string genderForSql = users[i].Gender;
            //    if (genderForSql == "1")
            //    {
            //        genderForSql = InfoHelper.Gender1;
            //    }
            //    else if (genderForSql == "2")
            //    {
            //        genderForSql = InfoHelper.Gender2;
            //    }
            //    Console.WriteLine($"{users[i].UserName} {users[i].NickName} {genderForSql} {users[i].Chinese} {users[i].English} {users[i].Math} {users[i].RecordTime}");
            //}
            ////获取数据库表格
            //List<TableModel> users = TableOpration.QureyAllUsers();

            //Console.WriteLine(InfoHelper.Info12);

            ////打印数据库表格
            //for (int i=0; i<users.Count; i++)
            //{
            //    //获取用户成绩表
            //    UserScoreTModel userScores = UserScoreOpration.GetUserScoresByUsernameOderByRecordTime(users[i].UserName);




            //    string genderForSql = users[i].Gender;
            //    if (genderForSql == "1")
            //    {
            //        genderForSql = InfoHelper.Gender1;
            //    }
            //    else if(genderForSql == "2")
            //    {
            //        genderForSql = InfoHelper.Gender2;
            //    }

            //    if (userScores == null)
            //    {
            //        Console.WriteLine($"{users[i].UserName} {users[i].NickName} {genderForSql} ");
            //        continue;
            //    }
            //    Console.WriteLine($"{users[i].UserName} {users[i].NickName} {genderForSql} {userScores.Chinese} {userScores.English} {userScores.Math} {userScores.RecordTime}");


            //}








            Console.ReadKey();
        }

        

    }
}
