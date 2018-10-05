using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager
{
    public class DataIO
    {

        public static void DataWriter(string[] DataTable, string FileName)
        {

            System.IO.File.WriteAllLines("Data.csv", DataTable);

            //
            Console.WriteLine($"Data Write Complete to {AppDomain.CurrentDomain.BaseDirectory.ToString() + $"{FileName}.csv"}, Press any key to continue...");
            Console.ReadLine();
            Console.Clear();

        }

        public static void DataWriter(string[] DataTable, string FileName, string path)
        {

            System.IO.File.WriteAllLines(path + $"{FileName}.csv", DataTable);

            //
            Console.WriteLine($"Data Write Complete to {path}, Press any key to continue...");
            Console.ReadLine();
            Console.Clear();

        }

        public static string[] DataReader(string path, string FileName)
        {

            return System.IO.File.ReadAllLines(path + $"{FileName}.csv");

        }

        public static string[] DataReader()
        {

            return System.IO.File.ReadAllLines("Data.csv");

        }

    }
}
