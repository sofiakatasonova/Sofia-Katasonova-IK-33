using System;

public class Menu
{
    private Train train;

    public Menu()
    {
        train = new Train();
    }

    public void Run()
    {
        bool running = true;
        while (running)
        {
            Console.WriteLine("\nTrain management system");
            Console.WriteLine("1. Add passenger car");
            Console.WriteLine("2. Display all cars");
            Console.WriteLine("3. Sort cars by comfort");
            Console.WriteLine("4. Find cars by Passenger range");
            Console.WriteLine("5. Display total assengers and luggage");
            Console.WriteLine("6. Exit");
            Console.Write("Select an option: ");

            int option = Convert.ToInt32(Console.ReadLine());

            switch (option)
            {
                case 1:
                    AddPassengerCar();
                    break;
                case 2:
                    DisplayCars();
                    break;
                case 3:
                    SortCarsByComfort();
                    break;
                case 4:
                    FindCarsByPassengerRange();
                    break;
                case 5:
                    DisplayTotals();
                    break;
                case 6:
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option, try again.");
                    break;
            }
        }
    }

    private void AddPassengerCar()
    {
        Console.WriteLine("Enter passenger capacity:");
        int passengers = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter luggage capacity:");
        int luggage = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter comfort level:");
        int comfort = Convert.ToInt32(Console.ReadLine());

        train.AddCar(new PassengerCar(passengers, luggage, comfort));
        Console.WriteLine("Passenger car added.");
    }

    private void DisplayCars()
    {
        train.DisplayCars();
    }

    private void SortCarsByComfort()
    {
        train.SortCarsByComfort();
        Console.WriteLine("Cars sorted by comfort level.");
    }

    private void FindCarsByPassengerRange()
    {
        Console.WriteLine("Enter minimum passenger capacity:");
        int min = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter maximum passenger capacity:");
        int max = Convert.ToInt32(Console.ReadLine());

        var cars = train.FindCarsByPassengerRange(min, max);
        foreach (var car in cars)
        {
            car.DisplayInfo();
        }
    }

    private void DisplayTotals()
    {
        Console.WriteLine($"Total Passengers: {train.TotalPassengers()}");
        Console.WriteLine($"Total Luggage: {train.TotalLuggage()}");
    }
}
