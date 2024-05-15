using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog1_project
{
    public class Content 
    {
        private static List<Account> Lt = new(){};
        public static List<Account> Items { get => Lt; }

        public Content() 
        {
            Lt.Clear();
            Items.Clear();

            Stream stream = new Stream();
            
            stream.ReadFromFile(Lt);
        }

        
    }
}
