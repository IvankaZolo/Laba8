namespace LAB8.Logic;

public class Train
{
    public int Id { get; }
    public string Name { get; }
    public string DepartureStation { get; }
    public string ArrivalStation { get; }
    public DateTime DepartureTime { get; }
    public DateTime ArrivalTime { get; }
    private List<Wagon> Wagons { get; }

    public Train(int id, string name, string departureStation, string arrivalStation,
        DateTime departureTime, DateTime arrivalTime)
    {
        Id = id;
        Name = name;
        DepartureStation = departureStation;
        ArrivalStation = arrivalStation;
        DepartureTime = departureTime;
        ArrivalTime = arrivalTime;
        Wagons = new List<Wagon>();
    }

    public void AddWagon(Wagon wagon)
    {
        if (Wagons.Any(w => w.Number == wagon.Number))
            throw new BookingException($"Wagon {wagon.Number} already exists in train {Name}.");
        Wagons.Add(wagon);
    }

    public void RemoveWagon(int wagonNumber)
    {
        var wagon = GetWagon(wagonNumber);

        if (wagon.HasBookedSeats)
            throw new BookingException($"Cannot remove wagon {wagonNumber} — it has booked seats.");

        Wagons.Remove(wagon);
    }

    public Wagon GetWagon(int wagonNumber)
    {
        return Wagons.FirstOrDefault(w => w.Number == wagonNumber)
            ?? throw new BookingException($"Wagon {wagonNumber} not found in train {Name}.");
    }

    public IEnumerable<(Wagon Wagon, double BookingPercentage)> GetWagonsWithBookingPercentage()
    {
        return Wagons.Select(w => (w, w.GetBookingPercentage()));
    }

    public string GetTrainSummary()
    {
        return $"Train #{Id} — {Name}\n" +
               $"Route: {DepartureStation} - {ArrivalStation}\n" +
               $"Departure: {DepartureTime:dd.MM.yyyy HH:mm}\n" +
               $"Arrival: {ArrivalTime:dd.MM.yyyy HH:mm}\n" +
               $"Wagons: {Wagons.Count}";
    }
}