using System;

internal class Program
{
    public static void Main()
    {
        Console.WriteLine("Введите слово или фразу, которые хотите проверить на палиндром");
        string s = Console.ReadLine()!.ToLower().Replace(" ", "");
        Console.WriteLine(IsPalindrome(s) ? "палиднром ✅" : "не палиндром ❌");
        Console.ReadKey();
    }

    private static bool IsPalindrome(string s)
    {
        if (s.Length <= 1) return true;
        if (s[0] != s[^1]) return false;
        return IsPalindrome(s[1..^1]);
    }
}
