using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Prog1_project
{
    public class Spends
    {
        public string Name;
        public DateTime date;
        public int Account_number;
        public int Balance;
        public int LockedMoney;
        public string Type;

        public Spends(string line) 
        {
            string[] strings = line.Split(';');
            Name = strings[0];
            date = DateTime.Parse(strings[1]);
            Account_number = Convert.ToInt32(strings[2]);
            Balance = Convert.ToInt32(strings[3]);
            LockedMoney = Convert.ToInt32(strings[4]);
            Type = strings[5];
        }
    }
}
