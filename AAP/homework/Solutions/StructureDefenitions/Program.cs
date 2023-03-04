Console.WriteLine();

// Загородная недвижимость
public struct SuburbanRealEstate
{
    public decimal Cost;     // Стоимость
    public double Area;      // Площадь
    public DateTime BuildAt; // Когда был построен
    public Person Owner;     // Владелец
    public Address Address;  // Адрес
}

// Акт нарушения пожарной безопасности
public struct FireSafetyViolationAct
{
    public Address CompilationPlace; // Место составления акта
    public DateTime CompiledAt;      // Дата составления
    public Employee[] Inspectors;    // Проверяющие
    public Violation[] Violations;   // нарушения
    public Signature[] Signatures;   // Подписи
}

// Музыкальный коллектив
public struct MusicBand
{
    public string Name;                // Название
    public Musician[] Musicians;       // Музыканты
    public DateTime FormedAt;          // Когда образована
    public Composition[] Compositions; // Композиции
}

// Адрес
public struct Address
{
    public string Settlement;  // Поселение
    public string Street;      // Улица
    public string HouseNumber; // Номер дома
    public int Index;          // Индекс
}

// Человек
public struct Person
{
    public string FirstName; // Имя
    public string LastName;  //  Фамилия
    public string Gender;    // Гендер
    public int Age;          // Возраст
}

// Работник
public struct Employee
{
    public Person Person;   // Данные о человеке
    public string Position; // Должность
}


// Нарушение
public struct Violation
{
    public string Title;           // Наименование нарушения
    public string Description;     // Описание
    public int Count;              // Количество нарушений
    public string InspectorRemark; // Примечание составителя настоящего Акта
    public string IntruderRemark;  // Примечание нарушителя
}

// Подпись
public struct Signature
{
    public string Text;       // Текст подписи
    public DateTime SignedAt; // Дата подписания
}

// Музыкант
public struct Musician
{
    public Person Person;     // Данные о человеке
    public string Instrument; // Музыкальный инструмент
}

// Композиция
public struct Composition
{
    public string Name;          // Название
    public string Album;         // Альбом
    public DateTime ReleaseDate; // Дата выхода
    public int PlaysCount;       // Количество воспроизведений
    public int SecondsLength;    // Длительность в секундах
}
