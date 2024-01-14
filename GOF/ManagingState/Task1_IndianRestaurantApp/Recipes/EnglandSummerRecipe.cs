using ManagingState.Task1_IndianRestaurantApp.Cookers;
using ManagingState.Task1_IndianRestaurantApp.Entities;

namespace ManagingState.Task1_IndianRestaurantApp.Recipes;

public class EnglandSummerRecipe : IRecipe
{
    public void PrepareRice(ICooker cooker)
    {
        cooker.FryRice(50, Level.Low);
        cooker.SaltRice(Level.None);
        cooker.PepperRice(Level.None);
    }

    public void PrepareChicken(ICooker cooker)
    {
        cooker.FryChicken(50, Level.Low);
        cooker.SaltChicken(Level.None);
        cooker.PepperChicken(Level.None);
    }
}