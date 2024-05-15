using System.Text;
using System.IO;

namespace Prog1_project
{
    class Program
    {
        static void Main(string[] args)
        {
            Content content = new Content();
            
            MenuO menu = new MenuO();
            menu.DefaultMenu();

            Console.ReadKey();
        }
    }
}
