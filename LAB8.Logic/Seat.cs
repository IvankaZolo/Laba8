namespace LAB8.Logic;

public class Seat
{
    public int Number { get; }
    public bool IsBooked { get; private set; }

    public Seat(int number)
    {
        Number = number;
        IsBooked = false;
    }

    public void Book()
    {
        if (IsBooked)
            throw new BookingException($"Seat {Number} is already booked.");

        IsBooked = true;
    }

    public void Release()
    {
        if (!IsBooked)
            throw new BookingException($"Seat {Number} is not booked.");

        IsBooked = false;
    }
}