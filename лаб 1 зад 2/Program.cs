//Видалити зі словника елементи з однаковими значеннями. Записати результат у JSON файл.
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        // Створення словника
        Dictionary<int, string> myDictionary = new Dictionary<int, string>
        {
            { 1, "Value1" },
            { 2, "Value2" },
            { 3, "Value3" },
            { 4, "Value4" },
            { 5, "Value2" } // Повторюване значення, яке будемо видаляти
        };

        // Конвертація словника в JSON
        string json = JsonConvert.SerializeObject(myDictionary, Formatting.Indented);

        // Запис JSON у файл
        File.WriteAllText("dictionary.json", json);

        // Видалення елементів з однаковими значеннями
        var uniqueValues = myDictionary.GroupBy(pair => pair.Value).Where(group => group.Count() == 1).SelectMany(group => group)
            .ToDictionary(pair => pair.Key, pair => pair.Value);

        // Конвертація в JSON
        string uniqueJson = JsonConvert.SerializeObject(uniqueValues, Formatting.Indented);

        // Запис у файл
        File.WriteAllText("unique_dictionary.json", uniqueJson);

        Console.WriteLine("JSON файли були створені успішно.");
    }
}
