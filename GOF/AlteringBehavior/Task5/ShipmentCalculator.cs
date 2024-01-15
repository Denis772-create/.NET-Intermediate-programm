namespace AlteringBehavior.Task5;

public class ShipmentCalculator
{
    private readonly Dictionary<ShipmentOptions, IShipmentCalculator> _strategies = new()
    {
        { ShipmentOptions.FedEx, new FedExCalculateStrategy() },
        { ShipmentOptions.UPS, new UpsCalculateStrategy() },
        { ShipmentOptions.USPS, new UspsCalculateStrategy() }
    };

    public double CalculatePrice(Order order)
    {
        if (_strategies.TryGetValue(order.ShipmentOptions, out var strategy))
        {
            return strategy.CalculatePrice(order);
        }

        throw new NotImplementedException("No strategy found for the given shipment option.");
    }
}
