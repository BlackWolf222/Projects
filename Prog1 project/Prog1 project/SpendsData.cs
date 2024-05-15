using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog1_project
{
    public class SpendsData
    {
        private static List<Spends> Lt = new() { };
        public static List<Spends> Items { get => Lt; }
        public SpendsData() 
        {
            Lt.Clear();
            Items.Clear();

            Stream stream = new Stream();

            stream.ReadDataFile(Lt);
        }
    }
}
