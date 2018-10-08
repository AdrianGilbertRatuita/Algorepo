using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DataManager.SortingAlgorithms
{
    public class InsertSorter
    {
        public static string[] IDSort(string[] DataTable)
        {
            Stopwatch stopwatch = new Stopwatch();
            string[] sortedDataTable = DataTable;
            stopwatch.Start();

            for (int i = 1; i < DataTable.Length; i++)
            {

                for (int j = i; j > 0 && Convert.ToInt16(sortedDataTable[j].Split(',')[0]) < Convert.ToInt16(sortedDataTable[j - 1].Split(',')[0]); j--)
                {
                    string lesserItem = sortedDataTable[j].Split(',')[0];
                    string greaterItem = sortedDataTable[j - 1].Split(',')[0];

                    sortedDataTable[j].Split(',')[0] = greaterItem;
                    sortedDataTable[j - 1].Split(',')[0] = lesserItem;
                }

            }

            stopwatch.Stop();
            DataMaster.OutputData(sortedDataTable, stopwatch.ElapsedMilliseconds, SortType.ID, SortingAlgorithmType.INSERTIONSORT);

            return sortedDataTable;
        }

        public static string[] GUIDSort(string[] DataTable)
        {
            Stopwatch stopwatch = new Stopwatch();
            string[] sortedDataTable = DataTable;
            stopwatch.Start();

            for (int i = 1; i < DataTable.Length; i++)
            {
                for (int j = i; j > 0 && GUIDToDecimal.GetDecimalValue(DataTable[j].Split(',')[1]) < GUIDToDecimal.GetDecimalValue(DataTable[j - 1].Split(',')[1]); j--)
                {
                    string lesserItem = sortedDataTable[j].Split(',')[1];
                    string greaterItem = sortedDataTable[j - 1].Split(',')[1];

                    sortedDataTable[j].Split(',')[1] = greaterItem;
                    sortedDataTable[j - 1].Split(',')[1] = lesserItem;
                }
            }

            stopwatch.Stop();
            DataMaster.OutputData(sortedDataTable, stopwatch.ElapsedMilliseconds, SortType.GUID, SortingAlgorithmType.INSERTIONSORT);

            return sortedDataTable;
        }

        public static string[] DoubleSort(string[] DataTable)
        {
            Stopwatch stopwatch = new Stopwatch();
            string[] sortedDataTable = DataTable;
            stopwatch.Start();

            for (int i = 1; i < DataTable.Length; i++)
            {


                for (int j = i; j > 0 && Convert.ToDouble(sortedDataTable[j].Split(',')[2]) < Convert.ToDouble(sortedDataTable[j - 1].Split(',')[2]); j--)
                {
                    string lesserItem = sortedDataTable[j].Split(',')[2];
                    string greaterItem = sortedDataTable[j - 1].Split(',')[2];

                    sortedDataTable[j].Split(',')[2] = greaterItem;
                    sortedDataTable[j - 1].Split(',')[2] = lesserItem;
                }
            }

            stopwatch.Stop();
            DataMaster.OutputData(sortedDataTable, stopwatch.ElapsedMilliseconds, SortType.DOUBLE, SortingAlgorithmType.INSERTIONSORT);

            return sortedDataTable;

        }
    }
}
