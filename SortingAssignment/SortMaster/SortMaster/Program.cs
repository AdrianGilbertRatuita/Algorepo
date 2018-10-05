using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//
using DataManager;
using DataManager.SortingAlgorithms;

namespace SortMaster
{
    class Program
    {

        static void Main(string[] args)
        {

            int Total;
            bool Output = false;

            //
            Console.WriteLine("Input total to generate and sort:");
            Total = int.Parse(Console.ReadLine());

            //
            Console.WriteLine("Output File? Y/N:");
            Output = Console.ReadLine()[0] == 'Y' ? true : false;

            //
            string[] DataTable;
            DataTable = DataMaster.GenerateData(Total);
            DataIO.DataWriter(DataTable, "ar-lg.csv");


            //=============================================

            //
            Console.WriteLine("ID SORTED");
            DataTable = MergeSorter.IDSort(DataTable);
            Console.ReadLine();

            //
            Console.WriteLine("GUID SORTED");
            DataTable = MergeSorter.GUIDSort(DataTable);
            Console.ReadLine();


            //
            Console.WriteLine("DOUBLE SORTED");
            DataTable = MergeSorter.DoubleSort(DataTable);
            Console.ReadLine();


            //=============================================

            //
            Console.WriteLine("ID SORTED");
            DataTable = BubbleSorter.IDSort(DataTable);
            Console.ReadLine();


            //
            Console.WriteLine("GUID SORTED");
            DataTable = BubbleSorter.GUIDSort(DataTable);
            Console.ReadLine();


            //
            Console.WriteLine("DOUBLE SORTED");
            DataTable = BubbleSorter.DoubleSort(DataTable);
            Console.ReadLine();


            //=============================================

            Console.Read();


        }
    }
}
