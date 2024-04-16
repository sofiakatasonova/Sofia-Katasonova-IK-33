using System;
using System.Collections.Generic;
using System.Linq;

public class Train
{
    private List<RailCar> railCars;

    public Train()
    {
        railCars = new List<RailCar>();
    }

    public void AddCar(RailCar car)
    {
        railCars.Add(car);
    }

    public int TotalPassengers() => railCars.Sum(car => car.PassengerCapacity);

    public int TotalLuggage() => railCars.Sum(car => car.LuggageCapacity);

    public void SortCarsByComfort()
    {
        railCars.Sort((car1, car2) => car1.ComfortLevel.CompareTo(car2.ComfortLevel));
    }

    public IEnumerable<RailCar> FindCarsByPassengerRange(int min, int max)
    {
        return railCars.Where(car => car.PassengerCapacity >= min && car.PassengerCapacity <= max);
    }

    public void DisplayCars()
    {
        foreach (var car in railCars)
        {
            car.DisplayInfo();
        }
    }
}
