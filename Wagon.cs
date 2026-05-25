namespace LAB8.Logic;

public abstract class Wagon
{
    public int Number { get; }
    public decimal BasePrice { get; }
    protected List<Seat> Seats { get; }
    public bool HasBookedSeats => Seats.Any(s => s.IsBooked);

    protected Wagon(int number, decimal basePrice, int capacity)
    {
    Number = number;
    BasePrice = basePrice;
    Seats = new List<Seat>();
    for (int i = 1; i <= capacity; i++)
        Seats.Add(new Seat(i));
    }

    public IEnumerable<Seat> GetAvailableSeats() => Seats.Where(s => !s.IsBooked);

    public IEnumerable<Seat> GetBookedSeats() => Seats.Where(s => s.IsBooked);

    public double GetBookingPercentage()
    {
        if (Seats.Count == 0) return 0;
        return (double)GetBookedSeats().Count() / Seats.Count * 100;
    }

    public Seat GetSeat(int number)
    {
        return Seats.FirstOrDefault(s => s.Number == number)
            ?? throw new BookingException($"Seat {number} not found in wagon {Number}.");
    }

    public abstract int GetCapacity();

    public abstract decimal GetPriceWithMarkup();
}