using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleConnectSqlServer
{
    public class UserScoreTModel
    {
        public string UserName {  get; set; }
        public float Chinese {  get; set; }
        public float English {  get; set; }
        public float Math {  get; set; }

        public DateTime RecordTime { get; set; }



    }
}
