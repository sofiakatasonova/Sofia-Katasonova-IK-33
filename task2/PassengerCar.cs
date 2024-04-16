using System;

public class PassengerCar : RailCar
{
    public PassengerCar(int passengerCapacity, int luggageCapacity, int comfortLevel)
    {
        PassengerCapacity = passengerCapacity;
        LuggageCapacity = luggageCapacity;
        ComfortLevel = comfortLevel;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Passenger Car -- Passengers: {PassengerCapacity}, Luggage: {LuggageCapacity}, Comfort: {ComfortLevel}");
    }
}
