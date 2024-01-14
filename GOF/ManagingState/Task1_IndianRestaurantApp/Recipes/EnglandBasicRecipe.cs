using ManagingState.Task1_IndianRestaurantApp.Cookers;
using ManagingState.Task1_IndianRestaurantApp.Entities;

namespace ManagingState.Task1_IndianRestaurantApp.Recipes;

public class EnglandBasicRecipe : IRecipe
{
    public void PrepareRice(ICooker cooker)
    {
        cooker.FryRice(100, Level.Low);
        cooker.SaltRice(Level.None);
        cooker.PepperRice(Level.None);
    }

    public void PrepareChicken(ICooker cooker)
    {
        cooker.FryChicken(100, Level.Low);
        cooker.SaltChicken(Level.None);
        cooker.PepperChicken(Level.None);
    }
}