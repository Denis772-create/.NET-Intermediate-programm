using ManagingState.Task1_IndianRestaurantApp.Cookers;
using ManagingState.Task1_IndianRestaurantApp.Entities;

namespace ManagingState.Task1_IndianRestaurantApp.Recipes;

public class UkraineBasicRecipe : IRecipe
{
    public void PrepareRice(ICooker cooker)
    {
        cooker.FryRice(500, Level.Strong);
        cooker.SaltRice(Level.Strong);
        cooker.PepperRice(Level.Low);
    }

    public void PrepareChicken(ICooker cooker)
    {
        cooker.FryChicken(300, Level.Medium);
        cooker.SaltChicken(Level.Medium);
        cooker.PepperChicken(Level.Low);
    }
}