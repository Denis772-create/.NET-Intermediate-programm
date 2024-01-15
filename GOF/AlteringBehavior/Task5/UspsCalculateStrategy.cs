namespace AlteringBehavior.Task5;

public class UspsCalculateStrategy : IShipmentCalculator
{
    public double CalculatePrice(Order order)
    {
        return order.Product == ProductType.Book ? 3.00d * 0.9 : 3.00d;
    }
}