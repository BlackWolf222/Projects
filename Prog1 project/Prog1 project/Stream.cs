using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;

namespace Prog1_project
{
    public class Stream
    {
        string appDirectory;
        string DataDirPath;
        string ContentFilePath;
        string DataFilePath;
        public Stream()
        {
            appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            DataDirPath = Path.Combine(appDirectory, "DataSource");
            ContentFilePath = Path.Combine(DataDirPath, "content.txt");
            DataFilePath = Path.Combine(DataDirPath, "Data.txt");
        }

        public void WriteToFile(string line, string FileName) 
        {
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string DataDirPath = Path.Combine(appDirectory, "DataSource"),
            DataFilePath = Path.Combine(DataDirPath, FileName);
            
            if (!Directory.Exists(DataDirPath))
                Directory.CreateDirectory(DataDirPath);

            using (StreamWriter streamWriter = new StreamWriter(DataFilePath, true))
            {
                streamWriter.WriteLine(line);
            }
        }
        public void ReadFromFile(List<Account> list) 
        {
            if(!Path.Exists(ContentFilePath))
                File.Create(ContentFilePath).Close();
            try
            {
                if (ContentFilePath == null)
                {
                    throw new ArgumentNullException("Path does not exist.");
                }
                if (ContentFilePath.Length == 0)
                {
                    throw new ArgumentException("Argument is empty.");
                }
                using (StreamReader streamReader = new StreamReader(ContentFilePath))
                {
                    string item;
                    while ((item = streamReader.ReadLine()) != null)
                        list.Add(new Account(item));
                }
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine(ane.Message);
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
            }
            catch (FileNotFoundException fnfe)
            {
                Console.WriteLine(fnfe.Message);
            }
        }
        
        public void RefreshContentFile() 
        {
            char split = ';';
            using StreamWriter streamWriter = new StreamWriter(ContentFilePath,false);
            {
                foreach (var item in Content.Items)
                {
                    streamWriter.WriteLine(item.Name + split + item.dateofcreate + split + item.Account_number + split + item.PIN + split + item.Balance + split + item.Type);
                }
            }

        }

        public void ReadDataFile(List<Spends> list)
        {
            
            if (!Path.Exists(DataFilePath))
                File.Create(DataFilePath).Close();
            try
            {
                if (DataFilePath == null)
                {
                    throw new ArgumentNullException("Path does not exist.");
                }
                if (DataFilePath.Length == 0)
                {
                    throw new ArgumentException("Argument is empty.");
                }
                using (StreamReader streamReader = new StreamReader(DataFilePath))
                {
                    string item;
                    while ((item = streamReader.ReadLine()) != null)
                        list.Add(new Spends(item));
                }
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine(ane.Message);
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
            }
            catch (FileNotFoundException fnfe)
            {
                Console.WriteLine(fnfe.Message);
            }
        }
    }
}
