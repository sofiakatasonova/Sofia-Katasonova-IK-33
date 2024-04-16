using System;

public abstract class RailCar
{
    public int PassengerCapacity { get; protected set; }
    public int LuggageCapacity { get; protected set; }
    public int ComfortLevel { get; protected set; }

    public abstract void DisplayInfo();
}
