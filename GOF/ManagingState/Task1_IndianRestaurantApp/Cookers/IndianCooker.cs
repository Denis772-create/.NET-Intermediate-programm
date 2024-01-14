using ManagingState.Task1_IndianRestaurantApp.Entities;

namespace ManagingState.Task1_IndianRestaurantApp.Cookers;

public class IndianCooker : ICooker
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
        Console.WriteLine($"Salting rice at {level} level.");
    }

    public void SaltChicken(Level level)
    {
        Console.WriteLine($"Salting chicken at {level} level.");
    }

    public void PepperRice(Level level)
    {
        Console.WriteLine($"Peppering rice at {level} level.");
    }

    public void PepperChicken(Level level)
    {
        Console.WriteLine($"Peppering chicken at {level} level.");
    }
}