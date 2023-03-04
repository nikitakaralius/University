using System;
using System.Linq;

internal class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Введите элементы массива через пробел");

        int[] nums = Console.ReadLine()!
           .Split()
           .Select(x => int.Parse(x))
           .ToArray();

        Console.WriteLine("Укажите на какое количество элементов вы хотите сдвинуть массив");

        int shift = int.Parse(Console.ReadLine()!);

        Rotate(nums, shift);
        Print(nums);
    }

    private static void Rotate(int[] nums, int shift)
    {
        int count = nums.Length;
        shift %= count;
        Reverse(nums, 0, count - 1);
        Reverse(nums, 0, shift - 1);
        Reverse(nums, shift, count - 1);
    }

    private static void Reverse(int[] nums, int start, int end)
    {
        while (start <= end)
        {
            (nums[start], nums[end]) = (nums[end], nums[start]);
            start++;
            end--;
        }
    }

    private static void Print(int[] array)
    {
        foreach (int element in array)
            Console.Write($"{element} ");
    }
}
