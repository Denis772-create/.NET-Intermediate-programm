using ManagingState.Task1_IndianRestaurantApp.Cookers;
using ManagingState.Task1_IndianRestaurantApp.Entities;
using ManagingState.Task1_IndianRestaurantApp.Factories;
using ManagingState.Task1_IndianRestaurantApp.Recipes;

namespace ManagingState.Task1_IndianRestaurantApp;

public class Restaurant
{
    public void CookMasala(ICooker cooker, Country country, DateTime currentTime)
    {
        var isSummer = currentTime.Month is >= 6 and <= 8;
        IRecipeFactory factory = isSummer 
            ? new SummerRecipeFactory() 
            : new BasicRecipeFactory();

        IRecipe recipe = factory.GetRecipe(country);
        recipe.PrepareRice(cooker);
        recipe.PrepareChicken(cooker);
    }
}