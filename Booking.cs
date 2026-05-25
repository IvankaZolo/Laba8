namespace LAB8.Logic;

public class Booking
{
    public int Id { get; }
    public DateTime BookingDate { get; }
    public Passenger Passenger { get; }
    public Train Train { get; }
    public Wagon Wagon { get; }
    public Seat Seat { get; }

    public Booking(int id, Passenger passenger, Train train, Wagon wagon, Seat seat)
    {
        Id = id;
        BookingDate = DateTime.Now;
        Passenger = passenger;
        Train = train;
        Wagon = wagon;
        Seat = seat;
    }

    public string GetInfo()
    {
        return $"Booking #{Id} [{BookingDate:dd.MM.yyyy HH:mm}]\n" +
               $"Passenger: {Passenger.FullName} ({Passenger.PassportNumber})\n" +
               $"Train: {Train.Name} ({Train.DepartureStation} - {Train.ArrivalStation})\n" +
               $"Wagon: #{Wagon.Number} ({Wagon.GetType().Name})\n" +
               $"Seat: #{Seat.Number}\n" +
               $"Price: {Wagon.GetPriceWithMarkup():F2} грн";
    }
}