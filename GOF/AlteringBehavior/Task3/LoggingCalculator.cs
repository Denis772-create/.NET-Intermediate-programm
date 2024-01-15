namespace AlteringBehavior.Task3;

public class LoggingCalculator : ICalculator
{
    private readonly ICalculator decoratedCalculator;
    private readonly ILogger logger;

    public LoggingCalculator(ICalculator decoratedCalculator, ILogger logger)
    {
        this.decoratedCalculator = decoratedCalculator;
        this.logger = logger;
    }

    public decimal CalculatePayment(string touristName)
    {
        logger.Log("Start");
        decimal payment = decoratedCalculator.CalculatePayment(touristName);
        logger.Log("End");
        return payment;
    }
}