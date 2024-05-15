using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using System.Runtime.Intrinsics.Arm;

namespace Prog1_project
{
    public class Bank
    {
        Stream stream = new Stream();

        public static string infoname;
        public static int infoacnum;
        public static string originalline;

        public Bank() { }

        public void create_account()
        {
            Console.Clear();
            Content content = new Content();
            string Name;
            bool val = false;
            do
            {
                Console.Write("Adja meg a nevét: ");
                Name = Console.ReadLine();
                foreach (var item in Content.Items)
                {
                    if (item.Name == Name)
                    {
                        val = true;
                        Console.WriteLine("Ez a név már létezik");
                        break;
                    }
                }
            } while (val);
            DateTime dateofcreate = DateTime.Now;
            Random random = new Random();

            int Account_number;
            bool valid = false;
            do
            {
                Account_number = random.Next(100000, 999999);
                foreach (var item in Content.Items)
                {
                    if (item.Account_number == Account_number)
                    {
                        valid = true;
                        break;
                    }
                }
            } while (valid);

            int p = 0;
            do
            {
                Console.Write("Adjon meg egy PIN kódot: ");
            } while (!int.TryParse(Console.ReadLine(), out p) || p <= 0);

            string type;
            do
            {
                Console.Write("Milyen számlátt szeretne nyitni(csekkszámla vagy takarékszámla)?: ");
                type = Console.ReadLine();
            }
            while (type != "csekkszámla" && type != "takarékszámla");

            int Balance = 0;
            Console.Clear();
            string line = "Név: " + Name + "\n" + "Létrehozás időpontja: " + dateofcreate + "\n" + "Számlaszáma: " + Account_number + "\n" + "PIN: " + p + "\n" + "Egyenleg: " + Balance.ToString() + "\n" + "Tipus: " + type;
            string split = ";";
            string linetowrite = Name + split + dateofcreate + split + Account_number + split + p + split + Balance.ToString() + split + type;
            stream.WriteToFile(linetowrite, "content.txt");
            MenuO menuO = new MenuO();
            menuO.EndMenu(line);
        }

        public void get_balance()
        {
            Content content = new Content();
            Console.Clear();
            string name;
            bool val = false;
            do
            {
                Console.Write("Adja meg a nevét: ");
                name = Console.ReadLine();
                string l = "";
                foreach (var item in Content.Items)
                {
                    if (item.Name != name)
                    {
                        val = true;
                        l = "Ez a név nem létezik";
                    }
                    else
                    {
                        val = false;
                        l = "";
                        break;
                    }
                }
                if (l.Length > 1)
                {
                    Console.WriteLine(l);
                }
            } while (val);
            int p;
            do
            {
                Console.Write("Adja meg a PIN kódot: ");
            } while (!int.TryParse(Console.ReadLine(), out p) || p <= 0);

            var q = from item in Content.Items
                    where item.PIN == p && item.Name == name
                    select item.Balance;
            string line;
            try
            {
                line = "A számlán található egyenleg: " + q.First().ToString() + " Ft";
            }
            catch (Exception)
            {
                line = "Hibás név vagy jelszó";
            }

            MenuO menuO = new MenuO();
            menuO.EndMenu(line);

        }

