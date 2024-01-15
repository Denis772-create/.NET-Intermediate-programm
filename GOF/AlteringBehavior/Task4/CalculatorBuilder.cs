namespace AlteringBehavior.Task4;

internal class CalculatorBuilder
{
    private ICalculator _calculator;
    private readonly ILogger _logger;

    public CalculatorBuilder(ICalculator calculator, ILogger logger)
    {
        this._calculator = calculator;
        this._logger = logger;
    }

    public CalculatorBuilder WithCaching()
    {
        _calculator = new CachedPaymentCalculator(_calculator);
        return this;
    }

    public CalculatorBuilder WithLogging()
    {
        _calculator = new LoggingCalculator(_calculator, _logger);
        return this;
    }

    public CalculatorBuilder WithRounding()
    {
        _calculator = new RoundingCalculator(_calculator);
        return this;
    }

    public ICalculator Build()
    {
        return _calculator;
    }
}