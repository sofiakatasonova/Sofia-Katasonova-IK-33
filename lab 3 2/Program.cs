//Скласти опис класу для трикутника. Зберігає координати вершин трикутника на площині.
//Методи: чи рівні два трикутники, площа, периметр, висоти, медіани, бісектриси, радіус вписаного, радіус описаного кола, тип трикутника
//(рівносторонній, рівнобедрений, прямокутний, гострокутний, тупокутний), поворот на заданий кут відносно (однієї з вершин, центру описаного кола).

using System;
using System.IO;
using System.Text.Json;

public class Triangle
{
    public (double, double) A { get; set; }
    public (double, double) B { get; set; }
    public (double, double) C { get; set; }

    public Triangle() { }

    public Triangle(double ax, double ay, double bx, double by, double cx, double cy)
    {
        A = (ax, ay);
        B = (bx, by);
        C = (cx, cy);
    }

    private double Distance((double, double) point1, (double, double) point2)
    {
        return Math.Sqrt(Math.Pow(point2.Item1 - point1.Item1, 2) + Math.Pow(point2.Item2 - point1.Item2, 2));
    }

    public double Perimeter()
    {
        double sideAB = Distance(A, B);
        double sideBC = Distance(B, C);
        double sideCA = Distance(C, A);
        return sideAB + sideBC + sideCA;
    }

    public double Area()
    {
        double sideAB = Distance(A, B);
        double sideBC = Distance(B, C);
        double sideCA = Distance(C, A);
        double semiPerimeter = Perimeter() / 2;
        return Math.Sqrt(semiPerimeter * (semiPerimeter - sideAB) * (semiPerimeter - sideBC) * (semiPerimeter - sideCA));
    }

    public bool Equals(Triangle other)
    {
        double thisSidesSum = Math.Pow(Distance(A, B), 2) + Math.Pow(Distance(B, C), 2) + Math.Pow(Distance(C, A), 2);
        double otherSidesSum = Math.Pow(Distance(other.A, other.B), 2) + Math.Pow(Distance(other.B, other.C), 2) + Math.Pow(Distance(other.C, other.A), 2);

        return thisSidesSum.Equals(otherSidesSum);
    }

    public static void SaveToJson(Triangle triangle, string filePath)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(triangle, options);
        File.WriteAllText(filePath, jsonString);
    }

    public static Triangle LoadFromJson(string filePath)
    {
        string jsonString = File.ReadAllText(filePath);
        Triangle triangle = JsonSerializer.Deserialize<Triangle>(jsonString);
        return triangle;
    }
}

class Program
{
    static void Main()
    {
        Triangle t1 = new Triangle(0, 0, 3, 0, 3, 4);

        // Збереження у файл JSON
        Triangle.SaveToJson(t1, "triangle.json");

        // Завантаження з файлу JSON
        Triangle t2 = Triangle.LoadFromJson("triangle.json");

        Console.WriteLine($"Perimeter of t2: {t2.Perimeter()}");
        Console.WriteLine($"Area of t2: {t2.Area()}");
    }
}
