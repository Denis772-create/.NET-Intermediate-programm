using ManagingState.Task1_IndianRestaurantApp.Cookers;
using ManagingState.Task1_IndianRestaurantApp.Entities;

namespace ManagingState.Task1_IndianRestaurantApp.Recipes;

public class IndiaBasicRecipe : IRecipe
{
    public void PrepareRice(ICooker cooker)
    {
        cooker.FryRice(200, Level.Strong);
        cooker.SaltRice(Level.Strong);
        cooker.PepperRice(Level.Strong);
    }

    public void PrepareChicken(ICooker cooker)
    {
        cooker.FryChicken(100, Level.Strong);
        cooker.SaltChicken(Level.Strong);
        cooker.PepperChicken(Level.Strong);
    }
}