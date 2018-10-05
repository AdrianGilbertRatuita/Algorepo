using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager
{
    public class DataMaster
    {

        // Generate and Return Data with Decimal Equivalent of GUID at End
        public static string[] GenerateData(int Amount, bool IncludeDecimal = true)
        {

            string[] DataTable = new string[Amount];
            Random Randomizer = new Random();

            for (int i = 0; i < Amount; i++)
            {

                Guid RandomGUID = Guid.NewGuid();

                Double RandomDouble = Randomizer.NextDouble() * DateTime.Now.Second * 10;
                if (IncludeDecimal)
                {

                    DataTable[i] = $"{i + 1}, {RandomGUID}, {RandomDouble}, {GUIDToDecimal.GetDecimalValue(RandomGUID.ToString())}";

                }
                else
                {

                    DataTable[i] = $"{i + 1}, {RandomGUID}, {RandomDouble}";

                }

            }

            return DataTable;

        }


        // Output passed DataTable
        public static void OutputData(string[] DataTable)
        {

            for (int i = 0; i < DataTable.Length; i++)
            {

                Console.WriteLine(DataTable[i]);

            }

        }

        // Output passed DataTable and Elapsed Time
        public static void OutputData(string[] DataTable, double SortTime)
        {

            for (int i = 0; i < DataTable.Length; i++)
            {

                Console.WriteLine(DataTable[i]);

            }

            Console.WriteLine("==========================================");
            Console.WriteLine($"Data Sort Time: {SortTime}");

        }

        // Output passed DataTable, Elapsed Time and SortType
        public static void OutputData(string[] DataTable, double SortTime, SortType SortParameters)
        {

            for (int i = 0; i < DataTable.Length; i++)
            {

                Console.WriteLine(DataTable[i]);

            }

            Console.WriteLine("==========================================");
            Console.WriteLine($"Data Sort Time: {SortTime} ms, Sorting by {SortParameters}");

        }
        // Output passed DataTable, Elapsed Time and SortType
        public static void OutputData(string[] DataTable, double SortTime, SortType SortParameters, SortingAlgorithmType SortAlgorithm)
        {

            for (int i = 0; i < DataTable.Length; i++)
            {

                Console.WriteLine(DataTable[i]);

            }

            Console.WriteLine("==========================================");
            Console.WriteLine($"Data Sort Time: {SortTime} ms, Sorting by {SortParameters}, Using {SortAlgorithm}");

        }


    }

}
