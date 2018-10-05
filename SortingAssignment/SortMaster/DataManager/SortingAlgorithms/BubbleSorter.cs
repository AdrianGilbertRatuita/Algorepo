using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DataManager.SortingAlgorithms
{
    public class BubbleSorter
    {

        public static string[] IDSort(string[] DataTable)
        {

            Stopwatch NewStopwatch = new Stopwatch();
            string[] SortedDatatable = DataTable;
            NewStopwatch.Start();

            for (int i = 0; i < SortedDatatable.Length; i++)
            {

                for (int j = 0; j < SortedDatatable.Length - 1; j++)
                {

                    string DataItem1 = SortedDatatable[j].Split(',')[0];
                    string DataItem2 = SortedDatatable[j + 1].Split(',')[0];

                    if (int.Parse(DataItem1) > int.Parse(DataItem2))
                    {

                        string Temp = SortedDatatable[j];
                        SortedDatatable[j] = SortedDatatable[j + 1];
                        SortedDatatable[j + 1] = Temp;

                    }

                }

            }

            NewStopwatch.Stop();
            DataMaster.OutputData(SortedDatatable, NewStopwatch.ElapsedMilliseconds, SortType.ID, SortingAlgorithmType.BUBBLESORT);

            return SortedDatatable;
        }

        public static string[] GUIDSort(string[] DataTable)
        {

            Stopwatch NewStopwatch = new Stopwatch();
            string[] SortedDatatable = DataTable;
            NewStopwatch.Start();

            for (int i = 0; i < SortedDatatable.Length; i++)
            {

                for (int j = 0; j < SortedDatatable.Length - 1; j++)
                {

                    decimal DataItem1 = GUIDToDecimal.GetDecimalValue(SortedDatatable[j].Split(',')[1]);
                    decimal DataItem2 = GUIDToDecimal.GetDecimalValue(SortedDatatable[j + 1].Split(',')[1]);

                    if (DataItem1 > DataItem2)
                    {

                        string Temp = SortedDatatable[j];
                        SortedDatatable[j] = SortedDatatable[j + 1];
                        SortedDatatable[j + 1] = Temp;

                    }

                }

            }

            NewStopwatch.Stop();
            DataMaster.OutputData(SortedDatatable, NewStopwatch.ElapsedMilliseconds, SortType.GUID, SortingAlgorithmType.BUBBLESORT);

            return SortedDatatable;
        }

        public static string[] DoubleSort(string[] DataTable)
        {

            Stopwatch NewStopwatch = new Stopwatch();
            string[] SortedDatatable = DataTable;
            NewStopwatch.Start();

            for (int i = 0; i < SortedDatatable.Length; i++)
            {

                for (int j = 0; j < SortedDatatable.Length - 1; j++)
                {

                    string DataItem1 = SortedDatatable[j].Split(',')[2];
                    string DataItem2 = SortedDatatable[j + 1].Split(',')[2];

                    if (double.Parse(DataItem1) > double.Parse(DataItem2))
                    {

                        string Temp = SortedDatatable[j];
                        SortedDatatable[j] = SortedDatatable[j + 1];
                        SortedDatatable[j + 1] = Temp;

                    }

                }

            }

            NewStopwatch.Stop();
            DataMaster.OutputData(SortedDatatable, NewStopwatch.ElapsedMilliseconds, SortType.DOUBLE, SortingAlgorithmType.BUBBLESORT);

            return SortedDatatable;
        }

    }

}
