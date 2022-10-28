char symbol = char.Parse(Console.ReadLine()!.ToLower());

if (symbol < 'а' || symbol > 'я')
{
    Console.WriteLine("Error");
    return;
}

bool isVowel = symbol switch
{
    'а' or 'у' or 'о' or 'и' or 'э' or 'ы' or 'я' or 'ю' or 'е' or 'ё' => true,
    _                                                                  => false
};

Console.WriteLine(isVowel ? "Гласная" : "Согласная");
