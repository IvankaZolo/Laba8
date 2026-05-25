namespace LAB8.Logic;

public interface ISearchable
{
    IEnumerable<Train> SearchTrains(string keyword);
    IEnumerable<Booking> SearchBookingsByDate(DateTime date);
}