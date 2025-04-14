using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleConnectSqlServer
{
    public class InfoHelper
    {
        public static string Info1 {  get; set; } 
        public static string Info2 { get; set; }
        public static string Info3 { get; set; }
        public static string Info4 { get; set; }
        public static string Info5 { get; set; }
        public static string Info6 { get; set; }
        public static string Info7 { get; set; }

        public static string Info8 { get; set; }
        
        public static string Info9 { get; set; }

        public static string Info10 { get; set; }

        public static string Info11 { get; set; }
        public static string Info12 { get; set; }
        


        public static string Gender1 { get; set; }
        public static string Gender2 { get;set; }

        public static void ChangeLanguage()
        {
            Console.WriteLine("1、中文 2、English");
            string flag = Console.ReadLine();
            if (flag == "2")
            {
                Info1 = "Welcome to access system!";
                Info2 = "Please input your UserName and PassWord!!!";
                Info3 = "Please input UserName:";
                Info4 = "Please input PassWord:";
                Info5 = "UserName or PassWrod Error!!!";
                Info6 = "Welcome To system honor @NickName!!!";
                Info7 = "Honor @NickName, Your gender is Null.So we need collect your gender!!!";
                Info8 = "Please Input your gender below.";
                Info9 = "Option: 1、Male 2、Female";
                Info10 = "Data collecting is failed.This time will not update your data.";
                Info11 = "Data collecting is succeed.";
                Info12 = "UserName NickName Gender";
                Gender1 = "Male";
                Gender2 = "Female";
            }
            else
            {
                Info1 = "欢迎访问系统！";
                Info2 = "请在下方输入您的用户名和密码！！！";
                Info3 = "请输入用户名：";
                Info4 = "请输入密码：";
                Info5 = "用户名或密码错误！！！";
                Info6 = "欢迎您登录本系统，尊敬的@NickName!!!";
                Info7 = "尊敬的@NickName，您的性别是空，所以需要您提供性别内容。";
                Info8 = "请在下方输入您的性别";
                Info9 = "性别选项：1、男 2、女";
                Info10 = "数据收集失败，本次不做更新！";
                Info11 = "数据更新成功！";
                Info12 = "用户名 昵称 性别";
                Gender1 = "男";
                Gender2 = "女";
            }
        }



    }
}
