using ManagingState.Task1_IndianRestaurantApp.Entities;

namespace ManagingState.Task1_IndianRestaurantApp.Cookers;

public class EnglishCooker : ICooker
{
    public void FryRice(int amount, Level level)
    {
        Console.WriteLine($"Frying {amount} grams of rice at {level} level.");
    }

    public void FryChicken(int amount, Level level)
    {
        Console.WriteLine($"Frying {amount} grams of chicken at {level} level.");
    }

    public void SaltRice(Level level)
    {
        Console.WriteLine("Rice is not salted.");
    }

    public void SaltChicken(Level level)
    {
        Console.WriteLine("Chicken is not salted.");
    }

    public void PepperRice(Level level)
    {
        Console.WriteLine("Rice is not peppered.");
    }

    public void PepperChicken(Level level)
    {
        Console.WriteLine("Chicken is not peppered.");
    }
}
