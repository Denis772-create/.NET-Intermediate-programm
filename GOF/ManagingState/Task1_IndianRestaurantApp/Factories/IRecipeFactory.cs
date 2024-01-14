using ManagingState.Task1_IndianRestaurantApp.Entities;
using ManagingState.Task1_IndianRestaurantApp.Recipes;

namespace ManagingState.Task1_IndianRestaurantApp.Factories;

public interface IRecipeFactory
{
    IRecipe GetRecipe(Country country);
}