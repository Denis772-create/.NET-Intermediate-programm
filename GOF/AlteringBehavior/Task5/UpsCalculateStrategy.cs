namespace AlteringBehavior.Task5;

public class UpsCalculateStrategy : IShipmentCalculator
{
    public double CalculatePrice(Order order)
    {
        return order.Weight > 400 ? 4.25d * 1.1 : 4.25d;
    }
}