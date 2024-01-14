using ManagingState.Task1_IndianRestaurantApp.Cookers;

namespace ManagingState.Task1_IndianRestaurantApp.Recipes;

public interface IRecipe
{
    void PrepareRice(ICooker cooker);
    void PrepareChicken(ICooker cooker);
}
