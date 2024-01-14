using ManagingState.Task1_IndianRestaurantApp.Cookers;
using ManagingState.Task1_IndianRestaurantApp.Entities;

namespace ManagingState.Task1_IndianRestaurantApp.Recipes;

public class UkraineSummerRecipe : IRecipe
{
    public void PrepareRice(ICooker cooker)
    {
        cooker.FryRice(150, Level.Medium);
        cooker.SaltRice(Level.Low);
        cooker.PepperRice(Level.None);
    }

    public void PrepareChicken(ICooker cooker)
    {
        cooker.FryChicken(200, Level.Medium);
        cooker.SaltChicken(Level.Low);
        cooker.PepperChicken(Level.None);
    }
}