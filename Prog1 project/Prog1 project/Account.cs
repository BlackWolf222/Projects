using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog1_project
{
    public class Account
    {
        public string Name;
        public DateTime dateofcreate;
        public int Account_number;
        public int PIN;
        public int Balance;
        public string Type;
        public Account(string line) 
        {
            string[] strings = line.Split(';');
            Name = strings[0];
            dateofcreate = DateTime.Parse(strings[1]);
            Account_number = Convert.ToInt32(strings[2]);
            PIN = Convert.ToInt32(strings[3]);
            Balance = Convert.ToInt32(strings[4]);
            Type = strings[5];
        }
    }
}
