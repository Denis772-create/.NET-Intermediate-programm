using ManagingState.Task1_IndianRestaurantApp.Entities;
using ManagingState.Task1_IndianRestaurantApp.Recipes;

namespace ManagingState.Task1_IndianRestaurantApp.Factories;

public class SummerRecipeFactory : IRecipeFactory
{
    public IRecipe GetRecipe(Country country)
    {
        return country switch
        {
            Country.India => new IndiaSummerRecipe(),
            Country.Ukraine => new UkraineSummerRecipe(),
            Country.England => new EnglandSummerRecipe(),
            _ => throw new NotImplementedException()
        };
    }
}