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

            //
            Console.ForegroundColor = ConsoleColor.Green;

            int Total;
            bool Output = false;
            bool Generate = true;

            string[] DataTable;

            //
            Console.WriteLine("Generate File? Y/N:");
            Generate = Console.ReadLine()[0] == 'Y' ? true : false;

            if (Generate)
            {

                Console.WriteLine("Input total to generate and sort:");
                Total = int.Parse(Console.ReadLine());

                //
                Console.WriteLine("Output File? Y/N:");
                Output = Console.ReadLine()[0] == 'Y' ? true : false;

                //
                DataTable = DataMaster.GenerateData(Total);

                //
                if (Output)
                {
                    Console.WriteLine("Writing File");
                    DataIO.DataWriter(DataTable, "ARLG.csv");
                }


            }
            else
            {

                Console.WriteLine("Read file from:");
                string Filename = Console.ReadLine();
                DataTable = DataIO.DataReader(string.Empty, Filename);

            }

            bool BubbleSort = true;
            bool MergeSort = true;
            bool InsertionSort = true;
            bool QuickSort = true;

            bool OutputSorts = true;
            string[] PartitionTable;


            int Partition = 0;
            Console.WriteLine("Input total to partition:");
            Partition = int.Parse(Console.ReadLine());

            PartitionTable = new string[Partition];

            for (int i = 0; i < Partition; i++)
            {

                PartitionTable[i] = DataTable[i];

            }

            //
            Console.WriteLine("SKIP MERGE SORT? Y/N:");
            MergeSort =  Console.ReadLine().ToUpper()[0] == 'N' ? false : true;

            if (MergeSort)
            {

                //=============================================

                //
                Console.WriteLine("ID SORTED");
                PartitionTable = MergeSorter.IDSort(PartitionTable);
                if (OutputSorts)
                {

                    Console.WriteLine("Writing ID SORTED DATA with MERGE SORT");
                    DataIO.DataWriter(PartitionTable, "IDMERGE_ARLG");

                }
                Console.ReadLine();

                //
                Console.WriteLine("GUID SORTED");
                PartitionTable = MergeSorter.GUIDSort(PartitionTable);
                if (OutputSorts)
                {

                    Console.WriteLine("Writing GUID SORTED DATA with MERGE SORT");
                    DataIO.DataWriter(PartitionTable, "GUIDMERGE_ARLG");

                }
                Console.ReadLine();


                //
                Console.WriteLine("DOUBLE SORTED");
                PartitionTable = MergeSorter.DoubleSort(PartitionTable);
                if (OutputSorts)
                {

                    Console.WriteLine("Writing DOUBLE SORTED DATA with MERGE SORT");
                    DataIO.DataWriter(PartitionTable, "DOUBLEMERGE_ARLG");

                }
                Console.ReadLine();

                //=============================================

            }

            //
            Console.WriteLine("SKIP QUCIK SORT? Y/N:");
            QuickSort = Console.ReadLine().ToUpper()[0] == 'N' ? false : true;

            if (QuickSort)
            {

                //=============================================
                //
                Console.WriteLine("ID SORTED");
                PartitionTable = QuickSorter.IDSort(PartitionTable);
                if (OutputSorts)
                {

                    Console.WriteLine("Writing ID SORTED DATA with QUICK SORT");
                    DataIO.DataWriter(PartitionTable, "IDQUICK_ARLG");

                }
                Console.ReadLine();

                //
                Console.WriteLine("GUID SORTED");
                PartitionTable = QuickSorter.GUIDSort(PartitionTable);
                if (OutputSorts)
                {

                    Console.WriteLine("Writing GUID SORTED DATA with QUICK SORT");
                    DataIO.DataWriter(PartitionTable, "GUIDQUICK_ARLG");

                }
                Console.ReadLine();


                //
                Console.WriteLine("DOUBLE SORTED");
                PartitionTable = QuickSorter.DoubleSort(PartitionTable);
                if (OutputSorts)
                {

                    Console.WriteLine("Writing DOUBLE SORTED DATA with QUICK SORT");
                    DataIO.DataWriter(PartitionTable, "DOUBLEIQUICK_ARLG");

                }
                Console.ReadLine();

                //=============================================


            }

            //
            Console.WriteLine("SKIP INSERTION SORT? Y/N:");
            InsertionSort = Console.ReadLine().ToUpper()[0] == 'N' ? false : true;

            if (InsertionSort)
            {

                //=============================================
                //
                Console.WriteLine("ID SORTED");
                PartitionTable = InsertSorter.IDSort(PartitionTable);
                if (OutputSorts)
                {

                    Console.WriteLine("Writing ID SORTED DATA with INSERTION SORT");
                    DataIO.DataWriter(PartitionTable, "IDINSERTION_ARLG");

                }
                Console.ReadLine();

                //
                Console.WriteLine("GUID SORTED");
                PartitionTable = InsertSorter.GUIDSort(PartitionTable);
                if (OutputSorts)
                {

                    Console.WriteLine("Writing GUID SORTED DATA with INSERTION SORT");
                    DataIO.DataWriter(PartitionTable, "GUIDINSERTION_ARLG");

                }
                Console.ReadLine();


                //
                Console.WriteLine("DOUBLE SORTED");
                PartitionTable = InsertSorter.DoubleSort(PartitionTable);
                if (OutputSorts)
                {

                    Console.WriteLine("Writing DOUBLE SORTED DATA with INSERTION SORT");
                    DataIO.DataWriter(PartitionTable, "DOUBLEINSERTION_ARLG");

                }
                Console.ReadLine();

                //=============================================


            }

            //
            Console.WriteLine("SKIP BUBBLE SORT? Y/N:");
            BubbleSort = Console.ReadLine().ToUpper()[0] == 'N' ? false : true;

            if (BubbleSort)
            {


                //=============================================

                //
                Console.WriteLine("ID SORTED");
                PartitionTable = BubbleSorter.IDSort(PartitionTable);
                if (OutputSorts)
                {

                    Console.WriteLine("Writing ID SORTED DATA with BUBBLE SORT");
                    DataIO.DataWriter(PartitionTable, "IDBUBBLE_ARLG");

                }
                Console.ReadLine();


                //
                Console.WriteLine("GUID SORTED");
                PartitionTable = BubbleSorter.GUIDSort(PartitionTable);
                if (OutputSorts)
                {

                    Console.WriteLine("Writing ID SORTED DATA with BUBBLE SORT");
                    DataIO.DataWriter(PartitionTable, "GUIDBUBBLE_ARLG");

                }
                Console.ReadLine();


                //
                Console.WriteLine("DOUBLE SORTED");
                PartitionTable = BubbleSorter.DoubleSort(PartitionTable);
                if (OutputSorts)
                {

                    Console.WriteLine("Writing ID SORTED DATA with BUBBLE SORT");
                    DataIO.DataWriter(PartitionTable, "DOUBLEBUBBLE_ARLG");

                }
                Console.ReadLine();


                //=============================================

            }

            Console.Read();

        }
    }
}