        public void deposit()
        {
            Content content = new Content();
            Console.Clear();
            string name;
            bool val = false;
            do
            {
                Console.Write("Adja meg a nevét: ");
                name = Console.ReadLine();
                string l = "";
                foreach (var item in Content.Items)
                {
                    if (item.Name != name)
                    {
                        val = true;
                        l = "Ez a név nem létezik";
                    }
                    else
                    {
                        val = false;
                        l = "";
                        break;
                    }
                }
                if (l.Length > 1)
                {
                    Console.WriteLine(l);
                }
            } while (val);
            int p;
            do
            {
                Console.Write("Adja meg a PIN kódot: ");
            } while (!int.TryParse(Console.ReadLine(), out p) || p <= 0);
            int dep;
            do
            {
                Console.Write("Adja meg a feltölteni kívánt összeget: ");
                
            } while (!int.TryParse(Console.ReadLine(), out dep) || dep <= 0);
            
            string line = "";
            try
            {
                double newmoney = 0;
                char split = ';';
                for (int i = 0; i < Content.Items.Count; ++i)
                {
                    string sname = Content.Items[i].Name;
                    DateTime date = Content.Items[i].dateofcreate;
                    int acnum = Content.Items[i].Account_number;
                    int spin = Content.Items[i].PIN;
                    int smoney = Content.Items[i].Balance;
                    string type = Content.Items[i].Type;
                    if (sname == name && spin == p)
                    {
                        newmoney = (smoney + dep) - ((dep * 0.6) / 100);
                        Content.Items[i] = new Account(sname + split + date + split + acnum + split + spin + split + newmoney + split + type);
                        line = "A feltöltés sikeres";
                        stream.WriteToFile(sname + split + DateTime.Now + split + acnum + split + newmoney + split + (dep - ((dep * 0.6) / 100)).ToString() + split + "in", "Data.txt");
                        stream.RefreshContentFile();

                        foreach (var item in Content.Items)
                        {
                            Console.WriteLine(item.Name);
                        }
                        break;
                    }
                }
            }
            catch (Exception)
            {
                line = "Nem sikerült a feltöltés";
            }

            MenuO menuO = new MenuO();
            menuO.EndMenu(line);
        }

        public void cashout()
        {
            Content content = new Content();
            Console.Clear();
            string name;
            bool val = false;
            do
            {
                Console.Write("Adja meg a nevét: ");
                name = Console.ReadLine();
                string l = "";
                foreach (var item in Content.Items)
                {
                    if (item.Name != name)
                    {
                        val = true;
                        l = "Ez a név nem létezik";
                    }
                    else
                    {
                        val = false;
                        l = "";
                        break;
                    }
                }
                if (l.Length > 1)
                {
                    Console.WriteLine(l);
                }
            } while (val);
            int p;
            do
            {
                Console.Write("Adja meg a PIN kódot: ");
            } while (!int.TryParse(Console.ReadLine(), out p) || p <= 0);
            int cashout;
            do
            {
                Console.Write("Adja meg a felvenni kívánt összeget: ");

            } while (!int.TryParse(Console.ReadLine(), out cashout) || cashout <= 0);

            string line = "";
            try
            {
                double newmoney = 0;
                char split = ';';
                for (int i = 0; i < Content.Items.Count; ++i)
                {
                    string sname = Content.Items[i].Name;
                    DateTime date = Content.Items[i].dateofcreate;
                    int acnum = Content.Items[i].Account_number;
                    int spin = Content.Items[i].PIN;
                    int smoney = Content.Items[i].Balance;
                    string type = Content.Items[i].Type;
                    if (sname == name && spin == p)
                    {
                        if (smoney > cashout + ((cashout * 0.6) / 100))
                        {
                            newmoney = (smoney - cashout) - ((cashout * 0.6) / 100);
                            Content.Items[i] = new Account(sname + split + date + split + acnum + split + spin + split + newmoney + split + type);
                            line = "A felvétel sikeres";
                            stream.WriteToFile(sname + split + DateTime.Now + split + acnum + split + newmoney + split + (cashout - ((cashout * 0.6) / 100)).ToString() + split + "out", "Data.txt");
                            stream.RefreshContentFile();
                            break;
                        }
                        else
                        {
                            line = "A számlán nincs elég pénz a kivánt összeg felvételéhez";
                        }
                    }
                }
            }
            catch (Exception)
            {
                line = "Nem sikerült a felvétel";
            }

            MenuO menuO = new MenuO();
            menuO.EndMenu(line);
        }

