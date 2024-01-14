using ManagingState.Task1_IndianRestaurantApp.Cookers;
using ManagingState.Task1_IndianRestaurantApp;
using ManagingState.Task1_IndianRestaurantApp.Entities;
using ManagingState.Task1_IndianRestaurantApp.Factories;

namespace ManagingState;

internal class Program
{
    static void Main(string[] args)
    {
        var restaurant = new Restaurant();
        ICooker cooker = CookerFactory.GetCooker(Country.India);
        var currentDate = DateTime.Now;
        restaurant.CookMasala(cooker, Country.India, currentDate);
    }
}