namespace LAB8.Logic;

public class TicketOffice : ISearchable
{
    private readonly List<Train> _trains = new();
    private readonly List<Booking> _bookings = new();
    private int _nextBookingId = 1;

    public void AddTrain(Train train)
    {
        if (_trains.Any(t => t.Id == train.Id))
            throw new BookingException($"Train with ID {train.Id} already exists.");

        _trains.Add(train);
    }
    public void RemoveTrain(int trainId)
    {
        var train = GetTrain(trainId);
        _trains.Remove(train);
    }

    public IEnumerable<Train> GetAllTrains() => _trains;

    public Train GetTrain(int trainId)
    {
        return _trains.FirstOrDefault(t => t.Id == trainId)
            ?? throw new BookingException($"Train with ID {trainId} not found.");
    }

    public IEnumerable<(Wagon Wagon, double BookingPercentage)> GetTrainWagonsInfo(int trainId)
    {
        return GetTrain(trainId).GetWagonsWithBookingPercentage();
    }

    public void AddWagonToTrain(int trainId, Wagon wagon)
    {
        GetTrain(trainId).AddWagon(wagon);
    }

    public void RemoveWagonFromTrain(int trainId, int wagonNumber)
    {
        GetTrain(trainId).RemoveWagon(wagonNumber);
    }

    public (IEnumerable<Seat> Available, IEnumerable<Seat> Booked) GetWagonSeats(int trainId, int wagonNumber)
    {
        var wagon = GetTrain(trainId).GetWagon(wagonNumber);
        return (wagon.GetAvailableSeats(), wagon.GetBookedSeats());
    }

    public Booking AddBooking(Passenger passenger, int trainId, int wagonNumber, int seatNumber)
    {
        var train = GetTrain(trainId);
        var wagon = train.GetWagon(wagonNumber);
        var seat = wagon.GetSeat(seatNumber);

        seat.Book();

        var booking = new Booking(_nextBookingId++, passenger, train, wagon, seat);
        _bookings.Add(booking);
        return booking;
    }

    public void CancelBooking(int bookingId)
    {
        var booking = GetBooking(bookingId);
        booking.Seat.Release();
        _bookings.Remove(booking);
    }

    public void ChangeBooking(int bookingId, int newWagonNumber, int newSeatNumber)
    {
        var booking = GetBooking(bookingId);

        var newWagon = booking.Train.GetWagon(newWagonNumber);
        var newSeat = newWagon.GetSeat(newSeatNumber);

        booking.Seat.Release();
        newSeat.Book();

        var updated = new Booking(booking.Id, booking.Passenger, booking.Train, newWagon, newSeat);
        var index = _bookings.IndexOf(booking);
        _bookings[index] = updated;
    }
    public Booking GetBooking(int bookingId)
    {
        return _bookings.FirstOrDefault(b => b.Id == bookingId)
            ?? throw new BookingException($"Booking with ID {bookingId} not found.");
    }

    public IEnumerable<Train> SearchTrains(string keyword)
    {
        var lower = keyword.ToLower();
        return _trains.Where(t =>
            t.Name.ToLower().Contains(lower) ||
            t.DepartureStation.ToLower().Contains(lower) ||
            t.ArrivalStation.ToLower().Contains(lower));
    }
    public IEnumerable<Booking> SearchBookingsByDate(DateTime date)
    {
        return _bookings.Where(b => b.BookingDate.Date == date.Date);
    }
}