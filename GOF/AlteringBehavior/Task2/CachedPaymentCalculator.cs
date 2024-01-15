namespace AlteringBehavior.Task2;

internal class CachedPaymentCalculator : ICalculator
{
    private readonly ICalculator _decoratedCalculator;
    private readonly Dictionary<string, decimal> _cache = new();

    public CachedPaymentCalculator(ICalculator decoratedCalculator)
    {
        _decoratedCalculator = decoratedCalculator;
    }

    public decimal CalculatePayment(string touristName)
    {
        if (_cache.TryGetValue(touristName, out var calculatePayment))
        {
            return calculatePayment;
        }

        decimal payment = _decoratedCalculator.CalculatePayment(touristName);
        _cache[touristName] = payment;
        return payment;
    }
}