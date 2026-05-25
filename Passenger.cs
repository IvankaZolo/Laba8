namespace LAB8.Logic;

public class Passenger
{
    public int Id { get; }
    public string FullName { get; }
    public string PassportNumber { get; }
    public string PhoneNumber { get; }

    public Passenger(int id, string fullName, string passportNumber, string phoneNumber)
    {
        Id = id;
        FullName = fullName;
        PassportNumber = passportNumber;
        PhoneNumber = phoneNumber;
    }
}