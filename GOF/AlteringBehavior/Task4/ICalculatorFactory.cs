namespace AlteringBehavior.Task4;

public interface ICalculatorFactory
{
    ICalculator CreateCalculator();
    ICalculator CreateCachedCalculator();
    ICalculator CreateLoggingCalculator();
    ICalculator CreateRoundingCalculator();
    ICalculator CreateCustomCalculator(bool useCaching, bool useLogging, bool useRounding);
}