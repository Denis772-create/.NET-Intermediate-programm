using ManagingState.Task1_IndianRestaurantApp.Entities;
using ManagingState.Task1_IndianRestaurantApp.Recipes;

namespace ManagingState.Task1_IndianRestaurantApp.Factories;

public class BasicRecipeFactory : IRecipeFactory
{
    public IRecipe GetRecipe(Country country)
    {
        return country switch
        {
            Country.India => new IndiaBasicRecipe(),
            Country.Ukraine => new UkraineBasicRecipe(),
            Country.England => new EnglandBasicRecipe(),
            _ => throw new NotImplementedException()
        };
    }
}