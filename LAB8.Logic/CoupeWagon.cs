namespace LAB8.Logic;

public class CoupeWagon : Wagon
{
    public int CompartmentsCount { get; }

    public CoupeWagon(int number, decimal basePrice, int compartmentsCount = 8)
        : base(number, basePrice, compartmentsCount * 4)
    {
        CompartmentsCount = compartmentsCount;
    }
    public override int GetCapacity() => CompartmentsCount * 4;

    public override decimal GetPriceWithMarkup() => BasePrice * 1.5m;
}