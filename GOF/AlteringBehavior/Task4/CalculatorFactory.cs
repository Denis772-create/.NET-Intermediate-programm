namespace AlteringBehavior.Task4;

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

    public ICalculator CreateCustomCalculator(bool useCaching, bool useLogging, bool useRounding)
    {
        var baseCalculator = new PaymentCalculator(_currencyService, _tripRepository);
        var builder = new CalculatorBuilder(baseCalculator, _logger);

        if (useCaching)
            builder.WithCaching();

        if (useLogging)
            builder.WithLogging();

        if (useRounding)
            builder.WithRounding();

        return builder.Build();
    }
}