namespace AlteringBehavior.Task3;

public class RoundingCalculator : ICalculator
{
    private readonly ICalculator _decoratedCalculator;

    public RoundingCalculator(ICalculator decoratedCalculator)
    {
        _decoratedCalculator = decoratedCalculator;
    }

    public decimal CalculatePayment(string touristName)
    {
        decimal payment = _decoratedCalculator.CalculatePayment(touristName);
        return Math.Round(payment, MidpointRounding.AwayFromZero);
    }
}