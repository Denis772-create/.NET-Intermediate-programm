namespace AlteringBehavior.Task2;

public class CalculatorFactory : ICalculatorFactory
{
    private readonly ICurrencyService _currencyService;
    private readonly ITripRepository _tripRepository;

    public CalculatorFactory(ICurrencyService currencyService, ITripRepository tripRepository)
    {
        _currencyService = currencyService;
        _tripRepository = tripRepository;
    }

    public ICalculator CreateCalculator()
    {
        return new PaymentCalculator(_currencyService, _tripRepository);
    }

    public ICalculator CreateCachedCalculator()
    {
        var paymentCalculator = new PaymentCalculator(_currencyService, _tripRepository);
        return new CachedPaymentCalculator(paymentCalculator);
    }
}