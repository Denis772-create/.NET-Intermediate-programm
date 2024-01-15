namespace AlteringBehavior.Task3;

public class CalculatorFactory : ICalculatorFactory
{
    private readonly ICurrencyService _currencyService;
    private readonly ITripRepository _tripRepository;
    private readonly ILogger _logger;

    public CalculatorFactory(ICurrencyService currencyService,
        ITripRepository tripRepository, 
        ILogger logger)
    {
        _currencyService = currencyService;
        _tripRepository = tripRepository;
        _logger = logger;
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

    public ICalculator CreateLoggingCalculator()
    {
        var basicCalculator = CreateCalculator();
        return new LoggingCalculator(basicCalculator, _logger);
    }

    public ICalculator CreateRoundingCalculator()
    {
        var basicCalculator = CreateCalculator();
        return new RoundingCalculator(basicCalculator);
    }
}