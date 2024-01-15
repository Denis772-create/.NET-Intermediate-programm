namespace AlteringBehavior.Task3;

public interface ICalculatorFactory
{
    ICalculator CreateCalculator();
    ICalculator CreateCachedCalculator();
    ICalculator CreateLoggingCalculator();
    ICalculator CreateRoundingCalculator();
}