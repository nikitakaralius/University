if (int.TryParse(Console.ReadLine()!, out int number) == false)
{
    Console.WriteLine("Input is not a number");
    return;
}

if (number < 1 || number > 99)
{
    Console.WriteLine("The number must be in the range [1;99]");
    return;
}

Console.WriteLine(TextRepresentation(number));

string TextRepresentation(int number)
{
    return number switch
    {
        10 => "десять",
        11 => "одиннадцать",
        12 => "двенадцать",
        13 => "тринадцать",
        14 => "четырнадцать",
        15 => "пятнадцать",
        16 => "шестнадцать",
        17 => "семнадцать",
        18 => "восемнадцать",
        19 => "девятнадцать",
        _  => $"{DozensRepresentation(number)} {UnitsRepresentation(number)}"
    };
}

string UnitsRepresentation(int number)
{
    return (number % 10) switch
    {
        0 => "",
        1 => "один",
        2 => "два",
        3 => "три",
        4 => "четыре",
        5 => "пять",
        6 => "шесть",
        7 => "семь",
        8 => "восемь",
        9 => "девять",
        _ => throw new ArgumentOutOfRangeException()
    };
}

string DozensRepresentation(int number)
{
    return (number / 10) switch
    {
        1 => "надцать",
        2 => "двадцать",
        3 => "тридцать",
        4 => "сорок",
        5 => "пятьдесят",
        6 => "шестьдесят",
        7 => "семьдесят",
        8 => "восемьдесят",
        9 => "девяносто",
        _ => throw new ArgumentOutOfRangeException()
    };
}
