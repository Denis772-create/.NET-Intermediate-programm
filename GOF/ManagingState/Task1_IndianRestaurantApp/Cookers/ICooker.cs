using ManagingState.Task1_IndianRestaurantApp.Entities;

namespace ManagingState.Task1_IndianRestaurantApp.Cookers;

public interface ICooker
{
    void FryRice(int amount, Level level);
    void FryChicken(int amount, Level level);
    void SaltRice(Level level);
    void SaltChicken(Level level);
    void PepperRice(Level level);
    void PepperChicken(Level level);
}