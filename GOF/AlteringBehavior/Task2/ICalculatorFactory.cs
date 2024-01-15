namespace AlteringBehavior.Task2;

public interface ICalculatorFactory
{
    ICalculator CreateCalculator();

    ICalculator CreateCachedCalculator();
}