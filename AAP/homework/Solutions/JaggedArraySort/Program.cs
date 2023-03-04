using System;

internal class Program
{
    public static void Main(string[] args)
    {
        RunFirstTestcase();
        RunSecondTestcase();
    }

    private static void RunFirstTestcase()
    {
        int[][] arrTaxi = new int[10][];

        arrTaxi[0] = new int[] {100, 289, 200, 101, 90, 230};
        arrTaxi[1] = new int[] {290, 300, 303, 120, 150};
        arrTaxi[2] = new int[] {80};
        arrTaxi[3] = new int[] {300, 60, 120, 400, 410};
        arrTaxi[4] = new int[] {60, 100, 40};
        arrTaxi[5] = new int[] {60, 160, 165, 120, 110, 230, 200, 30};
        arrTaxi[6] = new int[] {230, 200, 250, 100};
        arrTaxi[7] = new int[] {100, 209, 175, 100};
        arrTaxi[8] = new int[] {70, 120, 290};
        arrTaxi[9] = new int[] {90, 80, 105, 140, 120};

        RunTestcase(arrTaxi);
    }

    private static void RunSecondTestcase()
    {
        int[][] arrTaxi = new int[10][];

        arrTaxi[0] = new int[] {80};
        arrTaxi[1] = new int[] {60, 100, 40};
        arrTaxi[2] = new int[] {70, 120, 290};
        arrTaxi[3] = new int[] {100, 209, 175, 100};
        arrTaxi[4] = new int[] {230, 200, 250, 100};
        arrTaxi[5] = new int[] {90, 80, 105, 140, 120};
        arrTaxi[6] = new int[] {290, 300, 303, 120, 150};
        arrTaxi[7] = new int[] {300, 60, 120, 400, 410};
        arrTaxi[8] = new int[] {100, 289, 200, 101, 90, 230};
        arrTaxi[9] = new int[] {60, 160, 165, 120, 110, 230, 200, 30};

        RunTestcase(arrTaxi);
    }

    private static void RunTestcase(int[][] taxi)
    {
        int iterations = taxi.QuickSort((x, y) =>
        {
            if (x.Length == y.Length)
                return x.Sum() - y.Sum();

            return y.Length - x.Length;
        });

        Print(taxi, iterations);
    }

    private static void Print(int[][] jaggedArray, int iterations)
    {
        foreach (int[] array in jaggedArray)
        {
            foreach (int x in array)
            {
                Console.Write($"{x} ");
            }

            Console.WriteLine();
        }

        Console.WriteLine($"Количество итераций = {iterations}");
        Console.WriteLine();
    }
}

internal static class ArrayExtensions
{
    public static int QuickSort<T>(this T[] array, Func<T, T, int> comparer) =>
        QuickSort(array, 0, array.Length - 1, comparer);

    public static int Sum(this int[] array)
    {
        int sum = 0;

        foreach (int element in array)
        {
            sum += element;
        }

        return sum;
    }

    private static int QuickSort<T>(T[] array, int begin, int end, Func<T, T, int> comparer)
    {
        if (begin >= end) return 1;

        int partitionIndex = EvaluatePartitionIndex(array, begin, end, comparer);

        return 1 +
               QuickSort(array, begin, partitionIndex - 1, comparer) +
               QuickSort(array, partitionIndex + 1, end, comparer);
    }

    private static int EvaluatePartitionIndex<T>(T[] array, int begin, int end, Func<T, T, int> comparer)
    {
        T pivot = array[end];
        int i = begin - 1;

        for (int j = begin; j < end; j++)
        {
            if (comparer(array[j], pivot) <= 0)
            {
                i++;
                Swap(ref array[i], ref array[j]);
            }
        }

        Swap(ref array[i + 1], ref array[end]);

        return i + 1;
    }

    private static void Swap<T>(ref T x, ref T y)
    {
        T temp = x;
        x = y;
        y = temp;
    }
}
