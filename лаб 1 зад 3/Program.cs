//Дано список: {1,2,3,1,4,5,2,2,1} Потрібно замінити повторювані числа
//(якщо число повторюється більше ніж 1 раз) на 0 всередині масиву (1) (3)
using System;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        int[] numbers = { 1, 2, 3, 1, 4, 5, 2, 2, 1 };

        var uniqueNumbers = numbers.GroupBy(x => x)
                                   .Select(group => group.Count() > 1 ? 0 : group.Key)
                                   .ToArray();

        Console.WriteLine($"Результат: {{{string.Join(",", uniqueNumbers)}}}");
    }
}
