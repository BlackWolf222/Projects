using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog1_project
{
    public class MenuO
    {
        public MenuO() { }

        public void WriteMenu(List<Menu> options, Menu selectedOption, int index, string first = "Üdvözöljük bankunknál!")
        {
            Console.Clear();
            Console.WriteLine(first);
            Console.WriteLine();
            foreach (Menu menu in options)
            {
                if (menu == selectedOption)
                {
                    Console.Write("> ");
                }
                else
                {
                    Console.Write(" ");
                }

                Console.WriteLine(menu.Name);
            }

            ConsoleKeyInfo keyinfo;
            do
            {
                keyinfo = Console.ReadKey();

                if (keyinfo.Key == ConsoleKey.DownArrow)
                {
                    if (index + 1 < options.Count)
                    {
                        index++;
                        WriteMenu(options, options[index], index, first);
                    }
                }
                if (keyinfo.Key == ConsoleKey.UpArrow)
                {
                    if (index - 1 >= 0)
                    {
                        index--;
                        WriteMenu(options, options[index], index, first);
                    }
                }
                if (keyinfo.Key == ConsoleKey.Enter)
                {
                    options[index].Selected.Invoke();
                    index = 0;
                }
            }
            while (keyinfo.Key != ConsoleKey.X);

        }

        public void DefaultMenu()
        {
            Bank bank = new Bank();
            List<Menu> def = new List<Menu>
            {
                new Menu("Fiók létrehozása", () => bank.create_account()),
                new Menu("Számla egyenlege", () => bank.get_balance()),
                new Menu("Befizetés számlára", () => bank.deposit()),
                new Menu("Pénz felvétele", () => bank.cashout()),
                new Menu("Átutalás", () => bank.transaction()),
                new Menu("Fiók adatai", () => bank.accountinfo()),
                new Menu("Kilépés", () => Environment.Exit(0)),
            };
            int index = 0;
            WriteMenu(def, def[index], index);
        }

        public void EndMenu(string line) 
        {
            int index = 0;
            List<Menu> list = new List<Menu>();
            list = new List<Menu>
            {
                new Menu("Vissza a menübe",() => DefaultMenu()),
                new Menu("Kilépés", () => Environment.Exit(0)),
            };
            WriteMenu(list, list[index], index, line);
        }

        public void OptionalInfoMenu(string line,string originalline) 
        {
            int index = 0;
            List<Menu> list = new List<Menu>();
            list = new List<Menu>
            {
                new Menu("Vissza az Informáciokhoz",() => InfoMenu(originalline)),
                new Menu("Kilépés", () => Environment.Exit(0)),
            };
            WriteMenu(list, list[index], index, line);
        }

        public void InfoMenu(string line) 
        {
            int index = 0;
            Bank bank = new Bank();
            List<Menu> list = new List<Menu>();
            list = new List<Menu>
            {
                new Menu("Eddigi kiadások",() => bank.Out()),
                new Menu("Eddigi bejövő pénzek", () => bank.In()),
                new Menu("Vissza a menübe",() => DefaultMenu()),
                new Menu("Kilépés", () => Environment.Exit(0)),
            };
            WriteMenu(list, list[index], index, line);
        }
    }
}