        public void transaction() 
        {
            Content content = new Content();
            Console.Clear();
            string name;
            bool val = false;
            do
            {
                Console.Write("Adja meg a nevét: ");
                name = Console.ReadLine();
                string l = "";
                foreach (var item in Content.Items)
                {
                    if (item.Name != name)
                    {
                        val = true;
                        l = "Ez a név nem létezik";
                    }
                    else
                    {
                        val = false;
                        l = "";
                        break;
                    }
                }
                if (l.Length > 1)
                {
                    Console.WriteLine(l);
                }
            } while (val);
            int p;
            do
            {
                Console.Write("Adja meg a PIN kódot: ");
            } while (!int.TryParse(Console.ReadLine(), out p) || p <= 0);

            string kname;
            bool val1 = false;
            do
            {
                Console.Write("Adja meg a kedvezményezet nevét: ");
                kname = Console.ReadLine();
                string l = "";
                foreach (var item in Content.Items)
                {
                    if (item.Name != kname)
                    {
                        val1 = true;
                        l = "Ez a név nem létezik";
                    }
                    else
                    {
                        val1= false;
                        l = "";
                        break;
                    }
                }
                if (l.Length > 1)
                {
                    Console.WriteLine(l);
                }
            } while (val1);
            int knum;
            do
            {
                Console.Write("Adja meg a kedvezményezet számlaszámát: ");
            } while (!int.TryParse(Console.ReadLine(), out knum) || knum <= 0);
            int amount ;
            do
            {
                Console.Write("Adja meg az átutalni kívánt összeget: ");

            } while (!int.TryParse(Console.ReadLine(), out amount) || amount <= 0);
            string line = "";
            try
            {
                double newmoney = 0;
                char split = ';';
                for (int i = 0; i < Content.Items.Count; ++i)
                {
                    string sname = Content.Items[i].Name;
                    DateTime date = Content.Items[i].dateofcreate;
                    int acnum = Content.Items[i].Account_number;
                    int spin = Content.Items[i].PIN;
                    int smoney = Content.Items[i].Balance;
                    string type = Content.Items[i].Type;
                    
                    if (sname == name && spin == p)
                    {
                        if (smoney > amount + ((amount * 0.6) / 100))
                        {
                            newmoney = (smoney - amount) - ((amount * 0.6) / 100);
                            Content.Items[i] = new Account(sname + split + date + split + acnum + split + spin + split + newmoney + split + type);
                            stream.WriteToFile(sname + split + DateTime.Now + split + acnum + split + newmoney + split + (amount - ((amount * 0.6) / 100)).ToString() + split + "out", "Data.txt");
                            line = "A Tranzakció sikeres";
                            break;
                        }
                        else
                        {
                            line = "A számlán nincs elég pénz a kivánt összeg felvételéhez";
                        }
                    }
                }

                for (int i = 0; i < Content.Items.Count; ++i)
                {
                    string knam = Content.Items[i].Name;
                    DateTime kdate = Content.Items[i].dateofcreate;
                    int kacnum = Content.Items[i].Account_number;
                    int kpin = Content.Items[i].PIN;
                    int kmoney = Content.Items[i].Balance;
                    string ktype = Content.Items[i].Type;
                    if (knam == kname && knum == kacnum)
                    {
                        newmoney = kmoney + amount;
                        Content.Items[i] = new Account(knam + split + kdate + split + kacnum + split + kpin + split + newmoney + split + ktype);
                        stream.WriteToFile(knam + split + DateTime.Now + split + kacnum + split + newmoney + split + amount + split + "in", "Data.txt");
                        line += "\n" + knam + " nevü felhasználó sikeresen megkapta az összeget";
                        break;
                    }
                }

                stream.RefreshContentFile();
            }
            catch (Exception)
            {
                line = "Nem sikerült az átutalás";
            }

            MenuO menuO = new MenuO();
            menuO.EndMenu(line);
        }

