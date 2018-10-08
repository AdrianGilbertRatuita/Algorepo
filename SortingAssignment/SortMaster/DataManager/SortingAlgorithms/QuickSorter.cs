using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DataManager.SortingAlgorithms
{
    public class QuickSorter
    {

        public static string[] IDSort(string[] DataTable)
        {

            Stopwatch NewStopwatch = new Stopwatch();
            IList<string> ListTable = DataTable.ToList<string>();
            NewStopwatch.Start();

            //
            QuicksortParallel<string>(ListTable, 0, ListTable.Count - 1, SortType.ID);

            NewStopwatch.Stop();
            string[] SortedDatatable = ListTable.ToArray<string>();
            DataMaster.OutputData(SortedDatatable, NewStopwatch.ElapsedMilliseconds, SortType.ID, SortingAlgorithmType.QUICKSORT);

            return SortedDatatable;
        }

        public static string[] GUIDSort(string[] DataTable)
        {

            Stopwatch NewStopwatch = new Stopwatch();
            IList<string> ListTable = DataTable.ToList<string>();
            NewStopwatch.Start();

            //
            QuicksortParallel<string>(ListTable, 0, ListTable.Count - 1, SortType.GUID);

            NewStopwatch.Stop();
            string[] SortedDatatable = ListTable.ToArray<string>();
            DataMaster.OutputData(SortedDatatable, NewStopwatch.ElapsedMilliseconds, SortType.GUID, SortingAlgorithmType.QUICKSORT);

            return SortedDatatable;
        }

        public static string[] DoubleSort(string[] DataTable)
        {

            Stopwatch NewStopwatch = new Stopwatch();
            IList<string> ListTable = DataTable.ToList<string>(); 
            NewStopwatch.Start();

            //
            QuicksortParallel<string>(ListTable, 0, ListTable.Count - 1, SortType.DOUBLE);


            NewStopwatch.Stop();
            string[] SortedDatatable = ListTable.ToArray<string>();
            DataMaster.OutputData(SortedDatatable, NewStopwatch.ElapsedMilliseconds, SortType.DOUBLE, SortingAlgorithmType.QUICKSORT);

            return SortedDatatable;
        }

        private static void QuicksortParallel<T>(IList<T> arr, int left, int right, SortType SortingType) where T : IComparable<T>
        {
            // Defining a minimum length to use parallelism, over which using parallelism
            // got better performance than the sequential version.
            const int threshold = 2048;

            // If the list to sort contains one or less element, the list is already sorted.
            if (right <= left) return;

            // If the size of the list is under the threshold, sequential version is used.
            if (right - left < threshold)
                Quicksort(ref arr, left, right, SortingType);

            else
            {
                // Partitioning our list and getting a pivot.
                var pivot = Partition(ref arr, left, right, SortingType);

                // Sorting the left and right of the pivot in parallel
                Parallel.Invoke(
                    () => QuicksortParallel(arr, left, pivot - 1, SortingType),
                    () => QuicksortParallel(arr, pivot + 1, right, SortingType));
            }
        
         }

        private static void Swap<T>(ref IList<T> arr, int i, int j)
        {
            var tmp = arr[i];
            arr[i] = arr[j];
            arr[j] = tmp;
        }

        private static int Partition<T>(ref IList<T> arr, int low, int high, SortType SortingType) where T : IComparable<T>
        {

            Console.WriteLine($"{low} : {high}");
            /*
                * Defining the pivot position, here the middle element is used but the choice of a pivot
                * is a rather complicated issue. Choosing the left element brings us to a worst-case performance,
                * which is quite a common case, that's why it's not used here.
                */
            var pivotPos = (high + low) / 2;
            var pivot = arr[pivotPos];
            // Putting the pivot on the left of the partition (lowest index) to simplify the loop
            Swap(ref arr, low, pivotPos);

            /** The pivot remains on the lowest index until the end of the loop
                * The left variable is here to keep track of the number of values
                * that were compared as "less than" the pivot.
                */
            var left = low;
            for (var i = low + 1; i <= high; i++)
            {

                switch (SortingType)
                {

                    case SortType.ID:
                        {

                            int A = int.Parse(arr[i].ToString().Split(',')[0]);
                            int B = int.Parse(arr[pivotPos].ToString().Split(',')[0]);

                            // If the value is greater than the pivot value we continue to the next index.
                            if (A.CompareTo(B) >= 0) continue;

                            // If the value is less than the pivot, we increment our left counter (one more element below the pivot)
                            left++;
                            // and finally we swap our element on our left index.
                            Swap(ref arr, i, left);
                            break;
                        }
                    case SortType.GUID:
                        {

                            decimal A = GUIDToDecimal.GetDecimalValue(arr[i].ToString().Split(',')[1]);
                            decimal B = GUIDToDecimal.GetDecimalValue(arr[pivotPos].ToString().Split(',')[1]);

                            // If the value is greater than the pivot value we continue to the next index.
                            if (A.CompareTo(B) >= 0) continue;

                            // If the value is less than the pivot, we increment our left counter (one more element below the pivot)
                            left++;
                            // and finally we swap our element on our left index.
                            Swap(ref arr, i, left);
                            break;
                        }
                    case SortType.DOUBLE:
                        {

                            double A = double.Parse(arr[i].ToString().Split(',')[2]);
                            double B = double.Parse(arr[pivotPos].ToString().Split(',')[2]);

                            // If the value is greater than the pivot value we continue to the next index.
                            if (A.CompareTo(B) >= 0) continue;

                            // If the value is less than the pivot, we increment our left counter (one more element below the pivot)
                            left++;
                            // and finally we swap our element on our left index.
                            Swap(ref arr, i, left);
                            break;
                        }
  
                }


            }

            // The pivot is still on the lowest index, we need to put it back after all values found to be "less than" the pivot.
            Swap(ref arr, low, left);

            // We return the new index of our pivot
            return left;
        }

        private static void Quicksort<T>(ref IList<T> arr, int left, int right, SortType SortingType) where T : IComparable<T>
        {

            Console.WriteLine($"{left} : {right}");
            // If the list contains one or less element: no need to sort!
            if (right <= left) return;

            // Partitioning our list
            var pivot = Partition(ref arr, left, right, SortingType);

            // Sorting the left of the pivot
            Quicksort(ref arr, left, pivot - 1, SortingType);
            // Sorting the right of the pivot
            Quicksort(ref arr, pivot + 1, right, SortingType);
        }

    }
}
