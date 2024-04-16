using System;

public abstract class KitchenAppliance
{
    public double Weight { get; set; }
    public string Location { get; set; }

    public KitchenAppliance(double weight, string location)
    {
        Weight = weight;
        Location = location;
    }

    public void MoveTo(string newLocation)
    {
        Location = newLocation;
        Console.WriteLine($"Moved to {Location}.");
    }

    public abstract void DisplayLocation();
}

public abstract class Dishware : KitchenAppliance
{
    public Dishware(double weight, string location) : base(weight, location) { }

    public void Wash()
    {
        Console.WriteLine("Dishware has been washed.");
    }
}

public class Fork : Dishware
{
    public Fork(double weight, string location) : base(weight, location) { }

    public override void DisplayLocation()
    {
        Console.WriteLine($"The fork is currently at {Location}.");
    }
}

public class Plate : Dishware
{
    public Plate(double weight, string location) : base(weight, location) { }

    public override void DisplayLocation()
    {
        Console.WriteLine($"The plate is currently at {Location}.");
    }
}

public class Television : KitchenAppliance
{
    public Television(double weight, string location) : base(weight, location) { }

    public void TurnOn()
    {
        Console.WriteLine("The television has been turned on.");
    }

    public override void DisplayLocation()
    {
        Console.WriteLine($"The television is currently at {Location}.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Plate plate = new Plate(0.5, "Kitchen shelf");
        plate.DisplayLocation();
        plate.Wash();
        plate.MoveTo("Dining table");
        plate.DisplayLocation();

        Television tv = new Television(8.0, "Living room");
        tv.DisplayLocation();
        tv.TurnOn();
        tv.MoveTo("Bedroom");
        tv.DisplayLocation();
    }
}