        public void accountinfo() 
        {
            SpendsData spendsData = new SpendsData();
            Content content = new Content();
            Console.Clear();
            string name;
            bool val = false;
            do
            {
                Console.Write("Adja meg a nevét: ");
                name = Console.ReadLine();
                string l = "";
                foreach (var item in Content.Items)
                {
                    if (item.Name != name)
                    {
                        val = true;
                        l = "Ez a név nem létezik";
                    }
                    else
                    {
                        val = false;
                        l = "";
                        break;
                    }
                }
                if (l.Length > 1)
                {
                    Console.WriteLine(l);
                }
            } while (val);
            int p;
            do
            {
                Console.Write("Adja meg a PIN kódot: ");
            } while (!int.TryParse(Console.ReadLine(), out p) || p <= 0);

            Console.WriteLine("Fiók adatai");
            string line = "Fiók adatai \n\n";
            try
            {
                double newmoney = 0;
                char split = ';';
                for (int i = 0; i < Content.Items.Count; ++i)
                {
                    string sname = Content.Items[i].Name;
                    DateTime date = Content.Items[i].dateofcreate;
                    int acnum = Content.Items[i].Account_number;
                    int spin = Content.Items[i].PIN;
                    int smoney = Content.Items[i].Balance;
                    string type = Content.Items[i].Type;
                    if (sname == name && spin == p)
                    {
                        line += "Fiók neve: "+sname+"\n"+"Létrehozás dátuma: " + date +"\n"+"Számlaszáma: "+acnum+"\n"+ "PIN kód: " + spin + "\n" +"Egyenleg: " + smoney + " Ft"+"\n"+ "Fiók típusa: "+ type;
                        infoname = name;
                        infoacnum = acnum;
                        break;
                    }
                }
            }
            catch (Exception)
            {
                line = "Nem sikerült a lekérdezés";
            }
            originalline = line;
            MenuO menuO = new MenuO();
            menuO.InfoMenu(line);
        }

        public void Out() 
        {
            string line = "Kiadások: \n\n";
            try
            {
                for (int i = 0; i < SpendsData.Items.Count; ++i)
                {
                    string name = SpendsData.Items[i].Name;
                    int acnum = SpendsData.Items[i].Account_number;
                    if (name == infoname && acnum == infoacnum)
                    {
                        var q = from item in SpendsData.Items
                                where item.Account_number == acnum && item.Name == name && item.Type == "out"
                                select new { item.Name, item.date, item.Account_number, item.Balance, item.LockedMoney };
                        if (q.Count() > 0)
                        {
                            foreach (var item in q)
                            {
                                line += "Név: " + item.Name + " Dátum: " + item.date + " Számlaszám: " + item.Account_number + " Egyenleg: " + item.Balance + " Kimenő összeg: " + item.LockedMoney + "\n";
                            }
                        }
                        else
                        {
                            line = "Nincsenek kiadások";
                        }
                        break;
                    }
                }
            }
            catch (Exception)
            {
                line = "Nem sikerült a lekérdezés";
            }
            MenuO menu = new MenuO();
            menu.OptionalInfoMenu(line, originalline);
        }

        public void In() 
        {
            string line = "Bevétel: \n\n";
            try
            {
                for (int i = 0; i < SpendsData.Items.Count; ++i)
                {
                    string name = SpendsData.Items[i].Name;
                    int acnum = SpendsData.Items[i].Account_number;
                    string type = SpendsData.Items[i].Type;
                    if (name == infoname && acnum == infoacnum)
                    {
                        var q = from item in SpendsData.Items
                                where item.Account_number == acnum && item.Name == name && item.Type == "in"
                                select new { item.Name, item.date, item.Account_number, item.Balance, item.LockedMoney };
                        if (q.Count() > 0)
                        {
                            foreach (var item in q)
                            {
                                line += "Név: " + item.Name + " Dátum: " + item.date + " Számlaszám: " + item.Account_number + " Egyenleg: " + item.Balance + " Bejövő összeg: " + item.LockedMoney + "\n";
                            }
                        }
                        else
                        {
                            line = "Nincsenek bevételek";
                        }
                        break;
                    }
                }
            }
            catch (Exception)
            {
                line = "Nem sikerült a lekérdezés";
            }

            MenuO menu = new MenuO();
            menu.OptionalInfoMenu(line, originalline);
        }
    }
}
