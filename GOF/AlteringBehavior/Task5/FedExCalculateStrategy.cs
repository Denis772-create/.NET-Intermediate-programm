namespace AlteringBehavior.Task5;

public class FedExCalculateStrategy : IShipmentCalculator
{
    public double CalculatePrice(Order order)
    {
        return order.Weight > 300 ? 5.00d * 1.1 : 5.00d;
    }
}