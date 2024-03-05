using System;
using System.Collections.Generic;

class Program
{
    class Obj
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    static void Main(string[] args)
    {
        List<Obj> objList = new List<Obj>
        {
            new Obj { Id = 1, Name = "Object 1" },
            new Obj { Id = 2, Name = "Object 2" },
            new Obj { Id = 3, Name = "Object 3" }
        };

        Console.Write("Enter the Id to search for: ");
        int searchId = Convert.ToInt32(Console.ReadLine());

        Obj foundObj = objList.Find(obj => obj.Id == searchId);

        if (foundObj != null)
        {
            Console.WriteLine($"Object found: Id = {foundObj.Id}, Name = {foundObj.Name}");
        }
        else
        {
            Console.WriteLine("Object not found.");
        }
    }
}
