namespace LAB8.Logic;

public class PlatskarWagon : Wagon
{
    public PlatskarWagon(int number, decimal basePrice)
        : base(number, basePrice, 54) { }

    public override int GetCapacity() => 54;

    public override decimal GetPriceWithMarkup() => BasePrice * 1.0m;
}