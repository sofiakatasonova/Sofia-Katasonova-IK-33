//Створити об'єкт класу Текст, використовуючи класи Речення, Слово. Методи: доповнити текст, вивести на консоль текст, заголовок тексту.

using System;
using System.Collections.Generic;
using System.Linq;

// Клас Слово
public class Word
{
    public string Value { get; private set; }

    public Word(string value)
    {
        Value = value;
        Console.WriteLine($"Створено нове слово: {Value}");
    }

    // Перевизначення ToString для кращого відображення
    public override string ToString()
    {
        return Value;
    }

    // Перевизначення Equals
    public override bool Equals(object obj)
    {
        if (obj is Word)
        {
            Word other = (Word)obj;
            return this.Value.Equals(other.Value);
        }
        return false;
    }

    // Перевизначення GetHashCode
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}

// Клас Речення
public class Sentence
{
    private List<Word> Words { get; set; }

    public Sentence(IEnumerable<Word> words)
    {
        Words = new List<Word>(words);
        Console.WriteLine("Створено нове речення.");
    }

    // Метод для додавання слова в речення
    public void AddWord(Word word)
    {
        Words.Add(word);
        Console.WriteLine($"Додано слово до речення: {word}");
    }

    // Перевизначення ToString для відображення речення
    public override string ToString()
    {
        return string.Join(" ", Words.Select(w => w.ToString())) + ".";
    }
}

// Клас Текст
public class Text
{
    private List<Sentence> Sentences { get; set; }
    public string Title { get; set; }

    public Text(string title)
    {
        Title = title;
        Sentences = new List<Sentence>();
        Console.WriteLine($"Створено новий текст з назвою: {Title}");
    }

    // Метод для додавання речення в текст
    public void AddSentence(Sentence sentence)
    {
        Sentences.Add(sentence);
        Console.WriteLine("Додано нове речення до тексту.");
    }

    // Вивід тексту на консоль
    public void PrintText()
    {
        Console.WriteLine($"Текст: {Title}");
        foreach (var sentence in Sentences)
        {
            Console.WriteLine(sentence);
        }
    }

    // Перевизначення ToString
    public override string ToString()
    {
        return $"Текст: {Title}\n" + string.Join("\n", Sentences.Select(s => s.ToString()));
    }
}

// Основна програма
class Program
{
    static void Main(string[] args)
    {
        // Створення елементів тексту
        Word word1 = new Word("Hello");
        Word word2 = new Word("world");
        Sentence sentence1 = new Sentence(new List<Word> { word1, word2 });

        Text text = new Text("Вступ");
        text.AddSentence(sentence1);
        text.PrintText();
    }
}
