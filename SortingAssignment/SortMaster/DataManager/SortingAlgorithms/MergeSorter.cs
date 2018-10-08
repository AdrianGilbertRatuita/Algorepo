using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DataManager.SortingAlgorithms
{
    public class MergeSorter
    {

        public static string[] IDSort(string[] DataTable)
        {

            Stopwatch NewStopwatch = new Stopwatch();
            List<string> SortedDatatable = DataTable.ToList<string>();
            NewStopwatch.Start();

            SortedDatatable = CheckThenSplit(SortedDatatable, SortType.ID);

            NewStopwatch.Stop();
            DataMaster.OutputData(SortedDatatable.ToArray<string>(), NewStopwatch.ElapsedMilliseconds, SortType.ID, SortingAlgorithmType.MERGESORT);

            return SortedDatatable.ToArray<string>();
        }

        public static string[] GUIDSort(string[] DataTable)
        {

            Stopwatch NewStopwatch = new Stopwatch();
            List<string> SortedDatatable = DataTable.ToList<string>();
            NewStopwatch.Start();

            SortedDatatable = CheckThenSplit(SortedDatatable, SortType.GUID);

            NewStopwatch.Stop();
            DataMaster.OutputData(SortedDatatable.ToArray<string>(), NewStopwatch.ElapsedMilliseconds, SortType.GUID, SortingAlgorithmType.MERGESORT);

            return SortedDatatable.ToArray<string>();
        }

        public static string[] DoubleSort(string[] DataTable)
        {

            Stopwatch NewStopwatch = new Stopwatch();
            List<string> SortedDatatable = DataTable.ToList<string>();
            NewStopwatch.Start();

            SortedDatatable = CheckThenSplit(SortedDatatable, SortType.DOUBLE);

            NewStopwatch.Stop();
            DataMaster.OutputData(SortedDatatable.ToArray<string>(), NewStopwatch.ElapsedMilliseconds, SortType.DOUBLE, SortingAlgorithmType.MERGESORT);

            return SortedDatatable.ToArray<string>();
        }

        public static List<string> CheckThenSplit(List<string> DataList, SortType Sorting)
        {

            List<string> MergedList = new List<string>();
            List<string> ListReference = DataList;
            List<string> Left = new List<string>();
            List<string> Right = new List<string>();

            if (DataList.Count / 2 >= 1)
            {

                int Count = ListReference.Count;
                while (ListReference.Count > 0)
                {

                    Console.WriteLine($"Splitting LEFT: {Left.Count}, RIGHT: {Right.Count}");
                    if (Left.Count < Count / 2)
                    {

                        Left.Add(ListReference.First<string>());
                        ListReference.Remove(ListReference.First<string>());

                    }
                    else
                    {

                        Right.Add(ListReference.First<string>());
                        ListReference.Remove(ListReference.First<string>());

                    }


                }

                //
                if (Left.Count / 2 >= 1)
                {

                    Console.WriteLine("LeftRecursion Split");
                    Left = CheckThenSplit(Left, Sorting);

                }

                //
                if (Right.Count / 2 >= 1)
                {

                    Console.WriteLine("RightRecursion Split");
                    Right = CheckThenSplit(Right, Sorting);

                }

            }

            int FinalCount = Left.Count + Right.Count;
            while (MergedList.Count != FinalCount)
            {

                switch (Sorting)
                {

                    case SortType.ID:
                        {

                            if (Left.Count > 0 && Right.Count > 0)
                            {

                                if (int.Parse(Left.First<string>().Split(',')[0]) > int.Parse(Right.First<string>().Split(',')[0]))
                                {

                                    MergedList.Add(Right.First<string>());
                                    Right.Remove(Right.First<string>());

                                }
                                else if (int.Parse(Left.First<string>().Split(',')[0]) < int.Parse(Right.First<string>().Split(',')[0]))
                                {

                                    MergedList.Add(Left.First<string>());
                                    Left.Remove(Left.First<string>());


                                }

                            }
                            else
                            {

                                if (Left.Count != 0)
                                {

                                    while (Left.Count != 0)
                                    {

                                        MergedList.Add(Left.First<string>());
                                        Left.Remove(Left.First<string>());

                                    }

                                }
                                else if (Right.Count != 0)
                                {

                                    while (Right.Count != 0)
                                    {

                                        MergedList.Add(Right.First<string>());
                                        Right.Remove(Right.First<string>());

                                    }

                                }

                            }

                            if (Left.Count == 0 && Right.Count == 1)
                            {

                                MergedList.Add(Right.First<string>());
                                Right.Remove(Right.First<string>());

                            }
                            else if (Left.Count == 1 && Right.Count == 0)
                            {

                                MergedList.Add(Left.First<string>());
                                Left.Remove(Left.First<string>());

                            }
                            break;
                        }
                    case SortType.GUID:
                        {

                            if (Left.Count > 0 && Right.Count > 0)
                            {

                                if (GUIDToDecimal.GetDecimalValue(Left.First<string>().Split(',')[1]) > GUIDToDecimal.GetDecimalValue(Right.First<string>().Split(',')[1]))
                                {

                                    MergedList.Add(Right.First<string>());
                                    Right.Remove(Right.First<string>());

                                }
                                else if (GUIDToDecimal.GetDecimalValue(Left.First<string>().Split(',')[1]) < GUIDToDecimal.GetDecimalValue(Right.First<string>().Split(',')[1]))
                                {

                                    MergedList.Add(Left.First<string>());
                                    Left.Remove(Left.First<string>());


                                }

                            }
                            else
                            {

                                if (Left.Count != 0)
                                {

                                    while (Left.Count != 0)
                                    {

                                        MergedList.Add(Left.First<string>());
                                        Left.Remove(Left.First<string>());

                                    }

                                }
                                else if (Right.Count != 0)
                                {

                                    while (Right.Count != 0)
                                    {

                                        MergedList.Add(Right.First<string>());
                                        Right.Remove(Right.First<string>());

                                    }

                                }

                            }

                            if (Left.Count == 0 && Right.Count == 1)
                            {

                                MergedList.Add(Right.First<string>());
                                Right.Remove(Right.First<string>());

                            }
                            else if (Left.Count == 1 && Right.Count == 0)
                            {

                                MergedList.Add(Left.First<string>());
                                Left.Remove(Left.First<string>());

                            }
                            break;
                        }
                    case SortType.DOUBLE:
                        {
                            {

                                if (Left.Count > 0 && Right.Count > 0)
                                {

                                    if (double.Parse(Left.First<string>().Split(',')[2]) > double.Parse(Right.First<string>().Split(',')[2]))
                                    {

                                        MergedList.Add(Right.First<string>());
                                        Right.Remove(Right.First<string>());

                                    }
                                    else if (double.Parse(Left.First<string>().Split(',')[2]) < double.Parse(Right.First<string>().Split(',')[2]))
                                    {

                                        MergedList.Add(Left.First<string>());
                                        Left.Remove(Left.First<string>());


                                    }

                                }
                                else
                                {

                                    if (Left.Count != 0)
                                    {

                                        while (Left.Count != 0)
                                        {

                                            MergedList.Add(Left.First<string>());
                                            Left.Remove(Left.First<string>());

                                        }

                                    }
                                    else if (Right.Count != 0)
                                    {

                                        while (Right.Count != 0)
                                        {

                                            MergedList.Add(Right.First<string>());
                                            Right.Remove(Right.First<string>());

                                        }

                                    }

                                }

                                if (Left.Count == 0 && Right.Count == 1)
                                {

                                    MergedList.Add(Right.First<string>());
                                    Right.Remove(Right.First<string>());

                                }
                                else if (Left.Count == 1 && Right.Count == 0)
                                {

                                    MergedList.Add(Left.First<string>());
                                    Left.Remove(Left.First<string>());

                                }
                                break;
                            }
                        }

                }

            }

            return MergedList;

        }

        public static void DisplayList<T>(List<T> DataList)
        {

            foreach (T Item in DataList)
            {

                Console.WriteLine(Item);

            }

        }

    }
}
