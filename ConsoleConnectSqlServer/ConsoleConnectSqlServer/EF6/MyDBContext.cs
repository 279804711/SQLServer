using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ConsoleConnectSqlServer.EF6
{
    public class MyDBContext:DbContext
    {
        public MyDBContext():base("Server=localhost;Database=TestDB;Trusted_Connection=true") 
        {
        
        }
        public DbSet<TableModelForEF> table_1 {  get; set; }
        public DbSet<UserScoresModelForEF> userScoreT { get; set; }

    }
}
