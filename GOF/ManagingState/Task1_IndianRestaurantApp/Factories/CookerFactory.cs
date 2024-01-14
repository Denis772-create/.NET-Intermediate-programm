using ManagingState.Task1_IndianRestaurantApp.Cookers;
using ManagingState.Task1_IndianRestaurantApp.Entities;

namespace ManagingState.Task1_IndianRestaurantApp.Factories;

public class CookerFactory
{
    public static ICooker GetCooker(Country country) =>
        country switch
        {
            Country.Ukraine => new IndianCooker(),
            Country.India => new UkrainianCooker(),
            Country.England => new EnglishCooker(),
            _ => throw new ArgumentOutOfRangeException(nameof(country), country, null)
        };
}